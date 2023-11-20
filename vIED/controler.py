#!/root/vIED/vEnv/bin/python3

import subprocess, os, signal, time, yaml, psutil, sys

# Service Directory
myDir = os.getcwd()
workDir = os.path.dirname(os.path.abspath(__file__))
os.chdir(workDir)

C_buildPath = os.path.join(workDir, 'src', 'C_Build')
Python_buildPath = os.path.join(workDir, 'src', 'Python_Code')

mainClass = None

def main():
    global mainClass
    signal.signal(signal.SIGINT, stop)
    signal.signal(signal.SIGTERM, stop)
    signal.signal(signal.SIGIO, restartSignal)
    preload()
    config = loadYaml('IedConfig.yaml')
    mainClass = vIED_Brain(config, getIface())
    mainClass.run()
    print('Running')
    while 1:
        time.sleep(1)
    cleanUp()

def getIface() -> str:
    for d in psutil.net_if_stats().keys():
        if d != 'lo':
            return str(d)
    return None

def loadYaml(name):
    with open(name, 'r') as file:
        return yaml.safe_load(file)

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
    res = subprocess.run(['make', '-f', 'Makefile', 'clean'], stdout=subprocess.PIPE, stderr=subprocess.PIPE, text=True)
    sys.stdout.close()
    sys.stderr.close()
    exit(1)
    

curves = {
    'U1': {'A': 0.0226, 'B': 0.0104, 'C': 0.02},
    'U2': {'A': 0.180, 'B': 5.95, 'C': 2},
    'U3': {'A': 0.0963, 'B': 3.88, 'C': 2},
    'U4': {'A': 0.0352, 'B': 5.67, 'C': 2},
    'U4': {'A': 0.00262, 'B': 0.00342, 'C': 0.02},
    'C1': {'A': 0, 'B': 0.14, 'C': 0.02},
    'C2': {'A': 0, 'B': 13.5, 'C': 1},
    'C3': {'A': 0, 'B': 80, 'C': 2},
    'C4': {'A': 0, 'B': 120, 'C': 1},
    'C5': {'A': 0, 'B': 0.05, 'C': 0.04},
    }
        
