#include "shmMemory.h"
#include "mySocket.h"
#include "threadTask.h"
#include <fftw3.h>
#include <time.h>
#include <pthread.h>
#include <signal.h>
#include <math.h>
#include <string.h>
#include <stdint.h>
#include <unistd.h>
#include <sched.h>

#define MAX_THREADS 4
#define MAX_RAW 80
#define NUM_DFT_PER_CYCLE 4
#define NUM_THREADS 4
#define TASK_QUEUE_SIZE 5
#define NUM_CHANELS 8
ThreadPool pool;

double *values;
int32_t rawValues[MAX_RAW][8];
int32_t idxRaw = 0;
uint8_t updateFlag = 1;

uint8_t macSrc[6];
const char* svId;

#define SHM_USED 1
shm_setup_s* allShm[SHM_USED];

uint8_t frameCaptured[MAX_THREADS][2048];
int idxThreads = 0;
pthread_mutex_t mutex;

int pktProcessed = 0;

double* dftInput;
fftw_complex* dftOuput;
fftw_plan dftPlan;

struct eth_t *eth_p;

pthread_t watchDogThread;


void prepareDFT()
{
    dftInput = (double *)fftw_malloc(sizeof(double) * MAX_RAW);
    dftOuput = (fftw_complex *)fftw_malloc(sizeof(fftw_complex) * MAX_RAW);
    dftPlan = fftw_plan_dft_r2c_1d(MAX_RAW, dftInput, dftOuput, FFTW_ESTIMATE);
}

void deleteDFT()
{
    fftw_destroy_plan(dftPlan);
    fftw_free(dftInput);
    fftw_free(dftOuput);
}

void* watchDogTask(){
    while (1){
        if (updateFlag) updateFlag = 0;
        else {
            for(int i=0;i<NUM_CHANELS;i++){
                values[2*i] = 0;
                values[2*i+1] = 0; 
            }
        }
        usleep(300 * 1000);
    }
    return NULL;
}

void computeDFT() {
    updateFlag = 1;
    for (int j = 0; j < 8; j++) {

        int i = idxRaw;
        for(int k=0;k<MAX_RAW;k++){
            dftInput[i] = rawValues[i][j];
            i = (i+1)%MAX_RAW;
        }

        fftw_execute(dftPlan);

        values[j*2] = sqrt(2*(dftOuput[1][0]*dftOuput[1][0] + dftOuput[1][1]*dftOuput[1][1]))/MAX_RAW;
        values[j*2+1] =  atan2(dftOuput[1][0], dftOuput[1][1]);
    }
    // for(int i=0;i<8;i++) printf("%lf |_%lf\n", values[i+i],values[i+i+1]);
    // printf("\n");
}

void* processPacket(void* args){

    uint8_t* frame = (uint8_t *) args;

    if (frame[0] == 0x88 && frame[1] == 0xba)
        return NULL;
    
    int i = 0, j=0, noAsdu;
    if (frame[12] == 0x81 && frame[13] == 0x00)
        i = 16;
    else
        i = 12;
    if (!(frame[i] == 0x88 && frame[i+1] == 0xba))
        return NULL;


    if (frame[i+11] == 0x82)
        i += 17;
    else
        i += 15;

    noAsdu = frame[i-1];
    if (frame[i+1] == 0x82) // Seq Asdu
        i += 4;
    else
        i += 2;
    for (int k = 0; k < noAsdu; k++)
    {
        if (frame[i+1] == 0x82) // Asdu Tag
            i += 4;
        else
            i += 2;
        while (frame[i] != 0x87)
        {
            if (frame[i] == 0x80){
                if (memcmp(svId, &frame[i+2], frame[i+1]) != 0)
                    return NULL;
            }
            i += frame[i+1] + 2;
        }
        j = 0;
        pthread_mutex_lock(&mutex);
        while (j < frame[i+1])
        {
            rawValues[idxRaw][j/8] = 0;
            rawValues[idxRaw][j/8] |= (int)frame[i+2+j] << 24;
            rawValues[idxRaw][j/8] |= (int)frame[i+3+j] << 16;
            rawValues[idxRaw][j/8] |= (int)frame[i+4+j] << 8;
            rawValues[idxRaw][j/8] |= (int)frame[i+5+j];
            j += 8;
        }
        if (idxRaw % (MAX_RAW / NUM_DFT_PER_CYCLE) == 0) {
            
            struct timespec t0, t1;
            // clock_gettime(0, &t0);
            computeDFT();
            // clock_gettime(0, &t1);
            // printf("Time: %ld us\n", (t1.tv_nsec - t0.tv_nsec)/1000);
        }
        idxRaw = (idxRaw + 1) % MAX_RAW;
        pthread_mutex_unlock(&mutex);
        i += frame[i+1] + 2;
    }

    return NULL;
}

