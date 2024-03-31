#ifndef THREAD_TASK_H 
#define THREAD_TASK_H 

#include "QualityMeter_lib.h"

#define MAX_THREADS 4
#define MAX_RAW 80
#define NUM_THREADS 4
#define TASK_QUEUE_SIZE 5

/*
    * Task structure
    * @param function: Function to be executed
    * @param arg: Argument of the function
*/
typedef struct {
    void (*function)(uint8_t* arg1, uint64_t arg2); // Function to be executed
    uint8_t* arg1; // Argument of the function
    uint64_t arg2;
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
    Task tasks[TASK_QUEUE_SIZE];
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
    pthread_t threads[NUM_THREADS];
    ThreadPoolTaskQueue* task_queue;
} ThreadPool;

/*
    * Initialize the task queue 
    * @param queue: Task queue
*/
void task_queue_init(ThreadPoolTaskQueue* queue) {
    queue->front = 0;
    queue->rear = -1;
    queue->count = 0;
    pthread_mutex_init(&queue->mutex, NULL);
    pthread_cond_init(&queue->not_empty, NULL);
    pthread_cond_init(&queue->not_full, NULL);
}

/*
    * Push a task to the queue
    * @param queue: Task queue
    * @param task: Task to be pushed
*/
void task_queue_push(ThreadPoolTaskQueue* queue, Task task) {
    pthread_mutex_lock(&queue->mutex);
    while (queue->count >= TASK_QUEUE_SIZE) {
        pthread_cond_wait(&queue->not_full, &queue->mutex);
    }
    queue->rear = (queue->rear + 1) % TASK_QUEUE_SIZE;
    queue->tasks[queue->rear] = task;
    queue->count++;
    pthread_cond_signal(&queue->not_empty);
    pthread_mutex_unlock(&queue->mutex);
}

/*
    * Pop a task from the queue
    * @param queue: Task queue
*/
Task task_queue_pop(ThreadPoolTaskQueue* queue) {
    pthread_mutex_lock(&queue->mutex);
    while (queue->count <= 0) {
        pthread_cond_wait(&queue->not_empty, &queue->mutex);
    }
    Task task = queue->tasks[queue->front];
    queue->front++;
    if (queue->front == TASK_QUEUE_SIZE) queue->front = 0;
    // queue->front = (queue->front + 1) % TASK_QUEUE_SIZE;
    queue->count--;
    pthread_cond_signal(&queue->not_full);
    pthread_mutex_unlock(&queue->mutex);
    return task;
}

/*
    * Thread worker function
    * @param arg: Thread pool
*/
void* thread_worker(void* arg) {
    ThreadPool* pool = (ThreadPool*)arg;
    while (1) {
        Task task = task_queue_pop(pool->task_queue);
        task.function(task.arg1, task.arg2);
    }
    return NULL;
}

/*
    * Initialize the thread pool
    * @param pool: Thread pool
*/
void thread_pool_init(ThreadPool* pool) {
    pool->task_queue = (ThreadPoolTaskQueue*)malloc(sizeof(ThreadPoolTaskQueue));
    task_queue_init(pool->task_queue);
    for (int i = 0; i < NUM_THREADS; ++i) {
        pthread_create(&pool->threads[i], NULL, thread_worker, pool);
    }
}

/*
    * Submit a task to the thread pool
    * @param pool: Thread pool
    * @param function: Function to be executed
    * @param arg: Argument of the function
*/
void thread_pool_submit(ThreadPool* pool, void (*function)(void*), uint8_t* arg1, uint64_t arg2) {
    Task task;
    task.function = function;
    task.arg1 = arg1;
    task.arg2 = arg2;
    task_queue_push(pool->task_queue, task);
}

#endif