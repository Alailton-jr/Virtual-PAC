using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static Server.classes;

namespace Server
{
    public class classes
    {

        public class ServerCtl
        {

            public List<DeviceClass> devices { get; set; }
            public CommunicationConfig communicationConfig { get; set; }
            public SocketConnection serverCon { get; set; }

            public void saveClass()
            {
                //this.sclConf = new SCLConfig();
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                string filePath = "vServer_data.dat";
                File.WriteAllText(filePath, json);
            }

            public bool loadClass()
            {
                try
                {
                    if (File.Exists("vServer_data.dat"))
                    {
                        string loadedJson = File.ReadAllText("vServer_data.dat");
                        var tempData = JsonConvert.DeserializeObject<ServerCtl>(loadedJson);
                        this.devices = tempData.devices;
                        this.communicationConfig = tempData.communicationConfig;
                        this.serverCon = new SocketConnection();
                    }
                    else
                    {
                        this.devices = new List<DeviceClass>();
                        this.communicationConfig = new CommunicationConfig() {
                            ip = "10.3.193.100",
                            name = "Server 01",
                            password = "passwd",
                            port = 8082,
                            status = false
                        };
                        this.serverCon = new SocketConnection() {
                            serverIp= "10.3.193.100",
                            serverPort = 8082,
                        };
                        this.serverCon = new SocketConnection();
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            //public void TestConnection()
            //{
            //    try
            //    {
            //        if (!clientSocket.Connected)
            //        {
            //            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //            clientSocket.Connect(this.communicationConfig.ip, this.communicationConfig.port);
            //            clientSocket.ReceiveTimeout = 5000;
            //        }

            //        Dictionary<string, string> data = new Dictionary<string, string>()
            //    {
            //        {"entry", "testConnection" },
            //    };
            //        string jsonData = JsonConvert.SerializeObject(data);

            //        byte[] handShake = Encoding.ASCII.GetBytes(jsonData);
            //        byte[] buffer = new byte[1024];

            //        clientSocket.Send(handShake);
            //        int bytesRead = clientSocket.Receive(buffer);
            //        if (bytesRead > 0)
            //        {
            //            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            //            if (receivedData == "okay")
            //                connectionFlag = true;
            //            else
            //                connectionFlag = false;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        connectionFlag = false;
            //        MessageBox.Show("Error Connecting to Server");
            //    }
            //}

            //public void GetVmStatus(DeviceClass device)
            //{
            //    try
            //    {
            //        if (!clientSocket.Connected)
            //        {
            //            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //            clientSocket.Connect(this.communicationConfig.ip, this.communicationConfig.port);
            //            clientSocket.ReceiveTimeout = 5000;
            //        }

            //        Dictionary<string, string> data = new Dictionary<string, string>()
            //        {
            //            {"entry", "getVmState" },
            //            {"data", device.name }
            //        };
            //        string jsonData = JsonConvert.SerializeObject(data);

            //        byte[] handShake = Encoding.ASCII.GetBytes(jsonData);
            //        byte[] buffer = new byte[1024];

            //        clientSocket.Send(handShake);
            //        int bytesRead = clientSocket.Receive(buffer);
            //        if (bytesRead > 0)
            //        {
            //            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            //            Dictionary<string, int> results = JsonConvert.DeserializeObject<Dictionary<string, int>>(receivedData);

            //            if (results != null)
            //            {

            //                if (device == null)
            //                    return;
            //                foreach (KeyValuePair<string, int> kvp in results)
            //                {
            //                    switch (kvp.Key)
            //                    {
            //                        case "state":
            //                            if (kvp.Value == 0)
            //                                device.VmState = DeviceState.down;
            //                            else if (kvp.Value == 1)
            //                                device.VmState = DeviceState.up;
            //                            break;
            //                    }
            //                }

            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        connectionFlag = false;
            //        MessageBox.Show("Error Connecting to Server");
            //    }
            //}

            //public void GetDeviceList()
            //{
            //    try
            //    {
            //        if (!clientSocket.Connected)
            //        {
            //            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //            clientSocket.Connect(this.communicationConfig.ip, this.communicationConfig.port);
            //            clientSocket.ReceiveTimeout = 5000;
            //        }

            //        Dictionary<string, string> data = new Dictionary<string, string>()
            //        {
            //            {"entry", "getDeviceList" },
            //        };
            //        string jsonData = JsonConvert.SerializeObject(data);

            //        byte[] handShake = Encoding.ASCII.GetBytes(jsonData);
            //        byte[] buffer = new byte[1024];

            //        clientSocket.Send(handShake);
            //        int bytesRead = clientSocket.Receive(buffer);
            //        if (bytesRead > 0)
            //        {
            //            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            //            Dictionary<string, List<string>> results = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(receivedData);

            //            if (results != null)
            //            {
            //                this.devices.Clear();

            //                foreach (KeyValuePair<string, List<string>> kvp in results)
            //                {
            //                    switch (kvp.Key)
            //                    {
            //                        case "shut":
            //                            foreach (string vm in kvp.Value)
            //                            {
            //                                this.devices.Add(new DeviceClass()
            //                                {
            //                                    name = vm,
            //                                    VmState = DeviceState.down,
            //                                    type = DeviceType.None
            //                                });
            //                            }
            //                            break;

            //                        case "running":
            //                            foreach (string vm in kvp.Value)
            //                            {
            //                                this.devices.Add(new DeviceClass()
            //                                {
            //                                    name = vm,
            //                                    VmState = DeviceState.up,
            //                                    type = DeviceType.None
            //                                });
            //                            }
            //                            break;
            //                    }
            //                }

            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        connectionFlag = false;
            //        MessageBox.Show("Error Connecting to Server");
            //    }
            //}

            //public void GetVmInfo(string vmName)
            //{
            //    try
            //    {
            //        if (!clientSocket.Connected)
            //        {
            //            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //            clientSocket.Connect(this.communicationConfig.ip, this.communicationConfig.port);
            //            clientSocket.ReceiveTimeout = 5000;
            //        }

            //        Dictionary<string, string> data = new Dictionary<string, string>()
            //        {
            //            {"entry", "getVmInfo" },
            //            {"data", vmName}
            //        };
            //        string jsonData = JsonConvert.SerializeObject(data);

            //        byte[] handShake = Encoding.ASCII.GetBytes(jsonData);
            //        byte[] buffer = new byte[1024];

            //        clientSocket.Send(handShake);
            //        int bytesRead = clientSocket.Receive(buffer);
            //        if (bytesRead > 0)
            //        {
            //            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            //            Dictionary<string, string> results = JsonConvert.DeserializeObject<Dictionary<string, string>>(receivedData); 
            //            if (results != null)
            //            {

            //                var device = this.devices.Find(x => x.name == vmName);

            //                if (device == null)
            //                    return;

            //                if (results["type"] == "IED")
            //                {
            //                    device.type = DeviceType.ied;
            //                    device.port = 8080;
            //                }
            //                else
            //                {
            //                    device.type = DeviceType.mu;
            //                    device.port = 8081;
            //                }
            //                device.ip = results["ip"].Split("/")[0];
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        connectionFlag = false;
            //        MessageBox.Show("Error Connecting to Server");
            //    }
            //}

            //public void ShutdownVm(DeviceClass device)
            //{
            //    try
            //    {
            //        if (!clientSocket.Connected)
            //        {
            //            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //            clientSocket.Connect(this.communicationConfig.ip, this.communicationConfig.port);
            //            clientSocket.ReceiveTimeout = 5000;
            //        }

            //        Dictionary<string, string> data = new Dictionary<string, string>()
            //        {
            //            {"entry", "shutdownVm" },
            //            {"data", device.name}
            //        };
            //        string jsonData = JsonConvert.SerializeObject(data);

            //        byte[] handShake = Encoding.ASCII.GetBytes(jsonData);
            //        byte[] buffer = new byte[1024];

            //        clientSocket.Send(handShake);
            //        int bytesRead = clientSocket.Receive(buffer);
            //        if (bytesRead > 0)
            //        {
            //            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            //            if (receivedData == "okay")
            //                connectionFlag = true;
            //            else
            //                connectionFlag = false;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        connectionFlag = false;
            //        MessageBox.Show("Error Connecting to Server");
            //    }
            //}

            //public void RebootVm(DeviceClass device)
            //{
            //    try
            //    {
            //        if (!clientSocket.Connected)
            //        {
            //            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //            clientSocket.Connect(this.communicationConfig.ip, this.communicationConfig.port);
            //            clientSocket.ReceiveTimeout = 5000;
            //        }

            //        Dictionary<string, string> data = new Dictionary<string, string>()
            //        {
            //            {"entry", "rebootVm" },
            //            {"data", device.name}
            //        };
            //        string jsonData = JsonConvert.SerializeObject(data);

            //        byte[] handShake = Encoding.ASCII.GetBytes(jsonData);
            //        byte[] buffer = new byte[1024];

            //        clientSocket.Send(handShake);
            //        int bytesRead = clientSocket.Receive(buffer);
            //        if (bytesRead > 0)
            //        {
            //            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            //            if (receivedData == "okay")
            //                connectionFlag = true;
            //            else
            //                connectionFlag = false;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        connectionFlag = false;
            //        MessageBox.Show("Error Connecting to Server");
            //    }
            //}

            //public void StartVm(DeviceClass device)
            //{
            //    try
            //    {
            //        if (!clientSocket.Connected)
            //        {
            //            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //            clientSocket.Connect(this.communicationConfig.ip, this.communicationConfig.port);
            //            clientSocket.ReceiveTimeout = 5000;
            //        }

            //        Dictionary<string, string> data = new Dictionary<string, string>()
            //        {
            //            {"entry", "startVm" },
            //            {"data", device.name}
            //        };
            //        string jsonData = JsonConvert.SerializeObject(data);

            //        byte[] handShake = Encoding.ASCII.GetBytes(jsonData);
            //        byte[] buffer = new byte[1024];

            //        clientSocket.Send(handShake);
            //        int bytesRead = clientSocket.Receive(buffer);
            //        if (bytesRead > 0)
            //        {
            //            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            //            if (receivedData == "okay")
            //                connectionFlag = true;
            //            else
            //                connectionFlag = false;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        connectionFlag = false;
            //        MessageBox.Show("Error Connecting to Server");
            //    }
            //}

            //public async void TestConnection2()
            //{
            //    var x = new HttpClient();

            //    Dictionary<string, string> formData = new Dictionary<string, string>
            //    {
            //        { "password", this.communicationConfig.password }
            //    };

            //    try
            //    {
            //        var res = await x.PostAsync($"http://{communicationConfig.ip}:{communicationConfig.port}/testConnection", new FormUrlEncodedContent(formData));
            //        if (res.IsSuccessStatusCode)
            //        {
            //            string responseBody = await res.Content.ReadAsStringAsync();
            //            if (responseBody == "All Good")
            //            {
            //                connectionFlag = true;
            //            }
            //            else if (responseBody == "Invalid Password")
            //            {
            //                connectionFlag = false;
            //                MessageBox.Show("Senha Inválida");
            //            }
            //        }
            //        else
            //            connectionFlag = false;
            //    }
            //    catch
            //    {
            //        connectionFlag = false;
            //    }

            //}

            //public async void GetVmStatus2(DeviceClass device)
            //{

            //    var x = new HttpClient();

            //    Dictionary<string, string> formData = new Dictionary<string, string>
            //    {
            //        { "password", this.communicationConfig.password },
            //        { "VmName", device.name}
            //    };

            //    try
            //    {
            //        var res = await x.PostAsync($"http://{communicationConfig.ip}:{communicationConfig.port}/getVmState", new FormUrlEncodedContent(formData));
            //        if (res.IsSuccessStatusCode)
            //        {
            //            string responseBody = await res.Content.ReadAsStringAsync();

            //            Dictionary<string, int> results = JsonConvert.DeserializeObject<Dictionary<string, int>>(responseBody);

            //            if (results != null)
            //            {

            //                if (device == null)
            //                    return;
            //                foreach (KeyValuePair<string, int> kvp in results)
            //                {
            //                    switch (kvp.Key)
            //                    {
            //                        case "state":
            //                            if (kvp.Value == 0)
            //                                device.VmState = DeviceState.down;
            //                            else if (kvp.Value == 1)
            //                                device.VmState = DeviceState.up;
            //                            break;
            //                    }
            //                }

            //            }
            //        }
            //        else
            //        {

            //        }
            //    }
            //    catch
            //    {
            //    }

            //}

            //public async void GetDeviceList2()
            //{
            //    var x = new HttpClient();

            //    Dictionary<string, string> formData = new Dictionary<string, string>
            //    {
            //        { "password", this.communicationConfig.password }
            //    };

            //    try
            //    {
            //        var res = await x.PostAsync($"http://{communicationConfig.ip}:{communicationConfig.port}/getDeviceList", new FormUrlEncodedContent(formData));
            //        if (res.IsSuccessStatusCode)
            //        {
            //            string responseBody = await res.Content.ReadAsStringAsync();

            //            Dictionary<string, List<string>> results = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(responseBody);

            //            if (results != null)
            //            {
            //                this.devices.Clear();

            //                foreach (KeyValuePair<string, List<string>> kvp in results)
            //                {
            //                    switch  (kvp.Key)
            //                    {
            //                        case "shut":
            //                            foreach (string vm in kvp.Value)
            //                            {
            //                                this.devices.Add(new DeviceClass()
            //                                {
            //                                    name = vm,
            //                                    VmState = DeviceState.down,
            //                                    type = DeviceType.None
            //                                });
            //                            }
            //                            break;

            //                        case "running":
            //                            foreach (string vm in kvp.Value)
            //                            {
            //                                this.devices.Add(new DeviceClass()
            //                                {
            //                                    name = vm,
            //                                    VmState = DeviceState.up,
            //                                    type = DeviceType.None
            //                                });
            //                            }
            //                            break;
            //                    }
            //                }

            //            }
            //        }
            //        else
            //        {

            //        }
            //    }
            //    catch
            //    {
            //    }
            //}

            //public async void GetVmInfo2(string vmName)
            //{
            //    var x = new HttpClient();

            //    Dictionary<string, string> formData = new Dictionary<string, string>
            //    {
            //        { "password", this.communicationConfig.password },
            //        { "Virtual Machine", vmName}
            //    };

            //    try
            //    {
            //        var res = await x.PostAsync($"http://{communicationConfig.ip}:{communicationConfig.port}/getVmInfo", new FormUrlEncodedContent(formData));
            //        if (res.IsSuccessStatusCode)
            //        {
            //            string responseBody = await res.Content.ReadAsStringAsync();

            //            Dictionary<string, string> results = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);

            //            if (results != null)
            //            {

            //                var device = this.devices.Find(x => x.name == vmName);

            //                if (device == null)
            //                    return;
            //                foreach (KeyValuePair<string, string> kvp in results)
            //                {
            //                    switch (kvp.Key)
            //                    {
            //                        case "type":
            //                            if (kvp.Value == "ied")
            //                                device.type = DeviceType.ied;
            //                            else
            //                                device.type = DeviceType.mu;
            //                            break;

            //                        case "IP":
            //                            device.ip = kvp.Value;
            //                            break;

            //                        case "Processors":
            //                            device.processors = int.Parse(kvp.Value);
            //                            break;

            //                        case "Password":
            //                            device.password = kvp.Value;
            //                            break;

            //                        case "Port":
            //                            device.port = int.Parse(kvp.Value);
            //                            break;
            //                    }
            //                }

            //            }
            //        }
            //        else
            //        {

            //        }
            //    }
            //    catch
            //    {
            //    }
            //}

            //public void TestDeviceConnection(DeviceClass device)
            //{
            //    try
            //    {
            //        var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //        clientSocket.ReceiveTimeout = 5000;

            //        clientSocket.Connect(device.ip, device.port);


            //        Dictionary<string, string> data = new Dictionary<string, string>()
            //        {
            //            {"entry", "testConnection" },
            //        };
            //        string jsonData = JsonConvert.SerializeObject(data);

            //        byte[] handShake = Encoding.ASCII.GetBytes(jsonData);
            //        byte[] buffer = new byte[1024];

            //        clientSocket.Send(handShake);
            //        int bytesRead = clientSocket.Receive(buffer);
            //        if (bytesRead > 0)
            //        {
            //            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            //            if (receivedData == "okay")
            //            {
            //                device.connectionFlag = true;
            //                device.serverState = DeviceState.up;
            //            }
            //            else
            //            {
            //                device.connectionFlag = false;
            //                device.serverState = DeviceState.down;
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        device.connectionFlag = false;
            //        device.serverState = DeviceState.down;
            //    }
            //}

            //public async void TestDeviceConnection2(DeviceClass device)
            //{
            //    var x = new HttpClient();

            //    Dictionary<string, string> formData = new Dictionary<string, string>
            //    {
            //        { "password", device.password }
            //    };

            //    try
            //    {
            //        var res = await x.PostAsync($"http://{device.ip}:{device.port}/testConnection", new FormUrlEncodedContent(formData));
            //        if (res.IsSuccessStatusCode)
            //        {
            //            string responseBody = await res.Content.ReadAsStringAsync();
            //            if (responseBody == "All Good")
            //            {
            //                device.connectionFlag = true;
            //            }
            //            else if (responseBody == "Invalid Password")
            //            {
            //                device.connectionFlag = false;
            //                MessageBox.Show("Senha Inválida");
            //            }
            //        }
            //        else
            //            device.connectionFlag = false;
            //    }
            //    catch
            //    {
            //        device.connectionFlag = false;
            //    }

            //}

            //public async void ShutdownVm2(DeviceClass device)
            //{

            //    var x = new HttpClient();

            //    Dictionary<string, string> formData = new Dictionary<string, string>
            //    {
            //        {"password", this.communicationConfig.password },
            //        {"VmName", device.name }
            //    };

            //    try
            //    {
            //        var res = await x.PostAsync($"http://{this.communicationConfig.ip}:{this.communicationConfig.port}/shutdownVm", new FormUrlEncodedContent(formData));
            //        if (res.IsSuccessStatusCode)
            //        {
            //            string responseBody = await res.Content.ReadAsStringAsync();
            //            if (responseBody == "All Good")
            //            {
            //                connectionFlag = true;
            //            }
            //            else if (responseBody == "Invalid Password")
            //            {
            //                connectionFlag = false;
            //                MessageBox.Show("Senha Inválida");
            //            }
            //        }
            //        else
            //            connectionFlag = false;
            //    }
            //    catch
            //    {
            //        connectionFlag = false;
            //    }

            //}

            //public async void RebootVm2(DeviceClass device)
            //{
            //    var x = new HttpClient();

            //    Dictionary<string, string> formData = new Dictionary<string, string>
            //    {
            //        {"password", this.communicationConfig.password },
            //        {"VmName", device.name }
            //    };

            //    try
            //    {
            //        var res = await x.PostAsync($"http://{this.communicationConfig.ip}:{this.communicationConfig.port}/rebootVm", new FormUrlEncodedContent(formData));
            //        if (res.IsSuccessStatusCode)
            //        {
            //            string responseBody = await res.Content.ReadAsStringAsync();
            //            if (responseBody == "All Good")
            //            {
            //                connectionFlag = true;
            //            }
            //            else if (responseBody == "Invalid Password")
            //            {
            //                connectionFlag = false;
            //                MessageBox.Show("Senha Inválida");
            //            }
            //        }
            //        else
            //            connectionFlag = false;
            //    }
            //    catch
            //    {
            //        connectionFlag = false;
            //    }
            //}

            //public async void StartVm2(DeviceClass device)
            //{
            //    var x = new HttpClient();

            //    Dictionary<string, string> formData = new Dictionary<string, string>
            //    {
            //        {"password", this.communicationConfig.password },
            //        {"VmName", device.name }
            //    };

            //    try
            //    {
            //        var res = await x.PostAsync($"http://{this.communicationConfig.ip}:{this.communicationConfig.port}/startVm", new FormUrlEncodedContent(formData));
            //        if (res.IsSuccessStatusCode)
            //        {
            //            string responseBody = await res.Content.ReadAsStringAsync();
            //            if (responseBody == "All Good")
            //            {
            //                connectionFlag = true;
            //            }
            //            else if (responseBody == "Invalid Password")
            //            {
            //                connectionFlag = false;
            //                MessageBox.Show("Senha Inválida");
            //            }
            //        }
            //        else
            //            connectionFlag = false;
            //    }
            //    catch
            //    {
            //        connectionFlag = false;
            //    }
            //}
        }

        public class DeviceClass
        {
            public DeviceType type { get; set; }
            public DeviceState VmState { get; set; }
            public DeviceState ApiState { get; set; }
            public SocketConnection ApiCon { get; set; }
            public string name { get; set; }
            public string ip { get; set; }
            public long processors { get; set; }
            public long memory { get; set; }
            public int port { get; set; }
            public string password { get; set; }
        }
        
        public enum DeviceType
        {
            ied = 0,
            mu,
            None
        }
        
        public enum DeviceState
        {
            down =0,
            up,
        }

        public class CommunicationConfig
        {
            public bool status { get; set; }
            public string name { get; set; }
            public string password { get; set; }
            public string ip { get; set; }
            public int port { get; set; }

            public void setIpAddress(string ipAddress)
            {
                if (IPAddress.TryParse(ipAddress, out IPAddress parsedIpAddress))
                {
                    if (parsedIpAddress.ToString() == ipAddress)
                        this.ip = ipAddress;
                }
            }
            public void setPortNumber(string number)
            {
                if (int.TryParse(number, out int parsedNumber))
                {
                    this.port = parsedNumber;
                }
            }

        }

        public class SocketConnection
        {
            [JsonIgnore]
            public Socket clientSocket;
            public string serverIp;
            public int serverPort;
            public bool isConnected;

            public event EventHandler ConnectionLost;

            public SocketConnection()
            {
                this.serverIp = "0.0.0.0";
                this.serverPort = 8000;
                isConnected = false;
            }

            public void changeConProperties(string ip, int port)
            {
                var needDisconnect = false;
                if (this.serverIp != ip || this.serverPort != port) needDisconnect = true;

                this.serverIp = ip;
                this.serverPort = port;

                if (needDisconnect && isConnected) this.Disconnect();
            }

            public bool Connect()
            {
                try
                {
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    clientSocket.ReceiveTimeout = 2000;
                    clientSocket.Connect(serverIp, serverPort);
                    isConnected = true;
                    return true;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show($"Failed to connect: {ex.Message}");
                    isConnected = false;
                    return false;
                }
            }

            public void closeConnection()
            {
                if (isConnected)
                {
                    clientSocket.Close();
                    isConnected = false;
                }
            }

            private void HandleConnectionLost()
            {
                isConnected = false;
                ConnectionLost?.Invoke(this, EventArgs.Empty);
            }

            public void Disconnect()
            {
                if (isConnected)
                {
                    clientSocket.Close();
                    isConnected = false;
                }
            }

            public string SendData(string Entry, string Data)
            {
                if (!isConnected)
                    if (!Connect())
                    {
                        HandleConnectionLost();
                        return null;
                    }
                try
                {
                    Dictionary<string, string> data = new Dictionary<string, string>()
                    {
                        {"entry", Entry },
                        {"data", Data }
                    };

                    string jsonData = JsonConvert.SerializeObject(data);

                    byte[] yamlBytes = Encoding.ASCII.GetBytes(jsonData);
                    byte[] buffer = new byte[1024 * 8];

                    clientSocket.Send(yamlBytes);
                    int bytesRead = clientSocket.Receive(buffer);
                    if (bytesRead > 0)
                    {
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        return receivedData;
                    }
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    HandleConnectionLost();
                    return null;
                }
            }

        }

    }
}
