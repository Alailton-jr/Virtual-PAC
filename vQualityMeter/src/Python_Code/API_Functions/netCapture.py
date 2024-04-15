
from . import getIface, subprocess, signal, send_signal_to_process, find_process_id_by_name, os, fileFolder, shutil

netCaptDir = os.path.join(fileFolder, '..', '..', 'C_Build')
netCaptExec = os.path.join(netCaptDir, 'netCapture')
resultDir = os.path.join(netCaptDir, 'captSV')
resultFile = os.path.join(resultDir, 'svData.yaml')


def runNetworkCapture(duration:float):
    """
        Run the sniffer to capture packets
    """
    if not os.path.exists(resultDir):
        os.makedirs(resultDir)
    else:
        shutil.rmtree(resultDir)
        os.makedirs(resultDir)
    command = f'{netCaptExec} {getIface()} {duration}'
    process = subprocess.Popen(command, shell=True)
    return process

def stopNetworkCapture():
    """
        Stop a process
    """
    send_signal_to_process("netCapture", None, signal.SIGTERM)

def getNetCaptureStatus():
    """
        Get the status of the network capture
    """
    return True if find_process_id_by_name("netCapture") else False

def getNetCaptureResults():
    """
        Get the results of the network capture
    """
    if os.path.exists(resultFile):
        with open(resultFile, 'rb') as file:
            return file.read()
    else:
        return None

def getNetCaptureWaveForm(svId:str):
    """
        Get the waveform of the network capture
    """
    for _file in os.listdir(resultDir):
        if svId in _file:
            with open(os.path.join(resultDir, _file), 'rb') as file:
                return file.read()
    return None
            


