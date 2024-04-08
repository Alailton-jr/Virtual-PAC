#ifndef CONTINUOUS_REPLAY_H
#define CONTINUOUS_REPLAY_H

#include "mySocket.h"
#include "shmMemory.h"
#include "timers.h"
#include <sched.h>
#include <signal.h>


typedef struct {
    uint16_t smpRate;
    uint16_t freq;
    uint64_t interGap;
    uint16_t smpCountPos;
    uint16_t allDataPos;
    uint16_t asduLength;
    uint8_t frameShmName[256];
    uint32_t frameLength;
    uint8_t arrShmName[256];
    uint64_t arrLength;
    uint16_t n_channels;
    uint16_t n_asdu;
    uint64_t elapsedTime[2];
} continuousData_t;



#endif // CONTINUOUS_REPLAY_H

