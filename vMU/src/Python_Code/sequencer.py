#!/root/vMU/vEnv/bin/python3

import subprocess, os, signal, yaml, numpy as np, uuid, psutil
from Control import SvPublisher
from time import sleep
from multiprocessing import shared_memory, resource_tracker


def loadYaml(name:str):
    with open(name, 'r') as file:
        return yaml.safe_load(file)

def get_mac_address():
    mac = uuid.getnode()
    formatted_mac = ':'.join(('%012X' % mac)[i:i+2] for i in range(0, 12, 2))
    return formatted_mac

def estimateSequence(netConfig, testConfig, numPackets):
    pps = int(netConfig['pps'])
    noAsdu = netConfig['noAsdu']
    framesNum = []
    for numSeq in range(len(testConfig)):
        numPackets.append(testConfig[numSeq]['duration']*pps/noAsdu)
        t = np.linspace(0,1,pps)
        y = np.ndarray((len(testConfig[numSeq]['values']),len(t)))
        i = 0
        print()
        for value in testConfig[numSeq]['values']:
            _y = np.sqrt(2)*value['module'] * \
                np.sin(value['frequency'] * 2 * np.pi*t +
                np.radians(value['angle'])
            )
            y[i,:] = _y
            i += 1
        framesNum.append(y)
    frames = []
    smpCnt = 0
    for num in framesNum:
        _frames = []
        i = 0
        while i < len(t):
            chanels = []
            for j in range(noAsdu):
                chanels.append([[num[x,i+j], '0'] for x in range(num.shape[0])])
            if smpCnt == pps:
                smpCnt = 0
            else:
                smpCnt += 1*noAsdu
            _frames.append(sv.getFrame(chanels,i))
            i += 1*noAsdu
        frames.append(_frames)
    return frames

def send_signal_by_name(process_name, signal):
    for process in psutil.process_iter(['pid', 'name']):
        cmd = ' '.join(psutil.Process(process.info['pid']).cmdline())
        if cmd == process_name:
            process_pid = process.info['pid']
            try:
                psutil.Process(process_pid).send_signal(signal)
            except psutil.NoSuchProcess:
                print(f"Process '{process_name}' not found.")


def cleanUp(signum, frame):
    
    if replayProcess is not None:
        send_signal_by_name(command, psutil.signal.SIGTERM)
    
    #Do not Delete
    resource_tracker.unregister(stopMemory._name, "shared_memory")
    resource_tracker.unregister(resultMemory._name, "shared_memory")

    #delete shared memory
    paramMemory.unlink()
    bufSizeMemory.unlink()
    framesMemory.unlink()
    
    print("Closing sequencer Setup...")
    exit(0)

def getIface() -> str:
    for d in psutil.net_if_stats().keys():
        if d != 'lo':
            return str(d)
    return None

def is_service_running(service_name):
    flag = False
    for process in psutil.process_iter(['pid', 'name']):
        cmd = ' '.join(psutil.Process(process.info['pid']).cmdline())
        if cmd == service_name:
            flag = True
    return flag

if __name__=='__main__':

    print('Running sequencer setup!')

    signal.signal(signal.SIGINT, cleanUp)
    signal.signal(signal.SIGTERM, cleanUp)
    
    command = f'src/C_Build/replaySeq {getIface()}'

    config = loadYaml(r'sequencerSetup.yaml')
    netConfig = loadYaml(r'networkSetup.yaml')['SvNetwork']
    print(f'With config:')
    for key in netConfig.items():
        print(f'    {key[0]}: {key[1]}')
    testConfig = config['Test']
    
    sv = SvPublisher(
        AppId = netConfig['AppId'],
        macDst = netConfig['macDst'],
        macSrc = get_mac_address(),
        vLan = netConfig['vlan']
    )
    sv.asduSetup(
        svId = netConfig['svId'],
        confRev =netConfig['confRev'],
        smpSynch = netConfig['smpSync'],
    )
    
    
    numPackets = []
    frames = estimateSequence(netConfig, testConfig, numPackets)

    try:
        paramMemory = shared_memory.SharedMemory(name="sequencerParam",create=False,size=np.ndarray((4,1),dtype=np.uint32).nbytes)
    except:
        paramMemory = shared_memory.SharedMemory(name="sequencerParam",create=True,size=np.ndarray((4,1),dtype=np.uint32).nbytes)
    param = np.ndarray((4,),dtype=np.uint32, buffer= paramMemory.buf)
    param[0] = 1e9 / len(frames[0])
    param[1] = len(frames)
    param[2] = len(frames[0])
    param[3] = len(frames[0][0])

    try:
        bufSizeMemory = shared_memory.SharedMemory(name="sequencerBufSize",create=False,size=np.ndarray((param[1],1),dtype=np.uint32).nbytes)
    except:
        bufSizeMemory = shared_memory.SharedMemory(name="sequencerBufSize",create=True,size=np.ndarray((param[1],1),dtype=np.uint32).nbytes)
    buffSize = np.ndarray((param[1],),dtype=np.uint32, buffer= bufSizeMemory.buf)
    for i in range(len(frames)):
        buffSize[i] = numPackets[i]

    try:
        framesMemory = shared_memory.SharedMemory(name="sequencerFrames",create=False,size=np.ndarray((param[1]*param[2]*param[3],1),dtype=np.uint8).nbytes)
    except:
        framesMemory = shared_memory.SharedMemory(name="sequencerFrames",create=True,size=np.ndarray((param[1]*param[2]*param[3],1),dtype=np.uint8).nbytes)
    buffer = np.ndarray((param[1]*param[2]*param[3],),dtype=np.uint8, buffer=framesMemory.buf)

    _frames = []
    for i in range(param[1]):
        for j in range(param[2]):
            for k in range(param[3]):
                _frames.append(frames[i][j][k])
    buffer[:] = _frames[:]


    try:    
        stopMemory = shared_memory.SharedMemory(name="stopFlag",create=True,size=1)
    except:
        stopMemory = shared_memory.SharedMemory(name="stopFlag",create=False,size=1)
    stopFlag = np.ndarray((1,),dtype=np.uint8, buffer=stopMemory.buf)
    stopFlag[0] = 0

    try:
        resultMemory = shared_memory.SharedMemory(name="sequencerTime",create=False,size=16)
    except:
        resultMemory = shared_memory.SharedMemory(name="sequencerTime",create=True,size=16)
        print('resultMemory created')
    res = np.ndarray((2,1),dtype=np.uint64, buffer=resultMemory.buf)


    replayProcess = subprocess.Popen(command, shell=True)
    sleep(2)
    while is_service_running(command):
        sleep(2)

    cleanUp(0,0)