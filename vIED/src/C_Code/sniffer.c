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

/*
    Obs: The definitions MAX_RAW and NUM_CHANNELS was used considering the 4800Hz Sampled Value rate
*/

//--------------- Thread Pool -----------------//
#define NUM_THREADS 4 // Number of threads in the pool
#define MAX_RAW 80 // Number of samples per cycle
#define NUM_DFT_PER_CYCLE 4 // Number of DFTs computation per cycle
#define TASK_QUEUE_SIZE 5 // Max number of tasks in the queue
#define NUM_CHANELS 8 // Number of channels
ThreadPool pool; // Thread pool

//---------------- Sampled Values --------------//
double *values; // Shared memory for sampled values
int32_t rawValues[MAX_RAW][8]; // Raw values from SV Packets
int32_t idxRaw = 0; // Index of the raw values
uint8_t macSrc[6]; // MAC address of the source
const char* svId; // ID of the sampled values

//--------------- Pkt Processing ---------------//
uint8_t frameCaptured[NUM_THREADS][2048]; // Buffer to store the captured packets
int idxThreads = 0; // Index of the thread to process the packet
pthread_mutex_t mutex; // Mutex to protect the raw values
pthread_t watchDogThread; // Thread to reset the values if no packet is received
uint8_t updateFlag = 1; // Flag to indicate if the values are updated

//------------------ DFT ----------------------//
int pktProcessed = 0; // Number of packets processed
double* dftInput; // Input of the DFT
fftw_complex* dftOuput; // Output of the DFT
fftw_plan dftPlan; // Plan of the DFT


#define SHM_USED 1 // Number of shared memories used
shm_setup_s* allShm[SHM_USED]; // Array of shared memories structure used
struct eth_t *eth_p; // Pointer to the ethernet structure

/*
    * Prepare the DFT plan and allocate the memory
*/
void prepareDFT(){
    dftInput = (double *)fftw_malloc(sizeof(double) * MAX_RAW);
    dftOuput = (fftw_complex *)fftw_malloc(sizeof(fftw_complex) * MAX_RAW);
    dftPlan = fftw_plan_dft_r2c_1d(MAX_RAW, dftInput, dftOuput, FFTW_ESTIMATE);
}

/*
    * Delete the DFT plan and free the memory
*/
void deleteDFT(){
    fftw_destroy_plan(dftPlan);
    fftw_free(dftInput);
    fftw_free(dftOuput);
}

/*
    * Watchdog task to reset the values if no packet is received
*/
void* watchDogTask(){
    while (1){
        if (updateFlag) updateFlag = 0;
        else {
            for(int i=0;i<NUM_CHANNELS;i++){
                values[2*i] = 0;
                values[2*i+1] = 0; 
            }
        }
        usleep(300 * 1000);
    }
    return NULL;
}

/*
    * Compute the DFT of the raw values and store in the shared memory
*/
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

