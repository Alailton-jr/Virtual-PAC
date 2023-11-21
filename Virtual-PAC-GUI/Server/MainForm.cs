using System.Runtime.InteropServices;
using static Server.classes;

namespace Server
{
    public partial class MainForm : Form
    {
        public static ServerCtl main;

        public MainForm()
        {
            InitializeComponent();
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
                BtnResize.Image = Properties.Resources.outside;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = formSize;
                BtnResize.Image = Properties.Resources.inside;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            main = new ServerCtl();
            main.loadClass();
            openChildForm(new VmsForm());
        }

        private Form activeForm = null;
        private void openChildForm(Form chieldForm)
        {
            activeForm = chieldForm;
            chieldForm.TopLevel = false;
            chieldForm.FormBorderStyle = FormBorderStyle.None;
            chieldForm.Dock = DockStyle.Fill;
            PnChields.Controls.Add(chieldForm);
            PnChields.Tag = chieldForm;
            chieldForm.BringToFront();
            chieldForm.Show();
        }

        private void BtnVmForm_Click(object sender, EventArgs e)
        {
            openChildForm(new VmsForm());
        }

        private void BtnMMS_Click(object sender, EventArgs e)
        {
            openChildForm(new MmsForm());
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            main.saveClass();
        }
    }
}