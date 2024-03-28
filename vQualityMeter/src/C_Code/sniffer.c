#include "QualityMeter_lib.h"

// pthread_mutex_t mutex;

// Variables
ThreadPool pool; // Thread pool
eth_t eth; // Ethernet socket structure


// Packet Processing
idxThreads = 0;
uint8_t frameCaptured[NUM_THREADS][2048]; // Buffer to store the captured packets

void* processPacket(void* args){

    return NULL;
}

void runSniffer(){
    int32_t rx_bytes = 0;
    struct msghdr msg_hdr;
    struct iovec iov;
    memset(&msg_hdr, 0, sizeof(msg_hdr));
    memset(&iov, 0, sizeof(iov));

    msg_hdr.msg_name = &eth.bind_addr;
    msg_hdr.msg_namelen = eth.bind_addrSize;

    iov.iov_base = eth.rx_buffer;
    iov.iov_len = eth.rx_size;

    msg_hdr.msg_name = NULL;
    msg_hdr.msg_iov = &iov;
    msg_hdr.msg_iovlen = 1;
    msg_hdr.msg_control = NULL;
    msg_hdr.msg_controllen = 0;

    while(1) {
        rx_bytes = recvmsg(eth.socket, &msg_hdr, 0);
        if (rx_bytes) {
            memcpy(frameCaptured[idxThreads], eth.rx_buffer, rx_bytes); // Copy the packet to the buffer
            thread_pool_submit(&pool, processPacket, &frameCaptured[idxThreads][0]);
            idxThreads++;
            if (idxThreads == NUM_THREADS) idxThreads = 0;
        }
    }

}

void cleanup(int signum){
    printf("Leaving Sniffer...\n");
    socketCleanup(&eth);
    // deleteSharedMemory(&stopFlagShm);
    // pthread_mutex_destroy(&mutex);
    pthread_mutex_destroy(&pool.task_queue->mutex);
    exit(0);
}

int main(int argc, char *argv[]){
    // pthread_mutex_init(&mutex, NULL);

    // External Close up signals
    signal(SIGINT, cleanup);
    signal(SIGTERM, cleanup);

    // Initialize the Thread Pool
    thread_pool_init(&pool);

    // Initialize the Ethernet Socket
    eth.fanout_grp = 2;
    socketSetup(&eth, 0, 2048); // Tx size = 0; Rx Size = 2048
    if (createSocket(&eth, argv[1]) != 0){
        printf("Error creating socket\n");
        pthread_mutex_destroy(&pool.task_queue->mutex);
        return -1;
    }

    runSniffer();

    printf("hello World!");

    cleanup(0);
    return 0;
}
