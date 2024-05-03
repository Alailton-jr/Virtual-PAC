#!/root/Virtual-PAC/vQualityMeter/vEnv/bin/python3

from .pyCFunctions import openSvMemory_c, addSampledValue_c, getQualityAnalyseData_c, deleteSvShm_c

all = ['openSvMemory', 'addSampledValue', 'getQualityAnalyseData', 'deleteAllSampledValues']

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

def getQualityAnalyseData(svPtr:int, svId:int):
    '''
        Get the quality analyse data from the shared memory
    '''
    return getQualityAnalyseData_c(svPtr, svId)

def deleteSvShm():
    '''
        Delete all sampled values from the shared memory
    '''
    deleteSvShm_c()