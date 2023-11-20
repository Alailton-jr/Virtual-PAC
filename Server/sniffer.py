import numpy as np, ctypes
from multiprocessing import shared_memory
from time import perf_counter

class pcap_pkthdr(ctypes.Structure):
    _fields_ = [
        ("ts_sec", ctypes.c_long),
        ("ts_usec", ctypes.c_long),
        ("caplen", ctypes.c_uint),
        ("len", ctypes.c_uint)
    ]
    
class pcap_t(ctypes.Structure):
    pass

interface = r'\Device\NPF_{7934E0B7-356F-420B-971A-9F3B0E8EF3DA}'
# interface = r'\Device\NPF_{CBB73B96-3E81-4C32-9116-CB0A52B42DF8}'
# interface = r'\Device\NPF_{0C858132-7240-475E-A3DE-94F6DC9D5078}'
libpcap = ctypes.CDLL('wpcap.dll')
pcap_t_ptr = ctypes.POINTER(pcap_t)
errbuf = ctypes.create_string_buffer(256)
pcap_open_live = libpcap.pcap_open_live
pcap_open_live.argtypes = [ctypes.c_char_p, ctypes.c_int, ctypes.c_int, ctypes.c_int, ctypes.POINTER(ctypes.c_char)]
pcap_open_live.restype = pcap_t_ptr
pcap_handle = libpcap.pcap_open_live(interface.encode(), 65536, 1, 1000, errbuf)
pcap_next_ex = libpcap.pcap_next_ex
pcap_next_ex.argtypes = [pcap_t_ptr, ctypes.POINTER(ctypes.POINTER(pcap_pkthdr)), ctypes.POINTER(ctypes.POINTER(ctypes.c_ubyte))]
pcap_next_ex.restype = ctypes.c_int
pkt_data = ctypes.POINTER(ctypes.c_ubyte)()
pkt_header = ctypes.POINTER(pcap_pkthdr)()


size = 64 * 1024 * 1024
numBuffers = 3
SharedBuffers = []
bufferList = []
bufferNum = []

try:
    sharedConfig = shared_memory.SharedMemory(name='snifferConfig',create=True, size=np.ndarray((2,),dtype=np.uint64).nbytes)
except:
    sharedConfig = shared_memory.SharedMemory(name='snifferConfig',create=False, size=np.ndarray((2,),dtype=np.uint64).nbytes)
snifferConfig = np.ndarray((2,),dtype=np.uint64,buffer=sharedConfig.buf)
snifferConfig[0] = size
snifferConfig[1] = numBuffers

for i in range(numBuffers): # Buffers
    try:
        SharedBuffers.append(shared_memory.SharedMemory(name='snifferBuffer_'+ "{:02}".format(i),create=True, size=np.ndarray((size,),dtype=np.uint8).nbytes))
    except:
        SharedBuffers.append(shared_memory.SharedMemory(name='snifferBuffer_' + "{:02}".format(i),create=False, size=np.ndarray((size,),dtype=np.uint8).nbytes))
    bufferList.append(
        np.ndarray((size,),dtype=np.uint8,buffer=SharedBuffers[i].buf)
    )
try: # Quantidade de pacotes
    shared_bufferNum = shared_memory.SharedMemory(name='snifferBufferNum',create=True, size=np.ndarray((numBuffers,),dtype=np.uint32).nbytes)
except:
    shared_bufferNum = shared_memory.SharedMemory(name='snifferBufferNum',create=False, size=np.ndarray((numBuffers,),dtype=np.uint32).nbytes)
bufferNum = np.ndarray((numBuffers,),dtype=np.uint32,buffer=shared_bufferNum.buf)

try: # Index do buffer atual
    sharedIdx = shared_memory.SharedMemory(name='snifferIdx',create=True, size=np.ndarray((1,),dtype=np.uint8).nbytes)
except:
    sharedIdx = shared_memory.SharedMemory(name='snifferIdx',create=False, size=np.ndarray((1,),dtype=np.uint8).nbytes)
bufferIdx = np.ndarray((1,),dtype=np.uint8,buffer=sharedIdx.buf)


pos = 0
IndexNow = 0
bufferIdx[0] = 0 # Indice do buffer que está sendo preenchido
bufferNum[:] = 0 # Quantidade de pacotes
while 1:
    good = pcap_next_ex(pcap_handle, ctypes.byref(pkt_header), ctypes.byref(pkt_data))
    if good != 1:
        continue
    if pkt_header.contents.len < 12:
        continue
    if pkt_header.contents.len + pos + 2 > size:
        if bufferIdx[0] == numBuffers - 1:
            bufferIdx[0] = 0
        else:
            bufferIdx[0] += 1
    if IndexNow != bufferIdx[0]:
        pos = 0
        bufferNum[bufferIdx[0]] = 0
        IndexNow = bufferIdx[0]
    bufferList[bufferIdx[0]][pos+1] = pkt_header.contents.len & 0xff
    bufferList[bufferIdx[0]][pos] = (pkt_header.contents.len >> 8) & 0xff
    bufferList[bufferIdx[0]][pos+2:pos+2 + pkt_header.contents.len] = np.frombuffer((ctypes.c_uint8 * pkt_header.contents.len).from_address(ctypes.addressof(pkt_data.contents)),dtype=np.uint8)
    pos += 2 + pkt_header.contents.len
    bufferNum[bufferIdx[0]] += 1

