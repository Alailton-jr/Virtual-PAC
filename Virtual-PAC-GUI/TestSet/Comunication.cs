﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TestSet.MainForm;

namespace TestSet
{
    public partial class Comunication : Form
    {
        public CommunicationConfig config;


        public Comunication()
        {
            InitializeComponent();


            config = main.communicationConfig;

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
            if (int.TryParse(number, out int parsedNumber))
            {
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

        private long tempoConect = 0;
        private bool testConect = false;

        private long getTimeStamp()
        {
            DateTime currentUtcTime = DateTime.UtcNow;
            long utcTimestamp = currentUtcTime.Ticks / TimeSpan.TicksPerSecond;
            return utcTimestamp;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            main.serverCon.changeConProperties(main.communicationConfig.ip, main.communicationConfig.port);
            string res = main.serverCon.SendData("TestConnection", "All");
            if (res == null || res == "error") MessageBox.Show("It Wasn't possible to connected to the vMU!");
            else MessageBox.Show("Connected to the vMU!");
        }

    }
}
