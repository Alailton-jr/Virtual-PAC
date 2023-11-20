#!/root/Virtual-PAC/vIED/vEnv/bin/python3

import yaml, sys, socket, numpy as np, signal, psutil
from goPublisher import GoPublisher
from multiprocessing import shared_memory, resource_tracker
from time import perf_counter


def getIface():
    for interface, addrs in psutil.net_if_addrs().items():
        if interface != 'lo':
            return interface

def loadYaml(name) -> dict:
    with open(name, 'r') as file:
        return yaml.safe_load(file)

def cleanUp(signum, frame):
    for shm in sharedMemory:
        resource_tracker.unregister(shm._name, 'shared_memory')
    print("Closing Goose Sender")
    exit(0)

if __name__ == '__main__':
    signal.signal(signal.SIGINT, cleanUp)
    signal.signal(signal.SIGTERM, cleanUp)

    config = loadYaml('IedConfig.yaml')

    if len(sys.argv) != 2:
        print('Usage: python3 goose.py <gooseNum>')
        exit(0)
    else:
        goNum = int(sys.argv[1])

    # goNum = 0

    gooseConf = config['Goose'][goNum]

    x = GoPublisher(
        AppId= gooseConf['appId'],
        macDst= gooseConf['macDst'],
        vLan= gooseConf['vLanId'],
        vLanPriority= gooseConf['vLanPriority'],
    )

    x.asduSetup(
        gocbRef= gooseConf['cbRef'],
        timeAllowedtoLive= gooseConf['maxTime']*2,
        datSet= gooseConf['dataSetName'],
        t= int.to_bytes(0, length= 8, byteorder= 'big'),
        stNum= 0,
        sqNum= 0,
        simulation= False,
        confRev= 1,
        goID= gooseConf['goId'],
        ndsCom= False,
        numDatSetEntries= len(gooseConf['dataSet']),
    )

    raw_socket = socket.socket(socket.AF_PACKET, socket.SOCK_RAW, socket.htons(3)) 
    # Bypass the kernel qdisc layer and push frames directly to the driver
    # raw_socket.setsockopt(263, 20, 1)
    # Enable Tx ring to skip over malformed frames
    raw_socket.setsockopt(263, 14, 1)

    # fanout_type = 2
    # fanout_arg = 3 | (fanout_type << 16)
    # raw_socket.setsockopt(263, 18, fanout_arg)

    interface = getIface()
    raw_socket.bind((interface, 0))

    sharedMemory = []
    data = []

    print(f'Running GOOSE Sender {goNum}!')
    print(f'    Data:')
    for name in gooseConf['dataSet']:
        try:
            shm = shared_memory.SharedMemory(name.replace('$','.'),create=True,size=np.ndarray((1,1),dtype=np.uint8).nbytes)
        except:
            shm = shared_memory.SharedMemory(name.replace('$','.'),create=False,size=np.ndarray((1,1),dtype=np.uint8).nbytes)
        var = np.ndarray((1,1),dtype=np.uint8,buffer=shm.buf)
        sharedMemory.append(shm)
        data.append(var)
        print(f'      {name}')
    tMin = gooseConf['minTime']
    tMax = gooseConf['maxTime']
    tNext = tMin
    lastChange = [bool(y) for y in data]
    stNum = 0
    sqNum = 0
    t0 = perf_counter()
    while True:
        if perf_counter() - t0 > tNext/1000:
            t0 = perf_counter()
            x.changeAsduParam('stNum',stNum)
            x.changeAsduParam('sqNum',sqNum)
            raw_socket.send(x.getFrame(lastChange))
            sqNum += 1
            tNext *= 2
            if tNext >= tMax:
                tNext = tMax
        
        for i in range(len(data)):
            if lastChange[i] != bool(data[i]):
                sqNum = 0
                stNum += 1
                lastChange[i] = bool(data[i])
                tNext = tMin
                print('Data Changed')
