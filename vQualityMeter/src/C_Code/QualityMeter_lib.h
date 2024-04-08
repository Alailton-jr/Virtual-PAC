
#ifndef QUALITY_METER_LIB_H
#define QUALITY_METER_LIB_H

#define FREQUENCY 60

// ThreadTask.h
#include <pthread.h>
#include <stdint.h>

// mySocket.h
#include <sys/types.h>
#include <sys/socket.h>
#include <net/ethernet.h>
#include <linux/if_packet.h> 
#include <linux/net_tstamp.h>
#include <net/if.h>
#include <ifaddrs.h>          
#include <arpa/inet.h>        
#include <linux/sockios.h>
#include <sys/ioctl.h>
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

// shmMemory.h
#include <fcntl.h>
#include <sys/mman.h>
#include <sys/stat.h>

// sniffer.c
typedef struct {
    double real;
    double imag;
} complex;
#include <signal.h>

// sampledValue.h
#define MAX_SAMPLED_VALUES 20
#define NUM_CHANELS 8
#define MAX_HARMONIC 40
#define MAX_BUFFER_EVENT_SIZE 80*20

// analyse.c
#include <math.h>
#include <fftw3.h>
#include <time.h>
#include <libgen.h>

#include "threadTask.h"
#include "mySocket.h"
#include "shmMemory.h"
#include "sampledValue.h"
#include "wavelet.h"


// Debug Semaphore
#include <semaphore.h>
#include <fcntl.h>

#endif // QUALITY_METER_LIB_H