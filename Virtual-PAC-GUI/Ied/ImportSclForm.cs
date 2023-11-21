using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using YamlDotNet.Core;

namespace TestSet
{
    public partial class ImportSclForm : Form
    {

        public enum formType
        {
            SampledValue = 0x01,
            GOOSE = 0x02
        }

        private formType thisFormType;
        private dataSCL returnData;
        public dataSCL openExt(string filePath, formType type)
        {
            this.filePath = filePath;
            thisFormType = type;
            if (File.Exists(filePath))
            {
                if (!LoadSCL(filePath)) return null;
            }
            else
                return null;

            ShowDialog();
            return returnData;
        }

        public ImportSclForm()
        {
            InitializeComponent();
        }

        #region FormConfig

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private Size formSize;
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
            this.WindowState = FormWindowState.Minimized;
            BtnResize.Image = null;
        }
        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                formSize = this.ClientSize;
                this.WindowState = FormWindowState.Maximized;
                BtnResize.Image = Image.FromFile("C:\\Users\\ALAILTON\\My Drive\\Faculdade\\TCC\\Código\\Visual Studio\\Images\\inside.png");
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = formSize;
                BtnResize.Image = Image.FromFile("C:\\Users\\ALAILTON\\My Drive\\Faculdade\\TCC\\Código\\Visual Studio\\Images\\outside.png");
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Close();
        }
        #endregion

        private void ImportSclForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (dataList == null) dataList = new List<dataSCL>();
                dataList.Clear();

                foreach (var subNet in scl.Communication.SubNetwork)
                {
                    foreach (var ap in subNet.ConnectedAP)
                    {
                        if (ap.GSE == null || thisFormType != formType.GOOSE) continue;
                        foreach (var gse in ap.GSE)
                        {
                            dataSCL _data = new dataSCL();
                            _data.type = formType.GOOSE;
                            foreach (var address in gse.Address)
                            {
                                if (address.type == "VLAN-ID")
                                    _data.vlanid = address.Value;
                                else if (address.type == "MAC-Address")
                                    _data.macDst = address.Value;
                                else if (address.type == "APPID")
                                    _data.appId = address.Value;
                                else if (address.type == "VLAN-PRIORITY")
                                    _data.vlanPriority = address.Value;
                            }
                            _data.cbName = gse.cbName;
                            _data.iedName = ap.iedName;
                            _data.ldInst = gse.ldInst;
                            _data.accessPoint = ap.apName;

                            var ied = scl.IED.FirstOrDefault(ied => ied.name == _data.iedName);
                            var acessPoint = ied.AccessPoint.FirstOrDefault(ap => ap.name == _data.accessPoint);

                            var server = (tServer)acessPoint.Items.FirstOrDefault(x => x is tServer);
                            var ld = server.LDevice.FirstOrDefault(x => x.inst == _data.ldInst);
                            var gseControl = ld.LN0.GSEControl.FirstOrDefault(x => x.name == _data.cbName);

                            _data.desc = gseControl.desc;
                            _data.datSet = gseControl.datSet;
                            _data.confRev = gseControl.confRev;
                            _data.goId = gseControl.appID;

                            dataList.Add(_data);
                        }
                    }
                    foreach (var ap in subNet.ConnectedAP)
                    {
                        if (ap.SMV == null || thisFormType != formType.SampledValue) continue;
                        foreach (var smv in ap.SMV)
                        {
                            dataSCL _data = new dataSCL();
                            _data.type = formType.SampledValue;
                            foreach (var address in smv.Address)
                            {
                                if (address.type == "VLAN-ID")
                                    _data.vlanid = address.Value;
                                else if (address.type == "MAC-Address")
                                    _data.macDst = address.Value;
                                else if (address.type == "APPID")
                                    _data.appId = address.Value;
                                else if(address.type == "VLAN-PRIORITY")
                                    _data.vlanPriority = address.Value;
                            }
                            _data.cbName = smv.cbName;
                            _data.iedName = ap.iedName;
                            _data.ldInst = smv.ldInst;
                            _data.accessPoint = ap.apName;

                            var ied = scl.IED.FirstOrDefault(ied => ied.name == _data.iedName);
                            var acessPoint = ied.AccessPoint.FirstOrDefault(ap => ap.name == _data.accessPoint);

                            var server = (tServer)acessPoint.Items.FirstOrDefault(x => x is tServer);
                            var ld = server.LDevice.FirstOrDefault(x => x.inst == _data.ldInst);
                            var smvControl = ld.LN0.SampledValueControl.FirstOrDefault(x => x.name == _data.cbName);

                            if (smvControl == null) continue;

                            var datSet = ld.LN0.DataSet.FirstOrDefault(x => x.name == smvControl.datSet);
                            if (datSet != null)
                            {
                                _data.fcdaList= new List<dataSCL.Fcda>();
                                foreach (var fcda in datSet.Items)
                                {
                                    _data.fcdaList.Add(new dataSCL.Fcda()
                                    {
                                        ldInst = fcda.ldInst,
                                        prefix = fcda.prefix,
                                        lnClass = fcda.lnClass,
                                        lnInst = fcda.lnInst,
                                        doName = fcda.doName,
                                        daName = fcda.daName,
                                        fc = fcda.fc.ToString()
                                    });
                                }
                            }

                            _data.desc = smvControl.desc;
                            _data.datSet = smvControl.datSet;
                            _data.confRev = smvControl.confRev;
                            _data.svId = smvControl.smvID;
                            _data.smpRate = smvControl.smpRate;
                            _data.nofASDU = smvControl.nofASDU;


                            dataList.Add(_data);
                        }
                    }
                }

                updateTable();

            }

            catch
            {
                this.Close();
            }
        }

        private string filePath;
        private SCL scl;
        public class dataSCL
        {
            public string iedName;
            public string cbName;
            public string ldInst;
            public string vlanid;
            public string vlanPriority;
            public string appId;
            public string macDst;
            public string accessPoint;

            public string desc;
            public string datSet;
            public uint confRev;

            public formType type;

            //Sampled Value
            public string svId;
            public uint smpRate;
            public uint nofASDU;
            public string smpMod;

            //Goose
            public string goId;

            //DataSet
            public List<Fcda> fcdaList;
            public class Fcda
            {
                   public string ldInst;
                public string prefix;
                public string lnClass;
                public string lnInst;
                public string doName;
                public string daName;
                public string fc;
            }
        }

        private bool LoadSCL(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SCL));
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    this.scl = (SCL)serializer.Deserialize(fileStream);
                    if (this.scl != null) return true;
                    else return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        private void updateTable()
        {
            dgvGse.Rows.Clear();
            foreach (var data in dataList)
            {
                if (data.type == thisFormType)
                    dgvGse.Rows.Add("", data.iedName, data.cbName, data.datSet, data.desc);
            }
        }

        private List<dataSCL> dataList;

        private void dgvGse_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 0)
            {
                if (!(e.RowIndex >= 0)) return;
                returnData = dataList[e.RowIndex];
                this.Close();
            }
        }
    }
}
