#ifndef SAMPLEDVALUE_H
#define SAMPLEDVALUE_H

#include <stdint.h>
#include <time.h>
#include <pthread.h>
#include <complex.h>
#include <stdio.h>

typedef struct Capture_Info{
    uint8_t filled;
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
    pthread_mutex_t mutex;
}Capture_Info_t;

typedef struct SV_Info{
    uint8_t svId[128];
    uint32_t smpRate;
    int32_t frequency;
    uint8_t numChanels;
    int32_t nomCurrent;
    int32_t nomVoltage;
    int32_t vLanId;
    int16_t vLanPriority;
    uint8_t macSrc[6];
    uint8_t noAsdu;
    uint8_t noChannels;
    uint8_t voltagePos[2][4];
    uint8_t currentPos[2][4];
}SV_Info_t;

typedef struct VTCD_Info{
    int32_t topThreshold, bottomThreshold;
    int32_t minDuration, maxDuration;
    int32_t minVal, maxVal;
    FILE* fp;
    uint8_t detected;
    struct timespec t0, t1;
    uint8_t save_waveform;
    uint8_t pos_cycle;    
} VTCD_Info_t;

typedef struct Quality_Info{
    VTCD_Info_t sag, swell, interruption, overVoltage, underVoltage, sustainedinterruption;
}Quality_Info_t;

typedef struct SV_Processing{
    int32_t ***captArr;
    uint8_t found;
    pthread_mutex_t mutex;
    pthread_cond_t condition;
    uint16_t pSmpRate;
    struct {
        int32_t idxCycle;
        int32_t idxBuffer;
    }sniffer, analyse;
    double complex **phasor; // [Channle][Harm Order]
    double complex symI[3];
    double complex symV[3];
    double *rms;
}SV_Processing_t;

typedef struct SampledValue{
    SV_Info_t info;
    Quality_Info_t quality;
    SV_Processing_t process;
    Capture_Info_t capture;
}SampledValue_t;


#define MAX_SAMPLED_VALUES 20
extern SampledValue_t *pSvs;

#endif // SAMPLEDVALUE_H