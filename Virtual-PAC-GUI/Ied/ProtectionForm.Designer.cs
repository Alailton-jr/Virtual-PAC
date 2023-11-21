namespace Ied
{
    partial class ProtectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProtectionForm));
            PnMenu = new Panel();
            BtnPtov = new Button();
            BtnPtuv = new Button();
            BtnPdis = new Button();
            BtnPdir = new Button();
            PnPtoc = new Panel();
            BtnPtocNeutral = new Button();
            BtnPtocPhase = new Button();
            BtnPtoc = new Button();
            PnPioc = new Panel();
            BtnPiocNeutral = new Button();
            BtnPiocPhase = new Button();
            BtnPioc = new Button();
            panel2 = new Panel();
            label1 = new Label();
            panel1 = new Panel();
            PnChields = new Panel();
            PicBoxLogo = new PictureBox();
            PnMenu.SuspendLayout();
            PnPtoc.SuspendLayout();
            PnPioc.SuspendLayout();
            PnChields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PicBoxLogo).BeginInit();
            SuspendLayout();
            // 
            // PnMenu
            // 
            PnMenu.AutoScroll = true;
            PnMenu.BackColor = Color.FromArgb(1, 22, 39);
            PnMenu.Controls.Add(BtnPtov);
            PnMenu.Controls.Add(BtnPtuv);
            PnMenu.Controls.Add(BtnPdis);
            PnMenu.Controls.Add(BtnPdir);
            PnMenu.Controls.Add(PnPtoc);
            PnMenu.Controls.Add(BtnPtoc);
            PnMenu.Controls.Add(PnPioc);
            PnMenu.Controls.Add(BtnPioc);
            PnMenu.Controls.Add(panel2);
            PnMenu.Controls.Add(label1);
            PnMenu.Controls.Add(panel1);
            PnMenu.Dock = DockStyle.Left;
            PnMenu.Location = new Point(0, 0);
            PnMenu.Margin = new Padding(3, 2, 3, 2);
            PnMenu.Name = "PnMenu";
            PnMenu.Size = new Size(200, 616);
            PnMenu.TabIndex = 0;
            // 
            // BtnPtov
            // 
            BtnPtov.Dock = DockStyle.Top;
            BtnPtov.FlatAppearance.BorderColor = Color.Black;
            BtnPtov.FlatAppearance.MouseDownBackColor = Color.Black;
            BtnPtov.FlatStyle = FlatStyle.Flat;
            BtnPtov.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnPtov.ForeColor = Color.Lavender;
            BtnPtov.Location = new Point(0, 493);
            BtnPtov.Margin = new Padding(3, 2, 3, 2);
            BtnPtov.Name = "BtnPtov";
            BtnPtov.Padding = new Padding(10, 0, 20, 0);
            BtnPtov.Size = new Size(200, 60);
            BtnPtov.TabIndex = 8;
            BtnPtov.Text = "Over Voltage [PTOV]";
            BtnPtov.UseVisualStyleBackColor = true;
            BtnPtov.Click += BtnPtov_Click;
            // 
            // BtnPtuv
            // 
            BtnPtuv.Dock = DockStyle.Top;
            BtnPtuv.FlatAppearance.BorderColor = Color.Black;
            BtnPtuv.FlatAppearance.MouseDownBackColor = Color.Black;
            BtnPtuv.FlatStyle = FlatStyle.Flat;
            BtnPtuv.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnPtuv.ForeColor = Color.Lavender;
            BtnPtuv.Location = new Point(0, 433);
            BtnPtuv.Margin = new Padding(3, 2, 3, 2);
            BtnPtuv.Name = "BtnPtuv";
            BtnPtuv.Padding = new Padding(10, 0, 20, 0);
            BtnPtuv.Size = new Size(200, 60);
            BtnPtuv.TabIndex = 7;
            BtnPtuv.Text = "Under Voltage [PTUV]";
            BtnPtuv.UseVisualStyleBackColor = true;
            BtnPtuv.Click += BtnPTUV_click;
            // 
            // BtnPdis
            // 
            BtnPdis.Dock = DockStyle.Top;
            BtnPdis.FlatAppearance.BorderColor = Color.Black;
            BtnPdis.FlatAppearance.MouseDownBackColor = Color.Black;
            BtnPdis.FlatStyle = FlatStyle.Flat;
            BtnPdis.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnPdis.ForeColor = Color.Lavender;
            BtnPdis.Location = new Point(0, 384);
            BtnPdis.Margin = new Padding(3, 2, 3, 2);
            BtnPdis.Name = "BtnPdis";
            BtnPdis.Padding = new Padding(10, 0, 20, 0);
            BtnPdis.Size = new Size(200, 49);
            BtnPdis.TabIndex = 10;
            BtnPdis.Text = "Distance [PDIS]";
            BtnPdis.UseVisualStyleBackColor = true;
            BtnPdis.Click += BtnPdis_Click;
            // 
            // BtnPdir
            // 
            BtnPdir.Dock = DockStyle.Top;
            BtnPdir.FlatAppearance.BorderColor = Color.Black;
            BtnPdir.FlatAppearance.MouseDownBackColor = Color.Black;
            BtnPdir.FlatStyle = FlatStyle.Flat;
            BtnPdir.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnPdir.ForeColor = Color.Lavender;
            BtnPdir.Location = new Point(0, 335);
            BtnPdir.Margin = new Padding(3, 2, 3, 2);
            BtnPdir.Name = "BtnPdir";
            BtnPdir.Padding = new Padding(10, 0, 20, 0);
            BtnPdir.Size = new Size(200, 49);
            BtnPdir.TabIndex = 9;
            BtnPdir.Text = "Direction [PDIR]";
            BtnPdir.UseVisualStyleBackColor = true;
            BtnPdir.Click += BtnPdir_Click;
            // 
            // PnPtoc
            // 
            PnPtoc.Controls.Add(BtnPtocNeutral);
            PnPtoc.Controls.Add(BtnPtocPhase);
            PnPtoc.Dock = DockStyle.Top;
            PnPtoc.Location = new Point(0, 272);
            PnPtoc.Margin = new Padding(3, 2, 3, 2);
            PnPtoc.Name = "PnPtoc";
            PnPtoc.Size = new Size(200, 63);
            PnPtoc.TabIndex = 6;
            PnPtoc.Visible = false;
            // 
            // BtnPtocNeutral
            // 
            BtnPtocNeutral.Dock = DockStyle.Top;
            BtnPtocNeutral.FlatAppearance.BorderColor = Color.Black;
            BtnPtocNeutral.FlatAppearance.MouseDownBackColor = Color.Black;
            BtnPtocNeutral.FlatStyle = FlatStyle.Flat;
            BtnPtocNeutral.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnPtocNeutral.ForeColor = Color.Lavender;
            BtnPtocNeutral.Location = new Point(0, 30);
            BtnPtocNeutral.Margin = new Padding(3, 2, 3, 2);
            BtnPtocNeutral.Name = "BtnPtocNeutral";
            BtnPtocNeutral.Size = new Size(200, 30);
            BtnPtocNeutral.TabIndex = 4;
            BtnPtocNeutral.Text = "Neutro [51N]";
            BtnPtocNeutral.UseVisualStyleBackColor = true;
            BtnPtocNeutral.Click += BtnPtocNeutral_Click;
            // 
            // BtnPtocPhase
            // 
            BtnPtocPhase.Dock = DockStyle.Top;
            BtnPtocPhase.FlatAppearance.BorderColor = Color.Black;
            BtnPtocPhase.FlatAppearance.MouseDownBackColor = Color.Black;
            BtnPtocPhase.FlatStyle = FlatStyle.Flat;
            BtnPtocPhase.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnPtocPhase.ForeColor = Color.Lavender;
            BtnPtocPhase.Location = new Point(0, 0);
            BtnPtocPhase.Margin = new Padding(3, 2, 3, 2);
            BtnPtocPhase.Name = "BtnPtocPhase";
            BtnPtocPhase.Size = new Size(200, 30);
            BtnPtocPhase.TabIndex = 3;
            BtnPtocPhase.Text = "Phase [51P]";
            BtnPtocPhase.UseVisualStyleBackColor = true;
            BtnPtocPhase.Click += BtnPtocPhase_Click;
            // 
            // BtnPtoc
            // 
            BtnPtoc.Dock = DockStyle.Top;
            BtnPtoc.FlatAppearance.BorderColor = Color.Black;
            BtnPtoc.FlatAppearance.MouseDownBackColor = Color.Black;
            BtnPtoc.FlatStyle = FlatStyle.Flat;
            BtnPtoc.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnPtoc.ForeColor = Color.Lavender;
            BtnPtoc.Location = new Point(0, 212);
            BtnPtoc.Margin = new Padding(3, 2, 3, 2);
            BtnPtoc.Name = "BtnPtoc";
            BtnPtoc.Padding = new Padding(10, 0, 20, 0);
            BtnPtoc.Size = new Size(200, 60);
            BtnPtoc.TabIndex = 5;
            BtnPtoc.Text = "Inverse Time \r\nOverCurrent [PTOC]";
            BtnPtoc.UseVisualStyleBackColor = true;
            BtnPtoc.Click += BtnPtoc_Click;
            // 
            // PnPioc
            // 
            PnPioc.Controls.Add(BtnPiocNeutral);
            PnPioc.Controls.Add(BtnPiocPhase);
            PnPioc.Dock = DockStyle.Top;
            PnPioc.Location = new Point(0, 149);
            PnPioc.Margin = new Padding(3, 2, 3, 2);
            PnPioc.Name = "PnPioc";
            PnPioc.Size = new Size(200, 63);
            PnPioc.TabIndex = 4;
            PnPioc.Visible = false;
            // 
            // BtnPiocNeutral
            // 
            BtnPiocNeutral.Dock = DockStyle.Top;
            BtnPiocNeutral.FlatAppearance.BorderColor = Color.Black;
            BtnPiocNeutral.FlatAppearance.MouseDownBackColor = Color.Black;
            BtnPiocNeutral.FlatStyle = FlatStyle.Flat;
            BtnPiocNeutral.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnPiocNeutral.ForeColor = Color.Lavender;
            BtnPiocNeutral.Location = new Point(0, 30);
            BtnPiocNeutral.Margin = new Padding(3, 2, 3, 2);
            BtnPiocNeutral.Name = "BtnPiocNeutral";
            BtnPiocNeutral.Size = new Size(200, 30);
            BtnPiocNeutral.TabIndex = 4;
            BtnPiocNeutral.Text = "Neutro [50N]";
            BtnPiocNeutral.UseVisualStyleBackColor = true;
            BtnPiocNeutral.Click += BtnPiocNeutral_Click;
            // 
            // BtnPiocPhase
            // 
            BtnPiocPhase.Dock = DockStyle.Top;
            BtnPiocPhase.FlatAppearance.BorderColor = Color.Black;
            BtnPiocPhase.FlatAppearance.MouseDownBackColor = Color.Black;
            BtnPiocPhase.FlatStyle = FlatStyle.Flat;
            BtnPiocPhase.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnPiocPhase.ForeColor = Color.Lavender;
            BtnPiocPhase.Location = new Point(0, 0);
            BtnPiocPhase.Margin = new Padding(3, 2, 3, 2);
            BtnPiocPhase.Name = "BtnPiocPhase";
            BtnPiocPhase.Size = new Size(200, 30);
            BtnPiocPhase.TabIndex = 3;
            BtnPiocPhase.Text = "Phase [50P]";
            BtnPiocPhase.UseVisualStyleBackColor = true;
            BtnPiocPhase.Click += BtnPiocPhase_Click;
            // 
            // BtnPioc
            // 
            BtnPioc.Dock = DockStyle.Top;
            BtnPioc.FlatAppearance.BorderColor = Color.Black;
            BtnPioc.FlatAppearance.MouseDownBackColor = Color.Black;
            BtnPioc.FlatStyle = FlatStyle.Flat;
            BtnPioc.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnPioc.ForeColor = Color.Lavender;
            BtnPioc.Location = new Point(0, 89);
            BtnPioc.Margin = new Padding(3, 2, 3, 2);
            BtnPioc.Name = "BtnPioc";
            BtnPioc.Padding = new Padding(10, 0, 20, 0);
            BtnPioc.Size = new Size(200, 60);
            BtnPioc.TabIndex = 2;
            BtnPioc.Text = "Instantaneous \r\nOverCurrent [PIOC]";
            BtnPioc.UseVisualStyleBackColor = true;
            BtnPioc.Click += BtnPioc_Click;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 67);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(200, 22);
            panel2.TabIndex = 3;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.AliceBlue;
            label1.Location = new Point(0, 22);
            label1.Name = "label1";
            label1.Size = new Size(200, 45);
            label1.TabIndex = 1;
            label1.Text = "Proteção";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 22);
            panel1.TabIndex = 4;
            // 
            // PnChields
            // 
            PnChields.Controls.Add(PicBoxLogo);
            PnChields.Dock = DockStyle.Fill;
            PnChields.Location = new Point(200, 0);
            PnChields.Margin = new Padding(3, 2, 3, 2);
            PnChields.Name = "PnChields";
            PnChields.Size = new Size(1064, 616);
            PnChields.TabIndex = 1;
            // 
            // PicBoxLogo
            // 
            PicBoxLogo.BackgroundImageLayout = ImageLayout.None;
            PicBoxLogo.Dock = DockStyle.Fill;
            PicBoxLogo.ErrorImage = (Image)resources.GetObject("PicBoxLogo.ErrorImage");
            PicBoxLogo.Image = (Image)resources.GetObject("PicBoxLogo.Image");
            PicBoxLogo.InitialImage = (Image)resources.GetObject("PicBoxLogo.InitialImage");
            PicBoxLogo.Location = new Point(0, 0);
            PicBoxLogo.Name = "PicBoxLogo";
            PicBoxLogo.Size = new Size(1064, 616);
            PicBoxLogo.TabIndex = 1;
            PicBoxLogo.TabStop = false;
            // 
            // ProtectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1264, 616);
            Controls.Add(PnChields);
            Controls.Add(PnMenu);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "ProtectionForm";
            Text = "Protection";
            PnMenu.ResumeLayout(false);
            PnPtoc.ResumeLayout(false);
            PnPioc.ResumeLayout(false);
            PnChields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PicBoxLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel PnMenu;
        private Button BtnPioc;
        private Panel panel2;
        private Label label1;
        private Panel PnPioc;
        private Button BtnPiocNeutral;
        private Button BtnPiocPhase;
        private Panel PnChields;
        private Button BtnPtoc;
        private Panel PnPtoc;
        private Button BtnPtocNeutral;
        private Button BtnPtocPhase;
        private Button BtnPtov;
        private Button BtnPtuv;
        private Panel panel1;
        private Button BtnPdir;
        private Button button1;
        private Button BtnPdis;
        private PictureBox PicBoxLogo;
    }
}