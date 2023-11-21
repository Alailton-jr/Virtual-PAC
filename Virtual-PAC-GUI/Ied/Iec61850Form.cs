using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ied
{
    public partial class Iec61850Form : Form
    {
        public Iec61850Form()
        {
            InitializeComponent();
        }


        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(MainForm.mainForm.Handle, 0x112, 0xf012, 0);
        }

        private void Iec61850Form_Load(object sender, EventArgs e)
        {
            openChildForm(new Iec61850GeneralForm());
        }

        Form activeForm;
        private void openChildForm(Form chieldForm)
        {
            if (activeForm != null)
                activeForm.Dispose();
            activeForm = chieldForm;
            chieldForm.TopLevel = false;
            chieldForm.FormBorderStyle = FormBorderStyle.None;
            chieldForm.Dock = DockStyle.Fill;
            PnChields.Controls.Add(chieldForm);
            PnChields.Tag = chieldForm;
            chieldForm.BringToFront();
            chieldForm.Show();
        }

        private Form goose = null;
        private void BtnGo_Click(object sender, EventArgs e)
        {
            openChildForm(new IecGoose());
        }

        private Form dataSet = null;
        private void BtnDataSet_Click(object sender, EventArgs e)
        {
            //if (dataSet == null)
            //    dataSet = 
            openChildForm(new IecDataSetForm());
        }

        private void BtnSv_Click(object sender, EventArgs e)
        {
            openChildForm(new IecSvForm());
        }

        private void IecHome_Click(object sender, EventArgs e)
        {
            openChildForm(new Iec61850GeneralForm());
        }

        
    }
}
