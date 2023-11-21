using static Server.classes;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace Server
{
    public partial class DeviceDetailForm : Form
    {
        private DeviceClass device;
        private ServerCtl main;

        public DeviceDetailForm()
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

        public bool callForm(DeviceClass device)
        {

            main = MainForm.main;
            this.device = device;
            if (device.ApiCon == null) device.ApiCon = new SocketConnection();

            TbIp.Enabled = false;
            TbPort.Enabled = false;
            TbPasswd.Enabled = false;

            TbIp.Text = device.ip;
            TbPort.Text = device.port.ToString();
            TbPasswd.Text = device.password;

            BtnEdit.Enabled = false;
            BtnReboot.Enabled = false;

            if (device.VmState == DeviceState.up)
                BtnStartShut.Text = "Desligar";
            else if (device.VmState == DeviceState.down)
                BtnStartShut.Text = "Iniciar";

            if (device.VmState == DeviceState.up)
                VmState_Connected();
            else
                VmState_Disconnected();

            if (device.ApiState == DeviceState.up)
                VmAPI_Connected();
            else
                VmAPI_Disconnected();

            main.serverCon.ConnectionLost += ConnectionLost;

            device.ApiCon.ConnectionLost += ApiConnectionLost;

            this.ShowDialog();

            main.serverCon.ConnectionLost -= ConnectionLost;
            ThreadRunning = false;

            return true;
        }

        private void ApiConnectionLost(object? sender, EventArgs e)
        {
            if (CurForm != null) CurForm.BeginInvoke((MethodInvoker)delegate { CurForm.Close(); });

        }
        private void ConnectionLost(object? sender, EventArgs e)
        {
            if (this.InvokeRequired) this.BeginInvoke((MethodInvoker)delegate { this.Close(); });
            else this.Close();
        }

        private bool ThreadRunning;
        private Thread ThreadStatus;

        private void DeviceDetailForm_Load(object sender, EventArgs e)
        {
            if (device.type == DeviceType.ied)
                LbDeviceType.Text = "Ied Status";
            else
                LbDeviceType.Text = "Mu Status";

            ThreadRunning = true;
            ThreadStatus = new Thread(CheckStatus);
            ThreadStatus.Start();
        }

        private void CheckStatus()
        {
            while (ThreadRunning)
            {
                CheckVm();
                checkApi();

                if (device.VmState == DeviceState.up)
                    VmState_Connected();
                else
                    VmState_Disconnected();

                if (device.ApiState == DeviceState.up)
                    VmAPI_Connected();
                else
                    VmAPI_Disconnected();
                Thread.Sleep(1000);
            }
        }

        private void CheckVm()
        {
            var res = main.serverCon.SendData("getVmState", device.name);
            if (res != null && res != "error")
            {
                Dictionary<string, int> results = null;
                try
                {
                    results = JsonConvert.DeserializeObject<Dictionary<string, int>>(res);
                }
                catch {; }

                if (results != null)
                {
                    if (results["state"] == 1)
                        device.VmState = DeviceState.up;
                    else
                        device.VmState = DeviceState.down;
                    return;
                }
            }
            device.VmState = DeviceState.down;
        }

        private void checkApi()
        {
            device.ApiCon.changeConProperties(device.ip, device.port);
            var res = device.ApiCon.SendData("TestConnect", "all");
            if (res != null && res != "error") device.ApiState = DeviceState.up;
            else device.ApiState = DeviceState.down;
        }

        private void VmState_Connected()
        {
            try
            {
                if (LbVm.InvokeRequired)
                {
                    LbVm.BeginInvoke(() =>
                    {
                        LbVm.Text = "Running";
                        LbVm.Image = Properties.Resources.running;
                    });
                }
                else
                {
                    LbVm.Text = "Running";
                    LbVm.Image = Properties.Resources.running;
                }

                if (BtnReboot.InvokeRequired) BtnReboot.BeginInvoke(() => { BtnReboot.Enabled = true; });
                else BtnReboot.Enabled = true;

                if (BtnStartShut.InvokeRequired) BtnStartShut.BeginInvoke(() => { BtnStartShut.Text = "Desligar"; });
                else BtnStartShut.Text = "Desligar";
            }
            catch {; }

        }

        private void VmState_Disconnected()
        {
            if (BtnReboot.InvokeRequired) BtnReboot.BeginInvoke(() => { BtnReboot.Enabled = false; });
            else BtnReboot.Enabled = false;
            if (BtnStartShut.InvokeRequired) BtnStartShut.BeginInvoke(() => { BtnStartShut.Text = "Iniciar"; });
            else BtnStartShut.Text = "Iniciar";
            if (LbVm.InvokeRequired)
            {
                LbVm.BeginInvoke(() =>
                {
                    LbVm.Text = "Stoped";
                    LbVm.Image = Properties.Resources.stoped;
                });
            }
            else
            {
                LbVm.Text = "Stoped";
                LbVm.Image = Properties.Resources.stoped;
            }

        }

        private void VmAPI_Connected()
        {
            if (LbIed.InvokeRequired)
            {
                LbIed.BeginInvoke(() =>
                {
                    LbIed.Text = "Running";
                    LbIed.Image = Properties.Resources.running;
                });
            }
            else
            {
                LbIed.Text = "Running";
                LbIed.Image = Properties.Resources.running;
            }
            if (BtnEdit.InvokeRequired) BtnEdit.BeginInvoke(() => { BtnEdit.Enabled = true; });
            else BtnEdit.Enabled = true;

            if (BtnConfig.InvokeRequired) BtnConfig.BeginInvoke(() => { BtnConfig.Enabled = true; });
            else BtnConfig.Enabled = true;
        }

        private void VmAPI_Disconnected()
        {
            if (BtnEdit.InvokeRequired) BtnEdit.BeginInvoke(() => { BtnEdit.Enabled = false; });
            else BtnEdit.Enabled = false;
            if (BtnConfig.InvokeRequired) BtnConfig.BeginInvoke(() => { BtnConfig.Enabled = false; });
            else BtnConfig.Enabled = false;
            if (LbIed.InvokeRequired)
            {
                LbIed.BeginInvoke(() =>
                {
                    LbIed.Text = "Stoped";
                    LbIed.Image = Properties.Resources.stoped;
                });
            }
            else
            {
                LbIed.Text = "Stoped";
                LbIed.Image = Properties.Resources.stoped;
            }
        }


        private void BtnStartShut_Click(object sender, EventArgs e)
        {
            if (device.VmState == DeviceState.down)
            {
                //main.StartVm(device);
                var res = main.serverCon.SendData("startVm", device.name);
            }
            else
            {
                //main.ShutdownVm(device);
                var res = main.serverCon.SendData("shutdownVm", device.name);
            }

        }

        private void BtnReboot_Click(object sender, EventArgs e)
        {
            //main.RebootVm(device);
            var res = main.serverCon.SendData("rebootVm", device.name);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            //main.ed(device);
        }

        private Form CurForm = null;
        private void BtnConfig_Click(object sender, EventArgs e)
        {
            if (device.type == DeviceType.ied)
            {
                CurForm = new Ied.MainForm();
                device.ApiCon.Disconnect();
                ThreadRunning = false;
                if (!((Ied.MainForm)CurForm).callForm(device.ip, device.port))
                {
                    MessageBox.Show("Error Loading Form");
                }
                ThreadRunning = true;
                ThreadStatus = new Thread(CheckStatus);
                ThreadStatus.Start();
            }
            else if (device.type == DeviceType.mu)
            {



                CurForm = new TestSet.MainForm();
                if (!((TestSet.MainForm)CurForm).callForm(device.ip, device.port))
                {
                    MessageBox.Show("Error Loading Form");
                }
            }
            CurForm = null;
        }


    }
}
