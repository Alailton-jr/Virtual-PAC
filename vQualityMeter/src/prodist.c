#include "sampledValue.h"
#include "prodist.h"
#include "mySocket.h"
#include "threadTask.h"
#include "fft.h"
#include "util.h"

#define MAX_RX_BUFFER 2048

#include <stdlib.h>
#include <string.h>
#include <unistd.h>


typedef struct capture_data{
    int32_t **buffer; // [noChannel][SmpRate]
    uint32_t smpRate;
    uint8_t noChannel;
    char svId[128];
    uint32_t idx;
    FILE* fp;
    char fileName[256];
}capture_data_t;

typedef struct pkt_process_prodist{
    capture_data_t *svData;
    int numSv;
    uint8_t* frame;
    ssize_t frameSize;
    int* noEnded;
}pkt_process_prodist_t;

typedef enum {
    Normal = 0,
    Precarious = 1,
    Critical = 2
} voltageState_t;


typedef struct prodistThreadData{
    pthread_t threadAnalyse, threadCapture;
    int8_t running, stop;
    uint16_t numSv;
    SampledValue_t* sv;
    uint16_t noSamples;
    double interval;
    double nominalVoltage;
    double precVoltage[2];
    double criticalVoltage[2];
    FILE* fp;
    prodistData_t data;
    int index;
    int32_t buffer;
} prodistThreadData_t;


void* prodist_process_frame(void* threadInfo){

    pkt_process_prodist_t* pktInfo = (pkt_process_prodist_t*) threadInfo;
    uint8_t* frame = pktInfo->frame;

    // -------- Process the frame --------
    int i = 0;
    // Skip Ethernet and vLAN
    if (frame[12] == 0x81 && frame[13] == 0x00)  
        i = 16;
    else 
        i = 12;

    if (!(frame[i] == 0x88 && frame[i+1] == 0xba)) // Check if packet is SV
        return NULL; 

    int j=0, noAsdu, svIdx = -1;

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
            if (frame[i] == 0x80){ // If tag is svId 0x80
                if (svIdx == -1){
                    for (int idx = 0; idx < pktInfo->numSv; idx++){ // look for the sampled value
                        if (memcmp(pktInfo->svData[idx].svId, &frame[i+2], frame[i+1]) == 0){
                            svIdx = idx;
                            if (pktInfo->svData[svIdx].idx == pktInfo->svData[svIdx].smpRate){
                                return NULL;
                            }
                            break;
                        }
                    }
                }
            }
            i += frame[i+1] + 2; // Skip field Tag, Length and Value
            if (i >= pktInfo->frameSize) {
                return NULL;
            }
        }
        j = 0;
        if (svIdx == -1) {
            return NULL;
        }

        if (frame[i+1] > pktInfo->svData[svIdx].noChannel * 8) {
            return NULL;
        }
        while (j < frame[i+1]) // Decode the raw values
        {
            pktInfo->svData[svIdx].buffer[j/8][pktInfo->svData[svIdx].idx] = (int32_t)((frame[i+5+j]) | (frame[i+4+j]*256) | (frame[i+3+j]*65536) | (frame[i+2+j]*16777216));
            j += 8;
        }
        pktInfo->svData[svIdx].idx++;
        if (pktInfo->svData[svIdx].idx == pktInfo->svData[svIdx].smpRate){
            pktInfo->noEnded[0] = pktInfo->noEnded[0] + 1;
            break;
        }
        i += frame[i+1] + 2;
    }
    return NULL;
}

int prodistSocket(eth_t* myEth, char* ifName){

    myEth->fanout_grp = 3;
    socketSetup(myEth, 0, 2048);
    if(createSocket(myEth, ifName, 1, 0, 1, 0, 1) != 0){
        printf("Error creating socket\n");
        return -1;
    }
}

capture_data_t* alloc_sv_data(int numSv, prodistThread_t* prodistThd){
    capture_data_t *svData = (capture_data_t*) malloc(numSv * sizeof(capture_data_t));

    sprintf(svData->fileName, "%s/prodistFiles", curDir);
    remove_dir(svData->fileName);
    mkdir(svData->fileName, 0700);

    for (int i = 0; i < numSv;i++){
        svData[i].idx = 0;
        svData[i].noChannel = prodistThd->sv_info[i].noChannel;
        svData[i].smpRate = prodistThd->sv_info[i].smpRate;
        strcpy(svData[i].svId, prodistThd->sv_info[i].svID);
        svData[i].buffer = (int32_t**) malloc(svData[i].noChannel * sizeof(int32_t*));
        for (int j = 0; j < svData[i].noChannel; j++){
            svData[i].buffer[j] = (int32_t*) malloc(sizeof(int32_t) * svData[i].smpRate);
        }
        sprintf(svData[i].fileName, "%s/prodistFiles/%s.bin", curDir, svData[i].svId);
        svData[i].fp = fopen(svData[i].fileName, "ab");
    }
    return svData;
}

