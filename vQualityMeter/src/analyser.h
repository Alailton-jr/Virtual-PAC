
#ifndef ANALYSER_H
#define ANALYSER_H

#include "sampledValue.h"
#include <pthread.h>
#include <stdlib.h>
#include <stdio.h>

typedef struct analyserThread{
    pthread_t thread;
    int num_sv;
    SampledValue_t *sv;
    uint8_t stop;
    uint8_t* stopTh;
    uint8_t running;
    FILE* fp;
    char saveDir[180];
}analyserThread_t;

void* single_sv_analyser(void* ThreadInfo);

void* analyserThread(void* ThreadInfo);


#endif //ANALYSER_H