
#include "QualityMeter_lib.h"



int main(){

    deleteSampledValueMemory();

    QualityAnalyse_t qa;
    memset(qa.phasor, 0, sizeof(qa.phasor));
    qa.sagThreshold = 0.9*8000;
    qa.swellThreshold = 1.1*8000;
    qa.interruptionThreshold = 0.1*8000;
    qa.overVoltageThreshold = 0.1;
    qa.underVoltageThreshold = 0.1;
    
    addSampledValue(0, "TRTC", 60, 80, &qa);

    return 0;
}