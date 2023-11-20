#ifndef TIEMRS_H  // If not defined
#define TIEMRS_H  // Define it

#include <time.h>
#include <stdint.h>

#define CLOCK_REALTIME 0
#define CLOCK_MONOTONIC 1
#define TIMER_ABSTIME 1
#define CLOCK_USED CLOCK_MONOTONIC

struct period_info {
    struct timespec next_period;
    long period_ns;
};

static void inc_period(struct period_info *pinfo) 
{
    pinfo->next_period.tv_nsec += pinfo->period_ns;

    while (pinfo->next_period.tv_nsec >= 1000000000) {
            pinfo->next_period.tv_sec++;
            pinfo->next_period.tv_nsec -= 1000000000;
    }
}

static void periodic_task_init(struct period_info *pinfo, uint64_t interGap)
{ 
    pinfo->period_ns = interGap;
    clock_gettime(CLOCK_USED, &(pinfo->next_period));
    pinfo->next_period.tv_sec += 1;
}

static void wait_rest_of_period(struct period_info *pinfo)
{
    inc_period(pinfo);
    clock_nanosleep(CLOCK_USED, TIMER_ABSTIME, &pinfo->next_period, NULL);
}


#endif