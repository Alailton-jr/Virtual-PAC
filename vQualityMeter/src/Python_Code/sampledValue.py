
class QualityEvent_t(object):
    def __init__(self, ) -> None:
        self.topThreshold = 0
        self.bottomThreshold = 0
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

class SampledValue_t(object):
    def __init__(self, smpRate:int, freq:int, svID:str, analyseData:QualityAnalyse_t) -> None:
        self.smpRate = smpRate
        self.freq = freq
        self.analyseData = QualityAnalyse_t()
        self.svID = svID
    def loadFromYaml(self, yamlFile:str) -> bool:
        pass
    
