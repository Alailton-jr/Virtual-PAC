from ctypes import *

# Constants
MAX_SAMPLED_VALUES = 20
NUM_CHANNELS = 8
MAX_HARMONIC = 40
MAX_BUFFER_EVENT_SIZE = 80*20
FREQUENCY = 60

# Define QualityEvent_t struct
class QualityEvent_t(Structure):
    _fields_ = [
        ("topThreshold", c_uint32),
        ("bottomThreshold", c_uint32),
        ("minDuration", c_double),
        ("maxDuration", c_double),
        ("minVal", c_double),
        ("maxVal", c_double),
        ("idx", c_uint32 * NUM_CHANNELS),
        ("bufferIdx", c_uint32),
        ("posCycle", c_uint32),
        ("flag", c_uint8),
        ("t0", c_long * 2),
        ("t1", c_long * 2),
        ("eventName", c_char * 40),#
        ("fileName", c_char * 512),
        ("duration", c_double),
        ("fp", c_uint8 * 216),
    ]

# Define QualityAnalyse_t struct
class QualityAnalyse_t(Structure):
    _fields_ = [
        ("phasor_polar", c_double * NUM_CHANNELS * MAX_HARMONIC * 2),
        ("phasor_rect", c_double * NUM_CHANNELS * MAX_HARMONIC * 2),
        ("symmetrical", c_double * 2 * 3 * 2),
        ("unbalance", c_double * 2),
        ("sag", QualityEvent_t),
        ("swell", QualityEvent_t),
        ("interruption", QualityEvent_t),
        ("overVoltage", QualityEvent_t),
        ("underVoltage", QualityEvent_t),
        ("transient", QualityEvent_t)
    ]

# Define sampledValue_t struct
class sampledValue_t(Structure):
    _fields_ = [
        ("svId", c_uint8 * 128),
        ("smpRate", c_uint32),
        ("freq", c_uint32),
        ("initialized", c_uint8),
        ("numChannels", c_uint8),
        ("snifferArr", POINTER(c_int32) * FREQUENCY * NUM_CHANNELS),
        ("analyseArr", POINTER(c_int32) * FREQUENCY * NUM_CHANNELS),
        ("rms", c_double * NUM_CHANNELS),
        ("analyseData", QualityAnalyse_t),
        ("idxCycle", c_int32),
        ("idxBuffer", c_int32),
        ("idxProcessedBuffer", c_int32),
        ("idxProcessedCycle", c_int32),
        ("cycledCaptured", c_int64)
    ]