int runSniffer()
{
    int32_t rx_bytes = 0;

    struct msghdr msg_hdr;
    struct iovec iov;
    memset(&msg_hdr, 0, sizeof(msg_hdr));
    memset(&iov, 0, sizeof(iov));

    msg_hdr.msg_name = &eth_p->bind_addr;
    msg_hdr.msg_namelen = eth_p->bind_addrSize;
    
    msg_hdr.msg_name = NULL;
    msg_hdr.msg_iov = &iov;
    msg_hdr.msg_iovlen = 1;
    msg_hdr.msg_control = NULL;
    msg_hdr.msg_controllen = 0;

    iov.iov_base = eth_p->rx_buffer;
    iov.iov_len = eth_p->rx_size;

    while(1) {
        rx_bytes = recvmsg(eth_p->socket, &msg_hdr, 0);
        if (rx_bytes) {
            if (memcmp(macSrc, eth_p->rx_buffer, 6) == 0){
                memcpy(frameCaptured[idxThreads], eth_p->rx_buffer, rx_bytes);
                thread_pool_submit(&pool, processPacket, &frameCaptured[idxThreads][0]);
                idxThreads++;
                if (idxThreads == MAX_THREADS) idxThreads = 0;
            }
        }
    }

    return 0;
}

void cleanup(int signum){
    printf("Leaving Sniffer...\n");
    socketCleanup(eth_p);
    deleteDFT();
    pthread_mutex_destroy(&mutex);
    pthread_mutex_destroy(&pool.task_queue->mutex);
    for (int i = 0;i<SHM_USED;i++) deleteSharedMemory(allShm[i]);

    exit(0);
}

int main(int argc, char *argv[])
{   
    if (argc != 4){
        printf("Usage: ./sniffer <mac> <svId> <iface>\n");
        return 1;
    }

    struct sched_param paramS;
    paramS.sched_priority = 90;
    sched_setscheduler(0,SCHED_FIFO, &paramS);

    sscanf(argv[1], "%02hhx:%02hhx:%02hhx:%02hhx:%02hhx:%02hhx", macSrc, macSrc+1, macSrc+2, macSrc+3, macSrc+4, macSrc+5);
    svId = argv[2];
    printf("Running Sniffer with:\n");
    printf("    Mac: %02hhx:%02hhx:%02hhx:%02hhx:%02hhx:%02hhx\n", macSrc[0], macSrc[1], macSrc[2], macSrc[3], macSrc[4], macSrc[5]);
    printf("    svId: %s\n", svId);
    printf("    Interface: %s\n", argv[3]);

    signal(SIGINT, cleanup);
    signal(SIGTERM, cleanup);

    shm_setup_s valuesShm = createSharedMemory("phasor",2*NUM_CHANELS * sizeof(double));
    values = (double*) valuesShm.ptr;


    allShm[0] = &valuesShm;


    prepareDFT();

    pthread_mutex_init(&mutex, NULL);

    struct eth_t eth;
    eth_p = &eth;
    eth.fanout_grp = 4;
    socketSetup(&eth, 0, 2048);
    if (createSocket(&eth, argv[3]) != 0){
        printf("Error creating socket\n");
        return -1;
    }
    
    
    pthread_create(&watchDogThread, NULL, watchDogTask, NULL);
    

    thread_pool_init(&pool);

    runSniffer();

    cleanup(0);

    return 0;
}
