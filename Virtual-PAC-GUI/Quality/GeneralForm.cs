using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Quality.Classes;
using static Quality.MainForm;

namespace Quality
{
    public partial class GeneralForm : Form
    {

        private ServerConfig config;
        private SocketConnection mySocket;

        public GeneralForm()
        {
            InitializeComponent();
            config = mainControl.serverConfig;
            mySocket = mainControl.socket;
            fillFields();

            mySocket.ConnectionEstablished += (sender, e) =>
            {
                BtnConnect.Text = "Desconectar";
            };
            mySocket.ConnectionLost += (sender, e) =>
            {
                BtnConnect.Text = "Conectar";
            };
        }

        private void fillFields()
        {
            TbUser.Text = config.user;
            TbPassword.Text = config.password;
            TbIpAddress.Text = config.ipAddress;
            TbPort.Text = config.port;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            mySocket.changeConProperties(config.ipAddress, int.Parse(config.port));
            if ((!mySocket.isConnected))
            {
                if (mySocket.Connect())
                {
                    if (!mySocket.isConnected)
                        MessageBox.Show("Não foi possível connectar com o Virtual Qualimetro");
                }
                else
                {
                    MessageBox.Show("Não foi possível connectar com o Virtual Qualimetro");
                }
            }
            else
            {
                mySocket.closeConnection();
            }
            
        }

        private void TbUser_TextChanged(object sender, EventArgs e)
        {
            config.user = TbUser.Text;
            config.password = TbPassword.Text;
            config.ipAddress = TbIpAddress.Text;
            config.port = TbPort.Text;
            fillFields();
        }
    }
}
