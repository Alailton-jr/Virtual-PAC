using OxyPlot.Series;
using OxyPlot;
using System.DirectoryServices.ActiveDirectory;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using OxyPlot.Axes;
using static Ied.MainForm;
using static System.Windows.Forms.DataFormats;
using System.Security.Policy;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Markup;
using Newtonsoft.Json;
using System.Net.Sockets;

namespace Ied
{

    public partial class MainForm : Form
    {

        private string extIp;
        private int extPort;
        public bool callForm(string ip, int port)
        {
            externLoad = true;
            extIp = ip;
            extPort = port;
            this.ShowDialog();
            return true;
        }

        public static Ctl main;

        public static Form mainForm;


        public MainForm()
        {
            InitializeComponent();
            this.Padding = new Padding(borderSize);//Border size
            this.BackColor = Color.FromArgb(40, 58, 73);//Border color
            PicBoxLogo.SizeMode = PictureBoxSizeMode.CenterImage;

            mainForm = this;
        }

        public bool externLoad = false;

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

        //Event methods
        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustForm();
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
                BtnResize.Image = Image.FromFile("C:\\Users\\ALAILTON\\My Drive\\Faculdade\\TCC\\Código\\Visual Studio\\Images\\inside.png");
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = formSize;
                BtnResize.Image = Image.FromFile("C:\\Users\\ALAILTON\\My Drive\\Faculdade\\TCC\\Código\\Visual Studio\\Images\\outside.png");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            //Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            formSize = this.ClientSize;

            main = new Ctl();
            main.loadClass();

            if (externLoad)
            {
                if (main.serverCon == null) main.serverCon = new SocketConnection();
                main.serverCon.changeConProperties(extIp, extPort);
                
                string res = main.serverCon.SendData("LoadIedConfig", "all");
                if (res != null)
                {
                    main.parserYaml(res);
                    main.communication.ip = extIp;
                    main.communication.port = extPort;
                }
                else {
                    MessageBox.Show("Error While loading File");
                    this.Close();
                }

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
            PnChields.Controls.Add(chieldForm);
            PnChields.Tag = chieldForm;
            chieldForm.BringToFront();
            chieldForm.Show();
        }

        private Button currentSelectedButton;

        private void BtnProtection_Click(object sender, EventArgs e)
        {

            if (currentSelectedButton != null)
            {
                currentSelectedButton.BackColor = Color.FromArgb(1, 22, 39);

            }
            currentSelectedButton = sender as Button;
            if (currentSelectedButton != null)
            {
                currentSelectedButton.BackColor = Color.FromArgb(31, 45, 56);
            }

            openChildForm(new ProtectionForm());
        }

        private void BtnCommunication_Click(object sender, EventArgs e)
        {

            if (currentSelectedButton != null)
            {
                currentSelectedButton.BackColor = Color.FromArgb(1, 22, 39);

            }
            currentSelectedButton = sender as Button;
            if (currentSelectedButton != null)
            {
                currentSelectedButton.BackColor = Color.FromArgb(31, 45, 56);
            }

            openChildForm(new ComunicationForm());
        }

        private void BtnIEC61850_Click(object sender, EventArgs e)
        {

            if (currentSelectedButton != null)
            {
                currentSelectedButton.BackColor = Color.FromArgb(1, 22, 39);

            }
            currentSelectedButton = sender as Button;
            if (currentSelectedButton != null)
            {
                currentSelectedButton.BackColor = Color.FromArgb(31, 45, 56);
            }
            openChildForm(new Iec61850Form());

        }

        private void BtnMonitor_Click(object sender, EventArgs e)
        {

            if (currentSelectedButton != null)
            {
                currentSelectedButton.BackColor = Color.FromArgb(1, 22, 39);

            }
            currentSelectedButton = sender as Button;
            if (currentSelectedButton != null)
            {
                currentSelectedButton.BackColor = Color.FromArgb(31, 45, 56);
            }

            openChildForm(new MonitoringForm());
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            while (PnChields.Controls[0] is not System.Windows.Forms.PictureBox)
            {
                PnChields.Controls.RemoveAt(0);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            main.sclConf = new SCLConfig();
            main.saveClass();
        }

        private void BtnGenConfig_Click(object sender, EventArgs e)
        {
            if (currentSelectedButton != null)
            {
                currentSelectedButton.BackColor = Color.FromArgb(1, 22, 39);

            }
            currentSelectedButton = sender as Button;
            if (currentSelectedButton != null)
            {
                currentSelectedButton.BackColor = Color.FromArgb(31, 45, 56);
            }
            openChildForm(new GeneralForm());
        }
    }
}