#!/root/Virtual-PAC/vQualityMeter/vEnv/bin/python3
import numpy as np
import psutil, signal, os

# from util import loadYaml


# x = loadYaml('/root/Virtual-PAC/vQualityMeter/src/C_Build/captSV/svData.yaml')


# print(x)

freqs = [50, 60]
smpRates = [80, 240, 256]
min = 1
for freq in freqs:
    for smpRate in smpRates:
        if 1/(freq*smpRate) < min:
            min = 1/(freq*smpRate)
            print(f'Freq: {freq}, SmpRate: {smpRate}, Min: {min}')
        # print(1/(freq*smpRate))
print(min/10)