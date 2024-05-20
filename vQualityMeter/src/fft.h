#ifndef FFT_H
#define FFT_H

// #ifndef _COMPLEX_H
//     #define _COMPLEX_H 1
// #endif

#include <complex.h>
#include <stdlib.h>
#include <math.h>
#include <pthread.h>

#define PI 3.14159265358979323846

typedef struct fft_plan{
    int N, nW;
    int **reverseBitOrder;
    int nBitOrder;
    int *m_arr;
    int *m_arr_2;
    complex double *W, t, u;
    complex double *signal;
}fft_plan_t;

void fft_plan_destroy(fft_plan_t *plan);

fft_plan_t fft_plan_create(int N, complex double* signal);

void fft_exec(fft_plan_t *plan);

#endif

