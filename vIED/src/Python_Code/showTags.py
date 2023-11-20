#!/root/Virtual-PAC/vIED/vEnv/bin/python3


from multiprocessing import shared_memory, resource_tracker
import numpy as np, socket
import multiprocessing.resource_tracker  as rs
import pandas as pd

shm = {}
Tags = {}

# PIOC
shm['PIOC'] = {'Trip':[], 'Pickup':[], 'TripTag':[]}
Tags['PIOC'] = {'Trip':[], 'Pickup':[], 'TripTag':[]}
for i in range(1,4):
    try:
        shm['PIOC']['Trip'].append(shared_memory.SharedMemory(name=f'PIOC_TRIP_{i}', create=False, size=4))
        Tags['PIOC']['Trip'].append(np.ndarray((4,), dtype=np.uint8, buffer=shm['PIOC']['Trip'][i-1].buf))
        shm['PIOC']['Pickup'].append(shared_memory.SharedMemory(name=f'PIOC_PICKUP_{i}', create=False, size=4))
        Tags['PIOC']['Pickup'].append(np.ndarray((4,), dtype=np.uint8, buffer=shm['PIOC']['Pickup'][i-1].buf))
        shm['PIOC']['TripTag'].append(shared_memory.SharedMemory(name=f'PROT.P{i}PIOC.Op.general', create=False, size=1))
        Tags['PIOC']['TripTag'].append(np.ndarray((1,), dtype=np.uint8, buffer=shm['PIOC']['TripTag'][i-1].buf))
    except:
        print(f'PIOC {i} not found')


# PTOC
shm['PTOC'] = {'Trip':[], 'Pickup':[], 'TripTag':[]}
Tags['PTOC'] = {'Trip':[], 'Pickup':[], 'TripTag':[]}
for i in range(1,4):
    try:
        shm['PTOC']['Trip'].append(shared_memory.SharedMemory(name=f'PTOC_TRIP_{i}', create=False, size=4))
        Tags['PTOC']['Trip'].append(np.ndarray((4,), dtype=np.uint8, buffer=shm['PTOC']['Trip'][i-1].buf))
        shm['PTOC']['Pickup'].append(shared_memory.SharedMemory(name=f'PTOC_PICKUP_{i}', create=False, size=4))
        Tags['PTOC']['Pickup'].append(np.ndarray((4,), dtype=np.uint8, buffer=shm['PTOC']['Pickup'][i-1].buf))
        shm['PTOC']['TripTag'].append(shared_memory.SharedMemory(name=f'PROT.P{i}PTOC.Op.general', create=False, size=1))
        Tags['PTOC']['TripTag'].append(np.ndarray((1,), dtype=np.uint8, buffer=shm['PTOC']['TripTag'][i-1].buf))
    except:
        print(f'PTOC {i} not found')

#PTOV
shm['PTOV'] = {'Trip':[], 'Pickup':[], 'TripTag':[]}
Tags['PTOV'] = {'Trip':[], 'Pickup':[], 'TripTag':[]}
for i in range(1,4):
    try:
        shm['PTOV']['Trip'].append(shared_memory.SharedMemory(name=f'PTOV_TRIP_{i}', create=False, size=4))
        Tags['PTOV']['Trip'].append(np.ndarray((4,), dtype=np.uint8, buffer=shm['PTOV']['Trip'][i-1].buf))
        shm['PTOV']['Pickup'].append(shared_memory.SharedMemory(name=f'PTOV_PICKUP_{i}', create=False, size=4))
        Tags['PTOV']['Pickup'].append(np.ndarray((4,), dtype=np.uint8, buffer=shm['PTOV']['Pickup'][i-1].buf))
        shm['PTOV']['TripTag'].append(shared_memory.SharedMemory(name=f'PROT.P{i}PTOV.Op.general', create=False, size=1))
        Tags['PTOV']['TripTag'].append(np.ndarray((1,), dtype=np.uint8, buffer=shm['PTOV']['TripTag'][i-1].buf))
    except:
        print(f'PTOV {i} not found')

#PTUV
shm['PTUV'] = {'Trip':[], 'Pickup':[], 'TripTag':[]}
Tags['PTUV'] = {'Trip':[], 'Pickup':[], 'TripTag':[]}
for i in range(1,4):
    try:
        shm['PTUV']['Trip'].append(shared_memory.SharedMemory(name=f'PTUV_TRIP_{i}', create=False, size=4))
        Tags['PTUV']['Trip'].append(np.ndarray((4,), dtype=np.uint8, buffer=shm['PTUV']['Trip'][i-1].buf))
        shm['PTUV']['Pickup'].append(shared_memory.SharedMemory(name=f'PTUV_PICKUP_{i}', create=False, size=4))
        Tags['PTUV']['Pickup'].append(np.ndarray((4,), dtype=np.uint8, buffer=shm['PTUV']['Pickup'][i-1].buf))
        shm['PTUV']['TripTag'].append(shared_memory.SharedMemory(name=f'PROT.P{i}PTUV.Op.general', create=False, size=1))
        Tags['PTUV']['TripTag'].append(np.ndarray((1,), dtype=np.uint8, buffer=shm['PTUV']['TripTag'][i-1].buf))
    except:
        print(f'PTUV {i} not found')

