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

namespace Quality
{
    public partial class GeneralForm : Form
    {

        private ServerConfig config;

        public GeneralForm()
        {
            InitializeComponent();
            config = MainForm.mainControl.serverConfig;
            fillFields();
        }

        private void fillFields()
        {
            TbUser.Text = config.user;
            TbPassword.Text = config.password;
            TbIpAddress.Text = config.ipAddress;
            TbPort.Text = config.port;
        }

    }
}
