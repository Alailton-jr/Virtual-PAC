#ifndef SNIFFER_H
#define SNIFFER_H

#include "sampledValue.h"

#include <stdio.h>
#include <pthread.h>

#define MAX_RX_BUFFER 2048
#define MAX_TX_BUFFER 2048

typedef enum{
    CONTINUOUS,
    PREDEFINED_SV,
    ANY_SV
} timedCaptureType_t;

typedef struct processThread{
    uint8_t *frame;
    ssize_t frameSize;
    SampledValue_t *sv;
    int *num_sv;
    uint8_t save_waveform;
    FILE** files;
    uint8_t done;
} processThread_t;

typedef struct snifferThread{
    pthread_t thread;
    char ifName[128];
    int num_sv;
    int max_sv;
    SampledValue_t *sv;
    int8_t stop;
    int8_t running;
    double captureTime;
    double curTime;
    uint8_t save_waveform;
    timedCaptureType_t captureType;
}snifferThread_t;



void* snifferThread(void *threadInfo);
void* timed_snifferThread(void *threadInfo);
int save_captured_sv_data(SampledValue_t *sv, int num_sv, char* data[]);



#endif // SNIFFER_H