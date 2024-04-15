

#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/shm.h>
#include <semaphore.h>
#include <fcntl.h>
#include <signal.h>

#include <stdint.h>
#include <time.h>

typedef struct{
    int32_t *arr;
    int32_t size;
}test_st;

sem_t *semaphore;

void cleanup(int sig){
    sem_close(semaphore);
    sem_unlink("/my_semaphore");
    printf("Leaving");
}


int main(int argc, char *argv[]){


    printf("%lu\n", sizeof(struct timespec));
    
    printf("%lu\n", sizeof(FILE)/sizeof(uint8_t));

    return 0;

    signal(SIGINT, cleanup);
    signal(SIGTERM, cleanup);

    key_t key = 1234;
    int shmid;
    if (argc < 2) return 0;

    if (1){

        semaphore = sem_open("/my_semaphore", O_CREAT, 0666, 1);
        if (semaphore == SEM_FAILED) {
            perror("sem_open");
            exit(1);
        }

        test_st* x;
        if ((shmid = shmget(key, sizeof(test_st), IPC_CREAT | 0666)) < 0) {
            perror("shmget");
            exit(1);
        }
        if ((x = shmat(shmid, NULL, 0)) == (test_st *) -1) {
            perror("shmat");
            exit(1);
        }
        x->size = 10; // Initial size of the dynamic array
        // x->arr = (int *)malloc(x->size * sizeof(int32_t));
        // if (x->arr == NULL) {
        //     perror("malloc");
        //     exit(1);
        // }
        while (1){
            for (int i = 0; i < x->size; i++){
                sem_wait(semaphore);
                x->arr[i] = i;
                sem_post(semaphore);
            }
        }
    }
    
    sem_close(semaphore);
    sem_unlink("/my_semaphore");

    return 0;
}