using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace TestSet
{
    public partial class MainForm : Form
    {

        public static Ctl main;
        public static System.Windows.Forms.Timer timerConnection = new System.Windows.Forms.Timer();

        public bool externLoad = false;
        private string extIp;
        private int extPort;
        public bool callForm(string ip, int port)
        {
            externLoad = true;
            extIp = ip;
            extPort = port;
            this.Show();

            return true;
        }


        private int borderSize = 2;
        private Size formSize;

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized: //Maximized form (After)
                    this.Padding = new Padding(8, 8, 8, 0);
                    break;
                case FormWindowState.Normal: //Restored form (After)
                    if (this.Padding.Top != borderSize)
                        this.Padding = new Padding(borderSize);
                    break;
            }
        }

        private void CollapseMenu()
        {
            //if (this.panelMenu.Width > 200) //Collapse menu
            //{
            //    panelMenu.Width = 100;
            //    pictureBox1.Visible = false;
            //    btnMenu.Dock = DockStyle.Top;
            //    foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
            //    {
            //        menuButton.Text = "";
            //        menuButton.ImageAlign = ContentAlignment.MiddleCenter;
            //        menuButton.Padding = new Padding(0);
            //    }
            //}
            //else
            //{ //Expand menu
            //    panelMenu.Width = 230;
            //    pictureBox1.Visible = true;
            //    btnMenu.Dock = DockStyle.None;
            //    foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
            //    {
            //        menuButton.Text = "   " + menuButton.Tag.ToString();
            //        menuButton.ImageAlign = ContentAlignment.MiddleLeft;
            //        menuButton.Padding = new Padding(10, 0, 0, 0);
            //    }
            //}
        }

        //Event methods
        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            CollapseMenu();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
            this.WindowState = FormWindowState.Minimized;
            BtnResize.Image = null;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                formSize = this.ClientSize;
                this.WindowState = FormWindowState.Maximized;
                BtnResize.Image = Properties.Resources.inside;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = formSize;
                BtnResize.Image = Properties.Resources.outside;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            //Application.Exit();
        }

        public MainForm()
        {
            InitializeComponent();

            PicBoxLogo.SizeMode = PictureBoxSizeMode.CenterImage;
            this.Padding = new Padding(borderSize);//Border size
            this.BackColor = Color.FromArgb(40, 58, 73);//Border color
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            main.saveClass();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Captura
            InputSimulator inputSimulator = new InputSimulator();

            // Start a background thread to listen for the Space key
            Application.Idle += (sender, e) =>
            {
                if (inputSimulator.InputDeviceState.IsKeyDown(VirtualKeyCode.SPACE))
                {
                    CaptureActiveWindow();
                }
            };


            main = new Ctl();
            main.loadClass();
            if (externLoad)
            {
                main.communicationConfig.ip = extIp;
                main.communicationConfig.port = extPort;
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
                        if (res != null) main.loadYamlSequencer(res);
                        else this.Close();
                    }
                    else this.Close();
                }
                else this.Close();
            }
        }

        private Form activeForm = null;
        private void openChildForm(Form chieldForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = chieldForm;
            chieldForm.TopLevel = false;
            chieldForm.FormBorderStyle = FormBorderStyle.None;
            chieldForm.Dock = DockStyle.Fill;
            PnChild.Controls.Add(chieldForm);
            PnChild.Tag = chieldForm;
            chieldForm.BringToFront();
            chieldForm.Show();
        }

        private Network networkForm;
        private Comunication comunicationForm;
        private Sequencer sequencerForm;
        private continuous continousForm;

        private void BtnSequencer_Click(object sender, EventArgs e)
        {
            openChildForm(new Sequencer());
            LbCurrentPanel.Text = "Sequencer";
        }

        private void BtnNetwork_Click(object sender, EventArgs e)
        {
            openChildForm(new Network());
            LbCurrentPanel.Text = "Network";
        }

        private void BtnConnection_Click(object sender, EventArgs e)
        {
            openChildForm(new Comunication());
            LbCurrentPanel.Text = "Comunicação";
        }

        private void BtnContinous_Click(object sender, EventArgs e)
        {
            openChildForm(new continuous());
            LbCurrentPanel.Text = "Continuous";
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            while (PnChild.Controls[0] is not System.Windows.Forms.PictureBox)
            {
                PnChild.Controls.RemoveAt(0);
            }
        }

        private void BtnTransient_Click(object sender, EventArgs e)
        {
            openChildForm(new TransientForm());
            LbCurrentPanel.Text = "Transient";
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        static void CaptureActiveWindow()
        {
            IntPtr activeWindowHandle = GetForegroundWindow();

            if (activeWindowHandle != IntPtr.Zero)
            {
                RECT windowRect;
                GetWindowRect(activeWindowHandle, out windowRect);

                int width = windowRect.Right - windowRect.Left;
                int height = windowRect.Bottom - windowRect.Top;

                using (Bitmap bitmap = new Bitmap(width, height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(new Point(windowRect.Left, windowRect.Top), Point.Empty, new Size(width, height));
                    }

                    // Save the captured region as an image
                    bitmap.Save("captured_active_window.png");
                }
            }
        }

        
    }
}
