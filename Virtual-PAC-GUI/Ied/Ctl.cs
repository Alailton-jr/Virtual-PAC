using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using static Ied.MainForm;
using Newtonsoft.Json;
using YamlDotNet.RepresentationModel;
using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml.Linq;
using YamlDotNet.Serialization;
using Microsoft.VisualBasic.Logging;
using YamlDotNet.Serialization.NamingConventions;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using Ied;
using static Ied.yamlClass;
using System.Diagnostics;

namespace Ied
{

    public enum CurrentType // Como a corrente é mostrada
    {
        Ampere = 1,
        Pu = 0
    }
    public enum VoltageType // Como a corrente é mostrada
    {
        Volts = 1,
        Pu = 0
    }
    public enum TimeType // Como o Tempo é mostrado
    {
        seconds = 1,
        cycle = 0
    }

    public class Ctl
    {
        public bool wasSent = false;

        public PiocConfig pioc { get; set; }

        public PtocConfig ptoc{ get; set; }

        public PdirConfig pdir { get; set; }

        public PtuvConfig ptuv { get; set; }

        public PtovConfig ptov { get; set; }

        public PdisConfig pdis { get; set; }

        public GeneralConfig general { get; set; }

        public NetworkConfig network { get; set; }

        public SampledValueConfig sampledValue { get; set; }

        public CommunicationConfig communication { get; set; }

        public SCLConfig sclConf { get; set; }

        public Iec61850Config iecConf { get; set; }

        public SocketConnection serverCon { get; set; }


        public void saveClass()
        {
            string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            string filePath = "vIED_data.dat";
            File.WriteAllText(filePath, json);
        }