class vIED_Brain:

    def __init__(self, config, iface):
        self.config = config
        self.prot = {}
        self.sniffer = {}
        self.api = {}
        self.gooseSender = []
        self._iface = iface
        self._config()

    def _config(self,):
        self._protConfig()
        self._snifferConfig()
        self._ApiConfig()
        self._gooseSenderConfig()
        
    def send_signal_by_name(self, process_name, signal):
        for process in psutil.process_iter(['pid', 'name']):
            cmd = ' '.join(psutil.Process(process.info['pid']).cmdline())
            if cmd == process_name:
                process_pid = process.info['pid']
                try:
                    psutil.Process(process_pid).send_signal(signal)
                except psutil.NoSuchProcess:
                    print(f"Process '{process_name}' not found.")

    def _protConfig(self):
        protConfig = self.config['Protection Function']
        pioc_p = [{}, {}, {}]
        ptoc_p = [{}, {}, {}]
        pioc_n = [{}, {}, {}]
        ptoc_n = [{}, {}, {}]
        ptuv = [{}, {}, {}]
        ptov = [{}, {}, {}]
        pdis = [{}, {}, {}]
        for i in range(3):
            conf = protConfig['PIOC Phase'][i]
            pioc_p[i]['config'] = conf
            pioc_p[i]['command'] = f"{C_buildPath}/pioc_p {i+1} {conf['Pickup']} {conf['Time Delay']} PROT.P{i+1}PIOC.Op.general"
            pioc_p[i]['process'] = None
            pioc_p[i]['running'] = False
            pioc_p[i]['enable'] = conf['Enabled']

            conf = protConfig['PTOC Phase'][i]
            ptoc_p[i]['config'] = conf
            ptoc_p[i]['command'] = f"{C_buildPath}/ptoc_p {i+1} {conf['Pickup']} {conf['Time Dial']} {curves[conf['Curve']]['A']} {curves[conf['Curve']]['B']} {curves[conf['Curve']]['C']} PROT.P{i+1}PTOC.Op.general"
            ptoc_p[i]['process'] = None
            ptoc_p[i]['running'] = False
            ptoc_p[i]['enable'] = conf['Enabled']

            conf = protConfig['PIOC Neutral'][i]
            pioc_n[i]['config'] = conf
            pioc_n[i]['command'] = f"{C_buildPath}/pioc_n {i+1} {conf['Pickup']} {conf['Time Delay']} PROT.N{i+1}PIOC.Op.general"
            pioc_n[i]['process'] = None
            pioc_n[i]['running'] = False
            pioc_n[i]['enable'] = conf['Enabled']

            conf = protConfig['PTOC Neutral'][i]
            ptoc_n[i]['config'] = conf
            ptoc_n[i]['command'] = f"{C_buildPath}/ptoc_n {i+1} {conf['Pickup']} {conf['Time Dial']} {curves[conf['Curve']]['A']} {curves[conf['Curve']]['B']} {curves[conf['Curve']]['C']} &"
            ptoc_n[i]['process'] = None
            ptoc_n[i]['running'] = False
            ptoc_n[i]['enable'] = conf['Enabled']

            conf = protConfig['PTUV'][i]
            ptuv[i]['config'] = conf
            ptuv[i]['command'] = f"{C_buildPath}/ptuv {i+1} {conf['Pickup']} {conf['Time Delay']} PROT.P{i+1}PTUV.Op.general"
            ptuv[i]['process'] = None
            ptuv[i]['running'] = False
            ptuv[i]['enable'] = conf['Enabled']

            conf = protConfig['PTOV'][i]
            ptov[i]['config'] = conf
            ptov[i]['command'] = f"{C_buildPath}/ptov {i+1} {conf['Pickup']} {conf['Time Delay']} PROT.P{i+1}PTOV.Op.general"
            ptov[i]['process'] = None
            ptov[i]['running'] = False
            ptov[i]['enable'] = conf['Enabled']

            conf = protConfig['PDIS'][i]
            pdis[i]['config'] = conf
            if conf['Type'] == 'Admitancia':
                pdis[i]['command'] = f"{C_buildPath}/pdis_moh {i+1} {conf['Ajuste']} {conf['Angle']} {conf['Time Delay']} PROT.P{i+1}PDIS.Op.general"
            elif conf['Type'] == 'Reatancia':
                pdis[i]['command'] = f"{C_buildPath}/pdis_reat {i+1} {conf['Ajuste']} {conf['Time Delay']} PROT.P{i+1}PDIS.Op.general"
            elif conf['Type'] == 'Impedancia':
                pdis[i]['command'] = f"{C_buildPath}/pdis_ohm {i+1} {conf['Ajuste']} {conf['Time Delay']} PROT.P{i+1}PDIS.Op.general"
            pdis[i]['process'] = None
            pdis[i]['running'] = False
            pdis[i]['enable'] = conf['Enabled']

        self.prot['PIOC Phase'] = pioc_p
        self.prot['PTOC Phase'] = ptoc_p
        self.prot['PIOC Neutral'] = pioc_n
        self.prot['PTOC Neutral'] = ptoc_n
        self.prot['PTUV'] = ptuv
        self.prot['PTOV'] = ptov
        self.prot['PDIS'] = pdis

    def runProt(self, name):
        if name == 'all':
            for _name, val in self.prot.items():
                for i in range(3):
                    if val[i]['enable']:
                        val[i]['process'] = subprocess.Popen(val[i]['command'], shell=True)
                        val[i]['running'] = True
        else:
            val = self.prot[name]
            for i in range(3):
                if val[i]['enable']:
                    val[i]['process'] = subprocess.Popen(val[i]['command'], shell=True)
                    val[i]['running'] = True
    
    def stopProt(self, name):
        if name == 'all':
            for _name, val in self.prot.items():
                for i in range(3):
                    if val[i]['running']:
                        self.send_signal_by_name(val[i]['command'], psutil.signal.SIGTERM)
                        val[i]['running'] = False
        else:
            val = self.prot[name]
            for i in range(3):
                if val[i]['running']:
                    self.send_signal_by_name(val[i]['command'], psutil.signal.SIGTERM)
                    val[i]['running'] = False

    def _snifferConfig(self):
        conf = self.config['Sniffer']
        self.sniffer['config'] = conf
        self.sniffer['command'] = f"{C_buildPath}/sniffer {conf['MacDst']} {conf['SvId']} {self._iface}"
        self.sniffer['running'] = False

    def runSniffer(self,):
        self.sniffer['process'] = subprocess.Popen(self.sniffer['command'], shell=True)
        self.sniffer['running'] = True

    def stopSniffer(self,):
        if self.sniffer['running']:
            self.send_signal_by_name(self.sniffer['command'], psutil.signal.SIGTERM)
            self.sniffer['running'] = False

    def _ApiConfig(self,):
        self.api['command'] = f"{Python_buildPath}/iedApi.py"
        self.api['running'] = False

    def runApi(self,):
        self.api['process'] = subprocess.Popen(self.api['command'], shell=True)
        self.api['running'] = True
    
    def stopApi(self,):
        if self.api['running']:
            self.send_signal_by_name(self.api['command'], psutil.signal.SIGTERM)
            self.api['running'] = False

    def _gooseSenderConfig(self,):
        gooseConf = self.config['Goose']
        self.gooseSender = []
        i = 0
        for go in gooseConf:
            self.gooseSender.append({})
            self.gooseSender[-1]['command'] = f"{Python_buildPath}/goose.py {i}"
            self.gooseSender[-1]['running'] = False
            i += 1

    def runGooseSender(self,):
        for go in self.gooseSender:
            go['process'] = subprocess.Popen(go['command'], shell=True)
            go['running'] = True
    
    def stopGooseSender(self,):
        i = 1
        for go in self.gooseSender:
            if go['running']:
                self.send_signal_by_name(go['command'], psutil.signal.SIGTERM)
                go['running'] = False
            i+=1

    def run(self,):
        self.runSniffer()
        time.sleep(0.5)
        self.runProt('all')
        self.runApi()
        self.runGooseSender()
    
    def stop(self):
        self.stopProt('all')
        self.stopApi()
        time.sleep(0.5)
        self.stopSniffer()
        self.stopGooseSender()

def restartSignal(signum, frame):
    global mainClass
    print('Restarting!')
    mainClass.stop()
    time.sleep(0.5)
    mainClass.config = loadYaml('IedConfig.yaml')
    mainClass._config()
    mainClass.run()

def stop(signum, frame):
    global mainClass
    mainClass.stop()
    cleanUp()
    exit(0)



if __name__ == '__main__':
    main()

