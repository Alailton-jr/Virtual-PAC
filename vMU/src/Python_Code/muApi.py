#!/root/Virtual-PAC/vMU/vEnv/bin/python3


import signal, socket, threading, json, yaml, psutil, numpy as np, queue, time, sys, subprocess
from multiprocessing import shared_memory, resource_tracker
import os

#region Controller Commands
try:
    controllerShm = shared_memory.SharedMemory(name='controller', create=False, size=4)
except:
    controllerShm = shared_memory.SharedMemory(name='controller', create=True, size=4)
controller = np.ndarray((1,), dtype=np.uint32, buffer=controllerShm.buf)

workDir = os.path.dirname(os.path.abspath(__file__))
filesDir = os.path.join(workDir, '..', '..')

sys.path.append(workDir)
from util import get_ip_address

NOTHING = 0x00
START_CONTINUOUS = 0x01
STOP_CONTINUOUS = 0x02
UPDATE_CONTINUOUS = 0x03
START_SEQUENCER = 0x04
STOP_SEQUENCER = 0x05
START_TRANSIENT = 0x06
STOP_TRANSIENT = 0x07
RESTART_NETWORK = 0x08
STOP_ALL_TESTS = 0x09
controller[0] = NOTHING
#endregion

server_address = None
controller_pid = None

def main():
    signal.signal(signal.SIGINT, cleanUp)
    signal.signal(signal.SIGTERM, cleanUp)
    ipAddress = get_ip_address()
    print(f'Starting MU API at: IP {ipAddress}/8081')
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    server_address = (get_ip_address(), 8081)  # Use your desired server address and port
    server_socket.bind(server_address)
    server_socket.listen(5)

    while True:
        client_socket, client_address = server_socket.accept()
        print("Connected to client:", client_address)
        threading.Thread(target=ClientHandler, args=(client_socket,), daemon=True).start()

def ClientHandler(clientSocket):
    print("Server listening on", server_address)
    try:
        while True:
            data = clientSocket.recv(1024*4)
            if data:
                info = json.loads(data)
                if 'entry' not in info:
                    clientSocket.sendall("error".encode())
                    continue
                entry = info['entry']

                #region Continuous
                if entry == 'startContinous':
                    startContinuous()
                    clientSocket.sendall("okay".encode())
                elif entry == 'setupContinuous':
                    setupContinuous(info['data'])
                    clientSocket.sendall("okay".encode())
                elif entry == 'stopContinuous':
                    stopContinuous()
                    clientSocket.sendall("okay".encode())
                elif entry == 'updateContinuous':
                    updateContinuous()
                    clientSocket.sendall("okay".encode())
                elif entry == 'continousValues':
                    if (values := getContinousValues()) is not None:
                        clientSocket.sendall(json.dumps(values).encode())
                    else:
                        clientSocket.sendall("error".encode())
                elif entry == 'getContinuousSetup':
                    with open("continuousSetup.yaml", "r") as file:
                        values = yaml.safe_load(file)
                    clientSocket.sendall(json.dumps(values).encode())
                #endregion

                #region Sequencer
                elif entry == 'setupSequencer':
                    if setupSequencer(info['data']):
                        clientSocket.sendall("okay".encode())
                    else:
                        clientSocket.sendall("error".encode())
                elif entry == 'startSequencer':
                    if startSequencer():
                        clientSocket.sendall("okay".encode())
                    else:
                        clientSocket.sendall("error".encode())
                elif entry == 'stopSequencer':
                    if stopSequencer():
                        clientSocket.sendall("okay".encode())
                    else:
                        clientSocket.sendall("error".encode())
                elif entry == 'getSequencerResults':
                    if (values := getSequencerResults()) is not None:
                        clientSocket.sendall(values.encode())
                    else:
                        clientSocket.sendall("error".encode())
                elif entry == 'getSequencerSetup':
                    with open("sequencerSetup.yaml", "r") as file:
                        values = yaml.safe_load(file)
                    clientSocket.sendall(json.dumps(values).encode())
                #endregion

                #region Transient
                elif entry == 'setupTransient':
                    if setupTransient(info['data']):
                        clientSocket.sendall("okay".encode())
                    else:
                        clientSocket.sendall("error".encode())
                elif entry == 'receiveTransientFiles':
                    clientSocket.sendall("okay".encode())
                    if receiveTransientFiles(clientSocket):
                        clientSocket.sendall("okay".encode())
                    else:
                        clientSocket.sendall("error".encode())
                elif entry == 'startTransient':
                    startTransient()
                    clientSocket.sendall("okay".encode())
                elif entry == 'stopTransient':
                    print('Stopping Transient')
                    stopTransient()
                    clientSocket.sendall("okay".encode())
                elif entry == 'getTransientResult':
                    pass

                #endregion

                #region NetWork
                elif entry == 'setupNetwork':
                    if setupNetwork(info['data']):
                        clientSocket.sendall("okay".encode())
                    else:
                        clientSocket.sendall("error".encode())
                elif entry == 'getNetworkSetup':
                    with open("networkSetup.yaml", "r") as file:
                        values = yaml.safe_load(file)
                    clientSocket.sendall(json.dumps(values).encode())
                #endregion
                
                #default
                elif entry == 'TestConnection':
                    clientSocket.sendall("okay".encode())
                else:
                    clientSocket.sendall("error".encode())
    except Exception as ex:
        # stop all testes on main instance
        # _mainInstance.stopTests('all')
        print(ex)
        print("Client disconnected!")
    finally:
        # _mainInstance.stopTests('all')
        clientSocket.close()

