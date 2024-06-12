#include "fft.h"

void fft_plan_destroy(fft_plan_t *plan){

    for (int i = 0; i < plan->N; i++){
        free(plan->reverseBitOrder[i]);
    }
    free(plan->reverseBitOrder);
    free(plan->W);
    free(plan->m_arr);
    free(plan->m_arr_2);
}

fft_plan_t fft_plan_create(int N, complex double* signal){
    fft_plan_t plan;
    plan.N = N;
    plan.signal = signal;

    plan.reverseBitOrder = (int **)malloc(N * sizeof(int *));
    for (int i = 0; i < N; i++){
        plan.reverseBitOrder[i] = (int *)malloc(2 * sizeof(int));
    }
    plan.nBitOrder = 0;
    for (int i = 0; i < N; i++) {
        int j = 0;
        for (int k = 1; k < N; k <<= 1) {
            j = (j << 1) | (i & k ? 1 : 0);
        }
        if (i < j) {
            plan.reverseBitOrder[plan.nBitOrder][0] = i;
            plan.reverseBitOrder[plan.nBitOrder][1] = j;
            plan.nBitOrder++;
        }
    }

    plan.nW = 0;
    for (int m = 2; m <= N; m<<=1) plan.nW++;
    plan.W = (complex double *)malloc(plan.nW * sizeof(complex double));
    plan.m_arr = (int *)malloc(plan.nW * sizeof(int));
    plan.m_arr_2 = (int *)malloc(plan.nW * sizeof(int));
    int i = 0;
    for (int m = 2; m <= N; m<<=1){
        double p = -2 * PI / m;
        plan.W[i] = cos(p) + sin(p) * I;
        plan.m_arr[i] = m;
        plan.m_arr_2[i] = m >> 1;
        i++;
    }
    return plan;
}

void fft_exec(fft_plan_t *plan){
    for (int i = 0; i < plan->nBitOrder; i++){
        plan->t = plan->signal[plan->reverseBitOrder[i][0]];
        plan->signal[plan->reverseBitOrder[i][0]] = plan->signal[plan->reverseBitOrder[i][1]];
        plan->signal[plan->reverseBitOrder[i][1]] = plan->t;
    }

    for (int i = 0; i < plan->nW; i++){
        for (int k = 0; k < plan->N; k += plan->m_arr[i]){
            plan->u = 1;
            for (int j = 0; j < plan->m_arr_2[i]; j++){
                plan->t = plan->u * plan->signal[k + j + plan->m_arr_2[i]];
                plan->signal[k + j + plan->m_arr_2[i]] = plan->signal[k + j] - plan->t;
                plan->signal[k + j] += plan->t;
                plan->u *= plan->W[i];
            }
        }
    }
}



