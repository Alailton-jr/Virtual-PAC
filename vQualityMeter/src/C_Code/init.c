
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

    sampledValue_t sv;
    memset(&sv, 0, sizeof(sampledValue_t));
    strcpy(sv.svId, "TRTC");
    sv.numChanels = 8;
    sv.smpRate = 80;
    sv.freq = 60;
    sv.idxBuffer = 0;
    sv.idxCycle = 0;
    sv.idxProcessedCycle = 0;
    sv.idxProcessedBuffer = 0;
    sv.cycledCaptured = 0;
    sv.initialized = 1;
    sv.numChanels = 8;
    sv.analyseData = qa;

    // Debug
    sv.cycledCaptured = 200;

    addSampledValue(0, &sv);
    sv.svId[0] = 'A';
    addSampledValue(1, &sv);

    return 0;
}