#region general

LastCommandComplete = True
def receivedSignal(signalNumber, frame):
    global LastCommandComplete
    LastCommandComplete = True
signal.signal(signal.SIGUSR2, receivedSignal)

command_queue = queue.Queue()
def worker():
    '''
        Worker thread to execute commands.
    '''
    while True:
        cmd = command_queue.get()
        if cmd is None:
            break
        cmd()
        command_queue.task_done()
commandThread = threading.Thread(target=worker, daemon=True)
commandThread.start()

def SendCommand(signal):
    '''
        Send a command to a process.
    '''
    global LastCommandComplete
    while not LastCommandComplete:
        time.sleep(0.2)
        pass
    psutil.Process(controller_pid).send_signal(signal)
    LastCommandComplete = False

def sendSignal(signal:int):
    '''
    Send a signal to a process.

    Args:
        signal (int): The signal to send.
    Returns:
        bool: True if the signal was sent.
    '''
    if controller_pid is None: 
        return False
    try:
        command_queue.put(lambda: SendCommand(signal))
        return True
    except psutil.NoSuchProcess:
        print(f"Process '{controller_pid}' not found.")
        return False

def setupNetwork(file:str):
    '''
        Update the network setup file.

        Args:
            file (str): The yaml file.
        Returns:
            bool: True if the file was updated.
    '''
    try:
        yamlData = yaml.safe_load(file)
        with open("networkSetup.yaml", "w") as file:
            yaml.safe_dump(yamlData, file)
        controller[0] = RESTART_NETWORK
        changeIP(yamlData['General']['IpAddress'])
        return sendSignal(signal.SIGUSR1)
    except Exception as ex:
        print('Yaml Data: ', ex)
        return False
    
def changeIP(ip:str) -> bool:
    def getIface() -> str:
        for d in psutil.net_if_stats().keys():
            if d != 'lo':
                return str(d)
        return None
    iface = getIface()
    if iface is None:
        return False
    try:
        netplanPath = '/etc/netplan/00-installer-config.yaml'
        with open(netplanPath, 'r') as file:
            yamlData = yaml.safe_load(file)
            ip = f'{ip}/16'
            if ip != yamlData['network']['ethernets'][iface]['addresses'][0]:
                yamlData['network']['ethernets'][iface]['addresses'][0] = ip
                with open(netplanPath, 'w') as file:
                    yaml.safe_dump(yamlData, file)
                subprocess.run(['netplan', 'apply'])
                subprocess.run(['systemctl', 'restart', 'vMU'])
            return True
    except:
        return False
