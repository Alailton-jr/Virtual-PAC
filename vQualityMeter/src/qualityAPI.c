#include "qualityAPI.h"

#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <string.h>
#include <net/if.h>
#include <sys/ioctl.h>
#include <math.h>
#include <complex.h>

#define MATH_PI 3.14159265358979323846

#pragma region Network_Capture

void run_predefined_capture(int num_sv, SampledValue_t *sv, double captureTime){
    if (snifferThd->running){
        printf("Analyse already running\n");
        return;
    }
    snifferThd->num_sv = num_sv;
    snifferThd->sv = sv;
    snifferThd->stop = 0;
    snifferThd->running = 0;
    snifferThd->save_waveform = 1;
    snifferThd->captureTime = captureTime;
    strcpy(snifferThd->ifName, IF_NAME);
    pthread_create(&snifferThd->thread, NULL, timed_snifferThread, (void *)snifferThd);
}

void run_any_capture(SampledValue_t *sv, int max_sv, double captureTime){
    if (snifferThd->running){
        printf("Analyse already running\n");
        return;
    }

    snifferThd->num_sv = 0;
    snifferThd->max_sv = MAX_SAMPLED_VALUES;
    snifferThd->sv = sv;
    snifferThd->stop = 0;
    snifferThd->running = 0;
    snifferThd->save_waveform = 1;
    snifferThd->captureTime = captureTime;
    strcpy(snifferThd->ifName, IF_NAME);
    pthread_create(&snifferThd->thread, NULL, timed_snifferThread, (void *)snifferThd);
}

#pragma endregion

#pragma region Continuous_Analyse

void runAnalyse_Sniffer(char ifname[] , int num_sv, SampledValue_t *sv){
    if (snifferThd->running){
        printf("Analyse already running\n");
        return;
    }

    snifferThd->num_sv = num_sv;
    snifferThd->sv = sv;
    snifferThd->stop = 0;
    snifferThd->running = 0;
    strcpy(snifferThd->ifName, ifname);
    pthread_create(&snifferThd->thread, NULL, snifferThread, (void *)snifferThd);
}

void stopAnalyse(){
    
    snifferThd->stop = 1;
    pthread_detach(snifferThd->thread);
    // pthread_join(snifferThd->thread, NULL);

    analyserThd->stop = 1;
    while (analyserThd->running){
        for (int i=0;i<snifferThd->num_sv;i++){
            pthread_cond_signal(&snifferThd->sv[i].process.condition);
        }
    }
    pthread_join(analyserThd->thread, NULL);

    for (int i=0;i<snifferThd->num_sv;i++){
        pthread_mutex_destroy(&snifferThd->sv[i].process.mutex);
        pthread_cond_destroy(&snifferThd->sv[i].process.condition);
        for (int j=0;j<snifferThd->sv[i].info.noChannels;j++){
            for (int k=0;k<snifferThd->sv[i].info.frequency;k++){
                free(snifferThd->sv[i].process.captArr[j][k]);
            }

            free(snifferThd->sv[i].process.captArr[j]);
            free(snifferThd->sv[i].process.phasor[j]);
        }
        free(snifferThd->sv[i].process.captArr);
        free(snifferThd->sv[i].process.phasor);
        free(snifferThd->sv[i].process.rms);
    }
}

int load_analyser_setup(const char file_name[], int *num_sv, SampledValue_t *sv){
    FILE *file = fopen(file_name, "r");
    if (file == NULL) {
        printf("Error opening file\n");
        return 1;
    }
    *num_sv = 0;
    SampledValuesYaml_t svYaml[MAX_SAMPLED_VALUES];
    parse_yaml(file, num_sv, svYaml);
    fclose(file);
    if (*num_sv > 0){
        if (parseYaml2Sv(svYaml, *num_sv, sv) != 0){
            return 1;
        }
        return 0;
    }
    return 1;
}

