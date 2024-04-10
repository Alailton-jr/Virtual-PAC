
import numpy as np
import psutil, signal, os


# def send_signal_to_process(process_name, pid, signal_number):
#     while 1:
#         if pid is None:
#             pid = find_process_id_by_name(process_name)
#             if pid is None:
#                 return
#         try:
#             os.kill(pid, signal_number)
#             print(f"Signal {signal_number} sent to process {pid}.")
#             pid = None
#         except ProcessLookupError:
#             print(f"Process {pid} not found.")
#             return
#         except PermissionError:
#             print(f"Permission denied to send signal to process {pid}.")
#             return

# def find_process_id_by_name(process_name):
#     import psutil
#     for process in psutil.process_iter(['pid', 'name', 'cmdline']):
#         print(process.info)
#         if process.info['name'] == process_name:
#             return process.pid
#     return None

# send_signal_to_process('sequenceReplay', None, signal.SIGTERM)


symMatrix = np.asarray([
    [[1/3, 0], [-0.5/3, 0.86602540378/3], [-0.5/3, -0.86602540378/3]],
    [[1/3, 0], [-0.5/3, -0.86602540378/3], [-0.5/3, 0.86602540378/3]],
    [[1/3, 0], [1/3, 0], [1/3, 0]]

])

x_input = np.asarray([
    [-764.51988391713473, 215.61690701160813], [571.3908393983728, 562.61215753457861], [193.22673441149067, -778.25661594538462]
])

x_np = np.zeros((3, ), dtype=np.complex128)
symMatrix_np = np.zeros((3, 3), dtype=np.complex128)
for i in range(3):
    x_np[i] = x_input[i][0] + 1j * x_input[i][1]
    for j in range(3):
        symMatrix_np[i][j] = symMatrix[i][j][0] + 1j * symMatrix[i][j][1]

y = np.zeros((3,2))
for i in range(3):
    y[i][0] = 0
    y[i][1] = 0
    for j in range(3):
        y[i][0] += symMatrix[i][j][0] * x_input[j][0] - symMatrix[i][j][1] * x_input[j][1]
        y[i][1] += symMatrix[i][j][0] * x_input[j][1] + symMatrix[i][j][1] * x_input[j][0]

# y = np.dot(symMatrix_np, x_np)

x_polar = np.zeros((3, 2))
for i in range(3):
    x_polar[i][0] = np.sqrt(x_input[i][0]**2 + x_input[i][1]**2)
    x_polar[i][1] = np.arctan2(x_input[i][1], x_input[i][0])*180/np.pi

y_polar = np.zeros((3, 2))
for i in range(3):
    y_polar[i][0] = np.sqrt(y[i][0]**2 + y[i][1]**2)
    y_polar[i][1] = np.arctan2(y[i][1], y[i][0])*180/np.pi

angRef = x_polar[0][1]
for i in range(3):
    x_polar[i][1] -= angRef
    print(f'{x_polar[i][0]}|_ {x_polar[i][1]}')
for i in range(3):
    y_polar[i][1] -= angRef
    print(f'{y_polar[i][0]}|_ {y_polar[i][1]}')