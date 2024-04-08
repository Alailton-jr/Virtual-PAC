#include <stdio.h>
#include <sys/mman.h>
#include <fcntl.h>
#include "shmMemory.h"
#include "timers.h"
#include <signal.h>
#include <sched.h>

#define T_SLEEP 1000000

#define SHM_USED 4
shm_setup_s* allShm[SHM_USED];

void cleanup(int signum) {
    printf("PIOC Neutral Leaving...\n");
    for(int i = 0;i<SHM_USED-1;i++)
        close(allShm[i]->id);
    deleteSharedMemory(allShm[SHM_USED-1]);
    exit(0);
}

int main(int argc, char *argv[])
{

    signal(SIGINT, cleanup);
    signal(SIGTERM, cleanup);

    if (argc != 5){
        printf("Usage: ./pioc_n <relay number> <pickup> <td>\n");
        cleanup(1);
    }

    struct sched_param paramS;
    paramS.sched_priority = 80;
    sched_setscheduler(0,SCHED_FIFO, &paramS);

    printf("Running PIOC Neutral with:\n");
    printf("    Relay: %s\n", argv[1]);
    printf("    Pickup: %s\n", argv[2]);
    printf("    td: %s\n", argv[3]);
    printf("    Trip Tag: %s\n", argv[4]);
    

    char *endptr;

    shm_setup_s valuesShm = openSharedMemory("phasor",16 * sizeof(double));
    double* values = (double*) valuesShm.ptr;
    
    char tripName[] = "PIOC_TRIP_0";
    char pickupName[] = "PIOC_PICKUP_0";
    snprintf(tripName,sizeof(tripName) ,"PIOC_TRIP_%d", (int) strtod(argv[1], &endptr));
    snprintf(pickupName,sizeof(pickupName) , "PIOC_PICKUP_%d", (int) strtod(argv[1], &endptr));

    shm_setup_s tripFlagShm = openSharedMemory(tripName, 4 * sizeof(uint8_t));
    shm_setup_s pickupFlagShm = openSharedMemory(pickupName, 4 * sizeof(uint8_t));
    shm_setup_s tripTagShm = createSharedMemory(argv[4], 1 * sizeof(uint8_t));

    uint8_t* tripFlag = (uint8_t*) tripFlagShm.ptr;
    uint8_t* pickupFlag = (uint8_t*)  pickupFlagShm.ptr;
    uint8_t* tripTag = (uint8_t*) tripTagShm.ptr;
    
    allShm[0] = &valuesShm;
    allShm[1] = &tripFlagShm;
    allShm[2] = &pickupFlagShm;
    allShm[3] = &tripTagShm;

    double pickup = (double) strtod(argv[2], &endptr);
    double delay = (double) strtod(argv[3], &endptr);

    double tDelay = 0;
    struct timespec tPickup, t0;

    for (int i = 0; i < 3; i++)
        clock_gettime(CLOCK_MONOTONIC ,&tPickup);
    
    struct period_info tsleep;
    periodic_task_init(&tsleep, T_SLEEP);
    int i = 3;
    while (1){

        pickupFlag[i] = 1;
        if (values[i*2] > pickup){
            clock_gettime(CLOCK_MONOTONIC ,&t0);
            tDelay = (t0.tv_sec - tPickup.tv_sec) + (t0.tv_nsec - tPickup.tv_nsec) / 1e9;
            if (tDelay > delay){
                tripFlag[i] = 1;
            }
        }
        else{
            clock_gettime(CLOCK_MONOTONIC ,&tPickup);
            pickupFlag[i] = 0;
            tripFlag[i] = 0;
        }
        tripTag[0] = tripFlag[i];
        wait_rest_of_period(&tsleep);
    }

    return 0;
}
