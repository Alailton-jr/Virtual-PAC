#!/root/Virtual-PAC/vQualityMeter/vEnv/bin/python3

from .pyCFunctions import openSvMemory_c, addSampledValue_c

all = ['openSvMemory', 'getPoint']

def openSvMemory():
    '''
        Open the shared memory for the sampled values and return it's pointer
    '''
    return openSvMemory_c()

def addSampledValue(sv, svIdx):
    '''
        Add a sampled value to the shared memory
    '''
    addSampledValue_c(sv, svIdx)

