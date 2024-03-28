#ifndef SAMPLEDVALUE_H
#define SAMPLEDVALUE_H

#include "QualityMeter_lib.h"

typedef struct{
    uint8_t[6] macSrc;
    uint8_t[6] macDst;
    uint8_t[128] svId;

}sampledValue_t;










#endif