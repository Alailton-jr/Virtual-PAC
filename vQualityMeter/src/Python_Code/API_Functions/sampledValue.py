#!/root/Virtual-PAC/vQualityMeter/vEnv/bin/python3

from . import os, sys, ctypes, openSvMemory, addSampledValue, QualityEvent_t, QualityAnalyse_t, sampledValue_t

sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..'))) # Add the path to the C_Build folder


class QualityEvent(object):
    def __init__(self, topThreshold:int, bottomThreshold:int, minDuration:float, maxDuration:float, eventName:str) -> None:
        self.topThreshold = int(topThreshold)
        self.bottomThreshold = int(bottomThreshold)
        self.minDuration = minDuration
        self.maxDuration = maxDuration
        self.flag = 0
        self.eventName = eventName
    
    def getCStruct(self) -> QualityEvent_t:
        return QualityEvent_t(
            topThreshold = self.topThreshold,
            bottomThreshold = self.bottomThreshold,
            minDuration = self.minDuration,
            maxDuration = self.maxDuration,
            eventName = self.eventName.encode('utf-8'),
            minVal = -1,
            maxVal = -1
        )
    
    @staticmethod
    def loadFromCStruct(cStruct:QualityEvent_t):
        newClass = QualityEvent(
            topThreshold = cStruct.topThreshold,
            bottomThreshold = cStruct.bottomThreshold,
            minDuration = cStruct.minDuration,
            maxDuration = cStruct.maxDuration,
        )
        newClass.flag = cStruct.flag
        return newClass

class QualityAnalyse(object):
    def __init__(self, sag:QualityEvent, swell:QualityEvent, interruption:QualityEvent, overVoltage:QualityEvent, underVoltage:QualityEvent, sustainedInterruption:QualityEvent) -> None:
        self.phasor_polar = None
        self.phasor_rect = None
        self.symmetrical = None
        self.unbalance = None
        self.sag = sag
        self.swell = swell
        self.interruption = interruption
        self.overVoltage = overVoltage
        self.underVoltage = underVoltage
        self.sustainedInterruption = sustainedInterruption
    
    def getCStruct(self) -> QualityAnalyse_t:
        sag = self.sag.getCStruct()
        swell = self.swell.getCStruct()
        interruption = self.interruption.getCStruct()
        overVoltage = self.overVoltage.getCStruct()
        underVoltage = self.underVoltage.getCStruct()
        # transient = self.transient.getCStruct()
        return QualityAnalyse_t(
            sag = sag,
            swell = swell,
            interruption = interruption,
            overVoltage = overVoltage,
            underVoltage = underVoltage,
            # transient = transient
        )

    @staticmethod
    def loadFromCStruct(cStruct:QualityAnalyse_t):
        newClass = QualityAnalyse(
            sag = QualityEvent.loadFromCStruct(cStruct.sag),
            swell = QualityEvent.loadFromCStruct(cStruct.swell),
            interruption = QualityEvent.loadFromCStruct(cStruct.interruption),
            overVoltage = QualityEvent.loadFromCStruct(cStruct.overVoltage),
            underVoltage = QualityEvent.loadFromCStruct(cStruct.underVoltage),
            transient = QualityEvent.loadFromCStruct(cStruct.transient)
        )
        newClass.phasor_polar = cStruct.phasor_polar,
        newClass.phasor_rect = cStruct.phasor_rect,
        newClass.symmetrical = cStruct.symmetrical,
        newClass.unbalance = cStruct.unbalance,
        return newClass

class SampledValue(object):
    def __init__(self, svId:str, smpRate:int, freq:int, numChannels:int, idxCycle:int=0, idxBuffer:int=0, idxProcessedBuffer:int=0, idxProcessedCycle:int=0, cycledCaptured:int=0, initialized:int=0, analyseData:QualityAnalyse=None) -> None:
        self.svId = svId
        self.smpRate = smpRate
        self.freq = freq
        self.initialized = initialized
        self.numChannels = numChannels
        self.idxCycle = idxCycle
        self.idxBuffer = idxBuffer
        self.idxProcessedBuffer = idxProcessedBuffer
        self.idxProcessedCycle = idxProcessedCycle
        self.cycledCaptured = cycledCaptured
        self.analyseData = analyseData
        
    def getCStruct(self) -> sampledValue_t:
        svId = (ctypes.c_ubyte * 128)()
        for i, byte in enumerate(self.svId.encode('utf-8')):
            svId[i] = byte
        return sampledValue_t(
            svId = svId,
            smpRate = self.smpRate,
            freq = self.freq,
            initialized = self.initialized,
            numChannels = self.numChannels,
            idxCycle = self.idxCycle,
            idxBuffer = self.idxBuffer,
            idxProcessedBuffer = self.idxProcessedBuffer,
            idxProcessedCycle = self.idxProcessedCycle,
            cycledCaptured = self.cycledCaptured,
            analyseData = self.analyseData.getCStruct()
        )

    @staticmethod
    def loadFromCStruct(cStruct:sampledValue_t):
        return SampledValue(
            svId = ctypes.string_at(cStruct.svId).decode('utf-8'),
            smpRate = cStruct.smpRate,
            freq = cStruct.freq,
            initialized = cStruct.initialized,
            numChannels = cStruct.numChannels,
            idxCycle = cStruct.idxCycle,
            idxBuffer = cStruct.idxBuffer,
            idxProcessedBuffer = cStruct.idxProcessedBuffer,
            idxProcessedCycle = cStruct.idxProcessedCycle,
            cycledCaptured = cStruct.cycledCaptured,
            analyseData = QualityAnalyse.loadFromCStruct(cStruct.analyseData)
        )

class SampledValueControl(object):
    def __init__(self) -> None:
        self.shmPointer = None
        self.shmCapsule = None

    def openShm(self,):
        self.shmCapsule, self.shmPointer = openSvMemory()

    def getSvData(self, svIdx:int) -> SampledValue:
        if self.shmPointer is None:
            raise Exception("Shared memory not opened")
        sv = ctypes.cast(self.shmPointer, ctypes.POINTER(sampledValue_t))
        return SampledValue.loadFromCStruct(sv[svIdx])

    def addSampledValue(self, sv:sampledValue_t, svIdx:int):
        addSampledValue(ctypes.addressof(sv), svIdx)
        pass

if __name__ == '__main__':
    x = SampledValueControl()
    x.openShm()
    y = x.getSvData(0)
    print(y.svId)

    # new SV
    newSv = SampledValue(
        svId='newSv',
        smpRate=1,
        freq=60,
        initialized=1,
        numChannels=8,
        idxCycle=0,
        idxBuffer=0,
        idxProcessedBuffer=0,
        idxProcessedCycle=0,
        cycledCaptured=0,
        analyseData=QualityAnalyse(
            sag=QualityEvent(0, 0, 0, 0),
            swell=QualityEvent(0, 0, 0, 0),
            interruption=QualityEvent(0, 0, 0, 0),
            overVoltage=QualityEvent(0, 0, 0, 0),
            underVoltage=QualityEvent(0, 0, 0, 0),
            transient=QualityEvent(0, 0, 0, 0)
        )
    )
    x.addSampledValue(newSv.getCStruct(), 1)
    y = x.getSvData(1)
    print(y.svId)