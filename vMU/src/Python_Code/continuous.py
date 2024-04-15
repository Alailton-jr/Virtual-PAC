#!/root/Virtual-PAC/vMU/vEnv/bin/python3

import numpy as np, psutil, signal, os, sys, subprocess
from Control import SvPublisher
from time import sleep
from multiprocessing import shared_memory
from util import loadYaml, get_mac_address, getIface, send_signal_to_process

#debug
import matplotlib.pyplot as plt


# SharedMemory from C-API 
import os, sys
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..'))) # Add the path to the C_Build folder
from Python_C_Build import shm_continuousReplay, clearShmMemory

fileFolder = os.path.dirname(os.path.realpath(__file__))
setupFolder = os.path.join(fileFolder, '..', '..', 'continuousSetup.yaml')
replayFile = os.path.join(fileFolder, '..', 'C_Build', 'continuousReplay')
commands = []
process = []

onlyUpdate = False

def updateData(signum, frame):
    global config
    config = loadYaml(setupFolder)
    prepareFrames(config, True)
    

def prepareFrames(config:dict, update = False) -> list[str]:
    memNames = []
    nSV = config['numSV']
    for i in range(nSV):
        netConfig = config['Network'][i]['SvNetwork']

        smpRate = netConfig['smpRate']
        freq = netConfig['frequency']
        n_asdu = netConfig['noAsdu']
        n_channels = 8
        t = np.linspace(0, 1/freq, smpRate)
        data = np.zeros((n_channels,t.shape[0]))
        confValues = config['Test'][i]['values']
        for j in range(n_channels):
            
            data[j,:] = confValues[j]['module'] * np.sqrt(2) * np.sin(2*np.pi*freq*t + np.deg2rad(confValues[j]['angle']))
            
        # base frame
        sv = SvPublisher(
            AppId = netConfig['AppId'],
            macDst = netConfig['macDst'],
            macSrc = get_mac_address(),
            vLanId = netConfig['vLanID'],
            vLanPriority= netConfig['vLanPriority']
        )
        sv.asduSetup(
            svId = netConfig['svId'],
            confRev =netConfig['confRev'],
            smpSynch = netConfig['smpSync'],
        )
        frameBase = bytearray(sv.getFrame([[[10, '0']]* 8]*n_asdu, 0))

        plt.plot(data[0,:])
        plt.savefig(f"test_{i+1}.png")

        arr = data.flatten(order='C').astype(np.int32)

        if not update:
            clearShmMemory(f"continuousReplay_{i+1}")
            
        shm_continuousReplay(
            arr,
            sv.smpCountPos,
            sv.asduLength,
            sv.alldataPos,
            int(1e9/freq/smpRate*n_asdu),
            smpRate,
            freq,
            n_channels,
            n_asdu,
            f"continuousReplay_{i+1}",
            f"continuousReplay_{i+1}_arr",
            f"continuousFrame_{i+1}_frame",
            frameBase
        )
        memNames.append(f"continuousReplay_{i+1}")
    return memNames

def startReplay(memNames):
    process.clear()
    for name in memNames:
        command = f'{replayFile} {name} {getIface()}'
        process.append(subprocess.Popen(command, shell=True))
        commands.append(command)
    sleep(5)
    prepareFrames(config, True)
    for p in process:
        p.wait()

def cleanUp(signum, frame):
    print("Closing continuous Setup...")
    send_signal_to_process("continuousReplay", None, signal.SIGTERM)
    exit(0)

if __name__=='__main__':

    print('Running continuous setup!')

    signal.signal(signal.SIGINT, cleanUp)
    signal.signal(signal.SIGTERM, cleanUp)
    signal.signal(signal.SIGUSR1, updateData)

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

    

