#!/root/Virtual-PAC/vMU/vEnv/bin/python3

#set this file to his path
from os import path, getcwd

import numpy as np
import subprocess, psutil, signal
from scipy.interpolate import interp1d
from Control import SvPublisher
from util import loadYaml, get_mac_address, getIface, send_signal_by_name

# SharedMemory from C-API 
import os, sys
fileFolder = path.dirname(path.realpath(__file__))
sys.path.append(os.path.abspath(os.path.join(fileFolder, '..'))) # Add the path to the C_Build folder
from Python_C_Build import shm_transientReplay
setupFolder = os.path.join(fileFolder, '..', '..', 'transientSetup.yaml')


#Folder Path
folderPath = path.join(path.dirname(path.realpath(__file__)), 'transientFiles')
#Transient C Replay path
cReplayPath = os.path.abspath(os.path.join(os.path.dirname(__file__), '..', 'C_Build', 'transientReplay'))

def prepareFrames(config:dict):
    n_files = config['Number of Files']
    memoryName = []
    for i in range(n_files):
        fileName = config['Files'][i]
        network = config['Network'][i]
        netConfig = network['SvNetwork']

        sv = SvPublisher(
            AppId = netConfig['AppId'],
            macDst = netConfig['macDst'],
            macSrc = get_mac_address(),
            vLanId = netConfig['vLanID'],
            vLanPriority= netConfig['vLanPriority'],
        )
        sv.asduSetup(
            svId = netConfig['svId'],
            confRev =netConfig['confRev'],
            smpSynch = netConfig['smpSync'],
            smpRate= netConfig['smpRate'],
        )

        data = np.genfromtxt(path.join(folderPath, fileName))
        data = data.T if data.shape[0] > data.shape[1] else data
        n_channels = config['Number of Channels'][i]
        n_asdu = netConfig['noAsdu']
        channels = config['Channels'][i]
        pps = netConfig['smpRate'] * netConfig['frequency']
        frameBase = bytearray(sv.getFrame([[[10, '0']]* n_channels]*n_asdu, 0))

        time_file = data[0,:]
        time = np.arange(0, time_file[-1], 1/pps)
        vec = np.zeros((n_channels, len(time)))
        for channel in channels:
            inter = interp1d(time_file, data[channel[1], :], kind='linear')
            vec[channel[0], :] = inter(time)

        arr = vec.flatten(order='F').astype(np.int32)

        shm_transientReplay(
            arr,
            sv.smpCountPos,
            sv.asduLength,
            sv.alldataPos,
            int(1e9/pps*n_asdu),
            pps,
            n_channels,
            n_asdu,
            0,
            f'transientReplay_{i+1}',
            f'transientReplay_Array_{i+1}',
            f'transientReplay_Frame_{i+1}',
            frameBase
        )
        memoryName.append(f'transientReplay_{i+1}')
        # break
    return memoryName

def startReplay(memoryNames:list[str]):
    process = []
    for name in memoryNames:
        command = f'{cReplayPath} {name} {getIface()}'
        commands.append(command)
        process.append(subprocess.Popen(command, shell= True))
    for p in process:
        p.wait()

commands = []

def main():
    if os.path.exists(setupFolder):
        config = loadYaml(setupFolder)
        memNames = prepareFrames(config)
        startReplay(memNames)
    else:
        print(f'No transient setup file found at {setupFolder}')

def cleanUp(signum, frame):
    if len(commands) > 0:
        for command in commands:
            send_signal_by_name(command, psutil.signal.SIGTERM)
    print('Closing transient setup...')
    exit(0)

if __name__ == '__main__':
    signal.signal(signal.SIGINT, cleanUp)
    signal.signal(signal.SIGTERM, cleanUp)
    print('Running transient setup!')
    main()

    







