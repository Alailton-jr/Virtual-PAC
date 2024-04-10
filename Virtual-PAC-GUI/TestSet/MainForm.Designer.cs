namespace TestSet
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            contextMenuStrip1 = new ContextMenuStrip(components);
            fileToolStripMenuItem = new ToolStripMenuItem();
            PnSideMenu = new Panel();
            BtnTransient = new Button();
            BtnSequencer = new Button();
            BtnContinous = new Button();
            BtnNetwork = new Button();
            BtnConnection = new Button();
            panel4 = new Panel();
            panel2 = new Panel();
            label1 = new Label();
            PnLogo = new Panel();
            BtnHome = new Button();
            panel3 = new Panel();
            PnChild = new Panel();
            PicBoxLogo = new PictureBox();
            panel1 = new Panel();
            BtnResize = new Button();
            BtnMinimize = new Button();
            BtnExit = new Button();
            LbCurrentPanel = new Label();
            contextMenuStrip1.SuspendLayout();
            PnSideMenu.SuspendLayout();
            panel2.SuspendLayout();
            PnLogo.SuspendLayout();
            PnChild.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PicBoxLogo).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(93, 26);
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(92, 22);
            fileToolStripMenuItem.Text = "File";
            // 
            // PnSideMenu
            // 
            PnSideMenu.AutoScroll = true;
            PnSideMenu.BackColor = Color.FromArgb(1, 22, 39);
            PnSideMenu.Controls.Add(BtnTransient);
            PnSideMenu.Controls.Add(BtnSequencer);
            PnSideMenu.Controls.Add(BtnContinous);
            PnSideMenu.Controls.Add(BtnNetwork);
            PnSideMenu.Controls.Add(BtnConnection);
            PnSideMenu.Controls.Add(panel4);
            PnSideMenu.Controls.Add(panel2);
            PnSideMenu.Controls.Add(PnLogo);
            PnSideMenu.Controls.Add(panel3);
            PnSideMenu.Dock = DockStyle.Left;
            PnSideMenu.Location = new Point(0, 0);
            PnSideMenu.Margin = new Padding(3, 2, 3, 2);
            PnSideMenu.Name = "PnSideMenu";
            PnSideMenu.Size = new Size(184, 681);
            PnSideMenu.TabIndex = 1;
            // 
            // BtnTransient
            // 
            BtnTransient.Dock = DockStyle.Top;
            BtnTransient.FlatAppearance.BorderSize = 0;
            BtnTransient.FlatStyle = FlatStyle.Flat;
            BtnTransient.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            BtnTransient.ForeColor = Color.Lavender;
            BtnTransient.Image = (Image)resources.GetObject("BtnTransient.Image");
            BtnTransient.ImageAlign = ContentAlignment.MiddleLeft;
            BtnTransient.Location = new Point(0, 483);
            BtnTransient.Margin = new Padding(3, 2, 3, 2);
            BtnTransient.Name = "BtnTransient";
            BtnTransient.Size = new Size(184, 82);
            BtnTransient.TabIndex = 8;
            BtnTransient.Text = "     Transient";
            BtnTransient.UseVisualStyleBackColor = true;
            BtnTransient.Click += BtnTransient_Click;
            // 
            // BtnSequencer
            // 
            BtnSequencer.Dock = DockStyle.Top;
            BtnSequencer.FlatAppearance.BorderSize = 0;
            BtnSequencer.FlatStyle = FlatStyle.Flat;
            BtnSequencer.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSequencer.ForeColor = Color.Lavender;
            BtnSequencer.Image = (Image)resources.GetObject("BtnSequencer.Image");
            BtnSequencer.ImageAlign = ContentAlignment.MiddleLeft;
            BtnSequencer.Location = new Point(0, 401);
            BtnSequencer.Margin = new Padding(3, 2, 3, 2);
            BtnSequencer.Name = "BtnSequencer";
            BtnSequencer.Size = new Size(184, 82);
            BtnSequencer.TabIndex = 1;
            BtnSequencer.Text = "      Sequencer";
            BtnSequencer.UseVisualStyleBackColor = true;
            BtnSequencer.Click += BtnSequencer_Click;
            // 
            // BtnContinous
            // 
            BtnContinous.Dock = DockStyle.Top;
            BtnContinous.FlatAppearance.BorderSize = 0;
            BtnContinous.FlatStyle = FlatStyle.Flat;
            BtnContinous.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            BtnContinous.ForeColor = Color.Lavender;
            BtnContinous.Image = (Image)resources.GetObject("BtnContinous.Image");
            BtnContinous.ImageAlign = ContentAlignment.MiddleLeft;
            BtnContinous.Location = new Point(0, 319);
            BtnContinous.Margin = new Padding(3, 2, 3, 2);
            BtnContinous.Name = "BtnContinous";
            BtnContinous.Size = new Size(184, 82);
            BtnContinous.TabIndex = 4;
            BtnContinous.Text = "         Continuous";
            BtnContinous.UseVisualStyleBackColor = true;
            BtnContinous.Click += BtnContinous_Click;
            // 
            // BtnNetwork
            // 
            BtnNetwork.Dock = DockStyle.Top;
            BtnNetwork.FlatAppearance.BorderSize = 0;
            BtnNetwork.FlatStyle = FlatStyle.Flat;
            BtnNetwork.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            BtnNetwork.ForeColor = Color.Lavender;
            BtnNetwork.Image = (Image)resources.GetObject("BtnNetwork.Image");
            BtnNetwork.ImageAlign = ContentAlignment.MiddleLeft;
            BtnNetwork.Location = new Point(0, 237);
            BtnNetwork.Margin = new Padding(3, 2, 3, 2);
            BtnNetwork.Name = "BtnNetwork";
            BtnNetwork.Size = new Size(184, 82);
            BtnNetwork.TabIndex = 2;
            BtnNetwork.Text = "       Network";
            BtnNetwork.UseVisualStyleBackColor = true;
            BtnNetwork.Click += BtnNetwork_Click;
            // 
            // BtnConnection
            // 
            BtnConnection.Dock = DockStyle.Top;
            BtnConnection.FlatAppearance.BorderSize = 0;
            BtnConnection.FlatStyle = FlatStyle.Flat;
            BtnConnection.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            BtnConnection.ForeColor = Color.Lavender;
            BtnConnection.Image = (Image)resources.GetObject("BtnConnection.Image");
            BtnConnection.ImageAlign = ContentAlignment.MiddleLeft;
            BtnConnection.Location = new Point(0, 155);
            BtnConnection.Margin = new Padding(3, 2, 3, 2);
            BtnConnection.Name = "BtnConnection";
            BtnConnection.Size = new Size(184, 82);
            BtnConnection.TabIndex = 3;
            BtnConnection.Text = "General";
            BtnConnection.TextAlign = ContentAlignment.MiddleRight;
            BtnConnection.UseVisualStyleBackColor = true;
            BtnConnection.Click += BtnConnection_Click;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 130);
            panel4.Name = "panel4";
            panel4.Size = new Size(184, 25);
            panel4.TabIndex = 7;
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 84);
            panel2.Name = "panel2";
            panel2.Size = new Size(184, 46);
            panel2.TabIndex = 5;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(184, 46);
            label1.TabIndex = 1;
            label1.Text = "Faculdade de\r\nEngenharia Elétrica";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PnLogo
            // 
            PnLogo.Controls.Add(BtnHome);
            PnLogo.Dock = DockStyle.Top;
            PnLogo.Location = new Point(0, 23);
            PnLogo.Margin = new Padding(3, 2, 3, 2);
            PnLogo.Name = "PnLogo";
            PnLogo.Size = new Size(184, 61);
            PnLogo.TabIndex = 0;
            // 
            // BtnHome
            // 
            BtnHome.Dock = DockStyle.Fill;
            BtnHome.FlatAppearance.BorderSize = 0;
            BtnHome.FlatStyle = FlatStyle.Flat;
            BtnHome.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnHome.ForeColor = Color.Lavender;
            BtnHome.Image = (Image)resources.GetObject("BtnHome.Image");
            BtnHome.Location = new Point(0, 0);
            BtnHome.Margin = new Padding(3, 2, 3, 2);
            BtnHome.Name = "BtnHome";
            BtnHome.Size = new Size(184, 61);
            BtnHome.TabIndex = 8;
            BtnHome.TextAlign = ContentAlignment.MiddleRight;
            BtnHome.UseVisualStyleBackColor = true;
            BtnHome.Click += BtnHome_Click;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(184, 23);
            panel3.TabIndex = 6;
            // 
            // PnChild
            // 
            PnChild.BackColor = Color.FromArgb(40, 58, 73);
            PnChild.Controls.Add(PicBoxLogo);
            PnChild.Dock = DockStyle.Fill;
            PnChild.Location = new Point(184, 51);
            PnChild.Margin = new Padding(3, 2, 3, 2);
            PnChild.Name = "PnChild";
            PnChild.Padding = new Padding(4, 8, 4, 8);
            PnChild.Size = new Size(1080, 630);
            PnChild.TabIndex = 2;
            // 
            // PicBoxLogo
            // 
            PicBoxLogo.BackgroundImageLayout = ImageLayout.Center;
            PicBoxLogo.Dock = DockStyle.Fill;
            PicBoxLogo.ErrorImage = (Image)resources.GetObject("PicBoxLogo.ErrorImage");
            PicBoxLogo.Image = (Image)resources.GetObject("PicBoxLogo.Image");
            PicBoxLogo.InitialImage = (Image)resources.GetObject("PicBoxLogo.InitialImage");
            PicBoxLogo.Location = new Point(4, 8);
            PicBoxLogo.Name = "PicBoxLogo";
            PicBoxLogo.Size = new Size(1072, 614);
            PicBoxLogo.TabIndex = 1;
            PicBoxLogo.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(1, 22, 39);
            panel1.Controls.Add(BtnResize);
            panel1.Controls.Add(BtnMinimize);
            panel1.Controls.Add(BtnExit);
            panel1.Controls.Add(LbCurrentPanel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(184, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(20, 0, 0, 0);
            panel1.Size = new Size(1080, 51);
            panel1.TabIndex = 0;
            panel1.MouseDown += panelTitleBar_MouseDown;
            // 
            // BtnResize
            // 
            BtnResize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnResize.FlatAppearance.BorderSize = 0;
            BtnResize.FlatStyle = FlatStyle.Flat;
            BtnResize.Image = (Image)resources.GetObject("BtnResize.Image");
            BtnResize.Location = new Point(979, 5);
            BtnResize.Name = "BtnResize";
            BtnResize.Size = new Size(46, 41);
            BtnResize.TabIndex = 9;
            BtnResize.UseVisualStyleBackColor = true;
            BtnResize.Click += btnMaximize_Click;
            // 
            // BtnMinimize
            // 
            BtnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnMinimize.FlatAppearance.BorderSize = 0;
            BtnMinimize.FlatStyle = FlatStyle.Flat;
            BtnMinimize.Image = (Image)resources.GetObject("BtnMinimize.Image");
            BtnMinimize.Location = new Point(927, 5);
            BtnMinimize.Name = "BtnMinimize";
            BtnMinimize.Size = new Size(46, 41);
            BtnMinimize.TabIndex = 8;
            BtnMinimize.UseVisualStyleBackColor = true;
            BtnMinimize.Click += btnMinimize_Click;
            // 
            // BtnExit
            // 
            BtnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnExit.FlatAppearance.BorderSize = 0;
            BtnExit.FlatStyle = FlatStyle.Flat;
            BtnExit.Image = (Image)resources.GetObject("BtnExit.Image");
            BtnExit.Location = new Point(1031, 5);
            BtnExit.Name = "BtnExit";
            BtnExit.Size = new Size(46, 41);
            BtnExit.TabIndex = 7;
            BtnExit.UseVisualStyleBackColor = true;
            BtnExit.Click += btnClose_Click;
            // 
            // LbCurrentPanel
            // 
            LbCurrentPanel.Dock = DockStyle.Left;
            LbCurrentPanel.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point);
            LbCurrentPanel.Location = new Point(20, 0);
            LbCurrentPanel.Name = "LbCurrentPanel";
            LbCurrentPanel.Size = new Size(124, 51);
            LbCurrentPanel.TabIndex = 0;
            LbCurrentPanel.Text = "Home";
            LbCurrentPanel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1264, 681);
            Controls.Add(PnChild);
            Controls.Add(panel1);
            Controls.Add(PnSideMenu);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "General";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            contextMenuStrip1.ResumeLayout(false);
            PnSideMenu.ResumeLayout(false);
            panel2.ResumeLayout(false);
            PnLogo.ResumeLayout(false);
            PnChild.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PicBoxLogo).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private Panel PnSideMenu;
        private Button BtnSequencer;
        private Panel PnLogo;
        private Panel PnChild;
        private Button BtnNetwork;
        private Button BtnConnection;
        private Button BtnContinous;
        private Panel panel1;
        private Button BtnHome;
        private Label LbCurrentPanel;
        private Button BtnResize;
        private Button BtnMinimize;
        private Button BtnExit;
        private PictureBox PicBoxLogo;
        private Panel panel2;
        private Panel panel4;
        private Label label1;
        private Panel panel3;
        private Button BtnTransient;
    }
}