void runAnalyse(){
    if (analyserThd->running){
        stopAnalyse();
    }
    if (snifferThd->running){
        stopSnifferThread(1);
    }

    memset(analyserThd, 0, sizeof(analyserThread_t));
    memset(snifferThd, 0, sizeof(snifferThread_t));


    int num_sv = 0;
    load_analyser_setup("monitorSetup.yaml", &num_sv, pSvs);
    SampledValue_t* sv = pSvs;
    if (num_sv == 0){
        printf("No Sampled Values found\n");
        return;
    }

    for (int i=0;i<num_sv;i++){
        sv[i].process.captArr = (int32_t***)malloc(sv[i].info.noChannels * sizeof(int32_t**));
        sv[i].process.phasor  = (double complex **)malloc(sv[i].info.noChannels * sizeof(double complex *));
        sv[i].process.rms = (double *)malloc(sv[i].info.noChannels * sizeof(double));
        for (int j=0;j<sv[i].info.noChannels;j++){
            sv[i].process.captArr[j] = (int32_t**)malloc(sv[i].info.frequency * sizeof(int32_t*));
            for (int k=0;k<sv[i].info.frequency;k++){
                sv[i].process.captArr[j][k] = (int32_t*)malloc(sv[i].info.smpRate * sizeof(int32_t));
            }
            sv[i].process.phasor[j] = (double complex *)malloc(40 * sizeof(double complex)); // 40º Hamornic
        }

        sv[i].info.currentPos[0][0] = 0;
        sv[i].info.currentPos[0][1] = 1;
        sv[i].info.currentPos[0][2] = 2;
        sv[i].info.currentPos[0][3] = 3;
        sv[i].info.voltagePos[0][0] = 4;
        sv[i].info.voltagePos[0][1] = 5;
        sv[i].info.voltagePos[0][2] = 6;
        sv[i].info.voltagePos[0][3] = 7;

        sv[i].process.sniffer.idxBuffer = 0;
        sv[i].process.sniffer.idxCycle = 0;
        sv[i].process.analyse.idxBuffer = 0;
        sv[i].process.analyse.idxCycle = 0;
        pthread_mutex_init(&sv[i].process.mutex, NULL);
        pthread_cond_init(&sv[i].process.condition, NULL);
    }
    analyserThd->num_sv = num_sv;
    analyserThd->sv = sv;
    analyserThd->stop = 0;
    pthread_create(&analyserThd->thread, NULL, analyserThread, (void *)analyserThd);
    runAnalyse_Sniffer(IF_NAME, num_sv, sv);
}

#pragma endregion

typedef enum {
    CAPT_ANY_SV = 0,
    CAPT_ESPECIFC_SV = 1,
    CAPT_SV_STOP = 2,
    CAPT_SV_STATUS = 3,
    CAPT_SV_DATA = 4,
    CAPT_SV_WAVEFORM = 5,
    ANALYSER_START = 6,
    ANALYSER_STOP = 7,
    ANALYSER_SETUP = 8,
    ANALYSER_STATUS = 9,
    ANALYSER_DATA = 10,
    ANALYSER_EVENT_INFO = 11,
} entryType_e;