#PDIR
shm['PDIR'] = {'Trip':[], 'Pickup':[], 'TripTag':[]}
Tags['PDIR'] = {'Trip':[], 'Pickup':[], 'TripTag':[]}
for i in range(1,4):
    try:
        shm['PDIR']['TripTag'].append(shared_memory.SharedMemory(name=f'PROT.P{i}PDIR.Op.general', create=False, size=1))
        Tags['PDIR']['TripTag'].append(np.ndarray((1,), dtype=np.uint8, buffer=shm['PDIR']['TripTag'][i-1].buf))
    except:
        print(f'PDIR {i} not found')

#Phasor
shm['Phasor'] = sharedMemory = shared_memory.SharedMemory(name="phasor",create=False,size=16*8*2)
values = np.ndarray((16,1),dtype=np.double,buffer=shm['Phasor'].buf)
valuesNames = ['Ia', 'Ib', 'Ic', 'In', 'Va', 'Vb', 'Vc', 'Vn']
for i in range(8):
    print(f'{valuesNames[i]}: {values[i+i]}|_{values[i+i+1]}')

def deleteShmList(shmList):
    for item in shmList:
        if isinstance(item, list):
            deleteShmList(item)
        elif isinstance(item, dict):
            deleteShmDict(item)
        elif isinstance(item, shared_memory.SharedMemory):
            shared_memory.resource_tracker.unregister(item._name, "shared_memory")

def deleteShmDict(shmDict):
    for key, value in shmDict.items():
        if isinstance(value, list):
            deleteShmList(value)
        elif isinstance(value, dict):
            deleteShmDict(value)
        elif isinstance(value, shared_memory.SharedMemory):
            shared_memory.resource_tracker.unregister(value._name, "shared_memory")


from flask import Flask, render_template, jsonify  
import numpy as np
import pandas as pd

app = Flask(__name__, template_folder=r'/root/vIED/src/Python_Code/webData')

def getData():
    # Initialize data structures
    tables = []
    data = {'Tag':[], 'Value':[]}
    # PIOC
    for i in range(3):
        try:
            data['Tag'].append(shm['PIOC']['Pickup'][i]._name)
            data['Value'].append(Tags['PIOC']['Pickup'][i].tolist())

            data['Tag'].append(shm['PIOC']['Trip'][i]._name)
            data['Value'].append(Tags['PIOC']['Trip'][i].tolist())
            
            data['Tag'].append(shm['PIOC']['TripTag'][i]._name)
            data['Value'].append(Tags['PIOC']['TripTag'][i].tolist())
        except:
            pass
    tables.append(pd.DataFrame(data))

    data = {'Tag':[], 'Value':[]}
    # PTOC
    for i in range(3):
        try:
            data['Tag'].append(shm['PTOC']['Pickup'][i]._name)
            data['Value'].append(Tags['PTOC']['Pickup'][i].tolist())

            data['Tag'].append(shm['PTOC']['Trip'][i]._name)
            data['Value'].append(Tags['PTOC']['Trip'][i].tolist())
            
            data['Tag'].append(shm['PTOC']['TripTag'][i]._name)
            data['Value'].append(Tags['PTOC']['TripTag'][i].tolist())
        except:
            pass
    tables.append(pd.DataFrame(data))

    data = {'Tag':[], 'Value':[]}
    #PDIR   
    for i in range(3):
        try:
            data['Tag'].append(shm['PDIR']['TripTag'][i]._name)
            data['Value'].append(Tags['PDIR']['TripTag'][i].tolist())
        except:
            pass
    tables.append(pd.DataFrame(data))

    data = {'Tag':[], 'Value':[]}
    #PTOV
    for i in range(3):
        try:
            data['Tag'].append(shm['PTOV']['Pickup'][i]._name)
            data['Value'].append(Tags['PTOV']['Pickup'][i].tolist())

            data['Tag'].append(shm['PTOV']['Trip'][i]._name)
            data['Value'].append(Tags['PTOV']['Trip'][i].tolist())
            
            data['Tag'].append(shm['PTOV']['TripTag'][i]._name)
            data['Value'].append(Tags['PTOV']['TripTag'][i].tolist())
        except:
            pass
    tables.append(pd.DataFrame(data))

    data = {'Tag':[], 'Value':[]}
    #PTUV
    for i in range(3):
        try:
            data['Tag'].append(shm['PTUV']['Pickup'][i]._name)
            data['Value'].append(Tags['PTUV']['Pickup'][i].tolist())

            data['Tag'].append(shm['PTUV']['Trip'][i]._name)
            data['Value'].append(Tags['PTUV']['Trip'][i].tolist())
            
            data['Tag'].append(shm['PTUV']['TripTag'][i]._name)
            data['Value'].append(Tags['PTUV']['TripTag'][i].tolist())
        except:
            pass
    tables.append(pd.DataFrame(data))

    data = {'Tag':[], 'Value':[]}
    # Phasor
    for i in range(8):
        data['Tag'].append(valuesNames[i])
        data['Value'].append(f'{values[i+i].tolist()[0]}|_{values[i+i+1].tolist()[0]}')
    tables.append(pd.DataFrame(data))

    return tables

names = ['PIOC', 'PTOC', 'PDIR', 'PTOV', 'PTUV', 'Phasor']

#
@app.route('/refresh', methods=['POST'])
def refreshPage():
    return render_template(r'table_Refresh.html', dataframes=getData(), name=names)

@app.route('/')
def index():
    return render_template(r'table.html', dataframes=getData(), name=names)

@app.route('/get_data')
def get_data():
    jsonData = [df.to_dict(orient='split', index=True) for df in getData()]
    return jsonify(data=jsonData)

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

if __name__ == '__main__':
    app.run(host = get_ip_address(), port=8079, debug=True)

#unlink memory
deleteShmDict(shm)

