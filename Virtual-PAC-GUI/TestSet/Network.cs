using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static TestSet.MainForm;


namespace TestSet
{
    public partial class Network : Form
    {

        private NetworkConfig.GOConfig goConfig;
        private NetworkConfig.SVConfig svConfig;

        public Network()
        {
            InitializeComponent();
            if (main.networkConfig.goConfig == null) main.networkConfig.goConfig = new NetworkConfig.GOConfig();
            if (main.networkConfig.svConfig == null) main.networkConfig.svConfig = new NetworkConfig.SVConfig();
            goConfig = main.networkConfig.goConfig;
            svConfig = main.networkConfig.svConfig;

            if (main.generalConfig == null)
            {
                main.generalConfig = new GeneralConfig()
                {
                    ip = main.communicationConfig.ip,
                    port = main.communicationConfig.port
                };
            }

            UpdateTextBox();

        }

        private void UpdateTextBox()
        {
            TbGoAppId.Text = "0x" + goConfig.appId.ToString("X");
            TbGoAppId.Validated += textBoxValidation;
            TbGoAppId.KeyPress += textBoxValidation;
            TbGoControlRef.Text = goConfig.controlRef;
            TbGoControlRef.Validated += textBoxValidation;
            TbGoControlRef.KeyPress += textBoxValidation;
            TbGoID.Text = goConfig.goId;
            TbGoID.Validated += textBoxValidation;
            TbGoID.KeyPress += textBoxValidation;
            TbGoMacSrc.Text = goConfig.macSrc;
            TbGoMacSrc.Validated += textBoxValidation;
            TbGoMacSrc.KeyPress += textBoxValidation;
            TbGoRev.Text = goConfig.confRev.ToString();
            TbGoRev.Validated += textBoxValidation;
            TbGoRev.KeyPress += textBoxValidation;
            TbGoVLan.Text = "0x" + goConfig.vLan.ToString("X");
            TbGoVLan.Validated += textBoxValidation;
            TbGoVLan.KeyPress += textBoxValidation;
            TbSvAppID.Text = "0x" + svConfig.appId.ToString("X");
            TbSvAppID.Validated += textBoxValidation;
            TbSvAppID.KeyPress += textBoxValidation;
            TbSvFreq.Text = svConfig.frequency.ToString();
            TbSvFreq.Validated += textBoxValidation;
            TbSvFreq.KeyPress += textBoxValidation;
            TbSvID.Text = svConfig.svId;
            TbSvID.Validated += textBoxValidation;
            TbSvID.KeyPress += textBoxValidation;
            TbSvMacDest.Text = svConfig.macDest;
            TbSvMacDest.Validated += textBoxValidation;
            TbSvMacDest.KeyPress += textBoxValidation;
            TbSVNoAsdu.Text = svConfig.noAsdu.ToString();
            TbSVNoAsdu.Validated += textBoxValidation;
            TbSVNoAsdu.KeyPress += textBoxValidation;
            TbSvRev.Text = svConfig.confRev.ToString();
            TbSvRev.Validated += textBoxValidation;
            TbSvRev.KeyPress += textBoxValidation;
            TbSvVLan.Text = "0x" + svConfig.vLan.ToString("X");
            TbSvVLan.Validated += textBoxValidation;
            TbSvVLan.KeyPress += textBoxValidation;

            TbIpAddres.Text = main.generalConfig.ip;
            TbPort.Text = main.generalConfig.port.ToString();
            TbIpAddres.Validated += textBoxValidation;
            TbIpAddres.KeyPress += textBoxValidation;
            TbPort.Validated += textBoxValidation;
            TbPort.KeyPress += textBoxValidation;
        }

        private int HexTextValidation(TextBox tb, int val)
        {
            string text;
            if (tb.Text.Contains("0x"))
            {
                text = tb.Text.Replace("0x", "");
                if (int.TryParse(text, System.Globalization.NumberStyles.HexNumber, null, out int Res))
                {
                    if (Res < 0xffff)
                        val = Res;
                }
                tb.Text = "0x" + val.ToString("X");
            }
            else
            {
                text = tb.Text;
                if (int.TryParse(text, System.Globalization.NumberStyles.Integer, null, out int Res))
                {
                    if (Res < 0xffff)
                        val = Res;
                }
                tb.Text = val.ToString();
            }
            return val;
        }

        private int IntTextValidation(TextBox tb, int val)
        {
            string text = tb.Text;
            if (int.TryParse(text, System.Globalization.NumberStyles.Integer, null, out int Res))
            {
                val = Res;
            }

            tb.Text = val.ToString();
            return val;
        }

