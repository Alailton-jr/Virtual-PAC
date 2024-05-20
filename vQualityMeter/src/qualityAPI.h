
#ifndef QUALITYAPI_H
#define QUALITYAPI_H

#define PORT 8008
#define BUFFER_SIZE 4096
#define IF_NAME "eth0"

#include "yamlReader.h"
#include "sniffer.h"
#include "analyser.h"

typedef struct clientThread{
    pthread_t thread;
    int clientSocket;
    uint8_t stop;
    uint8_t running;
    struct clientThread *next;
}clientThread_t;

extern clientThread_t* clientThd;
extern snifferThread_t* snifferThd;
extern analyserThread_t* analyserThd;
extern char curDir[128];

int create_socket(const char ifname[], int port);
int wait_for_client(int server_fd);
void *handle_client(void *threadInfo);

void run_predefined_capture(int num_sv, SampledValue_t *sv, double captureTime);
void run_any_capture(SampledValue_t *sv, int max_sv, double captureTime);
void stopAnalyse();
void runAnalyse();
void stopSnifferThread(uint8_t wait);
void stopAnalyserThread(uint8_t wait);


#endif // QUALITYAPI_H