#ifndef SEQUENCE_REPLAY_H
#define SEQUENCE_REPLAY_H

#include "mySocket.h"
#include "shmMemory.h"
#include "timers.h"
#include <sched.h>
#include <signal.h>

#define MAX_SEQ_NUM 25

typedef struct {
    uint8_t seqNum;
    uint64_t smpPerSeq[MAX_SEQ_NUM];
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
} sequenceData_t;


#endif