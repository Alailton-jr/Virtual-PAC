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

//--------------- Thread Pool -----------------//
#define NUM_THREADS 4 // Number of threads in the pool
#define TASK_QUEUE_SIZE 5 // Max number of tasks in the queue
ThreadPool pool; // Thread pool

//------------- Pkt Processing ----------------//
uint8_t macSrc[6]; // MAC address of the source
const char* goId; // ID of the sampled values
uint8_t *stopFlag; // Flag to indicate if the values are updated
uint8_t logicType; // Type of logic to be used
uint8_t frameCaptured[NUM_THREADS][2048]; // Buffer to store the captured packets
int32_t idxThreads = 0; // Index of the thread to process the packet
pthread_mutex_t mutex; // Mutex to protect the raw values
shm_setup_s stopFlagShm; // Shared memory for stop flag

eth_t *eth_p; // Pointer to the ethernet structure

/*
    * Process the packet captured
    * @param args: pointer to the packet captured
*/
void* processPacket(void* args){

    uint8_t* frame = (uint8_t *) args;

    int32_t i = 0, j=0, noAsdu;

    // Skip Ethernet and vLAN
    if (frame[12] == 0x81 && frame[13] == 0x00) i = 16;
    else i = 12;

    if (!(frame[i] == 0x88 && frame[i+1] == 0xb8)) return NULL; // Check if packet is GOOSE

    // Skip GOOSE header
    if (frame[i+11] == 0x82) i += 14;
    else i += 12;
   
    while (frame[i] != 0xab) {  // skip all the fields until allData
        /*
            * 0x80 -> gocbRef
            * 0x81 -> timeAllowedtoLive
            * 0x82 -> datSet Name
            * 0x83 -> goID
            * 0x84 -> T
            * 0x85 -> stNum
            * 0x86 -> sqNum
            * 0x87 -> simulation
            * 0x88 -> confRev
            * 0x89 -> ndsCom
            * 0x8a -> numDatSetEntries
            * 0x8b -> allData
        */
        if (frame[i] == 0x83){ // Check if it is the goID
            if (memcmp(goId, &frame[i+2], frame[i+1]) != 0)
                return NULL;
        }
        i += frame[i+1] + 2; // Skip field Tag, Length and Value
    }

    uint16_t allDataSize = frame[i+1];
    j = 0; i += 2;
    uint8_t _stopFlag = 0;
    while(j<allDataSize){
        j += frame[i+1]+2;
        if (logicType) _stopFlag |= frame[i+2];
        else _stopFlag &= frame[i+2];
    }
    *stopFlag = _stopFlag;
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
                memcpy(frameCaptured[idxThreads], eth_p->rx_buffer, rx_bytes); // Copy the packet to the buffer
                thread_pool_submit(&pool, processPacket, &frameCaptured[idxThreads][0]);
                idxThreads++;
                if (idxThreads == NUM_THREADS)
                    idxThreads = 0;
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

    // Create the shared memory for the stop flag
    stopFlagShm = createSharedMemory("stopFlag", sizeof(uint8_t));
    stopFlag = (uint8_t*)  stopFlagShm.ptr;
    if (stopFlag == NULL)
        return -1;

    pthread_mutex_init(&mutex, NULL); // Initialize the mutex

    // Socket setup
    eth_t eth;
    eth_p = &eth;
    eth.fanout_grp = 2;
    socketSetup(&eth, 0, 2048);
    if (createSocket(&eth, argv[3]) != 0){
        printf("Error creating socket\n");
        return -1;
    }
    
    thread_pool_init(&pool); // Initialize the thread pool
    runSniffer(); // Run the sniffer

    cleanup(0);

    return 0;
}
