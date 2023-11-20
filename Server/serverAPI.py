#!/usr/bin/env python3

import json, subprocess, shlex, socket, threading, yaml, time, shutil, os

if not os.path.exists('/tmp/provision.ign'):
    with open('/tmp/provision.ign','w') as file:
        file.close()

def saveYaml(data,name):
    with open(name, 'w') as file:
        yaml.safe_dump(data, file)
def loadYaml(name):
    with open(name, 'r') as file:
        return yaml.safe_load(file)

def runCommand(command):
    result = subprocess.run(shlex.split(command), capture_output=True, text=True)
    return result.stdout, result.stderr, result.returncode

#region Basic

def getVmList():
    x = {'shut':[], 'running':[]}
    command = 'virsh list --all'
    output, error, returnCode = runCommand(command)
    if returnCode != 0:
        print(error)
        return x
    y = [x for x in  output.split('\n')[2:] if x!= '']
    for vm in y:
        param = [x for x in vm.split(' ') if x != '']
        if param[2] in x:
            x[param[2]].append(param[1])
        else:
            x[param[2]] = [param[1]]
    return x

def shutDownVm(vmName):
    command = f'virsh shutdown {vmName}'
    output, error, returnCode = runCommand(command)
    if returnCode != 0:
        print(error)
    else:
        print(output)

def startVm(vmName):
    command = f'virsh start {vmName}'
    output, error, returnCode = runCommand(command)
    if returnCode != 0:
        print(error)
    else:
        print(output)

def RebootVm(vmName):
    command = f'virsh reboot {vmName}'
    output, error, returnCode = runCommand(command)
    if returnCode != 0:
        print(error)
    else:
        print(output)

def shutDownForceVm(vmName):
    command = f'virsh destroy {vmName}'
    output, error, returnCode = runCommand(command)
    if returnCode != 0:
        print(error)
    else:
        print(output)

def cloneVm(origName, newName):
    x = getVmList()
    flag = False
    for status in x:
        if origName in x[status]:
            flag = True
    if flag == False:
        print("The VM doesn't exist")
        return 
    cloneCommand = f'virt-clone --original {origName} --name {newName} --auto-clone'
    
    output, error, returnCode = runCommand(cloneCommand)
    if returnCode != 0:
        print(error)

def deleteVm(vmName):
    x = getVmList()
    if vmName in x['shut']:
        command = f'virsh undefine --domain {vmName}'
        output, error, returnCode = runCommand(command)
        if returnCode != 0:
            print(error)
        else:
            command = f'rm -rf /var/lib/libvirt/images/{vmName}.qcow2'
            output, error, returnCode = runCommand(command)
            if returnCode != 0:
                print(error)
            else:
                print("VM Deleted")
    else:
        print("You need To shutdown the VM First")

def getVmsUuid():
    try:
        command = ["virsh", "list", "--all", "--name"]
        output = subprocess.check_output(command, text=True)
        vmNames = output.strip().split("\n")

        command = ["virsh", "list", "--all", "--uuid"]
        output = subprocess.check_output(command, text=True)
        uuid = output.strip().split("\n")

        data = {}
        for i in range(len(vmNames)):
            data[vmNames[i]] = uuid[i]
        return data
    except subprocess.CalledProcessError:
        return None
#endregion


#region Server Functions
shmFolderPath = '/root/ShmFolderVms/vmConfig/'
def getVmsConfig():
    if (uuid:= getVmsUuid()) is not None:
        data = {}
        for key, value in uuid.items():
            try:
                data[key] = loadYaml(shmFolderPath+value)
            except: 
                continue
        return data
    return None
#endregion

# vmList = {'shut': ['ied01', 'ied02', 'ied04', 'ied05', 'ied06'], 'running':['ied03', 'mu01', 'mu02', 'mu03', 'mu04', 'mu05', 'mu06']}

def get_ip_address():
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    try:
        sock.connect(("8.8.8.8", 80))
        ip_address = sock.getsockname()[0]
    except socket.error:
        ip_address = '0.0.0.0'
    finally:
        sock.close()
    return ip_address

AUTHORIZED_PASSWORD = "passwd"

VmCreated = True
def CreatingVm(data):
    global VmCreated
    VmCreated = False
    vmType = data.split(';')
    vmName = vmType[0]
    vmType = vmType[1]

    vmUUIDs= getVmsUuid()
    if vmType == 'IED':
        print('Creating IED')
        cloneVm('iedBase', vmName)
        vmUUIDs= getVmsUuid()
        shutil.copy(os.path.join(shmFolderPath, vmUUIDs['iedBase']), os.path.join(shmFolderPath, vmUUIDs[vmName]))
    elif vmType== 'Merging Unit':
        print('Creating MU')
        cloneVm('muBase', vmName)
        vmUUIDs= getVmsUuid()
        shutil.copy(os.path.join(shmFolderPath, vmUUIDs['muBase']), os.path.join(shmFolderPath, vmUUIDs[vmName]))
    VmCreated = True

