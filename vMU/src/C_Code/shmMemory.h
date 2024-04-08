#ifndef SHARED_MEMORY_H  // If not defined
#define SHARED_MEMORY_H  // Define it

// Commands to check shm
//  ipcs -m
// sysctl kernel.shmmax
// sysctl kernel.shmall



#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <sys/mman.h>
#include <sys/stat.h>
#include <unistd.h>
#include <string.h>

/*
    * Shared memory setup structure
    * @param size: Size of the shared memory
    * @param name: Name of the shared memory
    * @param id: Id of the shared memory
    * @param ptr: Pointer to the shared memory
*/
struct shm_setup_s{
    size_t size; // Size of the shared memory
    char name[256]; // Name of the shared memory
    int id; // Id of the shared memory
    void* ptr; // Pointer to the shared memory
}typedef shm_setup_s; 

/*
    * Create shared memory
    * @param shm_name: Name of the shared memory
    * @param size: Size of the shared memory
*/
static shm_setup_s createSharedMemory(const char* shm_name, size_t size) {
    int shm_id = shm_open(shm_name, O_CREAT | O_RDWR, 0666);

    if (shm_id == -1) {
        perror("shm_open");
        shm_setup_s x;
        x.size = size;
        strcpy(x.name, shm_name);
        x.id = shm_id;
        x.ptr = NULL;
        return x;
    }

    // Truncate the shared memory segment to the desired size
    if (ftruncate(shm_id, size) == -1) {
        perror("ftruncate");
        shm_unlink(shm_name); // Clean up if ftruncate fails
        shm_setup_s x;
        x.size = size;
        strcpy(x.name, shm_name);
        x.id = shm_id;
        x.ptr = NULL;
        return x;
    }

    void* shm_ptr = mmap(NULL, size, PROT_READ | PROT_WRITE, MAP_SHARED, shm_id, 0);
    if (shm_ptr == MAP_FAILED) {
        perror("mmap");
        shm_unlink(shm_name); // Clean up if mmap fails
        shm_setup_s x;
        x.size = size;
        strcpy(x.name, shm_name);
        x.id = shm_id;
        x.ptr = NULL;
        return x;
    }

    shm_setup_s x;
    x.size = size;
    strcpy(x.name, shm_name);
    x.id = shm_id;
    x.ptr = shm_ptr;
    return x;
}

/*
    * Open shared memory
    * @param shm_name: Name of the shared memory
    * @param size: Size of the shared memory
*/
static shm_setup_s openSharedMemory(const char* shm_name, size_t size)
{
    int shm_id = shm_open(shm_name, O_RDWR, 0666);
    if (shm_id == -1) {
        perror("shm_open");
        shm_setup_s x;
        x.size = size;
        strcpy(x.name, shm_name);
        x.id = shm_id;
        x.ptr = NULL;
        return x;
    }
    void* shm_ptr = mmap(NULL, size, PROT_READ | PROT_WRITE, MAP_SHARED, shm_id, 0);
    if (shm_ptr == MAP_FAILED) {
        perror("mmap");
        shm_setup_s x;
        x.size = size;
        strcpy(x.name, shm_name);
        x.id = shm_id;
        x.ptr = NULL;
        return x;
    }

    shm_setup_s x;
    x.size = size;
    strcpy(x.name, shm_name);
    x.id = shm_id;
    x.ptr = shm_ptr;
    return x;
}

/*
    * Delete shared memory
    * @param setup: Shared memory setup structure
*/
static void deleteSharedMemory(shm_setup_s* setup)
{
    if (munmap(setup->ptr, setup->size) == -1) { // Unmap the shared memory
        perror("munmap");
        return;
    }
    close(setup->id); // Close the shared memory
    if (shm_unlink(setup->name) == -1) { // Delete the shared memory
        perror("shm_unlink");
        return;
    }
}

#endif