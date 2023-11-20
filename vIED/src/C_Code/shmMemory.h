#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <sys/mman.h>
#include <sys/stat.h>
#include <unistd.h>

#ifndef SHARED_MEMORY_H  // If not defined
#define SHARED_MEMORY_H  // Define it

struct shm_setup_s{
    size_t size;
    const char* name;
    int id;
    void* ptr;
}typedef shm_setup_s;

static shm_setup_s createSharedMemory(const char* shm_name, size_t size) {
    int shm_id = shm_open(shm_name, O_CREAT | O_RDWR, 0666);
    if (shm_id == -1) {
        perror("shm_open");
        shm_setup_s x = {0, NULL, -1, NULL};
        return x;
    }

    // Truncate the shared memory segment to the desired size
    if (ftruncate(shm_id, size) == -1) {
        perror("ftruncate");
        shm_unlink(shm_name); // Clean up if ftruncate fails
        shm_setup_s x = {0, NULL, -1, NULL};
        return x;
    }

    void* shm_ptr = mmap(NULL, size, PROT_READ | PROT_WRITE, MAP_SHARED, shm_id, 0);
    if (shm_ptr == MAP_FAILED) {
        perror("mmap");
        shm_unlink(shm_name); // Clean up if mmap fails
        shm_setup_s x = {0, NULL, -1, NULL};
        return x;
    }

    shm_setup_s x = {size, shm_name, shm_id, shm_ptr};
    return x;
}

static shm_setup_s openSharedMemory(const char* shm_name, size_t size)
{
    int shm_id = shm_open(shm_name, O_RDWR, 0666);
    if (shm_id == -1) {
        perror("shm_open");
        shm_setup_s x = {0, NULL, -1, NULL};
        return x;
    }
    void* shm_ptr = mmap(NULL, size, PROT_READ | PROT_WRITE, MAP_SHARED, shm_id, 0);
    if (shm_ptr == MAP_FAILED) {
        perror("mmap");
        shm_setup_s x = {0, NULL, -1, NULL};
        return x;
    }

    shm_setup_s x = {size, shm_name, shm_id, shm_ptr};
    return x;
}

static void deleteSharedMemory(shm_setup_s* setup)
{
    // Unmap and unlink the shared memory
    if (munmap(setup->ptr, setup->size) == -1) {
        perror("munmap");
        return;
    }
    if (shm_unlink(setup->name) == -1) {
        perror("shm_unlink");
        return;
    }
}

#endif