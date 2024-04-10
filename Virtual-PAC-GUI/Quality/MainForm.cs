using System.Globalization;
using System.Runtime.InteropServices;
using static Quality.Classes;

namespace Quality
{
    public partial class MainForm : Form
    {

        private Form activeForm = null;
        private GeneralForm generalForm;
        private SnifferForm snifferForm;
        public static vQualityControl mainControl;

        public MainForm()
        {
            CultureInfo culture = new CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            InitializeComponent();
            mainControl = new vQualityControl();
            generalForm = new GeneralForm();
            snifferForm = new SnifferForm();
        }

        #region Windows Config

        // Move Window
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        // Buttons
        public void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        public void MaximizeButton_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        #endregion

        private void openChildForm(Form chieldForm)
        {
            //if (activeForm != null)
            //    activeForm.Close();
            activeForm = chieldForm;
            chieldForm.TopLevel = false;
            chieldForm.FormBorderStyle = FormBorderStyle.None;
            chieldForm.Dock = DockStyle.Fill;
            PnContent.Controls.Add(chieldForm);
            PnContent.Tag = chieldForm;
            chieldForm.BringToFront();
            chieldForm.Show();
        }

        private void BtnServerConfig_Click(object sender, EventArgs e)
        {
            openChildForm(generalForm);
        }

        private void BtnSniffer_Click(object sender, EventArgs e)
        {
            openChildForm(snifferForm);
        }
    }
}
