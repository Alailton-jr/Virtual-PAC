#!/root/Virtual-PAC/vMU/vEnv/bin/python3


import numpy as np


x = np.array([
    [1, 2, 3],
    [1, 2, 3]
])

print(x.flatten(order='F'))

# from scapy.all import conf
# import numpy as np


# packets = np.ndarray(shape=(120, 1), dtype=np.uint8)
# packets[:] = 0

# socket = conf.L2socket(iface='lo')

# for i in range(0, 12000):
#     socket.send(packets)

