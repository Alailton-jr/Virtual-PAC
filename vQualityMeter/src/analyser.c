
#include "analyser.h"
#include "util.h"

#include "fft.h"
#include <math.h>
#include <time.h>
#include <complex.h>
#include <libgen.h>
#include <stdio.h>
#include <unistd.h>
#include <sys/stat.h>

#define PRE_FAULT_CYCLES 5
#define POST_FAULT_CYCLES 5
#define CYCLE_WINDOW 10

static complex double symCompMatrix[3][3] = {
    {1.0/3.0, -0.5/3.0 - 0.86602540378/3.0*I, -0.5/3.0 + 0.86602540378/3.0*I},
    {1.0/3.0, -0.5/3.0 + 0.86602540378/3.0*I, -0.5/3.0 - 0.86602540378/3.0*I},
    {1.0/3.0, 1.0/3.0, 1.0/3.0}
};

static void threePhaseToSymComp(complex double* abc, complex double* sym){
    for (int i=0; i<3; i++){
        sym[i] = 0;
        for (int j=0; j<3; j++){
            sym[i] += symCompMatrix[i][j] * abc[j];
        }
    }
    return;
}

void vtcd_analyser(SampledValue_t* sv, VTCD_Info_t *event, SV_Processing_t* data, char saveDir[180], const char type[], FILE* infoFile){
    // 1º Detect if the event exist on the voltage RMS
        // -> if it's exist, save the last data from buffer into a file
    
    uint8_t detected = 0;
    for (uint8_t cn=0; cn<3;cn++){
        if (data->rms[sv->info.voltagePos[0][cn]] < event->topThreshold)
        if (data->rms[sv->info.voltagePos[0][cn]] > event->bottomThreshold){
            detected = 1;
            for (int i=cn; i<3; i++){
                if(event->minVal > data->rms[sv->info.voltagePos[0][i]] || event->minVal == -1) 
                    event->minVal = data->rms[sv->info.voltagePos[0][i]];
                if(event->maxVal < data->rms[sv->info.voltagePos[0][i]] || event->maxVal == -1) 
                    event->maxVal = data->rms[sv->info.voltagePos[0][i]];
            }
            if(!event->detected){
                clock_gettime(CLOCK_MONOTONIC, &event->t0);
                event->detected = 1;
                event->pos_cycle = 0;
                if (event->save_waveform){ // Open the File to register
                    char filename[180];
                    sprintf(filename, "%s/%s_%s_temp.csv", saveDir, sv->info.svId, type);
                    event->fp = fopen(filename, "w");

                    int bufferIdx, cycleIdx;
                    bufferIdx = data->analyse.idxBuffer - PRE_FAULT_CYCLES*sv->info.smpRate/CYCLE_WINDOW;
                    if (bufferIdx < 0) bufferIdx += sv->info.frequency;
                    cycleIdx = data->analyse.idxCycle;

                    while (bufferIdx != data->analyse.idxBuffer && cycleIdx != data->analyse.idxCycle){
                        for (int j=0; j<sv->info.noChannels; j++){
                            fprintf(event->fp, "%d,", data->captArr[j][bufferIdx][cycleIdx]);
                        }
                        fprintf(event->fp, "\n");
                        cycleIdx++;
                        if (cycleIdx == sv->info.smpRate){
                            cycleIdx = 0;
                            bufferIdx++;
                            if (bufferIdx == sv->info.frequency) bufferIdx = 0;
                        }
                    }
                    bufferIdx = data->analyse.idxBuffer;
                    cycleIdx = data->analyse.idxCycle - CYCLE_WINDOW;
                    if (cycleIdx < 0){
                        cycleIdx += sv->info.smpRate;
                        bufferIdx--;
                    }
                    for (int i=0; i<CYCLE_WINDOW; i++){
                        for (int j=0; j<sv->info.noChannels; j++){
                            fprintf(event->fp, "%d,", data->captArr[j][bufferIdx][cycleIdx]);
                        }
                        fprintf(event->fp, "\n");
                        cycleIdx++;
                        if (cycleIdx == sv->info.smpRate){
                            cycleIdx = 0;
                            bufferIdx++;
                            if (bufferIdx == sv->info.frequency) bufferIdx = 0;
                        }
                    }
                }
            }else{
                int bufferIdx, cycleIdx;
                bufferIdx = data->analyse.idxBuffer;
                cycleIdx = data->analyse.idxCycle - CYCLE_WINDOW;
                if (cycleIdx < 0){
                    cycleIdx += sv->info.smpRate;
                    bufferIdx--;
                    if (bufferIdx < 0) bufferIdx += sv->info.frequency;
                }
                if (event->save_waveform){
                    for (int i=0; i<CYCLE_WINDOW; i++){
                        for (int j=0; j<sv->info.noChannels; j++){
                            fprintf(event->fp, "%d,", data->captArr[j][bufferIdx][cycleIdx]);
                        }
                        fprintf(event->fp, "\n");
                        cycleIdx++;
                        if (cycleIdx == sv->info.smpRate){
                            cycleIdx = 0;
                            bufferIdx++;
                            if (bufferIdx == sv->info.frequency) bufferIdx = 0;
                        }
                    }
                }
            }
            break;
        }
    }
    if(!detected){
        if (event->detected){
            if (event->pos_cycle < POST_FAULT_CYCLES*sv->info.smpRate/CYCLE_WINDOW){
                if (event->pos_cycle == 0){
                    clock_gettime(CLOCK_MONOTONIC, &event->t1);
                }
                event->pos_cycle++;
                if (event->save_waveform){
                    int bufferIdx, cycleIdx;
                    bufferIdx = data->analyse.idxBuffer;
                    cycleIdx = data->analyse.idxCycle - CYCLE_WINDOW;
                    if (cycleIdx < 0){
                        cycleIdx += sv->info.smpRate;
                        bufferIdx--;
                        if (bufferIdx < 0) bufferIdx += sv->info.frequency;
                    }
                    for (int i=0; i<CYCLE_WINDOW; i++){
                        for (int j=0; j<sv->info.noChannels; j++){
                            fprintf(event->fp, "%d,", data->captArr[j][bufferIdx][cycleIdx]);
                        }
                        fprintf(event->fp, "\n");
                        cycleIdx++;
                        if (cycleIdx == sv->info.smpRate){
                            cycleIdx = 0;
                            bufferIdx++;
                            if (bufferIdx == sv->info.frequency) bufferIdx = 0;
                        }
                    }
                }
                
            }else{
                double duration = (event->t1.tv_sec - event->t0.tv_sec) + (event->t1.tv_nsec - event->t0.tv_nsec)/1e9;
                char filename[180];
                time_t rawtime = event->t0.tv_sec;
                struct tm * timeinfo = gmtime(&rawtime);
                if (event->save_waveform){
                    fclose(event->fp);
                    char temp_filename[180];
                    sprintf(temp_filename, "%s/%s_%s_temp.csv", saveDir, sv->info.svId, type);
                    sprintf(filename, "%s/%s_%s_%04d-%02d-%02d_%02d-%02d-%02d_%.3lf.csv", saveDir, sv->info.svId, type, timeinfo->tm_year + 1900, timeinfo->tm_mon + 1, timeinfo->tm_mday, timeinfo->tm_hour, timeinfo->tm_min,  timeinfo->tm_sec, event->t0.tv_nsec/1e9);
                    rename(temp_filename, filename);
                }
                if (duration > event->minDuration && duration < event->maxDuration){
                    char date[120];
                    strftime(date, 120, "%Y-%m-%d %H:%M:%S", timeinfo);
                    sprintf(date, "%s.%03ld", date, event->t0.tv_nsec/1e6);
                    if (event->save_waveform){
                        fprintf(infoFile, "%s | %s | %lf | %d | %d | %s \n", type, date, duration, event->minVal, event->maxVal, filename);
                    }else{
                        fprintf(infoFile, "%s | %s | %lf | %d | %d | %s \n", type, date, duration, event->minVal, event->maxVal, "-");
                    }
                    fflush(infoFile);
                }else{
                    remove(filename);
                }
                event->detected = 0;
                event->minVal = -1;
                event->maxVal = -1;
            }
        }
    }
    
}

