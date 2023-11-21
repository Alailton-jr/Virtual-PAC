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
using System.Xml.Linq;
using static Server.MainForm;

namespace Server
{
    public partial class DeviceRemoveForm : Form
    {
        public DeviceRemoveForm()
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

        private List<string[]> vmNames;
        private bool atLeastOne = false;
        public bool openExt(List<string[]> vmNames)
        {
            this.vmNames = vmNames;
            this.ShowDialog();
            return atLeastOne;
        }

        private void DeviceRemoveForm_Load(object sender, EventArgs e)
        {
            main = MainForm.main;
            PbInfo.Visible = false;
            LbInfo.Text = "";
            CbVms.Items.Clear();
            if (vmNames != null && vmNames.Count > 0)
            {
                foreach (var name in vmNames)
                {
                    CbVms.Items.Add(name[0]);
                }
                CbVms.Text = vmNames[0][0];
                TbTypes.Text = vmNames[0][1];
                curVm = new string[] { vmNames[0][0], vmNames[0][1] };
            }
            else this.Close();
        }

        private void CbVms_Validated(object sender, EventArgs e)
        {
            var vm = vmNames.Find(x => x[0] == CbVms.Text);
            if (vm != null)
            {
                TbTypes.Text = vm[1];
            }
            curVm = new string[] { vm[0], vm[1] };
        }
        private string[] curVm;
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var res = main.serverCon.SendData("deleteVm", $"{curVm[0]};{curVm[1]}");
            if (res != null && res != "error")
            {
                created = false;
                CreatingTh = new Thread(DeletingThread);
                CreatingTh.Start();
                LbInfo.Text = "Deleting Virtual Machine...";
                LbInfo.ForeColor = Color.Crimson;
                BtnDelete.Enabled = false;
                PbInfo.Visible = true;
                PbInfo.Style = ProgressBarStyle.Marquee;
                CbVms.Enabled = false;
            }
        }
        private Thread CreatingTh;
        private bool created = false;
        private void DeletingThread()
        {
            while (!created)
            {
                var res = main.serverCon.SendData("deleteVmStatus", "All");
                if (res != null && res != "error")
                {
                    if (res == "true") break;
                }
            }
            vmNames.RemoveAll(x=> x[0] == curVm[0]);

            CbVms.Invoke((MethodInvoker)delegate
            {
                CbVms.Items.Clear();
                foreach (var name in vmNames)
                {
                    CbVms.Items.Add(name[0]);
                }
                if (vmNames.Count > 0)
                {
                    CbVms.Text = vmNames[0][0];
                    TbTypes.Text = vmNames[0][1];
                    curVm = new string[] { vmNames[0][0], vmNames[0][1] };
                }
                else
                {
                    CbVms.Text = "";
                    TbTypes.Text = "";
                    curVm = new string[] { "","" };
                }
                
                CbVms.Enabled = true;
            });

            PbInfo.BeginInvoke((MethodInvoker)delegate
            {
                PbInfo.Style = ProgressBarStyle.Blocks;
                PbInfo.Value = 100;
            });
            LbInfo.BeginInvoke((MethodInvoker)delegate
            {
                LbInfo.Text = "Virtual Machine Deleted";
                LbInfo.ForeColor = Color.RoyalBlue;
            });
            atLeastOne = true;
            BtnDelete.BeginInvoke((MethodInvoker)delegate { BtnDelete.Enabled = true; });
        }
    }
}
