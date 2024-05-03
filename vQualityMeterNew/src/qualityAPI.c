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

#pragma region Network_Capture

void run_predefined_capture(int num_sv, SampledValue_t *sv, double captureTime){
    if (snifferThd->running){
        printf("Analyse already running\n");
        return;
    }
    snifferThd->nThread = 1;
    snifferThd->nTask = 8;
    snifferThd->maxFrameSize = 2048;
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
    snifferThd->nThread = 1;
    snifferThd->nTask = 1;
    snifferThd->maxFrameSize = 2048;
    snifferThd->num_sv = 0;
    snifferThd->max_sv = max_sv;
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

void runAnalyse_Sniffer(int nThread, int nTask, int maxFrameSize, char ifname[] , int num_sv, SampledValue_t *sv){
    if (snifferThd->running){
        printf("Analyse already running\n");
        return;
    }
    snifferThd->nThread = nThread;
    snifferThd->nTask = nTask;
    snifferThd->maxFrameSize = maxFrameSize;
    snifferThd->num_sv = num_sv;
    snifferThd->sv = sv;
    snifferThd->stop = 0;
    snifferThd->running = 0;
    strcpy(snifferThd->ifName, ifname);
    pthread_create(&snifferThd->thread, NULL, snifferThread, (void *)snifferThd);
}

void stopAnalyse(){
    
    snifferThd->stop = 1;
    pthread_join(snifferThd->thread, NULL);

    analyserThd->stop = 1;
    for (int i=0;i<snifferThd->num_sv;i++){
        while (analyserThd->running){
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
    }
    free(snifferThd->sv);
}

SampledValue_t* load_analyser_setup(const char file_name[], int *num_sv){
    FILE *file = fopen(file_name, "r");
    if (file == NULL) {
        printf("Error opening file\n");
        return 1;
    }
    *num_sv = 0;
    SampledValuesYaml_t *svYaml = parse_yaml(file, num_sv);
    fclose(file);
    if (*num_sv > 0){
        SampledValue_t *sv =  parseYaml2Sv(svYaml, *num_sv);
        deleteSampledValuesYaml(svYaml);
        return sv;
    }
    return NULL;
}

void runAnalyse(){
    if (analyserThd->running){
        stopAnalyse();
    }
    int num_sv = 0;
    SampledValue_t *sv = load_analyser_setup("monitorSetup.yaml", &num_sv);
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
    runAnalyse_Sniffer(2, 8, 2048, IF_NAME, num_sv, sv);
}

#pragma endregion

typedef enum {
    CAPT_ANY_SV = 0,
    CAPT_ANY_SV_STOP = 1,
    CAPT_ANY_SV_STATUS = 2,
    CAPT_ANY_SV_DATA = 3,
    CAPT_ANY_SV_WAVEFORM = 4,
    ANALYSER_START = 5,
    ANALYSER_STOP = 6,
    ANALYSER_SETUP = 7,
    ANALYSER_STATUS = 8,
    ANALYSER_DATA = 9,
    ANALYSER_EVENT_INFO = 10
} entryType_e;

void *handle_client(void *threadInfo) {
    clientThread_t *clientThread = (clientThread_t*)threadInfo;
    int client_socket = clientThread->clientSocket;
    char buffer[BUFFER_SIZE];
    clientThread->running = 1;
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
                    send(client_socket, "error", 5, 0);
                }else{
                    buffer[bytes_received] = '\0';
                    double captTime = atof(buffer + 5);
                    SampledValue_t *sv = malloc(20 * sizeof(SampledValue_t));
                    run_any_capture(sv, 20, captTime);
                    send(client_socket, "success", 7, 0);
                }
                break;
            }
            case CAPT_ANY_SV_STOP: {
                stopSnifferThread(0);
                break;
            }
            case CAPT_ANY_SV_STATUS: {
                char res[10];
                sprintf(res, "%d|%.2lf|%d|", snifferThd->running, snifferThd->curTime,snifferThd->num_sv);
                send(client_socket, res, 10, 0);
                break;
            }
            case CAPT_ANY_SV_DATA: {
                if (snifferThd->running){
                    send(client_socket, "error", 5, 0);
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
            case CAPT_ANY_SV_WAVEFORM: {
                char fileName [180];
                buffer[bytes_received] = '\0';
                sprintf(fileName, "%s/captFiles/%s", curDir, buffer + 5);
                FILE *fp = fopen(fileName, "r");
                if (fp == NULL){
                    send(client_socket, "error", 5, 0);
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
                if (analyserThd->running){
                    send(client_socket, "error", 5, 0);
                }else{
                    // Running flag
                    // Events flag
                    // RMS
                    // Phasors
                    // Sym
                    // Unbalance
                    char svId[40];
                    buffer[bytes_received] = '\0';
                    sprintf(svId, "%s", buffer + 5);
                    for (int i=0; i<analyserThd->num_sv;i++){
                        if (strcmp(analyserThd->sv[i].info.svId, svId) == 0){
                            char res[4000];
                            int pos =  snprintf(res, 0, "%d\n", analyserThd->sv[i].process.found);
                            for (int j=0;j<analyserThd->sv[i].info.noChannels;j++){
                                pos += snprintf(res + pos, 0, "%lf|", analyserThd->sv[i].process.rms[j]);
                            }
                            for (int j=0;j<analyserThd->sv[i].info.noChannels;j++){
                                for (int k=0;k<40;k++){
                                    pos += snprintf(res + pos, 0, "%lf-%lf|", cabs(analyserThd->sv[i].process.phasor[j][k]), ctan(analyserThd->sv[i].process.phasor[j][k]));
                                }
                            }
                            send(client_socket, res, 100, 0);
                            break;
                        }
                    }
                    send(client_socket, "error", 5, 0);
                }
                break;
            }
            default:
                send(client_socket, "error", 5, 0);
                break;
        }

    }
    close(client_socket);
    free(clientThread);
    clientThread->running = 1;
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
