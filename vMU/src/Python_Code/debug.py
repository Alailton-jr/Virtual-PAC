import psutil, signal

process = '/root/Virtual-PAC/vMU/vEnv/bin/python3 /root/Virtual-PAC/vMU/src/Python_Code/continuous.py'
process = '/root/Virtual-PAC/vMU/vEnv/bin/python3 /root/Virtual-PAC/vMU/src/Python_Code/continuous.py'
process = r'/root/Virtual-PAC/vMU/vEnv/bin/python3 /root/Virtual-PAC/vMU/src/Python_Code/continuous.py'

def send_signal_by_name(process_name, signal):
    try:
        for process in psutil.process_iter(['pid', 'name']):
            cmd = ' '.join(psutil.Process(process.info['pid']).cmdline())
            if process_name in cmd:
                process_pid = process.info['pid']
            else:
                continue
            try:
                psutil.Process(process_pid).send_signal(signal)
                break
            except psutil.NoSuchProcess:
                print(f"Process '{process_name}' not found.")
    except psutil.NoSuchProcess:
        pass

send_signal_by_name(process, signal.SIGTERM)