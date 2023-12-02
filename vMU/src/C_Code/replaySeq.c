#include "mySocket.h"
#include "shmMemory.h"
#include "timers.h"
#include <sched.h>
#include <signal.h>

#define ALL_SHM 5
shm_setup_s* allShm[ALL_SHM];

struct eth_t* eth_p;

void sigterm_handler(int signum) {
    printf("Sequencer Replay Leaving...\n");
    socketCleanup(&eth_p);
    for(int i = 0;i<ALL_SHM; i++)
        close(allShm[i]->id);
    exit(0);
}

struct time_struct_s {
    uint32_t sec;
    uint32_t nsec;
}typedef time_struct_s;

int main(int argc, char *argv[])
{
    printf("Running Sequencer Replay!\n");
    if (argc != 2)
        return -1;

    signal(SIGTERM, sigterm_handler);

    struct sched_param paramS;
    paramS.sched_priority = 80;
    sched_setscheduler(0,SCHED_FIFO, &paramS);


    // [0] -> Intergap; [1] -> Num of Sequences; [2] -> Buffer Size; [3] -> Frame Size
    shm_setup_s paramShm = openSharedMemory("sequencerParam",sizeof(uint32_t)*4);
    uint32_t* param =  (uint32_t*) paramShm.ptr;
    if (param == NULL)
        return -1;
    
    printf("With: Intergap: %u;Num of Sequences: %u; Buffer Size: %u; Packet Size: %u\n", param[0], param[1], param[2], param[3]);

    // frames[NoSequence][Buffers Size][Frame Size]
    shm_setup_s framesShm = openSharedMemory("sequencerFrames", param[1]*param[2]*param[3]);
    uint8_t (*frames)[param[2]][param[3]] = (uint8_t(*)[param[2]][param[3]]) framesShm.ptr;
    
    // Trip input
    shm_setup_s stopShm = openSharedMemory("stopFlag", sizeof(uint8_t));
    uint8_t* stop = (uint8_t*) stopShm.ptr;
    if (stop == NULL)
        return -1;

    // How many frames in each sequence
    shm_setup_s buffSizeShm = openSharedMemory("sequencerBufSize", sizeof(uint32_t)*param[1]);
    uint32_t* buffSize = (uint32_t*) buffSizeShm.ptr;
    if (buffSize == NULL)
        return -1;

    // 
    shm_setup_s timeShm = openSharedMemory("sequencerTime", 2*sizeof(uint64_t));
    uint64_t* time = (uint64_t*) timeShm.ptr;
    if (time == NULL)
        return -1;

    allShm[0] = &paramShm;
    allShm[1] = &framesShm;
    allShm[2] = &stopShm;
    allShm[3] = &buffSizeShm;
    allShm[4] = &timeShm;
    
    struct eth_t eth;
    eth.fanout_grp = 1;
    socketSetup(&eth, param[3], 0);
    if (createSocket(&eth, argv[1]) != 0)
        return -1;
    eth_p = &eth;

    int32_t tx_bytes;
    struct msghdr msg_hdr;
    struct iovec iov;
    memset(&msg_hdr, 0, sizeof(msg_hdr));
    memset(&iov, 0, sizeof(iov));
    msg_hdr.msg_name = &eth.bind_addr;
    msg_hdr.msg_namelen = eth.bind_addrSize;

    iov.iov_base = &frames[0][0][0];
    iov.iov_len = param[3];

    msg_hdr.msg_iov = &iov;
    msg_hdr.msg_iovlen = 1;

    struct period_info pinfo;
    int noSeq = 0, noBuf = 0, noFrame = 0;
    struct timespec t0, t1;
    periodic_task_init(&pinfo, param[0]);
    wait_rest_of_period(&pinfo);
    clock_gettime(CLOCK_MONOTONIC, &t0);
    while (*stop != 1){
        tx_bytes = sendmsg(eth.socket, &msg_hdr, 0);

        clock_gettime(CLOCK_MONOTONIC, &t1);

        time[0] = t1.tv_sec - t0.tv_sec;
        if (t1.tv_nsec < t0.tv_nsec){
            time[0] -= 1;
            time[1] = 1000000000 - t0.tv_nsec + t1.tv_nsec;
        }
        else time[1] = t1.tv_nsec - t0.tv_nsec;

        if (noBuf < param[2]) noBuf++;
        else noBuf = 0;
        
        if (noFrame < buffSize[noSeq]) noFrame++;
        else{
            noFrame = 0;
            noSeq++;
            if (noSeq >= param[1]) break;
        }

        iov.iov_base = &frames[noSeq][noBuf][0];
        wait_rest_of_period(&pinfo);
    }
    
    clock_gettime(CLOCK_MONOTONIC, &t1);
    time[0] = t1.tv_sec - t0.tv_sec;
    if (t1.tv_nsec < t0.tv_nsec){
        time[0] -= 1;
        time[1] = 1000000000 - t0.tv_nsec + t1.tv_nsec;
    }else time[1] = t1.tv_nsec - t0.tv_nsec;
    
    printf("Time Elapsed: %lfs\n", (double)((double)time[0] + time[1]/1e9));
    *stop = 1;
    
    socketCleanup(&eth);
    close(timeShm.id);
    close(stopShm.id);
    close(buffSizeShm.id);
    close(framesShm.id);
    close(paramShm.id);

    return 0;
}