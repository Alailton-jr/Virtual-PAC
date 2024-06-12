#ifndef PRODIST_H
#define PRODIST_H

#include <pthread.h>
#include <stdint.h>
#include "sampledValue.h"
#include <complex.h>

extern char curDir[128];

typedef struct prodistThread{
    pthread_t thread;
    char ifName[128];
    int8_t running, stop;
    uint16_t numSv;
    Prodist_Info_t *sv_info;
    double wait_time;
    int noSample;
    uint32_t noSaved;
}prodistThread_t;

typedef struct prodistData{
    uint64_t timestamp[2];
    double complex phasor[8];
    double complex aparentPower;
    double activePower, reactivePower;
    double fp;
    double dit_h, dtt, dtt_p, dtt_i, dtt_3, dtt_95, dtt_p_99, dtt_i_95, dtt_3_95;
    double complex compSym_I[3], compSym_V[3];
    double fd;
}prodistData_t;

void* startProdist(void* threadInfo);
void get_data_from_index(FILE* fp, int idx, prodistData_t* x);


#endif // PRODIST_H 