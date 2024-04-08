#ifndef WAVELET_H
#define WAVELET_H

#include <stdint.h>

typedef struct{
    double* input;
    double* cA;
    double* cD;
    uint64_t length;
    double high_pass[40];
    double low_pass[40];
    uint32_t filter_length;
}dwt_plan;

void findWavelet(char* motherWavelet, double* low_pass, double* high_pass, uint32_t *filter_length);

dwt_plan wt_dwt_plan_1d(int size, char* motherWavelet, double* input, double* cA, double* cD){
    dwt_plan plan;
    plan.input = input;
    plan.cA = cA;
    plan.cD = cD;
    plan.length = size;
    findWavelet(motherWavelet, plan.low_pass, plan.high_pass, &plan.filter_length);
    return plan;
}

void findWavelet(char* motherWavelet, double* low_pass, double* high_pass, uint32_t *filter_length){

    if (strcmp(motherWavelet, "db1") == 0){
        *filter_length = 2;
        low_pass[0] = 0.7071067811865476;
        low_pass[1] = 0.7071067811865476;
        high_pass[0] = -0.7071067811865476;
        high_pass[1] = 0.7071067811865476;
    }else if(strcmp(motherWavelet, "db2") == 0){
        *filter_length = 4;
        low_pass[0] = -0.48296291314453416;
        low_pass[1] = 0.8365163037378079;
        low_pass[2] = -0.2241438680420134;
        low_pass[3] = -0.12940952255126037;
        high_pass[0] = -0.12940952255126037;
        high_pass[1] = 0.2241438680420134;
        high_pass[2] = 0.8365163037378079;
        high_pass[3] = 0.48296291314453416;
    }else if(strcmp(motherWavelet, "db3") == 0){
        *filter_length = 6;
        low_pass[0] = 0.0352262918857096;
        low_pass[1] = -0.08544127388202666;
        low_pass[2] = -0.1350110200102546;
        low_pass[3] = 0.4598775021193313;
        low_pass[4] = 0.8068915093110924;
        low_pass[5] = 0.3326705529500826;
        high_pass[0] = -0.3326705529500826;
        high_pass[1] = 0.8068915093110924;
        high_pass[2] = -0.4598775021193313;
        high_pass[3] = -0.1350110200102546;
        high_pass[4] = 0.08544127388202666;
        high_pass[5] = 0.0352262918857096;
    }else if(strcmp(motherWavelet, "db4") == 0){
        *filter_length = 8;
        low_pass[0] = -0.010597401784997278;
        low_pass[1] = 0.032883011666982945;
        low_pass[2] = 0.030841381835986965;
        low_pass[3] = -0.18703481171888114;
        low_pass[4] = -0.02798376941698385;
        low_pass[5] = 0.6308807679295904;
        low_pass[6] = 0.7148465705525415;
        low_pass[7] = 0.23037781330885523;
        high_pass[0] = -0.23037781330885523;
        high_pass[1] = 0.7148465705525415;
        high_pass[2] = -0.6308807679295904;
        high_pass[3] = -0.02798376941698385;
        high_pass[4] = 0.18703481171888114;
        high_pass[5] = 0.030841381835986965;
        high_pass[6] = -0.032883011666982945;
        high_pass[7] = -0.010597401784997278;
    } else if(strcmp(motherWavelet, "db5") == 0){
        *filter_length = 10;
        low_pass[0] = 0.003335725285001549;
        low_pass[1] = -0.012580751999015526;
        low_pass[2] = -0.006241490213011705;
        low_pass[3] = 0.07757149384006515;
        low_pass[4] = -0.03224486958463734;
        low_pass[5] = -0.24229488706619015;
        low_pass[6] = 0.1384281459013203;
        low_pass[7] = 0.7243085284377726;
        low_pass[8] = 0.6038292697974729;
        low_pass[9] = 0.160102397974125;
        high_pass[0] = -0.160102397974125;
        high_pass[1] = 0.6038292697974729;
        high_pass[2] = -0.7243085284377726;
        high_pass[3] = 0.1384281459013203;
        high_pass[4] = 0.24229488706619015;
        high_pass[5] = -0.03224486958463734;
        high_pass[6] = -0.07757149384006515;
        high_pass[7] = -0.006241490213011705;
        high_pass[8] = 0.012580751999015526;
        high_pass[9] = 0.003335725285001549;
    } else if(strcmp(motherWavelet, "db6") == 0){
        *filter_length = 12;
        low_pass[0] = -0.00107730108499558;
        low_pass[1] = 0.004777257511010651;
        low_pass[2] = 0.0005538422009938016;
        low_pass[3] = -0.03158203931748618;
        low_pass[4] = 0.02752286553001629;
        low_pass[5] = 0.09750160558707936;
        low_pass[6] = -0.12976686756724787;
        low_pass[7] = -0.22626469396516913;
        low_pass[8] = 0.3152503517092432;
        low_pass[9] = 0.7511339080215775;
        low_pass[10] = 0.4946238903983854;
        low_pass[11] = 0.11154074335008017;
        high_pass[0] = -0.11154074335008017;
        high_pass[1] = 0.4946238903983854;
        high_pass[2] = -0.7511339080215775;
        high_pass[3] = 0.3152503517092432;
        high_pass[4] = 0.22626469396516913;
        high_pass[5] = -0.12976686756724787;
        high_pass[6] = -0.09750160558707936;
        high_pass[7] = 0.02752286553001629;
        high_pass[8] = 0.03158203931748618;
        high_pass[9] = 0.0005538422009938016;
        high_pass[10] = -0.004777257511010651;
        high_pass[11] = -0.00107730108499558;
    } else if(strcmp(motherWavelet, "db7") == 0){
        *filter_length = 14;
        low_pass[0] = 0.00035371379997452;
        low_pass[1] = -0.0018016407039998328;
        low_pass[2] = 0.000429577972921391;
        low_pass[3] = 0.012550998556013784;
        low_pass[4] = -0.01657454163101562;
        low_pass[5] = -0.03802993693503463;
        low_pass[6] = 0.0806126091510659;
        low_pass[7] = 0.07130921926705004;
        low_pass[8] = -0.22403618499381375;
        low_pass[9] = -0.14390600392910627;
        low_pass[10] = 0.4697822874053586;
        low_pass[11] = 0.7291320908465551;
        low_pass[12] = 0.3965393194819174;
        low_pass[13] = 0.07785205408506236;
        high_pass[0] = -0.07785205408506236;
        high_pass[1] = 0.3965393194819174;
        high_pass[2] = -0.7291320908465551;
        high_pass[3] = 0.4697822874053586;
        high_pass[4] = 0.14390600392910627;
        high_pass[5] = -0.22403618499381375;
        high_pass[6] = -0.07130921926705004;
        high_pass[7] = 0.0806126091510659;
        high_pass[8] = 0.03802993693503463;
        high_pass[9] = -0.01657454163101562;
        high_pass[10] = -0.012550998556013784;
        high_pass[11] = 0.000429577972921391;
        high_pass[12] = 0.0018016407039998328;
        high_pass[13] = 0.00035371379997452;
    } else if(strcmp(motherWavelet, "db8") == 0){
        *filter_length = 16;
        low_pass[0] = -0.00011747678400228192;
        low_pass[1] = 0.0006754494059985568;
        low_pass[2] = -0.0003917403729959771;
        low_pass[3] = -0.00487035299301066;
        low_pass[4] = 0.008746094047015655;
        low_pass[5] = 0.013981027917015516;
        low_pass[6] = -0.04408825393106472;
        low_pass[7] = -0.01736930100170901;
        low_pass[8] = 0.128747426620186;
        low_pass[9] = 0.00047248457399797254;
        low_pass[10] = -0.2840155429624281;
        low_pass[11] = -0.015829105256023893;
        low_pass[12] = 0.5853546836548691;
        low_pass[13] = 0.6756307362973199;
        low_pass[14] = 0.3128715909144659;
        low_pass[15] = 0.05441584224308161;
        high_pass[0] = -0.05441584224308161;
        high_pass[1] = 0.3128715909144659;
        high_pass[2] = -0.6756307362973199;
        high_pass[3] = 0.5853546836548691;
        high_pass[4] = 0.015829105256023893;
        high_pass[5] = -0.2840155429624281;
        high_pass[6] = -0.00047248457399797254;
        high_pass[7] = 0.128747426620186;
        high_pass[8] = 0.01736930100170901;
        high_pass[9] = -0.04408825393106472;
        high_pass[10] = -0.013981027917015516;
        high_pass[11] = 0.008746094047015655;
        high_pass[12] = 0.00487035299301066;
        high_pass[13] = -0.0003917403729959771;
        high_pass[14] = -0.0006754494059985568;
        high_pass[15] = -0.00011747678400228192;
    } else if(strcmp(motherWavelet, "db9") == 0){
        *filter_length = 18;
        low_pass[0] = 3.9347319995026125e-05;
        low_pass[1] = -0.0002519631889981789;
        low_pass[2] = 0.00023038576399541288;
        low_pass[3] = 0.0018476468829611268;
        low_pass[4] = -0.004281503681904723;
        low_pass[5] = -0.004723204757894831;
        low_pass[6] = 0.022361662123515244;
        low_pass[7] = 0.00025094711499193845;
        low_pass[8] = -0.06763282905952399;
        low_pass[9] = 0.0307256814793385;
        low_pass[10] = 0.14854074933849513;
        low_pass[11] = -0.09684078322087904;
        low_pass[12] = -0.29327378327258685;
        low_pass[13] = 0.13319738582498832;
        low_pass[14] = 0.6572880780366389;
        low_pass[15] = 0.6048231236900955;
        low_pass[16] = 0.24383467463766728;
        low_pass[17] = 0.03807794736316728;
        high_pass[0] = -0.03807794736316728;
        high_pass[1] = 0.24383467463766728;
        high_pass[2] = -0.6048231236900955;
        high_pass[3] = 0.6572880780366389;
        high_pass[4] = -0.13319738582498832;
        high_pass[5] = -0.29327378327258685;
        high_pass[6] = 0.09684078322087904;
        high_pass[7] = 0.14854074933849513;
        high_pass[8] = -0.0307256814793385;
        high_pass[9] = -0.06763282905952399;
        high_pass[10] = -0.00025094711499193845;
        high_pass[11] = 0.022361662123515244;
        high_pass[12] = 0.004723204757894831;
        high_pass[13] = -0.004281503681904723;
        high_pass[14] = -0.0018476468829611268;
        high_pass[15] = 0.00023038576399541288;
        high_pass[16] = 0.0002519631889981789;
        high_pass[17] = 3.9347319995026125e-05;
    }
}

void wt_excecute(dwt_plan plan){
    int i, j;
    for(i = 0; i < plan.length; i++){
        plan.cA[i] = 0;
        plan.cD[i] = 0;
        for(j = 0; j < plan.filter_length; j++){
            if(i - j >= 0){
                plan.cA[i] += plan.input[i - j] * plan.low_pass[j];
                plan.cD[i] += plan.input[i - j] * plan.high_pass[j];
            }
        }
    }
}




#endif // WAVELET_H