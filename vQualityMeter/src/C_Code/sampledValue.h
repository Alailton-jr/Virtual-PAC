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

typedef struct{
    uint8_t svId[128];
    uint32_t smpRate;
    uint32_t freq;
    int32_t** arrValue;
    uint32_t idxCycle;
    uint32_t idxBuffer;
    uint32_t idxProcessed;
    uint64_t cycledCaptured;
}sampledValue_t;

void addSampledValue(int index, uint8_t* svId, uint16_t freq, uint16_t smpRate){
    shm_setup_s svMemory = openSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    if (svMemory.ptr == NULL){
        shm_setup_s svMemory = createSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    }
    sampledValue_t *sv = (sampledValue_t *)svMemory.ptr;

    char arrName[] = "QualitySampledValue";

    strcpy(sv[index].svId, "TRTC");
    sv[index].smpRate = 80;
    sv[index].freq = 60;
    sv[index].arrValue = (int32_t **)malloc(sv[index].freq*sizeof(int32_t*));
    for (int i = 0; i < sv[index].freq; i++){
        sv[index].arrValue[i] = (int32_t *)malloc(sv[index].smpRate*sizeof(int32_t));
    }
    sv[index].idxBuffer = 0;
    sv[index].idxCycle = 0;
    sv[index].idxProcessed = 0;
    sv[index].cycledCaptured = 0;

    printf("Added Sampled Value\n");
}

void deleteSampledValue(int index){
    shm_setup_s svMemory = openSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    if (svMemory.ptr == NULL) return;
    sampledValue_t *sv = (sampledValue_t *)svMemory.ptr;
    free(sv[index].arrValue);
    sv[index].arrValue = NULL;
}

void deleteSampledValueMemory(){
    shm_setup_s svMemory = openSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    printf("%d", MAX_SAMPLED_VALUES);
    if (svMemory.ptr == NULL) return;
    sampledValue_t *sv = (sampledValue_t *)svMemory.ptr;
    for (int i = 0; i < MAX_SAMPLED_VALUES; i++){
        if (sv[i].arrValue == NULL) continue;
        free(sv[i].arrValue);
    }
    deleteSharedMemory(&svMemory);
}

#endif // SAMPLEDVALUE_H