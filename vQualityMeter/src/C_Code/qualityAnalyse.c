#include "QualityMeter_lib.h"

// #define MAX_SAG_SIZE 80*60*5
#define TEMP_BUFFER_SIZE 5
#define TEMP_BUFFER_RMS_SIZE 100
#define M_PI 3.14159265358979323846

sampledValue_t* sv;
uint16_t windowSize = 80/4;

double symCompMatrix[3][3][2] = {
    {{1/3,0}, {-0.5/3, 0.86602540378/3}, {-0.5/3, -0.86602540378/3}},
    {{1/3,0}, {-0.5/3, -0.86602540378/3}, {-0.5/3, 0.86602540378/3}},
    {{1/3,0}, {1/3,0}, {1/3,0}},
};

int32_t tempBufferArr[MAX_SAMPLED_VALUES][TEMP_BUFFER_SIZE][8][256]; // TODO Dynamic buffer size
int32_t tempBufferTrigger[MAX_SAMPLED_VALUES];
uint32_t tempBufferIdx[MAX_SAMPLED_VALUES];

double tempBufferRMSArr[MAX_SAMPLED_VALUES][TEMP_BUFFER_RMS_SIZE][8];
uint32_t tempBufferRMSIdx[MAX_SAMPLED_VALUES];

fftw_plan *fft_plan[MAX_SAMPLED_VALUES];
fftw_complex **fft_input[MAX_SAMPLED_VALUES];
fftw_complex **fft_output[MAX_SAMPLED_VALUES];

dwt_plan *wt_plan[MAX_SAMPLED_VALUES];
double **wt_input[MAX_SAMPLED_VALUES];
double **wt_output_cA[MAX_SAMPLED_VALUES];
double **wt_output_cD[MAX_SAMPLED_VALUES];

// Latter parallelism
// pthread_mutex_t svMutex[MAX_SAMPLED_VALUES];

FILE *SvInfoFp[MAX_SAMPLED_VALUES];


char curDir[256];

