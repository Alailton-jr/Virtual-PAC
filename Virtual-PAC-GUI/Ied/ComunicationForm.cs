using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Ied.MainForm;

namespace Ied
{
    public partial class ComunicationForm : Form
    {

        CommunicationConfig config = MainForm.main.communication;
        Ctl main = Ied.MainForm.main;

        public ComunicationForm()
        {
            InitializeComponent();

            UpdateData();

            TbIP.Validated += ValidateText;
            TbPort.Validated += ValidateText;
            TbName.Validated += ValidateText;
            TbSenha.Validated += ValidateText;

            TbIP.KeyPress += ValidateTextKey;
            TbPort.KeyPress += ValidateTextKey;
            TbName.KeyPress += ValidateTextKey;
            TbSenha.KeyPress += ValidateTextKey;

        }

        private void UpdateData()
        {
            TbIP.Text = config.ip;
            TbPort.Text = config.port.ToString();
            TbName.Text = config.name;
            TbSenha.Text = config.password;
        }

        public bool ValidateIpAddress(string ipAddress)
        {
            // Check if the IP address is a valid IPv4 or IPv6 address
            if (IPAddress.TryParse(ipAddress, out IPAddress parsedIpAddress))
            {
                // Check if the parsed IP address matches the original input
                return parsedIpAddress.ToString() == ipAddress;
            }

            return false;
        }
        public bool ValidateIntNumber(string number)
        {
            // Attempt to parse the input string to an int
            if (int.TryParse(number, out int parsedNumber))
            {
                // Check if the parsed number matches the original input
                return parsedNumber.ToString() == number;
            }

            return false;
        }

        public void ValidateText(object sender, EventArgs e)
        {

            if (ValidateIpAddress(TbIP.Text.Trim()))
                config.ip = TbIP.Text.Trim();
            if (ValidateIntNumber(TbPort.Text.Trim()))
                config.port = int.Parse(TbPort.Text);

            config.password = TbSenha.Text.Trim();
            config.name = TbName.Text.Trim();

            UpdateData();
        }

        public void ValidateTextKey(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (ValidateIpAddress(TbIP.Text))
                    config.ip = TbIP.Text;
                if (ValidateIntNumber(TbPort.Text))
                    config.port = int.Parse(TbPort.Text);

                config.password = TbSenha.Text;
                config.name = TbName.Text;

                UpdateData();
            }
        }

        private void BtnSendConfig_Click(object sender, EventArgs e)
        {
            var yamlFile = main.saveYaml();
            if (yamlFile != null)
            {
                // Needs to add a warning for chaging IP
                main.serverCon.changeConProperties(main.communication.ip, main.communication.port);
                string res = main.serverCon.SendData("SendIedConfig", yamlFile);
                if (res != null) MessageBox.Show("File was send!");
                else MessageBox.Show("Error While sending File");
            }
        }

        private void BtnLoadConfig_Click(object sender, EventArgs e)
        {
            main.serverCon.changeConProperties(main.communication.ip, main.communication.port);
            string res = main.serverCon.SendData("LoadIedConfig", "all");
            if (res != null)
            {
                MessageBox.Show("File Configuration Received");
                main.parserYaml(res);
            }
            else MessageBox.Show("Error While loading File");
        }
    }
}
