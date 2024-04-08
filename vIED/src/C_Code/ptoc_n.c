#include <stdio.h>
#include <sys/mman.h>
#include <fcntl.h>
#include "shmMemory.h"
#include "timers.h"
#include <math.h>
#include <signal.h>
#include <sched.h>

#define SHM_USED 4
shm_setup_s* allShm[SHM_USED];

void cleanup(int signum) {
    printf("PTOC Neutral Leaving...\n");
    for(int i = 0;i<SHM_USED-1;i++)
        close(allShm[i]->id);
    deleteSharedMemory(allShm[SHM_USED-1]);
    exit(0);
}

int main(int argc, char *argv[])
{

    signal(SIGINT, cleanup);
    signal(SIGTERM, cleanup);

    if (argc != 8){
        printf("Usage: ./ptoc_p <relay number> <pickup> <td> <a> <b> <c>\n");
        return 1;
    } 

    struct sched_param paramS;
    paramS.sched_priority = 80;
    sched_setscheduler(0,SCHED_FIFO, &paramS);

    
    printf("Running PTOC Neutral with:\n");
    printf("    Relay: %s\n", argv[1]);
    printf("    Pickup: %s\n", argv[2]);
    printf("    td: %s\n", argv[3]);
    printf("    a: %s\n", argv[4]);
    printf("    b: %s\n", argv[5]);
    printf("    c: %s\n", argv[6]);
    printf("    Trip Tag: %s\n", argv[7]);


    int stop[1] = {0};
    char *endptr;
    
    shm_setup_s valuesShm = openSharedMemory("phasor",16 * sizeof(double));
    double* values = (double*) valuesShm.ptr;
    
    char tripName[] = "PTOC_TRIP_0";
    char pickupName[] = "PTOC_PICKUP_0";
    snprintf(tripName,sizeof(tripName) ,"PTOC_TRIP_%d", (int) strtod(argv[1], &endptr));
    snprintf(pickupName,sizeof(pickupName) , "PTOC_PICKUP_%d", (int) strtod(argv[1], &endptr));

    shm_setup_s tripFlagShm = openSharedMemory(tripName, 4 * sizeof(uint8_t));
    shm_setup_s pickupFlagShm = openSharedMemory(pickupName, 4 * sizeof(uint8_t));
    shm_setup_s tripTagShm = createSharedMemory(argv[7], 1 * sizeof(uint8_t));

    uint8_t* tripFlag = (uint8_t*) tripFlagShm.ptr;
    uint8_t* pickupFlag = (uint8_t*) pickupFlagShm.ptr;
    uint8_t* tripTag = (uint8_t*) tripTagShm.ptr;   

    allShm[0] = &valuesShm;
    allShm[1] = &tripFlagShm;
    allShm[2] = &pickupFlagShm;
    allShm[3] = &tripTagShm;

    double pickup = (double) strtod(argv[2], &endptr);
    double td = (double) strtod(argv[3], &endptr);
    double a = (double) strtod(argv[4], &endptr);
    double b = (double) strtod(argv[5], &endptr);
    double c = (double) strtod(argv[6], &endptr);

    double tDelay[3] = {0, 0, 0};
    struct timespec tPickup[3], t0;

    for (int i = 0; i < 3; i++)
        clock_gettime(CLOCK_MONOTONIC ,&tPickup[i]);
    
    struct period_info tsleep;
    periodic_task_init(&tsleep, 1000000);
    int i = 3;
    while (!*stop)
    {
        pickupFlag[i] = 1;
        if (values[2*i] > pickup){
            clock_gettime(CLOCK_MONOTONIC ,&t0);
            tDelay[i] = (t0.tv_sec - tPickup[i].tv_sec) + (t0.tv_nsec - tPickup[i].tv_nsec) / 1e9;
            if (tDelay[i] > td * ( a + ( (b) / ( pow((values[2*i] / pickup ), c) - 1  ) ) ) ){
                tripFlag[i] = 1;
            }
        }
        else{
            clock_gettime(CLOCK_MONOTONIC ,&tPickup[i]);
            pickupFlag[i] = 0;
            tripFlag[i] = 0;
        }
        tripTag[0] = tripFlag[i];
        wait_rest_of_period(&tsleep);
    }

    return 0;
}
