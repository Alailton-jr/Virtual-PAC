#include "QualityMeter_lib.h"

sampledValue_t* sv;
uint16_t windowSize = 20;
fftw_plan *plan[MAX_SAMPLED_VALUES];

void saveArr(double *arr, int64_t size, char *name){
    FILE *fp = fopen(name, "w");
    if (fp == NULL) {
        printf("Error opening file.\n");
        return;
    }
    for (int i = 0; i < size; i++) {
        fprintf(fp, "%f", arr[i]);
        if (i < size - 1) {
            fprintf(fp, ",");
        }
    }
    fclose(fp);
}

#define MAX_SAG_SIZE 80*60*5
#define MAX_TEMPBUFFER_SIZE 5

void runAnalyse(){
    int i, j, k, idx, idy, _idx, kj;
    uint8_t found;
    int svIdx, channel;
    int cycleAnalised = 1;

    int64_t sum;
    fftw_complex **input[MAX_SAMPLED_VALUES];
    fftw_complex **output[MAX_SAMPLED_VALUES];

    int32_t tempBufferArr[MAX_SAMPLED_VALUES][MAX_TEMPBUFFER_SIZE][8][80];
    int32_t tempBufferTrigger[MAX_SAMPLED_VALUES];
    uint32_t tempBufferIdx[MAX_SAMPLED_VALUES];
    for (i = 0; i < MAX_SAMPLED_VALUES; i++){
        tempBufferTrigger[i] = -1;
        tempBufferIdx[i] = 0;
    }
    

    FILE *sagFile;
    int fileSearch;


    double sagArr[8][MAX_SAG_SIZE];
    uint32_t sagIdx[8];
    int32_t posSagCycle[8];

    //Debug
    struct timespec t0, t1;

    for (svIdx = 0; svIdx < MAX_SAMPLED_VALUES; svIdx++){
        if (!sv[svIdx].initialized) continue;
        input[svIdx] = (fftw_complex**) malloc(sv[svIdx].numChanels * sizeof(fftw_complex*));
        output[svIdx] = (fftw_complex**) malloc(sv[svIdx].numChanels * sizeof(fftw_complex*));
        plan[svIdx] = (fftw_plan*) malloc(sv[svIdx].numChanels * sizeof(fftw_plan));
        for (channel = 0; channel < sv[svIdx].numChanels; channel++){
            input[svIdx][channel] = (fftw_complex*) fftw_malloc(cycleAnalised * sv[svIdx].smpRate * sizeof(fftw_complex));
            output[svIdx][channel] = (fftw_complex*) fftw_malloc(cycleAnalised * sv[svIdx].smpRate * sizeof(fftw_complex));
            plan[svIdx][channel] = fftw_plan_dft_1d(cycleAnalised * sv[svIdx].smpRate, input[svIdx][channel], output[svIdx][channel], FFTW_FORWARD, FFTW_ESTIMATE);
        }
    }

    int32_t stop = 0;
    FILE *fp = fopen("output.csv", "w");

    while(stop<1000){
        stop++;
        for (svIdx = 0; svIdx < MAX_SAMPLED_VALUES; svIdx++){
            if (!sv[svIdx].initialized) continue;

            while (1){
                if (sv[svIdx].cycledCaptured <= 5) continue;
                if (sv[svIdx].idxProcessedBuffer > sv[svIdx].idxBuffer){
                    if (!(sv[svIdx].idxProcessedBuffer + cycleAnalised > sv[svIdx].idxBuffer + 60)) continue;
                }else{
                    if (!(sv[svIdx].idxProcessedBuffer + cycleAnalised > sv[svIdx].idxBuffer)) continue;
                }
                break;
            }

            for (channel = 0; channel < sv[svIdx].numChanels;channel++){
                k = 0;
                for (i = 0; i < cycleAnalised; i++){
                    idx = (sv[svIdx].idxProcessedBuffer + i) % sv[svIdx].freq;
                    idy = sv[svIdx].idxProcessedCycle;
                    for (j = 0; j < sv[svIdx].smpRate; j++){
                        idy++;
                        if (idy >= sv[svIdx].smpRate){
                            idx++;
                            if (idx >= sv[svIdx].freq) idx = 0;
                            idy -= sv[svIdx].smpRate;
                        }
                        input[svIdx][channel][k][0] = sv[svIdx].analyseArr[channel][idx][idy];
                        input[svIdx][channel][k][1] = 0.0;
                        k++;
                        if (channel == 0){ // Debug Save
                            fprintf(fp, "%d", sv[svIdx].analyseArr[channel][idx][idy]);
                            if (j < sv[svIdx].smpRate - 1){
                                fprintf(fp, ",");
                            }else{
                                fprintf(fp, "\n");
                            }
                        }
                    }
                }
                if (tempBufferTrigger[svIdx] == -1){
                        tempBufferTrigger[svIdx] = idx;
                    }
                if (tempBufferTrigger[svIdx] == idx){
                    for(i = 0; i<k; i++){
                        tempBufferArr[svIdx][tempBufferIdx[svIdx]][channel][i] = input[svIdx][channel][i][0];
                    }
                    tempBufferIdx[svIdx]++;
                    if (tempBufferIdx[svIdx] == 5){
                        tempBufferIdx[svIdx] = 0;
                    }
                }
                fftw_execute(plan[svIdx][channel]);
            }
            idy += windowSize;
            if (idy >= sv[svIdx].smpRate){
                idx++;
                if (idx >= sv[svIdx].freq) idx = 0;
                idy -= sv[svIdx].smpRate;
            }
            sv[svIdx].idxProcessedBuffer = idx;
            sv[svIdx].idxProcessedCycle = idy;

            for (channel = 0; channel < sv[svIdx].numChanels; channel++){
                for (i = 0; i < MAX_HARMONIC; i++){
                    sv[svIdx].analyseData.phasor[channel][i][0] = sqrt(output[svIdx][channel][i][0] * output[svIdx][channel][i][0] + output[svIdx][channel][i][1] * output[svIdx][channel][i][1])/(cycleAnalised * sv[svIdx].smpRate) * 1.41421356;
                    sv[svIdx].analyseData.phasor[channel][i][1] = atan2(output[svIdx][channel][i][1], output[svIdx][channel][i][0]);
                }
            }
            
            // Sag
            found = 0;
            for (channel = 4; channel < sv[svIdx].numChanels-1; channel++){
                if (sv[svIdx].analyseData.phasor[channel][1][0] < sv[svIdx].analyseData.sagThreshold){
                    found = 1;
                    if(!sv[svIdx].analyseData.sagFlag){
                        sv[svIdx].analyseData.sagFlag = 1;
                        clock_gettime(0, &t0);
                        posSagCycle[svIdx] = 0;
                        for (i = 0; i<sv[svIdx].numChanels; i++){

                            sagIdx[i] = 0;
                            
                            for (j=0;j<MAX_TEMPBUFFER_SIZE+1;j++){
                                idx = (tempBufferIdx[svIdx] + 1 + j)%MAX_TEMPBUFFER_SIZE;
                                for (k=0; k <cycleAnalised * sv[svIdx].smpRate; k++){
                                    if (sagIdx[i] >= MAX_SAG_SIZE){
                                        break;
                                    }
                                    sagArr[i][sagIdx[i]] = tempBufferArr[svIdx][idx][i][k];
                                    sagIdx[i]++;
                                }
                            }

                            if (sv[svIdx].idxProcessedCycle - windowSize > 0){
                                idx = cycleAnalised * sv[svIdx].smpRate - (sv[svIdx].idxProcessedCycle - windowSize + tempBufferTrigger[svIdx]);
                            }else{
                                idx = cycleAnalised * sv[svIdx].smpRate - (sv[svIdx].smpRate + sv[svIdx].idxProcessedCycle - windowSize + tempBufferTrigger[svIdx]);
                            }
                            
                            for (j = idx; j < cycleAnalised * sv[svIdx].smpRate; j++){
                                if (sagIdx[i] >= MAX_SAG_SIZE){
                                    break;
                                }
                                sagArr[i][sagIdx[i]] = input[svIdx][i][j][0];
                                sagIdx[i]++;
                            }
                        }
                    }else{
                        for (i = 0; i<sv[svIdx].numChanels; i++){
                            for (j = cycleAnalised * sv[svIdx].smpRate - windowSize; j < cycleAnalised * sv[svIdx].smpRate; j++){
                                if (sagIdx[i] >= MAX_SAG_SIZE){
                                    break;
                                }
                                sagArr[i][sagIdx[i]] = input[svIdx][i][j][0];
                                sagIdx[i]++;
                            }
                        }
                    }
                    break;
                }
            }
            if (sv[svIdx].analyseData.sagFlag){
                if(!found){
                    if (posSagCycle[svIdx] < sv[svIdx].smpRate/windowSize*MAX_TEMPBUFFER_SIZE){
                        for (i = 0; i<sv[svIdx].numChanels; i++){
                            for (j = cycleAnalised * sv[svIdx].smpRate - windowSize; j < cycleAnalised * sv[svIdx].smpRate; j++){
                                if (sagIdx[i] >= MAX_SAG_SIZE){
                                    break;
                                }
                                sagArr[i][sagIdx[i]] = input[svIdx][i][j][0];
                                sagIdx[i]++;
                            }
                        }
                        posSagCycle[svIdx]+=1;
                        continue;
                    }
                    sv[svIdx].analyseData.sagFlag = 0;
                    clock_gettime(0, &t1);
                    double time = (t1.tv_sec - t0.tv_sec) + (t1.tv_nsec - t0.tv_nsec) / 1e9;
                    printf("Sag time: %lf\n", time);
                    saveArr(sagArr[5], sagIdx[5], "sag.csv");
                }
            }

            // sv[svIdx].analyseData.phasor[]


            // Sag


            // char filenamse[256];


            // double arrSave[80];
            // for (i = 0; i < 80; i++){
            //     arrSave[i] = sqrt(2)*sqrt(output[svIdx][0][i][0] * output[svIdx][0][i][0] + output[svIdx][0][i][1] * output[svIdx][0][i][1])/80;
            // }
            // saveArr(arrSave, 80, "output.csv");

            // printf("%lf \n",arrSave[1]);
        }

        // break;
    }

    fclose(fp);

    for (svIdx = 0; svIdx < MAX_SAMPLED_VALUES; svIdx++){
        if (!sv[svIdx].initialized) continue;
        for (channel = 0; channel < sv[svIdx].numChanels; channel++){
            fftw_destroy_plan(plan[svIdx][channel]);
            fftw_free(input[svIdx][channel]);
            fftw_free(output[svIdx][channel]);

        }
        free(input[svIdx]);
        free(output[svIdx]);
        free(plan[svIdx]);
    }

}

sem_t *semaphore;

int main(){

    sv = openSampledValue(0);
    runAnalyse();

    printf("ended\n");
    return 0;
}