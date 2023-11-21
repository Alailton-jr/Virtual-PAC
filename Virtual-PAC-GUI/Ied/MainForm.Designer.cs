namespace Ied
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            panel3 = new Panel();
            BtnHome = new Button();
            BtnResize = new Button();
            BtnMinimize = new Button();
            BtnExit = new Button();
            BtnMonitor = new Button();
            BtnIEC61850 = new Button();
            BtnProtection = new Button();
            BtnGenConfig = new Button();
            BtnCommunication = new Button();
            PnChields = new Panel();
            PicBoxLogo = new PictureBox();
            miniToolStrip = new ToolStrip();
            Icons = new ImageList(components);
            panel3.SuspendLayout();
            PnChields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PicBoxLogo).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(1, 22, 39);
            panel3.Controls.Add(BtnHome);
            panel3.Controls.Add(BtnResize);
            panel3.Controls.Add(BtnMinimize);
            panel3.Controls.Add(BtnExit);
            panel3.Controls.Add(BtnMonitor);
            panel3.Controls.Add(BtnIEC61850);
            panel3.Controls.Add(BtnProtection);
            panel3.Controls.Add(BtnGenConfig);
            panel3.Controls.Add(BtnCommunication);
            panel3.Dock = DockStyle.Top;
            panel3.ForeColor = Color.Lavender;
            panel3.Location = new Point(0, 0);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(4);
            panel3.Size = new Size(1264, 65);
            panel3.TabIndex = 2;
            panel3.MouseDown += panelTitleBar_MouseDown;
            // 
            // BtnHome
            // 
            BtnHome.FlatAppearance.BorderSize = 0;
            BtnHome.FlatStyle = FlatStyle.Flat;
            BtnHome.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnHome.ForeColor = Color.Lavender;
            BtnHome.Image = (Image)resources.GetObject("BtnHome.Image");
            BtnHome.ImageAlign = ContentAlignment.MiddleLeft;
            BtnHome.Location = new Point(25, 6);
            BtnHome.Margin = new Padding(3, 2, 3, 2);
            BtnHome.Name = "BtnHome";
            BtnHome.Size = new Size(59, 55);
            BtnHome.TabIndex = 7;
            BtnHome.UseVisualStyleBackColor = true;
            BtnHome.Click += BtnHome_Click;
            // 
            // BtnResize
            // 
            BtnResize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnResize.FlatAppearance.BorderSize = 0;
            BtnResize.FlatStyle = FlatStyle.Flat;
            BtnResize.Image = (Image)resources.GetObject("BtnResize.Image");
            BtnResize.Location = new Point(1159, 7);
            BtnResize.Name = "BtnResize";
            BtnResize.Size = new Size(46, 41);
            BtnResize.TabIndex = 6;
            BtnResize.UseVisualStyleBackColor = true;
            BtnResize.Click += btnMaximize_Click;
            // 
            // BtnMinimize
            // 
            BtnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnMinimize.FlatAppearance.BorderSize = 0;
            BtnMinimize.FlatStyle = FlatStyle.Flat;
            BtnMinimize.Image = (Image)resources.GetObject("BtnMinimize.Image");
            BtnMinimize.Location = new Point(1107, 7);
            BtnMinimize.Name = "BtnMinimize";
            BtnMinimize.Size = new Size(46, 41);
            BtnMinimize.TabIndex = 2;
            BtnMinimize.UseVisualStyleBackColor = true;
            BtnMinimize.Click += btnMinimize_Click;
            // 
            // BtnExit
            // 
            BtnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnExit.FlatAppearance.BorderSize = 0;
            BtnExit.FlatStyle = FlatStyle.Flat;
            BtnExit.Image = (Image)resources.GetObject("BtnExit.Image");
            BtnExit.Location = new Point(1211, 7);
            BtnExit.Name = "BtnExit";
            BtnExit.Size = new Size(46, 41);
            BtnExit.TabIndex = 1;
            BtnExit.UseVisualStyleBackColor = true;
            BtnExit.Click += btnClose_Click;
            // 
            // BtnMonitor
            // 
            BtnMonitor.FlatAppearance.BorderSize = 0;
            BtnMonitor.FlatStyle = FlatStyle.Flat;
            BtnMonitor.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnMonitor.ForeColor = Color.Lavender;
            BtnMonitor.Image = (Image)resources.GetObject("BtnMonitor.Image");
            BtnMonitor.ImageAlign = ContentAlignment.MiddleLeft;
            BtnMonitor.Location = new Point(915, 7);
            BtnMonitor.Margin = new Padding(3, 2, 3, 2);
            BtnMonitor.Name = "BtnMonitor";
            BtnMonitor.Size = new Size(145, 52);
            BtnMonitor.TabIndex = 5;
            BtnMonitor.Text = "Monitor";
            BtnMonitor.TextAlign = ContentAlignment.MiddleRight;
            BtnMonitor.UseVisualStyleBackColor = true;
            BtnMonitor.Click += BtnMonitor_Click;
            // 
            // BtnIEC61850
            // 
            BtnIEC61850.FlatAppearance.BorderSize = 0;
            BtnIEC61850.FlatStyle = FlatStyle.Flat;
            BtnIEC61850.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnIEC61850.ForeColor = Color.Lavender;
            BtnIEC61850.Image = (Image)resources.GetObject("BtnIEC61850.Image");
            BtnIEC61850.ImageAlign = ContentAlignment.MiddleLeft;
            BtnIEC61850.Location = new Point(726, 7);
            BtnIEC61850.Margin = new Padding(3, 2, 3, 2);
            BtnIEC61850.Name = "BtnIEC61850";
            BtnIEC61850.Size = new Size(156, 52);
            BtnIEC61850.TabIndex = 3;
            BtnIEC61850.Text = "IEC 61850";
            BtnIEC61850.TextAlign = ContentAlignment.MiddleRight;
            BtnIEC61850.UseVisualStyleBackColor = true;
            BtnIEC61850.Click += BtnIEC61850_Click;
            // 
            // BtnProtection
            // 
            BtnProtection.FlatAppearance.BorderSize = 0;
            BtnProtection.FlatStyle = FlatStyle.Flat;
            BtnProtection.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnProtection.ForeColor = Color.Lavender;
            BtnProtection.Image = (Image)resources.GetObject("BtnProtection.Image");
            BtnProtection.ImageAlign = ContentAlignment.MiddleLeft;
            BtnProtection.Location = new Point(527, 7);
            BtnProtection.Margin = new Padding(3, 2, 3, 2);
            BtnProtection.Name = "BtnProtection";
            BtnProtection.Size = new Size(166, 52);
            BtnProtection.TabIndex = 2;
            BtnProtection.Text = "Funções de\r\nProteção";
            BtnProtection.TextAlign = ContentAlignment.MiddleRight;
            BtnProtection.UseVisualStyleBackColor = true;
            BtnProtection.Click += BtnProtection_Click;
            // 
            // BtnGenConfig
            // 
            BtnGenConfig.FlatAppearance.BorderSize = 0;
            BtnGenConfig.FlatStyle = FlatStyle.Flat;
            BtnGenConfig.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnGenConfig.ForeColor = Color.Lavender;
            BtnGenConfig.Image = (Image)resources.GetObject("BtnGenConfig.Image");
            BtnGenConfig.ImageAlign = ContentAlignment.MiddleLeft;
            BtnGenConfig.Location = new Point(312, 7);
            BtnGenConfig.Margin = new Padding(3, 2, 3, 2);
            BtnGenConfig.Name = "BtnGenConfig";
            BtnGenConfig.Size = new Size(182, 52);
            BtnGenConfig.TabIndex = 0;
            BtnGenConfig.Text = "Configurações\r\nGerais\r\n";
            BtnGenConfig.TextAlign = ContentAlignment.MiddleRight;
            BtnGenConfig.UseVisualStyleBackColor = true;
            BtnGenConfig.Click += BtnGenConfig_Click;
            // 
            // BtnCommunication
            // 
            BtnCommunication.FlatAppearance.BorderSize = 0;
            BtnCommunication.FlatStyle = FlatStyle.Flat;
            BtnCommunication.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnCommunication.ForeColor = Color.Lavender;
            BtnCommunication.Image = (Image)resources.GetObject("BtnCommunication.Image");
            BtnCommunication.ImageAlign = ContentAlignment.MiddleLeft;
            BtnCommunication.Location = new Point(106, 6);
            BtnCommunication.Margin = new Padding(3, 2, 3, 2);
            BtnCommunication.Name = "BtnCommunication";
            BtnCommunication.Size = new Size(173, 52);
            BtnCommunication.TabIndex = 1;
            BtnCommunication.Text = "Comunicação";
            BtnCommunication.TextAlign = ContentAlignment.MiddleRight;
            BtnCommunication.UseVisualStyleBackColor = true;
            BtnCommunication.Click += BtnCommunication_Click;
            // 
            // PnChields
            // 
            PnChields.Controls.Add(PicBoxLogo);
            PnChields.Dock = DockStyle.Fill;
            PnChields.Location = new Point(0, 65);
            PnChields.Margin = new Padding(3, 2, 3, 2);
            PnChields.Name = "PnChields";
            PnChields.Size = new Size(1264, 616);
            PnChields.TabIndex = 3;
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
            PicBoxLogo.Size = new Size(1264, 616);
            PicBoxLogo.TabIndex = 0;
            PicBoxLogo.TabStop = false;
            // 
            // miniToolStrip
            // 
            miniToolStrip.AccessibleName = "New item selection";
            miniToolStrip.AccessibleRole = AccessibleRole.ButtonDropDown;
            miniToolStrip.AutoSize = false;
            miniToolStrip.CanOverflow = false;
            miniToolStrip.Dock = DockStyle.None;
            miniToolStrip.Font = new Font("Trebuchet MS", 12F, FontStyle.Regular, GraphicsUnit.Point);
            miniToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            miniToolStrip.ImageScalingSize = new Size(20, 20);
            miniToolStrip.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            miniToolStrip.Location = new Point(122, 147);
            miniToolStrip.Name = "miniToolStrip";
            miniToolStrip.RenderMode = ToolStripRenderMode.Professional;
            miniToolStrip.Size = new Size(285, 839);
            miniToolStrip.TabIndex = 1;
            // 
            // Icons
            // 
            Icons.ColorDepth = ColorDepth.Depth8Bit;
            Icons.ImageStream = (ImageListStreamer)resources.GetObject("Icons.ImageStream");
            Icons.TransparentColor = Color.Transparent;
            Icons.Images.SetKeyName(0, "excluir.png");
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1264, 681);
            Controls.Add(PnChields);
            Controls.Add(panel3);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            FormClosing += MainForm_FormClosing;
            Load += Form1_Load;
            Resize += Form1_Resize;
            panel3.ResumeLayout(false);
            PnChields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PicBoxLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private Label label17;
        private Label label18;
        private ToolStripMenuItem salvarToolStripMenuItem;
        private ToolStripMenuItem enviarToolStripMenuItem;
        private Panel panel3;
        private Button BtnIEC61850;
        private Button BtnProtection;
        private Button BtnCommunication;
        private Button BtnGenConfig;
        private Panel PnChields;
        private Button BtnMonitor;
        private ToolStrip miniToolStrip;
        private ImageList Icons;
        private Button BtnResize;
        private Button BtnMinimize;
        private Button BtnExit;
        private Button BtnHome;
        private PictureBox PicBoxLogo;
    }
}