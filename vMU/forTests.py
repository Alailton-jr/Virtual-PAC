#!/root/Virtual-PAC/vMU/vEnv/bin/python3

import subprocess, os, yaml, psutil


processName = r'/root/Virtual-PAC/vMU/vEnv/bin/python3 /root/Virtual-PAC/vMU/src/Python_Code/muApi.py 9228'

def send_signal_by_name(process_name, signal):
    try:
        for process in psutil.process_iter(['pid', 'name']):
            cmd = ' '.join(psutil.Process(process.info['pid']).cmdline())
            if 'muApi' in cmd:
                ds = 4
            if cmd == process_name:
                process_pid = process.info['pid']
            else:
                continue
            try:
                psutil.Process(process_pid).send_signal(signal)
            except psutil.NoSuchProcess:
                print(f"Process '{process_name}' not found.")
    except psutil.NoSuchProcess:
        print(f"Process '{process_name}' not found.")


send_signal_by_name(processName, psutil.signal.SIGTERM)






exit(0)

def main():
    subprocess.run(command_Continuous, stdout=subprocess.PIPE, stderr=subprocess.PIPE, text=True)
    pass


# Service Directory
myDir = os.getcwd()
workDir = os.path.dirname(os.path.abspath(__file__))
os.chdir(workDir)

C_buildPath = os.path.join(workDir, 'src', 'C_Build')
Python_buildPath = os.path.join(workDir, 'src', 'Python_Code')
Python_Executable = '/root/vMU/vEnv/bin/python3'

res = subprocess.run(['make', '-f', 'Makefile', 'build'], stdout=subprocess.PIPE, stderr=subprocess.PIPE, text=True)
if res.returncode != 0:
    print(res.stderr)
    exit(0)
else:
    print(res.stdout)

def getIface():
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

command_Continuous = f'{Python_Executable} {Python_buildPath}/continuous.py'
with open("networkSetup.yaml", "r") as file:
    conf = yaml.safe_load(file)['GoNetwork']
command_Sniffer = f"{C_buildPath}/sniffer {conf['macSrc']} {conf['goId']} {getIface()} {'0'}"
command_API = f'{Python_Executable} {Python_buildPath}/muApi.py {os.getpid()}'
command_Sequencer = f'{Python_Executable} {Python_buildPath}/sequencer.py'

# send_signal_by_name(, psutil.signal.SIGTERM)

main()