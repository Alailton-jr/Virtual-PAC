#include <fftw3.h>
#include <complex.h>
#include <math.h>
#include <time.h>
#include <stdlib.h>
#include <string.h>
#include "fft.h"

#define PI 3.14159265358979323846




// void plot_and_save(double *data, int size, const char *filename) {
//     FILE *gnuplotPipe = popen("gnuplot -persistent", "w");
//     if (gnuplotPipe) {
//         fprintf(gnuplotPipe, "set terminal png\n");
//         fprintf(gnuplotPipe, "set output '%s'\n", filename);
//         fprintf(gnuplotPipe, "plot '-' with lines\n");
//         for (int i = 0; i < size; i++) {
//             fprintf(gnuplotPipe, "%d %f\n", i, data[i]);
//         }
//         fprintf(gnuplotPipe, "e\n");
//         fflush(gnuplotPipe);
//         fprintf(gnuplotPipe, "exit\n");
//         pclose(gnuplotPipe);
//     }
//     // FILE *gnuplot = popen(filename, "w");
//     // if (!gnuplot) {
//     //     perror("popen");
//     //     exit(EXIT_FAILURE);
//     // }
//     // fprintf(gnuplot, "plot '-' u 1:2 t 'Price' w lp\n");
//     // for (int i = 0; i < size; ++i) {
//     //     fprintf(gnuplot, "%d %d\n", i, data);
//     // }
//     // fprintf(gnuplot, "e\n"); // Terminate data input
//     // fflush(gnuplot);

//     // printf("Click Ctrl+d to quit...\n");
//     // getchar();
//     // pclose(gnuplot);
//     // exit(EXIT_SUCCESS);
// }

// void fft_fftw3(double input[64]){

//     fftw_complex *input_fftw = fftw_malloc(sizeof(fftw_complex) * 64);
//     fftw_complex *output_fftw = fftw_malloc(sizeof(fftw_complex) * 64);
//     fftw_plan plan_fftw = fftw_plan_dft_1d(64, input_fftw, output_fftw, FFTW_FORWARD, FFTW_ESTIMATE);

//     struct timespec t0,t1;

//     clock_gettime(CLOCK_MONOTONIC, &t0);
//     for (int i = 0;i < 64;i++){
//         input_fftw[i][0] = input[i];
//         input_fftw[i][1] = 0.0;
//     }
//     clock_gettime(CLOCK_MONOTONIC, &t1);
//     fftw_execute(plan_fftw);

//     printf("Time to fftw: %ld\n", (t1.tv_sec - t0.tv_sec) * 1000000000 + t1.tv_nsec - t0.tv_nsec);

//     double *output = (double *)malloc(64 * sizeof(double));
//     for (int i = 0;i < 64;i++){
//         output[i] = sqrt(output_fftw[i][0] * output_fftw[i][0] + output_fftw[i][1] * output_fftw[i][1]);
//     }
//     plot_and_save(output, 64, "fft_fftw.png");

//     free(output);

//     fftw_destroy_plan(plan_fftw);
//     fftw_free(input_fftw);
//     fftw_free(output_fftw);

// }


// int main(){

//     double input_time[64];
//     double t = 0.0;
//     for (int i = 0;i < 64;i++){
//         input_time[i] = 50*sin(2 * PI * 60 * t);
//         t += (1.0 / 60) * (1.0 / 64);
//     }

//     plot_and_save(input_time, 64, "fft_input.png");

//     // first fftw
//     fft_fftw3(input_time);

//     complex double fft_input[64];
//     for (int i = 0;i < 64;i++){
//         fft_input[i] = input_time[i];
//     }
//     complex double output[64];
//     fft_plan_t plan = fft_plan_create(64, fft_input);

//     // sleep(1);


//     struct timespec t0,t1;
//     clock_gettime(CLOCK_MONOTONIC, &t0);
//     // normalfft(fft_input, 64);
//     fft_exec(&plan);
//     clock_gettime(CLOCK_MONOTONIC, &t1);
//     printf("Time to normal fft: %ld ns\n", (t1.tv_sec - t0.tv_sec) * 1000000000 + t1.tv_nsec - t0.tv_nsec);

//     double _output[64];
//     for (int i = 0;i < 64;i++){
//         _output[i] = cabs(fft_input[i]);
//     }
//     plot_and_save(_output, 64, "fft_struct.png");
//     fft_plan_destroy(&plan);

//     return 0;
// }