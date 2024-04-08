
#ifndef TRANSIENT_REPLAY_H
#define TRANSIENT_REPLAY_H

#include <stdint.h>
#include <sched.h>
#include <signal.h>
#include "timers.h"
#include "shmMemory.h"
#include "mySocket.h"

typedef struct{
    uint64_t interGap;
    uint32_t maxSmpCount;
    uint16_t smpCountPos;
    uint16_t allDataPos;
    uint16_t asduLength;
    uint8_t arrShmName[256];
    uint64_t arrLength;
    uint8_t frameShmName[256];
    uint32_t frameLength;
    uint16_t n_channels;
    uint16_t n_asdu;
    uint8_t isLoop;
}transientData_t;



#endif // TRANSIENT_REPLAY_H