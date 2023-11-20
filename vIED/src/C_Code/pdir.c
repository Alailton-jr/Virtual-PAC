#include <stdio.h>
#include <sys/mman.h>
#include <fcntl.h>
#include "shmMemory.h"
#include "timers.h"
#include <signal.h>
#include <math.h>
#include <sched.h>

#define SHM_USED 2
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

    // Pickup, rTorq, Trip Tag
    if (argc != 5){
        printf("Usage: ./pdir <pickup> <rTorq> <trip Tag>\n");
        sigterm_handler(1);
    }
    struct sched_param paramS;
    paramS.sched_priority = 80;
    sched_setscheduler(0,SCHED_FIFO, &paramS);
    
    printf("Running PDIR Phase with:\n");
    printf("    Pickup: %s\n", argv[1]);
    printf("    rTorq: %s\n", argv[2]);
    printf("    Trip Tag: %s\n", argv[3]);
    
    
    char *endptr;
    
    shm_setup_s valuesShm = openSharedMemory("phasor",16 * sizeof(double));
    double* values = (double*) valuesShm.ptr;

    shm_setup_s tripTagShm = createSharedMemory(argv[4], 4 * sizeof(uint8_t));

    uint8_t* tripTag = (uint8_t*) tripTagShm.ptr;   

    allShm[0] = &valuesShm;
    allShm[1] = &tripTagShm;


    double pickup = (double) strtod(argv[1], &endptr);
    double rTorq = (double) strtod(argv[2], &endptr);
    double vAngle = 0;
    struct period_info tsleep;
    periodic_task_init(&tsleep, 1000000);
    while (1) {
        for (int i = 0; i < 3; i++)
        {
            // 0  1  2  3  4  5  6  7
            // Ia Ib Ic In Va Vb Vc Vn
            // i = 0 (Ia) -> 5 - 6 (Vb - Vc)
            // i = 1 (Ib) -> 6 - 4 (Vc - Va)
            // i = 2 (Ic) -> 4 - 5 (Va - Vb)
            
            if (i == 0) vAngle = values[11] - values[13]; // values[5+5+1] - values[6+6+1] -> Vb - Vc
            else if (i==1) vAngle = values[13] - values[9]; // values[6+6+1] - values[4+4+1] -> Vc - Va
            else if (i==2) vAngle = values[9] - values[11]; // values[4+4+1] - values[5+5+1] -> Va - Vb

            tripTag[i] = fabs(values[i+i] * cos(values[i+i+1] - (rTorq + vAngle))) > pickup;
        }
        wait_rest_of_period(&tsleep);
    }

    return 0;
}