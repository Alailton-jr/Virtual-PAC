#ifndef THREAD_TASK_H  // If not defined
#define THREAD_TASK_H  // Define it

#include <pthread.h>
#include <stdint.h>

#define MAX_THREADS 4
#define MAX_RAW 80
#define NUM_THREADS 4
#define TASK_QUEUE_SIZE 5

typedef struct {
    void (*function)(void* arg);
    void* arg;
} Task;

typedef struct {
    Task tasks[TASK_QUEUE_SIZE];
    int front, rear, count;
    pthread_mutex_t mutex;
    pthread_cond_t not_empty;
    pthread_cond_t not_full;
} ThreadPoolTaskQueue;

typedef struct {
    pthread_t threads[NUM_THREADS];
    ThreadPoolTaskQueue* task_queue;
} ThreadPool;

void task_queue_init(ThreadPoolTaskQueue* queue) {
    queue->front = 0;
    queue->rear = -1;
    queue->count = 0;
    pthread_mutex_init(&queue->mutex, NULL);
    pthread_cond_init(&queue->not_empty, NULL);
    pthread_cond_init(&queue->not_full, NULL);
}

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

Task task_queue_pop(ThreadPoolTaskQueue* queue) {
    pthread_mutex_lock(&queue->mutex);
    while (queue->count <= 0) {
        pthread_cond_wait(&queue->not_empty, &queue->mutex);
    }
    Task task = queue->tasks[queue->front];
    queue->front = (queue->front + 1) % TASK_QUEUE_SIZE;
    queue->count--;
    pthread_cond_signal(&queue->not_full);
    pthread_mutex_unlock(&queue->mutex);
    return task;
}

void* thread_worker(void* arg) {
    ThreadPool* pool = (ThreadPool*)arg;
    while (1) {
        Task task = task_queue_pop(pool->task_queue);
        task.function(task.arg);
    }
    return NULL;
}

void thread_pool_init(ThreadPool* pool) {
    pool->task_queue = (ThreadPoolTaskQueue*)malloc(sizeof(ThreadPoolTaskQueue));
    task_queue_init(pool->task_queue);
    for (int i = 0; i < NUM_THREADS; ++i) {
        pthread_create(&pool->threads[i], NULL, thread_worker, pool);
    }
}

void thread_pool_submit(ThreadPool* pool, void (*function)(void*), void* arg) {
    Task task;
    task.function = function;
    task.arg = arg;
    task_queue_push(pool->task_queue, task);
}


#endif