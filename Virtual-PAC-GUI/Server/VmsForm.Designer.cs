namespace Server
{
    partial class VmsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VmsForm));
            panel1 = new Panel();
            panel3 = new Panel();
            PnIeds = new Panel();
            TlpIeds = new TableLayoutPanel();
            panel2 = new Panel();
            BtnDelVm = new Button();
            BtnCreateVm = new Button();
            panel4 = new Panel();
            BtnConnect = new Button();
            panel8 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            TbIp = new TextBox();
            TbPort = new TextBox();
            TbPasswd = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            panel7 = new Panel();
            LbConnection = new Label();
            ImgListConButton = new ImageList(components);
            DeviceImages = new ImageList(components);
            TimerConnection = new System.Windows.Forms.Timer(components);
            DeviceImages2 = new ImageList(components);
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            PnIeds.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1463, 880);
            panel1.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Controls.Add(PnIeds);
            panel3.Controls.Add(panel4);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1463, 880);
            panel3.TabIndex = 2;
            // 
            // PnIeds
            // 
            PnIeds.AutoScroll = true;
            PnIeds.Controls.Add(TlpIeds);
            PnIeds.Controls.Add(panel2);
            PnIeds.Dock = DockStyle.Fill;
            PnIeds.Location = new Point(304, 0);
            PnIeds.Name = "PnIeds";
            PnIeds.Padding = new Padding(21, 11, 21, 0);
            PnIeds.Size = new Size(1159, 880);
            PnIeds.TabIndex = 2;
            // 
            // TlpIeds
            // 
            TlpIeds.BackColor = Color.FromArgb(31, 45, 56);
            TlpIeds.ColumnCount = 5;
            TlpIeds.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TlpIeds.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TlpIeds.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TlpIeds.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TlpIeds.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TlpIeds.Dock = DockStyle.Top;
            TlpIeds.Location = new Point(21, 126);
            TlpIeds.Name = "TlpIeds";
            TlpIeds.RowCount = 2;
            TlpIeds.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            TlpIeds.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            TlpIeds.Size = new Size(1117, 480);
            TlpIeds.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(BtnDelVm);
            panel2.Controls.Add(BtnCreateVm);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(21, 11);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1117, 115);
            panel2.TabIndex = 4;
            // 
            // BtnDelVm
            // 
            BtnDelVm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BtnDelVm.Enabled = false;
            BtnDelVm.FlatAppearance.BorderColor = Color.White;
            BtnDelVm.FlatAppearance.BorderSize = 0;
            BtnDelVm.FlatStyle = FlatStyle.Popup;
            BtnDelVm.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnDelVm.Location = new Point(581, 41);
            BtnDelVm.Name = "BtnDelVm";
            BtnDelVm.Size = new Size(234, 53);
            BtnDelVm.TabIndex = 8;
            BtnDelVm.Text = "Remover VM";
            BtnDelVm.UseVisualStyleBackColor = true;
            BtnDelVm.Click += BtnDelVm_Click;
            // 
            // BtnCreateVm
            // 
            BtnCreateVm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BtnCreateVm.Enabled = false;
            BtnCreateVm.FlatAppearance.BorderColor = Color.White;
            BtnCreateVm.FlatAppearance.BorderSize = 0;
            BtnCreateVm.FlatStyle = FlatStyle.Popup;
            BtnCreateVm.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnCreateVm.Location = new Point(254, 41);
            BtnCreateVm.Name = "BtnCreateVm";
            BtnCreateVm.Size = new Size(234, 53);
            BtnCreateVm.TabIndex = 7;
            BtnCreateVm.Text = "Adicionar VM";
            BtnCreateVm.UseVisualStyleBackColor = true;
            BtnCreateVm.Click += BtnCreateVm_Click;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(1, 22, 39);
            panel4.Controls.Add(BtnConnect);
            panel4.Controls.Add(panel8);
            panel4.Controls.Add(tableLayoutPanel1);
            panel4.Controls.Add(panel7);
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(10, 11, 10, 11);
            panel4.Size = new Size(304, 880);
            panel4.TabIndex = 1;
            // 
            // BtnConnect
            // 
            BtnConnect.BackgroundImageLayout = ImageLayout.None;
            BtnConnect.Dock = DockStyle.Top;
            BtnConnect.FlatStyle = FlatStyle.Flat;
            BtnConnect.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            BtnConnect.Location = new Point(10, 241);
            BtnConnect.Name = "BtnConnect";
            BtnConnect.Size = new Size(284, 59);
            BtnConnect.TabIndex = 8;
            BtnConnect.Text = "Conectar";
            BtnConnect.UseVisualStyleBackColor = true;
            BtnConnect.Click += BtnConnect_Click;
            // 
            // panel8
            // 
            panel8.Dock = DockStyle.Top;
            panel8.Location = new Point(10, 205);
            panel8.Name = "panel8";
            panel8.Size = new Size(284, 36);
            panel8.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackgroundImageLayout = ImageLayout.Stretch;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 229F));
            tableLayoutPanel1.Controls.Add(TbIp, 1, 1);
            tableLayoutPanel1.Controls.Add(TbPort, 1, 2);
            tableLayoutPanel1.Controls.Add(TbPasswd, 1, 3);
            tableLayoutPanel1.Controls.Add(label1, 0, 1);
            tableLayoutPanel1.Controls.Add(label2, 0, 2);
            tableLayoutPanel1.Controls.Add(label3, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(10, 90);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(284, 115);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // TbIp
            // 
            TbIp.BackColor = Color.FromArgb(31, 45, 56);
            TbIp.BorderStyle = BorderStyle.None;
            TbIp.Dock = DockStyle.Left;
            TbIp.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbIp.ForeColor = Color.Lavender;
            TbIp.Location = new Point(93, 3);
            TbIp.Name = "TbIp";
            TbIp.Size = new Size(187, 32);
            TbIp.TabIndex = 7;
            TbIp.TextAlign = HorizontalAlignment.Center;
            // 
            // TbPort
            // 
            TbPort.BackColor = Color.FromArgb(31, 45, 56);
            TbPort.BorderStyle = BorderStyle.None;
            TbPort.Dock = DockStyle.Left;
            TbPort.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbPort.ForeColor = Color.Lavender;
            TbPort.Location = new Point(93, 41);
            TbPort.Name = "TbPort";
            TbPort.Size = new Size(187, 32);
            TbPort.TabIndex = 10;
            TbPort.TextAlign = HorizontalAlignment.Center;
            // 
            // TbPasswd
            // 
            TbPasswd.BackColor = Color.FromArgb(31, 45, 56);
            TbPasswd.BorderStyle = BorderStyle.None;
            TbPasswd.Dock = DockStyle.Left;
            TbPasswd.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbPasswd.ForeColor = Color.Lavender;
            TbPasswd.Location = new Point(93, 79);
            TbPasswd.Name = "TbPasswd";
            TbPasswd.Size = new Size(187, 32);
            TbPasswd.TabIndex = 13;
            TbPasswd.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Lavender;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(84, 38);
            label1.TabIndex = 3;
            label1.Text = "IP";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Lavender;
            label2.Location = new Point(3, 38);
            label2.Name = "label2";
            label2.Size = new Size(84, 38);
            label2.TabIndex = 15;
            label2.Text = "Porta";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Lavender;
            label3.Location = new Point(3, 76);
            label3.Name = "label3";
            label3.Size = new Size(84, 39);
            label3.TabIndex = 16;
            label3.Text = "Senha";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panel7
            // 
            panel7.Controls.Add(LbConnection);
            panel7.Dock = DockStyle.Top;
            panel7.Location = new Point(10, 11);
            panel7.Name = "panel7";
            panel7.Padding = new Padding(40, 0, 40, 0);
            panel7.Size = new Size(284, 79);
            panel7.TabIndex = 7;
            // 
            // LbConnection
            // 
            LbConnection.Dock = DockStyle.Fill;
            LbConnection.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LbConnection.Image = Properties.Resources.stoped;
            LbConnection.ImageAlign = ContentAlignment.MiddleLeft;
            LbConnection.Location = new Point(40, 0);
            LbConnection.Name = "LbConnection";
            LbConnection.Size = new Size(204, 79);
            LbConnection.TabIndex = 0;
            LbConnection.Text = "Desconectado";
            LbConnection.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ImgListConButton
            // 
            ImgListConButton.ColorDepth = ColorDepth.Depth8Bit;
            ImgListConButton.ImageStream = (ImageListStreamer)resources.GetObject("ImgListConButton.ImageStream");
            ImgListConButton.TransparentColor = Color.Transparent;
            ImgListConButton.Images.SetKeyName(0, "buttonGreen.png");
            ImgListConButton.Images.SetKeyName(1, "buttonYellow.png");
            ImgListConButton.Images.SetKeyName(2, "buttonRed.png");
            // 
            // DeviceImages
            // 
            DeviceImages.ColorDepth = ColorDepth.Depth32Bit;
            DeviceImages.ImageStream = (ImageListStreamer)resources.GetObject("DeviceImages.ImageStream");
            DeviceImages.Tag = "1";
            DeviceImages.TransparentColor = Color.Transparent;
            DeviceImages.Images.SetKeyName(0, "Ied.png");
            DeviceImages.Images.SetKeyName(1, "Mu.png");
            // 
            // TimerConnection
            // 
            TimerConnection.Interval = 1000;
            TimerConnection.Tick += TimerConnection_Tick;
            // 
            // DeviceImages2
            // 
            DeviceImages2.ColorDepth = ColorDepth.Depth32Bit;
            DeviceImages2.ImageStream = (ImageListStreamer)resources.GetObject("DeviceImages2.ImageStream");
            DeviceImages2.Tag = "1";
            DeviceImages2.TransparentColor = Color.Transparent;
            DeviceImages2.Images.SetKeyName(0, "Ied.png");
            // 
            // VmsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1463, 880);
            Controls.Add(panel1);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Name = "VmsForm";
            Text = "General";
            Load += General_Load;
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            PnIeds.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel7.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel3;
        private Panel PnIeds;
        private TableLayoutPanel TlpIeds;
        private Panel panel4;
        private Button BtnConnect;
        private Panel panel8;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox TbIp;
        private TextBox TbPort;
        private TextBox TbPasswd;
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panel7;
        private ImageList DeviceImages;
        private System.Windows.Forms.Timer TimerConnection;
        private Label LbConnection;
        private ImageList ImgListConButton;
        private ImageList DeviceImages2;
        private Panel panel2;
        private Button BtnDelVm;
        private Button BtnCreateVm;
    }
}