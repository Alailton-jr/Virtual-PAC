namespace Server
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
            panel1 = new Panel();
            PnChields = new Panel();
            PnTitle = new Panel();
            BtnMMS = new Button();
            button5 = new Button();
            BtnVmForm = new Button();
            DeviceImages = new ImageList(components);
            BtnResize = new Button();
            BtnMinimize = new Button();
            BtnExit = new Button();
            panel1.SuspendLayout();
            PnTitle.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(PnChields);
            panel1.Controls.Add(PnTitle);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1280, 720);
            panel1.TabIndex = 0;
            // 
            // PnChields
            // 
            PnChields.Dock = DockStyle.Fill;
            PnChields.Location = new Point(0, 60);
            PnChields.Margin = new Padding(3, 2, 3, 2);
            PnChields.Name = "PnChields";
            PnChields.Size = new Size(1280, 660);
            PnChields.TabIndex = 2;
            // 
            // PnTitle
            // 
            PnTitle.BackColor = Color.FromArgb(1, 22, 39);
            PnTitle.Controls.Add(BtnResize);
            PnTitle.Controls.Add(BtnMinimize);
            PnTitle.Controls.Add(BtnMMS);
            PnTitle.Controls.Add(BtnExit);
            PnTitle.Controls.Add(button5);
            PnTitle.Controls.Add(BtnVmForm);
            PnTitle.Dock = DockStyle.Top;
            PnTitle.Location = new Point(0, 0);
            PnTitle.Margin = new Padding(3, 2, 3, 2);
            PnTitle.Name = "PnTitle";
            PnTitle.Size = new Size(1280, 60);
            PnTitle.TabIndex = 1;
            PnTitle.MouseDown += panelTitleBar_MouseDown;
            // 
            // BtnMMS
            // 
            BtnMMS.Dock = DockStyle.Left;
            BtnMMS.FlatStyle = FlatStyle.Flat;
            BtnMMS.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnMMS.ForeColor = Color.Lavender;
            BtnMMS.Location = new Point(298, 0);
            BtnMMS.Margin = new Padding(3, 2, 3, 2);
            BtnMMS.Name = "BtnMMS";
            BtnMMS.Size = new Size(149, 60);
            BtnMMS.TabIndex = 8;
            BtnMMS.Text = "Supervisório";
            BtnMMS.UseVisualStyleBackColor = true;
            BtnMMS.Click += BtnMMS_Click;
            // 
            // button5
            // 
            button5.Dock = DockStyle.Left;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button5.ForeColor = Color.Lavender;
            button5.Location = new Point(149, 0);
            button5.Margin = new Padding(3, 2, 3, 2);
            button5.Name = "button5";
            button5.Size = new Size(149, 60);
            button5.TabIndex = 7;
            button5.Text = "Monitoramento";
            button5.UseVisualStyleBackColor = true;
            // 
            // BtnVmForm
            // 
            BtnVmForm.Dock = DockStyle.Left;
            BtnVmForm.FlatStyle = FlatStyle.Flat;
            BtnVmForm.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnVmForm.ForeColor = Color.Lavender;
            BtnVmForm.Location = new Point(0, 0);
            BtnVmForm.Margin = new Padding(3, 2, 3, 2);
            BtnVmForm.Name = "BtnVmForm";
            BtnVmForm.Size = new Size(149, 60);
            BtnVmForm.TabIndex = 5;
            BtnVmForm.Text = "Máquinas\r\nVirtuais\r\n";
            BtnVmForm.UseVisualStyleBackColor = true;
            BtnVmForm.Click += BtnVmForm_Click;
            // 
            // DeviceImages
            // 
            DeviceImages.ColorDepth = ColorDepth.Depth32Bit;
            DeviceImages.ImageStream = (ImageListStreamer)resources.GetObject("DeviceImages.ImageStream");
            DeviceImages.Tag = "1";
            DeviceImages.TransparentColor = Color.Transparent;
            DeviceImages.Images.SetKeyName(0, "Ied.png");
            // 
            // BtnResize
            // 
            BtnResize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnResize.FlatAppearance.BorderSize = 0;
            BtnResize.FlatStyle = FlatStyle.Flat;
            BtnResize.Image = (Image)resources.GetObject("BtnResize.Image");
            BtnResize.Location = new Point(1177, 8);
            BtnResize.Name = "BtnResize";
            BtnResize.Size = new Size(46, 41);
            BtnResize.TabIndex = 12;
            BtnResize.UseVisualStyleBackColor = true;
            BtnResize.Click += btnMaximize_Click;
            // 
            // BtnMinimize
            // 
            BtnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnMinimize.FlatAppearance.BorderSize = 0;
            BtnMinimize.FlatStyle = FlatStyle.Flat;
            BtnMinimize.Image = (Image)resources.GetObject("BtnMinimize.Image");
            BtnMinimize.Location = new Point(1125, 8);
            BtnMinimize.Name = "BtnMinimize";
            BtnMinimize.Size = new Size(46, 41);
            BtnMinimize.TabIndex = 11;
            BtnMinimize.UseVisualStyleBackColor = true;
            BtnMinimize.Click += btnMinimize_Click;
            // 
            // BtnExit
            // 
            BtnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnExit.FlatAppearance.BorderSize = 0;
            BtnExit.FlatStyle = FlatStyle.Flat;
            BtnExit.Image = (Image)resources.GetObject("BtnExit.Image");
            BtnExit.Location = new Point(1229, 8);
            BtnExit.Name = "BtnExit";
            BtnExit.Size = new Size(46, 41);
            BtnExit.TabIndex = 10;
            BtnExit.UseVisualStyleBackColor = true;
            BtnExit.Click += btnClose_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1280, 720);
            Controls.Add(panel1);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Server";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            panel1.ResumeLayout(false);
            PnTitle.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button BtnDebug;
        private Panel PnChields;
        private Panel PnIeds;
        private Panel panel4;
        private Panel PnTitle;
        private Button BtnConnect;
        private Panel panel8;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox TbIaMod;
        private TextBox TbIbMod;
        private TextBox TbIcMod;
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panel7;
        private TableLayoutPanel TlpIeds;
        private ImageList DeviceImages;
        private Button BtnDebug2;
        private Panel panel5;
        private Label label5;
        private Button button1;
        private Label label4;
        private Panel panel6;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel9;
        private Button button3;
        private Panel panel10;
        private Button button2;
        private Button button5;
        private Button BtnVmForm;
        private Button BtnMMS;
        private Button BtnResize;
        private Button BtnMinimize;
        private Button BtnExit;
    }
}