using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Quality
{
    public class Classes
    {
        public class vQualityControl
        {
            public ServerConfig serverConfig { get; set; }
            public SocketConnection socketConnection { get; set; }
            public List<SampledValue> sampledValues { get; set; }
            
            public vQualityControl()
            {
                this.serverConfig = new ServerConfig();
                this.socketConnection = new SocketConnection();
            }
            
            public string CreateYamlSampledValue()
            {
                if (sampledValues == null) return null;
                if (sampledValues.Count == 0) return null;
                YamlSampledValue.YamlRoot yamlRoot = new YamlSampledValue.YamlRoot();
                yamlRoot.SampledValues = new YamlSampledValue.SampledValue[sampledValues.Count];
                for(int i=0;i< sampledValues.Count; i++)
                {
                    yamlRoot.SampledValues[i].overVoltage = new YamlSampledValue.QualityEvent()
                    {
                        bottomThreshold = sampledValues[i].overVoltage.bottomThreshold,
                        topThreshold = sampledValues[i].overVoltage.topThreshold,
                        maxDuration = sampledValues[i].overVoltage.maxDuration,
                        minDuration = sampledValues[i].overVoltage.minDuration
                    };
                    yamlRoot.SampledValues[i].underVoltage = new YamlSampledValue.QualityEvent()
                    {
                        bottomThreshold = sampledValues[i].underVoltage.bottomThreshold,
                        topThreshold = sampledValues[i].underVoltage.topThreshold,
                        maxDuration = sampledValues[i].underVoltage.maxDuration,
                        minDuration = sampledValues[i].underVoltage.minDuration
                    };
                    yamlRoot.SampledValues[i].sag = new YamlSampledValue.QualityEvent()
                    {
                        bottomThreshold = sampledValues[i].sag.bottomThreshold,
                        topThreshold = sampledValues[i].sag.topThreshold,
                        maxDuration = sampledValues[i].sag.maxDuration,
                        minDuration = sampledValues[i].sag.minDuration
                    };
                    yamlRoot.SampledValues[i].swell = new YamlSampledValue.QualityEvent()
                    {
                        bottomThreshold = sampledValues[i].swell.bottomThreshold,
                        topThreshold = sampledValues[i].swell.topThreshold,
                        maxDuration = sampledValues[i].swell.maxDuration,
                        minDuration = sampledValues[i].swell.minDuration
                    };
                    yamlRoot.SampledValues[i].interruption = new YamlSampledValue.QualityEvent()
                    {
                        bottomThreshold = sampledValues[i].interruption.bottomThreshold,
                        topThreshold = sampledValues[i].interruption.topThreshold,
                        maxDuration = sampledValues[i].interruption.maxDuration,
                        minDuration = sampledValues[i].interruption.minDuration
                    };
                    yamlRoot.SampledValues[i].sustainedinterruption = new YamlSampledValue.QualityEvent()
                    {
                        bottomThreshold = sampledValues[i].sustainedinterruption.bottomThreshold,
                        topThreshold = sampledValues[i].sustainedinterruption.topThreshold,
                        maxDuration = sampledValues[i].sustainedinterruption.maxDuration,
                        minDuration = sampledValues[i].sustainedinterruption.minDuration
                    };
                    yamlRoot.SampledValues[i].SVID = sampledValues[i].SVID;
                    yamlRoot.SampledValues[i].macSrc = sampledValues[i].macSrc;
                    yamlRoot.SampledValues[i].frequency = sampledValues[i].frequency;
                    yamlRoot.SampledValues[i].smpRate = sampledValues[i].smpRate;
                    yamlRoot.SampledValues[i].noAsdu = sampledValues[i].noAsdu;
                    yamlRoot.SampledValues[i].noChannels = sampledValues[i].noChannels;
                }

                var serializer = new SerializerBuilder().Build();
                string yaml = serializer.Serialize(yamlRoot);
                return yaml;
            }
            
            public List<SampledValue> loadYamlSampledValue(string fileData)
            {
                List<SampledValue> svList = new List<SampledValue>();
                var deserializer = new DeserializerBuilder().Build();
                var yamlRoot = deserializer.Deserialize<YamlSampledValue.YamlRoot>(fileData);
                if (yamlRoot.SampledValues == null) return null;
                if (yamlRoot.SampledValues.Length == 0) return null;
                sampledValues = new List<SampledValue>();
                foreach (var item in yamlRoot.SampledValues)
                {
                    SampledValue sv = new SampledValue()
                    {
                        SVID = item.SVID,
                        macSrc = item.macSrc,
                        frequency = item.frequency,
                        smpRate = item.smpRate,
                        noAsdu = item.noAsdu,
                        noChannels = item.noChannels,
                        sag = new SampledValue.QualityEvent()
                        {
                            bottomThreshold = item.sag.bottomThreshold,
                            topThreshold = item.sag.topThreshold,
                            maxDuration = item.sag.maxDuration,
                            minDuration = item.sag.minDuration
                        },
                        swell = new SampledValue.QualityEvent()
                        {
                            bottomThreshold = item.swell.bottomThreshold,
                            topThreshold = item.swell.topThreshold,
                            maxDuration = item.swell.maxDuration,
                            minDuration = item.swell.minDuration
                        },
                        interruption = new SampledValue.QualityEvent()
                        {
                            bottomThreshold = item.interruption.bottomThreshold,
                            topThreshold = item.interruption.topThreshold,
                            maxDuration = item.interruption.maxDuration,
                            minDuration = item.interruption.minDuration
                        },
                        overVoltage = new SampledValue.QualityEvent()
                        {
                            bottomThreshold = item.overVoltage.bottomThreshold,
                            topThreshold = item.overVoltage.topThreshold,
                            maxDuration = item.overVoltage.maxDuration,
                            minDuration = item.overVoltage.minDuration
                        },
                        underVoltage = new SampledValue.QualityEvent()
                        {
                            bottomThreshold = item.underVoltage.bottomThreshold,
                            topThreshold = item.underVoltage.topThreshold,
                            maxDuration = item.underVoltage.maxDuration,
                            minDuration = item.underVoltage.minDuration
                        },
                        sustainedinterruption = new SampledValue.QualityEvent()
                        {
                            bottomThreshold = item.sustainedinterruption.bottomThreshold,
                            topThreshold = item.sustainedinterruption.topThreshold,
                            maxDuration = item.sustainedinterruption.maxDuration,
                            minDuration = item.sustainedinterruption.minDuration
                        },
                    };
                    svList.Add(sv);
                }
                return svList;
            }
        }

        public class ServerConfig
        {
            public string _ipAddress;
            public string ipAddress
            {
                set { if (validateIP(value)) _ipAddress = value; }
                get { return _ipAddress;}
            }
            public int _port;
            public string port
            {
                set { if (validateInt(value, 1, 65535)) _port = int.Parse(value); }
                get { return _port.ToString(); }
            }
            public string user { get; set; }
            public string password { get; set; }

            public ServerConfig()
            {
                this._ipAddress = "0.0.0.0";
                this._port = 8000;
                this.user = "";
                this.password = "";
            }

        }

        public class SocketConnection
        {
            private Socket clientSocket;
            public string serverIp;
            public int serverPort;
            private bool isConnected;

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

            public string SendFile(string filePath)
            {
                // Load the file to send
                byte[] fileData = File.ReadAllBytes(filePath);
                string fileName = Path.GetFileName(filePath);

                byte[] buffer = new byte[1024 * 8];
                // Send the file name and length
                byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);
                byte[] fileNameLengthBytes = BitConverter.GetBytes(fileNameBytes.Length);
                byte[] fileLengthBytes = BitConverter.GetBytes((long)fileData.Length);
                clientSocket.Send(fileNameLengthBytes);
                clientSocket.Send(fileNameBytes);
                clientSocket.Send(fileLengthBytes);
                clientSocket.Send(fileData);

                clientSocket.ReceiveTimeout = 2000;
                int bytesRead = clientSocket.Receive(buffer);
                if (bytesRead > 0)
                {
                    string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    return receivedData;
                }
                else
                    return null;
            }
        }

        public class SampledValue
        {
            public string SVID { get; set; }
            public string macSrc { get; set; }
            public int frequency { get; set; }
            public int smpRate { get; set; }
            public int noAsdu { get; set; }
            public int noChannels { get; set; }
            public QualityEvent sag { get; set; }
            public QualityEvent swell { get; set; }
            public QualityEvent interruption { get; set; }
            public QualityEvent overVoltage { get; set; }
            public QualityEvent underVoltage { get; set; }
            public QualityEvent sustainedinterruption { get; set; }
            public class QualityEvent
            {
                public bool flag;
                public double topThreshold { get; set; }
                public double bottomThreshold { get; set; }
                public double minDuration { get; set; }
                public double maxDuration { get; set; }
            }
        }

        public class YamlSampledValue
        {
            public class YamlRoot
            {
                public SampledValue[] SampledValues { get; set; }
            }
            public class SampledValue
            {
                public string SVID { get; set; }
                public string macSrc { get; set; }
                public int frequency { get; set; }
                public int smpRate { get; set; }
                public int noAsdu { get; set; }
                public int noChannels { get; set; }
                public QualityEvent sag { get; set; }
                public QualityEvent swell { get; set; }
                public QualityEvent interruption { get; set; }
                public QualityEvent overVoltage { get; set; }
                public QualityEvent underVoltage { get; set; }
                public QualityEvent sustainedinterruption { get; set; }
            }
            public class QualityEvent
            {
                public double topThreshold { get; set; }
                public double bottomThreshold { get; set; }
                public double minDuration { get; set; }
                public double maxDuration { get; set; }
            }
        }

        public static bool validateIP(string ipString)
        {
            IPAddress ipAddress;
            if (IPAddress.TryParse(ipString, out ipAddress))
            {
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return true;
            }
            return false;
        }

        public static bool validateInt(string intString, int? min = null, int? max = null)
        {
            int parsedInt;
            if (int.TryParse(intString, out parsedInt))
            {
                if ((min == null || parsedInt >= min) && (max == null || parsedInt <= max))
                    return true;
            }
            return false;
        }

    }
}
