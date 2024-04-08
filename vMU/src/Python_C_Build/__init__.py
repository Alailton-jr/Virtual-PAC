#!/root/Virtual-PAC/vMU/vEnv/bin/python3

from .pyShmMemory import shm_continuousReplay_c, shm_sequenceReplay_c, shm_transientReplay_c, shm_clearMemory_c
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

def shm_sequenceReplay(arr:ndarray, seqNum:int, smpCountPos:int, asduLength:int, allDataPos:int, interGap:int, smpPerSeq:list[int], smpRate:int, freq:int, n_channels:int, n_asdu:int, structShmName:str, arrShmName:str, frameShmName:str, frame:bytearray):
    return shm_sequenceReplay_c(
        arr, 
        seqNum, 
        smpCountPos, 
        asduLength, 
        allDataPos, 
        interGap, 
        smpPerSeq, 
        smpRate, 
        freq, 
        n_channels, 
        n_asdu, 
        structShmName, 
        arrShmName, 
        frameShmName, 
        frame
    )

def shm_continuousReplay(arr:ndarray, smpCountPos:int, asduLength:int, allDataPos:int, interGap:int, smpRate:int, freq:int, n_channels:int, n_asdu:int, structShmName:str, arrShmName:str, frameShmName:str, frame:bytearray):
    return shm_continuousReplay_c(
        arr, 
        smpCountPos, 
        asduLength, 
        allDataPos, 
        interGap, 
        smpRate, 
        freq, 
        n_channels, 
        n_asdu, 
        structShmName, 
        arrShmName, 
        frameShmName, 
        frame
    )

def clearShmMemory(shmName:str):
    return shm_clearMemory_c(shmName)