/*
    * Process the SV packet and discard the others
    * @param args: Pointer to the packet buffer
*/
void* processPacket(void* args){

    uint8_t* frame = (uint8_t *) args;
    
    int i = 0, j=0, noAsdu;

    // Skip Ethernet and vLAN
    if (frame[12] == 0x81 && frame[13] == 0x00)  i = 16;
    else i = 12;

    if (!(frame[i] == 0x88 && frame[i+1] == 0xba)) return NULL; // Check if packet is SV

    // Skip SV Header until seqAsdu
    if (frame[i+11] == 0x82) i += 17;
    else i += 15;

    noAsdu = frame[i-1]; // Number of ASDUs

    // Skip until first asdu
    if (frame[i+1] == 0x82) i += 4;
    else i += 2;

    for (int k = 0; k < noAsdu; k++) { // Decode Each Asdu

        if (frame[i+1] == 0x82) i += 4;
        else i += 2;

        while (frame[i] != 0x87) { // skip all the fields until Sequence of Data
            /*
                * 0x80 -> svId
                * 0x81 -> datSet Name
                * 0x82 -> smpCnt
                * 0x83 -> confRev
                * 0x84 -> refrTm
                * 0x85 -> sympSync
                * 0x86 -> smpRate
                * 0x87 -> Sequence of Data
                * 0x88 -> SmpMod
                * 0x89 -> gmIdentity
            */
            if (frame[i] == 0x80){ // Check if svId is the same
                if (memcmp(svId, &frame[i+2], frame[i+1]) != 0)
                    return NULL;
            }
            i += frame[i+1] + 2; // Skip field Tag, Length and Value
        }
        j = 0;
        pthread_mutex_lock(&mutex); // Lock the raw values
        while (j < frame[i+1]) // Decode the raw values
        {
            // rawValues[idxRaw][j/8] = 0;
            // rawValues[idxRaw][j/8] |= (int)frame[i+2+j] << 24;
            // rawValues[idxRaw][j/8] |= (int)frame[i+3+j] << 16;
            // rawValues[idxRaw][j/8] |= (int)frame[i+4+j] << 8;
            // rawValues[idxRaw][j/8] |= (int)frame[i+5+j];
            rawValues[idxRaw][j/8] = ((int32_t) frame[i+2+j] << 24) | ((int32_t) frame[i+3+j] << 16) | ((int32_t) frame[i+4+j] << 8) | ((int32_t) frame[i+5+j]);
            j += 8;
        }
        if (idxRaw % (MAX_RAW / NUM_DFT_PER_CYCLE) == 0) { // Compute the DFT
            
            struct timespec t0, t1;
            // clock_gettime(0, &t0);
            computeDFT();
            // clock_gettime(0, &t1);
            // printf("Time: %ld us\n", (t1.tv_nsec - t0.tv_nsec)/1000);
        }
        idxRaw++;
        if (idxRaw == MAX_RAW) idxRaw = 0;
        pthread_mutex_unlock(&mutex); // Unlock the raw values
        i += frame[i+1] + 2;
    }

    return NULL;
}

/*
    * Run the sniffer
*/
int32_t runSniffer()
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
                memcpy(frameCaptured[idxThreads], eth_p->rx_buffer, rx_bytes); // Copy the packet to the buffer
                thread_pool_submit(&pool, processPacket, &frameCaptured[idxThreads][0]); // Submit the task to the thread pool
                idxThreads++; // Increment the index of the thread
                if (idxThreads == NUM_THREADS) idxThreads = 0; // Reset the index of the thread
            }
        }
    }

    return 0;
}

/*
    * Cleanup the sniffer
    * @param signum: Signal number
*/
void cleanup(int signum){
    printf("Leaving Sniffer...\n");
    socketCleanup(eth_p);
    deleteDFT();
    pthread_mutex_destroy(&mutex);
    pthread_mutex_destroy(&pool.task_queue->mutex);
    for (int i = 0;i<SHM_USED;i++) deleteSharedMemory(allShm[i]);

    exit(0);
}

/*
    * Main function
    * @param argc: Number of arguments
    * @param argv: Array of arguments
*/
int32_t main(int argc, char *argv[])
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

    signal(SIGINT, cleanup); // Register the cleanup function
    signal(SIGTERM, cleanup); // Register the cleanup function

    // Create the shared memory for the sampled values
    shm_setup_s valuesShm = createSharedMemory("phasor",2*NUM_CHANNELS * sizeof(double));
    values = (double*) valuesShm.ptr;
    allShm[0] = &valuesShm;

    prepareDFT(); // Prepare the DFT
    pthread_mutex_init(&mutex, NULL); // Initialize the mutex

    // Setup the socket
    struct eth_t eth;
    eth_p = &eth;
    eth.fanout_grp = 4;
    socketSetup(&eth, 0, 2048);
    if (createSocket(&eth, argv[3]) != 0){
        printf("Error creating socket\n");
        return -1;
    }
    
    
    pthread_create(&watchDogThread, NULL, watchDogTask, NULL); // Create the watchdog thread
    thread_pool_init(&pool); // Initialize the thread pool
    runSniffer(); // Run the sniffer on the main thread
    cleanup(0);
    return 0;
}