void *handle_client(void *threadInfo) {
    clientThread_t *clientThread = (clientThread_t*)threadInfo;
    int client_socket = clientThread->clientSocket;
    char buffer[BUFFER_SIZE];
    clientThread->running = 1;
    int error = 0;
    char curDir[180];
    getcwd(curDir, sizeof(curDir));

    printf("Client connected\n");
    char entry[128];
    while (!clientThread->stop) {
        buffer[0] = -1;
        int bytes_received = recv(client_socket, buffer, BUFFER_SIZE, 0);
        if (bytes_received <= 0 || clientThread->stop) {
            printf("Client disconnected\n");
            break;
        }
        switch (buffer[0]) {
            case CAPT_ANY_SV: {
                if (snifferThd->running){
                    send(client_socket, &error, 4, 0);
                }else{
                    send(client_socket, "success", 7, 0);
                    buffer[bytes_received] = '\0';
                    double captTime = atof(buffer + 5);
                    run_any_capture(pSvs, 20, captTime);
                }
                break;
            }
            case CAPT_ESPECIFC_SV:{
                // Durantion and SV ID -> "str|str"
                if (snifferThd->running){
                    send(client_socket, &error, 4, 0);
                }else{
                    send(client_socket, "success", 7, 0);
                    buffer[bytes_received] = '\0';
                    char *token = strtok(buffer + 5, "|");
                    double captTime = atof(token);
                    token = strtok(NULL, "|");
                    strcpy(pSvs->info.svId, token);
                    run_predefined_capture(1, pSvs, captTime);
                }
                break;
            }
            case CAPT_SV_STOP: {
                stopSnifferThread(0);
                send(client_socket, "success", 7, 0);
                break;
            }
            case CAPT_SV_STATUS: {
                char res[10];
                sprintf(res, "%d|%.2lf|%d|", snifferThd->running, snifferThd->curTime,snifferThd->num_sv);
                send(client_socket, res, 10, 0);
                break;
            }
            case CAPT_SV_DATA: {
                if (snifferThd->running){
                    send(client_socket, &error, 4, 0);
                }else{
                    char *data[2];
                    int size = save_captured_sv_data(snifferThd->sv, snifferThd->num_sv, data);
                    data[1] = (char*) malloc(4 + size);
                    memcpy(data[1], &size, 4);
                    memcpy(data[1] + 4, data[0], size);
                    send(client_socket, data[1], size+4, 0);
                    free(data[0]);
                    free(data[1]);
                }
                break;
            }
            case CAPT_SV_WAVEFORM: {
                char fileName [180];
                buffer[bytes_received] = '\0';
                sprintf(fileName, "%s/captFiles/%s", curDir, buffer + 5);
                FILE *fp = fopen(fileName, "r");
                if (fp == NULL){
                    send(client_socket, &error, 4, 0);
                }else{
                    fseek(fp, 0, SEEK_END);
                    int size = ftell(fp);
                    send(client_socket, &size, 4, 0);
                    fseek(fp, 0, SEEK_SET);
                    char *data = (char*) malloc(size);
                    fread(data, size, 1, fp);
                    fclose(fp);
                    send(client_socket, data, size, 0);
                    free(data);
                }
                break;
            }
            case ANALYSER_START:{
                send(client_socket, "success", 7, 0);
                runAnalyse();
                break;
            }
            case ANALYSER_STOP:{
                send(client_socket, "success", 7, 0);
                stopAnalyse();
                break;
            }
            case ANALYSER_SETUP:{
                int dataLen = buffer[1] + buffer[2]*256 + buffer[3]*65536 + buffer[4]*16777216;
                char *file = (char*) malloc(dataLen);
                int fileBytes = bytes_received-5;
                memcpy(file, buffer + 5, bytes_received - 5);
                while(fileBytes < dataLen){
                    fileBytes += recv(client_socket, file + fileBytes, BUFFER_SIZE, 0);
                    memcpy(file, buffer + 5, bytes_received - 5);
                    bytes_received+= fileBytes;
                }
                send(client_socket, "success", 7, 0);
                FILE *fp = fopen("monitorSetup.yaml", "w");
                fwrite(file, dataLen, 1, fp);
                fclose(fp);
                free(file);
                break;
            }
            case ANALYSER_STATUS:{
                char res[10];
                sprintf(res, "%d|", analyserThd->running);
                send(client_socket, res, 10, 0);
                break;
            }
            case ANALYSER_DATA:{
                if (!analyserThd->running){
                    send(client_socket, &error, 4, 0);
                }else{
                    char svId[40];
                    int found = 0;
                    buffer[bytes_received] = '\0';
                    sprintf(svId, "%s", buffer + 5);
                    for (int i=0; i<analyserThd->num_sv;i++){
                        if (strcmp(analyserThd->sv[i].info.svId, svId) == 0){

                            found = 1;
                            char res[6000];
                            int pos = 0;
                            int arraySize = 6000;

                            // Flags
                            pos +=  snprintf(&res[pos], arraySize - pos, "%u\n", analyserThd->sv[i].process.found);
                            pos +=  snprintf(&res[pos], arraySize - pos, "%u|", analyserThd->sv[i].quality.sag.detected);
                            pos +=  snprintf(&res[pos], arraySize - pos, "%u|", analyserThd->sv[i].quality.swell.detected);
                            pos +=  snprintf(&res[pos], arraySize - pos, "%u|", analyserThd->sv[i].quality.interruption.detected);
                            pos +=  snprintf(&res[pos], arraySize - pos, "%u|", analyserThd->sv[i].quality.overVoltage.detected);
                            pos +=  snprintf(&res[pos], arraySize - pos, "%u|", analyserThd->sv[i].quality.underVoltage.detected);
                            pos +=  snprintf(&res[pos], arraySize - pos, "%u|", analyserThd->sv[i].quality.sustainedinterruption.detected);
                            
                            // RMS
                            pos += snprintf(&res[pos], arraySize - pos, "\n");
                            for (int j=0;j<analyserThd->sv[i].info.noChannels;j++){
                                pos += snprintf(&res[pos], arraySize - pos, "%.4lf|", analyserThd->sv[i].process.rms[j]);
                            }

                            // Power Values Fundamental Only
                            pos += snprintf(&res[pos], arraySize - pos, "\n");
                            double active_power = 0, reactive_power = 0;
                            complex double apparent_power = 0;
                            for (int j=0; j < 3; j++){
                                apparent_power += analyserThd->sv[i].process.phasor[j+4][1] * conj(analyserThd->sv[i].process.phasor[j][1]);
                            }
                            apparent_power = apparent_power/1000;
                            active_power = creal(apparent_power);
                            reactive_power = cimag(apparent_power);
                            pos += snprintf(&res[pos], arraySize - pos, "%.4lf|%.4lf|%.4lf", active_power, reactive_power, cabs(apparent_power));
                            

                            double angRef = carg(analyserThd->sv[i].process.phasor[4][1]);
                            double mod, ang;

                            // Harmonics
                            pos += snprintf(&res[pos], arraySize - pos, "\n");
                            for (int j=0;j<analyserThd->sv[i].info.noChannels;j++){
                                for (int k=0;k<40;k++){
                                    mod = cabs(analyserThd->sv[i].process.phasor[j][k]);
                                    ang = (180/MATH_PI) * (carg(analyserThd->sv[i].process.phasor[j][k]) - angRef);
                                    if (ang > 180) ang -= 360;
                                    if (ang < -180) ang += 360;
                                    pos += snprintf(&res[pos], arraySize - pos, "%.4lf;%.2lf|", mod, ang);
                                }
                            }

                            // Symetrical Component
                            pos += snprintf(&res[pos], arraySize - pos, "\n");
                            angRef = carg(analyserThd->sv[i].process.symV[1]);
                            for (int j=0;j<3;j++){
                                mod = cabs(analyserThd->sv[i].process.symI[j]);
                                ang = (180/MATH_PI) * (carg(analyserThd->sv[i].process.symI[j]) - angRef);
                                pos += snprintf(&res[pos], arraySize - pos, "%.4lf;%.4lf|", mod, ang);
                            }
                            for (int j=0;j<3;j++){
                                mod = cabs(analyserThd->sv[i].process.symV[j]);
                                ang = (180/MATH_PI) * (carg(analyserThd->sv[i].process.symV[j]) - angRef);
                                pos += snprintf(&res[pos], arraySize - pos, "%.4lf;%.4lf|", mod, ang);
                            }
                            pos += snprintf(&res[pos], arraySize - pos, "\n");

                            // Unbalance
                            pos += snprintf(&res[pos], arraySize - pos, "%.2lf|", cabs(analyserThd->sv[i].process.symI[2]/analyserThd->sv[i].process.symI[1])*100);
                            pos += snprintf(&res[pos], arraySize - pos, "%.2lf|", cabs(analyserThd->sv[i].process.symV[2]/analyserThd->sv[i].process.symV[1])*100);

                            // Make a sumary of the file:
                            // - Flags
                            // - RMS
                            // - Power Values Fundamental Only
                            // - Harmonics
                            // - Symetrical Component
                            // - Unbalance

                            
                            FILE* fsada = fopen("test.txt", "w");
                            fwrite(res, pos, 1, fsada);
                            fclose(fsada);

                            send(client_socket, &pos, 4, 0);
                            send(client_socket, res, pos, 0);
                            break;
                        }
                    }
                    if (!found){
                        send(client_socket, &error, 4, 0);
                    }
                    break;
                }
                break;
            }
            case ANALYSER_EVENT_INFO:{
                if (!analyserThd->running){
                    send(client_socket, &error, 4, 0);
                }
                char filePath[300];
                sprintf(filePath, "%s/analyserFiles/%s_info.csv", curDir, buffer + 5);
                // printf("%s\n", filePath);
                FILE *fp = fopen(filePath, "r");
                if (fp == NULL){
                    send(client_socket, &error, 4, 0);
                }else{
                    fseek(fp, 0, SEEK_END);
                    int size = ftell(fp);
                    send(client_socket, &size, 4, 0);
                    fseek(fp, 0, SEEK_SET);
                    char *data = (char*) malloc(size);
                    fread(data, size, 1, fp);
                    fclose(fp);
                    send(client_socket, data, size, 0);
                    free(data);
                }
                break;
            }
            default:
                send(client_socket, &error, 4, 0);
                break;
        }

    }
    close(client_socket);
    free(clientThread);
    clientThread->running = 0;
    if (analyserThd->running){
        stopAnalyse();
    }
    if (snifferThd->running){
        stopSnifferThread(0);
    }
    return NULL;
}

