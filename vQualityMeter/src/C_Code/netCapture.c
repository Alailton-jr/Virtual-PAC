#include "QualityMeter_lib.h"

eth_t eth;
ThreadPool pool;

#define MAX_CAPTURED 20
int idxThreads = 0;
uint8_t frameCaptured[20][2048];
FILE *fp[MAX_CAPTURED];
char filename[MAX_CAPTURED][50];
char svID[MAX_CAPTURED][MAX_CAPTURED];
struct timespec t0, t1;

void* processPacket(uint8_t* frame, uint64_t size){
    return NULL;
}

void runSniffer(double time){

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

    clock_gettime(0, &t0);
    clock_gettime(0, &t1);

    while(1) {
        rx_bytes = recvmsg(eth.socket, &msg_hdr, 0);
        if (rx_bytes) {
            memcpy(frameCaptured[idxThreads], eth.rx_buffer, rx_bytes); // Copy the packet to the buffer
            thread_pool_submit(&pool, processPacket, &frameCaptured[idxThreads][0], rx_bytes);
            idxThreads++;
            if (idxThreads == pool.numThreads) idxThreads = 0;
        }
        clock_gettime(0, &t1);
        if ((t1.tv_sec - t0.tv_sec) + (t1.tv_nsec - t0.tv_nsec)/1e9 > time) break;
    }
}

void cleanUp(int signum){
    printf("Cleaning up...\n");
    thread_pool_destroy(&pool);
    socketCleanup(&eth);
    exit(0);
}

int main(int argc, uint8_t* argv[]){

    // argv[1] -> Interface
    // argv[2] -> Time for capturing in milliseconds

    if (argc < 3){
        printf("Usage: %s <interface> <time>\n", argv[0]);
        return 1;
    }

    // Stop capturing after the time
    double time = atof(argv[2]);

    // External signals
    signal(SIGINT, cleanUp);
    signal(SIGTERM, cleanUp);

    // Socket creation
    socketSetup(&eth, 0, 2048); // Tx size = 0; Rx Size = 2048
    if (createSocket(&eth, argv[1]) != 0){
        printf("Error creating socket\n");
        return -1;
    }

    thread_pool_init(&pool, 2, 10);
    
    runSniffer(time);


}