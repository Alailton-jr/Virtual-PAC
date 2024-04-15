namespace Quality
{
    partial class MonitorForm
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
            panel1 = new Panel();
            BtnStartSearch = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel2 = new Panel();
            TlpSvTable = new TableLayoutPanel();
            svMonitor10 = new SvMonitor();
            panel3 = new Panel();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            PnMenu = new Panel();
            Cbxfluctuation = new CheckBox();
            CbxUnbalance = new CheckBox();
            CbxTransient = new CheckBox();
            CbxHarm = new CheckBox();
            LbSvStatus = new Label();
            CbxVtld = new CheckBox();
            CbxVtcd = new CheckBox();
            CbxGeneral = new CheckBox();
            PnVt = new Panel();
            CbVtEvents = new ComboBox();
            BtnVtConfirm = new Button();
            BtnVtDownload = new Button();
            groupBox1 = new GroupBox();
            TbVtMagnitude = new TextBox();
            TbVtDate = new TextBox();
            TbVtType = new TextBox();
            label8 = new Label();
            TbVtDuration = new TextBox();
            label1 = new Label();
            label3 = new Label();
            label2 = new Label();
            label4 = new Label();
            TimerEvents = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel2.SuspendLayout();
            TlpSvTable.SuspendLayout();
            panel3.SuspendLayout();
            PnVt.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(BtnStartSearch);
            panel1.Controls.Add(tableLayoutPanel2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1068, 597);
            panel1.TabIndex = 0;
            // 
            // BtnStartSearch
            // 
            BtnStartSearch.AutoSize = true;
            BtnStartSearch.FlatAppearance.BorderSize = 5;
            BtnStartSearch.FlatStyle = FlatStyle.Popup;
            BtnStartSearch.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnStartSearch.Location = new Point(12, 27);
            BtnStartSearch.Name = "BtnStartSearch";
            BtnStartSearch.Size = new Size(210, 33);
            BtnStartSearch.TabIndex = 29;
            BtnStartSearch.Text = "Iniciar Monitoramento";
            BtnStartSearch.UseVisualStyleBackColor = true;
            BtnStartSearch.Click += BtnStartSearch_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoScroll = true;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 727F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(panel2, 0, 0);
            tableLayoutPanel2.Controls.Add(panel3, 1, 0);
            tableLayoutPanel2.Location = new Point(12, 80);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1053, 505);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.AutoScrollMargin = new Size(40, 0);
            panel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel2.Controls.Add(TlpSvTable);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(0, 0, 5, 0);
            panel2.Size = new Size(721, 499);
            panel2.TabIndex = 0;
            // 
            // TlpSvTable
            // 
            TlpSvTable.AutoSize = true;
            TlpSvTable.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TlpSvTable.ColumnCount = 4;
            TlpSvTable.ColumnStyles.Add(new ColumnStyle());
            TlpSvTable.ColumnStyles.Add(new ColumnStyle());
            TlpSvTable.ColumnStyles.Add(new ColumnStyle());
            TlpSvTable.ColumnStyles.Add(new ColumnStyle());
            TlpSvTable.Controls.Add(svMonitor10, 0, 0);
            TlpSvTable.Dock = DockStyle.Top;
            TlpSvTable.Location = new Point(0, 0);
            TlpSvTable.Name = "TlpSvTable";
            TlpSvTable.RowCount = 2;
            TlpSvTable.RowStyles.Add(new RowStyle());
            TlpSvTable.RowStyles.Add(new RowStyle());
            TlpSvTable.Size = new Size(716, 248);
            TlpSvTable.TabIndex = 0;
            // 
            // svMonitor10
            // 
            svMonitor10.AutoSize = true;
            svMonitor10.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            svMonitor10.BackColor = Color.FromArgb(31, 45, 56);
            svMonitor10.fluctuation = false;
            svMonitor10.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            svMonitor10.ForeColor = Color.FromArgb(78, 79, 80);
            svMonitor10.harm = false;
            svMonitor10.isRunning = false;
            svMonitor10.Location = new Point(5, 5);
            svMonitor10.Margin = new Padding(5);
            svMonitor10.Name = "svMonitor10";
            svMonitor10.Padding = new Padding(5);
            svMonitor10.selected = false;
            svMonitor10.Size = new Size(165, 238);
            svMonitor10.svID = null;
            svMonitor10.TabIndex = 1;
            svMonitor10.transient = false;
            svMonitor10.unbalance = false;
            svMonitor10.vtcd = false;
            svMonitor10.vtld = false;
            svMonitor10.wasFound = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(button3);
            panel3.Controls.Add(button2);
            panel3.Controls.Add(button1);
            panel3.Controls.Add(PnMenu);
            panel3.Controls.Add(Cbxfluctuation);
            panel3.Controls.Add(CbxUnbalance);
            panel3.Controls.Add(CbxTransient);
            panel3.Controls.Add(CbxHarm);
            panel3.Controls.Add(LbSvStatus);
            panel3.Controls.Add(CbxVtld);
            panel3.Controls.Add(CbxVtcd);
            panel3.Controls.Add(CbxGeneral);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(737, 3);
            panel3.Margin = new Padding(10, 3, 3, 3);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(5);
            panel3.Size = new Size(313, 499);
            panel3.TabIndex = 1;
            // 
            // button3
            // 
            button3.AutoSize = true;
            button3.FlatAppearance.BorderSize = 5;
            button3.FlatStyle = FlatStyle.Popup;
            button3.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.Location = new Point(11, 419);
            button3.Name = "button3";
            button3.Size = new Size(133, 33);
            button3.TabIndex = 84;
            button3.Text = "Configurar SV";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.AutoSize = true;
            button2.FlatAppearance.BorderSize = 5;
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.Location = new Point(180, 419);
            button2.Name = "button2";
            button2.Size = new Size(125, 33);
            button2.TabIndex = 83;
            button2.Text = "Excluir";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.AutoSize = true;
            button1.FlatAppearance.BorderSize = 5;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(20, 458);
            button1.Name = "button1";
            button1.Size = new Size(272, 33);
            button1.TabIndex = 82;
            button1.Text = "Capturar Forma de Onda";
            button1.UseVisualStyleBackColor = true;
            // 
            // PnMenu
            // 
            PnMenu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PnMenu.BackColor = Color.FromArgb(31, 45, 56);
            PnMenu.Location = new Point(5, 112);
            PnMenu.Name = "PnMenu";
            PnMenu.Size = new Size(303, 301);
            PnMenu.TabIndex = 81;
            // 
            // Cbxfluctuation
            // 
            Cbxfluctuation.Appearance = Appearance.Button;
            Cbxfluctuation.AutoSize = true;
            Cbxfluctuation.FlatStyle = FlatStyle.Flat;
            Cbxfluctuation.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Cbxfluctuation.Location = new Point(219, 76);
            Cbxfluctuation.Name = "Cbxfluctuation";
            Cbxfluctuation.Size = new Size(86, 30);
            Cbxfluctuation.TabIndex = 80;
            Cbxfluctuation.Tag = "6";
            Cbxfluctuation.Text = "Flutuação";
            Cbxfluctuation.UseVisualStyleBackColor = true;
            Cbxfluctuation.CheckedChanged += CbxGeneral_CheckedChanged;
            // 
            // CbxUnbalance
            // 
            CbxUnbalance.Appearance = Appearance.Button;
            CbxUnbalance.AutoSize = true;
            CbxUnbalance.FlatStyle = FlatStyle.Flat;
            CbxUnbalance.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CbxUnbalance.Location = new Point(97, 76);
            CbxUnbalance.Name = "CbxUnbalance";
            CbxUnbalance.Size = new Size(98, 30);
            CbxUnbalance.TabIndex = 79;
            CbxUnbalance.Tag = "5";
            CbxUnbalance.Text = "Desbalanço";
            CbxUnbalance.UseVisualStyleBackColor = true;
            CbxUnbalance.CheckedChanged += CbxGeneral_CheckedChanged;
            // 
            // CbxTransient
            // 
            CbxTransient.Appearance = Appearance.Button;
            CbxTransient.AutoSize = true;
            CbxTransient.FlatStyle = FlatStyle.Flat;
            CbxTransient.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CbxTransient.Location = new Point(5, 76);
            CbxTransient.Name = "CbxTransient";
            CbxTransient.Size = new Size(68, 30);
            CbxTransient.TabIndex = 78;
            CbxTransient.Tag = "4";
            CbxTransient.Text = "Transit.";
            CbxTransient.UseVisualStyleBackColor = true;
            CbxTransient.CheckedChanged += CbxGeneral_CheckedChanged;
            // 
            // CbxHarm
            // 
            CbxHarm.Appearance = Appearance.Button;
            CbxHarm.AutoSize = true;
            CbxHarm.FlatStyle = FlatStyle.Flat;
            CbxHarm.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CbxHarm.Location = new Point(245, 40);
            CbxHarm.Name = "CbxHarm";
            CbxHarm.Size = new Size(61, 30);
            CbxHarm.TabIndex = 77;
            CbxHarm.Tag = "3";
            CbxHarm.Text = "Harm.";
            CbxHarm.UseVisualStyleBackColor = true;
            CbxHarm.CheckedChanged += CbxGeneral_CheckedChanged;
            // 
            // LbSvStatus
            // 
            LbSvStatus.BackColor = Color.FromArgb(31, 45, 56);
            LbSvStatus.BorderStyle = BorderStyle.FixedSingle;
            LbSvStatus.Dock = DockStyle.Top;
            LbSvStatus.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LbSvStatus.Image = Properties.Resources.running;
            LbSvStatus.ImageAlign = ContentAlignment.MiddleLeft;
            LbSvStatus.Location = new Point(5, 5);
            LbSvStatus.Name = "LbSvStatus";
            LbSvStatus.Size = new Size(303, 32);
            LbSvStatus.TabIndex = 76;
            LbSvStatus.Text = "       Sampled Value Encontrado";
            LbSvStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CbxVtld
            // 
            CbxVtld.Appearance = Appearance.Button;
            CbxVtld.AutoSize = true;
            CbxVtld.FlatStyle = FlatStyle.Flat;
            CbxVtld.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CbxVtld.Location = new Point(171, 40);
            CbxVtld.Name = "CbxVtld";
            CbxVtld.Size = new Size(55, 30);
            CbxVtld.TabIndex = 75;
            CbxVtld.Tag = "2";
            CbxVtld.Text = "VTLD";
            CbxVtld.UseVisualStyleBackColor = true;
            CbxVtld.CheckedChanged += CbxGeneral_CheckedChanged;
            // 
            // CbxVtcd
            // 
            CbxVtcd.Appearance = Appearance.Button;
            CbxVtcd.AutoSize = true;
            CbxVtcd.FlatStyle = FlatStyle.Flat;
            CbxVtcd.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CbxVtcd.Location = new Point(96, 40);
            CbxVtcd.Name = "CbxVtcd";
            CbxVtcd.Size = new Size(56, 30);
            CbxVtcd.TabIndex = 74;
            CbxVtcd.Tag = "1";
            CbxVtcd.Text = "VTCD";
            CbxVtcd.UseVisualStyleBackColor = true;
            CbxVtcd.CheckedChanged += CbxGeneral_CheckedChanged;
            // 
            // CbxGeneral
            // 
            CbxGeneral.Appearance = Appearance.Button;
            CbxGeneral.AutoSize = true;
            CbxGeneral.Checked = true;
            CbxGeneral.CheckState = CheckState.Checked;
            CbxGeneral.FlatStyle = FlatStyle.Flat;
            CbxGeneral.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CbxGeneral.Location = new Point(5, 40);
            CbxGeneral.Name = "CbxGeneral";
            CbxGeneral.Size = new Size(72, 30);
            CbxGeneral.TabIndex = 73;
            CbxGeneral.Tag = "0";
            CbxGeneral.Text = "General";
            CbxGeneral.UseVisualStyleBackColor = true;
            CbxGeneral.CheckedChanged += CbxGeneral_CheckedChanged;
            // 
            // PnVt
            // 
            PnVt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PnVt.BackColor = Color.FromArgb(31, 45, 56);
            PnVt.Controls.Add(CbVtEvents);
            PnVt.Controls.Add(BtnVtConfirm);
            PnVt.Controls.Add(BtnVtDownload);
            PnVt.Controls.Add(groupBox1);
            PnVt.Controls.Add(label4);
            PnVt.Location = new Point(47, 626);
            PnVt.Name = "PnVt";
            PnVt.Padding = new Padding(5);
            PnVt.Size = new Size(303, 301);
            PnVt.TabIndex = 1;
            PnVt.Visible = false;
            // 
            // CbVtEvents
            // 
            CbVtEvents.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CbVtEvents.BackColor = Color.FromArgb(31, 45, 56);
            CbVtEvents.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            CbVtEvents.ForeColor = Color.Lavender;
            CbVtEvents.FormattingEnabled = true;
            CbVtEvents.Location = new Point(19, 41);
            CbVtEvents.Name = "CbVtEvents";
            CbVtEvents.Size = new Size(268, 28);
            CbVtEvents.TabIndex = 42;
            CbVtEvents.SelectedIndexChanged += CbVtEvents_SelectedIndexChanged;
            // 
            // BtnVtConfirm
            // 
            BtnVtConfirm.AutoSize = true;
            BtnVtConfirm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnVtConfirm.FlatAppearance.BorderSize = 5;
            BtnVtConfirm.FlatStyle = FlatStyle.Popup;
            BtnVtConfirm.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            BtnVtConfirm.Location = new Point(27, 262);
            BtnVtConfirm.Name = "BtnVtConfirm";
            BtnVtConfirm.Size = new Size(88, 30);
            BtnVtConfirm.TabIndex = 86;
            BtnVtConfirm.Text = "Confirmar";
            BtnVtConfirm.UseVisualStyleBackColor = true;
            // 
            // BtnVtDownload
            // 
            BtnVtDownload.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnVtDownload.AutoSize = true;
            BtnVtDownload.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnVtDownload.FlatAppearance.BorderSize = 5;
            BtnVtDownload.FlatStyle = FlatStyle.Popup;
            BtnVtDownload.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            BtnVtDownload.Location = new Point(155, 262);
            BtnVtDownload.Name = "BtnVtDownload";
            BtnVtDownload.Size = new Size(122, 30);
            BtnVtDownload.TabIndex = 85;
            BtnVtDownload.Text = "Baixar Registro";
            BtnVtDownload.UseVisualStyleBackColor = true;
            BtnVtDownload.Click += BtnVtDownload_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(TbVtMagnitude);
            groupBox1.Controls.Add(TbVtDate);
            groupBox1.Controls.Add(TbVtType);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(TbVtDuration);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Font = new Font("Segoe UI", 11F);
            groupBox1.ForeColor = Color.Lavender;
            groupBox1.Location = new Point(22, 67);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(262, 191);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Infomações";
            // 
            // TbVtMagnitude
            // 
            TbVtMagnitude.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbVtMagnitude.BackColor = Color.FromArgb(31, 45, 56);
            TbVtMagnitude.BorderStyle = BorderStyle.FixedSingle;
            TbVtMagnitude.Font = new Font("Segoe UI", 13F);
            TbVtMagnitude.ForeColor = Color.Lavender;
            TbVtMagnitude.Location = new Point(146, 108);
            TbVtMagnitude.Margin = new Padding(0, 5, 0, 5);
            TbVtMagnitude.Name = "TbVtMagnitude";
            TbVtMagnitude.Size = new Size(109, 31);
            TbVtMagnitude.TabIndex = 40;
            TbVtMagnitude.Text = "0.4234 pu";
            TbVtMagnitude.TextAlign = HorizontalAlignment.Center;
            // 
            // TbVtDate
            // 
            TbVtDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbVtDate.BackColor = Color.FromArgb(31, 45, 56);
            TbVtDate.BorderStyle = BorderStyle.FixedSingle;
            TbVtDate.Font = new Font("Segoe UI", 13F);
            TbVtDate.ForeColor = Color.Lavender;
            TbVtDate.Location = new Point(63, 67);
            TbVtDate.Margin = new Padding(0, 5, 0, 5);
            TbVtDate.Name = "TbVtDate";
            TbVtDate.Size = new Size(192, 31);
            TbVtDate.TabIndex = 37;
            TbVtDate.Text = "04/12/2024 5:38:54";
            TbVtDate.TextAlign = HorizontalAlignment.Center;
            // 
            // TbVtType
            // 
            TbVtType.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbVtType.BackColor = Color.FromArgb(31, 45, 56);
            TbVtType.BorderStyle = BorderStyle.FixedSingle;
            TbVtType.Font = new Font("Segoe UI", 13F);
            TbVtType.ForeColor = Color.Lavender;
            TbVtType.Location = new Point(63, 26);
            TbVtType.Margin = new Padding(0, 5, 0, 5);
            TbVtType.Name = "TbVtType";
            TbVtType.Size = new Size(192, 31);
            TbVtType.TabIndex = 35;
            TbVtType.Text = " Interrupção Sustentada";
            TbVtType.TextAlign = HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.BackColor = Color.FromArgb(31, 45, 56);
            label8.BorderStyle = BorderStyle.FixedSingle;
            label8.FlatStyle = FlatStyle.Popup;
            label8.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold);
            label8.Location = new Point(5, 26);
            label8.Margin = new Padding(0, 5, 0, 5);
            label8.Name = "label8";
            label8.Size = new Size(58, 31);
            label8.TabIndex = 34;
            label8.Text = "Tipo";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TbVtDuration
            // 
            TbVtDuration.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbVtDuration.BackColor = Color.FromArgb(31, 45, 56);
            TbVtDuration.BorderStyle = BorderStyle.FixedSingle;
            TbVtDuration.Font = new Font("Segoe UI", 13F);
            TbVtDuration.ForeColor = Color.Lavender;
            TbVtDuration.Location = new Point(146, 149);
            TbVtDuration.Margin = new Padding(0, 5, 0, 5);
            TbVtDuration.Name = "TbVtDuration";
            TbVtDuration.Size = new Size(109, 31);
            TbVtDuration.TabIndex = 41;
            TbVtDuration.Text = "2.45 s";
            TbVtDuration.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(31, 45, 56);
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.FlatStyle = FlatStyle.Popup;
            label1.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold);
            label1.Location = new Point(5, 67);
            label1.Margin = new Padding(0, 5, 0, 5);
            label1.Name = "label1";
            label1.Size = new Size(58, 31);
            label1.TabIndex = 36;
            label1.Text = "Data";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(31, 45, 56);
            label3.BorderStyle = BorderStyle.FixedSingle;
            label3.FlatStyle = FlatStyle.Popup;
            label3.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold);
            label3.Location = new Point(5, 149);
            label3.Margin = new Padding(0, 5, 0, 5);
            label3.Name = "label3";
            label3.Size = new Size(141, 31);
            label3.TabIndex = 39;
            label3.Text = "Duração";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(31, 45, 56);
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.FlatStyle = FlatStyle.Popup;
            label2.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold);
            label2.Location = new Point(5, 108);
            label2.Margin = new Padding(0, 5, 0, 5);
            label2.Name = "label2";
            label2.Size = new Size(141, 31);
            label2.TabIndex = 38;
            label2.Text = "Magnitude";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.BackColor = Color.FromArgb(31, 45, 56);
            label4.BorderStyle = BorderStyle.FixedSingle;
            label4.FlatStyle = FlatStyle.Popup;
            label4.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold);
            label4.Location = new Point(19, 6);
            label4.Margin = new Padding(0, 5, 0, 5);
            label4.Name = "label4";
            label4.Size = new Size(268, 31);
            label4.TabIndex = 43;
            label4.Text = "Eventos Registrados";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TimerEvents
            // 
            TimerEvents.Interval = 1000;
            TimerEvents.Tick += TimerEvents_Tick;
            // 
            // MonitorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1068, 954);
            Controls.Add(PnVt);
            Controls.Add(panel1);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Name = "MonitorForm";
            Text = "MonitorForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            TlpSvTable.ResumeLayout(false);
            TlpSvTable.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            PnVt.ResumeLayout(false);
            PnVt.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private TableLayoutPanel TlpSvTable;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel3;
        private SvMonitor svMonitor8;
        private SvMonitor svMonitor7;
        private SvMonitor svMonitor6;
        private SvMonitor svMonitor5;
        private SvMonitor svMonitor4;
        private SvMonitor svMonitor3;
        private SvMonitor svMonitor1;
        private SvMonitor svMonitor2;
        private SvMonitor svMonitor10;
        private CheckBox CbxGeneral;
        private Label LbSvStatus;
        private CheckBox CbxVtld;
        private CheckBox CbxVtcd;
        private CheckBox CbxUnbalance;
        private CheckBox CbxTransient;
        private CheckBox CbxHarm;
        private Panel PnMenu;
        private CheckBox Cbxfluctuation;
        private Button button2;
        private Button button1;
        private Button button3;
        private Panel PnVt;
        private Label label8;
        private TextBox TbVtType;
        private Label label2;
        private TextBox TbVtDate;
        private Label label1;
        private TextBox TbVtDuration;
        private Label label3;
        private TextBox TbVtMagnitude;
        private Button BtnVtDownload;
        private GroupBox groupBox1;
        private Label label4;
        private ComboBox CbVtEvents;
        private Button BtnVtConfirm;
        private Button BtnStartSearch;
        private System.Windows.Forms.Timer TimerEvents;
    }
}