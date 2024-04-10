#include "QualityMeter_lib.h"

eth_t eth;
ThreadPool pool;

#define MAX_CAPTURED 20
int idxThreads = 0;
uint8_t frameCaptured[20][2048];
FILE *fp[MAX_CAPTURED];
char filename[MAX_CAPTURED][50];
char svID[MAX_CAPTURED][256];
uint8_t nSV = 0;
struct timespec t0, t1;

typedef struct{
    char svID[256];
    char FileName[256];
    double lastTime;
    double meanTime;
    uint8_t nAsdu;
    uint8_t macAddr[6];
    uint16_t vLanId;
    uint8_t vLanPriority;
    uint8_t nChannels;
    struct timespec t0;
}svCapt_t;

svCapt_t svCaptured[MAX_CAPTURED];

void* processPacket(uint8_t* frame, uint64_t size){

    int i = 0;

    // Skip Ethernet and vLAN
    if (frame[12] == 0x81 && frame[13] == 0x00)  i = 16;
    else i = 12;

    if (!(frame[i] == 0x88 && frame[i+1] == 0xba)) return NULL; // Check if packet is SV

    int j=0, noAsdu, svIdx = -1, idx, found, vLanID, vLanPriority;
    

    // Skip SV Header to seqAsdu parameter
    if (frame[i+11] == 0x82){
        i += 17;
    }else if (frame[i+11] == 0x81){
        i += 16;
    }else{
        i += 15;
    }

    noAsdu = frame[i-1]; // Number of ASDUs

    // Skip to first asdu
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

        while (frame[i] != 0x87 && i <= size) { // skip all the fields until Sequence of Data
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
            if (frame[i] == 0x80){
                found = 0;
                for (idx = 0; idx < nSV; idx++){
                    if (memcmp(svCaptured[idx].svID, &frame[i+2], frame[i+1]) == 0){
                        svIdx = idx;
                        found = 1;
                        break;
                    }
                }
                if (!found){
                    svIdx = nSV;
                    memcpy(svCaptured[idx].svID, &frame[i+2], frame[i+1]);
                    svCaptured[idx].vLanId = frame[14] | frame[15] << 8;
                    svCaptured[idx].vLanPriority = frame[16] & 0x07; // May be wrong
                    memcpy(svCaptured[idx].macAddr, &frame[6], 6);
                    svCaptured[idx].nAsdu = noAsdu;
                    svCaptured[idx].lastTime = 0;
                    nSV++;
                }
            }
            i += frame[i+1] + 2; // Skip field Tag, Length and Value
        }
        j = 0;
        if (svIdx == -1) return NULL;
        
        while (j < frame[i+1]) // Decode the raw values
        {
            // rawValues[idxRaw][j/8] = 0;
            // rawValues[idxRaw][j/8] |= (int)frame[i+2+j] << 24;
            // rawValues[idxRaw][j/8] |= (int)frame[i+3+j] << 16;
            // rawValues[idxRaw][j/8] |= (int)frame[i+4+j] << 8;
            // rawValues[idxRaw][j/8] |= (int)frame[i+5+j];
            // rawValues[idxRaw][j/8] = ((int32_t) frame[i+2+j] << 24) | ((int32_t) frame[i+3+j] << 16) | ((int32_t) frame[i+4+j] << 8) | ((int32_t) frame[i+5+j]);
            sampledValues[svIdx].snifferArr[j/8][sampledValues[svIdx].idxBuffer][sampledValues[svIdx].idxCycle] = (int32_t)((frame[i+5+j]) | (frame[i+4+j]*256) | (frame[i+3+j]*65536) | (frame[i+2+j]*16777216));
            // if (j/8 == 4) 
                // fprintf(fp, "%d, ", sampledValues[svIdx].snifferArr[j/8][sampledValues[svIdx].idxBuffer][sampledValues[svIdx].idxCycle]);
            j += 8;
        }
        sampledValues[svIdx].idxCycle++;
        if (sampledValues[svIdx].idxCycle >= sampledValues[svIdx].smpRate) {
            sampledValues[svIdx].idxCycle = 0;
            sampledValues[svIdx].idxBuffer++;
            sampledValues[svIdx].cycledCaptured++;
            if (sampledValues[svIdx].idxBuffer >= sampledValues[svIdx].freq) {
                sampledValues[svIdx].idxBuffer = 0;
                // saveArray(&sampledValues[svIdx]);
            }
        }
        
        // if (idxRaw % (MAX_RAW / NUM_DFT_PER_CYCLE) == 0) { // Compute the DFT
        //     struct timespec t0, t1;
        //     // clock_gettime(0, &t0);
        //     computeDFT();
        //     // clock_gettime(0, &t1);
        //     // printf("Time: %ld us\n", (t1.tv_nsec - t0.tv_nsec)/1000);
        // }
        // idxRaw++;
        // if (idxRaw == MAX_RAW) idxRaw = 0;
        // pthread_mutex_unlock(&mutex); // Unlock the raw values
        i += frame[i+1] + 2;
    }

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

void openFiles(){
    char filePath[512], curDir[512];
    readlink("/proc/self/exe", filePath, sizeof(filePath) - 1);
    strcpy(curDir, dirname(filePath));

    for (int i = 0; i < nSV; i++){
        sprintf(filename[i], "%s/%s.csv", curDir, svID[i]);
        fp[i] = fopen(filename[i], "w");
        if (fp[i] == NULL){
            printf("Error opening file %s\n", filename[i]);
            exit(1);
        }
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