        private string macTextValidation(TextBox tb, string val)
        {
            string text = tb.Text;
            string macAddress = text.Trim();
            string macAddressPattern = "^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$";

            Regex regex = new Regex(macAddressPattern);

            if (regex.IsMatch(macAddress))
            {
                val = text.ToUpper();
            }
            tb.Text = val;

            return val;
        }

        private void ValidateData()
        {
            goConfig.appId = HexTextValidation(TbGoAppId, goConfig.appId);
            goConfig.vLan = HexTextValidation(TbGoVLan, goConfig.vLan);
            goConfig.confRev = IntTextValidation(TbGoRev, goConfig.confRev);
            goConfig.macSrc = macTextValidation(TbGoMacSrc, goConfig.macSrc);
            goConfig.controlRef = TbGoControlRef.Text;
            goConfig.goId = TbGoID.Text;


            svConfig.frequency = IntTextValidation(TbSvFreq, svConfig.frequency);
            svConfig.svId = TbSvID.Text;
            svConfig.vLan = HexTextValidation(TbSvVLan, svConfig.vLan);
            svConfig.noAsdu = IntTextValidation(TbSVNoAsdu, svConfig.noAsdu);
            svConfig.confRev = IntTextValidation(TbSvRev, svConfig.confRev);
            svConfig.appId = HexTextValidation(TbSvAppID, svConfig.appId);
            svConfig.macDest = macTextValidation(TbSvMacDest, svConfig.macDest);

            main.generalConfig.port = IntTextValidation(TbPort, main.generalConfig.port);
            main.generalConfig.ip = TbIpAddres.Text;

        }

        private void textBoxValidation(object sender, EventArgs e)
        {
            ValidateData();
        }
        private void textBoxValidation(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                ValidateData();
            }
        }

        private void BtnImportScl_Click(object sender, EventArgs e)
        {

            openFileDialog1.Title = "Open File Dialog";
            openFileDialog1.Filter = "SCL Files|*.icd;*.scd;*.scl;*.cid|ICD Files (*.icd)|*.icd|SCD Files (*.scd)|*.scd|SCL Files (*.scl)|*.scl|CID Files (*.cid)|*.cid";

            openFileDialog1.FilterIndex = 1; // Set the default filter index to .icd
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                var form = new ImportSclForm();
                var data = form.openExt(selectedFilePath);
                if (data == null)
                    MessageBox.Show("Não foi possível abrir o arquivo.");
                else
                {
                    TbGoAppId.Text = data.appId;
                    TbGoControlRef.Text = data.cbName;
                    TbGoID.Text = data.goId;
                    TbGoMacSrc.Text = data.macDst;
                    TbGoRev.Text = data.confRev.ToString();
                    TbGoVLan.Text = data.vlanid;
                    // Update the goConfig values
                    goConfig.appId = Convert.ToInt32(data.appId, 16);
                    goConfig.controlRef = data.cbName;
                    goConfig.goId = data.goId;
                    goConfig.macSrc = data.macDst;
                    goConfig.confRev = Convert.ToInt32(data.confRev);
                    goConfig.vLan = Convert.ToInt32(data.vlanid, 16);

                }
            }
        }

        private void BtnLoadConfig_Click(object sender, EventArgs e)
        {
            main.serverCon.changeConProperties(main.communicationConfig.ip, main.communicationConfig.port);

            var res = main.serverCon.SendData("getNetworkSetup", "all");
            if (res != null)
            {
                main.loadYamlNetwork(res);
                res = main.serverCon.SendData("getContinuousSetup", "all");
                if (res != null)
                {
                    main.loadYamlContinuous(res);
                    res = main.serverCon.SendData("getSequencerSetup", "all");
                    if (res != null)
                    {
                        main.loadYamlSequencer(res);
                        UpdateTextBox();
                    }
                    else
                    {
                        MessageBox.Show("Error While loading File");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Error While loading File");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Error While loading File");
                return;
            }
        }

        private void BtnSendConfig_Click(object sender, EventArgs e)
        {
            var yamlFile = main.CreateYamlNetwork();
            if (yamlFile != null)
            {
                main.serverCon.changeConProperties(main.communicationConfig.ip, main.communicationConfig.port);
                string res = main.serverCon.SendData("setupNetwork", yamlFile);
                if (res == null || res == "error")
                {
                    MessageBox.Show("It Wasn't possible to connected to vMU!");
                    return;
                }
            }
        }
    }
}