int create_socket(const char ifname[], int port) {
    int server_fd;
    struct sockaddr_in address;
    int opt = 1;

    if ((server_fd = socket(AF_INET, SOCK_STREAM, 0)) == 0) {
        perror("socket failed");
        exit(EXIT_FAILURE);
    }
    
    if (setsockopt(server_fd, SOL_SOCKET, SO_REUSEADDR | SO_REUSEPORT, &opt, sizeof(opt))) {
        perror("setsockopt");
        exit(EXIT_FAILURE);
    }

    // Get IP address of eth0 interface
    struct ifreq ifr;
    memset(&ifr, 0, sizeof(ifr));
    strncpy(ifr.ifr_name, ifname, IFNAMSIZ-1);
    if (ioctl(server_fd, SIOCGIFADDR, &ifr) < 0) {
        perror("ioctl failed");
        exit(EXIT_FAILURE);
    }
    struct sockaddr_in *ip_addr = (struct sockaddr_in *)&ifr.ifr_addr;

    address.sin_family = AF_INET;
    address.sin_addr.s_addr = ip_addr->sin_addr.s_addr;
    address.sin_port = htons(port);
    
    if (bind(server_fd, (struct sockaddr *)&address, sizeof(address))<0) {
        perror("bind failed");
        exit(EXIT_FAILURE);
    }
    
    if (listen(server_fd, 3) < 0) {
        perror("listen");
        exit(EXIT_FAILURE);
    }

    printf("Socket bound to %s:%d\n", inet_ntoa(ip_addr->sin_addr), ntohs(address.sin_port));

    return server_fd;
}

int wait_for_client(int server_fd) {
    int client_socket;
    struct sockaddr_in address;
    int addrlen = sizeof(address);

    // Accept incoming connection
    if ((client_socket = accept(server_fd, (struct sockaddr *)&address, (socklen_t*)&addrlen))<0) {
        perror("accept");
        exit(EXIT_FAILURE);
    }
    printf("\tIP: %s\n", inet_ntoa(address.sin_addr));
    return client_socket;
}
