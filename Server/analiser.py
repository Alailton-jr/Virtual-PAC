import numpy as np
from multiprocessing import shared_memory, Manager, Pool
from time import sleep, perf_counter
from yaml import safe_dump, safe_load



try:
    sharedConfig = shared_memory.SharedMemory(name='snifferConfig',create=True, size=np.ndarray((2,),dtype=np.uint64).nbytes)
except:
    sharedConfig = shared_memory.SharedMemory(name='snifferConfig',create=False, size=np.ndarray((2,),dtype=np.uint64).nbytes)
snifferConfig = np.ndarray((2,),dtype=np.uint64,buffer=sharedConfig.buf)

size = snifferConfig[0]
numBuffers = snifferConfig[1]
SharedBuffers = []
bufferList = []
bufferNum = []

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

goose = {}
sv = {}

def decode(frame, sv):
    if  frame[12]   == 0x81 and frame[13] == 0x00:
        i_etherType = 16
    else:
        i_etherType = 12
    if frame[i_etherType] != 0x88:
        return
    if frame[i_etherType + 1] == 0xba:
        _type = 'sv'
    elif frame[i_etherType + 1] == 0xb8:
        _type = 'go'
    else:
        return

    if _type == 'sv':
        i_sv = i_etherType + 2
        _sv = {
            'appId' : int(frame[i_sv]*256 + frame[i_sv+1]),
            'length' : int(frame[i_sv+2]*256 + frame[i_sv+3]),
            'reserved' : frame[i_sv + 4 : 8 + i_sv],#.tobytes(),
            'noAsdu' :  int(frame[i_sv+8+4]),
        }
        i_seq = i_sv+8+7
        _sv['asdu'] = []
        for i in range(frame[i_sv+8+4]):
            if frame[i_seq + 1] == 0x82:
                length = frame[i_seq + 2]*256 + frame[i_seq + 3]
                i_asdu = i_seq + 4
            else:
                length = frame[i_seq + 1]
                i_asdu = i_seq + 2
            i_seq += length+2
            _asdu = {}
            while i_asdu < i_seq:
                length = frame[i_asdu+1]
                if frame[i_asdu] in [0x80, 0x81]:
                    _asdu[asduTypes[frame[i_asdu]]] = frame[i_asdu+2:i_asdu+2+length].tobytes().decode('utf-8')
                elif frame[i_asdu] in [0x87]:
                    _asdu[asduTypes[frame[i_asdu]]] = frame[i_asdu+2:i_asdu+2+length].tobytes()
                else:
                    _asdu[asduTypes[frame[i_asdu]]] = int.from_bytes(frame[i_asdu+2:i_asdu+2+length].tobytes())
                i_asdu += length + 2
            _sv['asdu'].append(_asdu)
        try:
            sv[frame[0:6].tobytes().hex()].append(_sv)
        except:
            sv[frame[0:6].tobytes().hex()] = [_sv]
asduTypes = {
    0x80: 'svID',
    0x81: 'datSet',
    0x82: 'smpCnt',
    0x83: 'confRev',
    0x84: 'refrTm',
    0x85: 'smpSynch',
    0x86: 'smpRate',
    0x87: 'seqData',
    0x88: 'smpMod',
    0x89: 'gmidData'
}

def inspect(t0, t1):
        idx = bufferIdx[0]
        if bufferIdx[0] == numBuffers - 1:
            bufferIdx[0] = 0
        else:
            bufferIdx[0] += 1
        sleep(0.1)
        pos = 0
        sizeCalc = 0
        for i in range(bufferNum[idx]):
            length = bufferList[idx][pos] * 256 + bufferList[idx][pos+1]
            decode(bufferList[idx][pos+2:pos+2+length], sv)
            pos += length+2
            sizeCalc += length
        print(f'{sizeCalc/(t0-t1)/1024/1024*8} Mb')
        bufferNum[idx] = 0

def main():
    j = 0
    t1 = 0
    t0 = 0
    while 1:
        j += 1
        sleep(1)
        t0 = perf_counter()
        inspect(t0, t1)
        t1 = perf_counter()
        print(t1-t0-0.1)
        if j == 40:
            break

main()

for key, values in sv.items():
    print(len(values))


# import cProfile
# frame = np.frombuffer(bytes.fromhex('010ccd0400009cda3e84a1d48100800088ba40000042000000006038800101a23330318008544361000000000082020c698304000000018501008718ffffb78000000000ffffb78000000000ffffb78000000000'), dtype= np.uint8)

# import cProfile, pstats
# profiler = cProfile.Profile()
# profiler.enable()
# decode()
# profiler.disable()
# stats = pstats.Stats(profiler).sort_stats('tottime')
# stats.print_stats()

# cProfile.run('decode()')



# with open('captures.yaml', 'w') as f:
#     safe_dump(sv, f)

# frame = bytes.fromhex('010ccd0100019cda3e84a1d48100800088ba4000008400000000607a800103a275302580085443610000000000820201a3830400000001850100870c4218544141d9143bca8df311302580085443620000000000820201a3830400000001850100870cc2c63c6c41d9143bca8df311302580085443630000000000820201a3830400000001850100870c4274249641d9143bca8df311')
# frame = np.frombuffer(frame, dtype=np.uint8)
# decode(frame)

# for key, values  in sv.items():
#     print(f'{key}:')
#     for value in values:
#         for k, v in value.items():
#             if k != 'asdu':
#                 print(f'  {k}: {v}')
#             else:
#                 print('  Asdu:')
#                 for asdu in v:
#                     for a, b in asdu.items():
#                         print(f'    {a}: {b}')
        



