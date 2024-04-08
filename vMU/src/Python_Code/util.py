import uuid, psutil
from yaml import safe_load

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

def send_signal_by_name(process_name, signal):
    for process in psutil.process_iter(['pid', 'name']):
        cmd = ' '.join(psutil.Process(process.info['pid']).cmdline())
        if cmd == process_name:
            process_pid = process.info['pid']
            try:
                psutil.Process(process_pid).send_signal(signal)
            except psutil.NoSuchProcess:
                print(f"Process '{process_name}' not found.")