#endregion

#region continuous
def setupContinuous(file:str):
    '''
        Update the continuous setup file.

        Args:
            file (str): The yaml file.
        Returns:
            bool: True if the file was updated.
    '''
    try:
        yamlData = yaml.safe_load(file)
        with open(os.path.join(filesDir, "continuousSetup.yaml"), "w") as file:
            yaml.safe_dump(yamlData, file)
        print('Yaml Data: ', "Received and saved YAML file.")
        return True
    except Exception as ex:
        print('Yaml Data: ', ex)
        return False

def startContinuous():
    '''
        Send a command to the Controller to start the continuous Test.

        Returns:
            bool: True if the signal was sent.
    '''
    controller[0] = START_CONTINUOUS
    return sendSignal(signal.SIGUSR1)

def stopContinuous():
    '''
        Send a command to the Controller to stop the continuous Test.

        Returns:
            bool: True if the signal was sent.
    '''
    controller[0] = STOP_CONTINUOUS
    return sendSignal(signal.SIGUSR1)

def updateContinuous() -> bool:
    '''
        Send a command to the Controller to update the values in continuous Test.

        Returns:
            bool: True if the signal was sent.
    '''
    controller[0] = UPDATE_CONTINUOUS
    return sendSignal(signal.SIGUSR1)

def getContinousValues():
    '''
        Get the values from the continuous setup file.

        Returns:
            Setup Values: [[module, angle], ...]
    '''
    try:
        with open("continuousSetup.yaml", "r") as file:
            testConfig = yaml.safe_load(file)['Test']
        value:list[list[float]] = [[x['module'], x['angle']] for x in testConfig['values']]
        return value 
    except:
        return None
#endregion

#region sequencer
def setupSequencer(file:str):
    '''
        Update the sequencer setup file.

        Args:
            file (str): The yaml file.
        Returns:
            bool: True if the file was updated.
    '''
    try:
        yamlData = yaml.safe_load(file)
        with open(os.path.join(filesDir, "sequencerSetup.yaml"), "w") as file:
            yaml.safe_dump(yamlData, file)
        print('Yaml Data: ', "Received and saved YAML file.")
        return True
    except Exception as ex:
        print('Yaml Data: ', ex)
        return False

def startSequencer() -> bool: 
    '''
        Send a command to the Controller to start the sequencer Test.

        Returns:
            bool: True if the signal was sent.
    '''
    global sequencerSharedMemory, sequencerStopSharedMemory, sequencerTime, stopFlag
    try:
        sequencerSharedMemory = shared_memory.SharedMemory(name='sequencerTime', create=False, size=16)
        sequencerTime = np.ndarray((2,1), dtype=np.uint64, buffer=sequencerSharedMemory.buf)
    except:
        sequencerSharedMemory = shared_memory.SharedMemory(name='sequencerTime', create=True, size=16)
        sequencerTime = np.ndarray((2,1), dtype=np.uint64, buffer=sequencerSharedMemory.buf)
    try:
        sequencerStopSharedMemory = shared_memory.SharedMemory(name='stopFlag', create=False, size=1)
        stopFlag = np.ndarray((1,), dtype=np.int8, buffer=sequencerStopSharedMemory.buf)
    except:
        sequencerStopSharedMemory = shared_memory.SharedMemory(name='stopFlag', create=True, size=1)
        stopFlag = np.ndarray((1,), dtype=np.int8, buffer=sequencerStopSharedMemory.buf)
    stopFlag[0] = 0
    controller[0] = START_SEQUENCER
    return sendSignal(signal.SIGUSR1)

def stopSequencer():
    '''
        Send a command to the Controller to stop the sequencer Test.
        Returns:
            bool: True if the signal was sent.
    '''
    controller[0] = STOP_SEQUENCER
    return sendSignal(signal.SIGUSR1)

