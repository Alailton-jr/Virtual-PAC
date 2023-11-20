#include "send.h"
#include "shmMemory.h"
#include "timers.h"
#include <sched.h>
#include <signal.h>

#define ALL_SHM 3
shm_setup_s* allShm[ALL_SHM];

struct eth_t eth;
void sigterm_handler(int signum) {
    printf("Replay Leaving...\n");
    socketCleanup(&eth);
    for(int i = 0;i<ALL_SHM; i++)
        close(allShm[i]->id);
    exit(0);
}



int main(int argc, char *argv[])
{
    printf("Running Replay!\n");
    if (argc != 2)
        return -1;

    signal(SIGTERM, sigterm_handler);

    struct sched_param paramS;
    paramS.sched_priority = 80;
    sched_setscheduler(0,SCHED_FIFO, &paramS);

    // [0] -> Intergap; [1] -> Buffer Size; [2] -> Frame Size
    shm_setup_s paramShm = openSharedMemory("continousParam",sizeof(uint32_t)*3);
    uint32_t* param = (uint32_t*) paramShm.ptr;
    if (param == NULL)
        return -1;
    
    printf("With: Intergap: %u; Buffer Size: %u; Packet Size: %u\n", param[0], param[1], param[2]);
    shm_setup_s framesShm = openSharedMemory("continousFrames", param[1]*param[2]);
    uint8_t (*frames)[param[2]] = (uint8_t(*)[param[2]]) framesShm.ptr;
    if (frames == NULL)
        return -1;
    
    shm_setup_s stopShm = openSharedMemory("continousStop", sizeof(uint8_t));
    uint8_t* stop = (uint8_t *) stopShm.ptr;

    allShm[0] = &paramShm;
    allShm[1] = &framesShm;
    allShm[2] = &stopShm;

    eth.fanout_grp = 1;
    socketSetup(&eth, param[2], 0);
    if (createSocket(&eth, argv[1]) != 0)
        return -1;

    int32_t tx_bytes;
    struct msghdr msg_hdr;
    struct iovec iov;
    memset(&msg_hdr, 0, sizeof(msg_hdr));
    memset(&iov, 0, sizeof(iov));
    msg_hdr.msg_name = &eth.bind_addr;
    msg_hdr.msg_namelen = eth.bind_addrSize;
    iov.iov_base = &frames[0][0];
    iov.iov_len = param[2];
    msg_hdr.msg_iov = &iov;
    msg_hdr.msg_iovlen = 1;

    struct period_info pinfo;
    periodic_task_init(&pinfo, param[0]);
    int i = 0;
    // memcpy(eth.tx_buffer, frames+(i*param[2]), param[2]);

    wait_rest_of_period(&pinfo);
    while (*stop != 1)
    {
        tx_bytes = sendmsg(eth.socket, &msg_hdr, 0);
        // sendFrame(frames+(i*param[2]),param[2], pcap_handle);
        if (i < param[1])
            i++;
        else
            i = 0;
        iov.iov_base = &frames[i][0];
        // memcpy(eth.tx_buffer, &frames[i][0], param[2]);
        wait_rest_of_period(&pinfo);
    }
    socketCleanup(&eth);

    close(paramShm.id);
    close(stopShm.id);
    close(framesShm.id);


    return 0;
}