VmDeleted = True
def DeletingVm(data):
    # Lembrar de remover o arquivo
    global VmDeleted
    VmDeleted = False
    vmType = data.split(';')
    vmName = vmType[0]
    vmType = vmType[1]
    vmUUIDs= getVmsUuid()
    if os.path.exists(os.path.join(shmFolderPath, vmUUIDs[vmName])):
        os.remove(os.path.join(shmFolderPath, vmUUIDs[vmName]))
    if vmType == 'IED':
        print(f'Deleting IED {vmName}')
        deleteVm(vmName)
    elif vmType == 'Merging Unit':
        print(f'Deleting MU {vmName}')
        deleteVm(vmName)
    VmDeleted = True

def handleClient(clientSocket):
    global VmCreated,VmDeleted, vmList
    print("Server listening on", server_address)
    try:
        while True:
            data = clientSocket.recv(1024*4)
            if data:
                info = json.loads(data)
                if 'entry' not in info:
                    clientSocket.sendall("error".encode())
                    continue
                try:
                    match info['entry']:

                        case 'getDeviceList':
                            print('Device List')
                            if (data:= getVmList()) is not None:
                                # data = vmList
                                for key, value in data.items():
                                    if 'iedBase' in value:
                                        value.remove('iedBase')
                                    if 'muBase' in value:
                                        value.remove('muBase')
                                print(data)
                                clientSocket.sendall(json.dumps(data).encode())
                            else:
                                clientSocket.sendall("error".encode())

                        case 'getVmInfo':
                            print(f"VM Infor for: {info['data']}")
                            # data = {'type': 'MU', 'ip': '172.20.129.129/24'}
                            # clientSocket.sendall(json.dumps(data).encode())
                            if (data:= getVmsConfig()) is not None:
                                print(f"VM Infor for: {info['data']}")
                                clientSocket.sendall(json.dumps(data[info['data']]).encode())
                            else:
                                clientSocket.sendall("error".encode())

                        case 'shutdownVm':
                            clientSocket.sendall("okay".encode())
                            print(f"VM Shutdown for: {info['data']}")
                            threading.Thread(target = shutDownVm, args=(info['data'],)).start()

                        case 'startVm':
                            clientSocket.sendall("okay".encode())
                            print(f"VM Start for: {info['data']}")
                            threading.Thread(target = startVm, args=(info['data'],)).start()

                        case 'rebootVm':
                            clientSocket.sendall("okay".encode())
                            print(f"VM Reboot for: {info['data']}")
                            threading.Thread(target = RebootVm, args=(info['data'],)).start()

                        case 'getVmState':
                            print('Vm State')
                            vmName = info['data']
                            res = {'state': 0}
                            vmList = getVmList()
                            if vmName in vmList['shut']:
                                res['state'] = 0
                            elif vmName in vmList['running']:
                                res['state'] = 1
                            clientSocket.sendall(json.dumps(res).encode())

                        case 'createVm':
                            VmCreated = False
                            data = info['data']
                            threading.Thread(target = CreatingVm, args=(data,)).start()
                            clientSocket.sendall("okay".encode())

                        case 'createVmStatus':
                            clientSocket.sendall(json.dumps(VmCreated).encode())

                        case 'deleteVm':
                            VmCreated = False
                            data = info['data']
                            threading.Thread(target = DeletingVm, args=(data,)).start()
                            clientSocket.sendall("okay".encode())

                        case 'deleteVmStatus':
                            clientSocket.sendall(json.dumps(VmDeleted).encode())

                        case 'testConnection':
                            print('Test Connection')
                            clientSocket.sendall("okay".encode())

                        case _:
                            print("Entry Not Found", client_address)
                            clientSocket.sendall("error".encode())
                except Exception as ex:
                    print(ex)
                    clientSocket.sendall("error".encode())
            else:
                print("No more data from", client_address)
                break
    finally:
        clientSocket.close()

if __name__ == "__main__":
    ipAddress = get_ip_address()
    print(f'Starting Server API at: IP {ipAddress}/8082')
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    server_address = (ipAddress, 8082) 
    server_socket.bind(server_address)
    server_socket.listen(5)
    while True:
        client_socket, client_address = server_socket.accept()
        print("Connected to client:", client_address)
        threading.Thread(target=handleClient, args=(client_socket,)).start()

