#!/root/vMU/vEnv/bin/python3

import subprocess, os, signal, time, socket, threading, json, psutil, yaml, numpy as np, src.Python_Code.muApi as muApi
from multiprocessing import shared_memory


# Service Directory
myDir = os.getcwd()
workDir = os.path.dirname(os.path.abspath(__file__))
os.chdir(workDir)

C_buildPath = os.path.join(workDir, 'src', 'C_Build')
Python_buildPath = os.path.join(workDir, 'src', 'Python_Code')
Python_Executable = '/root/vMU/vEnv/bin/python3'

try:
    controllerShm = shared_memory.SharedMemory(name='controller', create=False, size=4)
except:
    controllerShm = shared_memory.SharedMemory(name='controller', create=True, size=4)
controller = np.ndarray((1,), dtype=np.uint32, buffer=controllerShm.buf)

def getIface():
    for d in psutil.net_if_stats().keys():
        if d != 'lo':
            return str(d)
    return None

def preload():
    import src.Python_Code.guestConf as guest
    guest.hostUpdate()
    res = subprocess.run(['make', '-f', 'Makefile', 'build'], stdout=subprocess.PIPE, stderr=subprocess.PIPE, text=True)
    if res.returncode != 0:
        print(res.stderr)
        exit(0)
    else:
        print(res.stdout)

def cleanUp():
    controllerShm.unlink()
    
    res = subprocess.run(['make', '-f', 'Makefile', 'clean'], stdout=subprocess.PIPE, stderr=subprocess.PIPE, text=True)
    if res.returncode != 0:
        print(res.stderr)
        exit(1)
    else:
        print(res.stdout)

class vMU_Brain:
    def __init__(self) -> None:
        self.tests = {}
        self.sniffer = {}
        self.api = {}
        self.runSniffer()
        time.sleep(0.5)
        self.runAPI()

    def send_signal_by_name(self, process_name, signal):
        for process in psutil.process_iter(['pid', 'name']):
            cmd = ' '.join(psutil.Process(process.info['pid']).cmdline())
            if cmd == process_name:
                process_pid = process.info['pid']
                try:
                    psutil.Process(process_pid).send_signal(signal)
                except psutil.NoSuchProcess:
                    print(f"Process '{process_name}' not found.")

    def runAPI(self):
        # put pid 
        self.api['command'] = f'{Python_Executable} {Python_buildPath}/muApi.py {os.getpid()}'
        self.api['process'] = subprocess.Popen(self.api['command'], shell=True)
        self.api['running'] = True
    
    def stopAPI(self):
        if self.api['running']:
            self.send_signal_by_name(self.api['command'], psutil.signal.SIGTERM)
            self.api['running'] = False

    def API_Handler(self, signal1, signal2):
        if controller[0] == muApi.START_CONTINUOUS:
            self.runContinuous()
            controller[0] = muApi.NOTHING
        elif controller[0] == muApi.STOP_CONTINUOUS:
            self.stopContinuous()
            controller[0] = muApi.NOTHING
        elif controller[0] == muApi.UPDATE_CONTINUOUS:
            self.updateContinuous()
            controller[0] = muApi.NOTHING
        elif controller[0] == muApi.START_SEQUENCER:
            self.runSequencer()
            controller[0] = muApi.NOTHING
        elif controller[0] == muApi.STOP_SEQUENCER:
            self.stopSequencer()
            controller[0] = muApi.NOTHING
        elif controller[0] == muApi.RESTART_NETWORK:
            self.restartSniffer()
            controller[0] = muApi.NOTHING
        self.send_signal_by_name(self.api['command'], psutil.signal.SIGUSR2)

    def runSequencer(self):
        if 'continuous' in self.tests:
            if self.tests['continuous']['running']:
                self.stopContinuous()
        if 'sequencer' in self.tests:
            if self.tests['sequencer']['running']:
                self.stopSequencer()
        self.tests['sequencer'] = {}
        self.tests['sequencer']['command'] = f'{Python_Executable} {Python_buildPath}/sequencer.py'
        self.tests['sequencer']['process'] = subprocess.Popen(self.tests['sequencer']['command'], shell=True)
        self.tests['sequencer']['running'] = True

    def stopSequencer(self,):
        if 'sequencer' not in self.tests:
            return
        self.tests['sequencer']['running'] = False
        self.send_signal_by_name(self.tests['sequencer']['command'], psutil.signal.SIGTERM)

    def runContinuous(self):
        if 'continuous' in self.tests:
            if self.tests['continuous']['running']:
                print('Continuous already running')
                self.updateContinuous()
                return
        self.tests['continuous'] = {}
        self.tests['continuous']['command'] = f'{Python_Executable} {Python_buildPath}/continuous.py'
        self.tests['continuous']['process'] = subprocess.Popen(self.tests['continuous']['command'], shell=True)
        self.tests['continuous']['running'] = True

    def updateContinuous(self,):
        if 'continuous' not in self.tests:
            return
        if self.tests['continuous']['running']:
            self.send_signal_by_name(self.tests['continuous']['command'], psutil.signal.SIGUSR1)

    def stopContinuous(self):
        if 'continuous' not in self.tests:
            return
        self.tests['continuous']['running'] = False
        self.send_signal_by_name(self.tests['continuous']['command'], psutil.signal.SIGTERM)

    def runSniffer(self):
        with open("networkSetup.yaml", "r") as file:
            conf = yaml.safe_load(file)['GoNetwork']
        self.sniffer['config'] = conf
        self.sniffer['command'] = f"{C_buildPath}/sniffer {conf['macSrc']} {conf['goId']} {getIface()} {'0'}"
        self.sniffer['process'] = subprocess.Popen(self.sniffer['command'], shell=True)
        self.sniffer['running'] = True

    def stopSniffer(self):
        if self.sniffer['running']:
            self.send_signal_by_name(self.sniffer['command'], psutil.signal.SIGTERM)
            self.sniffer['running'] = False

    def restartSniffer(self):
        if self.sniffer['running']:
            with open("networkSetup.yaml", "r") as file:
                _conf = yaml.safe_load(file)['GoNetwork']
            if _conf != self.sniffer['config']:
                self.stopSniffer()
                self.runSniffer()

    def cleanSniffer(self):
        pass

    def stopTests(self, name):
        if name in ['continuous', 'all']:
            self.stopContinuous()
        elif name in ['sequencer', 'all']:
            self.stopSequencer()

    def stopAll(self):
        self.stopTests('all')
        self.stopSniffer()
        self.stopAPI()

def stop(signal1 , signal2):
    x.stopAll()
    cleanUp()
    controllerShm.unlink()
    exit(0)

signal.signal(signal.SIGINT, stop)
signal.signal(signal.SIGTERM, stop)

preload()
x = vMU_Brain()
signal.signal(signal.SIGUSR1, x.API_Handler)

# x.runContinuous()

while(1):
    time.sleep(1)

# x.stopContinuous()

cleanUp()


# x.runContinuous()

# t0 = time.time()
# while time.time() - t0 < 120:
#     pass