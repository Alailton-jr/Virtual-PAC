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
            public double frequency { get; set; }
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
            public TestSequence[] Test { get; set; }
        }

    }

    public class YamlContinuous
    {
        public class TestClass
        {
            public string type { get; set; }
            public int pps { get; set; }
            public List<ValuesClass> values { get; set; }
        }
        public class ValuesClass
        {
            public double angle { get; set; }
            public double frequency { get; set; }
            public double module { get; set; }
        }
        public class YamlRoot
        {
            public TestClass Test { get; set; }
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
            public int vlan { get; set; }
            public string svId { get; set; }
            public int confRev { get; set; }
            public int smpSync { get; set; }
            public int noAsdu { get; set; }
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

        public class General
        {
            [YamlDotNet.Serialization.YamlMember(Alias = "IpAddress")]
            public string ipAddress { get; set; }
            [YamlDotNet.Serialization.YamlMember(Alias = "Port")]
            public int port { get; set; }
        }

        public class YamlRoot
        {
            public SampledValue SvNetwork { get; set; }
            public GOOSE GoNetwork { get; set; }
            [YamlDotNet.Serialization.YamlMember(Alias = "General")]
            public General general { get; set; }
        }

    }

    public class Ctl
    {

        public NetworkConfig networkConfig;
        public CommunicationConfig communicationConfig;
        public ContinuousConfig continuousConfig;
        public GeneralConfig generalConfig;
        public bool connectionFlag = false;
        public List<SequenceConfig> sequencesConfig;
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
                }
                else
                {
                    this.networkConfig = new NetworkConfig();
                    this.communicationConfig = new CommunicationConfig();
                    this.continuousConfig = new ContinuousConfig() { data = new List<ContinuousConfig.Variable>() };
                    for (int i = 0; i < 8; i++)
                    {
                        this.continuousConfig.data.Add(new ContinuousConfig.Variable
                        {
                            Ang = 0,
                            Mod = 0
                        });
                    }
                    this.sequencesConfig = new List<SequenceConfig>();
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
            var rootObject = new YamlNetwork.YamlRoot();
            rootObject.SvNetwork = new YamlNetwork.SampledValue()
            {
                pps = networkConfig.svConfig.frequency * 80,
                AppId = networkConfig.svConfig.appId,
                macDst = networkConfig.svConfig.macDest,
                vlan = networkConfig.svConfig.vLan,
                svId = networkConfig.svConfig.svId,
                confRev = networkConfig.svConfig.confRev,
                smpSync = 0,
                noAsdu = networkConfig.svConfig.noAsdu
            };
            rootObject.GoNetwork = new YamlNetwork.GOOSE()
            {
                goId = networkConfig.goConfig.goId,
                controlRef = networkConfig.goConfig.controlRef,
                vLan = networkConfig.goConfig.vLan,
                appId = networkConfig.goConfig.appId,
                macSrc = networkConfig.goConfig.macSrc,
                confRev = networkConfig.goConfig.confRev
            };
            rootObject.general = new YamlNetwork.General()
            {
                ipAddress = generalConfig.ip,
                port = generalConfig.port
            };
            var serializer = new SerializerBuilder().Build();
            string yaml = serializer.Serialize(rootObject);
            return yaml;
        }

        public string CreateYamlSequencer()
        {

            var rootObject = new YamlSequencer.YamlRoot
            {
                Test = new YamlSequencer.TestSequence[sequencesConfig.Count]
            };
            for (int i = 0; i < sequencesConfig.Count; i++)
            {
                rootObject.Test[i] = new YamlSequencer.TestSequence
                {
                    duration = sequencesConfig[i].time,
                    type = "Iabc",
                    values = new List<YamlSequencer.Values>()
                };
                for (int j = 0; j < sequencesConfig[i].data.Count; j++)
                {
                    rootObject.Test[i].values.Add(new YamlSequencer.Values
                    {
                        angle = sequencesConfig[i].data[j].angle,
                        frequency = networkConfig.svConfig.frequency,
                        module = sequencesConfig[i].data[j].module,
                    });
                }
            };
            var serializer = new SerializerBuilder().Build();
            string yaml = serializer.Serialize(rootObject);
            return yaml;
        }

        public string CreateYamlContinuous()
        {

            var rootObject = new YamlContinuous.YamlRoot()
            {
                Test = new YamlContinuous.TestClass()
                {
                    pps = networkConfig.svConfig.frequency * 80,
                    type = "Iabc",
                    values = new List<YamlContinuous.ValuesClass>()
                }
            };
            for (int j = 0; j < continuousConfig.data.Count; j++)
            {
                rootObject.Test.values.Add(new YamlContinuous.ValuesClass()
                {
                    angle = continuousConfig.data[j].Ang,
                    frequency = networkConfig.svConfig.frequency,
                    module = continuousConfig.data[j].Mod,
                });
            }
            var serializer = new SerializerBuilder().Build();
            string yaml = serializer.Serialize(rootObject);
            return yaml;
        }

        public void loadYamlNetwork(string yamlData)
        {
            YamlNetwork.YamlRoot yamlFile;
            var deserializer = new DeserializerBuilder().Build();
            yamlFile = deserializer.Deserialize<YamlNetwork.YamlRoot>(yamlData);
            
            this.generalConfig.ip = yamlFile.general.ipAddress;
            this.generalConfig.port = yamlFile.general.port;

            this.networkConfig.svConfig.frequency = yamlFile.SvNetwork.pps / 80;
            this.networkConfig.svConfig.appId = yamlFile.SvNetwork.AppId;
            this.networkConfig.svConfig.macDest = yamlFile.SvNetwork.macDst;
            this.networkConfig.svConfig.vLan = yamlFile.SvNetwork.vlan;
            this.networkConfig.svConfig.svId = yamlFile.SvNetwork.svId;
            this.networkConfig.svConfig.confRev = yamlFile.SvNetwork.confRev;
            this.networkConfig.svConfig.noAsdu = yamlFile.SvNetwork.noAsdu;

            this.networkConfig.goConfig.goId = yamlFile.GoNetwork.goId;
            this.networkConfig.goConfig.controlRef = yamlFile.GoNetwork.controlRef;
            this.networkConfig.goConfig.vLan = yamlFile.GoNetwork.vLan;
            this.networkConfig.goConfig.appId = yamlFile.GoNetwork.appId;
            this.networkConfig.goConfig.macSrc = yamlFile.GoNetwork.macSrc;
            this.networkConfig.goConfig.confRev = yamlFile.GoNetwork.confRev;

        }

        public void loadYamlSequencer(string yamlData)
        {
            YamlSequencer.YamlRoot yamlFile;
            var deserializer = new DeserializerBuilder().Build();
            yamlFile = deserializer.Deserialize<YamlSequencer.YamlRoot>(yamlData);

            this.sequencesConfig = new List<SequenceConfig>();
            for(int i = 0; i < yamlFile.Test.Length; i++)
            {
                SequenceConfig sequence = new SequenceConfig(
                     "Sequence " + (i + 1).ToString(),
                     yamlFile.Test[i].duration
                );

                sequence.data = new List<SequenceConfig.variable>();
                for (int j = 0; j < yamlFile.Test[i].values.Count; j++)
                {
                    sequence.data.Add(new SequenceConfig.variable(null, yamlFile.Test[i].values[j].module, yamlFile.Test[i].values[j].angle));
                }
                this.sequencesConfig.Add(sequence);
            }
        }
    
        public void loadYamlContinuous(string yamlData)
        {
            YamlContinuous.YamlRoot yamlFile;
            var deserializer = new DeserializerBuilder().Build();
            yamlFile = deserializer.Deserialize<YamlContinuous.YamlRoot>(yamlData);

            for (int j = 0; j < yamlFile.Test.values.Count; j++)
            {
                this.continuousConfig.data[j].Mod = yamlFile.Test.values[j].module;
                this.continuousConfig.data[j].Ang = yamlFile.Test.values[j].angle;
            }
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
            public int appId { get; set; }
            public int noAsdu { get; set; }
            public string macDest { get; set; }
            public int confRev { get; set; }
        }
        public class GOConfig
        {
            public string controlRef { get; set; }
            public string goId { get; set; }
            public int vLan { get; set; }
            public int appId { get; set; }
            public string macSrc { get; set; }
            public int confRev { get; set; }
        }
    }

    public class CommunicationConfig
    {
        public bool status { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
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
    }

    public class ContinuousConfig
    {
        public List<Variable> data { set; get; }

        //public Variable? Ib { set; get; }
        //public Variable? Ic { set; get; }
        //public Variable? In { set; get; }
        //public Variable? Va { set; get; }
        //public Variable? Vb { set; get; }
        //public Variable? Vc { set; get; }
        //public Variable? Vn { set; get; }
        //public List<List<double>>? values;

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

    }


}
