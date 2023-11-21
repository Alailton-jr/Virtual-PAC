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
using static Ied.yamlClass;

namespace Ied
{
    public partial class ProtectionForm : Form
    {
        // Main buttons
        private Button curButton;
        private Panel leftBorderBtn;
        private Panel curPanel;
        //Sub Buttons
        private Button curSubButton;
        private Panel curSubPanel;
        private Panel leftBorderPiocBtn;
        private Panel leftBorderPtocBtn;

        // Form Panels
        private Form activeForm = null;

        public ProtectionForm()
        {
            InitializeComponent();

            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            PnMenu.Controls.Add(leftBorderBtn);
            leftBorderBtn.Location = new Point(0, 0);

            leftBorderPiocBtn = new Panel();
            leftBorderPiocBtn.Size = new Size(7, 30);
            PnPioc.Controls.Add(leftBorderPiocBtn);
            leftBorderBtn.Location = new Point(0, 0);

            leftBorderPtocBtn = new Panel();
            leftBorderPtocBtn.Size = new Size(7, 30);
            PnPtoc.Controls.Add(leftBorderPtocBtn);
            leftBorderBtn.Location = new Point(0, 0);
            PicBoxLogo.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        //Methods

        private void activeButton(object sender, Color color, ref Button oldBtn, Panel border)
        {
            if (sender != null)
            {
                disableButton(oldBtn);
                // button
                oldBtn = (Button)sender;
                oldBtn.BackColor = Color.FromArgb(37, 36, 81);
                oldBtn.ForeColor = color;
                // left border
                border.BackColor = color;
                border.Location = new Point(border.Location.X, oldBtn.Location.Y);
                border.Visible = true;
                border.BringToFront();
            }
        }

        private void disableButton(Button oldBtn)
        {
            if (oldBtn != null)
            {
                oldBtn.BackColor = Color.FromArgb(1, 22, 39);
                oldBtn.ForeColor = Color.Lavender;
            }
        }

        private void disableBorder(Panel border)
        {
            border.Visible = false;
        }

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

        private void BtnPioc_Click(object sender, EventArgs e)
        {

            if (curPanel != null) curPanel.Visible = false;
            curPanel = PnPioc;
            curPanel.Visible = true;

            disableButton(curSubButton);
            disableBorder(leftBorderPiocBtn);
            disableBorder(leftBorderPtocBtn);

            activeButton(sender, Color.Lavender, ref curButton, leftBorderBtn);
        }

        private void BtnPiocPhase_Click(object sender, EventArgs e)
        {

            activeButton(sender, Color.Lavender, ref curSubButton, leftBorderPiocBtn);
            var x = new PiocForm()
            {
                conf50 = main.pioc.phase,
                conf51 = main.ptoc.phase,
                formName = "SobreCorrente Instântanea de Fase"
            };
            openChildForm(x);
        }

        private void BtnPiocNeutral_Click(object sender, EventArgs e)
        {
            activeButton(sender, Color.Lavender, ref curSubButton, leftBorderPiocBtn);
            var x = new PiocForm()
            {
                conf50 = main.pioc.neutral,
                conf51 = main.ptoc.neutral,
                formName = "SobreCorrente Instântanea de Neutro"
            };
            openChildForm(x);
        }

        private void BtnPtoc_Click(object sender, EventArgs e)
        {

            if (curPanel != null) curPanel.Visible = false;
            curPanel = PnPtoc; curPanel.Visible = true;

            disableButton(curSubButton);
            disableBorder(leftBorderPiocBtn);
            disableBorder(leftBorderPtocBtn);
            activeButton(sender, Color.Lavender, ref curButton, leftBorderBtn);


        }

        private void BtnPtocPhase_Click(object sender, EventArgs e)
        {
            activeButton(sender, Color.Lavender, ref curSubButton, leftBorderPtocBtn);

            var x = new PtocForm()
            {
                conf50 = main.pioc.phase,
                conf51 = main.ptoc.phase,
                formName = "SobreCorrente de Tempo Inverso de Fase"
            };
            openChildForm(x);
        }

        private void BtnPtocNeutral_Click(object sender, EventArgs e)
        {
            activeButton(sender, Color.Lavender, ref curSubButton, leftBorderPtocBtn);
            var x = new PtocForm()
            {
                conf50 = main.pioc.neutral,
                conf51 = main.ptoc.neutral,
                formName = "SobreCorrente de Tempo Inverso de Neutro"
            };
            openChildForm(x);
        }

        private void BtnPTUV_click(object sender, EventArgs e)
        {
            disableButton(curSubButton);
            disableBorder(leftBorderPiocBtn);
            disableBorder(leftBorderPtocBtn);
            activeButton(sender, Color.Lavender, ref curButton, leftBorderBtn);
            var x = new PtuvForm();
            openChildForm(x);
        }

        private void BtnPtov_Click(object sender, EventArgs e)
        {
            disableButton(curSubButton);
            disableBorder(leftBorderPiocBtn);
            disableBorder(leftBorderPtocBtn);
            activeButton(sender, Color.Lavender, ref curButton, leftBorderBtn);
            var x = new PtovForm();
            openChildForm(x);
        }

        private void BtnPdir_Click(object sender, EventArgs e)
        {
            disableButton(curSubButton);
            disableBorder(leftBorderPiocBtn);
            disableBorder(leftBorderPtocBtn);
            activeButton(sender, Color.Lavender, ref curButton, leftBorderBtn);
            var x = new PdirForm();
            openChildForm(x);
        }

        private void BtnPdis_Click(object sender, EventArgs e)
        {
            disableButton(curSubButton);
            disableBorder(leftBorderPiocBtn);
            disableBorder(leftBorderPtocBtn);
            activeButton(sender, Color.Lavender, ref curButton, leftBorderBtn);
            var x = new PdisForm();
            openChildForm(x);
        }
    }
}
