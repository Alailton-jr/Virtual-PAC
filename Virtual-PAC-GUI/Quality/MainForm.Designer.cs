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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            PnTop = new Panel();
            BtnResize = new Button();
            BtnMinimize = new Button();
            BtnExit = new Button();
            PnLeft = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            BtnSniffer = new Button();
            BtnGeneral = new Button();
            BtnServerConfig = new Button();
            label1 = new Label();
            button3 = new Button();
            PnContent = new Panel();
            panel4 = new Panel();
            PnButton = new Panel();
            button1 = new Button();
            button2 = new Button();
            button4 = new Button();
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
            PnTop.Size = new Size(1062, 47);
            PnTop.TabIndex = 0;
            PnTop.MouseDown += topPanel_MouseDown;
            // 
            // BtnResize
            // 
            BtnResize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnResize.FlatAppearance.BorderSize = 0;
            BtnResize.FlatStyle = FlatStyle.Flat;
            BtnResize.Image = (Image)resources.GetObject("BtnResize.Image");
            BtnResize.Location = new Point(958, 3);
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
            BtnMinimize.Location = new Point(906, 3);
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
            BtnExit.Location = new Point(1010, 3);
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
            PnLeft.Controls.Add(button3);
            PnLeft.Dock = DockStyle.Left;
            PnLeft.Location = new Point(0, 0);
            PnLeft.Name = "PnLeft";
            PnLeft.Padding = new Padding(10, 40, 10, 10);
            PnLeft.Size = new Size(200, 654);
            PnLeft.TabIndex = 1;
            PnLeft.MouseDown += topPanel_MouseDown;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(BtnSniffer, 0, 2);
            tableLayoutPanel1.Controls.Add(BtnGeneral, 0, 1);
            tableLayoutPanel1.Controls.Add(BtnServerConfig, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(10, 140);
            tableLayoutPanel1.Margin = new Padding(3, 30, 3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(0, 40, 0, 0);
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(180, 190);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // BtnSniffer
            // 
            BtnSniffer.AutoSize = true;
            BtnSniffer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnSniffer.Dock = DockStyle.Top;
            BtnSniffer.FlatStyle = FlatStyle.Flat;
            BtnSniffer.Font = new Font("Segoe UI", 13F);
            BtnSniffer.Location = new Point(3, 143);
            BtnSniffer.Margin = new Padding(3, 3, 3, 10);
            BtnSniffer.Name = "BtnSniffer";
            BtnSniffer.Size = new Size(174, 37);
            BtnSniffer.TabIndex = 4;
            BtnSniffer.Text = "Sniffer";
            BtnSniffer.TextAlign = ContentAlignment.MiddleRight;
            BtnSniffer.UseVisualStyleBackColor = true;
            BtnSniffer.Click += BtnSniffer_Click;
            // 
            // BtnGeneral
            // 
            BtnGeneral.AutoSize = true;
            BtnGeneral.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnGeneral.Dock = DockStyle.Top;
            BtnGeneral.FlatStyle = FlatStyle.Flat;
            BtnGeneral.Font = new Font("Segoe UI", 13F);
            BtnGeneral.Location = new Point(3, 93);
            BtnGeneral.Margin = new Padding(3, 3, 3, 10);
            BtnGeneral.Name = "BtnGeneral";
            BtnGeneral.Size = new Size(174, 37);
            BtnGeneral.TabIndex = 3;
            BtnGeneral.Text = "General Analyse";
            BtnGeneral.TextAlign = ContentAlignment.MiddleRight;
            BtnGeneral.UseVisualStyleBackColor = true;
            // 
            // BtnServerConfig
            // 
            BtnServerConfig.AutoSize = true;
            BtnServerConfig.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnServerConfig.Dock = DockStyle.Top;
            BtnServerConfig.FlatStyle = FlatStyle.Flat;
            BtnServerConfig.Font = new Font("Segoe UI", 13F);
            BtnServerConfig.Location = new Point(3, 43);
            BtnServerConfig.Margin = new Padding(3, 3, 3, 10);
            BtnServerConfig.Name = "BtnServerConfig";
            BtnServerConfig.Size = new Size(174, 37);
            BtnServerConfig.TabIndex = 2;
            BtnServerConfig.Text = "Server Config";
            BtnServerConfig.TextAlign = ContentAlignment.MiddleRight;
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
            label1.Text = "Faculdade de \r\nEngenharia Elétrica\r\n";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button3
            // 
            button3.Dock = DockStyle.Top;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(10, 40);
            button3.Name = "button3";
            button3.Size = new Size(180, 45);
            button3.TabIndex = 4;
            button3.Text = "HOME";
            button3.UseVisualStyleBackColor = true;
            // 
            // PnContent
            // 
            PnContent.BackColor = Color.FromArgb(40, 58, 73);
            PnContent.Dock = DockStyle.Fill;
            PnContent.Location = new Point(200, 47);
            PnContent.Name = "PnContent";
            PnContent.Padding = new Padding(10);
            PnContent.Size = new Size(1062, 607);
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
            panel4.Size = new Size(1264, 681);
            panel4.TabIndex = 6;
            // 
            // PnButton
            // 
            PnButton.Controls.Add(button1);
            PnButton.Controls.Add(button2);
            PnButton.Controls.Add(button4);
            PnButton.Dock = DockStyle.Bottom;
            PnButton.Location = new Point(0, 654);
            PnButton.Name = "PnButton";
            PnButton.Size = new Size(1262, 25);
            PnButton.TabIndex = 1;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(2020, 3);
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
            button2.Location = new Point(1968, 3);
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
            button4.Location = new Point(2072, 3);
            button4.Name = "button4";
            button4.Size = new Size(46, 41);
            button4.TabIndex = 10;
            button4.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(1, 22, 39);
            ClientSize = new Size(1264, 681);
            Controls.Add(panel4);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            PnTop.ResumeLayout(false);
            PnLeft.ResumeLayout(false);
            PnLeft.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            PnButton.ResumeLayout(false);
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
        private Button button3;
        private Panel panel4;
        private Panel PnButton;
        private Button button1;
        private Button button2;
        private Button button4;
        private Button BtnSniffer;
    }
}
