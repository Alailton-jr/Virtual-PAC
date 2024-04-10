#include "continuousReplay.h"

#define ALL_SHM 3
shm_setup_s allShm[ALL_SHM];
eth_t eth;
int32_t** arrMatrix;
continuousData_t* data;

void cleanup(int signum) {
    printf("Continuous Replay Leaving...\n");
    socketCleanup(&eth);
    free(arrMatrix);
    for(int i = 0;i<ALL_SHM; i++)
        close(allShm[i].id);
    exit(0);
}

int main(int argc, char *argv[])
{

    if (argc != 3){
        printf("Usage: %s <shmName> <interface>\n", argv[0]);
        return -1;
    }


    signal(SIGTERM, cleanup);
    signal(SIGINT, cleanup);

    // Set the priority of the process
    struct sched_param paramS;
    paramS.sched_priority = 80;
    sched_setscheduler(0,SCHED_FIFO, &paramS);

    // Open the shared memory for the struct data
    allShm[0] = openSharedMemory(argv[1], sizeof(continuousData_t));
    if (allShm[0].ptr == NULL){
        printf("Error: Could not open shared memory\n");
        return -1;
    }
    data = (continuousData_t*) allShm[0].ptr;
    printf("Starting Continuous %s\n", argv[1]);

    // Open Array and frame shared memory
    allShm[1] = openSharedMemory(data->frameShmName, data->frameLength* sizeof(int8_t));
    if (allShm[1].ptr == NULL)
        return -1;
    allShm[2] = openSharedMemory(data->arrShmName, data->arrLength * sizeof(int32_t));
    if (allShm[2].ptr == NULL)
        return -1;

    uint8_t *frame = (uint8_t*) allShm[1].ptr;
    int32_t *arr = (int32_t*) allShm[2].ptr;

    arrMatrix = (int32_t**) malloc(data->n_channels*sizeof(int32_t**));
    for (int i = 0; i < data->n_channels; i++){
        arrMatrix[i] = &arr[i*data->smpRate];
    }

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

    // Main Loop
    struct timespec t0, t1;

    int8_t i_asdu, i_channel;
    int16_t smpCount = 0, i_smp = 0, maxSmpCount = data->smpRate * data->freq;
    clock_gettime(CLOCK_MONOTONIC, &t0);
    while(1){
        
        // Fill the frame
        for(i_asdu = 0; i_asdu < data->n_asdu; i_asdu++){
            frame[data->smpCountPos + (i_asdu*data->asduLength)] = (smpCount & 0xFF00) >> 8;
            frame[data->smpCountPos + (i_asdu*data->asduLength) + 1] = smpCount & 0x00FF;
            for(i_channel = 0; i_channel < data->n_channels; i_channel++){
                frame[data->allDataPos + (i_asdu*data->asduLength) + 8*i_channel] = (arrMatrix[i_channel][i_smp] & 0xFF000000) >> 24;
                frame[data->allDataPos + (i_asdu*data->asduLength) + 8*i_channel + 1] = (arrMatrix[i_channel][i_smp] & 0x00FF0000) >> 16;
                frame[data->allDataPos + (i_asdu*data->asduLength) + 8*i_channel + 2] = (arrMatrix[i_channel][i_smp] & 0x0000FF00) >> 8;
                frame[data->allDataPos + (i_asdu*data->asduLength) + 8*i_channel + 3] = arrMatrix[i_channel][i_smp] & 0x000000FF;
            }
            i_smp++;
            if(i_smp >= data->smpRate) i_smp = 0;
            smpCount++;
            if (smpCount >= maxSmpCount) smpCount = 0;
        }

        // Send the frame
        wait_rest_of_period(&pinfo);
        tx_bytes = sendmsg(eth.socket, &msg_hdr, 0);

        // Get the time of the last sent frame
        clock_gettime(CLOCK_MONOTONIC, &t1);
        data->elapsedTime[0] = t1.tv_sec - t0.tv_sec;
        if (t1.tv_nsec < t0.tv_nsec){
            data->elapsedTime[0] -= 1;
            data->elapsedTime[1] = 1000000000 - t0.tv_nsec + t1.tv_nsec;
        }
        else data->elapsedTime[1] = t1.tv_nsec - t0.tv_nsec;
    }

    cleanup(0);

    return 0;
}