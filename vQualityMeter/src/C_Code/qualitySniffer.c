#include "QualityMeter_lib.h"

// pthread_mutex_t mutex;

// Variables
ThreadPool pool; // Thread pool
eth_t eth; // Ethernet socket structure
sampledValue_t* sampledValues; // Array of sampled values

double meanTime[MAX_SAMPLED_VALUES];

// Packet Processing
int idxThreads = 0;
uint8_t **frameCaptured;

void cleanup(int signum);

// FILE *fp;

void saveArray(sampledValue_t* sv){
    printf("Saving Array\n");
    FILE *fp = fopen("Data.csv", "w");
    if (fp == NULL) {
        printf("Error opening file.\n");
        return;
    }
    for (int i = 0; i < sv->freq; i++) {
        for (int j = 0; j < sv->smpRate; j++) {
            fprintf(fp, "%d", sv->snifferArr[0][i][j]);
            if (j < sv->smpRate - 1) {
                fprintf(fp, ",");
            }
        }
        fprintf(fp, "\n");
    }
    fclose(fp);
    cleanup(0);
}

void* watchDogSv(){
    while(1){
        for (int i = 0; i < MAX_SAMPLED_VALUES; i++){
            if (sampledValues[i].initialized){
                if (sampledValues[i].nPackets == 0){
                    sampledValues[i].found = 0;
                }else{
                    sampledValues[i].nPackets = 0;
                    sampledValues[i].found = 1;
                }
            }
        }
        usleep(1000000);
    }
}

void* processPacket(uint8_t* frame, int32_t size){
    // return NULL;

    // uint8_t* frame = (uint8_t *) args;
    
    int i = 0;

    // Skip Ethernet and vLAN
    if (frame[12] == 0x81 && frame[13] == 0x00)  i = 16;
    else i = 12;

    if (!(frame[i] == 0x88 && frame[i+1] == 0xba)) return NULL; // Check if packet is SV

    int j=0, noAsdu, svIdx = -1;

    // 10 + 

    // Skip SV Header until seqAsdu
    if (frame[i+11] == 0x82){
        i += 17;
    }else if (frame[i+11] == 0x81){
        i += 16;
    }else{
        i += 15;
    }

    noAsdu = frame[i-1]; // Number of ASDUs

    // Skip until first asdu
    if (frame[i+1] == 0x82) {
        i += 4;
    } else if(frame[i+1] == 0x81){
        i += 3;
    } else {
        i += 2;
    }

    for (int k = 0; k < noAsdu; k++) { // Decode Each Asdu

        if (frame[i+1] == 0x82) 
            i += 4;
        else 
            i += 2;
        uint32_t debug;
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
                for (int idx = 0; idx < MAX_SAMPLED_VALUES; idx++){
                    if (sampledValues[idx].initialized){
                        char* debug = sampledValues[idx].svId;
                        if (memcmp(sampledValues[idx].svId, &frame[i+2], frame[i+1]) == 0){
                            svIdx = idx;
                            sampledValues[svIdx].nPackets++;
                            break;
                        }
                    } 
                }
            }
            i += frame[i+1] + 2; // Skip field Tag, Length and Value
            if (i >= size) return NULL;
        }
        j = 0;
        if (svIdx == -1) return NULL;
        // pthread_mutex_lock(&mutex); // Lock the raw values
        
        while (j < frame[i+1]) // Decode the raw values
        {
            if (j > 8*sampledValues[svIdx].numChanels) return NULL;
            sampledValues[svIdx].snifferArr[j/8][sampledValues[svIdx].idxBuffer][sampledValues[svIdx].idxCycle] = (int32_t)((frame[i+5+j]) | (frame[i+4+j]*256) | (frame[i+3+j]*65536) | (frame[i+2+j]*16777216));
            j += 8;
        }
        // if (sampledValues[svIdx].idxBuffer == 0 && sampledValues[svIdx].idxCycle == 0){
        //     clock_gettime(0, &sampledValues[svIdx].t0);
        // }

        sampledValues[svIdx].idxCycle++;
        if (sampledValues[svIdx].idxCycle >= sampledValues[svIdx].smpRate) {
            sampledValues[svIdx].idxCycle = 0;
            sampledValues[svIdx].idxBuffer++;
            sampledValues[svIdx].cycledCaptured++;
            if (sampledValues[svIdx].idxBuffer >= sampledValues[svIdx].freq) {
                sampledValues[svIdx].idxBuffer = 0;
                // clock_gettime(0, &sampledValues[svIdx].t1);
                // sampledValues[svIdx].meanTime = sampledValues[svIdx].nPackets/((sampledValues[svIdx].t1.tv_sec - sampledValues[svIdx].t0.tv_sec) + (sampledValues[svIdx].t1.tv_nsec - sampledValues[svIdx].t0.tv_nsec)/1e9)*1e6;
            }
        }
        
        i += frame[i+1] + 2;
    }
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
            thread_pool_submit(&pool, processPacket, &frameCaptured[idxThreads][0], rx_bytes);
            idxThreads++;
            if (idxThreads == NUM_THREADS) idxThreads = 0;
        }
    }
}

void cleanup(int signum){
    printf("Leaving Sniffer...\n");
    socketCleanup(&eth);
    thread_pool_destroy(&pool);
    exit(0);
}

void sigsegv_handler(int signum) {
    FILE *log_file = fopen("segfault.log", "a");
    if (log_file != NULL) {
        fprintf(log_file, "Segmentation fault occurred at Sniffer\n");
        fclose(log_file);
    }
    exit(EXIT_FAILURE);
}

int main(int argc, char *argv[]){

    if (argc != 2) return -1;

    printf("Running Sniffer!\n");

    // External Close up signals
    signal(SIGINT, cleanup);
    signal(SIGTERM, cleanup);
    signal(SIGSEGV, sigsegv_handler);

    // Initialize the Thread Pool
    // thread_pool_init(&pool, 4,5);
    
    thread_pool_init(&pool, 1,5);

    frameCaptured = (uint8_t**)malloc((pool.task_queue->numTasks + 2)*sizeof(uint8_t));
    for (int i = 0; i < pool.task_queue->numTasks + 2; i++){
        frameCaptured[i] = (uint8_t*)malloc(4096*sizeof(uint8_t));
    }

    // Initialize the Ethernet Socket
    eth.fanout_grp = 2;
    socketSetup(&eth, 0, 2048); // Tx size = 0; Rx Size = 2048
    if (createSocket(&eth, argv[1]) != 0){
        printf("Error creating socket\n");
        thread_pool_destroy(&pool);
        return -1;
    }
    
    sampledValues = openSampledValue(1);

    uint8_t nSVdetected = 0;
    for (int i = 0; i < MAX_SAMPLED_VALUES; i++){
        if (sampledValues[i].initialized){
            nSVdetected++;
        }
    }
    

    pthread_t watchDogThread;
    pthread_create(&watchDogThread, NULL, watchDogSv, NULL);

    printf("Running Quality Sniffer: \n");
    printf("Number of Sampled Values detected: %d\n", nSVdetected);
    runSniffer();


    cleanup(0);
    return 0;
}
