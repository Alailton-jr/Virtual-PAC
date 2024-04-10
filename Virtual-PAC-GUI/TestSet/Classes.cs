using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace TestSet
{
    
    public class YamlSequencer
    {

        public class Values
        {
            public double angle { get; set; }
            public double module { get; set; }
        }

        public class TestSequence
        {
            public double duration { get; set; }
            public string type { get; set; }
            public List<Values> values { get; set; }
        }

        public class YamlRoot
        {
            public TestSequence[][] Test { get; set; }
            public YamlNetwork.YamlRoot[] Network { get; set; }
            public int numSV { get; set; }
        }

    }

    public class YamlContinuous
    {
        public class TestClass
        {
            public List<ValuesClass> values { get; set; }
        }
        public class ValuesClass
        {
            public double angle { get; set; }
            public double module { get; set; }
        }
        public class YamlRoot
        {
            public TestClass[] Test { get; set; }
            public YamlNetwork.YamlRoot[] Network { get;set; }
            public int numSV { get; set; }
        }
    }

    public class YamlTransient
    {
        public class YamlRoot
        {
            [YamlDotNet.Serialization.YamlMember(Alias = "Number of Files")]
            public int nFiles { get; set; }
            [YamlDotNet.Serialization.YamlMember(Alias = "Number of Channels")]
            public List<int> nChannels { get; set; }
            public YamlNetwork.YamlRoot[] Network { get; set; }
            public List<string> Files { get; set; }
            [YamlDotNet.Serialization.YamlMember(Alias = "GOOSE STOP")]
            public int GooseStop { get; set; }
            public List<List<List<int>>> Channels;
        }
    }
    
    public class YamlNetwork
    {
        public class SampledValue
        {
            public int pps { get; set; }
            public int AppId { get; set; }
            public string macDst { get; set; }
            public string macSrc { get; set; }
            public int vLanID { get; set; }
            public int vLanPriority { get; set; }
            public int smpRate { get; set; }
            public string svId { get; set; }
            public int confRev { get; set; }
            public int smpSync { get; set; }
            public int noAsdu { get; set; }
            public int frequency { get; set; }
        }

        public class GOOSE
        {
            public string goId { get; set; }
            public uint dataSize { get; set; }
            public string controlRef { get; set; }
            public int vLan { get; set; }
            public int appId { get; set; }
            public string macSrc { get; set; }
            public int confRev { get; set; }
        }


        public class YamlRoot
        {
            public SampledValue SvNetwork { get; set; }
            public GOOSE GoNetwork { get; set; }
        }
    }

    public class Ctl
    {
        public int numSV;
        public List<NetworkConfig> networkConfig;
        public CommunicationConfig communicationConfig;
        public List<ContinuousConfig> continuousConfig;
        public List<List<SequenceConfig>> sequencesConfig;
        public List<TransientConfig> transientConfig;
        public GeneralConfig generalConfig;
        public bool connectionFlag = false;
        public TestRun testRun;
        public SocketConnection serverCon { get; set; }

        public void saveClass()
        {
            //this.sclConf = new SCLConfig();
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            string filePath = "vMU_data.dat";
            File.WriteAllText(filePath, json);
        }

        public bool loadClass()
        {
            try
            {
                if (File.Exists("vMU_data.dat"))
                {
                    string loadedJson = File.ReadAllText("vMU_data.dat");
                    var tempData = JsonConvert.DeserializeObject<Ctl>(loadedJson);
                    this.networkConfig = tempData.networkConfig;
                    this.communicationConfig = tempData.communicationConfig;
                    this.continuousConfig = tempData.continuousConfig;
                    this.sequencesConfig = tempData.sequencesConfig;
                    this.testRun = tempData.testRun;
                    this.serverCon = tempData.serverCon;
                    this.generalConfig = tempData.generalConfig;
                    this.serverCon = tempData.serverCon;
                    this.transientConfig = tempData.transientConfig;
                    this.numSV = tempData.numSV;

                    if (this.transientConfig != null)
                    foreach(var config in this.transientConfig)
                    {
                        if (!String.IsNullOrEmpty(config.fileName))
                        {
                            if(!config.LoadDataFromFile(config.fileName))
                            {
                                config.fileName = "";
                            }
                        }
                    }
                }
                else
                {
                    this.numSV = 1;
                    this.networkConfig = new List<NetworkConfig>();
                    this.communicationConfig = new CommunicationConfig();
                    this.continuousConfig = new List<ContinuousConfig>();
                    this.sequencesConfig = new List<List<SequenceConfig>>() { SequenceConfig.defaultSequences() };
                    this.testRun = new TestRun();
                    this.serverCon = new SocketConnection();
                    this.generalConfig = new GeneralConfig();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string CreateYamlNetwork()
        {
            // Needs Fixing

            //var rootObject = new YamlNetwork.YamlRoot();
            //rootObject.SvNetwork = new YamlNetwork.SampledValue()
            //{
            //    pps = networkConfig.svConfig.frequency * 80,
            //    AppId = networkConfig.svConfig.appId,
            //    macDst = networkConfig.svConfig.macDest,
            //    vlan = networkConfig.svConfig.vLan,
            //    svId = networkConfig.svConfig.svId,
            //    confRev = networkConfig.svConfig.confRev,
            //    smpSync = 0,
            //    noAsdu = networkConfig.svConfig.noAsdu
            //};
            //rootObject.GoNetwork = new YamlNetwork.GOOSE()
            //{
            //    goId = networkConfig.goConfig.goId,
            //    controlRef = networkConfig.goConfig.controlRef,
            //    vLan = networkConfig.goConfig.vLan,
            //    appId = networkConfig.goConfig.appId,
            //    macSrc = networkConfig.goConfig.macSrc,
            //    confRev = networkConfig.goConfig.confRev
            //};
            //rootObject.general = new YamlNetwork.General()
            //{
            //    ipAddress = generalConfig.ip,
            //    port = generalConfig.port
            //};
            //var serializer = new SerializerBuilder().Build();
            //string yaml = serializer.Serialize(rootObject);
            //return yaml;
            return null;
        }

        public string CreateYamlSequencer()
        {
            var rootObject = new YamlSequencer.YamlRoot()
            {
                Test = new YamlSequencer.TestSequence[this.numSV][],
                Network = new YamlNetwork.YamlRoot[this.numSV],
                numSV = this.numSV
            };

            for (int nSV = 0; nSV < this.numSV; nSV++)
            {
                rootObject.Test[nSV] = new YamlSequencer.TestSequence[sequencesConfig[nSV].Count];
                for (int i = 0; i < sequencesConfig[nSV].Count; i++)
                {
                    rootObject.Test[nSV][i] = new YamlSequencer.TestSequence
                    {
                        duration = sequencesConfig[nSV][i].time,
                        type = "Iabc",
                        values = new List<YamlSequencer.Values>()
                    };
                    for (int j = 0; j < sequencesConfig[nSV][i].data.Count; j++)
                    {
                        rootObject.Test[nSV][i].values.Add(new YamlSequencer.Values
                        {
                            angle = sequencesConfig[nSV][i].data[j].angle,
                            module = sequencesConfig[nSV][i].data[j].module,
                        });
                    }
                };

                rootObject.Network[nSV] = new YamlNetwork.YamlRoot()
                {
                    SvNetwork = new YamlNetwork.SampledValue()
                    {
                        pps = networkConfig[nSV].svConfig.frequency * 80,
                        AppId = networkConfig[nSV].svConfig.appId,
                        macDst = networkConfig[nSV].svConfig.macSrc,
                        vLanID = networkConfig[nSV].svConfig.vLan,
                        svId = networkConfig[nSV].svConfig.svId,
                        confRev = networkConfig[nSV].svConfig.confRev,
                        smpRate = networkConfig[nSV].svConfig.smpRate,
                        vLanPriority = networkConfig[nSV].svConfig.vLanPriority,
                        smpSync = 0,
                        noAsdu = networkConfig[nSV].svConfig.noAsdu,
                        frequency = networkConfig[nSV].svConfig.frequency
                    },
                    GoNetwork = new YamlNetwork.GOOSE()
                    {
                        goId = networkConfig[nSV].goConfig.goId,
                        controlRef = networkConfig[nSV].goConfig.controlRef,
                        vLan = networkConfig[nSV].goConfig.vLan,
                        appId = networkConfig[nSV].goConfig.appId,
                        macSrc = networkConfig[nSV].goConfig.macSrc,
                        confRev = networkConfig[nSV].goConfig.confRev
                    }
                };

            }
            var serializer = new SerializerBuilder().Build();
            string yaml = serializer.Serialize(rootObject);
            return yaml;
        }

        public string CreateYamlTransient()
        {

            var rootObject = new YamlTransient.YamlRoot()
            {
                nFiles = transientConfig.Count,
                nChannels = new List<int>(),
                Network = new YamlNetwork.YamlRoot[transientConfig.Count],
                Files = new List<string>(),
                GooseStop = 0,
                Channels = new List<List<List<int>>>()
            };
            for (int i = 0; i < this.transientConfig.Count; i++)
            {
                rootObject.nChannels.Add(8);
                rootObject.Files.Add(Path.GetFileName(transientConfig[i].fileName));
                rootObject.Network[i] = new YamlNetwork.YamlRoot()
                {
                    SvNetwork = new YamlNetwork.SampledValue()
                    {
                        pps = networkConfig[i].svConfig.frequency * 80,
                        AppId = networkConfig[i].svConfig.appId,
                        macDst = networkConfig[i].svConfig.macSrc,
                        vLanID = networkConfig[i].svConfig.vLan,
                        svId = networkConfig[i].svConfig.svId,
                        confRev = networkConfig[i].svConfig.confRev,
                        smpRate = networkConfig[i].svConfig.smpRate,
                        vLanPriority = networkConfig[i].svConfig.vLanPriority,
                        smpSync = 0,
                        noAsdu = networkConfig[i].svConfig.noAsdu,
                        frequency = networkConfig[i].svConfig.frequency
                    },
                    GoNetwork = new YamlNetwork.GOOSE()
                    {
                        goId = networkConfig[i].goConfig.goId,
                        controlRef = networkConfig[i].goConfig.controlRef,
                        vLan = networkConfig[i].goConfig.vLan,
                        appId = networkConfig[i].goConfig.appId,
                        macSrc = networkConfig[i].goConfig.macSrc,
                        confRev = networkConfig[i].goConfig.confRev
                    }
                };
                var channel = new List<List<int>>();
                for (int j = 0; j < 8; j++)
                {
                    if (transientConfig[i].setup[j] != -1)
                        channel.Add(new List<int>() { j, transientConfig[i].setup[j] });
                }
                rootObject.Channels.Add(channel);
            }
            var serializer = new SerializerBuilder().Build();
            string yaml = serializer.Serialize(rootObject);
            return yaml;
        }

        public string CreateYamlContinuous()
        {
            var rooObject = new YamlContinuous.YamlRoot()
            {
                Test = new YamlContinuous.TestClass[this.numSV],
                Network = new YamlNetwork.YamlRoot[this.numSV],
                numSV = this.numSV
            };
            for (int nSV = 0; nSV < this.numSV; nSV++)
            {
                rooObject.Test[nSV] = new YamlContinuous.TestClass()
                {
                    values = new List<YamlContinuous.ValuesClass>()
                };
                for (int i = 0; i < continuousConfig[nSV].data.Count; i++)
                {
                    rooObject.Test[nSV].values.Add(new YamlContinuous.ValuesClass()
                    {
                        angle = continuousConfig[nSV].data[i].Ang,
                        module = continuousConfig[nSV].data[i].Mod
                    });
                }
                rooObject.Network[nSV] = new YamlNetwork.YamlRoot()
                {
                    SvNetwork = new YamlNetwork.SampledValue()
                    {
                        pps = networkConfig[nSV].svConfig.frequency * 80,
                        AppId = networkConfig[nSV].svConfig.appId,
                        macDst = networkConfig[nSV].svConfig.macSrc,
                        vLanID = networkConfig[nSV].svConfig.vLan,
                        svId = networkConfig[nSV].svConfig.svId,
                        confRev = networkConfig[nSV].svConfig.confRev,
                        smpRate = networkConfig[nSV].svConfig.smpRate,
                        vLanPriority = networkConfig[nSV].svConfig.vLanPriority,
                        smpSync = 0,
                        noAsdu = networkConfig[nSV].svConfig.noAsdu,
                        frequency = networkConfig[nSV].svConfig.frequency
                    },
                    GoNetwork = new YamlNetwork.GOOSE()
                    {
                        goId = networkConfig[nSV].goConfig.goId,
                        controlRef = networkConfig[nSV].goConfig.controlRef,
                        vLan = networkConfig[nSV].goConfig.vLan,
                        appId = networkConfig[nSV].goConfig.appId,
                        macSrc = networkConfig[nSV].goConfig.macSrc,
                        confRev = networkConfig[nSV].goConfig.confRev
                    }
                };
            }
            var serializer = new SerializerBuilder().Build();
            string yaml = serializer.Serialize(rooObject);
            return yaml;
        }

        public void loadYamlNetwork(string yamlData)
        {
            // Needs Fixing

            //YamlNetwork.YamlRoot yamlFile;
            //var deserializer = new DeserializerBuilder().Build();
            //yamlFile = deserializer.Deserialize<YamlNetwork.YamlRoot>(yamlData);

            //this.generalConfig.ip = yamlFile.general.ipAddress;
            //this.generalConfig.port = yamlFile.general.port;

            //this.networkConfig.svConfig.frequency = yamlFile.SvNetwork.pps / 80;
            //this.networkConfig.svConfig.appId = yamlFile.SvNetwork.AppId;
            //this.networkConfig.svConfig.macDest = yamlFile.SvNetwork.macDst;
            //this.networkConfig.svConfig.vLan = yamlFile.SvNetwork.vlan;
            //this.networkConfig.svConfig.svId = yamlFile.SvNetwork.svId;
            //this.networkConfig.svConfig.confRev = yamlFile.SvNetwork.confRev;
            //this.networkConfig.svConfig.noAsdu = yamlFile.SvNetwork.noAsdu;

            //this.networkConfig.goConfig.goId = yamlFile.GoNetwork.goId;
            //this.networkConfig.goConfig.controlRef = yamlFile.GoNetwork.controlRef;
            //this.networkConfig.goConfig.vLan = yamlFile.GoNetwork.vLan;
            //this.networkConfig.goConfig.appId = yamlFile.GoNetwork.appId;
            //this.networkConfig.goConfig.macSrc = yamlFile.GoNetwork.macSrc;
            //this.networkConfig.goConfig.confRev = yamlFile.GoNetwork.confRev;

        }

        public void loadYamlSequencer(string yamlData)
        {
            //needs fixing

            //YamlSequencer.YamlRoot yamlFile;
            //var deserializer = new DeserializerBuilder().Build();
            //yamlFile = deserializer.Deserialize<YamlSequencer.YamlRoot>(yamlData);

            //this.sequencesConfig = new List<SequenceConfig>();
            //for(int i = 0; i < yamlFile.Test.Length; i++)
            //{
            //    SequenceConfig sequence = new SequenceConfig(
            //         "Sequence " + (i + 1).ToString(),
            //         yamlFile.Test[i].duration
            //    );

            //    sequence.data = new List<SequenceConfig.variable>();
            //    for (int j = 0; j < yamlFile.Test[i].values.Count; j++)
            //    {
            //        sequence.data.Add(new SequenceConfig.variable(null, yamlFile.Test[i].values[j].module, yamlFile.Test[i].values[j].angle));
            //    }
            //    this.sequencesConfig.Add(sequence);
            //}
        }

        public void loadYamlContinuous(string yamlData)
        {
            // needs fixing


            //YamlContinuous.YamlRoot yamlFile;
            //var deserializer = new DeserializerBuilder().Build();
            //yamlFile = deserializer.Deserialize<YamlContinuous.YamlRoot>(yamlData);

            //for (int j = 0; j < yamlFile.Test.values.Count; j++)
            //{
            //    this.continuousConfig.data[j].Mod = yamlFile.Test.values[j].module;
            //    this.continuousConfig.data[j].Ang = yamlFile.Test.values[j].angle;
            //}
            return;
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

    public class TestRun
    {
        public double startedTime { get; set; }
        public double endedTime { get; set; }
        public double tripTime { get; set; }
        public double supposedTripTime { get; set; }
        public double currentTime { get; set; }
        public double delay { get; set; }
        public List<List<double>> input { get; set; }
    }

    public class GeneralConfig
    {
        public string ip { get; set; }
        public int port { get; set; }
    }

    public class NetworkConfig
    {

        public SVConfig svConfig;
        public GOConfig goConfig;

        public class SVConfig
        {
            public int frequency { get; set; }
            public string svId { get; set; }
            public int vLan { get; set; }
            public int vLanPriority { get; set; }
            public int appId { get; set; }
            public int noAsdu { get; set; }
            public string macSrc { get; set; }
            public int confRev { get; set; }

            public int smpRate { get; set; }

            public SVConfig(int idx)
            {
                this.appId = 4800;
                this.confRev = 1;
                this.frequency = 60;
                this.macSrc = $"01:0C:CD:01:00:0{idx}";
                this.noAsdu = 1;
                this.svId = "SV_" + idx.ToString("0");
                this.vLan = 100;
                this.vLanPriority = 4;
                this.smpRate = 80;
            }

        }
        public class GOConfig
        {
            public string controlRef { get; set; }
            public string goId { get; set; }
            public int vLan { get; set; }
            public int vLanPriority { get; set; }
            public int appId { get; set; }
            public string macSrc { get; set; }
            public int confRev { get; set; }

            public GOConfig(int idx)
            {
                this.appId = 4800;
                this.confRev = 1;
                this.controlRef = "GOOSE";
                this.goId = "GOOSE_" + idx.ToString("0");
                this.macSrc = $"01:0C:CD:04:00:0{idx}";
                this.vLan = 100;
                this.vLanPriority = 4;
            }
        }

        public NetworkConfig(int idx)
        {
            this.svConfig = new SVConfig(idx);
            this.goConfig = new GOConfig(idx);
        }
    }

    public class CommunicationConfig
    {
        public bool status { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string ip { get; set; }
        public int port { get; set; }

        public CommunicationConfig()
        {
            this.status = false;
            this.name = "admin";
            this.password = "admin";
            this.ip = "";
        }
    }

    public class SequenceConfig
    {
        public string name { set; get; }
        public double time { set; get; }
        public List<variable> data { set; get; }
        public SequenceConfig(string name, double time)
        {
            this.name = name;
            this.time = time;
            this.data = new List<variable>();
        }
        public class variable
        {
            public string? name { set; get; }
            public double module { set; get; }
            public double angle { set; get; }
            public variable(string? name, double module, double angle)
            {
                this.name = name;
                this.module = module;
                this.angle = angle;
            }
        }
    
        public static List<SequenceConfig> defaultSequences()
        {
            List<SequenceConfig> sequences = new List<SequenceConfig>();
            return sequences;
        }
    }

    public class ContinuousConfig
    {
        public List<Variable> data { set; get; }

        public List<List<double>>? values;

        public class Variable
        {
            public double Mod { set; get; }
            public double Ang { set; get; }
            public void setValueMod(string val)
            {
                double multi = 1;
                if (val.Contains('k'))
                    multi = 1e3;
                else if (val.Contains('M'))
                    multi = 1e6;
                var value = Regex.Replace(val, "[^0-9,\\.]", "");
                value = value.Replace(',', '.');
                if (double.TryParse(value, out double valueD))
                    this.Mod = valueD * multi;
            }
            public void setValueAng(string val)
            {
                double multi = 1;
                if (val.Contains('k'))
                    multi = 1e3;
                else if (val.Contains('M'))
                    multi = 1e6;
                var value = Regex.Replace(val, "[^0-9,\\.]", "");
                value.Replace(',', '.');
                if (double.TryParse(value, out double valueD))
                    this.Ang = NormalizeAngle(valueD * multi);
            }
            private double NormalizeAngle(double angle)
            {
                angle %= 360.0;

                if (angle > 180.0)
                {
                    angle -= 360.0;
                }
                else if (angle < -180.0)
                {
                    angle += 360.0;
                }
                return angle;
            }
        }

        public ContinuousConfig()
        {
            this.data = new List<Variable>();
            for (int i = 0; i < 8; i++)
            {
                this.data.Add(new Variable
                {
                    Ang = 0,
                    Mod = 0
                });
            }
        }

    }

    public class TransientConfig
    {
        [JsonIgnore]
        public List<List<double>> data { get; set; }
        public string fileName { get; set; }
        public int nData { get; set; }
        public int[] setup { get; 
        set; }
        public TransientConfig()
        {
            this.data = new List<List<double>>();
            this.fileName = "";
            this.setup = new int[8];
            this.nData = -1;
            for (int i = 0; i < 8; i++)
                this.setup[i] = -1;
        }
        public void resetSetup()
        {
            for (int i = 0; i < 8; i++)
                this.setup[i] = -1;
        }
    
        public bool LoadDataFromFile(string filePath)
        {
            data = new List<List<double>>();
            try
            {
                using StreamReader sr = new StreamReader(filePath);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] columns = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (columns.Length > 0)
                        data.Add(columns.Select(x => double.Parse(x)).ToList());
                }
                if (data.Count >= 0)
                {
                    if (data.Count > data[0].Count)
                    {
                        List<List<double>> newData = new List<List<double>>();
                        for (int i = 0; i < data[0].Count; i++)
                        {
                            List<double> newLine = new List<double>();
                            for (int j = 0; j < data.Count; j++)
                            {
                                newLine.Add(data[j][i]);
                            }
                            newData.Add(newLine);
                        }
                        data = newData;
                    }
                }

                this.data = data;
                this.fileName = filePath;
                this.nData = data.Count - 1;
                this.resetSetup();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
