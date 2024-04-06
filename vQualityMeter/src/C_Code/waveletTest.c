#include <math.h>
#include <stdio.h>
#include <stdlib.h>

#define M_PI 3.14159265358979323846


static const double db2_HI[4] = {
    -0.12940952255126037,
    0.2241438680420134,
    0.8365163037378079,
    0.48296291314453416
};

static const double db2_LO[4] = {
    -0.48296291314453416,
    0.8365163037378079,
    -0.2241438680420134,
    -0.12940952255126037
};

typedef struct{
    double hp[20];
    double lp[20];
    int len;
}waveletFiler_t;

void discrete_wavelet(double* signal, int size, double* cA, double* cD, waveletFiler_t *wave){
    int k;
    int half_filter_len = wave->len / 2;
    for (int i = 0; i < size; i++){
        cA[i] = 0.0;
        cD[i] = 0.0;
        for (int j = -half_filter_len; j < half_filter_len; j++){
            k = i + j;
            if (k >= 0 && k < size-1) {
                cA[i] += signal[k] * wave->lp[j+half_filter_len];
                cD[i] += signal[k] * wave->hp[j+half_filter_len];
            }
        }
    }
}

void savearr(double* arr, double* arr2, int size){
    FILE *fp;
    fp = fopen("wavelet.csv", "w");
    for(int i=0;i<size;i++){
        fprintf(fp, "%f", arr[i]);
        if (i<size-1){
            fprintf(fp, ",");
        }
    }
    fprintf(fp, "\n");
    for(int i=0;i<size;i++){
        fprintf(fp, "%f", arr2[i]);
        if (i<size-1){
            fprintf(fp, ",");
        }
    }
    fclose(fp);
}


int main(){

    double arr[6000];
    double time[6000];
    double cA[6000];
    double cD[6000];

    for(int i=0;i<3000;i++){
        arr[i] = sin(2*M_PI*60.0*i/6000.0);
        arr[i+3000] = sin(3*2*M_PI*60.0*i/6000.0);
    }

    waveletFiler_t myWave;
    myWave.len = 4;
    for(int i=0;i<4;i++){
        myWave.hp[i] = db2_HI[i];
        myWave.lp[i] = db2_LO[i];
    }

    discrete_wavelet(&arr[0], 6000, cA, cD, &myWave);

    savearr(cA, cD, 6000);
    // savearr(cD, 6000);

    return 0;
}