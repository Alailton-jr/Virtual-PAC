#!/root/Virtual-PAC/vQualityMeter/vEnv/bin/python3

import subprocess, os


exit()
subprocess.run(['gcc', '-shared', '-o', 'src/Python_Code/QualityMeter_lib.so', '-fPIC', '-I', '/usr/include/python3.11/', 'vQualityMeter/src/C_Code/sampledValue.c'])

# subprocess.run(['make', 'build'], cwd='vQualityMeter')


import QualityMeter_lib as qm
from time import sleep

qm.addSampledValue(0, "TRTC", 80)


# path = r'/root/Virtual-PAC/vQualityMeter/src/C_Build'
# codePath = os.path.join(path, r'sniffer')

# if __name__ == '__main__':
    # process = subprocess.Popen([codePath, 'eth0'])
    # process.wait()
    # subprocess.run([codePath, 'eth0'])

# sleep(5)
input("Press Enter to continue...")
qm.deleteSampledValueMemory()
print("done")