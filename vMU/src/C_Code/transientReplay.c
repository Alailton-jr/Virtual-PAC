

#include "transientReplay.h"

#define ALL_SHM 3
shm_setup_s allShm[ALL_SHM];
eth_t eth;

void cleanUp(int signum){
    printf("Transient Replay Leaving...\n");
    socketCleanup(&eth);
    for(int i = 0;i<ALL_SHM; i++)
        deleteSharedMemory(&allShm[i]);
    exit(0);
}


int main(int argc, char *argv[]){

    if (argc < 3) {
        printf("Usage: ./transientReplay <shmName> <iface>\n");
        return -1;
    }

    printf("Transient Replay Starting...\n");

    // Signal handling for closing the program
    signal(SIGTERM, cleanUp);
    signal(SIGINT, cleanUp);

    // Set the priority of the process
    struct sched_param paramS;
    paramS.sched_priority = 80;
    sched_setscheduler(0,SCHED_FIFO, &paramS);

    // Open the shared memory for the struct data
    allShm[0] = openSharedMemory(argv[1],sizeof(transientData_t));
    if (allShm[0].ptr == NULL) return -1;
    transientData_t* data = (transientData_t*) allShm[0].ptr;

    //Open Array and frame shared memory
    allShm[1] = openSharedMemory(data->arrShmName, data->arrLength*sizeof(int32_t));
    if (allShm[1].ptr == NULL) return -1;
    allShm[2] = openSharedMemory(data->frameShmName, data->frameLength*sizeof(uint8_t));
    if (allShm[2].ptr == NULL) return -1;

    int32_t *arr = (int32_t*) allShm[1].ptr;
    uint8_t *frame = (uint8_t*) allShm[2].ptr;
    uint32_t nframes = data->arrLength/data->n_channels;

    // Socket setup
    eth.fanout_grp = 1;
    socketSetup(&eth, data->frameLength, 0);
    if (createSocket(&eth, argv[2]) != 0) return -1;
    int32_t tx_bytes;
    struct msghdr msg_hdr;
    struct iovec iov;
    memset(&msg_hdr, 0, sizeof(msg_hdr));
    memset(&iov, 0, sizeof(iov));
    msg_hdr.msg_name = &eth.bind_addr;
    msg_hdr.msg_namelen = eth.bind_addrSize;
    iov.iov_base = &frame[0];
    iov.iov_len = data->frameLength;
    msg_hdr.msg_iov = &iov;
    msg_hdr.msg_iovlen = 1;

    // Timer Setup
    struct period_info pinfo;
    periodic_task_init(&pinfo, data->interGap);
    
    // Main loop
    uint64_t i = 0;
    uint16_t smpCount = 0, i_asdu, tempSmpCount, channel;
    uint8_t debug = 0;
    while(1){
        if (i >= data->arrLength){
            if (data->isLoop) i = 0;
            else break;
        }
        // Adjust frame
        for(i_asdu = 0; i_asdu < data->n_asdu; i_asdu++){
            frame[data->smpCountPos + (i_asdu*data->asduLength)] = (smpCount & 0xFF00) >> 8;
            frame[data->smpCountPos + (i_asdu*data->asduLength) + 1] = smpCount & 0x00FF;
            for (channel = 0; channel < data->n_channels; channel++){
                if (channel == 4){
                    int xdsa = 2;
                }
                frame[data->allDataPos + (i_asdu*data->asduLength) + 8*channel] = (arr[i] & 0xFF000000) >> 24;
                frame[data->allDataPos + (i_asdu*data->asduLength) + 8*channel + 1] = (arr[i] & 0x00FF0000) >> 16;
                frame[data->allDataPos + (i_asdu*data->asduLength) + 8*channel + 2] = (arr[i] & 0x0000FF00) >> 8;
                frame[data->allDataPos + (i_asdu*data->asduLength) + 8*channel + 3] = arr[i] & 0x000000FF;
                i++;
            }
            smpCount++;
            if (smpCount >= data->maxSmpCount) smpCount = 0;
        }
        wait_rest_of_period(&pinfo);
        tx_bytes = sendmsg(eth.socket, &msg_hdr, 0);
    }
    cleanUp(0);

    printf("Transient Replay Leaving...\n");
    return 0;
}