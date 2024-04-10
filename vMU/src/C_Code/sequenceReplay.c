#include "sequenceReplay.h"

#define ALL_SHM 3
shm_setup_s allShm[ALL_SHM];
eth_t eth;

int32_t ***arrMatrix;
sequenceData_t* data;

void cleanUp(int signum) {
    printf("Sequencer Replay Leaving...\n");
    socketCleanup(&eth);
    for (int i = 0; i < data->seqNum; i++){
        free(arrMatrix[i]);
    }
    free(arrMatrix);
    for(int i = 0;i<ALL_SHM; i++)
        deleteSharedMemory(&allShm[i]);
    exit(0);
}

struct time_struct_s {
    uint32_t sec;
    uint32_t nsec;
}typedef time_struct_s;

int main(int argc, char *argv[])
{
    if (argc != 3){
        printf("Usage: %s <shmName> <interface>\n", argv[0]);
        return -1;
    }

    signal(SIGTERM, cleanUp);
    signal(SIGINT, cleanUp);

    // Set the priority of the process
    struct sched_param paramS;
    paramS.sched_priority = 80;
    sched_setscheduler(0,SCHED_FIFO, &paramS);

    // Open the shared memory for the struct data
    allShm[0] = openSharedMemory(argv[1], sizeof(sequenceData_t));
    if (allShm[0].ptr == NULL){
        printf("Error: Could not open shared memory\n");
        return -1;
    }
    data = (sequenceData_t*) allShm[0].ptr;
    printf("Starting Sequence %s\n", argv[1]);

    // Open Array and frame shared memory
    allShm[1] = openSharedMemory(data->frameShmName, data->frameLength* sizeof(int8_t));
    if (allShm[1].ptr == NULL)
        return -1;
    allShm[2] = openSharedMemory(data->arrShmName, data->arrLength * sizeof(int32_t));
    if (allShm[2].ptr == NULL)
        return -1;

    uint8_t *frame = (uint8_t*) allShm[1].ptr;

    int32_t *arr = (int32_t*) allShm[2].ptr;
    arrMatrix = (int32_t***) malloc(data->seqNum * sizeof(int32_t**));
    for (int i = 0; i < data->seqNum; i++){
        arrMatrix[i] = (int32_t**) malloc(data->n_channels * sizeof(int32_t*));
        for (int j = 0; j < data->n_channels; j++){
            arrMatrix[i][j] = &arr[i*data->n_channels*data->smpRate + j*data->smpRate];
        }
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
    uint64_t noFrame = 0;
    int8_t i_asdu, channel, noSeq = 0;
    int16_t smpCount = 0, i=0, maxSmpCount = data->smpRate * data->freq;
    struct timespec t0, t1;

    clock_gettime(CLOCK_MONOTONIC, &t0);
    while (1){
        for(i_asdu = 0; i_asdu < data->n_asdu; i_asdu++){
            frame[data->smpCountPos + (i_asdu*data->asduLength)] = (smpCount & 0xFF00) >> 8;
            frame[data->smpCountPos + (i_asdu*data->asduLength) + 1] = smpCount & 0x00FF;
            for (channel = 0; channel < data->n_channels; channel++){

                frame[data->allDataPos + (i_asdu*data->asduLength) + 8*channel] = (arrMatrix[noSeq][channel][i] & 0xFF000000) >> 24;
                frame[data->allDataPos + (i_asdu*data->asduLength) + 8*channel + 1] = (arrMatrix[noSeq][channel][i] & 0x00FF0000) >> 16;
                frame[data->allDataPos + (i_asdu*data->asduLength) + 8*channel + 2] = (arrMatrix[noSeq][channel][i] & 0x0000FF00) >> 8;
                frame[data->allDataPos + (i_asdu*data->asduLength) + 8*channel + 3] = arrMatrix[noSeq][channel][i] & 0x000000FF;
            }
            i++;
            if (i >= data->smpRate) i = 0;
            smpCount++;
            if (smpCount >= maxSmpCount) smpCount = 0;
            noFrame++;
            if (noFrame >= data->smpPerSeq[noSeq]){
                noFrame = 0;
                noSeq++;
                if (noSeq >= data->seqNum) 
                    goto end_of_loop;
            }
        }

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

end_of_loop:
    cleanUp(0);
    return 0;
}