
#include "interface.h"
#include <unistd.h>

void init_ncurses() {
    initscr();
    cbreak();
    noecho();
    keypad(stdscr, TRUE);
}

void cleanup_ncurses() {
    endwin();
}

void update_screen(cursesThread_t* cursesThd) {
    clear();
    mvprintw(0, 0, "vQualityMeter");
    mvprintw(1, 0, "Press 'q' to quit");
    mvprintw(2, 0, "Press 's' to start/stop sniffer");
    mvprintw(3, 0, "Press 'a' to start/stop analyser");
    mvprintw(4, 0, "Press 'c' to start/stop client");
    mvprintw(5, 0, "Sniffer: %s", *cursesThd->snifferRunning ? "Running" : "Stopped");
    mvprintw(6, 0, "Analyser: %s", *cursesThd->analyserRunning ? "Running" : "Stopped");
    mvprintw(7, 0, "Client: %s", *cursesThd->clientRunning ? "Running" : "Stopped");
    refresh();
}

void* screen_control(void* arg) {

    cursesThread_t* cursesThd = (cursesThread_t*)arg;

    while(1) {
        update_screen(cursesThd);
        usleep(500000); // Update every 0.5 seconds
    }
    return NULL;
}