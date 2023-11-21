namespace Server
{
    partial class DeviceDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceDetailForm));
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            TbIp = new TextBox();
            TbPort = new TextBox();
            TbPasswd = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            panel1 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel6 = new Panel();
            BtnConfig = new Button();
            panel5 = new Panel();
            BtnEdit = new Button();
            panel4 = new Panel();
            BtnStartShut = new Button();
            panel3 = new Panel();
            BtnReboot = new Button();
            panel7 = new Panel();
            label5 = new Label();
            panel2 = new Panel();
            tableLayoutPanel3 = new TableLayoutPanel();
            label6 = new Label();
            LbDeviceType = new Label();
            LbVm = new Label();
            LbIed = new Label();
            panel8 = new Panel();
            BtnMinimize = new Button();
            BtnExit = new Button();
            imageList1 = new ImageList(components);
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel6.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel8.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(226, 40);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackgroundImageLayout = ImageLayout.Stretch;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 439F));
            tableLayoutPanel1.Controls.Add(TbIp, 1, 1);
            tableLayoutPanel1.Controls.Add(TbPort, 1, 2);
            tableLayoutPanel1.Controls.Add(TbPasswd, 1, 3);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 248);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(529, 115);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // TbIp
            // 
            TbIp.BackColor = Color.FromArgb(31, 45, 56);
            TbIp.BorderStyle = BorderStyle.None;
            TbIp.Dock = DockStyle.Fill;
            TbIp.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbIp.ForeColor = Color.Lavender;
            TbIp.Location = new Point(93, 3);
            TbIp.Name = "TbIp";
            TbIp.Size = new Size(433, 32);
            TbIp.TabIndex = 7;
            TbIp.TextAlign = HorizontalAlignment.Center;
            // 
            // TbPort
            // 
            TbPort.BackColor = Color.FromArgb(31, 45, 56);
            TbPort.BorderStyle = BorderStyle.None;
            TbPort.Dock = DockStyle.Fill;
            TbPort.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbPort.ForeColor = Color.Lavender;
            TbPort.Location = new Point(93, 41);
            TbPort.Name = "TbPort";
            TbPort.Size = new Size(433, 32);
            TbPort.TabIndex = 10;
            TbPort.TextAlign = HorizontalAlignment.Center;
            // 
            // TbPasswd
            // 
            TbPasswd.BackColor = Color.FromArgb(31, 45, 56);
            TbPasswd.BorderStyle = BorderStyle.None;
            TbPasswd.Dock = DockStyle.Fill;
            TbPasswd.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbPasswd.ForeColor = Color.Lavender;
            TbPasswd.Location = new Point(93, 79);
            TbPasswd.Name = "TbPasswd";
            TbPasswd.Size = new Size(433, 32);
            TbPasswd.TabIndex = 13;
            TbPasswd.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Lavender;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(84, 38);
            label2.TabIndex = 3;
            label2.Text = "IP";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Lavender;
            label3.Location = new Point(3, 38);
            label3.Name = "label3";
            label3.Size = new Size(84, 38);
            label3.TabIndex = 15;
            label3.Text = "Porta";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.Lavender;
            label4.Location = new Point(3, 76);
            label4.Name = "label4";
            label4.Size = new Size(84, 39);
            label4.TabIndex = 16;
            label4.Text = "Senha";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel2);
            panel1.Controls.Add(panel7);
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(panel8);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(529, 573);
            panel1.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(panel6, 1, 1);
            tableLayoutPanel2.Controls.Add(panel5, 0, 1);
            tableLayoutPanel2.Controls.Add(panel4, 0, 0);
            tableLayoutPanel2.Controls.Add(panel3, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(0, 392);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(529, 147);
            tableLayoutPanel2.TabIndex = 10;
            // 
            // panel6
            // 
            panel6.Controls.Add(BtnConfig);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(267, 76);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(10, 5, 10, 5);
            panel6.Size = new Size(259, 68);
            panel6.TabIndex = 15;
            // 
            // BtnConfig
            // 
            BtnConfig.BackgroundImageLayout = ImageLayout.None;
            BtnConfig.Dock = DockStyle.Fill;
            BtnConfig.FlatStyle = FlatStyle.Flat;
            BtnConfig.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            BtnConfig.Location = new Point(10, 5);
            BtnConfig.Name = "BtnConfig";
            BtnConfig.Size = new Size(239, 58);
            BtnConfig.TabIndex = 11;
            BtnConfig.Text = "Parametrizar";
            BtnConfig.UseVisualStyleBackColor = true;
            BtnConfig.Click += BtnConfig_Click;
            // 
            // panel5
            // 
            panel5.Controls.Add(BtnEdit);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(3, 76);
            panel5.Name = "panel5";
            panel5.Padding = new Padding(10, 5, 10, 5);
            panel5.Size = new Size(258, 68);
            panel5.TabIndex = 14;
            // 
            // BtnEdit
            // 
            BtnEdit.BackgroundImageLayout = ImageLayout.None;
            BtnEdit.Dock = DockStyle.Fill;
            BtnEdit.FlatStyle = FlatStyle.Flat;
            BtnEdit.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            BtnEdit.Location = new Point(10, 5);
            BtnEdit.Name = "BtnEdit";
            BtnEdit.Size = new Size(238, 58);
            BtnEdit.TabIndex = 11;
            BtnEdit.Text = "Editar VM";
            BtnEdit.UseVisualStyleBackColor = true;
            BtnEdit.Click += BtnEdit_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(BtnStartShut);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(10, 5, 10, 5);
            panel4.Size = new Size(258, 67);
            panel4.TabIndex = 13;
            // 
            // BtnStartShut
            // 
            BtnStartShut.BackgroundImageLayout = ImageLayout.None;
            BtnStartShut.Dock = DockStyle.Fill;
            BtnStartShut.FlatStyle = FlatStyle.Flat;
            BtnStartShut.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            BtnStartShut.Location = new Point(10, 5);
            BtnStartShut.Name = "BtnStartShut";
            BtnStartShut.Size = new Size(238, 57);
            BtnStartShut.TabIndex = 11;
            BtnStartShut.Text = "Desligar";
            BtnStartShut.UseVisualStyleBackColor = true;
            BtnStartShut.Click += BtnStartShut_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(BtnReboot);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(267, 3);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(10, 5, 10, 5);
            panel3.Size = new Size(259, 67);
            panel3.TabIndex = 12;
            // 
            // BtnReboot
            // 
            BtnReboot.BackgroundImageLayout = ImageLayout.None;
            BtnReboot.Dock = DockStyle.Fill;
            BtnReboot.FlatStyle = FlatStyle.Flat;
            BtnReboot.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            BtnReboot.Location = new Point(10, 5);
            BtnReboot.Name = "BtnReboot";
            BtnReboot.Size = new Size(239, 57);
            BtnReboot.TabIndex = 11;
            BtnReboot.Text = "Reiniciar";
            BtnReboot.UseVisualStyleBackColor = true;
            BtnReboot.Click += BtnReboot_Click;
            // 
            // panel7
            // 
            panel7.Dock = DockStyle.Top;
            panel7.Location = new Point(0, 363);
            panel7.Name = "panel7";
            panel7.Size = new Size(529, 29);
            panel7.TabIndex = 11;
            // 
            // label5
            // 
            label5.Dock = DockStyle.Top;
            label5.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(0, 188);
            label5.Name = "label5";
            label5.Size = new Size(529, 60);
            label5.TabIndex = 0;
            label5.Text = "IED";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(tableLayoutPanel3);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 60);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(40, 15, 40, 0);
            panel2.Size = new Size(529, 128);
            panel2.TabIndex = 8;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.BackgroundImageLayout = ImageLayout.Stretch;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 143F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 306F));
            tableLayoutPanel3.Controls.Add(label6, 0, 1);
            tableLayoutPanel3.Controls.Add(LbDeviceType, 0, 2);
            tableLayoutPanel3.Controls.Add(LbVm, 1, 1);
            tableLayoutPanel3.Controls.Add(LbIed, 1, 2);
            tableLayoutPanel3.Dock = DockStyle.Top;
            tableLayoutPanel3.Location = new Point(40, 15);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(449, 71);
            tableLayoutPanel3.TabIndex = 8;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = Color.Lavender;
            label6.Location = new Point(3, 0);
            label6.Name = "label6";
            label6.Size = new Size(137, 85);
            label6.TabIndex = 3;
            label6.Text = "VM Status";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LbDeviceType
            // 
            LbDeviceType.AutoSize = true;
            LbDeviceType.Dock = DockStyle.Fill;
            LbDeviceType.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            LbDeviceType.ForeColor = Color.Lavender;
            LbDeviceType.Location = new Point(3, 85);
            LbDeviceType.Name = "LbDeviceType";
            LbDeviceType.Size = new Size(137, 43);
            LbDeviceType.TabIndex = 15;
            LbDeviceType.Text = "Ied Status";
            LbDeviceType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LbVm
            // 
            LbVm.Dock = DockStyle.Fill;
            LbVm.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            LbVm.Image = Properties.Resources.stoped;
            LbVm.ImageAlign = ContentAlignment.MiddleLeft;
            LbVm.Location = new Point(146, 0);
            LbVm.Name = "LbVm";
            LbVm.Size = new Size(300, 85);
            LbVm.TabIndex = 16;
            LbVm.Text = "Stoped";
            LbVm.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LbIed
            // 
            LbIed.Dock = DockStyle.Fill;
            LbIed.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            LbIed.Image = Properties.Resources.stoped;
            LbIed.ImageAlign = ContentAlignment.MiddleLeft;
            LbIed.Location = new Point(146, 85);
            LbIed.Name = "LbIed";
            LbIed.Size = new Size(300, 43);
            LbIed.TabIndex = 17;
            LbIed.Text = "Stoped";
            LbIed.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel8
            // 
            panel8.Controls.Add(BtnMinimize);
            panel8.Controls.Add(BtnExit);
            panel8.Dock = DockStyle.Top;
            panel8.Location = new Point(0, 0);
            panel8.Name = "panel8";
            panel8.Size = new Size(529, 60);
            panel8.TabIndex = 12;
            panel8.MouseDown += panelTitleBar_MouseDown;
            // 
            // BtnMinimize
            // 
            BtnMinimize.Dock = DockStyle.Right;
            BtnMinimize.FlatAppearance.BorderSize = 0;
            BtnMinimize.FlatStyle = FlatStyle.Flat;
            BtnMinimize.Image = (Image)resources.GetObject("BtnMinimize.Image");
            BtnMinimize.Location = new Point(423, 0);
            BtnMinimize.Margin = new Padding(3, 4, 3, 4);
            BtnMinimize.Name = "BtnMinimize";
            BtnMinimize.Size = new Size(53, 60);
            BtnMinimize.TabIndex = 13;
            BtnMinimize.UseVisualStyleBackColor = true;
            BtnMinimize.Click += BtnMinimize_Click;
            // 
            // BtnExit
            // 
            BtnExit.Dock = DockStyle.Right;
            BtnExit.FlatAppearance.BorderSize = 0;
            BtnExit.FlatStyle = FlatStyle.Flat;
            BtnExit.Image = (Image)resources.GetObject("BtnExit.Image");
            BtnExit.Location = new Point(476, 0);
            BtnExit.Margin = new Padding(3, 4, 3, 4);
            BtnExit.Name = "BtnExit";
            BtnExit.Size = new Size(53, 60);
            BtnExit.TabIndex = 12;
            BtnExit.UseVisualStyleBackColor = true;
            BtnExit.Click += BtnExit_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "buttonGreen.png");
            imageList1.Images.SetKeyName(1, "buttonRed.png");
            imageList1.Images.SetKeyName(2, "buttonYellow.png");
            // 
            // DeviceDetailForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(1, 22, 39);
            ClientSize = new Size(529, 573);
            Controls.Add(panel1);
            Controls.Add(label1);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Name = "DeviceDetailForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DeviceDetailForm";
            Load += DeviceDetailForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            panel8.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox TbIp;
        private TextBox TbPort;
        private TextBox TbPasswd;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel6;
        private Button BtnConfig;
        private Panel panel5;
        private Button BtnEdit;
        private Panel panel4;
        private Button BtnStartShut;
        private Panel panel3;
        private Button BtnReboot;
        private Panel panel7;
        private Label label5;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label6;
        private Label LbDeviceType;
        private Label LbVm;
        private ImageList imageList1;
        private Label LbIed;
        private Panel panel8;
        private Button BtnExit;
        private Button BtnMinimize;
    }
}