void saveArr(double *arr, int64_t size, char *name){
    FILE *fp = fopen(name, "w");
    if (fp == NULL) {
        perror("Error opening file");
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

#pragma region old

// void saveEvent(QualityEvent_t *event, int16_t channel, int16_t smpRate, char *name){
//     FILE *fp = fopen(name, "w");
//     if (fp == NULL) {
//         printf("Error opening file.\n");
//         return;
//     }
//     for (int i = 0; i < channel; i++) {
//         for (int j = 0; j < smpRate; j++) {
//             fprintf(fp, "%lf", event->arr[i][j]);
//             if (j < smpRate - 1) {
//                 fprintf(fp, ",");
//             }
//         }
//         fprintf(fp, "\n");
//     }
//     fclose(fp);
// }

// void swellAnalise(sampledValue_t *sv, uint16_t svIdx){

//     int found = 0;
//     QualityEvent_t *swell = &sv->analyseData.swell;

//     // 0-3: Current; 4-7: Voltage
//     for (int channel = 4; channel < sv->numChanels-1; channel++){
//         if ( sv->analyseData.phasor_polar[channel][1][0] > swell->bottomThreshold ){
//             found = 1;
//             if(!swell->flag){
//                 printf("%lf\n ", sv->analyseData.phasor_polar[channel][1][0]); // Debug
//                 swell->flag = 1;
//                 swell->posCycle = 0;
//                 clock_gettime(0, &swell->t0);
//                 for (channel = 0; channel<sv->numChanels; channel++){
//                     swell->idx[channel] = 0;
//                     for (int idxBuffer = 1; idxBuffer < TEMP_BUFFER_RMS_SIZE+1; idxBuffer++){
//                         if (swell->idx[channel] > MAX_BUFFER_EVENT_SIZE) break;
//                         swell->arr[channel][swell->idx[channel]] = tempBufferRMSArr[svIdx][(tempBufferIdx[svIdx] + idxBuffer)%TEMP_BUFFER_SIZE][channel];
//                         swell->idx[channel]++;
//                     }
//                 }
//             }else{
//                 for (channel = 0; channel < sv->numChanels; channel++){
//                     if (swell->idx[channel] > MAX_BUFFER_EVENT_SIZE) break;
//                     else{
//                         swell->arr[channel][swell->idx[channel]] = tempBufferRMSArr[svIdx][tempBufferRMSIdx[svIdx]][channel];
//                         swell->idx[channel]++;
//                     }
//                 }
//             }
//             break;
//         }
//     }
//     if (swell->flag){
//         if(!found){
//             if (swell->bufferIdx != tempBufferIdx[svIdx]){
//                 if (swell->posCycle < TEMP_BUFFER_RMS_SIZE){
//                     for (int channel = 0; channel < sv->numChanels; channel++){
//                         if (swell->idx[channel] > MAX_BUFFER_EVENT_SIZE) 
//                             break;
//                         else{
//                             swell->arr[channel][swell->idx[channel]] = tempBufferRMSArr[svIdx][tempBufferRMSIdx[svIdx]][channel];
//                             swell->idx[channel]++;
//                         }
//                     }
//                     swell->posCycle++;
//                 }else{
//                     swell->flag = 0;
//                     clock_gettime(0, &swell->t1);
//                     double time_val = (swell->t1.tv_sec - swell->t0.tv_sec) + (swell->t1.tv_nsec - swell->t0.tv_nsec) / 1e9;
//                     printf("Swell time: %lf\n", time_val); // Debug
//                     struct tm *local_time;
//                     time_t current_time;
//                     current_time = time(NULL);
//                     local_time = localtime(&current_time);
//                     char fileName [256];
//                     sprintf(fileName, "files/%s_swell_%04d-%02d-%02d_%02d-%02d-%02d.csv", sv->svId,
//                         local_time->tm_year + 1900, local_time->tm_mon + 1, local_time->tm_mday,
//                         local_time->tm_hour, local_time->tm_min, local_time->tm_sec);
//                     saveEvent(swell, sv->numChanels, swell->idx[0], fileName);
//                 }
//             }
//         }
//     }

// }

// void sagAnalise(sampledValue_t *sv, uint16_t svIdx){
//     int found = 0;
//     QualityEvent_t *sag = &sv->analyseData.sag;

//     // 0-3: Current; 4-7: Voltage
//     for (int channel = 4; channel < sv->numChanels-1; channel++){
//         if (sv->analyseData.phasor_polar[channel][1][0] < sag->topThreshold &&
//             sv->analyseData.phasor_polar[channel][1][0] > sag->bottomThreshold){
//             found = 1;
//             if(!sag->flag){
//                 printf("%lf\n ", sv->analyseData.phasor_polar[channel][1][0]); // Debug
//                 sag->flag = 1;
//                 sag->posCycle = 0;
//                 clock_gettime(0, &sag->t0);
//                 for (channel = 0; channel<sv->numChanels; channel++){
//                     sag->idx[channel] = 0;
//                     for (int idxBuffer = 1; idxBuffer < TEMP_BUFFER_RMS_SIZE+1; idxBuffer++){
//                         if (sag->idx[channel] > MAX_BUFFER_EVENT_SIZE) break;
//                         sag->arr[channel][sag->idx[channel]] = tempBufferRMSArr[svIdx][(tempBufferIdx[svIdx] + idxBuffer)%TEMP_BUFFER_SIZE][channel];
//                         sag->idx[channel]++;
//                     }
//                 }
//             }else{
//                 for (channel = 0; channel < sv->numChanels; channel++){
//                     if (sag->idx[channel] > MAX_BUFFER_EVENT_SIZE) break;
//                     else{
//                         sag->arr[channel][sag->idx[channel]] = tempBufferRMSArr[svIdx][tempBufferRMSIdx[svIdx]][channel];
//                         sag->idx[channel]++;
//                     }

//                 }
//             }
//             break;
//         }
//     }
//     if (sag->flag){
//         if(!found){
//             if (sag->bufferIdx != tempBufferIdx[svIdx]){
//                 if (sag->posCycle < TEMP_BUFFER_RMS_SIZE*3){
//                     for (int channel = 0; channel < sv->numChanels; channel++){
//                         if (sag->idx[channel] > MAX_BUFFER_EVENT_SIZE) 
//                             break;
//                         else{
//                             sag->arr[channel][sag->idx[channel]] = tempBufferRMSArr[svIdx][tempBufferRMSIdx[svIdx]][channel];
//                             sag->idx[channel]++;
//                         }
//                     }
//                     sag->posCycle++;
//                 }else{
//                     sag->flag = 0;
//                     clock_gettime(0, &sag->t1);
//                     double time_val = (sag->t1.tv_sec - sag->t0.tv_sec) + (sag->t1.tv_nsec - sag->t0.tv_nsec) / 1e9;
//                     printf("Sag time: %lf\n", time_val); // Debug
//                     struct tm *local_time;
//                     time_t current_time;
//                     current_time = time(NULL);
//                     local_time = localtime(&current_time);
//                     char fileName [512];
//                     sprintf(fileName, "%s/files/%s_sag_%04d-%02d-%02d_%02d-%02d-%02d.csv", curDir, sv->svId,
//                         local_time->tm_year + 1900, local_time->tm_mon + 1, local_time->tm_mday,
//                         local_time->tm_hour, local_time->tm_min, local_time->tm_sec);
//                     if (time_val > 0.01)
//                     saveEvent(sag, sv->numChanels, sag->idx[0], fileName);
//                 }
//             }
//         }
//     }
// }

// void transientAnalise(sampledValue_t *sv, uint16_t svIdx){
//     return;
// }

#pragma endregion

void threePhaseToSymComp(sampledValue_t *sv){
    for (int i = 0; i < 3; i++) {
        sv->analyseData.symetrical[0][i][0] = 0;
        sv->analyseData.symetrical[0][i][1] = 0;
        for (int j = 0; j < 3; j++){
            sv->analyseData.symetrical[0][i][0] += symCompMatrix[i][j][0] * sv->analyseData.phasor_rect[j][1][0] - symCompMatrix[i][j][1] * sv->analyseData.phasor_rect[j][1][1];
            sv->analyseData.symetrical[0][i][1] += symCompMatrix[i][j][0] * sv->analyseData.phasor_rect[j][1][1] + symCompMatrix[i][j][1] * sv->analyseData.phasor_rect[j][1][0];
        }
    }
}

void VTCDAnalyse(sampledValue_t *sv, QualityEvent_t *event, uint16_t svIdx){

    // 1º Detect if the event exist on the current RMS
        // -> if it's exist, save the last data from buffer into a file
    // 2 

    // Considere that is 4 current and 4 voltage
    uint8_t cn, flag = 0;
    for (cn = 4; cn < sv->numChanels-1;cn++){
        // Check if limits are violeted
        if ( sv->rms[cn] < event->topThreshold && sv->rms[cn] > event->bottomThreshold ){
            // Check if the event is already detected
            if (sv->rms[cn] < event->minVal || event->minVal + 1 < 0.001)  event->minVal = sv->rms[cn];
            if (sv->rms[cn] > event->maxVal || event->maxVal + 1 < 0.001)  event->maxVal = sv->rms[cn];
            if (flag) continue;
            flag = 1;
            if (!event->flag){
                // if it's not previously detected
                event->flag = 1;
                clock_gettime(0, &event->t0);

                struct tm *local_time; time_t current_time;
                current_time = time(NULL); local_time = localtime(&current_time);
                sprintf(event->fileName, "%s/files/%s_%s_%04d-%02d-%02d_%02d-%02d-%02d.csv", curDir, sv->svId, event->eventName,local_time->tm_year + 1900, local_time->tm_mon + 1, local_time->tm_mday, local_time->tm_hour, local_time->tm_min,  local_time->tm_sec);
                
                event->fp = fopen(event->fileName, "w");

                for (int idxBuffer = 1; idxBuffer < TEMP_BUFFER_RMS_SIZE+1; idxBuffer++){
                    for (int cnBuffer = 0; cnBuffer < sv->numChanels; cnBuffer++){
                        fprintf(event->fp, "%lf, ", tempBufferRMSArr[svIdx][(tempBufferIdx[svIdx] + idxBuffer)%TEMP_BUFFER_SIZE][cnBuffer]);
                    }
                    fprintf(event->fp, "\n");
                }
            }
            else{
                for (int channel = 0; channel < sv->numChanels; channel++){
                    fprintf(event->fp, "%lf, ",sv->rms[channel]);
                }
                fprintf(event->fp, "\n");
            }
            event->posCycle = 0;
        }
    }
    if (!flag && event->flag){
        if (event->posCycle == 0){
            clock_gettime(0, &event->t1);
            event->duration = (event->t1.tv_sec - event->t0.tv_sec) + (event->t1.tv_nsec - event->t0.tv_nsec) / 1e9;
        }
        if (event->posCycle < TEMP_BUFFER_RMS_SIZE){
            event->posCycle++;
            for (int channel = 0; channel < sv->numChanels; channel++){
                fprintf(event->fp, "%lf, ",sv->rms[channel]);
            }
            fprintf(event->fp, "\n");
        }else{
            for (int channel = 0; channel < sv->numChanels; channel++){
                fprintf(event->fp, "%lf, ",sv->rms[channel]);
            }
            fprintf(event->fp, "\n", event->duration);
            event->flag = 0;
            fclose(event->fp);
            if (event->duration < event->minDuration || event->duration > event->maxDuration){
                remove(event->fileName);
            }else{
                if (SvInfoFp[svIdx] != NULL){
                    fprintf(SvInfoFp[svIdx], "%s | %lf | %lf | %lf | %s \n", event->eventName, event->duration, event->minVal, event->maxVal, event->fileName);
                    fflush(SvInfoFp[svIdx]);
                }
            }
            event->minVal = event->maxVal = -1;
        }
    }
}

void runAnalyse(){
    int i, j, k, idx, idy, _idx, kj;
    uint8_t found;
    int svIdx, channel;
    int cycleAnalised = 1;
    int idxBuffer, smpGap = 2;
    

    int64_t sum;
    for (i = 0; i < MAX_SAMPLED_VALUES; i++){
        tempBufferTrigger[i] = -1;
        tempBufferIdx[i] = 0;

        tempBufferRMSIdx[i] = 0;
    }

    uint32_t sagIdx[8];
    int32_t posSagCycle[8];

    int debug = 0;
    uint32_t wt_pos[MAX_SAMPLED_VALUES][8];

    //Debug
    struct timespec t0, t1;

    for (svIdx = 0; svIdx < MAX_SAMPLED_VALUES; svIdx++){
        if (!sv[svIdx].initialized) continue;
        fft_plan[svIdx] = (fftw_plan*) malloc(sv[svIdx].numChanels * sizeof(fftw_plan));
        fft_input[svIdx] = (fftw_complex**) malloc(sv[svIdx].numChanels * sizeof(fftw_complex*));
        fft_output[svIdx] = (fftw_complex**) malloc(sv[svIdx].numChanels * sizeof(fftw_complex*));
        
        wt_plan[svIdx] = (dwt_plan*) malloc(sv[svIdx].numChanels * sizeof(dwt_plan));
        wt_input[svIdx] = (double**) malloc(sv[svIdx].numChanels * sizeof(double*));
        wt_output_cA[svIdx] = (double**) malloc(sv[svIdx].numChanels * sizeof(double*));
        wt_output_cD[svIdx] = (double**) malloc(sv[svIdx].numChanels * sizeof(double*));

        for (channel = 0; channel < sv[svIdx].numChanels; channel++){
            fft_input[svIdx][channel] = (fftw_complex*) fftw_malloc(cycleAnalised * sv[svIdx].smpRate * sizeof(fftw_complex));
            fft_output[svIdx][channel] = (fftw_complex*) fftw_malloc(cycleAnalised * sv[svIdx].smpRate * sizeof(fftw_complex));
            fft_plan[svIdx][channel] = fftw_plan_dft_1d(cycleAnalised * sv[svIdx].smpRate, fft_input[svIdx][channel], fft_output[svIdx][channel], FFTW_FORWARD, FFTW_ESTIMATE);

            wt_input[svIdx][channel] = (double*) malloc(cycleAnalised * sv[svIdx].smpRate * sizeof(double));
            wt_output_cA[svIdx][channel] = (double*) malloc(cycleAnalised * sv[svIdx].smpRate * sizeof(double));
            wt_output_cD[svIdx][channel] = (double*) malloc(cycleAnalised * sv[svIdx].smpRate * sizeof(double));
            wt_plan[svIdx][channel] = wt_dwt_plan_1d(cycleAnalised * sv[svIdx].smpRate, "db4", wt_input[svIdx][channel],wt_output_cA[svIdx][channel], wt_output_cD[svIdx][channel]);
        }
    }

    int32_t stop = 0;
    stop++;

    while(1){
        for (svIdx = 0; svIdx < MAX_SAMPLED_VALUES; svIdx++){
            if (!sv[svIdx].initialized) continue;

            idxBuffer = sv[svIdx].idxBuffer - smpGap;
            if (idxBuffer < 0) idxBuffer = sv[svIdx].freq + idxBuffer;
            if (sv[svIdx].idxProcessedBuffer == idxBuffer) continue;

            for (channel = 0; channel < sv[svIdx].numChanels;channel++){
                k = 0;

                for (i = 0; i < cycleAnalised; i++){
                    idx = sv[svIdx].idxProcessedBuffer + i; //sv[svIdx].freq;
                    if (idx >= sv[svIdx].freq) idx = 0;
                    idy = sv[svIdx].idxProcessedCycle;
                    for (j = 0; j < sv[svIdx].smpRate; j++){
                        idy++;
                        if (idy >= sv[svIdx].smpRate){
                            idx++;
                            if (idx >= sv[svIdx].freq) idx = 0;
                            idy = 0;
                        }
                        fft_input[svIdx][channel][k][0] = sv[svIdx].analyseArr[channel][idx][idy];
                        fft_input[svIdx][channel][k][1] = 0.0;

                        wt_input[svIdx][channel][k] = sv[svIdx].analyseArr[channel][idx][idy];
                        
                        k++;
                    }
                }

                fftw_execute(fft_plan[svIdx][channel]);
                wt_excecute(wt_plan[svIdx][channel]);
                
                if (tempBufferTrigger[svIdx] == -1){
                        tempBufferTrigger[svIdx] = idx;
                    }
                if (tempBufferTrigger[svIdx] == idx){
                    for(i = 0; i<k; i++){
                        tempBufferArr[svIdx][tempBufferIdx[svIdx]][channel][i] = fft_input[svIdx][channel][i][0];
                    }
                    tempBufferIdx[svIdx]++;
                    if (tempBufferIdx[svIdx] == 5){
                        tempBufferIdx[svIdx] = 0;
                    }
                }
            }

            sv[svIdx].idxProcessedCycle += windowSize;
            if (sv[svIdx].idxProcessedCycle >= sv[svIdx].smpRate){
                sv[svIdx].idxProcessedCycle -= sv[svIdx].smpRate;
                sv[svIdx].idxProcessedBuffer++;
                if (sv[svIdx].idxProcessedBuffer >= sv[svIdx].freq){
                    sv[svIdx].idxProcessedBuffer = 0;
                }
            }

            for (channel = 0; channel < sv[svIdx].numChanels; channel++){
                sv[svIdx].rms[channel] = 0;
                for (i = 0; i < MAX_HARMONIC; i++){
                    sv[svIdx].analyseData.phasor_polar[channel][i][0] = sqrt(fft_output[svIdx][channel][i][0] * fft_output[svIdx][channel][i][0] + fft_output[svIdx][channel][i][1] * fft_output[svIdx][channel][i][1])/(cycleAnalised * sv[svIdx].smpRate) * 1.41421356;
                    sv[svIdx].analyseData.phasor_polar[channel][i][1] = atan2(fft_output[svIdx][channel][i][1], fft_output[svIdx][channel][i][0]);

                    sv[svIdx].analyseData.phasor_rect[channel][i][0] = fft_output[svIdx][channel][i][0]/(cycleAnalised * sv[svIdx].smpRate) * 1.41421356;
                    sv[svIdx].analyseData.phasor_rect[channel][i][1] = fft_output[svIdx][channel][i][1]/(cycleAnalised * sv[svIdx].smpRate) * 1.41421356;

                    sv[svIdx].rms[channel] +=  sv[svIdx].analyseData.phasor_polar[channel][i][0] * sv[svIdx].analyseData.phasor_polar[channel][i][0];
                }
                sv[svIdx].rms[channel] = sqrt(sv[svIdx].rms[channel]);

                tempBufferRMSArr[svIdx][tempBufferRMSIdx[svIdx]][channel] = sv[svIdx].analyseData.phasor_polar[channel][1][0];

                // if (!debug)
                // for (i=sv[svIdx].smpRate*0.4; i<sv[svIdx].smpRate*0.6; i++){
                //     if (wt_output_cD[svIdx][channel][i] > 1){
                //         debug = 1;
                //         saveArr(wt_output_cD[svIdx][channel], sv[svIdx].smpRate, "wt_output.csv");
                //         saveArr(wt_input[svIdx][channel], sv[svIdx].smpRate, "wt_input.csv");
                //     }
                // }
            }
            tempBufferRMSIdx[svIdx]++;
            if (tempBufferRMSIdx[svIdx] > TEMP_BUFFER_RMS_SIZE){
                tempBufferRMSIdx[svIdx] = 0;
            }

            VTCDAnalyse(&sv[svIdx], &(sv[svIdx].analyseData.sag), svIdx);
            // VTCDAnalyse(&sv[svIdx], &(sv[svIdx].analyseData.swell), svIdx);
            // VTCDAnalyse(&sv[svIdx], &(sv[svIdx].analyseData.interruption), svIdx);
            // threePhaseToSymComp(&sv[svIdx]);
            // sagAnalise(&sv[svIdx], svIdx);
            // swellAnalise(&sv[svIdx], svIdx);
        }
    }

    for (svIdx = 0; svIdx < MAX_SAMPLED_VALUES; svIdx++){
        if (!sv[svIdx].initialized) continue;
        for (channel = 0; channel < sv[svIdx].numChanels; channel++){
            fftw_destroy_plan(fft_plan[svIdx][channel]);
            fftw_free(fft_input[svIdx][channel]);
            fftw_free(fft_output[svIdx][channel]);

        }
        free(fft_input[svIdx]);
        free(fft_output[svIdx]);
        free(fft_plan[svIdx]);
    }

}

FILE *fp;
void cleanUp(int signal){
    for (int svIdx = 0; svIdx < MAX_SAMPLED_VALUES; svIdx++){
        if (!sv[svIdx].initialized) continue;
        for (int channel = 0; channel < sv[svIdx].numChanels; channel++){
            free(fft_input[svIdx][channel]);
            free(fft_output[svIdx][channel]);
            fftw_destroy_plan(fft_plan[svIdx][channel]);
        }
        free(fft_input[svIdx]);
        free(fft_output[svIdx]);
        free(fft_plan[svIdx]);
        fclose(SvInfoFp[svIdx]);
    }
    fclose(fp);
    printf("Leaving Quality Analyser\n");
    exit(0);
}

int main(){

    fp = fopen("data.csv", "w");

    signal(SIGINT, cleanUp);
    signal(SIGTERM, cleanUp);

    char filePath[512];
    readlink("/proc/self/exe", filePath, sizeof(filePath) - 1);
    strcpy(curDir, dirname(filePath));

    sv = openSampledValue(0);

    
    uint8_t nSVdetected = 0;
    for (int i = 0; i < MAX_SAMPLED_VALUES; i++){
        if (sv[i].initialized) {
            char svInfoName[512];
            nSVdetected++;
            sv[i].analyseData.interruption.flag = 0;
            sv[i].analyseData.overVoltage.flag = 0;
            sv[i].analyseData.sag.flag = 0;
            sv[i].analyseData.swell.flag = 0;
            sv[i].analyseData.transient.flag = 0;
            sv[i].analyseData.underVoltage.flag = 0;
            sprintf(svInfoName, "%s/files/%s.info", curDir, sv[i].svId);
            SvInfoFp[i] = fopen(svInfoName,"a");
        }
    }
    printf("Number of Sampled Values detected: %d\n", nSVdetected);

    runAnalyse();

    printf("ended\n");
    return 0;
}


// Voltage Sags and Swells: Sudden, short-duration decreases (sags) or increases (swells) in voltage levels, often caused by faults or switching operations.

// Voltage Interruptions: Complete loss of voltage for a short period, typically caused by faults or equipment failure.

// Voltage Fluctuations: Rapid and repetitive variations in voltage magnitude, often caused by the operation of large loads or switching of equipment.

// Harmonics: Non-linear loads such as variable speed drives or power electronics can introduce harmonic currents, resulting in distorted voltage and current waveforms. Harmonics can lead to overheating of equipment and interference with sensitive electronics.

// Transient Voltage Surges: Short-duration increases in voltage, commonly caused by lightning strikes, switching operations, or capacitor switching.

// Voltage Unbalance: Imbalance in the distribution of voltages among the phases of a three-phase system, leading to unequal loading and potential overheating of equipment.

// Flicker: Rapid variations in voltage magnitude that can cause visible fluctuations in lighting intensity, often attributed to large fluctuating loads.

// Frequency Variations: Departures from the nominal frequency of the power system, which can affect the operation of synchronous equipment and clocks.
