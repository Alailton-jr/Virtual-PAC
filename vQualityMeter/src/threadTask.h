#ifndef THREAD_TASK_H 
#define THREAD_TASK_H 

#include <pthread.h>
#include <stdint.h>

#define NUM_THREADS 2
#define NUM_TASK 8

/*
    * Task structure
    * @param function: Function to be executed
    * @param arg: Argument of the function
*/
typedef struct {
    void (*function)(void* arg); // Function to be executed
    void* arg; // Argument of the function
} Task;

/*
    * Task queue structure
    * @param tasks: Array of tasks
    * @param front: Index of the front of the queue
    * @param rear: Index of the rear of the queue
    * @param count: Number of tasks in the queue
    * @param mutex: Mutex to protect the queue
    * @param not_empty: Condition variable to indicate that the queue is not empty
    * @param not_full: Condition variable to indicate that the queue is not full
*/
typedef struct {
    int16_t numTasks;
    Task *tasks;
    int32_t front, rear, count;
    pthread_mutex_t mutex;
    pthread_cond_t not_empty;
    pthread_cond_t not_full;
} ThreadPoolTaskQueue;

/*
    * Thread pool structure
    * @param threads: Array of threads
    * @param task_queue: Task queue
*/
typedef struct {
    pthread_t *threads;
    ThreadPoolTaskQueue task_queue;
    uint8_t numThreads;
    uint8_t stop;
    uint8_t running;
} ThreadPool;

/*
    * Initialize the task queue 
    * @param queue: Task queue
*/
void task_queue_init(ThreadPoolTaskQueue* queue, int numTasks);

/*
    * Push a task to the queue
    * @param queue: Task queue
    * @param task: Task to be pushed
*/
void task_queue_push(ThreadPoolTaskQueue* queue, Task task);

/*
    * Pop a task from the queue
    * @param queue: Task queue
*/
Task task_queue_pop(ThreadPoolTaskQueue* queue, uint8_t *stop);

/*
    * Thread worker function
    * @param arg: Thread pool
*/
void* thread_worker(void* arg);

/*
    * Initialize the thread pool
    * @param pool: Thread pool
*/
void thread_pool_init(ThreadPool* pool, int numThreads, int numTasks);

/*
    * Submit a task to the thread pool
    * @param pool: Thread pool
    * @param function: Function to be executed
    * @param arg: Argument of the function
*/
void thread_pool_submit(ThreadPool* pool, void (*function)(void*), void* arg);

/*
    * Destroy the thread pool
    * @param pool: Thread pool
*/
void thread_pool_destroy(ThreadPool* pool);

#endif