void free_sv_data(capture_data_t* svData, int numSv) {
    for (int i = 0; i < numSv; i++) {
        for (int j = 0; j < svData[i].noChannel; j++) {
            free(svData[i].buffer[j]);
        }
        free(svData[i].buffer);
        // fclose(svData->fp);
    }
    free(svData);
}

double get_diference_time(struct timespec *t0, struct timespec *t1){
    double res;
    res = (double)(t1->tv_sec - t0->tv_sec);
    res += (double)(t1->tv_nsec - t0->tv_nsec) / 1E9;
    return res;
}

static void downsample(int32_t* input, int input_size, complex double *output, int output_size) {
    double step = (double)input_size / output_size;
    int i;
    for (i = 0; i < output_size; i++) {
        double index = i * step;
        int lower = (int)index;
        int upper = lower + 1;
        double frac = index - lower;
        if (upper >= input_size) {
            output[i] = (complex double)input[input_size - 1];
        } else {
            output[i] = (complex double)input[lower] +  frac * (complex double)(input[upper] - input[lower]);
        }
    }
}

static complex double symCompMatrix[3][3] = {
    {1.0/3.0, -0.5/3.0 - 0.86602540378/3.0*I, -0.5/3.0 + 0.86602540378/3.0*I},
    {1.0/3.0, -0.5/3.0 + 0.86602540378/3.0*I, -0.5/3.0 - 0.86602540378/3.0*I},
    {1.0/3.0, 1.0/3.0, 1.0/3.0}
};

static void threePhaseToSymComp(complex double* abc, complex double* sym){
    for (int i=0; i<3; i++){
        sym[i] = 0;
        for (int j=0; j<3; j++){
            sym[i] += symCompMatrix[i][j] * abc[j];
        }
    }
    return;
}

void process_sv_data(capture_data_t* svData, int numSv){

    static complex double input[256];
    static struct timespec t0;

    static fft_plan_t plan64, plan256;
    static uint8_t inited = 0;

    if (inited == 0){
        plan64 = fft_plan_create(64, input);
        plan256 = fft_plan_create(256, input);
        inited = 1;
    }

    if (numSv == 0){
        fft_plan_destroy(&plan64);
        fft_plan_destroy(&plan256);
        inited = 0;
        return;
    }

    static complex double measurement[41][8];

    for (int i=0;i<numSv;i++){
        svData->idx = 0;
        clock_gettime(CLOCK_REALTIME, &t0);
        prodistData_t data;
        for (int j=0; j<svData[i].noChannel; j++){
            if (svData[i].smpRate == 80){
                downsample(svData[i].buffer[j], 80, &input, 64);
                fft_exec(&plan64);
                for(int k=0;k<41;k++){
                    measurement[k][j] = input[k] * sqrt(2) / 64;
                }
            }else{
                for (int k=0;k<256;k++){
                    input[k] = svData[i].buffer[j][k];
                }
                fft_exec(&plan256);
                for(int k=0;k<41;k++){
                    measurement[k][j] = input[k] * sqrt(2) / 256;
                }
            }
        }

        // Data Time Stamp
        data.timestamp[0] = t0.tv_sec;
        data.timestamp[1] = t0.tv_nsec;

        // Power
        data.aparentPower = 0;
        for (int i=0;i<3;i++){
            data.aparentPower += measurement[1][i] * measurement[1][i+4];
        }
        data.activePower = creal(data.aparentPower);
        data.reactivePower = cimag(data.aparentPower);
        data.fp = carg(data.aparentPower) * 180 / PI;

        // Fundamental Phasor
        for (int i=0;i<8;i++){
            data.phasor[i] = measurement[1][i];
        }

        // CompSym
        threePhaseToSymComp(&measurement[1][0], data.compSym_I);
        threePhaseToSymComp(&measurement[1][4], data.compSym_V);
        
        
        save_data(svData[i].fp, &data);
        printf("%lf -  ", cabs(measurement[1][0]));
        printf("%lf\n", cabs(data.compSym_I[1]));

    }
    printf("\n");
}

