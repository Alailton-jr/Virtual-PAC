#!/root/Virtual-PAC/vIED/vEnv/bin/python3

import socket, time, threading, json, sys, subprocess, signal, yaml, psutil
import numpy as np
from multiprocessing import shared_memory, resource_tracker


sharedMemory = None
# Create shared memory
try:
    sharedMemory = shared_memory.SharedMemory(name="phasor",create=True,size=128)
except:
    sharedMemory = shared_memory.SharedMemory(name="phasor",create=False,size=128)

# print(f' Shared Memory size: {sharedMemory.size}')
values = np.ndarray((16,1),dtype=np.double,buffer=sharedMemory.buf)

def get_ip_address():
    '''
        Get the IP address of the machine.

        Returns:
            str: The IP address.
    '''
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    try:
        sock.connect(("8.8.8.8", 80))
        ip_address = sock.getsockname()[0]
    except socket.error:
        ip_address = '0.0.0.0'
    finally:
        sock.close()
    return ip_address

def yamlData(file) -> bool:
    '''
        Save the IED configuration to a YAML file locally.

        Args:
            file (str): The YAML file.
        Returns:
            bool: True if the file was saved.
    '''
    try:
        yamlData = yaml.safe_load(file)
        with open("IedConfig.yaml", "w") as file:
            yaml.safe_dump(yamlData, file)
        changeIP(yamlData['General Settings']['IpAddress']) # Check if IP address needs to be changed on the VM
        print('Yaml Data: ', "Received and saved YAML file.")
        return True
    except Exception as ex:
        print('Yaml Data: ', ex)
        return False

def loadYaml(name) -> (dict|None):
    '''
        Load a YAML file.

        Args:
            name (str): The name of the file.
        Returns:
            dict: The contents of the file.
    '''
    try:
        with open(name, 'r') as file:
            return file.read()
    except:
        return None

def changeIP(ip:str) -> bool:
    '''
        Change the IP address of the VM.
        
        Args:
            ip (str): The IP address.
        Returns:
            bool: True if the IP address was changed.
    '''
    def getIface() -> (str|None):
        '''
            Get the interface name of the first non-loopback interface.

            Returns:
                str: The name of the interface.
        '''
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
            ip = f'{ip}/16'
            yamlData = yaml.safe_load(file)
            if ip != yamlData['network']['ethernets'][iface]['addresses'][0]:
                yamlData['network']['ethernets'][iface]['addresses'][0] = ip
                with open(netplanPath, 'w') as file:
                    yaml.safe_dump(yamlData, file)
                subprocess.run(['netplan', 'apply'])
                restartIED()
            return True
    except:
        return False

def restartIED():
    '''
        Restart the IED service.
    '''
    subprocess.run(['systemctl', 'restart', 'vIED'])

def ClientHandler(clientSocket):
    '''
        Handle the client connection.
    '''
    print("Server listening on", server_address)
    try:
        while True:
            data = clientSocket.recv(1024*4)
            if data:
                info = json.loads(data)
                if 'entry' not in info:
                    continue
                
                # Measures phasors
                if 'getCurrentValues' == info['entry']:
                    clientSocket.sendall(json.dumps(values.reshape(8,2).tolist()).encode())

                # Update IED Config on server
                elif 'SendIedConfig' == info['entry']:
                    if yamlData(info['data']):
                        clientSocket.sendall("okay".encode())
                        restartIED()
                    else:
                        clientSocket.sendall("error".encode())

                # Send IED Config to Client
                elif 'LoadIedConfig' == info['entry']:
                    if (yaml := loadYaml("IedConfig.yaml")) is not None:
                        clientSocket.sendall(yaml.encode())
                        print('Yaml Data was Sent')
                    else:
                        clientSocket.sendall("error".encode())
                elif 'TestConnect':
                    clientSocket.sendall("okay".encode())
            else:
                print("No more data from", client_address)
                break   
    finally:
        clientSocket.close()

def cleanUp(signum, frame):
    '''
        Clean up the shared memory and exit.
    '''
    resource_tracker.unregister(sharedMemory._name, "shared_memory")
    server_socket.close()
    print("Closing IED API")
    exit(0)

if __name__ == "__main__":
    signal.signal(signal.SIGINT, cleanUp)
    signal.signal(signal.SIGTERM, cleanUp)
    ipAddress = get_ip_address()
    print(f'Starting IED API at: IP {ipAddress}/8080')
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    server_address = (get_ip_address(), 8080)  # Use your desired server address and port
    server_socket.bind(server_address)
    server_socket.listen(5)

    while True:
        client_socket, client_address = server_socket.accept()
        print("Connected to client:", client_address)
        threading.Thread(target=ClientHandler, args=(client_socket,), daemon=True).start()
