#!/root/Virtual-PAC/vMU/vEnv/bin/python3

from .pyShmMemory import shm_transientReplay as shm_transientReplay_c
from numpy import ndarray

def shm_transientReplay(arr:ndarray, smpCountPos:int, asduLength:int, allDataPos:int, interGap:int, maxSmpCount:int, n_channels:int, n_asdu:int, isLoop:int, structShmName:str, arrShmName:str, frameShmName:str, frame:bytearray):
    return shm_transientReplay_c(
        arr, 
        smpCountPos, 
        asduLength, 
        allDataPos, 
        interGap,
        maxSmpCount,
        n_channels,
        n_asdu,
        isLoop, 
        structShmName, 
        arrShmName, 
        frameShmName, 
        frame)