void* startProdist(void* threadInfo){

    prodistThread_t* prodistThd = (prodistThread_t*) threadInfo;

    // Inicialização

    prodistThd->running = 1;
    prodistThd->noSaved = 0;

    int numSv = prodistThd->numSv; // Number of Sampled Values to capture
    int noSample = prodistThd->noSample; // Number of Samples to capture
    if (numSv == 0){
        prodistThd->running = 0;
        return NULL;
    }

    // Allocation of SVs Data 
    capture_data_t *svData = alloc_sv_data(numSv, prodistThd);
    
    // Socket
    eth_t myEth;
    struct msghdr msg_hdr;
    struct iovec iov;
    prodistSocket(&myEth, prodistThd->ifName);
    configMsgHdr(&msg_hdr, &iov, &myEth);


    int idxFrameInfo = 0;
    ssize_t rx_bytes;
    
    double tWait = prodistThd->wait_time; // Waiting time
    int num_ended; // number of SV Buffers that are full

    ThreadPool pool; // pool of threads that will process the pkt
    thread_pool_init(&pool, NUM_THREADS, NUM_TASK); // Initialization of the threads
    pkt_process_prodist_t frameInfo[NUM_TASK+2]; // Information of the frames for the process function
    uint8_t frames[NUM_TASK+2][MAX_RX_BUFFER]; //  

    for (int i = 0; i < NUM_TASK+2; i++){
        frameInfo[i].numSv = numSv;
        frameInfo[i].noEnded = &num_ended;
        frameInfo[i].svData = svData;
    }

    // Espera
    struct timespec t0, t1;

    // Captura
    clock_gettime(CLOCK_MONOTONIC, &t0);
    
    int smpCount = 0;
    while(!prodistThd->stop){
        // Start Capturing
        num_ended = 0;
        for (int i=0;i<numSv;i++){
            svData[i].idx = 0;
        }
        while (num_ended != numSv){
            if (prodistThd->stop) break;
            rx_bytes = recvmsg(myEth.socket, &msg_hdr, 0);
            if (rx_bytes > 0 && rx_bytes < myEth.rx_size) {
                memcpy(frames[idxFrameInfo], myEth.rx_buffer, rx_bytes);
                frameInfo[idxFrameInfo].frame = frames[idxFrameInfo];
                frameInfo[idxFrameInfo].frameSize = rx_bytes;
                thread_pool_submit(&pool, prodist_process_frame, &frameInfo[idxFrameInfo]);
                idxFrameInfo++;
                if (idxFrameInfo == NUM_TASK+2) idxFrameInfo = 0;
            }
        }

        // Processamento
        process_sv_data(svData, numSv);

        smpCount++;
        prodistThd->noSaved = prodistThd->noSaved + 1;
        if (smpCount == noSample)
            goto CLEANUP;



        // Wait for next Capture
        while (get_diference_time(&t0, &t1) < tWait){
            clock_gettime(CLOCK_MONOTONIC, &t1);
            usleep(0.5e6);
            if (prodistThd->stop){
                break;
            }
        }
        clock_gettime(CLOCK_MONOTONIC, &t0);
    }

    // Processamento

CLEANUP:

    //Test Capt
    // FILE* readSV;
    // readSV = fopen(svData[0].fileName, "rb");
    // if (readSV != NULL){
    //     prodistData_t dat;
    //     get_data_from_index(readSV, 3, &dat);
    //     printf("%lf\n", cabs(dat.measurement[1][0]));
    //     fclose(readSV);
    // }

    // Limpeza
    process_sv_data(NULL, 0);
    thread_pool_destroy(&pool);
    free_sv_data(svData, numSv);

    prodistThd->running = 0;
    return NULL;
}

void save_data(FILE* fp, prodistData_t *x) {
    if (fp == NULL) return;
    fwrite(x, sizeof(prodistData_t), 1, fp);
    fflush(fp);
}

void get_data_from_index(FILE* fp, int idx, prodistData_t* x){
    if (fp == NULL) return;
    size_t size = sizeof(prodistData_t) * idx;
    fseek(fp, size, SEEK_SET);
    fread(x, sizeof(prodistData_t), 1, fp);
}

void prodist_test(){

    prodistThreadData_t x;

    FILE* readFP;

    x.fp = fopen("testProdist.bin", "ab");
    
    x.data.dtt = 4;
    save_data(x.fp, &x.data);

    x.data.dtt = 2;
    save_data(x.fp, &x.data);

    x.data.dtt = 8;
    save_data(x.fp, &x.data);

    
    readFP = fopen("testProdist.bin", "rb");

    get_data_from_index(readFP, 0, &x.data);
    printf("%lf\n", x.data.dtt);

    fclose(x.fp);
    fclose(readFP);

}

void saveArrayToFile(int arr[], int size, const char* filename) {
    FILE* file = fopen(filename, "wb");
    if (file == NULL) {
        perror("Error opening file");
        return;
    }

    fwrite(arr, sizeof(int), size, file); // Write the array to the file
    fclose(file);
}

int getValueAtIndex(const char* filename, int index, int size) {
    FILE* file = fopen(filename, "rb");
    if (file == NULL) {
        perror("Error opening file");
        return -1; // Return an error indicator
    }

    fseek(file, index * sizeof(int), SEEK_SET); // Seek to the position of the element at the given index
    int value;
    fread(&value, sizeof(int), 1, file); // Read the value

    fclose(file);
    return value;
}