#!/root/Virtual-PAC/vQualityMeter/vEnv/bin/python3
import os, sys
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..'))) # Add the path to the C_Build folder
from Python_C_Build import shm_sequenceReplay

class SampledValueControl(object):
    def __init__(self) -> None:
        pass

    def openShm(self,):
        pass


class QualityEvent_t(object):
    def __init__(self, ) -> None:
        self.topThreshold = 0
        self.bottomThreshold = 0
        self.minDuration = 0;
        self.maxDuration = 0;
        self.flag = 0

class QualityAnalyse_t(object):
    def __init__(self, ) -> None:
        self.symetrical = [[[]]]
        self.unbalance = []
        self.sag = QualityEvent_t()
        self.swell = QualityEvent_t()
        self.interruption = QualityEvent_t()
        self.overVoltage = QualityEvent_t()
        self.underVoltage = QualityEvent_t()
        self.sustainedInterruption = QualityEvent_t()

class SampledValue_t(object):
    def __init__(self, smpRate:int, freq:int, svID:str, analyseData:QualityAnalyse_t) -> None:
        self.smpRate = smpRate
        self.freq = freq
        self.analyseData = QualityAnalyse_t()
        self.svID = svID
    def loadFromYaml(self, yamlFile:str) -> bool:
        pass
     