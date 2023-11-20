#include <stdio.h>
#include <sys/mman.h>
#include <fcntl.h>
#include "shmMemory.h"
#include "timers.h"
#include <signal.h>
#include <math.h>
#include <sched.h>

#define SHM_USED 4
shm_setup_s* allShm[SHM_USED];

void sigterm_handler(int signum) {
    printf("PDIR Phase Leaving...\n");
    close(allShm[0]->id);
    for (int i = 1;i<SHM_USED;i++) deleteSharedMemory(allShm[i]);
    exit(0);
}

int main(int argc, char *argv[])
{

    signal(SIGINT, sigterm_handler);
    signal(SIGTERM, sigterm_handler);

    // Pickup, Ang, Delay, Trip Tag
    /*
        Mho -> Adimitância Y
    */

    // Pickup, rTorq, Trip Tag
    if (argc != 5){
        printf("Usage: ./pdir <level> <pickup> <delay> <trip tag>\n");
        sigterm_handler(1);
    }
    struct sched_param paramS;
    paramS.sched_priority = 80;
    sched_setscheduler(0,SCHED_FIFO, &paramS);
    printf("Running PDIR Phase with:\n");
    printf("Level: %s\n", argv[1]);
    printf("Pickup: %s\n", argv[2]);
    printf("Delay: %s\n", argv[3]);
    printf("Trip Tag: %s\n", argv[4]);
    
    char *endptr;
    
    shm_setup_s valuesShm = openSharedMemory("phasor",16 * sizeof(double));
    double* values = (double*) valuesShm.ptr;

    char tripName[] = "PDIS_TRIP_0";
    char pickupName[] = "PIOC_PICKUP_0";
    snprintf(tripName,sizeof(tripName) ,"PDIS_TRIP_%d", (int) strtod(argv[1], &endptr));
    snprintf(pickupName,sizeof(pickupName) , "PIOC_PICKUP_%d", (int) strtod(argv[1], &endptr));

    shm_setup_s tripFlagShm = createSharedMemory(tripName, 4 * sizeof(uint8_t));
    shm_setup_s pickupFlagShm = createSharedMemory(pickupName, 4 * sizeof(uint8_t));
    shm_setup_s tripTagShm = createSharedMemory(argv[5], 1 * sizeof(uint8_t));

    uint8_t* tripFlag = (uint8_t*) tripFlagShm.ptr;
    uint8_t* pickupFlag = (uint8_t*) pickupFlagShm.ptr;
    uint8_t* tripTag = (uint8_t*) tripTagShm.ptr;

    allShm[0] = &valuesShm;
    allShm[1] = &tripTagShm;
    allShm[2] = &tripFlagShm;
    allShm[3] = &pickupFlagShm;

    double z[6];
    double pickup = (double) strtod(argv[2], &endptr);
    double delay = (double) strtod(argv[3], &endptr);

    double tDelay[3] = {0, 0, 0};
    struct timespec tPickup[3], t0;
    
    struct period_info tsleep;
    periodic_task_init(&tsleep, 1000000);
    while (1) {
        for (int i = 0; i < 3; i++)
        {
            z[i+i] =  values[i+i+4]/values[i+i];
            z[i+i+1] = values[i+i+5] - values[i+i+1];
            
            if (z[i+i]*sin(z[i+i+1]) > pickup){
                clock_gettime(CLOCK_MONOTONIC ,&t0);
                tDelay[i] = (t0.tv_sec - tPickup[i].tv_sec) + (t0.tv_nsec - tPickup[i].tv_nsec) / 1e9;
                if (tDelay[i] > delay) {
                    tripFlag[i] = 1;
                }
            }
            else{
                clock_gettime(CLOCK_MONOTONIC ,&tPickup[i]);
                pickupFlag[i] = 0;
                tripFlag[i] = 0;
            }
        }
        tripTag[0] = tripFlag[0] | tripFlag[1] | tripFlag[2];
        wait_rest_of_period(&tsleep);
    }

    return 0;
}