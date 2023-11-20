
#include "shmMemory.h"
#include "timers.h"

int main()
{

    shm_setup_s valuesShm = openSharedMemory("phasor",16 * sizeof(double));
    double* values = (double*) valuesShm.ptr;

    if (values == NULL)
    {
        printf("Error opening shared memory\n");
        return 1;
    }

    for (int i = 0; i < 8; i++)
    {
        values[2*i] = 0;
        values[2*i+1] = 0;
    }

    shm_setup_s tripFlagShm = openSharedMemory("PIOC_TRIP_1", 3 * sizeof(uint8_t));
    uint8_t* tripFlag = (uint8_t*) tripFlagShm.ptr;

    // uint8_t* tripFlag = (uint8_t*) openSharedMemory("PTOC_TRIP_1", 3 * sizeof(uint8_t));

    if (tripFlag == NULL)
    {
        printf("Error opening shared memory\n");
        return 1;
    }
    
    for (int i = 0; i < 30; i++)
    {
        sleep(1);
        struct timespec t0, t1;
        clock_gettime(CLOCK_USED, &t0);
        while (!*tripFlag)
        {
            for (int i = 0; i < 8; i++)
            {
                values[2*i] = 10000;
                values[2*i+1] = 10000;
            }
        }
        clock_gettime(CLOCK_USED, &t1);
        printf("Time: %f\n", (t1.tv_sec - t0.tv_sec) + (t1.tv_nsec - t0.tv_nsec) / 1e9);

        for (int i = 0; i < 8; i++)
        {
            values[2*i] = 0;
            values[2*i+1] = 0;
        }
    }
    

    return 0;
}