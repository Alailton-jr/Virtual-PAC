
#include "analyser.h"
#include <fftw3.h>
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

complex double symCompMatrix[3][3] = {
    {1.0/3.0, -0.5/3.0 - 0.86602540378/3.0*I, -0.5/3.0 + 0.86602540378/3.0*I},
    {1.0/3.0, -0.5/3.0 + 0.86602540378/3.0*I, -0.5/3.0 - 0.86602540378/3.0*I},
    {1.0/3.0, 1.0/3.0, 1.0/3.0}
};

void threePhaseToSymComp(complex double* abc, complex double* sym){
    // cm
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
                // save current data buffer into the file
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
            }else{
                double duration = (event->t1.tv_sec - event->t0.tv_sec) + (event->t1.tv_nsec - event->t0.tv_nsec)/1e9;
                char filename[180];
                if (event->save_waveform){
                    fclose(event->fp);
                    char temp_filename[180];
                    time_t rawtime = event->t0.tv_sec;
                    struct tm * timeinfo = gmtime(&rawtime);
                    sprintf(temp_filename, "%s/%s_%s_temp.csv", saveDir, sv->info.svId, type);
                    sprintf(filename, "%s/%s_%s_%04d-%02d-%02d_%02d-%02d-%02d_%.3lf.csv", saveDir, sv->info.svId, type, timeinfo->tm_year + 1900, timeinfo->tm_mon + 1, timeinfo->tm_mday, timeinfo->tm_hour, timeinfo->tm_min,  timeinfo->tm_sec, event->t0.tv_nsec/1e9);
                    rename(temp_filename, filename);
                }
                if (duration > event->minDuration && duration < event->maxDuration){
                    if (event->save_waveform){
                        fprintf(infoFile, "%s | %lf | %d | %d | %s \n", type, duration, event->minVal, event->maxVal, filename);
                    }else{
                        fprintf(infoFile, "%s | %lf | %d | %d | %s \n", type, duration, event->minVal, event->maxVal, "-");
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
    pthread_mutex_unlock(&procs->mutex);

    double (*input)[2] = (double (*)[2])malloc(sizeof(double[2]) * smpRate);
    double (*output)[2] = (double (*)[2])malloc(sizeof(double[2]) * smpRate);
    fftw_plan plan = fftw_plan_dft_1d(smpRate, (fftw_complex*)input, (fftw_complex*)output, FFTW_FORWARD, FFTW_ESTIMATE);

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
        
        clock_gettime(CLOCK_MONOTONIC, &t0);

        // PreProcessing
        // FFT analysis for each Channel
        for (int i = 0; i < noChannels; i++){
            int idxCycle = procs->analyse.idxCycle;
            int idxBuffer = procs->analyse.idxBuffer;
            for (int j = 0; j < smpRate; j++){
                input[j][0] = sv->process.captArr[i][idxBuffer][idxCycle];
                input[j][1] = 0;
                idxCycle++;
                if (idxCycle == smpRate){
                    idxCycle = 0;
                    idxBuffer++;
                    if (idxBuffer == frequency){
                        idxBuffer = 0;
                    }
                }
            }
            fftw_execute(plan);

            for (int j = 0; j<40;j++){
                sv->process.phasor[i][j] = sqrt(2) * (output[j][0] + I *output[j][1])/smpRate;
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

        clock_gettime(CLOCK_MONOTONIC, &t1);
        // printf("Duration: %lf\n", (t1.tv_sec - t0.tv_sec) + (t1.tv_nsec - t0.tv_nsec)/1e9);

        // VTCD Analysis
        vtcd_analyser(sv, &sv->quality.sag, procs, svInfo->saveDir, "sag", svInfo->fp);
        vtcd_analyser(sv, &sv->quality.swell, procs, svInfo->saveDir, "swell", svInfo->fp);
        vtcd_analyser(sv, &sv->quality.interruption, procs, svInfo->saveDir, "interruption", svInfo->fp);
        vtcd_analyser(sv, &sv->quality.overVoltage, procs, svInfo->saveDir, "overVoltage", svInfo->fp);
        vtcd_analyser(sv, &sv->quality.underVoltage, procs, svInfo->saveDir, "underVoltage", svInfo->fp);
        vtcd_analyser(sv, &sv->quality.sustainedinterruption, procs, svInfo->saveDir, "sustainedinterruption", svInfo->fp);

        procs->analyse.idxCycle += CYCLE_WINDOW;
        if (procs->analyse.idxCycle >= smpRate) {
            procs->analyse.idxCycle -= smpRate;
            procs->analyse.idxBuffer++;
            if (procs->analyse.idxBuffer == sv->info.frequency) procs->analyse.idxBuffer = 0;
        }
    }

CLOSE_ANALYSER_SINGLE:

    fftw_destroy_plan(plan);
    free(input);
    free(output);
    fclose(svInfo->fp);
    
    svInfo->running = 0;
    return NULL;
}

void* analyserThread(void* ThreadInfo){
    
    analyserThread_t* analyserInfo = (analyserThread_t*)ThreadInfo;

    char curDir[128];
    getcwd(curDir, 128);
    sprintf(analyserInfo->saveDir, "%s/%s", curDir, "analyserFiles");

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
        while(!svInfo[i].running){
            // FFTW Plan getting problem when multiples threads are created at the same time
            usleep(1000);
        }
    }
    for (int i = 0; i < analyserInfo->num_sv; i++){
        pthread_join(svInfo[i].thread, NULL);
    }
    free(svInfo);
    analyserInfo->running = 0;

    return NULL;
}


