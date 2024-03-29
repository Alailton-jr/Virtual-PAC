
#ifndef QUALITY_METER_LIB_H
#define QUALITY_METER_LIB_H

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
#include <signal.h>

// sampledValue.h
#define MAX_SAMPLED_VALUES 20

#include "threadTask.h"
#include "mySocket.h"
#include "shmMemory.h"
#include "sampledValue.h"

#endif // QUALITY_METER_LIB_H