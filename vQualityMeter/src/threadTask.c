
#include "threadTask.h"
#include <stdlib.h>


void task_queue_init(ThreadPoolTaskQueue* queue, int numTasks) {
    queue->front = 0;
    queue->rear = -1;
    queue->count = 0;
    queue->numTasks = numTasks;
    queue->tasks = (Task*)malloc(numTasks * sizeof(Task));
    pthread_mutex_init(&queue->mutex, NULL);
    pthread_cond_init(&queue->not_empty, NULL);
    pthread_cond_init(&queue->not_full, NULL);
}

void task_queue_push(ThreadPoolTaskQueue* queue, Task task) {
    pthread_mutex_lock(&queue->mutex);
    while (queue->count >= queue->numTasks) {
        pthread_cond_wait(&queue->not_full, &queue->mutex);
    }
    queue->rear++;
    if (queue->rear == queue->numTasks) queue->rear = 0;
    queue->tasks[queue->rear] = task;
    queue->count++;
    pthread_cond_signal(&queue->not_empty);
    pthread_mutex_unlock(&queue->mutex);
}

Task task_queue_pop(ThreadPoolTaskQueue* queue, uint8_t *stop) {
    pthread_mutex_lock(&queue->mutex);
    while (queue->count <= 0) {
        pthread_cond_wait(&queue->not_empty, &queue->mutex);
        if (*stop){
            pthread_mutex_unlock(&queue->mutex);
            return (Task){NULL, NULL};
        }
    }
    if (*stop){
        pthread_mutex_unlock(&queue->mutex);
        return (Task){NULL, NULL};
    }
    Task task = queue->tasks[queue->front];
    queue->front++;
    if (queue->front == queue->numTasks) queue->front = 0;
    queue->count--;
    pthread_cond_signal(&queue->not_full);
    pthread_mutex_unlock(&queue->mutex);
    return task;
}

void* thread_worker(void* arg) {
    ThreadPool* pool = (ThreadPool*)arg;
    while (!pool->stop) {
        Task task = task_queue_pop(&pool->task_queue, &pool->stop);
        if (task.function == NULL) break;
        task.function(task.arg);
    }
    pool->running = 0;
    return NULL;
}

void thread_pool_init(ThreadPool* pool, int numThreads, int numTasks) {
    task_queue_init(&pool->task_queue, numTasks);
    pool->threads = (pthread_t*)malloc(numThreads * sizeof(pthread_t));
    pool->numThreads = numThreads;
    pool->stop = 0;
    pool->running = 1;
    for (int i = 0; i < numThreads; ++i) {
        pthread_create(&pool->threads[i], NULL, thread_worker, pool);
    }
}

void thread_pool_submit(ThreadPool* pool, void (*function)(void*), void* arg) {
    Task task;
    task.function = function;
    task.arg = arg;
    task_queue_push(&pool->task_queue, task);
}

void thread_pool_destroy(ThreadPool* pool) {
    pool->stop = 1;

    for (int i=0; i<pool->task_queue.numTasks;i++){
        pthread_cond_signal(&pool->task_queue.not_empty);
    }
    for (int i = 0; i < pool->numThreads; ++i) {
        pthread_detach(pool->threads[i]);
    }
    pthread_mutex_destroy(&pool->task_queue.mutex);
    free(pool->task_queue.tasks);
    free(pool->threads);
}
