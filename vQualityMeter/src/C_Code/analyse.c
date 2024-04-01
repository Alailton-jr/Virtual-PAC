#include "QualityMeter_lib.h"


#define MAX_SAG_SIZE 80*60*5
#define TEMP_BUFFER_SIZE 5

sampledValue_t* sv;
uint16_t windowSize = 20;
fftw_plan *plan[MAX_SAMPLED_VALUES];

int32_t tempBufferArr[MAX_SAMPLED_VALUES][TEMP_BUFFER_SIZE][8][256]; // TODO Dynamic buffer size
int32_t tempBufferTrigger[MAX_SAMPLED_VALUES];
uint32_t tempBufferIdx[MAX_SAMPLED_VALUES];

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

void sagAnalise(sampledValue_t *sv, uint16_t svIdx){
    int found = 0, channel;
    QualityEvent_t *sag = &sv->analyseData.sag;

    // 0-3: Current; 4-7: Voltage
    for (int channel = 4; channel < sv->numChanels-1; channel++){
        if (sv->analyseData.phasor[channel][1][0] < sag->topThreshold){
            found = 1;
            if(!sag->flag){
                sag->flag = 1;
                sag->posCycle = 0;
                clock_gettime(0, &sag->t0);
                for (channel = 0; channel<sv->numChanels; channel++){
                    sag->idx[channel] = 0;
                    for(int idxBuffer = 1; idxBuffer < TEMP_BUFFER_SIZE+1; idxBuffer++){
                        if (sag->idx[channel] + sv->smpRate > MAX_SAG_SIZE){
                            break;
                        }
                        memcpy(&sag->arr[channel][sag->idx[channel]], tempBufferArr[svIdx][(tempBufferIdx[svIdx] + idxBuffer)%TEMP_BUFFER_SIZE][channel], sv->smpRate);
                        sag->idx[channel] += sv->smpRate;
                    }
                }
                sag->bufferIdx = tempBufferIdx[svIdx];
            }else{
                if (sag->bufferIdx != tempBufferIdx[svIdx]){
                    for (channel = 0; channel < sv->numChanels; channel++){
                        if (sag->idx[channel] + sv->smpRate > MAX_SAG_SIZE){
                            break;
                        }
                        memcpy(&sag->arr[channel][sag->idx[channel]], tempBufferArr[svIdx][tempBufferIdx[svIdx]][channel], sv->smpRate);
                        sag->idx[channel] += sv->smpRate;
                    }
                }
            }
            break;
        }
    }
    
    if (sag->flag){
        if(!found){
            if (sag->bufferIdx != tempBufferIdx[svIdx]){
                if (sag->posCycle < TEMP_BUFFER_SIZE){
                    for (channel = 0; channel < sv->numChanels; channel++){
                        if (sag->idx[channel] + sv->smpRate > MAX_SAG_SIZE){
                            break;
                        }
                        memcpy(&sag->arr[channel][sag->idx[channel]], tempBufferArr[svIdx][tempBufferIdx[svIdx]][channel], sv->smpRate);
                        sag->idx[channel] += sv->smpRate;
                    }
                    sag->posCycle++;
                }else{
                    sag->flag = 0;
                    clock_gettime(0, &sag->t1);
                    double time = (sag->t1.tv_sec - sag->t0.tv_sec) + (sag->t1.tv_nsec - sag->t0.tv_nsec) / 1e9;
                    printf("Sag time: %lf\n", time);
                    saveArr(sag->arr[5], sag->idx[5], "sag.csv");
                }
            }
        }
    }

}

void runAnalyse(){
    int i, j, k, idx, idy, _idx, kj;
    uint8_t found;
    int svIdx, channel;
    int cycleAnalised = 1;

    int64_t sum;
    fftw_complex **input[MAX_SAMPLED_VALUES];
    fftw_complex **output[MAX_SAMPLED_VALUES];

    for (i = 0; i < MAX_SAMPLED_VALUES; i++){
        tempBufferTrigger[i] = -1;
        tempBufferIdx[i] = 0;
    }

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
            sagAnalise(&sv[svIdx], svIdx);
        }
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