        public bool loadClass()
        {
            try
            {
                if (File.Exists("vIED_data.dat"))
                {
                    string loadedJson = File.ReadAllText("vIED_data.dat");
                    var tempData = JsonConvert.DeserializeObject<Ctl>(loadedJson);
                    this.pioc = tempData.pioc;
                    this.ptoc = tempData.ptoc;
                    this.ptuv = tempData.ptuv;
                    this.ptov = tempData.ptov;
                    this.pdis = tempData.pdis;
                    this.pdir = tempData.pdir;
                    this.general = tempData.general;
                    this.network = tempData.network;
                    this.serverCon = tempData.serverCon;
                    this.sampledValue = tempData.sampledValue;
                    this.sclConf = tempData.sclConf;
                    this.iecConf = tempData.iecConf;
                    this.communication = tempData.communication;
                }
                else
                {
                    this.communication = new CommunicationConfig()
                    {
                        ip = "192.168.100.30",
                        name = "IED01",
                        port = 8002,
                        password = "passwd",
                        status = false,
                    };
                    this.general = new GeneralConfig()
                    {
                        frequency = 60,
                        NominalCurrent = 200,
                        ipAddress = "192.168.100.30",
                        NominalVoltage = 13800,
                        port = 8080,
                    };
                    this.network = new NetworkConfig()
                    {
                        iface = "enp1s0",
                        ppc = 80
                    };
                    this.pioc = new PiocConfig()
                    {
                        phase = new PiocConfig.Phase(),
                        neutral = new PiocConfig.Phase()
                    };
                    this.ptoc = new PtocConfig()
                    {
                        phase = new PtocConfig.Phase(),
                        neutral = new PtocConfig.Phase()
                    };
                    this.ptuv = new PtuvConfig();
                    this.ptov = new PtovConfig();
                    this.pdis = new PdisConfig();
                    this.pdir = new PdirConfig();
                    this.sampledValue = new SampledValueConfig()
                    {
                        vLan = 0x8000,
                        appId = 0x4000,
                        confRev = 1,
                        macDest = "01:0C:CD:04:00:00",
                        noAsdu = 1,
                        svId = "TRTC",
                        frequency = 60,
                    };
                    this.iecConf = new Iec61850Config
                    {
                        dataSet = new List<Iec61850Config.DataSetConfig>(),
                        goSendList = new List<Iec61850Config.GoSend>(),
                        iedModel = new IedModel(),
                    };
                    this.sclConf = new SCLConfig();
                    this.sclConf.defaultConfig();
                    this.serverCon = new SocketConnection();
                    wasSent = false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string saveYaml(string path = null)
        {
            try
            {
                string svId = "TRTC";
                yamlClass.Configuration config = new yamlClass.Configuration();
                config.GeneralSettings = new yamlClass.GeneralSettings
                {
                    IpAddress = this.general.ipAddress,
                    CurrentNominal = (double)this.general.NominalCurrent,
                    VoltageNominal = (double)this.general.NominalVoltage,
                    Port = (int)this.general.port,
                    frequecy = (double)this.general.frequency
                };

                config.ProtectionFunction = new yamlClass.ProtectionFunction
                {
                    PIOCPhase = new List<PIOC>(),
                    PIOCNeutral = new List<PIOC>(),
                    PTOCNeutral = new List<PTOC>(),
                    PTOCPhase = new List<PTOC>(),
                    PTUV = new List<PTUV>(),
                    PTOV = new List<PTOV>(),
                    PDIS = new List<PDIS>(),
                    PDIR = new PDIR()
                };
                for (int i = 0; i < 3; i++)
                {
                    // PIOC Phase
                    config.ProtectionFunction.PIOCPhase.Add(new PIOC { Enabled = this.pioc.phase.level[i].enabled, Pickup = (double)this.pioc.phase.level[i].pickup, TimeDelay = (double)this.pioc.phase.level[i].timeDelay });
                    // PIOC Neutral
                    config.ProtectionFunction.PIOCNeutral.Add(new PIOC { Enabled = this.pioc.neutral.level[i].enabled, Pickup = (double)this.pioc.neutral.level[i].pickup, TimeDelay = (double)this.pioc.neutral.level[i].timeDelay });
                    // PTOC Phase
                    config.ProtectionFunction.PTOCPhase.Add(new PTOC { Enabled = this.ptoc.phase.level[i].enabled, Curve = this.ptoc.phase.level[i].Curve, Pickup = (double)this.ptoc.phase.level[i].pickup, TimeDial = (double)this.ptoc.phase.level[i].timeDial });
                    // PTOC Neutral
                    config.ProtectionFunction.PTOCNeutral.Add(new PTOC { Enabled = this.ptoc.neutral.level[i].enabled, Curve = this.ptoc.neutral.level[i].Curve, Pickup = (double)this.ptoc.neutral.level[i].pickup, TimeDial = (double)this.ptoc.neutral.level[i].timeDial });
                    // PTUV
                    config.ProtectionFunction.PTUV.Add(new PTUV { Enabled = this.ptuv.level[i].enabled, Pickup = (double)this.ptuv.level[i].pickup, TimeDelay = (double)this.ptuv.level[i].timeDelay });
                    // PTOV
                    config.ProtectionFunction.PTOV.Add(new PTOV { Enabled = this.ptov.level[i].enabled, Pickup = (double)this.ptov.level[i].pickup, TimeDelay = (double)this.ptov.level[i].timeDelay });
                    // PDIS
                    config.ProtectionFunction.PDIS.Add(new PDIS { Enabled = this.pdis.zone[i].enabled, Ajuste = (double)this.pdis.zone[i].ajuste, Angle = (double)this.pdis.zone[i].angle, TimeDelay = (double)this.pdis.zone[i].timeDelay, Type = this.pdis.zone[i].type.ToString() });
                }
                // PDIR
                //config.ProtectionFunction.PDIR = new PDIR { Pickup = (double)this.pdir.pickup, Angle = (double)this.pdir.angle, Polarity = this.pdir.polarity.ToString() };

                config.Logic = new yamlClass.Logic
                {
                    Name = "Temp"
                };

                config.Sniffer = new yamlClass.Sniffer
                {
                    Pps = (float)((double)this.general.frequency * this.network.ppc),
                    Frequency = (double)this.general.frequency,
                    Ppc = (float)this.network.ppc,
                    MacDst = this.sampledValue.macDest,
                    MacIntern = "01:0C:CD:0A:00:00",
                    SvId = svId
                };

                config.Goose = new List<yamlClass.GOOSE>();
                if (iecConf != null)
                    foreach (var goose in iecConf.goSendList)
                    {
                        if (string.IsNullOrEmpty(goose.dataSetName)) continue;
                        yamlClass.GOOSE newGoose = new yamlClass.GOOSE()
                        {
                            appId = goose.appId,
                            cbRef = goose.cbRef,
                            confRef = goose.confRef,
                            dataSetName = goose.dataSetName,
                            goId = goose.goId,
                            macDst = goose.macDst,
                            maxTime = goose.maxTime,
                            minTime = goose.minTime,
                            vLanId = goose.vLanId,
                            vLanPriority = goose.vLanPriority,
                            dataSet = goose.getSendListString()
                        };

                        config.Goose.Add(newGoose);
                    }

                SerializerBuilder builder = new SerializerBuilder();
                var serializer = builder.Build();

                string yaml = serializer.Serialize(config);
                if (path != null)
                    File.WriteAllText(path, yaml);
                return yaml;
            }
            catch { return null; }

        }

        public void parserYaml(string yamlString)
        {
            var yaml = new YamlDotNet.Serialization.Deserializer().Deserialize<yamlClass.Configuration>(new StringReader(yamlString));
            
            var prot = yaml.ProtectionFunction;
            var gen = yaml.GeneralSettings;
            var logic = yaml.Logic;
            var snif = yaml.Sniffer;
            var goose = yaml.Goose;

            // General
            this.general = new GeneralConfig
            {
                frequency = gen.frequecy,
                ipAddress = gen.IpAddress,
                NominalVoltage = gen.VoltageNominal,
                NominalCurrent =gen.CurrentNominal
            };
            this.communication = new CommunicationConfig
            {
                ip = gen.IpAddress,
                port = gen.Port,
                password = "passwd",
            };

            // Protection
            for (int i = 0; i<3;i++)
            {
                this.ptoc.phase.level[i].Curve = prot.PTOCPhase[i].Curve;
                this.ptoc.phase.level[i].pickup = prot.PTOCPhase[i].Pickup;
                this.ptoc.phase.level[i].timeDial = prot.PTOCPhase[i].TimeDial;
                this.ptoc.phase.level[i].enabled = prot.PTOCPhase[i].Enabled;

                this.ptoc.neutral.level[i].Curve = prot.PTOCNeutral[i].Curve;
                this.ptoc.neutral.level[i].pickup = prot.PTOCNeutral[i].Pickup;
                this.ptoc.neutral.level[i].timeDial = prot.PTOCNeutral[i].TimeDial;
                this.ptoc.neutral.level[i].enabled = prot.PTOCNeutral[i].Enabled;

                this.pioc.phase.level[i].pickup = prot.PIOCPhase[i].Pickup;
                this.pioc.phase.level[i].timeDelay = prot.PIOCPhase[i].TimeDelay;
                this.pioc.phase.level[i].enabled = prot.PIOCPhase[i].Enabled;

                this.pioc.neutral.level[i].pickup = prot.PIOCNeutral[i].Pickup;
                this.pioc.neutral.level[i].timeDelay = prot.PIOCNeutral[i].TimeDelay;
                this.pioc.neutral.level[i].enabled = prot.PIOCNeutral[i].Enabled;

                this.ptuv.level[i].pickup = prot.PTUV[i].Pickup;
                this.ptuv.level[i].timeDelay = prot.PTUV[i].TimeDelay;
                this.ptuv.level[i].enabled = prot.PTUV[i].Enabled;

                this.ptov.level[i].pickup = prot.PTOV[i].Pickup;
                this.ptov.level[i].timeDelay = prot.PTOV[i].TimeDelay;
                this.ptov.level[i].enabled = prot.PTOV[i].Enabled;

            }

            //GOOSE
            this.iecConf.goSendList.Clear();
            foreach (var go in goose)
            {
                var goSendTemp = new Iec61850Config.GoSend()
                {
                    appId = go.appId,
                    cbRef = go.cbRef,
                    confRef = go.confRef,
                    dataSetName = go.dataSetName,
                    goId = go.goId,
                    macDst = go.macDst,
                    maxTime = go.maxTime,
                    minTime = go.minTime,
                    vLanId = go.vLanId,
                    vLanPriority = go.vLanPriority,
                };
                goSendTemp.dataSet = new Iec61850Config.DataSetConfig();
                this.iecConf.goSendList.Add(goSendTemp);
            }

            //Sniffer
            this.sampledValue = new SampledValueConfig
            {
                appId = snif.AppId,
                confRev = snif.ConfRev,
                frequency = snif.Frequency,
                macDest = snif.MacDst,
                noAsdu = snif.NoAsdu,
                svId = snif.SvId,
                vLan = snif.Vlan
            };


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

        public void extSocketConnection(Socket extSocket, bool extConnected)
        {
            this.clientSocket = extSocket;
            this.isConnected = extConnected;
        }

        public void changeConProperties(string ip, int port)
        {
            var needDisconnect = false;
            if (this.serverIp != ip || this.serverPort != port) needDisconnect = true;

            this.serverIp = ip;
            this.serverPort = port;

            if (needDisconnect && isConnected) this.Disconnect();
        }

        private bool Connect()
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
                MessageBox.Show($"Failed to connect: {ex.Message}");
                isConnected = false;
                return false;
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
            catch  (Exception ex)
            {
                HandleConnectionLost();
                return null;
            }
        }

    }

    public class PtuvConfig
    {
        public VoltageType voltageType;
        public TimeType timeType = TimeType.seconds;
        public Levels[] level = { new Levels(), new Levels(), new Levels() };
        public class Levels
        {
            public bool enabled = true;
            private double _pickup;
            private double _timeDelay;


            public object pickup
            {
                get { return _pickup; }
                set
                {
                    double temp;
                    if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                        this._pickup = temp;
                }
            }

            public object timeDelay
            {
                get { return this._timeDelay; }
                set
                {
                    double temp;
                    if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                        this._timeDelay = temp;
                }
            }

        }
    }

    public class PtovConfig
    {
        public VoltageType voltageType;
        public TimeType timeType = TimeType.seconds;
        public Levels[] level = { new Levels(), new Levels(), new Levels() };
        public class Levels
        {
            public bool enabled = true;
            private double _pickup;
            private double _timeDelay;

            public object pickup
            {
                get { return _pickup; }
                set
                {
                    double temp;
                    if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                        this._pickup = temp;
                }
            }

            public object timeDelay
            {
                get { return this._timeDelay; }
                set
                {
                    double temp;
                    if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                        this._timeDelay = temp;
                }
            }

        }
    }

    public class PdisConfig
    {
        //public VoltageType voltageType;
        public TimeType timeType = TimeType.seconds;
        public Zones[] zone = { new Zones(), new Zones(), new Zones() };

        public class Zones
        {
            public bool enabled = true;
            public PdisType type;

            private double _ajuste;
            private double _angle;
            private double _timeDelay;

            public object ajuste
            {
                get { return _ajuste; }
                set
                {
                    double temp;
                    if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                        this._ajuste = temp;
                }
            }

            public object angle
            {
                get { return _angle; }
                set
                {
                    double temp;
                    if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                    {
                        temp = (temp + 180) % 360;
                        if (temp < 0)
                        {
                            temp += 360;
                        }
                        temp -= 180;
                        this._angle = temp;
                    }
                }
            }

            public object timeDelay
            {
                get { return this._timeDelay; }
                set
                {
                    double temp;
                    if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                        this._timeDelay = temp;
                }
            }

        }
        public enum PdisType
        {
            Impedancia = 0x00,
            Admitancia = 0x01,
            Reatancia = 0x02
        }
    }

    public class PiocConfig
    {
        public Phase phase = new Phase();
        public Phase neutral = new Phase();
        public class Phase
        {
            public CurrentType currentType = CurrentType.Ampere;
            public TimeType timeType = TimeType.seconds;

            public Levels[] level = { new Levels(), new Levels(), new Levels() };

            public class Levels
            {
                public bool enabled = true;
                private double _pickup;
                private double _timeDelay;
               

                public object pickup {
                    get { return _pickup; }
                    set
                    {
                        double temp;
                        if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                            this._pickup = temp;
                    }
                }

                public object timeDelay {
                    get { return this._timeDelay; }
                    set
                    {
                        double temp;
                        if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                            this._timeDelay = temp;
                    }
                }
            }

        }

    }

    public class PtocConfig
    {
        public Phase phase = new Phase();
        public Phase neutral = new Phase();

        public class Phase
        {
            public Dictionary<string, Dictionary<string, double>> curves = new Dictionary<string, Dictionary<string, double>>
            {
                {"U1", new Dictionary<string, double> { {"A", 0.0226}, {"B", 0.0104}, {"C", 0.02} }},
                {"U2", new Dictionary<string, double> { {"A", 0.180}, {"B", 5.95}, {"C", 2} }},
                {"U3", new Dictionary<string, double> { {"A", 0.0963}, {"B", 3.88}, {"C", 2} }},
                {"U4", new Dictionary<string, double> { {"A", 0.0352}, {"B", 5.67}, {"C", 2} }},
                {"U5", new Dictionary<string, double> { {"A", 0.00262}, {"B", 0.00342}, {"C", 0.02} }},
                {"C1", new Dictionary<string, double> { {"A", 0}, {"B", 0.14}, {"C", 0.02} }},
                {"C2", new Dictionary<string, double> { {"A", 0}, {"B", 13.5}, {"C", 1} }},
                {"C3", new Dictionary<string, double> { {"A", 0}, {"B", 80}, {"C", 2} }},
                {"C4", new Dictionary<string, double> { {"A", 0}, {"B", 120}, {"C", 1} }},
                {"C5", new Dictionary<string, double> { {"A", 0}, {"B", 0.05}, {"C", 0.04} }}
            };

            public Levels[] level = { new Levels(), new Levels(), new Levels()};

            public class Levels
            {
                public bool enabled = false;
                public double pickup = 0;
                public double timeDial = 0;
                public string Curve = "C1";

                public void setPickup(string x)
                {
                    double temp;
                    if (double.TryParse(x, out temp))
                        this.pickup = temp;
                }
                public void setTimeDial(string x)
                {
                    double temp;
                    if (double.TryParse(x, out temp))
                        this.timeDial = temp;
                }
                public void setCurve(string x)
                {
                    if ("C1 C2 C3 C4 C5 U1 U2 U3 U4".Contains(x))
                        this.Curve = x;
                }
            
            }

        }
    }

    public class PdirConfig
    {
        public double _pickup { get; set; }
        public double _angle { get; set; }
        public int _polarity { get; set; }

        public object pickup
        {
            get { return _pickup; }
            set
            {
                double temp;
                if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                    this._pickup = temp;
            }
        }

        public object polarity
        {
            get { return _polarity; }
            set
            {
                int temp;
                if (int.TryParse(value.ToString().Replace("°",""), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                    this._polarity = temp;
            }
        }

        public object angle
        {
            get { return _angle; }
            set
            {
                double temp;
                if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                    this._angle = temp;
            }
        }

    }

    public class GeneralConfig
    {
        private double _NominalCurrent;
        public object NominalCurrent
        {
            get { return _NominalCurrent; }
            set
            {
                double temp;
                if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                    this._NominalCurrent = temp;
            }
        }
        private double _NominalVoltage;
        public object NominalVoltage
        {
            get { return _NominalVoltage; }
            set
            {
                double temp;
                if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                    this._NominalVoltage = temp;
            }
        }

        private double _frequency;
        public object frequency
        {
            get { return _frequency; }
            set
            {
                double temp;
                if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                    this._frequency = temp;
            }
        }
        private string _ipAddress;
        public string ipAddress 
        {
            get { return _ipAddress; }
            set
            {
                if (value == null) return;
                string patternIPv4 = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
                if (Regex.IsMatch(value, patternIPv4))
                    _ipAddress = value;
            }
        }

        private int _port;
        public object port
        {
            get { return _port; }
            set
            {
                int temp;
                if (int.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                    this._port = temp;
            }
        }

    }

    public class NetworkConfig
    {
        public string iface = "Intel(R) Dual Band Wireless-AC 7265";
        public int ppc = 80;
    }

    public class SampledValueConfig
    {
        public double frequency { get; set; }
        public string svId { get; set; }
        public int vLan { get; set; }
        public int appId { get; set; }
        public int noAsdu { get; set; }
        public string macDest { get; set; }
        public int confRev { get; set; }
    }

    public class CommunicationConfig
    {
        public bool status { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
    }

    public class Iec61850Config
    {

        public List<DataSetConfig> dataSet;
        public List<GoSend> goSendList;
        public IedModel iedModel;

        public class GoSend
        {
            public string macDst { get; set; }
            public string goId { get; set; }
            public uint appId { get; set; }
            public uint vLanId { get; set; }
            public uint vLanPriority { get; set; }
            public uint minTime { get; set; }
            public uint maxTime { get; set; }
            public string cbRef { get; set; }
            public uint confRef { get; set; }
            public string dataSetName { get; set; }
            public DataSetConfig dataSet { get; set; }

            public GoSend()
            {
                macDst = "01:0C:CD:04:00:00";
                goId = "GO01";
                appId = 0x4000;
                vLanId = 0x100;
                vLanPriority = 4;
                minTime = 2;
                maxTime = 2000;
                cbRef = "GOOSE";
                confRef = 1;
            }

            private void findChield(List<DataSetConfig.FcdaClass> list, DataSetConfig.TreeNodeData node)
            {
                if(node.check)
                {
                    if (node.Children.Count == 0 && node.check)
                    {
                        if (node.Path != null)
                            list.Add(node.fcda);
                    }
                    else foreach (var x in node.Children) findChield(list, x);
                }
            }

            public List<string> getSendListString()
            {
                var fcda = this.getSendList();
                if (fcda == null) return null;
                var list = new List<string>();
                foreach(var x in fcda)
                {
                    string temp = "";
                    //PROT.P1PTOV_1.Op.general
                    if (x.ldInst != null) temp += x.ldInst + ".";
                    if (x.prefix != null) temp += x.prefix;
                    if (x.lnClass != null) temp += x.lnClass + ".";
                    if (x.doName != null) temp += x.doName + ".";
                    if (x.sdoName != null) temp += x.sdoName + ".";
                    foreach (var da in x.daName) temp += da + ".";
                    temp = temp.Substring(0,temp.Length - 1);
                    list.Add(temp);
                }
                return list;
            }

            public List<DataSetConfig.FcdaClass> getSendList()
            {
                var list = new List<DataSetConfig.FcdaClass>();
                if (this.dataSet == null) return null;
                this.dataSet.saveData();
                foreach(var x in this.dataSet.treeViewData)
                {
                    if(x.check)  findChield(list, x);
                }
                return list;
            }

            public static string RemoveNonNumericChars(string input)
            {
                // Use a regular expression to match and replace non-numeric characters
                string pattern = "[^0-9,.]";
                string result = Regex.Replace(input, pattern, "");

                return result;
            }


            public void setMac(string macAddress)
            {
                //macAddress = RemoveNonNumericChars(macAddress);
                string pattern = @"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$";
                if (Regex.IsMatch(macAddress, pattern)) this.macDst = macAddress.ToUpper();
            }
            public void setAppId(string AppId)
            {
                AppId = RemoveNonNumericChars(AppId);
                if (uint.TryParse(AppId, out uint x))
                    if (x <= 0xffff) this.appId = x;
            }
            public void setvLanId(string VLanId)
            {
                VLanId = RemoveNonNumericChars(VLanId);
                if (uint.TryParse(VLanId, out uint x))
                    if (x <= 0x1000) this.vLanId = x;
            }
            public void setvLanPriotiry(string VlanPriority)
            {
                VlanPriority = RemoveNonNumericChars(VlanPriority);
                if (uint.TryParse(VlanPriority, out uint x))
                    if (x <= 7) this.vLanPriority = x;
            }
            public void setConfRev(string ConfRev)
            {
                ConfRev = RemoveNonNumericChars(ConfRev);
                if (uint.TryParse(ConfRev, out uint x)) this.confRef = x;
            }
            public void setMinTime(string MinTime)
            {
                MinTime = RemoveNonNumericChars(MinTime);
                if (uint.TryParse(MinTime, out uint x))
                    if (x < this.maxTime) this.minTime = x;
            }
            public void setMaxTime(string MaxTime)
            {
                MaxTime = RemoveNonNumericChars(MaxTime);
                if (uint.TryParse(MaxTime, out uint x))
                    if (x > this.minTime) this.maxTime = x;
            }

        }
        
        public class DataSetConfig
        {

            public string dataSetName { get; set; }
            public string desc { get; set; }
            public List<string[]> data { get; set; }
            public List<TreeNodeData> treeViewData { get; set; }

            [Newtonsoft.Json.JsonIgnore]
            public List<TreeNode> nodes { get; set; }
            [Newtonsoft.Json.JsonIgnore]
            public TreeView treeview { get; set; }
           
            public DataSetConfig()
            {
                this.treeViewData = new List<TreeNodeData>();
            }

            public class FcdaClass
            {
                public string fc { get; set; }
                public string lnClass { get; set; }
                public string lnInst { get; set; }
                public string doName { get; set; }
                public List<string> daName { get; set; }
                public string sdoName { get; set; }
                public string ldInst { get; set; }
                public string prefix { get; set; }

                public FcdaClass()
                {
                    daName = new List<string>();
                }

                public FcdaClass clone()
                {
                    var x = new FcdaClass()
                    {
                        fc = this.fc,
                        lnClass = this.lnClass,
                        lnInst = this.lnInst,
                        doName = this.doName,
                        ldInst = this.ldInst,
                        prefix = this.prefix,
                        sdoName = this.sdoName
                    };
                    foreach (var da in this.daName) x.daName.Add(da);
                    return x;
                }
            }

            public class TreeNodeData
            {
                public string Text { get; set; }
                public bool check { get; set; }
                public string Path { get;  set; }
                public List<TreeNodeData> Children { get; set; }
                public FcdaClass fcda { get; set; }
                
            }

            public void saveData()
            { 
                if (this.treeview == null) return;
                this.treeViewData = new List<TreeNodeData>();
                foreach (TreeNode rootNode in this.treeview.Nodes)
                    this.treeViewData.Add(ExtractTreeViewData(rootNode));
            }

            public void loadData()
            {
                if (this.treeViewData == null) return;
                foreach (var value in this.treeViewData) if (value == null) return;
                this.treeview = new TreeView();
                this.treeview.PathSeparator = "$";
                foreach (TreeNodeData nodeData in this.treeViewData)
                {
                    this.treeview.Nodes.Add(nodeData.Text);
                    this.treeview.Nodes[this.treeview.Nodes.Count - 1].Checked = nodeData.check;
                    this.treeview.Nodes[this.treeview.Nodes.Count - 1].Tag = nodeData.fcda.clone();
                    if (nodeData.Children != null)
                        PopulateTreeView(this.treeview.Nodes[this.treeview.Nodes.Count-1], nodeData.Children);
                }
            }

            private void PopulateTreeView(TreeNode treeView, List<TreeNodeData> data)
            {
                foreach (TreeNodeData nodeData in data)
                {
                    TreeNode newNode = new TreeNode(nodeData.Text);
                    newNode.Checked = nodeData.check;
                    treeView.Nodes.Add(newNode);
                    newNode.Tag = nodeData.fcda.clone();
                    if (nodeData.Children != null)
                    {
                        PopulateTreeView(newNode, nodeData.Children);
                    }
                }
            }

            private TreeNodeData ExtractTreeViewData(TreeNode node)
            {
                try
                {
                    TreeNodeData data = new TreeNodeData
                    {
                        Text = node.Text,
                        check = node.Checked,
                        Path = node.FullPath,
                        Children = new List<TreeNodeData>(),
                        fcda = ((FcdaClass)node.Tag).clone()
                    };

                    foreach (TreeNode childNode in node.Nodes)
                    {
                        data.Children.Add(ExtractTreeViewData(childNode));
                    }
                    return data;
                }
                catch
                {
                    return null;
                }
            }

        }

    }

    public class IedModel
    {
        public SCL model;
        
        [XmlRoot(ElementName = "SCL", Namespace = "http://www.iec.ch/61850/2003/SCL")]
        public class SCL
        {
            [XmlElement("Header")]
            public Header _Header { get; set; } = new Header();
            [XmlElement("Communication")]
            public Communication _Communication { get; set; } = new Communication();

            [XmlIgnore]
            public Dictionary<string, IED> IedDict { get; set; }
            [XmlElement("IED")]
            public IED[] IedList
            {
                get => IedDict.Values.ToArray();
                set => IedDict = new Dictionary<string, IED>(value.ToDictionary(ied => ied._name));
            }

            [XmlElement("DataTypeTemplates")]
            public DataTypeTemplate _DataTypeTemplate { get; set; } = new DataTypeTemplate();
            [XmlAttribute("version")]
            public string _version = "2007";
            [XmlAttribute("revision")]
            public string _revision = "B";
        }
        public class DataTypeTemplate
        {
            [XmlElement("LNodeType")]
            public List<LNodeType> _LNodeTypeList = new List<LNodeType>();
            [XmlElement("DOType")]
            public List<DOType> _DOTypeList = new List<DOType>();
            [XmlElement("DAType")]
            public List<DAType> _DATypeList = new List<DAType>();
            [XmlElement("EnumType")]
            public List<EnumType> _EnumTypeList = new List<EnumType>();
        }
        public class EnumType
        {
            [XmlAttribute("id")]
            public string _id;
            [XmlElement("EnumVal")]
            public List<EnumVal> _enumValList = new List<EnumVal>();
        }
        public class EnumVal
        {
            [XmlAttribute("ord")]
            public string _ord;
            [XmlText]
            public string _value;
        }
        public class DAType
        {
            [XmlAttribute("id")]
            public string _id;
            [XmlElement("BDA")]
            public List<DAType_BDA> _bdaList = new List<DAType_BDA>();
        }
        public class DAType_BDA
        {
            [XmlAttribute("name")]
            public string _name;
            [XmlAttribute("bType")]
            public string _bType;
            [XmlAttribute("type")]
            public string _type;
            [XmlAttribute("FC")]
            public string _fc;
        }
        public class DOType
        {
            [XmlAttribute("id")]
            public string _id;
            [XmlAttribute("cdc")]
            public string _cdc = "";
            [XmlElement("DA")]
            public List<DOType_DA> _daList = new List<DOType_DA>();
        }
        public class DOType_DA
        {
            [XmlAttribute("name")]
            public string _name;
            [XmlAttribute("type")]
            public string _type;
            [XmlAttribute("fc")]
            public string _fc;
            [XmlAttribute("bType")]
            public string _bType;
        }
        public class LNodeType
        {
            [XmlAttribute("id")]
            public string _id;
            [XmlAttribute("lnClass")]
            public string _lnClass;
            [XmlElement("DO")]
            public List<LNodeType_DO> _doList = new List<LNodeType_DO>();
        }
        public class LNodeType_DO
        {
            [XmlAttribute("name")]
            public string _name;
            [XmlAttribute("type")]
            public string _type;
        }
        public class IED
        {
            [XmlAttribute("name")]
            public string _name;
            [XmlAttribute("type")]
            public string _type;
            [XmlAttribute("manufacturer")]
            public string _manufacturer;
            [XmlAttribute("configVersion")]
            public string _configVersion;
            [XmlAttribute("desc")]
            public string _desc;
            [XmlElement("Services")]
            public Services _Services = new Services();
            [XmlElement("AccessPoint")]
            public AccessPoint _AccessPoint = new AccessPoint();
        }
        public class Services
        {
            [XmlAttribute("nameLength")]
            public int NameLength { get; set; }

            [XmlElement(ElementName = "DynAssociation")]
            public DynAssociation dynAssociation { get; set; }

            [XmlElement(ElementName = "SettingGroups")]
            public string settingGroups { get; set; }

            [XmlElement(ElementName = "GetDirectory")]
            public string getDirectory { get; set; }

            [XmlElement(ElementName = "GetDataObjectDefinition")]
            public string getDataObjectDefinition { get; set; }

            [XmlElement(ElementName = "DataObjectDirectory")]
            public string dataObjectDirectory { get; set; }

            [XmlElement(ElementName = "GetDataSetValue")]
            public string getDataSetValue { get; set; }

            [XmlElement(ElementName = "DataSetDirectory")]
            public string dataSetDirectory { get; set; }

            [XmlElement(ElementName = "ConfDataSet")]
            public ConfDataSet confDataSet { get; set; }

            [XmlElement(ElementName = "ReadWrite")]
            public string readWrite { get; set; }

            [XmlElement(ElementName = "ConfReportControl")]
            public ConfReportControl confReportControl { get; set; }

            [XmlElement(ElementName = "GetCBValues")]
            public string getCBValues { get; set; }

            [XmlElement(ElementName = "ReportSettings")]
            public ReportSettings reportSettings { get; set; }

            [XmlElement(ElementName = "GSESettings")]
            public GSESettings gSESettings { get; set; }

            [XmlElement(ElementName = "GOOSE")]
            public GOOSE gOOSE { get; set; }

            [XmlElement(ElementName = "FileHandling")]
            public FileHandling fileHandling { get; set; }

            [XmlElement(ElementName = "ConfLNs")]
            public string confLNs { get; set; }

            [XmlElement(ElementName = "ClientServices")]
            public ClientServices clientServices { get; set; }

            [XmlElement(ElementName = "RedProt")]
            public RedProt redProt { get; set; }

            public class DynAssociation
            {
                [XmlAttribute("max")]
                public int Max { get; set; }
            }

            public class SettingGroups
            {
            }

            public class GetDirectory
            {
            }

            public class GetDataObjectDefinition
            {
            }

            public class DataObjectDirectory
            {
            }

            public class GetDataSetValue
            {
            }

            public class DataSetDirectory
            {
            }

            public class ConfDataSet
            {
                [XmlAttribute("max")]
                public int Max { get; set; }

                [XmlAttribute("maxAttributes")]
                public int MaxAttributes { get; set; }
            }

            public class ReadWrite
            {
            }

            public class ConfReportControl
            {
                [XmlAttribute("max")]
                public int Max { get; set; }

                [XmlAttribute("bufMode")]
                public string BufMode { get; set; }

                [XmlAttribute("bufConf")]
                public bool BufConf { get; set; }
            }

            public class GetCBValues
            {
            }

            public class ReportSettings
            {
                [XmlAttribute("cbName")]
                public string CbName { get; set; }

                [XmlAttribute("datSet")]
                public string DatSet { get; set; }

                [XmlAttribute("rptID")]
                public string RptID { get; set; }

                [XmlAttribute("optFields")]
                public string OptFields { get; set; }

                [XmlAttribute("bufTime")]
                public string BufTime { get; set; }

                [XmlAttribute("trgOps")]
                public string TrgOps { get; set; }

                [XmlAttribute("intgPd")]
                public string IntgPd { get; set; }
            }

            public class GSESettings
            {
                [XmlAttribute("cbName")]
                public string CbName { get; set; }

                [XmlAttribute("datSet")]
                public string DatSet { get; set; }

                [XmlAttribute("appID")]
                public string AppID { get; set; }
            }

            public class GOOSE
            {
                [XmlAttribute("max")]
                public int Max { get; set; }
            }

            public class FileHandling
            {
                [XmlAttribute("ftp")]
                public bool Ftp { get; set; }
            }

            public class ConfLNs
            {
            }

            public class ClientServices
            {
                [XmlAttribute("goose")]
                public bool Goose { get; set; }

                [XmlAttribute("maxGOOSE")]
                public int MaxGOOSE { get; set; }

                public TimeSyncProt TimeSyncProt { get; set; }
            }

            public class TimeSyncProt
            {
                [XmlAttribute("other")]
                public bool Other { get; set; }
            }

            public class RedProt
            {
                [XmlAttribute("prp")]
                public bool Prp { get; set; }
            }
        }
        public class AccessPoint
        {
            [XmlAttribute("name")]
            public string _name;
            [XmlAttribute("router")]
            public string _router;
            [XmlAttribute("desc")]
            public string _desc;
            [XmlAttribute("clock")]
            public string _clock;
            [XmlElement("Server")]
            public Server _server = new Server();
        }
        public class Server
        {
            [XmlAttribute("timeout")]
            public string _timeout;
            [XmlAttribute("desc")]
            public string _desc;
            [XmlElement("Authentication")]
            public Authentication _Authentication = new Authentication();

            [XmlIgnore]
            public Dictionary<string, LDevice> _LDeviceDict { get; set; }
            [XmlElement("LDevice")]
            public LDevice[] _LDeviceList
            {
                get => _LDeviceDict.Values.ToArray();
                set => _LDeviceDict = new Dictionary<string, LDevice>(value.ToDictionary(x => x._inst));
            }
        }
        public class LDevice
        {
            [XmlAttribute("inst")]
            public string _inst;
            [XmlElement("LN0")]
            public LN _LN0 = new LN();
            [XmlElement("LN")]
            public List<LN> _LNList = new List<LN>();
        }
        public class LN
        {
            [XmlAttribute("lnType")]
            public string lnType;
            [XmlAttribute("lnClass")]
            public string lnClass;
            [XmlAttribute("inst")]
            public string inst;
            [XmlAttribute("prefix")]
            public string prefix;
            [XmlElement("DataSet")]
            public List<DataSet> _DataSetList = new List<DataSet>();
            [XmlElement("DOI")]
            public List<DOI> _doiList = new List<DOI>();
            [XmlElement("Inputs")]
            public Inputs _inputs = new Inputs();
            [XmlElement("GSEControl")]
            public List<GSEControl> _GSEControlList = new List<GSEControl>();
            [XmlElement("SampledValueControl")]
            public List<SampledValueControl> _SampledValueControlList = new List<SampledValueControl>();
        }
        public class Inputs
        {
            [XmlElement("ExtRef")]
            public List<ExternRef> externRef = new List<ExternRef>();
        }
        public class ExternRef
        {
            public string iedName;
            public string ldInst;
            public string lnClass;
            public string lnInst;
            public string doName;
            public string daName;
            public string prefix;
            public string serviceType;
            public string srcLDInst;
            public string srcLNClass;
            public string srcCBName;
            public string desc;
            public string intAddr;
        }
        public class DOI
        {
            [XmlAttribute("name")]
            public string _name;
            [XmlElement("DAI")]
            public List<DAI> _daiList = new List<DAI>();
        }
        public class DAI
        {
            [XmlAttribute("name")]
            public string _name;
            [XmlElement("Val")]
            public Val _val;
        }
        public class Val
        {
            [XmlText]
            public string _value;
        }
        public class SampledValueControl
        {
            [XmlAttribute("name")]
            public string _name;
            [XmlAttribute("datSet")]
            public string _datSet;
            [XmlAttribute("desc")]
            public string _desc;
            [XmlAttribute("confRev")]
            public string _confRev;
            [XmlAttribute("smvID")]
            public string _smvID;
            [XmlAttribute("multicast")]
            public string _multicast;
            [XmlAttribute("smpRate")]
            public string _smpRate;
            [XmlAttribute("nofASDU")]
            public string _nofASDU;
            [XmlElement("SmvOpts")]
            public SmvOpts _SmvOpts = new SmvOpts();
        }
        public class SmvOpts
        {
            [XmlAttribute("refreshTime")]
            public string _refreshTime;
            [XmlAttribute("sampleSynchronized")]
            public string _sampleSynchronized;
            [XmlAttribute("sampleRate")]
            public string _sampleRate;
            [XmlAttribute("dataSet")]
            public string _dataSet;
            [XmlAttribute("security")]
            public string _security;
        }
        public class GSEControl
        {
            [XmlAttribute("name")]
            public string _name;
            [XmlAttribute("datSet")]
            public string _datSet;
            [XmlAttribute("type")]
            public string _type;
            [XmlAttribute("appID")]
            public string _appID;
            [XmlAttribute("confRev")]
            public string _confRev;
            [XmlAttribute("desc")]
            public string _desc;
        }
        public class DataSet
        {
            [XmlAttribute("name")]
            public string _name;
            [XmlAttribute("desc")]
            public string _desc;
            [XmlElement("FCDA")]
            public List<FCDA> _FCDAList = new List<FCDA>();
        }
        public class FCDA
        {
            [XmlAttribute("ldInst")]
            public string _ldInst;
            [XmlAttribute("prefix")]
            public string _prefix;
            [XmlAttribute("lnClass")]
            public string _lnClass;
            [XmlAttribute("lnInst")]
            public string _lnInst;
            [XmlAttribute("doName")]
            public string _doName;
            [XmlAttribute("daName")]
            public string _daName;
            [XmlAttribute("fc")]
            public string _fc;
        }
        public class Authentication
        {
            [XmlAttribute("none")]
            public string none;
            [XmlAttribute("certificate")]
            public string certificate;
            [XmlAttribute("password")]
            public string password;
            [XmlAttribute("strong")]
            public string strong;
            [XmlAttribute("weak")]
            public string weak;
        }
        public class Header
        {
            [XmlAttribute("id")]
            public string id;
            [XmlAttribute("version")]
            public string version;
            [XmlAttribute("revision")]
            public string revision;
            [XmlAttribute("toolID")]
            public string toolID;
            [XmlAttribute("nameStructure")]
            public string nameStructure;
        }
        public class Communication
        {
            [XmlElement("SubNetwork")]
            public List<SubNetwork> _SubNetwork = new List<SubNetwork>();
        }
        public class SubNetwork
        {
            [XmlAttribute("name")]
            public string _name;
            [XmlElement("BitRate")]
            public bitRate _bitRate = new bitRate();
            [XmlElement("ConnectedAP")]
            public List<ConnectedAP> _ConnectedAP = new List<ConnectedAP>();
        }
        public class ConnectedAP
        {
            [XmlAttribute("iedName")]
            public string _iedName;
            [XmlAttribute("apName")]
            public string _apName;
            [XmlElement("Address")]
            public Address _address = new Address();
            [XmlElement("GSE")]
            public List<GSE> _gseList = new List<GSE>();
            [XmlElement("SMV")]
            public List<SMV> _smvList = new List<SMV>();
            [XmlElement("PhysConn")]
            public PhysConn _PhysConn = new PhysConn();
        }
        public class PhysConn
        {
            [XmlAttribute("type")]
            public string type;
            [XmlElement("P")]
            public List<PClass> _pList = new List<PClass>();
        }
        public class SMV
        {
            [XmlAttribute("ldInst")]
            public string _ldInst;
            [XmlAttribute("cbName")]
            public string _cbName;
            [XmlElement("Address")]
            public Address _address = new Address();
        }
        public class GSE
        {
            [XmlAttribute("ldInst")]
            public string _ldInst;
            [XmlAttribute("cbName")]
            public string _cbName;
            [XmlElement("Address")]
            public Address _address = new Address();
            [XmlElement("MinTime")]
            public bitRate _minTime = new bitRate();
            [XmlElement("MaxTime")]
            public bitRate _maxTime = new bitRate();
        }
        public class Address
        {
            [XmlElement("P")]
            public List<PClass> _p = new List<PClass>();
        }
        public class PClass
        {
            [XmlAttribute("type")]
            public string _type;
            [XmlText]
            public string _value;
        }
        public class bitRate
        {
            [XmlAttribute("unit")]
            public string _unit;
            [XmlAttribute("multiplier")]
            public string _multiplier;
            [XmlText]
            public string _value;
        }
    }

    public class yamlClass 
    {
        public class GeneralSettings
        {
            public string IpAddress { get; set; }
            public int Port { get; set; }
            public double frequecy { get; set; }
            public double VoltageNominal { get; set; }
            public double CurrentNominal { get; set; }
        }

        public class ProtectionFunction
        {
            [YamlMember(Alias = "PIOC Phase")]
            public List<PIOC> PIOCPhase { get; set; }

            [YamlMember(Alias = "PIOC Neutral")]
            public List<PIOC> PIOCNeutral { get; set; }

            [YamlMember(Alias = "PTOC Phase")]
            public List<PTOC> PTOCPhase { get; set; }

            [YamlMember(Alias = "PTOC Neutral")]
            public List<PTOC> PTOCNeutral { get; set; }

            [YamlMember(Alias = "PTOV")]
            public List<PTOV> PTOV { get; set; }

            [YamlMember(Alias = "PTUV")]
            public List<PTUV> PTUV { get; set; }

            [YamlMember(Alias = "PDIS")]
            public List<PDIS> PDIS { get; set; }

            public PDIR PDIR { get; set; }
        }

        public class PIOC
        {
            public bool Enabled { get; set; }
            public double Pickup { get; set; }
            [YamlMember(Alias = "Time Delay")]
            public double TimeDelay { get; set; }
        }

        public class PTOV
        {
            public bool Enabled { get; set; }
            public double Pickup { get; set; }
            [YamlMember(Alias = "Time Delay")]
            public double TimeDelay { get; set; }
        }

        public class PTUV
        {
            public bool Enabled { get; set; }
            public double Pickup { get; set; }
            [YamlMember(Alias = "Time Delay")]
            public double TimeDelay { get; set; }
        }

        public class PTOC
        {
            public bool Enabled { get; set; }
            public double Pickup { get; set; }
            [YamlMember(Alias = "Time Dial")]
            public double TimeDial { get; set; }
            public string Curve { get; set; }
        }

        public class PDIS
        {
            public bool Enabled { get; set; }
            public double Ajuste { get; set; }
            public double Angle { get; set; }
            [YamlMember(Alias = "Time Delay")]
            public double TimeDelay { get; set; }
            public string   Type { get; set; }
        }

        public class PDIR
        {
            public bool Enabled { get; set; }
            public double Pickup { get; set; }
            public double Angle { get; set; }
            public string Polarity { get; set; }
        }

        public class Logic
        {
            public String Name { get; set; }
        }
        
        public class GOOSE
        {
            public string macDst { get; set; }
            public string goId { get; set; }
            public uint appId { get; set; }
            public uint vLanId { get; set; }
            public uint vLanPriority { get; set; }
            public uint minTime { get; set; }
            public uint maxTime { get; set; }
            public string cbRef { get; set; }
            public uint confRef { get; set; }
            public string dataSetName { get; set; }
            public List<string> dataSet { get; set; }
        }

        public class Sniffer
        {
            public float Pps { get; set; }
            public double Frequency { get; set; }
            public float Ppc { get; set; }
            public string MacDst { get; set; }
            public string MacIntern { get; set; }
            public string Iface { get; set; }
            public string IfaceIntern { get; set; }
            public string SvId { get; set; }
            public int AppId { get; set; }
            public int ConfRev { get; set; }
            public int NoAsdu { get; set; }
            public int Vlan { get; set; }
        }

        public class Configuration
        {
            [YamlMember(Alias = "General Settings")]
            public GeneralSettings GeneralSettings { get; set; }

            [YamlMember(Alias = "Protection Function")]
            public ProtectionFunction ProtectionFunction { get; set; }

            [YamlMember(Alias = "Goose")]
            public List<GOOSE> Goose { get; set; }

            [YamlMember(Alias = "Logic")]
            public Logic Logic { get; set; }

            [YamlMember(Alias = "Sniffer")]
            public Sniffer Sniffer { get; set; }
        }
    }

    public class SCLConfig
    {
        public SCL scl { get; set; }

        private tLN createLN(string lnType, string lnClass, string inst, string prefix)
        {
            var x = new tLN
            {
                lnType = lnType,
                lnClass = lnClass,
                inst = inst,
                prefix = prefix,
                DOI = new tDOI[]
                {
                    new tDOI {
                        name = "Mod",
                        Items = new tUnNaming[] {
                            new tDAI { name = "stVal"},
                            new tDAI { name = "q"},
                            new tDAI { name = "cltModel"},
                        }
                    },
                    new tDOI {
                        name = "Health",
                        Items = new tUnNaming[] {
                            new tDAI { name = "stVal"},
                            new tDAI { name = "q"}
                        }
                    },
                    new tDOI {
                        name = "Beh",
                        Items = new tUnNaming[] {
                            new tDAI { name = "stVal"},
                            new tDAI { name = "q"}
                        }
                    },
                    new tDOI{
                        name = "NamPlt",
                        Items = new tUnNaming[] {
                            new tDAI { name = "vendor"},
                            new tDAI { name = "swRev"},
                            new tDAI { name = "d"},
                            new tDAI { name = "configRev"},
                            new tDAI { name = "ldNs"},
                        }
                    },
                    new tDOI
                    {
                        name = "Str",
                        Items = new tUnNaming[] {
                            new tDAI { name = "general"},
                            new tDAI { name = "dirGeneral"},
                            new tDAI { name = "q"}
                        }
                    },
                    new tDOI
                    {
                        name = "Op",
                        Items = new tUnNaming[] {
                            new tDAI { name = "general"},
                            new tDAI { name = "q"}
                        }
                    }
                }
            };
            return x;
        }

        public Dictionary<string, tLDevice> ldDevice;

        public void defaultConfig()
        {
            var x = new SCL();
            var ip = "10.3.193.100";
            var ip_subnet = "255.255.255.0";
            var ip_gatway = "10.3.192.1";
            var iedName = "vIED_0";
            var ap = "vNet";
            

            x.Header = new tHeader()
            {
                id = "Virtual IED",
                version = "1",
                revision = "1.0",
                toolID = "UFU_vSCL"
            };
            x.Communication = new tCommunication
            {
                SubNetwork = new tSubNetwork[]
                {
                    new tSubNetwork
                    {
                        name = "vNetwork",
                        ConnectedAP = new tConnectedAP[]
                        {
                            new tConnectedAP
                            {
                                Address = new tP[]
                                {
                                    new tP{type = "IP", Value = ip},
                                    new tP{type = "IP-SUBNET", Value = ip_subnet},
                                    new tP{type = "IP-GATEWAY", Value = ip_gatway},
                                    new tP{type = "OSI-TSEL", Value = "0001"},
                                    new tP{type = "OSI-PSEL", Value = "00000001"},
                                    new tP{type = "OSI-SSEL", Value = "0001"},
                                },
                                iedName = iedName,
                                apName = ap,
                            }
                        }
                    }
                }
            };
            x.IED = new tIED[]
            {
                new tIED
                {
                    name = iedName,
                    type = "vIED",
                    manufacturer = "LRI",
                    configVersion = "1.0",
                    AccessPoint = new tAccessPoint[]
                    {
                        new tAccessPoint
                        {
                            name = ap,
                            Items = new[]
                            {
                                new tServer
                                {
                                    Authentication = new tServerAuthentication{none = true},
                                    LDevice = new tLDevice[]
                                    {
                                        new tLDevice
                                        {
                                            inst = "CFG",
                                            desc = "Data Sets",
                                        },
                                        new tLDevice
                                        {
                                            inst = "PROT",
                                            desc = "Protection"
                                        },
                                        new tLDevice
                                        {
                                            inst = "MET",
                                            desc = "Metering"
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            };

            LN0 baseLN0 = new LN0
            {
                lnClass = "LLN0",
                lnType = "LN0_1",
                inst = "",
                DOI = new tDOI[]
                {
                    new tDOI {
                        name = "Mod",
                        Items = new tUnNaming[] {
                            new tDAI { name = "stVal"},
                            new tDAI { name = "q"},
                            new tDAI { name = "cltModel"},
                        }
                    },
                    new tDOI {
                        name = "Health",
                        Items = new tUnNaming[] {
                            new tDAI { name = "stVal"},
                            new tDAI { name = "q"}
                        }
                    },
                    new tDOI {
                        name = "Beh",
                        Items = new tUnNaming[] {
                            new tDAI { name = "stVal"},
                            new tDAI { name = "q"}
                        }
                    },
                    new tDOI{
                        name = "NamPlt",
                        Items = new tUnNaming[] {
                            new tDAI { name = "vendor"},
                            new tDAI { name = "swRev"},
                            new tDAI { name = "d"},
                            new tDAI { name = "configRev"},
                            new tDAI { name = "ldNs"},
                        }
                    }
                }

            };
            tLN lnLPHD = new tLN()
            {
                lnClass = "LPHD",
                lnType = "LPHD_1",
                inst = "1",
                prefix = "vID",
                DOI = new tDOI[]
                {
                    new tDOI {
                        name = "PhyNam",
                        Items = new tUnNaming[] {
                            new tDAI { name = "vendor"},
                            new tDAI { name = "serNum"},
                            new tDAI { name = "model"},
                        }
                    },
                    new tDOI {
                        name = "PhyHealth",
                        Items = new tUnNaming[] {
                            new tDAI { name = "stVal"},
                            new tDAI { name = "q"}
                        }
                    },
                    new tDOI {
                        name = "Proxy",
                        Items = new tUnNaming[] {
                            new tDAI { name = "stVal"},
                            new tDAI { name = "q"}
                        }
                    },
                }
            };

            tServer server = (tServer)x.IED[0].AccessPoint[0].Items[0];
            tLDevice cfg = server.LDevice.FirstOrDefault(ld => ld.inst == "CFG");
            cfg.LN0 = new LN0
            {
                lnClass = "LLN0",
                lnType = "LN0_1",
                inst = "",
                DOI = new tDOI[]
                {
                    new tDOI {
                        name = "Mod",
                        Items = new tUnNaming[] {
                            new tDAI { name = "stVal"},
                            new tDAI { name = "q"},
                            new tDAI { name = "cltModel"},
                        }
                    },
                    new tDOI {
                        name = "Health",
                        Items = new tUnNaming[] {
                            new tDAI { name = "stVal"},
                            new tDAI { name = "q"}
                        }
                    },
                    new tDOI {
                        name = "Beh",
                        Items = new tUnNaming[] {
                            new tDAI { name = "stVal"},
                            new tDAI { name = "q"}
                        }
                    },
                    new tDOI{
                        name = "NamPlt",
                        Items = new tUnNaming[] {
                            new tDAI { name = "vendor"},
                            new tDAI { name = "swRev"},
                            new tDAI { name = "d"},
                            new tDAI { name = "configRev"},
                            new tDAI { name = "ldNs"},
                        }
                    }
                }
            };
            cfg.LN = new tLN[]{
                lnLPHD
            };

            tLDevice prot = server.LDevice.FirstOrDefault(ld => ld.inst == "PROT");
            prot.LN0 = baseLN0;
            prot.LN = new tLN[]{
                lnLPHD,
                // 50
                createLN("PIOC_1", "PIOC", "1", "P1"),
                createLN("PIOC_1", "PIOC", "2", "P2"),
                createLN("PIOC_1", "PIOC", "3", "P3"),
                createLN("PIOC_1", "PIOC", "4", "N1"),
                createLN("PIOC_1", "PIOC", "5", "N2"),
                createLN("PIOC_1", "PIOC", "6", "N3"),
                // 51
                createLN("PTOC_1", "PTOC", "1", "P1"),
                createLN("PTOC_1", "PTOC", "2", "P2"),
                createLN("PTOC_1", "PTOC", "3", "P3"),
                createLN("PTOC_1", "PTOC", "4", "N1"),
                createLN("PTOC_1", "PTOC", "5", "N2"),
                createLN("PTOC_1", "PTOC", "6", "N3"),
                // Trip
                new tLN{
                    lnType = "PTRC_1",
                    lnClass = "PTRC",
                    inst = "1",
                    prefix = "TRIP"
                }

            };

            tLDevice met = server.LDevice.FirstOrDefault(ld => ld.inst == "MET");
            met.LN0 = baseLN0;
            met.LN = new tLN[]{
                lnLPHD,
                new tLN
                {
                    lnType = "MMXU_1",
                    lnClass = "MMXU",
                    inst = "1",
                    prefix = "MET",
                    DOI = new tDOI[]
                    {
                        new tDOI {
                        name = "Mod",
                        Items = new tUnNaming[] {
                            new tDAI { name = "stVal"},
                            new tDAI { name = "q"},
                            new tDAI { name = "cltModel"},
                        }
                    },
                        new tDOI {
                            name = "Health",
                            Items = new tUnNaming[] {
                                new tDAI { name = "stVal"},
                                new tDAI { name = "q"}
                            }
                        },
                        new tDOI {
                            name = "Beh",
                            Items = new tUnNaming[] {
                                new tDAI { name = "stVal"},
                                new tDAI { name = "q"}
                            }
                        },
                        new tDOI{
                            name = "NamPlt",
                            Items = new tUnNaming[] {
                                new tDAI { name = "vendor"},
                                new tDAI { name = "swRev"},
                                new tDAI { name = "d"},
                                new tDAI { name = "configRev"},
                                new tDAI { name = "ldNs"},
                            }
                        },
                        new tDOI{
                            name = "TotW",
                            Items = new tUnNaming[] {
                                new tSDI
                                {
                                    name = "instMag",
                                    Items = new tUnNaming[]
                                    {
                                        new tDAI{name = "f"}
                                    }
                                },
                                new tDAI { name = "q"},
                                new tSDI
                                {
                                    name = "units",
                                    Items = new tUnNaming[]
                                    {
                                        new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Watts"} }},
                                        new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="k"} }}
                                    }
                                },
                                new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="100"} } },
                            }
                        },
                        new tDOI{
                            name = "TotVAr",
                            Items = new tUnNaming[] {
                                new tSDI
                                {
                                    name = "instMag",
                                    Items = new tUnNaming[]
                                    {
                                        new tDAI{name = "f"}
                                    }
                                },
                                new tDAI { name = "q"},
                                new tSDI
                                {
                                    name = "units",
                                    Items = new tUnNaming[]
                                    {
                                        new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="VAr"} }},
                                        new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="k"} }}
                                    }
                                },
                                new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="100"} } },
                            }
                        },
                        new tDOI{
                            name = "TotVA",
                            Items = new tUnNaming[] {
                                new tSDI
                                {
                                    name = "instMag",
                                    Items = new tUnNaming[]
                                    {
                                        new tDAI{name = "f"}
                                    }
                                },
                                new tDAI { name = "q"},
                                new tSDI
                                {
                                    name = "units",
                                    Items = new tUnNaming[]
                                    {
                                        new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="VA"} }},
                                        new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="k"} }}
                                    }
                                },
                                new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="100"} } },
                            }
                        },
                        new tDOI{
                            name = "Hz",
                            Items = new tUnNaming[] {
                                new tSDI
                                {
                                    name = "instMag",
                                    Items = new tUnNaming[]
                                    {
                                        new tDAI{name = "f"}
                                    }
                                },
                                new tDAI { name = "q"},
                                new tSDI
                                {
                                    name = "units",
                                    Items = new tUnNaming[]
                                    {
                                        new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                        new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                    }
                                },
                                new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="500"} } },
                            }
                        },
                        new tDOI{
                            name = "PPV",
                            Items = new tUnNaming[] {
                                new tSDI
                                {
                                    name = "phsAB",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                                new tSDI
                                {
                                    name = "phsBC",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                                new tSDI
                                {
                                    name = "phsCA",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                            }
                        },
                        new tDOI{
                            name = "PhV",
                            Items = new tUnNaming[] {
                                new tSDI
                                {
                                    name = "phsA",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                                new tSDI
                                {
                                    name = "phsB",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                                new tSDI
                                {
                                    name = "phsC",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                                new tSDI
                                {
                                    name = "res",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                            }
                        },
                        new tDOI{
                            name = "A1",
                            Items = new tUnNaming[] {
                                new tSDI
                                {
                                    name = "phsA",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                                new tSDI
                                {
                                    name = "phsB",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                                new tSDI
                                {
                                    name = "phsC",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                                new tSDI
                                {
                                    name = "res",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                            }
                        },
                        new tDOI{
                            name = "A2",
                            Items = new tUnNaming[] {
                                new tSDI
                                {
                                    name = "phsA",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                                new tSDI
                                {
                                    name = "phsB",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                                new tSDI
                                {
                                    name = "phsC",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                                new tSDI
                                {
                                    name = "res",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                            }
                        },
                        new tDOI{
                            name = "A",
                            Items = new tUnNaming[] {
                                new tSDI
                                {
                                    name = "neut",
                                    Items = new tUnNaming[]
                                    {
                                        new tSDI{
                                            name = "instCVal",
                                            Items = new tUnNaming[]
                                            {
                                                new tSDI {name = "mag", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                                new tSDI {name = "ang", Items = new tUnNaming[]{ new tDAI { name = "f"} } },
                                            }
                                        },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="V"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value=""} }}
                                            }
                                        },
                                        new tDAI { name = "q"},
                                        new tDAI { name = "db", Val = new tVal[]{ new tVal {Value="50"} } },
                                        new tSDI
                                        {
                                            name = "units",
                                            Items = new tUnNaming[]
                                            {
                                                new tDAI{name = "SIUnit", Val = new tVal[]{ new tVal {Value="Hz"} }},
                                                new tDAI{name = "multiplier", Val = new tVal[]{ new tVal {Value="50"} }}
                                            }
                                        },
                                    }
                                },
                            }
                        },
                    }
                }
            };

            ldDevice = new Dictionary<string, tLDevice>()
            {
                {"cfg", cfg },
                {"prot", prot },
                {"met", met }
            };

            var lnType = new tLNodeType[]
            {
                new tLNodeType
                {
                    id = "LN0_1",
                    lnClass = "LLN0",
                    DO = new tDO[]
                    {
                        new tDO{name = "Mod", type= "modENC"},
                        new tDO{name = "Beh", type= "behENS"},
                        new tDO{name = "Health", type= "healthENS"},
                        new tDO{name = "NamPlt", type= "LPL_LN0"},
                    }
                },
                new tLNodeType
                {
                    id = "LPHD_1",
                    lnClass = "LPHD",
                    DO = new tDO[]
                    {
                        new tDO{name = "PhyNam", type= "DPL_1"},
                        new tDO{name = "PhyHealth", type= "healthENS"},
                        new tDO{name = "Proxy", type= "SPS_1"},
                    }
                },
                new tLNodeType
                {
                    id = "PIOC_1",
                    lnClass = "PIOC",
                    desc = "ANSI 50",
                    DO = new tDO[]
                    {
                        new tDO{name = "Mod", type= "modENC"},
                        new tDO{name = "Beh", type= "behENS"},
                        new tDO{name = "Health", type= "healthENS"},
                        new tDO{name = "NamPlt", type= "LPL_1"},
                        new tDO{name = "Str", type= "ACD_1"},
                        new tDO{name = "Op", type= "ACT_1"}
                    }
                },
                new tLNodeType
                {
                    id = "PTOC_1",
                    lnClass = "PTOC",
                    desc = "ANSI 51",
                    DO = new tDO[]
                    {
                        new tDO{name = "Mod", type= "modENC"},
                        new tDO{name = "Beh", type= "behENS"},
                        new tDO{name = "Health", type= "healthENS"},
                        new tDO{name = "NamPlt", type= "LPL_1"},
                        new tDO{name = "Str", type= "ACD_1"},
                        new tDO{name = "Op", type= "ACT_1"}
                    }
                },
                new tLNodeType
                {
                    id = "MMXU_1",
                    lnClass = "MMXU",
                    desc = "ANSI 51",
                    DO = new tDO[]
                    {
                        new tDO{name = "Mod", type= "modENC"},
                        new tDO{name = "Beh", type= "behENS"},
                        new tDO{name = "Health", type= "healthENS"},
                        new tDO{name = "NamPlt", type= "LPL_1"},
                        new tDO{name = "TotW", type= "analogMV_1"},
                        new tDO{name = "TotVAr", type= "analogMV_1"},
                        new tDO{name = "TotVA", type= "analogMV_1"},
                        new tDO{name = "Hz", type= "analogMV_1"},
                        new tDO{name = "PPV", type= "DEL_1"},
                        new tDO{name = "PhV", type= "WYE_1"},
                        new tDO{name = "A1", type= "WYE_1"},
                        new tDO{name = "A2", type= "WYE_1"},
                        new tDO{name = "A", type= "WYE_2"},
                    }
                },
                new tLNodeType
                {
                    id = "PTRC_1",
                    lnClass = "PTRC",
                    desc = "Trip",
                    DO = new tDO[]
                    {
                        new tDO{name = "Mod", type= "modENC"},
                        new tDO{name = "Beh", type= "behENS"},
                        new tDO{name = "Health", type= "healthENS"},
                        new tDO{name = "NamPlt", type= "LPL_1"},
                        new tDO{name = "Tr", type= "ACT_1"},
                        new tDO{name = "Str", type= "ACD_1"},
                        new tDO{name = "Op", type= "ACT_1"}
                    }
                },
            };

            var doType = new tDOType[]
            {
                new tDOType
                {
                    id = "modENC",
                    cdc = tCDCEnum.ENC,
                    Items  = new tDA[]
                    {
                        new tDA{name = "stVal", bType = tBasicTypeEnum.Enum, type="Mod", dchg = true, fc= tFCEnum.ST},
                        new tDA{name = "q", bType = tBasicTypeEnum.Quality, qchg = true, fc= tFCEnum.ST},
                        new tDA{name = "t", bType = tBasicTypeEnum.Timestamp, fc= tFCEnum.ST},
                        new tDA{name = "ctlModel", bType = tBasicTypeEnum.Enum,type="ctlModel", fc= tFCEnum.CF},
                    }
                },
                new tDOType
                {
                    id = "behENS",
                    cdc = tCDCEnum.ENS,
                    Items  = new tDA[]
                    {
                        new tDA{name = "stVal", bType = tBasicTypeEnum.Enum, type="Beh", dchg = true, fc= tFCEnum.ST},
                        new tDA{name = "q", bType = tBasicTypeEnum.Quality, qchg = true, fc= tFCEnum.ST},
                        new tDA{name = "t", bType = tBasicTypeEnum.Timestamp, fc= tFCEnum.ST},
                    }
                },
                new tDOType
                {
                    id = "healthENS",
                    cdc = tCDCEnum.ENS,
                    Items  = new tDA[]
                    {
                        new tDA{name = "stVal", bType = tBasicTypeEnum.Enum, type="Health", dchg = true, fc= tFCEnum.ST},
                        new tDA{name = "q", bType = tBasicTypeEnum.Quality, qchg = true, fc= tFCEnum.ST},
                        new tDA{name = "t", bType = tBasicTypeEnum.Timestamp, fc= tFCEnum.ST},
                    }
                },
                new tDOType
                {
                    id = "LPL_LN0",
                    cdc = tCDCEnum.LPL,
                    Items  = new tDA[]
                    {
                        new tDA{name = "vendor", bType = tBasicTypeEnum.VisString255, fc= tFCEnum.DC},
                        new tDA{name = "swRev", bType = tBasicTypeEnum.VisString255, fc= tFCEnum.DC},
                        new tDA{name = "d", bType = tBasicTypeEnum.VisString255, fc= tFCEnum.DC},
                        new tDA{name = "configRev", bType = tBasicTypeEnum.VisString255, fc= tFCEnum.DC},
                        new tDA{name = "ldNs", bType = tBasicTypeEnum.VisString255, fc= tFCEnum.EX},
                    }
                },
                new tDOType
                {
                    id = "DPL_1",
                    cdc = tCDCEnum.DPL,
                    Items  = new tDA[]
                    {
                        new tDA{name = "vendor", bType = tBasicTypeEnum.VisString255, fc= tFCEnum.DC},
                        new tDA{name = "serNum", bType = tBasicTypeEnum.VisString255, fc= tFCEnum.DC},
                        new tDA{name = "model", bType = tBasicTypeEnum.VisString255, fc= tFCEnum.DC},
                    }
                },
                new tDOType
                {
                    id = "SPS_1",
                    cdc = tCDCEnum.SPS,
                    Items  = new tDA[]
                    {
                        new tDA{name = "stVal", bType = tBasicTypeEnum.BOOLEAN, dchg = true, fc= tFCEnum.ST},
                        new tDA{name = "q", bType = tBasicTypeEnum.Quality, qchg = true, fc= tFCEnum.ST},
                        new tDA{name = "t", bType = tBasicTypeEnum.Timestamp, fc= tFCEnum.ST},
                    }
                },
                new tDOType
                {
                    id = "LPL_1",
                    cdc = tCDCEnum.LPL,
                    Items  = new tDA[]
                    {
                        new tDA{name = "vendor", bType = tBasicTypeEnum.VisString255, fc= tFCEnum.DC},
                        new tDA{name = "swRev", bType = tBasicTypeEnum.VisString255, fc= tFCEnum.DC},
                        new tDA{name = "d", bType = tBasicTypeEnum.VisString255, fc= tFCEnum.DC},
                        new tDA{name = "configRev", bType = tBasicTypeEnum.VisString255, fc= tFCEnum.DC},
                    }
                },
                new tDOType
                {
                    id = "ACD_1",
                    cdc = tCDCEnum.ACD,
                    Items  = new tDA[]
                    {
                        new tDA{name = "general", bType = tBasicTypeEnum.BOOLEAN, dchg = true, fc= tFCEnum.ST},
                        new tDA{name = "dirGeneral", bType = tBasicTypeEnum.Enum, type= "dir", dchg = true, fc= tFCEnum.ST},
                        new tDA{name = "q", bType = tBasicTypeEnum.Quality, qchg = true, fc= tFCEnum.ST},
                        new tDA{name = "t", bType = tBasicTypeEnum.Timestamp, fc= tFCEnum.ST},
                        new tDA{name = "d", bType = tBasicTypeEnum.VisString255, fc= tFCEnum.DC},
                    }
                },
                new tDOType
                {
                    id = "ACT_1",
                    cdc = tCDCEnum.ACT,
                    Items  = new tDA[]
                    {
                        new tDA{name = "general", bType = tBasicTypeEnum.BOOLEAN, dchg = true, fc= tFCEnum.ST},
                        new tDA{name = "q", bType = tBasicTypeEnum.Quality, qchg = true, fc= tFCEnum.ST},
                        new tDA{name = "t", bType = tBasicTypeEnum.Timestamp, fc= tFCEnum.ST},
                        new tDA{name = "d", bType = tBasicTypeEnum.VisString255, fc= tFCEnum.DC},
                    }
                },
                new tDOType
                {
                    id = "analogMV_1",
                    cdc = tCDCEnum.MV,
                    Items  = new tDA[]
                    {
                        new tDA{name = "instMag", bType = tBasicTypeEnum.Struct, type="AnalogValue_1", fc= tFCEnum.MX},
                        new tDA{name = "mag", bType = tBasicTypeEnum.Struct, type="AnalogValue_1",dchg=true, fc= tFCEnum.MX},
                        new tDA{name = "q", bType = tBasicTypeEnum.Quality, qchg = true, fc= tFCEnum.MX},
                        new tDA{name = "t", bType = tBasicTypeEnum.Timestamp, fc= tFCEnum.MX},
                        new tDA{name = "units", bType = tBasicTypeEnum.Struct, type="Units_1", fc= tFCEnum.CF},
                        new tDA{name = "db", bType = tBasicTypeEnum.INT32U, fc= tFCEnum.CF},
                        new tDA{name = "d", bType = tBasicTypeEnum.VisString255, fc= tFCEnum.DC},
                    }
                },
                new tDOType
                {
                    id = "DEL_1",
                    cdc = tCDCEnum.DEL,
                    Items  = new tSDO[]
                    {
                        new tSDO{name = "phsAB", type = "CMV_1"},
                        new tSDO{name = "phsBC", type = "CMV_1"},
                        new tSDO{name = "phsCA", type = "CMV_1"},
                    }
                },
                new tDOType
                {
                    id = "CMV_1",
                    cdc = tCDCEnum.CMV,
                    Items  = new tDA[]
                    {
                        new tDA{name = "instCVal", bType = tBasicTypeEnum.Struct, type="Vector_1", fc= tFCEnum.MX},
                        new tDA{name = "cVal", bType = tBasicTypeEnum.Struct, type="Vector_1",dchg=true, fc= tFCEnum.MX},
                        new tDA{name = "q", bType = tBasicTypeEnum.Quality, qchg = true, fc= tFCEnum.MX},
                        new tDA{name = "t", bType = tBasicTypeEnum.Timestamp, fc= tFCEnum.MX},
                        new tDA{name = "units", bType = tBasicTypeEnum.Struct, type="Units_1", fc= tFCEnum.CF},
                        new tDA{name = "db", bType = tBasicTypeEnum.INT32U, fc= tFCEnum.CF},
                    }
                },
                new tDOType
                {
                    id = "WYE_1",
                    cdc = tCDCEnum.WYE,
                    Items  = new tSDO[]
                    {
                        new tSDO{name = "phsA", type = "CMV_1"},
                        new tSDO{name = "phsB", type = "CMV_1"},
                        new tSDO{name = "phsC", type = "CMV_1"},
                        new tSDO{name = "res", type = "CMV_1"},
                    }
                },
                new tDOType
                {
                    id = "WYE_2",
                    cdc = tCDCEnum.WYE,
                    Items  = new tSDO[]
                    {
                        new tSDO{name = "neut", type = "CMV_1"},
                    }
                },

            };

            var daType = new tDAType[]
            {
                new tDAType
                {
                    id = "AnalogValue_1",
                    BDA = new tBDA[]
                    {
                        new tBDA{name = "f", bType = tBasicTypeEnum.FLOAT32}
                    }
                },
                new tDAType
                {
                    id = "Units_1",
                    BDA = new tBDA[]
                    {
                        new tBDA{name = "SIUnit", bType = tBasicTypeEnum.Enum, type = "SIUnit_1"},
                        new tBDA{name = "multiplier", bType = tBasicTypeEnum.Enum, type = "multiplier"}
                    }
                },
                new tDAType
                {
                    id = "Vector_1",
                    BDA = new tBDA[]
                    {
                        new tBDA{name = "mag", bType = tBasicTypeEnum.Struct, type = "AnalogValue_1"},
                        new tBDA{name = "ang", bType = tBasicTypeEnum.Struct, type = "AnalogValue_1"}
                    }
                }
            };

            var enumType = new tEnumType[]
            {
                new tEnumType
                {
                    id = "Mod",
                    EnumVal = new tEnumVal[]
                    {
                        new tEnumVal{ord = 1, Value = "on"},
                        new tEnumVal{ord = 2, Value = "blocked"},
                        new tEnumVal{ord = 3, Value = "test"},
                        new tEnumVal{ord = 4, Value = "test/blocked"},
                        new tEnumVal{ord = 5, Value = "off"},
                    }
                },
                new tEnumType
                {
                    id = "ctlModel",
                    EnumVal = new tEnumVal[]
                    {
                        new tEnumVal{ord = 1, Value = "status-only"},
                        new tEnumVal{ord = 2, Value = "direct-with-normal-security"},
                        new tEnumVal{ord = 3, Value = "sbo-with-normal-security"},
                        new tEnumVal{ord = 4, Value = "direct-with-enhanced-security"},
                        new tEnumVal{ord = 5, Value = "sbo-with-enhanced-security"},
                    }
                },
                new tEnumType
                {
                    id = "Beh",
                    EnumVal = new tEnumVal[]
                    {
                        new tEnumVal{ord = 1, Value = "on"},
                        new tEnumVal{ord = 2, Value = "blocked"},
                        new tEnumVal{ord = 3, Value = "test"},
                        new tEnumVal{ord = 4, Value = "test/blocked"},
                        new tEnumVal{ord = 5, Value = "off"},
                    }
                },
                new tEnumType
                {
                    id = "Health",
                    EnumVal = new tEnumVal[]
                    {
                        new tEnumVal{ord = 1, Value = "Ok"},
                        new tEnumVal{ord = 2, Value = "Warning"},
                        new tEnumVal{ord = 3, Value = "Alarm"}
                    }
                },
                new tEnumType
                {
                    id = "dir",
                    EnumVal = new tEnumVal[]
                    {
                        new tEnumVal{ord = 0, Value = "unknown"},
                        new tEnumVal{ord = 1, Value = "forward"},
                        new tEnumVal{ord = 2, Value = "backward"},
                        new tEnumVal{ord = 3, Value = "both"}
                    }
                },
                new tEnumType
                {
                    id = "SIUnit_1",
                    EnumVal = new tEnumVal[]
                    {
                        new tEnumVal { ord = 1, Value = "" },
                        new tEnumVal { ord = 2, Value = "m" },
                        new tEnumVal { ord = 3, Value = "kg" },
                        new tEnumVal { ord = 4, Value = "s" },
                        new tEnumVal { ord = 5, Value = "A" },
                        new tEnumVal { ord = 6, Value = "K" },
                        new tEnumVal { ord = 7, Value = "mol" },
                        new tEnumVal { ord = 8, Value = "cd" },
                        new tEnumVal { ord = 9, Value = "deg" },
                        new tEnumVal { ord = 10, Value = "rad" },
                        new tEnumVal { ord = 11, Value = "sr" },
                        new tEnumVal { ord = 21, Value = "Gy" },
                        new tEnumVal { ord = 22, Value = "q" },
                        new tEnumVal { ord = 23, Value = "°C" },
                        new tEnumVal { ord = 24, Value = "Sv" },
                        new tEnumVal { ord = 25, Value = "F" },
                        new tEnumVal { ord = 26, Value = "C" },
                        new tEnumVal { ord = 27, Value = "S" },
                        new tEnumVal { ord = 28, Value = "H" },
                        new tEnumVal { ord = 29, Value = "V" },
                        new tEnumVal { ord = 30, Value = "ohm" },
                        new tEnumVal { ord = 31, Value = "J" },
                        new tEnumVal { ord = 32, Value = "N" },
                        new tEnumVal { ord = 33, Value = "Hz" },
                        new tEnumVal { ord = 34, Value = "lx" },
                        new tEnumVal { ord = 35, Value = "Lm" },
                        new tEnumVal { ord = 36, Value = "Wb" },
                        new tEnumVal { ord = 37, Value = "T" },
                        new tEnumVal { ord = 38, Value = "W" },
                        new tEnumVal { ord = 39, Value = "Pa" },
                        new tEnumVal { ord = 41, Value = "m²" },
                        new tEnumVal { ord = 42, Value = "m³" },
                        new tEnumVal { ord = 43, Value = "m/s" },
                        new tEnumVal { ord = 44, Value = "m/s²" },
                        new tEnumVal { ord = 45, Value = "m³/s" },
                        new tEnumVal { ord = 46, Value = "m/m³" },
                        new tEnumVal { ord = 47, Value = "M" },
                        new tEnumVal { ord = 48, Value = "kg/m³" },
                        new tEnumVal { ord = 49, Value = "m²/s" },
                        new tEnumVal { ord = 50, Value = "W/m K" },
                        new tEnumVal { ord = 51, Value = "J/K" },
                        new tEnumVal { ord = 52, Value = "ppm" },
                        new tEnumVal { ord = 53, Value = "1/s" },
                        new tEnumVal { ord = 54, Value = "rad/s" },
                        new tEnumVal { ord = 61, Value = "VA" },
                        new tEnumVal { ord = 62, Value = "Watts" },
                        new tEnumVal { ord = 63, Value = "VAr" },
                        new tEnumVal { ord = 64, Value = "phi" },
                        new tEnumVal { ord = 65, Value = "cos(phi)" },
                        new tEnumVal { ord = 66, Value = "Vs" },
                        new tEnumVal { ord = 67, Value = "V²" },
                        new tEnumVal { ord = 68, Value = "As" },
                        new tEnumVal { ord = 69, Value = "A²" },
                        new tEnumVal { ord = 70, Value = "A²t" },
                        new tEnumVal { ord = 71, Value = "VAh" },
                        new tEnumVal { ord = 72, Value = "Wh" },
                        new tEnumVal { ord = 73, Value = "VArh" },
                        new tEnumVal { ord = 74, Value = "V/Hz" },
                    }
                },
                new tEnumType
                {
                    id = "multiplier",
                    EnumVal = new tEnumVal[]
                    {
                        new tEnumVal { ord = -24, Value = "y" },
                        new tEnumVal { ord = -21, Value = "z" },
                        new tEnumVal { ord = -18, Value = "a" },
                        new tEnumVal { ord = -15, Value = "f" },
                        new tEnumVal { ord = -12, Value = "p" },
                        new tEnumVal { ord = -9, Value = "n" },
                        new tEnumVal { ord = -6, Value = "µ" },
                        new tEnumVal { ord = -3, Value = "m" },
                        new tEnumVal { ord = -2, Value = "c" },
                        new tEnumVal { ord = -1, Value = "d" },
                        new tEnumVal { ord = 0, Value = "" },
                        new tEnumVal { ord = 1, Value = "da" },
                        new tEnumVal { ord = 2, Value = "h" },
                        new tEnumVal { ord = 3, Value = "k" },
                        new tEnumVal { ord = 6, Value = "M" },
                        new tEnumVal { ord = 9, Value = "G" },
                        new tEnumVal { ord = 12, Value = "T" },
                        new tEnumVal { ord = 15, Value = "P" },
                        new tEnumVal { ord = 18, Value = "E" },
                        new tEnumVal { ord = 21, Value = "Z" },
                        new tEnumVal { ord = 24, Value = "Y" }
                    }
                },
            };

            var dat = new tDataTypeTemplates()
            {
                LNodeType = lnType,
                DOType = doType,
                DAType = daType,
                EnumType = enumType
            };

            x.DataTypeTemplates = dat;

            this.scl = x;
        }

        public void saveSCL(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SCL));

            using (TextWriter writer = new StreamWriter(@"C:\Users\ALAILTON\My Drive\Faculdade\TCC\Código\Visual Studio\vIed.scd"))
            {
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");
                serializer.Serialize(writer, this.scl, namespaces);
            }
        }
    }

}