int closestPowerOfTwo(int n){
    int i = 1;
    while (i < n){
        i *= 2;
    }
    return i/2;
}

void downsample(complex double* input, int input_size, complex double *output, int output_size) {
    double step = (double)input_size / output_size;
    int i;
    for (i = 0; i < output_size; i++) {
        double index = i * step;
        int lower = (int)index;
        int upper = lower + 1;
        double frac = index - lower;
        if (upper >= input_size) {
            output[i] = input[input_size - 1];
        } else {
            output[i] = input[lower] +  frac * (input[upper] - input[lower]);
        }
    }
}

void* single_sv_analyser(void* ThreadInfo){

#pragma region definitions

    analyserThread_t* svInfo = (analyserThread_t*)ThreadInfo;
    SampledValue_t *sv = svInfo->sv;
    SV_Processing_t *procs = &sv->process;

    int pos;
    pos = procs->sniffer.idxBuffer - 2;
    if (pos < 0) pos += sv->info.frequency;
    procs->analyse.idxBuffer = pos;

    char infoSVFile[180];
    sprintf(infoSVFile, "%s/%s_info.csv", svInfo->saveDir, sv->info.svId);
    svInfo->fp = fopen(infoSVFile, "w");


    pthread_mutex_lock(&procs->mutex);
    uint32_t smpRate = sv->info.smpRate;
    uint32_t frequency = sv->info.frequency;
    uint32_t noChannels = sv->info.noChannels;
    int32_t nomCurrent = sv->info.nomCurrent;
    int32_t nomVoltage = sv->info.nomVoltage;
    sv->process.pSmpRate = closestPowerOfTwo(smpRate);
    uint32_t pSmpRate = sv->process.pSmpRate;
    pthread_mutex_unlock(&procs->mutex);

    complex double *pre_input = (complex double*)malloc(sizeof(complex double) * smpRate);
    complex double *output = (complex double *)malloc(pSmpRate * sizeof(complex double));
    fft_plan_t plan = fft_plan_create(pSmpRate, output);

    svInfo->running = 1;

    while(procs->sniffer.idxBuffer < 10){
        pthread_cond_wait(&procs->condition, &procs->mutex);
        if(*svInfo->stopTh) goto CLOSE_ANALYSER_SINGLE;
    }
    procs->analyse.idxBuffer = procs->sniffer.idxBuffer-5;
    procs->analyse.idxCycle = 0;

    double complex arr3Complex[3];

#pragma endregion

    struct timespec t0, t1;

    while (!(*svInfo->stopTh)){
        
        while(1){
            pos = procs->sniffer.idxBuffer - 2;
            if (pos < 0) pos += sv->info.frequency;
            if (procs->analyse.idxBuffer != pos) break;
            else{
                pthread_cond_wait(&procs->condition, &procs->mutex);
            }
            if (*svInfo->stopTh) goto CLOSE_ANALYSER_SINGLE;
        }
        
        // clock_gettime(CLOCK_MONOTONIC, &t0);

        // ----------- PreProcessing ------------
        // FFT analysis for each Channel
        for (int i = 0; i < noChannels; i++){
            int idxCycle = procs->analyse.idxCycle;
            int idxBuffer = procs->analyse.idxBuffer;
            for (int j = 0; j < smpRate; j++){
                pre_input[j] = (double)sv->process.captArr[i][idxBuffer][idxCycle];
                idxCycle++;
                if (idxCycle == smpRate){
                    idxCycle = 0;
                    idxBuffer++;
                    if (idxBuffer == frequency){
                        idxBuffer = 0;
                    }
                }
            }
            if (smpRate != pSmpRate){
                downsample(pre_input, smpRate, output, pSmpRate);
            }else{
                memcpy(output, pre_input, smpRate * sizeof(complex double));
            }
            fft_exec(&plan);

            for (int j = 0; j<40;j++){
                sv->process.phasor[i][j] = sqrt(2) * (output[j])/pSmpRate;
            }
            sv->process.rms[i] = cabs((sv->process.phasor[i][1]));
        }

        // Symmetrical Components
        for(int i=0;i<3;i++){
            arr3Complex[i] = sv->process.phasor[sv->info.currentPos[0][i]][1];
        }
        threePhaseToSymComp(arr3Complex, sv->process.symI);
        for(int i=0;i<3;i++){
            arr3Complex[i] = sv->process.phasor[sv->info.voltagePos[0][i]][1];
        }
        threePhaseToSymComp(arr3Complex, sv->process.symV);
        
        // clock_gettime(CLOCK_MONOTONIC, &t1);
        // printf("Duration: %lf\n", (t1.tv_sec - t0.tv_sec) + (t1.tv_nsec - t0.tv_nsec)/1e9);
        
        // ----------- Analyser ------------
        // VTCD Analysis
        vtcd_analyser(sv, &sv->quality.sag, procs, svInfo->saveDir, "sag", svInfo->fp);
        vtcd_analyser(sv, &sv->quality.swell, procs, svInfo->saveDir, "swell", svInfo->fp);
        vtcd_analyser(sv, &sv->quality.interruption, procs, svInfo->saveDir, "interruption", svInfo->fp);
        vtcd_analyser(sv, &sv->quality.overVoltage, procs, svInfo->saveDir, "overVoltage", svInfo->fp);
        vtcd_analyser(sv, &sv->quality.underVoltage, procs, svInfo->saveDir, "underVoltage", svInfo->fp);
        vtcd_analyser(sv, &sv->quality.sustainedinterruption, procs, svInfo->saveDir, "sustainedinterruption", svInfo->fp);

        // ----------- Update Indexes ------------
        procs->analyse.idxCycle += CYCLE_WINDOW;
        if (procs->analyse.idxCycle >= smpRate) {
            procs->analyse.idxCycle -= smpRate;
            procs->analyse.idxBuffer++;
            if (procs->analyse.idxBuffer == sv->info.frequency) procs->analyse.idxBuffer = 0;
        }
    }

CLOSE_ANALYSER_SINGLE:


    free(output);
    free(pre_input);
    fft_plan_destroy(&plan);
    fclose(svInfo->fp);
    
    svInfo->running = 0;
    return NULL;
}

