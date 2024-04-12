#!/root/Virtual-PAC/vQualityMeter/vEnv/bin/python3
from util import get_ip_address

class QualityAPI(object):
    def __init__(self):
        self.server_socket = None

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

    def clientHandler(self, clientSocket):
        try:
            while True:
                data = clientSocket.recv(1024*4)
                if data:
                    info = json.loads(data)
                    if 'entry' not in info:
                        clientSocket.sendall("error".encode())
                        continue
                    entry = info['entry']
                    
                    if entry == 'testConnection':
                        clientSocket.sendall("success".encode())
        except Exception as e:
            print(f'Error: {e}')
        finally:
            clientSocket.close()
            


if __name__ == '__main__':
    api = QualityAPI()
    api.connect(get_ip_address(), 8008)
    api.runServer()











