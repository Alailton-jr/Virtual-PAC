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
    QualityEventTypes type;
    uint8_t* arrValue;
    uint64_t timeStampIni;
    uint64_t timeStampEnd;
}QualityEvent_t;

typedef struct {
    double phasor[NUM_CHANELS][MAX_HARMONIC][2];
    uint8_t sagFlag;
    uint8_t swellFlag;
    uint8_t interruptionFlag;
    uint8_t overVoltageFlag;
    uint8_t underVoltageFlag;
    uint8_t transientFlag;
    double sagThreshold;
    double swellThreshold;
    double interruptionThreshold;
    double overVoltageThreshold;
    double underVoltageThreshold;
}QualityAnalyse_t;

typedef struct{
    uint8_t svId[128];
    uint32_t smpRate;
    uint32_t freq;
    uint8_t initialized;
    int32_t *snifferArr[NUM_CHANELS][FREQUENCY];
    int32_t *analyseArr[NUM_CHANELS][FREQUENCY];
    QualityAnalyse_t analyseData;
    uint8_t numChanels;
    uint32_t idxCycle;
    uint32_t idxBuffer;
    uint32_t idxProcessedBuffer;
    uint32_t idxProcessedCycle;
    uint64_t cycledCaptured;
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

void addSampledValue(int index, uint8_t* svId, uint16_t freq, uint16_t smpRate, QualityAnalyse_t *analyseData){
    shm_setup_s svMemory, bufferMemory, cycleMemoryMem;

    svMemory = openSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    if (svMemory.ptr == NULL){
        svMemory = createSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
        memset(svMemory.ptr, 0, MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    }

    sampledValue_t *sv = (sampledValue_t *)svMemory.ptr;

    //Delete old ...
    deleteSampledValue(index);

    strcpy(sv[index].svId, svId);
    sv[index].smpRate = smpRate;
    sv[index].freq = freq;
    sv[index].idxBuffer = 0;
    sv[index].idxCycle = 0;
    sv[index].idxProcessedCycle = 0;
    sv[index].idxProcessedBuffer = 0;
    sv[index].cycledCaptured = 0;
    sv[index].initialized = 1;
    sv[index].numChanels = 8;
    memcpy(&sv[index].analyseData, analyseData, sizeof(QualityAnalyse_t));

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