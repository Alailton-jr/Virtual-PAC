#!/root/Virtual-PAC/vMU/vEnv/bin/python3

import numpy as np, psutil, signal, os, sys
from Control import SvPublisher
from time import sleep
from multiprocessing import shared_memory
from util import loadYaml, get_mac_address, getIface, send_signal_by_name


# SharedMemory from C-API 
import os, sys
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..'))) # Add the path to the C_Build folder
from Python_C_Build import shm_sequenceReplay

fileFolder = os.path.dirname(os.path.realpath(__file__))
setupFolder = os.path.join(fileFolder, '..', '..', 'continuousSetup.yaml')
replayFile = os.path.join(fileFolder, '..', 'C_Build', 'continuousReplay')
commands = []

onlyUpdate = False

def estimateFrames(sv, netConfig, testConfig):
    pps = int(testConfig['pps'])
    noAsdu = int(netConfig['noAsdu'])
    frames = []
    t = np.linspace(0, 1, pps)
    y = []
    for value in testConfig['values']:
        num = np.sqrt(2)*value['module'] * \
                np.sin(value['frequency'] * 2 * np.pi*t +
                np.radians(value['angle'])
                ) + \
                0.5*np.sqrt(2)*value['module'] * \
                np.sin(3*value['frequency'] * 2 * np.pi*t +
                np.radians(value['angle']))
        
        y.append(num)

    sv.asduSetup(
        svId = netConfig['svId'],
        confRev = netConfig['confRev'],
        smpSynch = netConfig['smpSync'],
    )
    i = 0
    smpCnt = 0
    while i < len(t):
        chanels = []
        for j in range(noAsdu):
            chanels.append([[y[x][i+j], '0'] for x in range(len(y))])
        if smpCnt == pps:
            smpCnt = 0
        else:
            smpCnt += 1*noAsdu
        frames.append(sv.getFrame(chanels,i))
        i += 1*noAsdu
    return frames

def runContinous():
    config = loadYaml(r'continuousSetup.yaml')
    netConfig = loadYaml(r'networkSetup.yaml')['SvNetwork']
    testConfig = config['Test']
    
    sv = SvPublisher(
        AppId = netConfig['AppId'],
        macDst = netConfig['macDst'],
        macSrc = get_mac_address(),
        vLanId = netConfig['vlan']
    )

    return estimateFrames(sv, netConfig, testConfig)

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
        os.kill(replayProcess.pid, signal.SIGTERM)

    paramMemory.unlink()
    framesMemory.unlink()
    stopMemory.unlink()

    print("Closing continuous Setup...")
    exit(0)

def updateData(signum, frame):
    print("Updating continuous Setup...")
    frames = runContinous()
    _frames = []
    for i in range(param[1]):
        for j in range(param[2]):
            _frames.append(frames[i][j])
    buffer[:] = _frames[:]

def prepareFrames(config:dict) -> list[str]:
    memNames = []
    nSV = config['numSV']
    for i in range(nSV):
        netConfig = config['Network'][i]['SvNetwork']

        smpRate = netConfig['smpRate']
        freq = netConfig['frequency']
        n_asdu = netConfig['noAsdu']

        t = np.arange(0, 1/freq, smpRate)
        



if __name__=='__main__':

    print('Running continuous setup!')

    signal.signal(signal.SIGINT, cleanUp)
    signal.signal(signal.SIGTERM, cleanUp)

    if not os.path.exists(setupFolder):
        print(f'No sequencer setup file found at {setupFolder}')

    if len(sys.argv) > 1:
        onlyUpdate = True

    config = loadYaml(setupFolder)
    memNames = prepareFrames(config)

    if not onlyUpdate:
        startReplay(memNames)

    exit(0)


    command = f'src/C_Build/replay {getIface()}'

    replayProcess = None
    frames = runContinous()

    try:
        paramMemory = shared_memory.SharedMemory(name="continousParam",create=False,size=np.ndarray((3,1),dtype=np.uint32).nbytes)
    except:
        paramMemory = shared_memory.SharedMemory(name="continousParam",create=True,size=np.ndarray((3,1),dtype=np.uint32).nbytes)

    param = np.ndarray((3,),dtype=np.uint32, buffer= paramMemory.buf)
    param[0] = 1e9 / len(frames)
    param[1] = len(frames)
    param[2] = len(frames[0])

    try:
        framesMemory = shared_memory.SharedMemory(name="continousFrames",create=False,size=np.ndarray((param[1]*param[2],1),dtype=np.uint8).nbytes)
    except:
        framesMemory = shared_memory.SharedMemory(name="continousFrames",create=True,size=np.ndarray((param[1]*param[2],1),dtype=np.uint8).nbytes)
    buffer = np.ndarray((param[1]*param[2],),dtype=np.uint8, buffer=framesMemory.buf)

    _frames = []
    for i in range(param[1]):
        for j in range(param[2]):
            _frames.append(frames[i][j])
    buffer[:] = _frames[:]

    try:    
        stopMemory = shared_memory.SharedMemory(name="continousStop",create=True,size=np.ndarray((1,1),dtype=np.uint8).nbytes)
    except:
        stopMemory = shared_memory.SharedMemory(name="continousStop",create=False,size=np.ndarray((1,1),dtype=np.uint8).nbytes)
    stopFlag = np.ndarray((1,),dtype=np.uint8, buffer=stopMemory.buf)
    stopFlag[0] = 0

    replayProcess = subprocess.Popen(command, shell= True)

    while stopFlag[0] != 1:
        if stopFlag[0] == 2:
            stopFlag[0] = 0
            frames = runContinous()
            buffer[:] = [x for y in frames for x in y]
        sleep(2)
    cleanUp(0,0)

    

