#!/root/Virtual-PAC/vMU/vEnv/bin/python3

import subprocess, os, signal, numpy as np, psutil
from Control import SvPublisher
from util import loadYaml, get_mac_address, getIface, send_signal_by_name

# SharedMemory from C-API 
import os, sys
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..'))) # Add the path to the C_Build folder
from Python_C_Build import shm_sequenceReplay

fileFolder = os.path.dirname(os.path.realpath(__file__))
setupFolder = os.path.join(fileFolder, '..', '..', 'sequencerSetup.yaml')
replayFile = os.path.join(fileFolder, '..', 'C_Build', 'sequenceReplay')

commands = []

def prepareFrames(config:dict):
    memNames = []
    nSV = config['numSV']
    for i in range(nSV):
        netConfig = config['Network'][i]['SvNetwork']
        testConfig = config['Test'][i]
        numTest = len(testConfig)
        smpRate = netConfig['smpRate']
        freq = netConfig['frequency']
        n_asdu = netConfig['noAsdu']

        # 1 cycle of the signal
        seqData = np.zeros((numTest, 8, smpRate))
        t = np.linspace(0,1/freq, smpRate)
        smpPerSeq = []
        for j in range(numTest):
            for channel in range(8):
                seqData[j,channel,:] = np.sqrt(2) * testConfig[j]['values'][channel]['module'] * \
                    np.sin(2 * np.pi * freq * t + testConfig[j]['values'][channel]['angle'])
                seqData[j,channel,:] = (100*j) + channel
            smpPerSeq.append(int(testConfig[j]['duration'] * smpRate * freq))

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

        arr = seqData.flatten(order='C').astype(np.int32)
        
        shm_sequenceReplay(
            arr,
            numTest,
            sv.smpCountPos, 
            sv.asduLength, 
            sv.alldataPos, 
            int(1e9/smpRate/freq*n_asdu), 
            smpPerSeq, 
            smpRate, 
            freq, 
            8, 
            n_asdu, 
            f'sequenceReplay_{i+1}', 
            f'sequenceReplay_{i+1}_arr', 
            f'sequenceReplay_{i+1}_frame', 
            frameBase
        )
        memNames.append(f'sequenceReplay_{i+1}')
    return memNames

def startReplay(memNames:list[str]):
    process = []
    for name in memNames:
        command = f'{replayFile} {name} {getIface()}'
        process.append(subprocess.Popen(command, shell=True))
        commands.append(command)
    for p in process:
        p.wait()

def cleanUp(signum, frame):
    print('Closing sequencer setup...')
    for command in commands:
        send_signal_by_name(command, psutil.signal.SIGTERM)
    exit(0)

if __name__=='__main__':

    print('Running sequencer setup!')

    signal.signal(signal.SIGINT, cleanUp)
    signal.signal(signal.SIGTERM, cleanUp)

    if not os.path.exists(setupFolder):
        print(f'No sequencer setup file found at {setupFolder}')
    
    config = loadYaml(setupFolder)
    memNames = prepareFrames(config)
    startReplay(memNames)

    exit(0)