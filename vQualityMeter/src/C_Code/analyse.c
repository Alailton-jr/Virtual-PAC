#include "QualityMeter_lib.h"
sampledValue_t* sampledValues;

int32_t rmsFilter[] = {1, 2, 1, 2, 1};

void runAnalyse(){
    sampledValue_t* sv;
    int i, j, k, idx;
    while (1){
        for (int i = 0; i < MAX_SAMPLED_VALUES; i++){
            if (sampledValues[i].arrValue == NULL) continue;
            if (sampledValues[i].cycledCaptured <= 5) {
                continue;
            }
            if (sampledValues[i].idxProcessed + 3 > sampledValues[i].idxBuffer){
                sv = &sampledValues[i];
                for (i = 0; i < 3; i++){
                    idx = (sv->idxProcessed) + i % sv->freq;
                    // Convolution Code
                    for (j = 0; j < sv->smpRate; j++){
                        sv->arrValue[idx][j] = 0;
                        for (k = 0; k < 5; k++){
                            sv->arrValue[idx][j] += sv->arrValue[idx][j+k] * rmsFilter[k];
                        }
                    }
                }
                printf("Saving Array\n");
                FILE *fp = fopen("Data.csv", "w");
                if (fp == NULL) {
                    printf("Error opening file.\n");
                    return;
                }
                for (i = 0; i < 3; i++){
                    idx = (sv->idxProcessed) + i % sv->freq;
                    for (j = 0; j < sv->smpRate; j++){
                        fprintf(fp, "%d", sv->arrValue[idx][j]);
                        if (j < sv->smpRate - 1) {
                            fprintf(fp, ",");
                        }
                    }
                }
                fclose(fp);
                printf("Saved Array\n");
                return;
            }
        }
    }
}

int main(){

    shm_setup_s shm_sampledValue = openSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    if (shm_sampledValue.ptr == NULL){
        shm_sampledValue = createSharedMemory("QualitySampledValue", MAX_SAMPLED_VALUES*sizeof(sampledValue_t));
    }
    sampledValues = (sampledValue_t *)shm_sampledValue.ptr;

    sampledValues[0].arrValue[0][0] = 2;

    runAnalyse();

    printf("ended\n");
    return 0;
}