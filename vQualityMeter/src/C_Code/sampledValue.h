#ifndef SAMPLEDVALUE_H
#define SAMPLEDVALUE_H

#include "QualityMeter_lib.h"

typedef enum {
    Sag,
    Swell,
    Interruption,
    OverVoltage,
    UnderVoltage,
    Transient,
}QualityEventTypes;

typedef struct{
    uint32_t topThreshold;
    uint32_t bottomThreshold;
    uint32_t idx[NUM_CHANELS];
    uint32_t bufferIdx;
    uint32_t posCycle;
    uint8_t flag;
    struct timespec t0;
    struct timespec t1;
    double arr[NUM_CHANELS][MAX_BUFFER_EVENT_SIZE];
}QualityEvent_t;

typedef struct {
    double phasor_polar[NUM_CHANELS][MAX_HARMONIC][2];
    double phasor_rect[NUM_CHANELS][MAX_HARMONIC][2];
    double symetrical[2][3][2];
    double unbalance[2];
    QualityEvent_t sag;
    QualityEvent_t swell;
    QualityEvent_t interruption;
    QualityEvent_t overVoltage;
    QualityEvent_t underVoltage;
    QualityEvent_t transient;
}QualityAnalyse_t;

typedef struct{
    uint8_t svId[128];
    uint32_t smpRate;
    uint32_t freq;
    uint8_t initialized;
    int32_t *snifferArr[NUM_CHANELS][FREQUENCY];
    int32_t *analyseArr[NUM_CHANELS][FREQUENCY];
    double rms[NUM_CHANELS];
    QualityAnalyse_t analyseData;
    uint8_t numChanels;
    int32_t idxCycle;
    int32_t idxBuffer;
    int32_t idxProcessedBuffer;
    int32_t idxProcessedCycle;
    int64_t cycledCaptured;
}sampledValue_t;

void deleteSampledValue(int index);

sampledValue_t* openSampledValue(int sniffer){
    shm_setup_s svMemory = openSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    if (svMemory.ptr == NULL){
        shm_setup_s svMemory = createSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    }
    char memName[256];
    sampledValue_t *sv = (sampledValue_t *)svMemory.ptr;
    for (int i = 0; i < MAX_SAMPLED_VALUES; i++){
        if(!sv[i].initialized) continue;

        for (int j = 0; j < sv[i].numChanels; j++){
            for (int k=0; k < sv[i].freq; k++){
                sprintf(memName, "QualitySampledValue_%d_%d_%d", i, j, k);
                shm_setup_s cycleMemoryMem = openSharedMemory(memName, sv[i].smpRate*sizeof(int32_t));
                if (cycleMemoryMem.ptr == NULL) continue;
                if (sniffer) {
                    sv[i].snifferArr[j][k] = (int32_t *)cycleMemoryMem.ptr;
                } else {
                    sv[i].analyseArr[j][k] = (int32_t *)cycleMemoryMem.ptr;
                }
            }
        }
    }
    return svMemory.ptr;
}

void addSampledValue(int index, sampledValue_t *_sv){
    shm_setup_s svMemory, bufferMemory, cycleMemoryMem;

    svMemory = openSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    if (svMemory.ptr == NULL){
        svMemory = createSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
        memset(svMemory.ptr, 0, MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    }

    sampledValue_t *sv = (sampledValue_t *)svMemory.ptr;

    //Delete old ...
    deleteSampledValue(index);

    memcpy(&sv[index], _sv, sizeof(sampledValue_t));

    char memName[256];
    for (int i = 0;i < sv[index].numChanels; i++){
        for (int j=0; j<sv[index].freq; j++){
            sprintf(memName, "QualitySampledValue_%d_%d_%d", index, i, j);
            cycleMemoryMem = createSharedMemory(memName, sv[index].smpRate*sizeof(int32_t));
            sv[index].snifferArr[i][j] = (int32_t *)cycleMemoryMem.ptr;
        }
    }
}

void deleteSampledValue(int index){
    shm_setup_s svMemory = openSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    if (svMemory.ptr == NULL) return;

    char memName[256];
    sampledValue_t *sv = (sampledValue_t *)svMemory.ptr;
    if (!sv[index].initialized) return;

    for (int i = 0;i < sv[index].numChanels; i++){
        for (int j=0; j<sv[index].freq; j++){
            sprintf(memName, "QualitySampledValue_%d_%d_%d", index, i, j);
            shm_setup_s cycleMemoryMem = openSharedMemory(memName, sv[index].smpRate*sizeof(int32_t));
            if (cycleMemoryMem.ptr == NULL) continue;
            deleteSharedMemory(&cycleMemoryMem);
        }
    }
    sv[index].initialized = 0;
}

void deleteSampledValueMemory(){
    shm_setup_s svMemory = openSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    printf("%d", MAX_SAMPLED_VALUES);
    if (svMemory.ptr == NULL) return;
    for (int i = 0; i < MAX_SAMPLED_VALUES; i++){
        deleteSampledValue(i);
    }
    deleteSharedMemory(&svMemory);
}

#endif // SAMPLEDVALUE_H