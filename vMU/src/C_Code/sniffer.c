#include "shmMemory.h"
#include "send.h"
#include "threadTask.h"
#include <fftw3.h>
#include <time.h>
#include <pthread.h>
#include <signal.h>
#include <math.h>
#include <string.h>
#include <stdint.h>

#define MAX_THREADS 4
#define MAX_RAW 80
#define NUM_THREADS 4
#define TASK_QUEUE_SIZE 5
ThreadPool pool;


int idxRaw = 0;

uint8_t macSrc[6];
const char* goId;
uint8_t *stopFlag;
uint8_t logicType;
uint8_t goInputSize;
uint8_t frameCaptured[NUM_THREADS][2048];
int32_t idxThreads = 0;
pthread_mutex_t mutex;

int pktProcessed = 0;

struct eth_t *eth_p;

uint64_t t_delay[2]={0};
struct timespec t0, t1;

void* processPacket(void* args)
{
    uint8_t* frame = (uint8_t *) args;

    if (memcmp(macSrc, frame, 6) != 0)
        return NULL;

    int32_t i = 0, j=0, noAsdu;
    if (frame[12] == 0x81 && frame[13] == 0x00)
        i = 16;
    else
        i = 12;
    if (!(frame[i] == 0x88 && frame[i+1] == 0xb8))
        return NULL;

    if (frame[i+11] == 0x82)
        i += 14;
    else
        i += 12;

    while (frame[i] != 0xab)
    {
        if (frame[i] == 0x83){
            if (memcmp(goId, &frame[i+2], frame[i+1]) != 0)
                return NULL;
        }
        i += frame[i+1] + 2;
    }
    uint16_t allDataSize = frame[i+1];
    j = 0; i += 2;
    uint8_t _stopFlag = 0;
    while(j<allDataSize){
        j += frame[i+1]+2;
        if (logicType) _stopFlag |= frame[i+2];
        else _stopFlag &= frame[i+2];
    }
    // if (*stopFlag != _stopFlag) printf("Data Changed on Sniffer");
    *stopFlag = _stopFlag;
    // if(*stopFlag) printf("Stop Flag: %u\n", *stopFlag);

    clock_gettime(CLOCK_MONOTONIC, &t1);
    t_delay[0] = t1.tv_sec - t0.tv_sec;
    if (t1.tv_nsec < t0.tv_nsec){
        t_delay[0] -= 1;
        t_delay[1] = 1000000000 - t0.tv_nsec + t1.tv_nsec;
    }
    else t_delay[1] = t1.tv_nsec - t0.tv_nsec;
    printf("Delay: %lfs\n", (double)((double)t_delay[0] + t_delay[1]/1e9));
    t0 = t1;

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

    iov.iov_base = eth_p->rx_buffer;
    iov.iov_len = eth_p->rx_size;

    msg_hdr.msg_name = NULL;
    msg_hdr.msg_iov = &iov;
    msg_hdr.msg_iovlen = 1;
    msg_hdr.msg_control = NULL;
    msg_hdr.msg_controllen = 0;

    while(1) {
        rx_bytes = recvmsg(eth_p->socket, &msg_hdr, 0);
        if (rx_bytes) {
            if (memcmp(macSrc, eth_p->rx_buffer, 6) == 0){
                memcpy(frameCaptured[idxThreads], eth_p->rx_buffer, rx_bytes);
                thread_pool_submit(&pool, processPacket, &frameCaptured[idxThreads][0]);
                idxThreads++;
                if (idxThreads == NUM_THREADS)
                    idxThreads = 0;
            }
        }
    }

    return 0;
}

shm_setup_s stopFlagShm;
void cleanup(int signum){
    printf("Leaving Sniffer...\n");
    socketCleanup(eth_p);
    deleteSharedMemory(&stopFlagShm);
    pthread_mutex_destroy(&mutex);
    pthread_mutex_destroy(&pool.task_queue->mutex);
    exit(0);
}

int main(int argc, char *argv[])
{   
    if (argc != 5){
        printf("Usage: ./sniffer <mac> <goId> <iface> <logicType>\n");
        return 1;
    }
    // Just for test, print all argv
    // for (int i = 0; i < argc; i++){
    //     printf("%s ", argv[i]);
    // }

    sscanf(argv[1], "%02hhx:%02hhx:%02hhx:%02hhx:%02hhx:%02hhx", macSrc, macSrc+1, macSrc+2, macSrc+3, macSrc+4, macSrc+5);
    goId = argv[2];
    printf("Running Sniffer with:\n");
    printf("    Mac: %02hhx:%02hhx:%02hhx:%02hhx:%02hhx:%02hhx\n", macSrc[0], macSrc[1], macSrc[2], macSrc[3], macSrc[4], macSrc[5]);
    printf("    goId: %s\n", goId);
    printf("    Interface: %s\n", argv[3]);
    printf("    Logic: %s\n", argv[4][0]=='0'? "OR":"AND");

    logicType = argv[4][0]=='0';

    signal(SIGINT, cleanup);
    signal(SIGTERM, cleanup);

    stopFlagShm = createSharedMemory("stopFlag", sizeof(uint8_t));
    stopFlag = (uint8_t*)  stopFlagShm.ptr;
    if (stopFlag == NULL)
        return -1;

    pthread_mutex_init(&mutex, NULL);

    struct eth_t eth;
    eth_p = &eth;
    eth.fanout_grp = 2;
    socketSetup(&eth, 0, 2048);
    if (createSocket(&eth, argv[3]) != 0){
        printf("Error creating socket\n");
        return -1;
    }
    
    thread_pool_init(&pool);

    runSniffer();

    cleanup(0);

    return 0;
}
