
#include "QualityMeter_lib.h"


void greet(){
    printf("Hello World!");
}

int main(int argc, uint8_t* argv[]){

    deleteSampledValueMemory();

    if (argc > 1) return 0;

    printf("Sampled Valued Added\n");

    QualityAnalyse_t qa;
    memset(&qa, 0, sizeof(QualityAnalyse_t));

    qa.sag.topThreshold = 0.9*8000;
    qa.sag.bottomThreshold = 0.1*8000;

    qa.swell.topThreshold = 1.8*8000;
    qa.swell.bottomThreshold = 1.1*8000;

    qa.interruption.topThreshold = 0.1*8000;
    qa.interruption.bottomThreshold = 0*8000;

    qa.overVoltage.topThreshold = 1.5*8000;
    qa.overVoltage.bottomThreshold = 1.1*8000;

    qa.underVoltage.topThreshold = 0.9*8000;
    qa.underVoltage.bottomThreshold = 0.5*8000;
    
    addSampledValue(0, "TRTC", 60, 80, &qa);

    return 0;
}