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
using static Server.MainForm;

namespace Server
{
    public partial class DeviceCreatingForm : Form
    {
        private classes.ServerCtl main;
        public DeviceCreatingForm()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private bool atLeastOne = false;
        public bool openExt()
        {
            this.ShowDialog();
            return atLeastOne;
        }

        private void DeviceCreatingForm_Load(object sender, EventArgs e)
        {
            main = MainForm.main;
            PbInfo.Visible = false;
            LbInfo.Text = "";
        }

        private string VmName;
        private string VmType;

        private void BtnStartShut_Click(object sender, EventArgs e)
        {
            if (VmName == null || VmType == null) return;
            var res = main.serverCon.SendData("createVm", $"{VmName};{VmType}");
            if (res != null && res != "error")
            {
                created = false;
                CreatingTh = new Thread(CreatingThread);
                CreatingTh.Start();
                LbInfo.Text = "Creating Virtual Machine...";
                LbInfo.ForeColor = Color.Crimson;
                BtnCreate.Enabled = false;
                PbInfo.Visible = true;
                PbInfo.Style = ProgressBarStyle.Marquee;
            }
        }
        private Thread CreatingTh;
        private bool created = false;
        private void CreatingThread()
        {
            while (!created)
            {
                var res = main.serverCon.SendData("createVmStatus", "All");
                if (res != null && res != "error")
                {
                    if (res == "true") break;
                }
            }
            PbInfo.BeginInvoke((MethodInvoker)delegate
            {
                PbInfo.Style = ProgressBarStyle.Blocks;
                PbInfo.Value = 100;
            });
            LbInfo.BeginInvoke((MethodInvoker)delegate
            {
                LbInfo.Text = "Virtual Machine Created";
                LbInfo.ForeColor = Color.RoyalBlue;
            });
            BtnCreate.BeginInvoke((MethodInvoker)delegate { BtnCreate.Enabled = true; });
            atLeastOne = true; 
        }

        private void DeviceCreatingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            created = true;
            while (CreatingTh.IsAlive) { }
        }

        private void VbType_Validated(object sender, EventArgs e)
        {
            VmName = TbVmName.Text;
            VmType = CbType.Text;
        }
    }
}
