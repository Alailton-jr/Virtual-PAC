using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Ied.MainForm;

namespace Ied
{
    public partial class GeneralForm : Form
    {

        private GeneralConfig conf;


        public GeneralForm()
        {
            InitializeComponent();
        }

        private void GeneralForm_Load(object sender, EventArgs e)
        {
            this.conf = main.general;
            TbCurNominal.Text = ((double)conf.NominalCurrent).ToString("0.00");
            TbFrequency.Text = ((double)conf.frequency).ToString("0.00");
            TbVolNominal.Text = ((double)conf.NominalVoltage).ToString("0.00");
            TbIP.Text = conf.ipAddress;
            TbPort.Text = ((int)conf.port).ToString();
        }

        private void TbName_Validated(object sender, EventArgs e)
        {
            conf.NominalCurrent = TbCurNominal.Text;
            conf.frequency = TbFrequency.Text;
            conf.NominalVoltage = TbVolNominal.Text;
            conf.ipAddress = TbIP.Text;
            conf.port = TbPort.Text;

            TbCurNominal.Text = ((double)conf.NominalCurrent).ToString("0.00");
            TbFrequency.Text = ((double)conf.frequency).ToString("0.00");
            TbVolNominal.Text = ((double)conf.NominalVoltage).ToString("0.00");
            TbIP.Text = conf.ipAddress;
            TbPort.Text = ((int)conf.port).ToString();
        }


    }
}
