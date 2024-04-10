#include "QualityMeter_lib.h"

eth_t eth;
ThreadPool pool;

#define MAX_CAPTURED 20
int idxThreads = 0;
uint8_t frameCaptured[20][2048];
FILE *fp[MAX_CAPTURED];
FILE *svData;
char filename[MAX_CAPTURED][128];
char curDir[512];
char svID[MAX_CAPTURED][256];
uint8_t nSV = 0;
struct timespec t0, t1;
pthread_mutex_t mutex;

typedef struct{
    char svID[256];
    char FileName[256];
    double meanTime;
    uint64_t nPackets;
    uint8_t nAsdu;
    uint8_t macSrc[6];
    uint8_t macDst[6];
    int32_t vLanId;
    int16_t vLanPriority;
    uint8_t nChannels;
    uint16_t appID;
    struct timespec t0;
    struct timespec t1;
}svCapt_t;

svCapt_t svCaptured[MAX_CAPTURED];

void* processPacket(uint8_t* frame, uint64_t size){

    int i = 0, has_vLAN = 0;

    // Skip Ethernet and vLAN
    if (frame[12] == 0x81 && frame[13] == 0x00)  {
        i = 16;
        has_vLAN = 1;
    }
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
                pthread_mutex_lock(&mutex);
                for (idx = 0; idx < nSV; idx++){
                    if (memcmp(svCaptured[idx].svID, &frame[i+2], frame[i+1]) == 0){
                        svIdx = idx;
                        found = 1;
                        clock_gettime(0, &svCaptured[idx].t1);
                        svCaptured[idx].meanTime += ((svCaptured[idx].t1.tv_sec - svCaptured[idx].t0.tv_sec) + (svCaptured[idx].t1.tv_nsec - svCaptured[idx].t0.tv_nsec)/1e9)*1e3;
                        svCaptured[idx].nPackets++;
                        svCaptured[idx].t0 = svCaptured[idx].t1;
                        break;
                    }
                }
                if (!found){
                    svIdx = nSV;
                    nSV++;
                    memcpy(svCaptured[svIdx].svID, &frame[i+2], frame[i+1]);
                    if (has_vLAN){
                        svCaptured[svIdx].vLanPriority = (frame[14] & 0xC0) >> 5;
                        svCaptured[svIdx].vLanId = frame[15] + (frame[14] & 0x0f)*256;
                        svCaptured[svIdx].appID = (frame[18] << 8) | frame[19];
                    }else{
                        svCaptured[svIdx].vLanPriority = -1;
                        svCaptured[svIdx].vLanId = -1;
                        svCaptured[svIdx].appID = (frame[14] << 8) | frame[15];
                    }
                    memcpy(svCaptured[svIdx].macDst, &frame[0], 6);
                    memcpy(svCaptured[svIdx].macSrc, &frame[6], 6);
                    
                    svCaptured[svIdx].nAsdu = noAsdu;
                    svCaptured[svIdx].meanTime = 0;
                    svCaptured[svIdx].nPackets = 0;
                    clock_gettime(0, &svCaptured[svIdx].t0);
                    
                }
                pthread_mutex_unlock(&mutex);
            }
            i += frame[i+1] + 2; // Skip field Tag, Length and Value
        }
        j = 0;
        if (svIdx == -1) return NULL;
        svCaptured[svIdx].nChannels = (frame[i+1])/8;
        pthread_mutex_lock(&mutex);
        while (j < frame[i+1]) // Decode the raw values
        {
            fprintf(fp[svIdx], "%d,", (int32_t)((frame[i+5+j]) | (frame[i+4+j]*256) | (frame[i+3+j]*65536) | (frame[i+2+j]*16777216)));
            j += 8;
        }
        fprintf(fp[svIdx], "\n");
        pthread_mutex_unlock(&mutex);
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

void closeFiles(){

    char SvFileName[256];

    for (int i = 0; i < MAX_CAPTURED; i++){
        if (i >= nSV) {
            fclose(fp[i]);
            remove(filename[i]);
            continue;
        }
        fclose(fp[i]);
        sprintf(SvFileName, "%s/captSV/%s.csv", curDir, svCaptured[i].svID);
        if (rename(filename[i], SvFileName) != 0){
            perror("Error renaming file");
        }

        svCaptured[i].meanTime /= svCaptured[i].nPackets;
        fprintf(svData, "- svID: %s\n", svCaptured[i].svID);
        fprintf(svData, "  MeanTime: %lf\n", svCaptured[i].meanTime);
        fprintf(svData, "  nPackets: %lu\n", svCaptured[i].nPackets);
        fprintf(svData, "  nAsdu: %u\n", svCaptured[i].nAsdu);
        fprintf(svData, "  macSrc: %02x:%02x:%02x:%02x:%02x:%02x\n", svCaptured[i].macSrc[0], svCaptured[i].macSrc[1], svCaptured[i].macSrc[2], svCaptured[i].macSrc[3], svCaptured[i].macSrc[4], svCaptured[i].macSrc[5]);
        fprintf(svData, "  macDst: %02x:%02x:%02x:%02x:%02x:%02x\n", svCaptured[i].macDst[0], svCaptured[i].macDst[1], svCaptured[i].macDst[2], svCaptured[i].macDst[3], svCaptured[i].macDst[4], svCaptured[i].macDst[5]);
        fprintf(svData, "  vLanId: %d\n", svCaptured[i].vLanId);
        fprintf(svData, "  vLanPriority: %d\n", svCaptured[i].vLanPriority);
        fprintf(svData, "  appID: %d\n", svCaptured[i].appID);
        fprintf(svData, "  nChannels: %d\n", svCaptured[i].nChannels);
    }
    fprintf(svData, "nSV: %u\n", nSV);
    fclose(svData);
    
}

void openFiles(){
    char filePath[512], svDataName[512];
    readlink("/proc/self/exe", filePath, sizeof(filePath) - 1);
    strcpy(curDir, dirname(filePath));

    for (int i = 0; i < MAX_CAPTURED; i++){
        sprintf(filename[i], "%s/captSV/%d.csv", curDir, i);
        fp[i] = fopen(filename[i], "w");
        if (fp[i] == NULL){
            printf("Error opening file %s\n", filename[i]);
            exit(1);
        }
    }
    sprintf(svDataName, "%s/captSV/svData.yaml", curDir);
    svData = fopen(svDataName, "w");
}

void cleanUp(int signum){
    printf("Cleaning up...\n");
    closeFiles();
    pthread_mutex_destroy(&mutex);
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

    thread_pool_init(&pool, 4, 10);
    
    openFiles();
    pthread_mutex_init(&mutex, NULL);
    runSniffer(time);
    cleanUp(0);

}