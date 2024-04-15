#!/root/Virtual-PAC/vQualityMeter/vEnv/bin/python3

from yaml import safe_load
import uuid, psutil, os, socket
from time import sleep

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


def send_signal_by_arg(arg:str, signal_number):
    for process in psutil.process_iter(['pid', 'name', 'cmdline']):
        if arg in process.info['cmdline']:
            process_pid = process.info['pid']
        else:
            continue
        try:
            os.kill(process_pid, signal_number)
            print(f"Signal {signal_number} sent to process {process_pid}.")
            break
        except ProcessLookupError:
            print(f"Process {process_pid} not found.")
            return
        except PermissionError:
            print(f"Permission denied to send signal to process {process_pid}.")
            return

def send_signal_to_process(process_name, pid, signal_number):
    while 1:
        if pid is None:
            pid = find_process_id_by_name(process_name)
            if pid is None:
                return
        try:
            os.kill(pid, signal_number)
            print(f"Signal {signal_number} sent to process {pid}.")
            pid = None
            sleep(0.5)
        except ProcessLookupError:
            print(f"Process {pid} not found.")
            return
        except PermissionError:
            print(f"Permission denied to send signal to process {pid}.")
            return

def find_process_id_by_name(process_name):
    import psutil
    for process in psutil.process_iter(['pid', 'name']):
        if process.info['name'] == process_name:
            return process.pid
    return None