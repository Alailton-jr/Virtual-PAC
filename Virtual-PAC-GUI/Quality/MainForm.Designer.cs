namespace Quality
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
            PnTop = new Panel();
            BtnResize = new Button();
            BtnMinimize = new Button();
            BtnExit = new Button();
            PnLeft = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            button3 = new Button();
            BtnMonitor = new Button();
            BtnSniffer = new Button();
            BtnGeneral = new Button();
            BtnServerConfig = new Button();
            label1 = new Label();
            BtnHome = new Button();
            PnContent = new Panel();
            panel4 = new Panel();
            PnButton = new Panel();
            LbConStatus = new Label();
            button1 = new Button();
            button2 = new Button();
            button4 = new Button();
            TimerServerCon = new System.Windows.Forms.Timer(components);
            PnTop.SuspendLayout();
            PnLeft.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel4.SuspendLayout();
            PnButton.SuspendLayout();
            SuspendLayout();
            // 
            // PnTop
            // 
            PnTop.AutoSize = true;
            PnTop.Controls.Add(BtnResize);
            PnTop.Controls.Add(BtnMinimize);
            PnTop.Controls.Add(BtnExit);
            PnTop.Dock = DockStyle.Top;
            PnTop.Location = new Point(200, 0);
            PnTop.Name = "PnTop";
            PnTop.Size = new Size(1078, 47);
            PnTop.TabIndex = 0;
            PnTop.MouseDown += topPanel_MouseDown;
            // 
            // BtnResize
            // 
            BtnResize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnResize.FlatAppearance.BorderSize = 0;
            BtnResize.FlatStyle = FlatStyle.Flat;
            BtnResize.Image = (Image)resources.GetObject("BtnResize.Image");
            BtnResize.Location = new Point(974, 3);
            BtnResize.Name = "BtnResize";
            BtnResize.Size = new Size(46, 41);
            BtnResize.TabIndex = 12;
            BtnResize.UseVisualStyleBackColor = true;
            BtnResize.Click += MaximizeButton_Click;
            // 
            // BtnMinimize
            // 
            BtnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnMinimize.FlatAppearance.BorderSize = 0;
            BtnMinimize.FlatStyle = FlatStyle.Flat;
            BtnMinimize.Image = (Image)resources.GetObject("BtnMinimize.Image");
            BtnMinimize.Location = new Point(922, 3);
            BtnMinimize.Name = "BtnMinimize";
            BtnMinimize.Size = new Size(46, 41);
            BtnMinimize.TabIndex = 11;
            BtnMinimize.UseVisualStyleBackColor = true;
            BtnMinimize.Click += MinimizeButton_Click;
            // 
            // BtnExit
            // 
            BtnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnExit.FlatAppearance.BorderSize = 0;
            BtnExit.FlatStyle = FlatStyle.Flat;
            BtnExit.Image = (Image)resources.GetObject("BtnExit.Image");
            BtnExit.Location = new Point(1026, 3);
            BtnExit.Name = "BtnExit";
            BtnExit.Size = new Size(46, 41);
            BtnExit.TabIndex = 10;
            BtnExit.UseVisualStyleBackColor = true;
            BtnExit.Click += CloseButton_Click;
            // 
            // PnLeft
            // 
            PnLeft.Controls.Add(tableLayoutPanel1);
            PnLeft.Controls.Add(label1);
            PnLeft.Controls.Add(BtnHome);
            PnLeft.Dock = DockStyle.Left;
            PnLeft.Location = new Point(0, 0);
            PnLeft.Name = "PnLeft";
            PnLeft.Padding = new Padding(10, 40, 10, 10);
            PnLeft.Size = new Size(200, 693);
            PnLeft.TabIndex = 1;
            PnLeft.MouseDown += topPanel_MouseDown;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(button3, 0, 4);
            tableLayoutPanel1.Controls.Add(BtnMonitor, 0, 2);
            tableLayoutPanel1.Controls.Add(BtnSniffer, 0, 3);
            tableLayoutPanel1.Controls.Add(BtnGeneral, 0, 1);
            tableLayoutPanel1.Controls.Add(BtnServerConfig, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(10, 140);
            tableLayoutPanel1.Margin = new Padding(3, 30, 0, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(0, 40, 0, 0);
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Size = new Size(180, 335);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // button3
            // 
            button3.AutoSize = true;
            button3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button3.Dock = DockStyle.Top;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI", 15F);
            button3.Image = Properties.Resources.file;
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.Location = new Point(3, 279);
            button3.Margin = new Padding(3, 3, 3, 18);
            button3.Name = "button3";
            button3.Size = new Size(174, 38);
            button3.TabIndex = 6;
            button3.Text = "          Load SCL";
            button3.TextAlign = ContentAlignment.MiddleLeft;
            button3.UseVisualStyleBackColor = true;
            // 
            // BtnMonitor
            // 
            BtnMonitor.AutoSize = true;
            BtnMonitor.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnMonitor.Dock = DockStyle.Top;
            BtnMonitor.FlatAppearance.BorderSize = 0;
            BtnMonitor.FlatStyle = FlatStyle.Flat;
            BtnMonitor.Font = new Font("Segoe UI", 15F);
            BtnMonitor.Image = Properties.Resources.spyware;
            BtnMonitor.ImageAlign = ContentAlignment.MiddleLeft;
            BtnMonitor.Location = new Point(3, 161);
            BtnMonitor.Margin = new Padding(3, 3, 3, 18);
            BtnMonitor.Name = "BtnMonitor";
            BtnMonitor.Size = new Size(174, 38);
            BtnMonitor.TabIndex = 5;
            BtnMonitor.Text = "          Monitor";
            BtnMonitor.TextAlign = ContentAlignment.MiddleLeft;
            BtnMonitor.UseVisualStyleBackColor = true;
            BtnMonitor.Click += BtnMonitor_Click;
            // 
            // BtnSniffer
            // 
            BtnSniffer.AutoSize = true;
            BtnSniffer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnSniffer.Dock = DockStyle.Top;
            BtnSniffer.FlatAppearance.BorderSize = 0;
            BtnSniffer.FlatStyle = FlatStyle.Flat;
            BtnSniffer.Font = new Font("Segoe UI", 15F);
            BtnSniffer.Image = Properties.Resources.data;
            BtnSniffer.ImageAlign = ContentAlignment.MiddleLeft;
            BtnSniffer.Location = new Point(3, 220);
            BtnSniffer.Margin = new Padding(3, 3, 3, 18);
            BtnSniffer.Name = "BtnSniffer";
            BtnSniffer.Size = new Size(174, 38);
            BtnSniffer.TabIndex = 4;
            BtnSniffer.Text = "          Sniffer";
            BtnSniffer.TextAlign = ContentAlignment.MiddleLeft;
            BtnSniffer.UseVisualStyleBackColor = true;
            BtnSniffer.Click += BtnSniffer_Click;
            // 
            // BtnGeneral
            // 
            BtnGeneral.AutoSize = true;
            BtnGeneral.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnGeneral.Dock = DockStyle.Top;
            BtnGeneral.FlatAppearance.BorderSize = 0;
            BtnGeneral.FlatStyle = FlatStyle.Flat;
            BtnGeneral.Font = new Font("Segoe UI", 15F);
            BtnGeneral.Image = Properties.Resources.monitoring;
            BtnGeneral.ImageAlign = ContentAlignment.MiddleLeft;
            BtnGeneral.Location = new Point(3, 102);
            BtnGeneral.Margin = new Padding(3, 3, 3, 18);
            BtnGeneral.Name = "BtnGeneral";
            BtnGeneral.Size = new Size(174, 38);
            BtnGeneral.TabIndex = 3;
            BtnGeneral.Text = "          Analyse";
            BtnGeneral.TextAlign = ContentAlignment.MiddleLeft;
            BtnGeneral.UseVisualStyleBackColor = true;
            BtnGeneral.Click += BtnGeneral_Click;
            // 
            // BtnServerConfig
            // 
            BtnServerConfig.AutoSize = true;
            BtnServerConfig.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnServerConfig.Dock = DockStyle.Top;
            BtnServerConfig.FlatAppearance.BorderSize = 0;
            BtnServerConfig.FlatStyle = FlatStyle.Flat;
            BtnServerConfig.Font = new Font("Segoe UI", 15F);
            BtnServerConfig.Image = Properties.Resources.process;
            BtnServerConfig.ImageAlign = ContentAlignment.MiddleLeft;
            BtnServerConfig.Location = new Point(3, 43);
            BtnServerConfig.Margin = new Padding(3, 3, 3, 18);
            BtnServerConfig.Name = "BtnServerConfig";
            BtnServerConfig.Size = new Size(174, 38);
            BtnServerConfig.TabIndex = 2;
            BtnServerConfig.Text = "          VM Config";
            BtnServerConfig.TextAlign = ContentAlignment.MiddleLeft;
            BtnServerConfig.UseVisualStyleBackColor = true;
            BtnServerConfig.Click += BtnServerConfig_Click;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label1.Location = new Point(10, 85);
            label1.Name = "label1";
            label1.Padding = new Padding(0, 8, 0, 0);
            label1.Size = new Size(180, 55);
            label1.TabIndex = 5;
            label1.Text = "Laboratório de Sistemas de Energia Elétrica";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BtnHome
            // 
            BtnHome.Dock = DockStyle.Top;
            BtnHome.FlatAppearance.BorderSize = 0;
            BtnHome.FlatStyle = FlatStyle.Flat;
            BtnHome.Image = Properties.Resources.logo_removebg_preview2;
            BtnHome.Location = new Point(10, 40);
            BtnHome.Name = "BtnHome";
            BtnHome.Size = new Size(180, 45);
            BtnHome.TabIndex = 4;
            BtnHome.UseVisualStyleBackColor = true;
            // 
            // PnContent
            // 
            PnContent.BackColor = Color.FromArgb(40, 58, 73);
            PnContent.Dock = DockStyle.Fill;
            PnContent.Location = new Point(200, 47);
            PnContent.Name = "PnContent";
            PnContent.Size = new Size(1078, 646);
            PnContent.TabIndex = 2;
            PnContent.MouseDown += topPanel_MouseDown;
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(PnContent);
            panel4.Controls.Add(PnTop);
            panel4.Controls.Add(PnLeft);
            panel4.Controls.Add(PnButton);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(1280, 720);
            panel4.TabIndex = 6;
            // 
            // PnButton
            // 
            PnButton.Controls.Add(LbConStatus);
            PnButton.Controls.Add(button1);
            PnButton.Controls.Add(button2);
            PnButton.Controls.Add(button4);
            PnButton.Dock = DockStyle.Bottom;
            PnButton.Location = new Point(0, 693);
            PnButton.Name = "PnButton";
            PnButton.Size = new Size(1278, 25);
            PnButton.TabIndex = 1;
            // 
            // LbConStatus
            // 
            LbConStatus.AutoSize = true;
            LbConStatus.Location = new Point(5, 5);
            LbConStatus.Name = "LbConStatus";
            LbConStatus.Size = new Size(0, 15);
            LbConStatus.TabIndex = 13;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(2036, 3);
            button1.Name = "button1";
            button1.Size = new Size(46, 41);
            button1.TabIndex = 12;
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.Location = new Point(1984, 3);
            button2.Name = "button2";
            button2.Size = new Size(46, 41);
            button2.TabIndex = 11;
            button2.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Image = (Image)resources.GetObject("button4.Image");
            button4.Location = new Point(2088, 3);
            button4.Name = "button4";
            button4.Size = new Size(46, 41);
            button4.TabIndex = 10;
            button4.UseVisualStyleBackColor = true;
            // 
            // TimerServerCon
            // 
            TimerServerCon.Interval = 1000;
            TimerServerCon.Tick += TimerServerCon_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(1, 22, 39);
            ClientSize = new Size(1280, 720);
            Controls.Add(panel4);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            FormClosing += MainForm_FormClosing;
            PnTop.ResumeLayout(false);
            PnLeft.ResumeLayout(false);
            PnLeft.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            PnButton.ResumeLayout(false);
            PnButton.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel PnTop;
        private Button BtnResize;
        private Button BtnMinimize;
        private Button BtnExit;
        private Panel PnLeft;
        private Button BtnServerConfig;
        private TableLayoutPanel tableLayoutPanel1;
        private Button BtnGeneral;
        private Panel PnContent;
        private Label label1;
        private Button BtnHome;
        private Panel panel4;
        private Panel PnButton;
        private Button button1;
        private Button button2;
        private Button button4;
        private Button BtnSniffer;
        private Button BtnMonitor;
        private System.Windows.Forms.Timer TimerServerCon;
        private Label LbConStatus;
        private Button button3;
    }
}
