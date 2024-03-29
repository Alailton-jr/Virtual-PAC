#!/root/Virtual-PAC/vQualityMeter/vEnv/bin/python3
import ctypes, os

path = os.path.dirname(os.path.abspath(__file__))

qualityCLib = ctypes.CDLL(os.path.join(path, r'QualityMeter_lib.so'))

qualityCLib.addSampledValue.argtypes = [ctypes.c_int, ctypes.c_char_p, ctypes.c_int]
qualityCLib.addSampledValue.restype = None

qualityCLib.deleteSampledValue.argtypes = [ctypes.c_int]
qualityCLib.deleteSampledValue.restype = None

qualityCLib.deleteSampledValueMemory.argtypes = []
qualityCLib.deleteSampledValueMemory.restype = None

def addSampledValue(index:int, svName:str, smpRate:int) -> None:
    '''
        Add a Sampled Value to be analyzed by the Quality Meter
    '''
    qualityCLib.addSampledValue(index, svName.encode('utf-8'), smpRate)

def deleteSampledValue(index:int) -> None:
    '''
        Delete a Sampled Value from the Quality Meter
    '''
    qualityCLib.deleteSampledValue(index)

def deleteSampledValueMemory() -> None:
    '''
        Delete all Sampled Values from the Quality Meter
    '''
    qualityCLib.deleteSampledValueMemory()

# print('hi')