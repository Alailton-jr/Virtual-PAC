#!/root/Virtual-PAC/vMU/vEnv/bin/python3

#set this file to his path
from os import path, getcwd

import numpy as np
import uuid, psutil, subprocess
from yaml import safe_load, safe_dump
from scipy.interpolate import interp1d
from Control import SvPublisher
import copy

# SharedMemory from C-API 
import os, sys
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..'))) # Add the path to the C_Build folder
from Python_C_Build import shm_transientReplay


#Folder Path
folderPath = path.join(path.dirname(path.realpath(__file__)), 'transientFiles')
#Transient C Replay path
cReplayPath = os.path.abspath(os.path.join(os.path.dirname(__file__), '..', 'C_Build', 'transientReplay'))

def loadYaml(name:str):
    with open(name, 'r') as file:
        return safe_load(file)
    
def get_mac_address():
    mac = uuid.getnode()
    formatted_mac = ':'.join(('%012X' % mac)[i:i+2] for i in range(0, 12, 2))
    return formatted_mac

def getIface() -> str:
    for d in psutil.net_if_stats().keys():
        if d != 'lo':
            return str(d)
    return None

def test():
    transientFile = {}
    transientFile['GOOSE STOP'] = 0
    transientFile['Number of Files'] = 3
    transientFile['Files'] = ['simulation_01.out', 'simulation_02.out', 'simulation_03.out']
    transientFile['Number of Channels'] = [8, 8, 8]
    transientFile['Channels'] = [
        [[0,1], [1,2], [2,3], [4,4], [5,5], [6,6]],
        [[0,1], [1,2], [2,3], [4,4], [5,5], [6,6]],
        [[0,1], [1,2], [2,3], [4,4], [5,5], [6,6]],
    ]
    transientFile['Network'] = [
        {'General': {
            'IpAddress': '172.20.129.129',
            'Port': 8081
        },
        'GoNetwork': {
            'appId': 4000,
            'confRev': 0,
            'controlRef': 'GOOSE',
            'dataSize': 0,
            'goId': 'GO01',
            'macSrc': '01:0C:CD:01:00:00',
            'vLan': 100
        },
        'SvNetwork': {
            'AppId': 16384,
            'confRev': 1,
            'macDst': '01-0C-CD-04-00-00',
            'macSrc': None,
            'noAsdu': 1,
            'pps': 4800,
            'smpRate': 80,
            'freq': 60,
            'smpSync': 0,
            'svId': 'TRTC',
            'vlan': 256,
            'n_asdu': 2
        }}
    ]
    transientFile['Network'].append(copy.deepcopy(transientFile['Network'][0]))
    transientFile['Network'].append(copy.deepcopy(transientFile['Network'][0]))
    transientFile['Network'][1]['SvNetwork']['svId'] = 'TRTC2'
    transientFile['Network'][2]['SvNetwork']['svId'] = 'TRTC3'
    
    with open ('transientReplaySetup.yaml', 'w') as file:
        safe_dump(transientFile, file)

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
            vLanId = netConfig['vlan']
        )
        sv.asduSetup(
            svId = netConfig['svId'],
            confRev =netConfig['confRev'],
            smpSynch = netConfig['smpSync'],
        )

        data = np.genfromtxt(path.join(folderPath, fileName))
        data = data.T if data.shape[0] > data.shape[1] else data
        n_channels = config['Number of Channels'][i]
        n_asdu = netConfig['n_asdu']
        channels = config['Channels'][i]

        frameBase = bytearray(sv.getFrame([[[10, '0']]* n_channels]*n_asdu, 0))

        time_file = data[0,:]
        time = np.arange(0, time_file[-1], 1/netConfig['pps'])
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
            int(1e9/netConfig['pps']*n_asdu),
            netConfig['pps'],
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

def startReplay(memoryNames:str):
    process = []
    for name in memoryNames:
        command = f'{cReplayPath} {name} {getIface()}'
        process.append(subprocess.Popen(command, shell= True))
    for p in process:
        p.wait()

def main():
    config = loadYaml('transientReplaySetup.yaml')
    memNames = prepareFrames(config)
    startReplay(memNames)


if __name__ == '__main__':
    test()
    main()
    # fileName = 'simulation_01.out'
    # data = np.genfromtxt(path.join(folderPath, fileName))

    







