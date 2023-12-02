#ifndef TIEMRS_H  // If not defined
#define TIEMRS_H  // Define it

#include <time.h>
#include <stdint.h>

#define CLOCK_REALTIME 0
#define CLOCK_MONOTONIC 1
#define TIMER_ABSTIME 1
#define CLOCK_USED CLOCK_MONOTONIC

/*
    * This structure is used to keep track of the time
    * @param next_period: Struct that keeps track of the next WakeUp time
    * @param period_ns: Increment of the time in nanoseconds
*/
struct period_info {
    struct timespec next_period;
    long period_ns;
};

/*
    * This function increments the time by the period_ns
    * @param pinfo: Struct that keeps track of the next WakeUp time
*/
static void inc_period(struct period_info *pinfo) {
    pinfo->next_period.tv_nsec += pinfo->period_ns;
    while (pinfo->next_period.tv_nsec >= 1000000000) {
            pinfo->next_period.tv_sec++;
            pinfo->next_period.tv_nsec -= 1000000000;
    }
}

/*
    * This function initializes the time struct
    * @param pinfo: Struct that keeps track of the next WakeUp time
    * @param interGap: Time in nanoseconds between each WakeUp
*/
static void periodic_task_init(struct period_info *pinfo, uint64_t interGap){ 
    pinfo->period_ns = interGap;
    clock_gettime(CLOCK_USED, &(pinfo->next_period));
    pinfo->next_period.tv_sec += 1;
}

/*
    * This function waits for the rest of the period
    * @param pinfo: Struct that keeps track of the next WakeUp time
*/
static void wait_rest_of_period(struct period_info *pinfo){
    inc_period(pinfo);
    clock_nanosleep(CLOCK_USED, TIMER_ABSTIME, &pinfo->next_period, NULL);
}

#endif