def getSequencerResults():
    '''
        Get the results from the sequencer Test.
        Returns:
            Results Value: [sequencerTime, stopFlag]
    '''
    global sequencerSharedMemory, sequencerStopSharedMemory, sequencerTime, stopFlag
    try:
        jsonData = [(sequencerTime[0] + sequencerTime[1]/1e9).tolist()[0], stopFlag[0].tolist()]
        return json.dumps(jsonData)
    except Exception as ex:
        print('Yaml Data: ', ex)
        return None

try:
    sequencerSharedMemory = shared_memory.SharedMemory(name='sequencerTime', create=False, size=16)
    sequencerTime = np.ndarray((2,1), dtype=np.uint64, buffer=sequencerSharedMemory.buf)
except:
    sequencerSharedMemory = shared_memory.SharedMemory(name='sequencerTime', create=True, size=16)
    sequencerTime = np.ndarray((2,1), dtype=np.uint64, buffer=sequencerSharedMemory.buf)
try:
    sequencerStopSharedMemory = shared_memory.SharedMemory(name='stopFlag', create=False, size=1)
    stopFlag = np.ndarray((1,), dtype=np.int8, buffer=sequencerStopSharedMemory.buf)
except:
    sequencerStopSharedMemory = shared_memory.SharedMemory(name='stopFlag', create=True, size=1)
    stopFlag = np.ndarray((1,), dtype=np.int8, buffer=sequencerStopSharedMemory.buf)
#endregion


#region Transient

transientFilePath = os.path.join(workDir, 'transientFiles')

def setupTransient(file:str):
    '''
        Update the transient setup file.

        Args:
            file (str): The yaml file.
        Returns:
            bool: True if the file was updated.
    '''
    try:
        yamlData = yaml.safe_load(file)
        with open(os.path.join(filesDir, "transientSetup.yaml"), "w") as file:
            yaml.safe_dump(yamlData, file)
        print('Yaml Data: ', "Received and saved YAML file.")
        return True
    except Exception as ex:
        print('Yaml Data: ', ex)
        return False

def receiveTransientFiles(clientSocket) -> bool:
    try:
        # Receive file name length
        debugBytes = clientSocket.recv(4)
        file_name_length = int.from_bytes(debugBytes, byteorder='little')

        # Receive file name
        file_name = clientSocket.recv(file_name_length).decode('utf-8')

        # Receive file length
        file_length = int.from_bytes(clientSocket.recv(8), byteorder='little')

        # Receive file data
        with open(os.path.join(transientFilePath, file_name), 'wb') as file:
            remaining_bytes = file_length
            while remaining_bytes > 0:
                data = clientSocket.recv(4096)
                file.write(data)
                remaining_bytes -= len(data)
        print(f"Received file: {file_name}")
        return True
    except Exception as ex:
        print('Yaml Data: ', ex)
        return False

def startTransient() -> bool:
    '''
        Send a command to the Controller to start the transient Test.

        Returns:
            bool: True if the signal was sent.
    '''
    controller[0] = START_TRANSIENT
    return sendSignal(signal.SIGUSR1)

def stopTransient() -> bool:
    '''
        Send a command to the Controller to stop the transient Test.

        Returns:
            bool: True if the signal was sent.
    '''
    controller[0] = STOP_TRANSIENT
    return sendSignal(signal.SIGUSR1)

#endregion

def cleanUp(signal1 , signal2):
    #Do not Delete Memory
    resource_tracker.unregister(controllerShm._name, 'shared_memory')
    
    #Delete Memory
    sequencerSharedMemory.unlink()
    sequencerStopSharedMemory.unlink()
    
    print('Closing MU API!')
    exit(0)

if __name__ == '__main__':
    signal.signal(signal.SIGINT, cleanUp)
    signal.signal(signal.SIGTERM, cleanUp)
    if len(sys.argv) >= 2:
        controller_pid = int(sys.argv[1])
    main()

