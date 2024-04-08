#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>

#include "shmMemory.h"
#include "sequenceReplay.h"

int main(){

    shm_setup_s stData = openSharedMemory("sequenceReplay_1", sizeof(sequenceData_t));
    sequenceData_t* data = (sequenceData_t*) stData.ptr;


    shm_setup_s sm = openSharedMemory("sequenceReplay_1_arr", data->arrLength * sizeof(int32_t));
    if (sm.ptr == NULL){
        printf("Error opening shared memory\n");
        return -1;
    }

    int32_t* arr = (int32_t*) sm.ptr;
    for (int de = 0; de < 7200;de++){
        if (arr[de*8] != 100){
            printf("ERRRRRRRRRRRRRRRRRRRRRRRRRRRRRO");
        }
    }

    return 0;
}