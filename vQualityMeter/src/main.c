// valgrind --leak-check=full --log-file="valgrind_log.txt" /root/Virtual-PAC/vQualityMeterNew/src/main

#include "qualityAPI.h"
#include "interface.h"
#include "sampledValue.h"
#include "util.h"


#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>
#include <string.h>
#include <unistd.h>
#include <fftw3.h>
#include <complex.h>
#include <signal.h>

cursesThread_t* cursesThd;
clientThread_t* clientThd;
snifferThread_t* snifferThd;
analyserThread_t* analyserThd;
char curDir[128];

SampledValue_t *pSvs;

#define MATH_PI 3.14159265358979323846

#pragma region Interface

void startInterface(){
    cursesThd->snifferRunning = &snifferThd->running;
    cursesThd->analyserRunning = &analyserThd->running;
    cursesThd->clientRunning = &clientThd->running;
    cursesThd->running = 1;
    init_ncurses();
    pthread_create(&cursesThd->thread, NULL, screen_control, (void *)&cursesThd);
}

#pragma endregion

#pragma region CleanUp

void stopSnifferThread(uint8_t wait){
    if (snifferThd->running){
        snifferThd->stop = 1;
        if (wait){
            while (snifferThd->running){
                sleep(1);
            }
        }
        pthread_detach(snifferThd->thread);
    }
}

void stopAnalyserThread(uint8_t wait){
    if (analyserThd->running){
        analyserThd->stop = 1;
        if (wait){
            while (analyserThd->running){
                sleep(1);
            }
        }
        pthread_detach(analyserThd->thread);
    }
}

void cleanUp(){
    // Server cleanup
    clientThread_t *curClient = clientThd->next;
    while (curClient != NULL){
        curClient->stop = 1;
        pthread_detach(curClient->thread);
        curClient = curClient->next;
    }
    stopSnifferThread(1);
    stopAnalyserThread(1);
    if (cursesThd->running) cleanup_ncurses();
}

#pragma endregion


void runServer(){
    int serverSocket = create_socket(IF_NAME, PORT);
    clientThread_t *curClient = clientThd;
    while(1){
        int clientSocket = wait_for_client(serverSocket);
        curClient->next = (clientThread_t *)malloc(sizeof(clientThread_t));
        curClient = curClient->next;
        curClient->clientSocket = clientSocket;
        curClient->stop = 0;
        handle_client((void *)curClient);
        // pthread_create(&curClient->thread, NULL, handle_client, (void *)curClient);
    }
}

int main(){

    // SampledValue_t test;

    SampledValue_t svs[20];
    pSvs = svs; // Glogal Sampled Value Pointer

    clientThread_t _clientThd = {0};
    snifferThread_t _snifferThd = {0};
    analyserThread_t _analyserThd = {0};
    cursesThread_t _cursesThd = {0};

    clientThd = &_clientThd;
    snifferThd = &_snifferThd;
    analyserThd = &_analyserThd;
    cursesThd = &_cursesThd;

    signal(SIGINT, cleanUp);
    signal(SIGTERM, cleanUp);

    get_script_dir(curDir, sizeof(curDir));

    runServer();

    // startInterface();

    // FILE *file = fopen("monitorSetup.yaml", "r");
    // if (file == NULL) {
    //     printf("Error opening file\n");
    //     return 1;
    // }
    // int numSv = 0;
    // SampledValuesYaml_t *sv = parse_yaml(file, &numSv);
    // fclose(file);

    // numSv = 1; //debug

    // SampledValue_t *svArr = (SampledValue_t *)malloc(numSv * sizeof(SampledValue_t));
    // parseYaml2Sv(sv, svArr, numSv);
    // deleteSampledValuesYaml(sv);

    // printf("Number of Sampled Values: %d\n", numSv);
    // for (int i=0; i<numSv; i++){
    //     for (int j=0; j<4; j++){
    //         svArr[i].info.currentPos[0][j] = j;
    //         svArr[i].info.currentPos[1][j] = j;
    //         svArr[i].info.voltagePos[0][j] = j+4;
    //         svArr[i].info.voltagePos[1][j] = j+4;
    //         svArr[i].quality.interruption.save_waveform = 1;
    //         svArr[i].quality.overVoltage.save_waveform = 1;
    //         svArr[i].quality.sag.save_waveform = 1;
    //         svArr[i].quality.sustainedinterruption.save_waveform = 1;
    //         svArr[i].quality.swell.save_waveform = 1;
    //         svArr[i].quality.underVoltage.save_waveform = 1;
    //     }
    // }

    // runAnalyse(numSv, svArr); 

    // // sleep(15);

    // for(int i=0;i<50;i++){
    //     usleep(100000);
    //     // printf("%lf\n", svArr[0].process.rms[4]);
    //     printf("0: %lf | 1: %lf | 2: %lf\n", cabs(svArr[0].process.symV[0]), cabs(svArr[0].process.symV[1]), cabs(svArr[0].process.symV[2]));
    // }

    // stopAnalyse(numSv, svArr);

    // pthread_join(analyserThd.thread, NULL);
    // pthread_join(snifferThd.thread, NULL);

    // run_predefined_capture(numSv, svArr, 8);

    // SampledValue_t *newSvs = (SampledValue_t *)malloc(5 * sizeof(SampledValue_t));

    // run_any_capture(newSvs, 5, 4);

    // pthread_join(snifferThd.thread, NULL);

    // char *data[1];
    // long size = save_captured_sv_data(newSvs, snifferThd.num_sv, data);

    // FILE *fp = fopen("capturedData.yaml", "w");
    // fwrite(data[0], size, 1, fp);
    // fclose(fp);
    // free(data[0]);


    // free(svArr);
    // free(newSvs);
    cleanUp();
    return 0;
}


