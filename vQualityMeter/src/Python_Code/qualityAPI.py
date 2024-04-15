#!/root/Virtual-PAC/vQualityMeter/vEnv/bin/python3

from util import get_ip_address
import socket, threading, json, datetime
from API_Functions import *

QualityAPIDir = os.path.dirname(os.path.realpath(__file__))

class QualityAPI(object):
    def __init__(self):
        self.server_socket = None
        self.netCapture = [False, None]
        self.logFile = None

    def log(self, msg:str):
        if self.logFile:
            self.logFile.write(f'{datetime.datetime.now()} - {msg}\n')
        else:
            self.logFile = open(os.path.join(QualityAPIDir, 'log.txt'), 'a')
            

    def connect(self, ip:str, port:int) -> bool:
        print(f'Starting MU API at: IP {ip}/{port}')
        try:
            self.server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            self.server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
            self.server_socket.bind((ip, port))
            self.server_socket.listen(5)
            return True
        except Exception as e:
            print(f'Error: {e}')
            return False
    
    def runServer(self, onThread=False):
        if onThread:
            threading.Thread(target=self._runServer, daemon=True).start()
        else:
            self._runServer()
    
    def _runServer(self):
        while True:
            client_socket, client_address = self.server_socket.accept()
            print("Connected to client:", client_address)
            threading.Thread(target=self.clientHandler, args=(client_socket,), daemon=True).start()

    def sendFile(self, file:str):
        pass

    def receiveFile(self):
        pass

    def clientHandler(self, clientSocket:socket.socket):
        try:
            while True:
                data = clientSocket.recv(1024*4)
                if data:
                    info = json.loads(data)
                    if 'entry' not in info:
                        clientSocket.sendall("error".encode())
                        self.log("Error: No Entry")
                        continue
                    entry = info['entry']
                    
                    if entry == 'testConnection':
                        self.log("Test Connection")
                        clientSocket.sendall("success".encode())

                    #region Network Capture
                    elif entry == 'netCaptureStart':
                        if getNetCaptureStatus():
                            self.log("Error: Network Capture already running")
                            clientSocket.sendall("error".encode())
                        else:
                            self.log("Starting Network Capture")
                            duration = info['data']
                            runNetworkCapture(duration)
                            clientSocket.sendall("success".encode())
                    elif entry == 'netCaptureStop':
                        if not getNetCaptureStatus():
                            self.log("Error: Network Capture not running")
                            clientSocket.sendall("error".encode())
                        else:
                            self.log("Stopping Network Capture")
                            stopNetworkCapture()
                            clientSocket.sendall("success".encode())
                    elif entry == 'netCaptureStatus':
                        self.log("Getting Network Capture Status")
                        status = getNetCaptureStatus()
                        clientSocket.sendall(str(status).encode())
                    elif entry == 'netCaptureResults':
                        results = getNetCaptureResults()
                        if results:
                            self.log("Sending Results of Network Capture")
                            clientSocket.sendall(len(results).to_bytes(4, byteorder='little'))
                            clientSocket.sendall(results)
                        else:
                            self.log("Error: No Results")
                            clientSocket.sendall("error".encode())
                    elif entry == 'netCaptureWaveForm':
                        svId = info['data']
                        waveform = getNetCaptureWaveForm(svId)
                        if waveform:
                            self.log("Sending Waveform of Network Capture")
                            clientSocket.sendall(len(waveform).to_bytes(4, byteorder='little'))
                            clientSocket.sendall(waveform)
                        else:
                            self.log("Error: No Waveform")
                            clientSocket.sendall("error".encode())
                    #endregion
                    
                    #region Network Monitoring

                    elif entry == 'ReceiveMonitorSetup':
                        fileData = info['data']
                        if fileData:
                            if saveMonitorSetup(fileData):
                                self.log("Setting up Monitor")
                                clientSocket.sendall("success".encode())
                            else:
                                self.log("Error: Setting up Monitor")
                                clientSocket.sendall("error".encode())
                        else:
                            self.log("Error: No Data")
                            clientSocket.sendall("error".encode())
                    elif entry == 'getRegistedEvents':
                        svId = info['data']
                        if svId:
                            self.log(f"Getting Registered Events of {svId}")
                            data = getAnalyseEvents(svId)
                            clientSocket.sendall(json.dumps(data).encode())
                        else:
                            self.log("Error: No Data")
                            clientSocket.sendall("error".encode())
                    elif entry == 'getMonitorWaveForm':
                        name = info['data']
                        if name:
                            waveform = getAnalyseWaveForm(name)
                            if waveform:
                                self.log("Sending Waveform from Analyse")
                                clientSocket.sendall(len(waveform).to_bytes(4, byteorder='little'))
                                clientSocket.sendall(waveform)
                            else:
                                clientSocket.sendall((0).to_bytes(4, byteorder='little'))
                        else:
                            clientSocket.sendall((0).to_bytes(4, byteorder='little'))

                    elif entry == 'netMonitorStart':
                        pass

                    #end region

                    else:
                        self.log("Error: Invalid Entry")
                        clientSocket.sendall("error".encode())
                    
        except Exception as e:
            self.log(f"Exception: {e}")
            print(f'Error: {e} ')
        finally:
            clientSocket.close()
            


if __name__ == '__main__':
    api = QualityAPI()
    api.connect(get_ip_address(), 8008)
    api.runServer()