void* analyserThread(void* ThreadInfo){
    
    analyserThread_t* analyserInfo = (analyserThread_t*)ThreadInfo;

    char curDir[128];
    get_script_dir(curDir, sizeof(curDir));
    sprintf(analyserInfo->saveDir, "%s/%s", curDir, "analyserFiles");

    sprintf(curDir, "%s/events", analyserInfo->saveDir);

    // Create the directory to save the files and clean it if it already exists
    mkdir(analyserInfo->saveDir, 0777);
    sprintf(curDir, "rm -rf %s/*", analyserInfo->saveDir);
    system(curDir);

    analyserInfo->running = 1;
    analyserThread_t* svInfo = (analyserThread_t*)malloc(analyserInfo->num_sv * sizeof(analyserThread_t));

    for (int i = 0; i < analyserInfo->num_sv; i++){
        
        svInfo[i].sv = &analyserInfo->sv[i];
        svInfo[i].running = 0;
        svInfo[i].num_sv = i;
        svInfo[i].stopTh = &analyserInfo->stop;
        sprintf(svInfo[i].saveDir, "%s", analyserInfo->saveDir);

        pthread_create(&svInfo[i].thread, NULL, single_sv_analyser, (void*)&svInfo[i]);
        // while(!svInfo[i].running){
        //     usleep(1000);
        // }
    }
    for (int i = 0; i < analyserInfo->num_sv; i++){
        pthread_join(svInfo[i].thread, NULL);
    }
    free(svInfo);
    analyserInfo->running = 0;

    return NULL;
}


