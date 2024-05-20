#ifndef PRODIST_H
#define PRODIST_H

#include <pthread.h>
#include <stdint.h>
#include "sampledValue.h"

typedef enum {
    Normal = 0,
    Precarious = 1,
    Critical = 2
} voltageState_t;

typedef struct voltageVarThread{
    pthread_t threadAnalyse, threadCapture;
    int8_t running, stop;
    uint16_t numSv;
    SampledValue_t* sv;
    uint16_t noSamples;
    double interval;
    double nominalVoltage;
    double precVoltage[2];
    double criticalVoltage[2];
    union{
        uint64_t timestamp[2];
        double voltage[3];
        voltageState_t state;
    }data[4096];
    int index;
    int32_t buffer;
} voltageVarThread_t;

typedef struct prodistThread{
    pthread_t thread;
    int8_t running, stop;
    uint16_t numSv;
    SampledValue_t *sv;
}prodistThread_t;






#endif // PRODIST_H 