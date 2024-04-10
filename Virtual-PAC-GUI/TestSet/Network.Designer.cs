namespace TestSet
{
    partial class Network
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
            panel1 = new Panel();
            BtnSendConfig = new Button();
            groupBox1 = new GroupBox();
            label16 = new Label();
            TbSvSmpRate = new TextBox();
            TbSvVLanPriority = new TextBox();
            button2 = new Button();
            TbSvMacDest = new TextBox();
            label15 = new Label();
            label7 = new Label();
            label6 = new Label();
            TbSvRev = new TextBox();
            label5 = new Label();
            label3 = new Label();
            TbSvVLanID = new TextBox();
            TbSVNoAsdu = new TextBox();
            label4 = new Label();
            TbSvAppID = new TextBox();
            label2 = new Label();
            TbSvID = new TextBox();
            label1 = new Label();
            TbSvFreq = new TextBox();
            BtnLoadConfig = new Button();
            GOOSE_GroupBox = new GroupBox();
            BtnImportScl = new Button();
            TbGoMacSrc = new TextBox();
            label8 = new Label();
            label9 = new Label();
            TbGoRev = new TextBox();
            label11 = new Label();
            TbGoAppId = new TextBox();
            label12 = new Label();
            TbGoVLan = new TextBox();
            label13 = new Label();
            TbGoID = new TextBox();
            label14 = new Label();
            TbGoControlRef = new TextBox();
            label10 = new Label();
            openFileDialog1 = new OpenFileDialog();
            PnSv = new Panel();
            panel5 = new Panel();
            TPnSV = new TableLayoutPanel();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            GOOSE_GroupBox.SuspendLayout();
            PnSv.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(40, 58, 73);
            panel1.Controls.Add(BtnSendConfig);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(BtnLoadConfig);
            panel1.Controls.Add(GOOSE_GroupBox);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 91);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(18, 15, 0, 0);
            panel1.Size = new Size(1072, 523);
            panel1.TabIndex = 0;
            // 
            // BtnSendConfig
            // 
            BtnSendConfig.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnSendConfig.FlatAppearance.BorderColor = Color.White;
            BtnSendConfig.FlatAppearance.BorderSize = 0;
            BtnSendConfig.FlatStyle = FlatStyle.Popup;
            BtnSendConfig.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSendConfig.Location = new Point(653, 479);
            BtnSendConfig.Margin = new Padding(3, 2, 3, 2);
            BtnSendConfig.Name = "BtnSendConfig";
            BtnSendConfig.Size = new Size(202, 33);
            BtnSendConfig.TabIndex = 9;
            BtnSendConfig.Text = "Enviar para vMU";
            BtnSendConfig.UseVisualStyleBackColor = true;
            BtnSendConfig.Click += BtnSendConfig_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label16);
            groupBox1.Controls.Add(TbSvSmpRate);
            groupBox1.Controls.Add(TbSvVLanPriority);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(TbSvMacDest);
            groupBox1.Controls.Add(label15);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(TbSvRev);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(TbSvVLanID);
            groupBox1.Controls.Add(TbSVNoAsdu);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(TbSvAppID);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(TbSvID);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(TbSvFreq);
            groupBox1.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.GhostWhite;
            groupBox1.Location = new Point(97, 17);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(426, 448);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Sampled Value";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label16.ForeColor = Color.Lavender;
            label16.Location = new Point(27, 241);
            label16.Name = "label16";
            label16.Size = new Size(127, 25);
            label16.TabIndex = 18;
            label16.Text = "Sampled Rate";
            // 
            // TbSvSmpRate
            // 
            TbSvSmpRate.BackColor = Color.FromArgb(31, 45, 56);
            TbSvSmpRate.BorderStyle = BorderStyle.None;
            TbSvSmpRate.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSvSmpRate.ForeColor = Color.Lavender;
            TbSvSmpRate.Location = new Point(157, 241);
            TbSvSmpRate.Margin = new Padding(3, 2, 3, 2);
            TbSvSmpRate.Name = "TbSvSmpRate";
            TbSvSmpRate.Size = new Size(218, 25);
            TbSvSmpRate.TabIndex = 17;
            TbSvSmpRate.TextAlign = HorizontalAlignment.Center;
            // 
            // TbSvVLanPriority
            // 
            TbSvVLanPriority.BackColor = Color.FromArgb(31, 45, 56);
            TbSvVLanPriority.BorderStyle = BorderStyle.None;
            TbSvVLanPriority.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSvVLanPriority.ForeColor = Color.Lavender;
            TbSvVLanPriority.Location = new Point(157, 361);
            TbSvVLanPriority.Margin = new Padding(3, 2, 3, 2);
            TbSvVLanPriority.Name = "TbSvVLanPriority";
            TbSvVLanPriority.Size = new Size(223, 25);
            TbSvVLanPriority.TabIndex = 16;
            TbSvVLanPriority.TextAlign = HorizontalAlignment.Center;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button2.FlatAppearance.BorderColor = Color.White;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(125, 407);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(147, 33);
            button2.TabIndex = 8;
            button2.Text = "Exportar SCL";
            button2.UseVisualStyleBackColor = true;
            // 
            // TbSvMacDest
            // 
            TbSvMacDest.BackColor = Color.FromArgb(31, 45, 56);
            TbSvMacDest.BorderStyle = BorderStyle.None;
            TbSvMacDest.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSvMacDest.ForeColor = Color.Lavender;
            TbSvMacDest.Location = new Point(146, 81);
            TbSvMacDest.Margin = new Padding(3, 2, 3, 2);
            TbSvMacDest.Name = "TbSvMacDest";
            TbSvMacDest.Size = new Size(228, 25);
            TbSvMacDest.TabIndex = 14;
            TbSvMacDest.TextAlign = HorizontalAlignment.Center;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label15.ForeColor = Color.Lavender;
            label15.Location = new Point(34, 361);
            label15.Name = "label15";
            label15.Size = new Size(117, 25);
            label15.TabIndex = 15;
            label15.Text = "vLan Priority";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = Color.Lavender;
            label7.Location = new Point(27, 121);
            label7.Name = "label7";
            label7.Size = new Size(69, 25);
            label7.TabIndex = 13;
            label7.Text = "App ID";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = Color.Lavender;
            label6.Location = new Point(26, 201);
            label6.Name = "label6";
            label6.Size = new Size(105, 25);
            label6.TabIndex = 11;
            label6.Text = "Frequência";
            // 
            // TbSvRev
            // 
            TbSvRev.BackColor = Color.FromArgb(31, 45, 56);
            TbSvRev.BorderStyle = BorderStyle.None;
            TbSvRev.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSvRev.ForeColor = Color.Lavender;
            TbSvRev.Location = new Point(102, 161);
            TbSvRev.Margin = new Padding(3, 2, 3, 2);
            TbSvRev.Name = "TbSvRev";
            TbSvRev.Size = new Size(273, 25);
            TbSvRev.TabIndex = 12;
            TbSvRev.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.Lavender;
            label5.Location = new Point(33, 281);
            label5.Name = "label5";
            label5.Size = new Size(79, 25);
            label5.TabIndex = 9;
            label5.Text = "NoAsdu";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Lavender;
            label3.Location = new Point(32, 321);
            label3.Name = "label3";
            label3.Size = new Size(126, 25);
            label3.TabIndex = 5;
            label3.Text = "Virtual Lan ID";
            // 
            // TbSvVLanID
            // 
            TbSvVLanID.BackColor = Color.FromArgb(31, 45, 56);
            TbSvVLanID.BorderStyle = BorderStyle.None;
            TbSvVLanID.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSvVLanID.ForeColor = Color.Lavender;
            TbSvVLanID.Location = new Point(157, 321);
            TbSvVLanID.Margin = new Padding(3, 2, 3, 2);
            TbSvVLanID.Name = "TbSvVLanID";
            TbSvVLanID.Size = new Size(222, 25);
            TbSvVLanID.TabIndex = 4;
            TbSvVLanID.TextAlign = HorizontalAlignment.Center;
            // 
            // TbSVNoAsdu
            // 
            TbSVNoAsdu.BackColor = Color.FromArgb(31, 45, 56);
            TbSVNoAsdu.BorderStyle = BorderStyle.None;
            TbSVNoAsdu.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSVNoAsdu.ForeColor = Color.Lavender;
            TbSVNoAsdu.Location = new Point(118, 281);
            TbSVNoAsdu.Margin = new Padding(3, 2, 3, 2);
            TbSVNoAsdu.Name = "TbSVNoAsdu";
            TbSVNoAsdu.Size = new Size(263, 25);
            TbSVNoAsdu.TabIndex = 8;
            TbSVNoAsdu.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.Lavender;
            label4.Location = new Point(28, 161);
            label4.Name = "label4";
            label4.Size = new Size(75, 25);
            label4.TabIndex = 7;
            label4.Text = "Revisão";
            // 
            // TbSvAppID
            // 
            TbSvAppID.BackColor = Color.FromArgb(31, 45, 56);
            TbSvAppID.BorderStyle = BorderStyle.None;
            TbSvAppID.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSvAppID.ForeColor = Color.Lavender;
            TbSvAppID.Location = new Point(102, 121);
            TbSvAppID.Margin = new Padding(3, 2, 3, 2);
            TbSvAppID.Name = "TbSvAppID";
            TbSvAppID.Size = new Size(272, 25);
            TbSvAppID.TabIndex = 6;
            TbSvAppID.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Lavender;
            label2.Location = new Point(26, 41);
            label2.Name = "label2";
            label2.Size = new Size(160, 25);
            label2.TabIndex = 3;
            label2.Text = "Sampled Value ID";
            // 
            // TbSvID
            // 
            TbSvID.BackColor = Color.FromArgb(31, 45, 56);
            TbSvID.BorderStyle = BorderStyle.None;
            TbSvID.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSvID.ForeColor = Color.Lavender;
            TbSvID.Location = new Point(192, 41);
            TbSvID.Margin = new Padding(3, 2, 3, 2);
            TbSvID.Name = "TbSvID";
            TbSvID.Size = new Size(182, 25);
            TbSvID.TabIndex = 2;
            TbSvID.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Lavender;
            label1.Location = new Point(26, 81);
            label1.Name = "label1";
            label1.Size = new Size(117, 25);
            label1.TabIndex = 1;
            label1.Text = "Mac Destino";
            // 
            // TbSvFreq
            // 
            TbSvFreq.BackColor = Color.FromArgb(31, 45, 56);
            TbSvFreq.BorderStyle = BorderStyle.None;
            TbSvFreq.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSvFreq.ForeColor = Color.Lavender;
            TbSvFreq.Location = new Point(137, 201);
            TbSvFreq.Margin = new Padding(3, 2, 3, 2);
            TbSvFreq.Name = "TbSvFreq";
            TbSvFreq.Size = new Size(237, 25);
            TbSvFreq.TabIndex = 0;
            TbSvFreq.TextAlign = HorizontalAlignment.Center;
            // 
            // BtnLoadConfig
            // 
            BtnLoadConfig.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnLoadConfig.FlatAppearance.BorderColor = Color.White;
            BtnLoadConfig.FlatAppearance.BorderSize = 0;
            BtnLoadConfig.FlatStyle = FlatStyle.Popup;
            BtnLoadConfig.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnLoadConfig.Location = new Point(201, 479);
            BtnLoadConfig.Margin = new Padding(3, 2, 3, 2);
            BtnLoadConfig.Name = "BtnLoadConfig";
            BtnLoadConfig.Size = new Size(202, 33);
            BtnLoadConfig.TabIndex = 7;
            BtnLoadConfig.Text = "Carregar do vMU";
            BtnLoadConfig.UseVisualStyleBackColor = true;
            BtnLoadConfig.Click += BtnLoadConfig_Click;
            // 
            // GOOSE_GroupBox
            // 
            GOOSE_GroupBox.Controls.Add(BtnImportScl);
            GOOSE_GroupBox.Controls.Add(TbGoMacSrc);
            GOOSE_GroupBox.Controls.Add(label8);
            GOOSE_GroupBox.Controls.Add(label9);
            GOOSE_GroupBox.Controls.Add(TbGoRev);
            GOOSE_GroupBox.Controls.Add(label11);
            GOOSE_GroupBox.Controls.Add(TbGoAppId);
            GOOSE_GroupBox.Controls.Add(label12);
            GOOSE_GroupBox.Controls.Add(TbGoVLan);
            GOOSE_GroupBox.Controls.Add(label13);
            GOOSE_GroupBox.Controls.Add(TbGoID);
            GOOSE_GroupBox.Controls.Add(label14);
            GOOSE_GroupBox.Controls.Add(TbGoControlRef);
            GOOSE_GroupBox.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            GOOSE_GroupBox.ForeColor = Color.GhostWhite;
            GOOSE_GroupBox.Location = new Point(549, 17);
            GOOSE_GroupBox.Margin = new Padding(3, 2, 3, 2);
            GOOSE_GroupBox.Name = "GOOSE_GroupBox";
            GOOSE_GroupBox.Padding = new Padding(3, 2, 3, 2);
            GOOSE_GroupBox.Size = new Size(426, 448);
            GOOSE_GroupBox.TabIndex = 1;
            GOOSE_GroupBox.TabStop = false;
            GOOSE_GroupBox.Text = "GOOSE";
            // 
            // BtnImportScl
            // 
            BtnImportScl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BtnImportScl.FlatAppearance.BorderColor = Color.White;
            BtnImportScl.FlatAppearance.BorderSize = 0;
            BtnImportScl.FlatStyle = FlatStyle.Popup;
            BtnImportScl.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnImportScl.Location = new Point(139, 407);
            BtnImportScl.Margin = new Padding(3, 2, 3, 2);
            BtnImportScl.Name = "BtnImportScl";
            BtnImportScl.Size = new Size(147, 33);
            BtnImportScl.TabIndex = 6;
            BtnImportScl.Text = "Importar SCL";
            BtnImportScl.UseVisualStyleBackColor = true;
            BtnImportScl.Click += BtnImportScl_Click;
            // 
            // TbGoMacSrc
            // 
            TbGoMacSrc.BackColor = Color.FromArgb(31, 45, 56);
            TbGoMacSrc.BorderStyle = BorderStyle.None;
            TbGoMacSrc.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbGoMacSrc.ForeColor = Color.Lavender;
            TbGoMacSrc.Location = new Point(156, 214);
            TbGoMacSrc.Margin = new Padding(3, 2, 3, 2);
            TbGoMacSrc.Name = "TbGoMacSrc";
            TbGoMacSrc.Size = new Size(214, 25);
            TbGoMacSrc.TabIndex = 14;
            TbGoMacSrc.TextAlign = HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = Color.Lavender;
            label8.Location = new Point(23, 172);
            label8.Name = "label8";
            label8.Size = new Size(69, 25);
            label8.TabIndex = 13;
            label8.Text = "App ID";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = Color.Lavender;
            label9.Location = new Point(23, 46);
            label9.Name = "label9";
            label9.Size = new Size(101, 25);
            label9.TabIndex = 11;
            label9.Text = "ControlRef";
            // 
            // TbGoRev
            // 
            TbGoRev.BackColor = Color.FromArgb(31, 45, 56);
            TbGoRev.BorderStyle = BorderStyle.None;
            TbGoRev.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbGoRev.ForeColor = Color.Lavender;
            TbGoRev.Location = new Point(110, 256);
            TbGoRev.Margin = new Padding(3, 2, 3, 2);
            TbGoRev.Name = "TbGoRev";
            TbGoRev.Size = new Size(260, 25);
            TbGoRev.TabIndex = 12;
            TbGoRev.Text = " ";
            TbGoRev.TextAlign = HorizontalAlignment.Center;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = Color.Lavender;
            label11.Location = new Point(23, 256);
            label11.Name = "label11";
            label11.Size = new Size(75, 25);
            label11.TabIndex = 7;
            label11.Text = "Revisão";
            // 
            // TbGoAppId
            // 
            TbGoAppId.BackColor = Color.FromArgb(31, 45, 56);
            TbGoAppId.BorderStyle = BorderStyle.None;
            TbGoAppId.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbGoAppId.ForeColor = Color.Lavender;
            TbGoAppId.Location = new Point(104, 172);
            TbGoAppId.Margin = new Padding(3, 2, 3, 2);
            TbGoAppId.Name = "TbGoAppId";
            TbGoAppId.Size = new Size(266, 25);
            TbGoAppId.TabIndex = 6;
            TbGoAppId.TextAlign = HorizontalAlignment.Center;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label12.ForeColor = Color.Lavender;
            label12.Location = new Point(23, 130);
            label12.Name = "label12";
            label12.Size = new Size(103, 25);
            label12.TabIndex = 5;
            label12.Text = "Virtual Lan";
            // 
            // TbGoVLan
            // 
            TbGoVLan.BackColor = Color.FromArgb(31, 45, 56);
            TbGoVLan.BorderStyle = BorderStyle.None;
            TbGoVLan.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbGoVLan.ForeColor = Color.Lavender;
            TbGoVLan.Location = new Point(139, 130);
            TbGoVLan.Margin = new Padding(3, 2, 3, 2);
            TbGoVLan.Name = "TbGoVLan";
            TbGoVLan.Size = new Size(231, 25);
            TbGoVLan.TabIndex = 4;
            TbGoVLan.TextAlign = HorizontalAlignment.Center;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label13.ForeColor = Color.Lavender;
            label13.Location = new Point(23, 88);
            label13.Name = "label13";
            label13.Size = new Size(96, 25);
            label13.TabIndex = 3;
            label13.Text = "GOOSE ID";
            // 
            // TbGoID
            // 
            TbGoID.BackColor = Color.FromArgb(31, 45, 56);
            TbGoID.BorderStyle = BorderStyle.None;
            TbGoID.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbGoID.ForeColor = Color.Lavender;
            TbGoID.Location = new Point(134, 88);
            TbGoID.Margin = new Padding(3, 2, 3, 2);
            TbGoID.Name = "TbGoID";
            TbGoID.Size = new Size(236, 25);
            TbGoID.TabIndex = 2;
            TbGoID.TextAlign = HorizontalAlignment.Center;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label14.ForeColor = Color.Lavender;
            label14.Location = new Point(23, 214);
            label14.Name = "label14";
            label14.Size = new Size(116, 25);
            label14.TabIndex = 1;
            label14.Text = "Mac Origem";
            // 
            // TbGoControlRef
            // 
            TbGoControlRef.BackColor = Color.FromArgb(31, 45, 56);
            TbGoControlRef.BorderStyle = BorderStyle.None;
            TbGoControlRef.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbGoControlRef.ForeColor = Color.Lavender;
            TbGoControlRef.Location = new Point(139, 46);
            TbGoControlRef.Margin = new Padding(3, 2, 3, 2);
            TbGoControlRef.Name = "TbGoControlRef";
            TbGoControlRef.Size = new Size(231, 25);
            TbGoControlRef.TabIndex = 0;
            TbGoControlRef.TextAlign = HorizontalAlignment.Center;
            // 
            // label10
            // 
            label10.BackColor = Color.FromArgb(31, 45, 56);
            label10.Dock = DockStyle.Fill;
            label10.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(0, 0);
            label10.Name = "label10";
            label10.Size = new Size(102, 61);
            label10.TabIndex = 10;
            label10.Text = "Sampled\r\nValue\r\nChannel\r\n";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // PnSv
            // 
            PnSv.Controls.Add(panel5);
            PnSv.Controls.Add(TPnSV);
            PnSv.Dock = DockStyle.Top;
            PnSv.Location = new Point(0, 0);
            PnSv.Name = "PnSv";
            PnSv.Size = new Size(1072, 91);
            PnSv.TabIndex = 10;
            // 
            // panel5
            // 
            panel5.Controls.Add(label10);
            panel5.Location = new Point(97, 12);
            panel5.Margin = new Padding(20, 3, 20, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(102, 61);
            panel5.TabIndex = 11;
            // 
            // TPnSV
            // 
            TPnSV.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TPnSV.BackColor = Color.FromArgb(31, 45, 56);
            TPnSV.ColumnCount = 5;
            TPnSV.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TPnSV.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TPnSV.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TPnSV.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TPnSV.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TPnSV.Location = new Point(201, 12);
            TPnSV.Name = "TPnSV";
            TPnSV.RowCount = 2;
            TPnSV.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            TPnSV.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            TPnSV.Size = new Size(774, 61);
            TPnSV.TabIndex = 0;
            // 
            // Network
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1072, 614);
            Controls.Add(panel1);
            Controls.Add(PnSv);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Network";
            Text = "Form2";
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            GOOSE_GroupBox.ResumeLayout(false);
            GOOSE_GroupBox.PerformLayout();
            PnSv.ResumeLayout(false);
            panel5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel4;
        private GroupBox GOOSE_GroupBox;
        private TextBox TbGoMacSrc;
        private Label label8;
        private Label label9;
        private TextBox TbGoRev;
        private Label label11;
        private TextBox TbGoAppId;
        private Label label12;
        private TextBox TbGoVLan;
        private Label label13;
        private TextBox TbGoID;
        private Label label14;
        private TextBox TbGoControlRef;
        private Panel panel3;
        private Panel panel2;
        private GroupBox groupBox1;
        private TextBox TbSvMacDest;
        private Label label7;
        private Label label6;
        private TextBox TbSvRev;
        private Label label5;
        private TextBox TbSVNoAsdu;
        private Label label4;
        private TextBox TbSvAppID;
        private Label label3;
        private TextBox TbSvVLanID;
        private Label label2;
        private TextBox TbSvID;
        private Label label1;
        private TextBox TbSvFreq;
        private Button BtnImportScl;
        private OpenFileDialog openFileDialog1;
        private Button BtnLoadConfig;
        private Button button2;
        private Button BtnSendConfig;
        private Panel PnSv;
        private TableLayoutPanel TPnSV;
        private Label label10;
        private Panel panel5;
        private TextBox TbSvVLanPriority;
        private Label label15;
        private Label label16;
        private TextBox TbSvSmpRate;
    }
}