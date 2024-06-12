using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Quality
{
    public class Classes
    {
        public class vQualityControl
        {
            public ServerConfig serverConfig { get; set; }
            public SocketConnection socket { get; set; }
            public List<SampledValue> sampledValues { get; set; }
            
            public vQualityControl()
            {
                this.serverConfig = new ServerConfig();
                this.socket = new SocketConnection();
            }
            
            public void saveConfig()
            {
                try
                {
                    string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                    string filePath = "vQuality_data.dat";
                    File.WriteAllText(filePath, json);
                }
                catch(Exception)
                {
                    throw;
                }
            }

            public void loadConfig()
            {
                try
                {
                    string filePath = "vQuality_data.dat";
                    string fileData = File.ReadAllText(filePath);
                    vQualityControl control = JsonConvert.DeserializeObject<vQualityControl>(fileData);
                    this.serverConfig = control.serverConfig;
                    this.socket = control.socket;
                    this.sampledValues = control.sampledValues;
                }
                catch (Exception)
                {
                    this.serverConfig = new ServerConfig();
                    this.socket = new SocketConnection();
                    this.sampledValues = new List<SampledValue>();
                }
            }

            public string CreateYamlSampledValue()
            {
                if (sampledValues == null) return null;
                if (sampledValues.Count == 0) return null;
                YamlSampledValue.YamlRoot yamlRoot = new YamlSampledValue.YamlRoot();
                yamlRoot.SampledValues = new YamlSampledValue.SampledValue[sampledValues.Count];
                for (int i = 0; i < sampledValues.Count; i++)
                { 
                    yamlRoot.SampledValues[i] = new YamlSampledValue.SampledValue();
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
                    yamlRoot.SampledValues[i].macSrc = sampledValues[i].macDst;
                    yamlRoot.SampledValues[i].frequency = sampledValues[i].frequency;
                    yamlRoot.SampledValues[i].smpRate = sampledValues[i].smpRate;
                    yamlRoot.SampledValues[i].noAsdu = sampledValues[i].noAsdu;
                    yamlRoot.SampledValues[i].noChannels = sampledValues[i].noChannels;
                    yamlRoot.SampledValues[i].nominalCurrent = sampledValues[i].nominalCurrent;
                    yamlRoot.SampledValues[i].nominalVoltage = sampledValues[i].nominalVoltage;
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
                        macDst = item.macSrc,
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
            private bool _isConnected;
            public bool isConnected
            {
                get { return _isConnected; }
                set
                {
                    if (value != _isConnected)
                    {
                        if (value == false) HandleConnectionLost();
                        else HandleConnectionEstablished();
                        _isConnected = value;
                    }
                }
            }
            private Mutex _mutex;

            public event EventHandler ConnectionLost;
            public event EventHandler ConnectionEstablished;

            public enum entryType:byte
            {
                CAPT_ANY_SV = 0,
                CAPT_ESPECIFC_SV = 1,
                CAPT_SV_STOP = 2,
                CAPT_SV_STATUS = 3,
                CAPT_SV_DATA = 4,
                CAPT_SV_WAVEFORM = 5,
                ANALYSER_START = 6,
                ANALYSER_STOP = 7,
                ANALYSER_SETUP = 8,
                ANALYSER_STATUS = 9,
                ANALYSER_DATA = 10,
                ANALYSER_EVENT_INFO = 11,
                PRODIST_STATUS = 12,
                PRODIST_SETUP = 13,
                PRODIST_START = 14,
                PRODIST_STOP = 15,
                PRODIST_DATA = 16,
                PRODIST_INFO_DATA = 17,
                NONE = 255,
            }

            public SocketConnection()
            {
                this.serverIp = "0.0.0.0";
                this.serverPort = 8008;
                isConnected = false;
                _mutex = new Mutex();
            }

            public void IsSocketConnected()
            {
                try
                {
                    if (clientSocket == null) Connect();
                    if (clientSocket.Connected)
                    {
                        this.isConnected = !(clientSocket.Poll(1, SelectMode.SelectRead) && clientSocket.Available == 0);
                    }
                    else
                    {
                        this.isConnected = clientSocket.Connected;
                    }
                    
                }
                catch (SocketException) { this.isConnected = false; }
                catch (Exception) { this.isConnected = false; }
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
                ConnectionLost?.Invoke(this, EventArgs.Empty);
            }

            private void HandleConnectionEstablished()
            {
                ConnectionEstablished?.Invoke(this, EventArgs.Empty);
            }

            public void Disconnect()
            {
                if (isConnected)
                {
                    clientSocket.Close();
                    isConnected = false;
                }
            }

            public string? SendByte(entryType entry , byte[] Data)
            {
                IsSocketConnected();

                if (!isConnected)
                    if (!Connect())
                        return null;
                _mutex.WaitOne();
                try
                {
                    int dataLen = Data.Length;
                    byte[] dataLenBytes = BitConverter.GetBytes(dataLen);

                    int totalLength = dataLen + 5;

                    if (totalLength < 4096)
                    {
                        byte[] sendbuffer = new byte[totalLength];
                        sendbuffer[0] = (byte)entry;
                        dataLenBytes.CopyTo(sendbuffer, 1);
                        Data.CopyTo(sendbuffer, 5);

                        clientSocket.Send(sendbuffer, totalLength, SocketFlags.None);
                    }
                    else
                    {
                        int remainingLength = totalLength;
                        int offset = 0;
                        byte[] sendbuffer = new byte[4096];
                        sendbuffer[0] = (byte)entry;
                        dataLenBytes.CopyTo(sendbuffer, 1);
                        Array.Copy(Data, offset, sendbuffer, 5, Math.Min(4096 - (5), Data.Length - offset));
                        clientSocket.Send(sendbuffer, 4096, SocketFlags.None);

                        remainingLength -= 4096;
                        offset += 4096 - 5;

                        while (remainingLength > 0)
                        {
                            int chunkSize = Math.Min(remainingLength, 4096);
                            sendbuffer = new byte[chunkSize];
                            Array.Copy(Data, offset, sendbuffer, 0, chunkSize);
                            clientSocket.Send(sendbuffer, chunkSize, SocketFlags.None);
                            offset += chunkSize;
                            remainingLength -= chunkSize;
                        }
                    }

                    byte[] buffer = new byte[4096];
                    int bytesRead = clientSocket.Receive(buffer);
                    if (bytesRead > 0)
                    {
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        return receivedData;
                    }
                    else
                        return null;
                }
                catch (Exception)
                {
                    IsSocketConnected();
                    return null;
                }
                finally
                {
                    _mutex.ReleaseMutex();
                }
            }


            public string? SendData(entryType entry, string Data)
            {
                //try
                //{
                //    clientSocket.Shutdown(SocketShutdown.Receive);
                //}catch(Exception) { }
                
                IsSocketConnected();

                if (!isConnected)
                    if (!Connect()) 
                        return null;
                _mutex.WaitOne();
                try
                {
                    


                    int dataLen = Data.Length;
                    byte[] dataBytes = Encoding.ASCII.GetBytes(Data);
                    byte[] dataLenBytes = BitConverter.GetBytes(dataLen);


                    int totalLength = dataLen + 5;

                    if (totalLength < 4096)
                    {
                        byte[] sendbuffer = new byte[totalLength];
                        sendbuffer[0] = (byte)entry;
                        dataLenBytes.CopyTo(sendbuffer, 1);
                        dataBytes.CopyTo(sendbuffer, 5);

                        clientSocket.Send(sendbuffer, totalLength, SocketFlags.None);
                    }
                    else
                    {
                        int remainingLength = totalLength;
                        int offset = 0;
                        byte[] sendbuffer = new byte[4096];
                        sendbuffer[0] = (byte)entry;
                        dataLenBytes.CopyTo(sendbuffer, 1);
                        Array.Copy(dataBytes, offset, sendbuffer, 5, Math.Min(4096 - (5), dataBytes.Length - offset));
                        clientSocket.Send(sendbuffer, 4096, SocketFlags.None);

                        remainingLength -= 4096;
                        offset += 4096 - 5;

                        while (remainingLength > 0)
                        {
                            int chunkSize = Math.Min(remainingLength, 4096);
                            sendbuffer = new byte[chunkSize];
                            Array.Copy(dataBytes, offset, sendbuffer, 0, chunkSize);
                            clientSocket.Send(sendbuffer, chunkSize, SocketFlags.None);
                            offset += chunkSize;
                            remainingLength -= chunkSize;
                        }
                    }

                    byte[] buffer = new byte[4096];
                    int bytesRead = clientSocket.Receive(buffer);
                    if (bytesRead > 0)
                    {
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        return receivedData;
                    }
                    else
                        return null;
                }
                catch (Exception)
                {
                    IsSocketConnected();
                    return null;
                }
                finally
                {
                    _mutex.ReleaseMutex();
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

            public byte[]? ReceiveFile(entryType entry, string Data)
            {
                IsSocketConnected();

                if (!isConnected)
                    if (!Connect())
                        return null;
                _mutex.WaitOne();
                try
                { 

                    int dataLen = Data.Length;
                    byte[] dataBytes = Encoding.ASCII.GetBytes(Data);
                    byte[] dataLenBytes = BitConverter.GetBytes(dataLen);


                    int totalLength = dataLen + 5;

                    if (totalLength < 4096)
                    {
                        byte[] sendbuffer = new byte[totalLength];
                        sendbuffer[0] = (byte)entry;
                        dataLenBytes.CopyTo(sendbuffer, 1);
                        dataBytes.CopyTo(sendbuffer, 5);

                        clientSocket.Send(sendbuffer, totalLength, SocketFlags.None);
                    }
                    else
                    {
                        int remainingLength = totalLength;
                        int offset = 0;
                        byte[] sendbuffer = new byte[4096];
                        sendbuffer[0] = (byte)entry;
                        dataLenBytes.CopyTo(sendbuffer, 1);
                        Array.Copy(dataBytes, offset, sendbuffer, 5, Math.Min(4096 - (5), dataBytes.Length - offset));
                        clientSocket.Send(sendbuffer, 4096, SocketFlags.None);

                        remainingLength -= 4096;
                        offset += 4096 - 5;

                        while (remainingLength > 0)
                        {
                            int chunkSize = Math.Min(remainingLength, 4096);
                            sendbuffer = new byte[chunkSize];
                            Array.Copy(dataBytes, offset, sendbuffer, 0, chunkSize);
                            clientSocket.Send(sendbuffer, chunkSize, SocketFlags.None);
                            offset += chunkSize;
                            remainingLength -= chunkSize;
                        }
                    }

                    // Receive the length of the file data (4 bytes)
                    byte[] lengthBytes = new byte[4];
                    clientSocket.Receive(lengthBytes);
                    int fileLength = BitConverter.ToInt32(lengthBytes, 0);

                    // Receive the file data
                    byte[] fileData = new byte[fileLength];
                    int totalBytesReceived = 0;
                    while (totalBytesReceived < fileLength)
                    {
                        int bytesReceived = clientSocket.Receive(fileData, totalBytesReceived, fileLength - totalBytesReceived, SocketFlags.None);
                        if (bytesReceived == 0)
                            throw new Exception("Connection closed prematurely while receiving file data.");
                        totalBytesReceived += bytesReceived;
                    }
                    return fileData;
                }
                catch (Exception ex)
                {
                    // Handle exception
                    return null;
                }
                finally
                {
                    _mutex.ReleaseMutex();
                }
            }
        }

        public class SampledValue
        {
            public string SVID { get; set; }
            public string macDst { get; set; }
            public string macSrc { get; set; }
            public int frequency { get; set; }
            public int smpRate { get; set; }
            public int noAsdu { get; set; }
            public int noChannels { get; set; }
            public bool running { get; set; }
            public int vLANID { get; set; }
            public int vLANPriority { get; set; }
            public int nominalVoltage { get; set; }
            public int nominalCurrent { get; set; }    
            public GeneralInfo generalInfo { get; set; }
            public QualityEvent sag { get; set; }
            public QualityEvent swell { get; set; }
            public QualityEvent interruption { get; set; }
            public QualityEvent overVoltage { get; set; }
            public QualityEvent underVoltage { get; set; }
            public QualityEvent sustainedinterruption { get; set; }
            public ProdistInfo prodistInfo { get; set; }

            public SampledValue()
            {
                this.generalInfo = new GeneralInfo();
                this.sag = new QualityEvent()
                {
                    bottomThreshold = 0.1,
                    topThreshold = 0.9,
                    minDuration = 0.01,
                    maxDuration = 60
                };
                this.swell = new QualityEvent()
                {
                    bottomThreshold = 1.1,
                    topThreshold = 1.9,
                    minDuration = 0.01,
                    maxDuration = 60
                };
                this.interruption = new QualityEvent()
                {
                    bottomThreshold = 0,
                    topThreshold = 0.1,
                    minDuration = 0.01,
                    maxDuration = 60
                };
                this.overVoltage = new QualityEvent()
                {
                    bottomThreshold = 1.1,
                    topThreshold = 1.5,
                    minDuration = 60,
                    maxDuration = 180
                };
                this.underVoltage = new QualityEvent()
                {
                    bottomThreshold = 0.1,
                    topThreshold = 0.9,
                    minDuration = 60,
                    maxDuration = 180
                };
                this.sustainedinterruption = new QualityEvent()
                {
                    bottomThreshold = 0,
                    topThreshold = 0.1,
                    minDuration = 60,
                    maxDuration = 180
                };
                this.prodistInfo = new ProdistInfo();
            }

            public class GeneralInfo()
            {
                public double[][][] PhasorPolar { get; set; }
                public double[] Rms { get; set; }
                public double[][][] Symmetrical { get; set; }
                public double[] Unbalance { get; set; }
                public double[] power { get; set; }
                public double Frequency { get; set; }
            }
            
            public class JsonServerRegistedEvents
            {
                public string Type { get; set; }
                public string Duration { get; set; }
                public string MinValue { get; set; }
                public string MaxValue { get; set; }
                public string Name { get; set; }
                public string Date { get; set; }
            }

            public class QualityEvent
            {
                public bool flag;

                public double topThreshold { get; set; }
                public double bottomThreshold { get; set; }
                public double minDuration { get; set; }
                public double maxDuration { get; set; }

                public List<RegisteredEvents> RegisteredEvents { get; set; }

                public QualityEvent()
                {
                    this.flag = false;
                    this.topThreshold = 0;
                    this.bottomThreshold = 0;
                    this.minDuration = 0;
                    this.maxDuration = 0;
                    this.RegisteredEvents = new List<RegisteredEvents>();
                }
            }

            public class RegisteredEvents
            {
                public DateTime date { get; set; }
                public string name { get; set; }
                public double magnitude { get; set; }
                public double duration { get; set; }
                public EventTypes eventType { get; set; }
            }
            
            public enum EventTypes
            {
                sag,
                swell,
                interruption,
                overVoltage,
                underVoltage,
                sustainedinterruption
            }
        
            public class ProdistInfo
            {
                public double[] precLims;
                public double[] critLims;
                public double[] fpLims;

                public ProdistInfo()
                {
                    precLims = new double[2];
                    critLims = new double[2];
                    fpLims = new double[2];
                }

                public void setNormalLimist(double nomVoltage)
                {
                    if (nomVoltage > 230e3)
                    {
                        //0.93 | 0.95 | 1.05 | 1.07
                        critLims[0] = 0.93 * nomVoltage;
                        precLims[0] = 0.95 * nomVoltage;
                        precLims[1] = 1.05 * nomVoltage;
                        critLims[1] = 1.07 * nomVoltage;
                    }else if (nomVoltage > 69e3)
                    {
                        // 0.90 | 0.95 | 1.05 | 1.07
                        critLims[0] = 0.90 * nomVoltage;
                        precLims[0] = 0.95 * nomVoltage;
                        precLims[1] = 1.05 * nomVoltage;
                        critLims[1] = 1.07 * nomVoltage;
                    }else if (nomVoltage > 2.3e3)
                    {
                        // 0.9 | 0.93 | 1.06 | 1.05
                        critLims[0] = 0.9 * nomVoltage;
                        precLims[0] = 0.93 * nomVoltage;
                        precLims[1] = 1.06 * nomVoltage;
                        critLims[1] = 1.05 * nomVoltage;
                    }else
                    {
                        // 0.9 | 0.95 | 1.05 | 1.1
                        critLims[0] = 0.9 * nomVoltage;
                        precLims[0] = 0.95 * nomVoltage;
                        precLims[1] = 1.05 * nomVoltage;
                        critLims[1] = 1.1 * nomVoltage;
                    }
                    fpLims[0] = 0.92;
                    fpLims[1] = 0.92;
                }
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
                public int nominalVoltage { get; set; }
                public int nominalCurrent { get; set; }
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

        public class YamlSnifferData
        {
            public class YamlRoot
            {
                public int nSV { get; set; }
                public List<SvData> svData { get; set; }
            }
            public class SvData
            {
                public string svID { get; set; }
                public double MeanTime { get; set; }
                public int nPackets { get; set; }
                public int nAsdu { get; set; }
                public string macSrc { get; set; }
                public string macDst { get; set; }
                public int vLanId { get; set; }
                public int vLanPriority { get; set; }
                public int appID { get; set; }
                public int nChannels { get; set; }
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

        public static bool validateDouble(string doubleString, double? min = null, double? max = null)
        {
            double parsedDouble;
            if (double.TryParse(doubleString, out parsedDouble))
            {
                if ((min == null || parsedDouble >= min) && (max == null || parsedDouble <= max))
                    return true;
            }
            return false;
        }

        public static int changeInt(string intString, int ExtValue, int? min = null, int? max = null)
        {
            int parsedInt;
            if (int.TryParse(intString, out parsedInt))
            {
                if ((min == null || parsedInt >= min) && (max == null || parsedInt <= max))
                    return parsedInt;
            }
            return ExtValue;
        }

        public static double changeDouble(string doubleString, double ExtValue, double? min = null, double? max = null)
        {
            double parsedDouble;
            if (double.TryParse(doubleString, out parsedDouble))
            {
                if ((min == null || parsedDouble >= min) && (max == null || parsedDouble <= max))
                    return parsedDouble;
            }
            return ExtValue;
        }

    }
}
