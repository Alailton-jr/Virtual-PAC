#ifndef INTERFACE_H
#define INTERFACE_H

#include <ncurses.h>
#include <pthread.h>
#include <stdint.h>

typedef struct cursesThread{
    pthread_t thread;
    int running;
    uint8_t *snifferRunning;
    uint8_t *analyserRunning;
    uint8_t *clientRunning;
} cursesThread_t;


void init_ncurses();

void cleanup_ncurses();

void update_screen(cursesThread_t* cursesThd);

void* screen_control(void* arg);







#endif