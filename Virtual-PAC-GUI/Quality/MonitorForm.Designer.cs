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
            BtnConfig = new Button();
            BtnDel = new Button();
            BtnSaveWaveForm = new Button();
            PnMenu = new Panel();
            Cbxfluctuation = new CheckBox();
            CbxUnbalance = new CheckBox();
            CbxTransient = new CheckBox();
            CbxHarm = new CheckBox();
            LbSvStatus = new Label();
            CbxVtld = new CheckBox();
            CbxVtcd = new CheckBox();
            CbxGeneral = new CheckBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            TbGenVaAng = new TextBox();
            LbGenVa = new Label();
            TbGenVaMod = new TextBox();
            label7 = new Label();
            label6 = new Label();
            TbGenVbAng = new TextBox();
            LbGenVb = new Label();
            TbGenVbMod = new TextBox();
            LbGenVc = new Label();
            LbGenVn = new Label();
            TbGenVcMod = new TextBox();
            TbGenVnMod = new TextBox();
            TbGenVcAng = new TextBox();
            TbGenVnAng = new TextBox();
            LbGenIa = new Label();
            LbGenIc = new Label();
            LbGenIn = new Label();
            TbGenIaMod = new TextBox();
            TbGenIaAng = new TextBox();
            TbGenIbMod = new TextBox();
            TbGenIbAng = new TextBox();
            TbGenIcMod = new TextBox();
            TbGenIcAng = new TextBox();
            TbGenInMod = new TextBox();
            TbGenInAng = new TextBox();
            label5 = new Label();
            label17 = new Label();
            LbGenIb = new Label();
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
            PnGeneral = new Panel();
            groupBox2 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            panel5 = new Panel();
            tableLayoutPanel4 = new TableLayoutPanel();
            label18 = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            TbGenUnbalanceI = new TextBox();
            TbGenUnbalanceV = new TextBox();
            label19 = new Label();
            label20 = new Label();
            tableLayoutPanel6 = new TableLayoutPanel();
            CbGenMeasures = new ComboBox();
            BtnAbcOr012 = new Button();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel2.SuspendLayout();
            TlpSvTable.SuspendLayout();
            panel3.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            PnVt.SuspendLayout();
            groupBox1.SuspendLayout();
            PnGeneral.SuspendLayout();
            groupBox2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel5.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
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
            panel3.Controls.Add(BtnConfig);
            panel3.Controls.Add(BtnDel);
            panel3.Controls.Add(BtnSaveWaveForm);
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
            // BtnConfig
            // 
            BtnConfig.AutoSize = true;
            BtnConfig.FlatAppearance.BorderSize = 5;
            BtnConfig.FlatStyle = FlatStyle.Popup;
            BtnConfig.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnConfig.Location = new Point(11, 419);
            BtnConfig.Name = "BtnConfig";
            BtnConfig.Size = new Size(133, 33);
            BtnConfig.TabIndex = 84;
            BtnConfig.Text = "Configurar SV";
            BtnConfig.UseVisualStyleBackColor = true;
            // 
            // BtnDel
            // 
            BtnDel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnDel.AutoSize = true;
            BtnDel.FlatAppearance.BorderSize = 5;
            BtnDel.FlatStyle = FlatStyle.Popup;
            BtnDel.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnDel.Location = new Point(180, 419);
            BtnDel.Name = "BtnDel";
            BtnDel.Size = new Size(125, 33);
            BtnDel.TabIndex = 83;
            BtnDel.Text = "Excluir";
            BtnDel.UseVisualStyleBackColor = true;
            BtnDel.Click += BtnDel_Click;
            // 
            // BtnSaveWaveForm
            // 
            BtnSaveWaveForm.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnSaveWaveForm.AutoSize = true;
            BtnSaveWaveForm.FlatAppearance.BorderSize = 5;
            BtnSaveWaveForm.FlatStyle = FlatStyle.Popup;
            BtnSaveWaveForm.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnSaveWaveForm.Location = new Point(20, 458);
            BtnSaveWaveForm.Name = "BtnSaveWaveForm";
            BtnSaveWaveForm.Size = new Size(272, 33);
            BtnSaveWaveForm.TabIndex = 82;
            BtnSaveWaveForm.Text = "Capturar Forma de Onda";
            BtnSaveWaveForm.UseVisualStyleBackColor = true;
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
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.636364F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.90909F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.Controls.Add(TbGenVaAng, 2, 1);
            tableLayoutPanel1.Controls.Add(LbGenVa, 0, 1);
            tableLayoutPanel1.Controls.Add(TbGenVaMod, 1, 1);
            tableLayoutPanel1.Controls.Add(label7, 2, 0);
            tableLayoutPanel1.Controls.Add(label6, 1, 0);
            tableLayoutPanel1.Controls.Add(TbGenVbAng, 2, 2);
            tableLayoutPanel1.Controls.Add(LbGenVb, 0, 2);
            tableLayoutPanel1.Controls.Add(TbGenVbMod, 1, 2);
            tableLayoutPanel1.Controls.Add(LbGenVc, 0, 3);
            tableLayoutPanel1.Controls.Add(LbGenVn, 0, 4);
            tableLayoutPanel1.Controls.Add(TbGenVcMod, 1, 3);
            tableLayoutPanel1.Controls.Add(TbGenVnMod, 1, 4);
            tableLayoutPanel1.Controls.Add(TbGenVcAng, 2, 3);
            tableLayoutPanel1.Controls.Add(TbGenVnAng, 2, 4);
            tableLayoutPanel1.Controls.Add(LbGenIa, 3, 1);
            tableLayoutPanel1.Controls.Add(LbGenIc, 3, 3);
            tableLayoutPanel1.Controls.Add(LbGenIn, 3, 4);
            tableLayoutPanel1.Controls.Add(TbGenIaMod, 4, 1);
            tableLayoutPanel1.Controls.Add(TbGenIaAng, 5, 1);
            tableLayoutPanel1.Controls.Add(TbGenIbMod, 4, 2);
            tableLayoutPanel1.Controls.Add(TbGenIbAng, 5, 2);
            tableLayoutPanel1.Controls.Add(TbGenIcMod, 4, 3);
            tableLayoutPanel1.Controls.Add(TbGenIcAng, 5, 3);
            tableLayoutPanel1.Controls.Add(TbGenInMod, 4, 4);
            tableLayoutPanel1.Controls.Add(TbGenInAng, 5, 4);
            tableLayoutPanel1.Controls.Add(label5, 4, 0);
            tableLayoutPanel1.Controls.Add(label17, 5, 0);
            tableLayoutPanel1.Controls.Add(LbGenIb, 3, 2);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(3, 41);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));
            tableLayoutPanel1.Size = new Size(275, 121);
            tableLayoutPanel1.TabIndex = 93;
            // 
            // TbGenVaAng
            // 
            TbGenVaAng.BackColor = Color.FromArgb(31, 45, 56);
            TbGenVaAng.BorderStyle = BorderStyle.FixedSingle;
            TbGenVaAng.Dock = DockStyle.Fill;
            TbGenVaAng.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenVaAng.ForeColor = Color.Lavender;
            TbGenVaAng.Location = new Point(95, 21);
            TbGenVaAng.Margin = new Padding(0);
            TbGenVaAng.Name = "TbGenVaAng";
            TbGenVaAng.Size = new Size(41, 25);
            TbGenVaAng.TabIndex = 92;
            // 
            // LbGenVa
            // 
            LbGenVa.BackColor = Color.FromArgb(31, 45, 56);
            LbGenVa.BorderStyle = BorderStyle.FixedSingle;
            LbGenVa.Dock = DockStyle.Fill;
            LbGenVa.FlatStyle = FlatStyle.Popup;
            LbGenVa.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            LbGenVa.Location = new Point(0, 21);
            LbGenVa.Margin = new Padding(0);
            LbGenVa.Name = "LbGenVa";
            LbGenVa.Size = new Size(32, 25);
            LbGenVa.TabIndex = 90;
            LbGenVa.Text = "Va";
            LbGenVa.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TbGenVaMod
            // 
            TbGenVaMod.BackColor = Color.FromArgb(31, 45, 56);
            TbGenVaMod.BorderStyle = BorderStyle.FixedSingle;
            TbGenVaMod.Dock = DockStyle.Fill;
            TbGenVaMod.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenVaMod.ForeColor = Color.Lavender;
            TbGenVaMod.Location = new Point(32, 21);
            TbGenVaMod.Margin = new Padding(0);
            TbGenVaMod.Name = "TbGenVaMod";
            TbGenVaMod.Size = new Size(63, 25);
            TbGenVaMod.TabIndex = 91;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.FromArgb(31, 45, 56);
            label7.BorderStyle = BorderStyle.FixedSingle;
            label7.Dock = DockStyle.Fill;
            label7.FlatStyle = FlatStyle.Popup;
            label7.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label7.Location = new Point(95, 0);
            label7.Margin = new Padding(0);
            label7.Name = "label7";
            label7.Size = new Size(41, 21);
            label7.TabIndex = 37;
            label7.Text = "Ang";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(31, 45, 56);
            label6.BorderStyle = BorderStyle.FixedSingle;
            label6.Dock = DockStyle.Fill;
            label6.FlatStyle = FlatStyle.Popup;
            label6.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label6.Location = new Point(32, 0);
            label6.Margin = new Padding(0);
            label6.Name = "label6";
            label6.Size = new Size(63, 21);
            label6.TabIndex = 36;
            label6.Text = "Modulo";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TbGenVbAng
            // 
            TbGenVbAng.BackColor = Color.FromArgb(31, 45, 56);
            TbGenVbAng.BorderStyle = BorderStyle.FixedSingle;
            TbGenVbAng.Dock = DockStyle.Fill;
            TbGenVbAng.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenVbAng.ForeColor = Color.Lavender;
            TbGenVbAng.Location = new Point(95, 46);
            TbGenVbAng.Margin = new Padding(0);
            TbGenVbAng.Name = "TbGenVbAng";
            TbGenVbAng.Size = new Size(41, 25);
            TbGenVbAng.TabIndex = 94;
            // 
            // LbGenVb
            // 
            LbGenVb.BackColor = Color.FromArgb(31, 45, 56);
            LbGenVb.BorderStyle = BorderStyle.FixedSingle;
            LbGenVb.Dock = DockStyle.Fill;
            LbGenVb.FlatStyle = FlatStyle.Popup;
            LbGenVb.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            LbGenVb.Location = new Point(0, 46);
            LbGenVb.Margin = new Padding(0);
            LbGenVb.Name = "LbGenVb";
            LbGenVb.Size = new Size(32, 25);
            LbGenVb.TabIndex = 93;
            LbGenVb.Text = "Vb";
            LbGenVb.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TbGenVbMod
            // 
            TbGenVbMod.BackColor = Color.FromArgb(31, 45, 56);
            TbGenVbMod.BorderStyle = BorderStyle.FixedSingle;
            TbGenVbMod.Dock = DockStyle.Fill;
            TbGenVbMod.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenVbMod.ForeColor = Color.Lavender;
            TbGenVbMod.Location = new Point(32, 46);
            TbGenVbMod.Margin = new Padding(0);
            TbGenVbMod.Name = "TbGenVbMod";
            TbGenVbMod.Size = new Size(63, 25);
            TbGenVbMod.TabIndex = 95;
            // 
            // LbGenVc
            // 
            LbGenVc.BackColor = Color.FromArgb(31, 45, 56);
            LbGenVc.BorderStyle = BorderStyle.FixedSingle;
            LbGenVc.Dock = DockStyle.Fill;
            LbGenVc.FlatStyle = FlatStyle.Popup;
            LbGenVc.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            LbGenVc.Location = new Point(0, 71);
            LbGenVc.Margin = new Padding(0);
            LbGenVc.Name = "LbGenVc";
            LbGenVc.Size = new Size(32, 25);
            LbGenVc.TabIndex = 96;
            LbGenVc.Text = "Vc";
            LbGenVc.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LbGenVn
            // 
            LbGenVn.BackColor = Color.FromArgb(31, 45, 56);
            LbGenVn.BorderStyle = BorderStyle.FixedSingle;
            LbGenVn.Dock = DockStyle.Fill;
            LbGenVn.FlatStyle = FlatStyle.Popup;
            LbGenVn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            LbGenVn.Location = new Point(0, 96);
            LbGenVn.Margin = new Padding(0);
            LbGenVn.Name = "LbGenVn";
            LbGenVn.Size = new Size(32, 25);
            LbGenVn.TabIndex = 97;
            LbGenVn.Text = "Vn";
            LbGenVn.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TbGenVcMod
            // 
            TbGenVcMod.BackColor = Color.FromArgb(31, 45, 56);
            TbGenVcMod.BorderStyle = BorderStyle.FixedSingle;
            TbGenVcMod.Dock = DockStyle.Fill;
            TbGenVcMod.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenVcMod.ForeColor = Color.Lavender;
            TbGenVcMod.Location = new Point(32, 71);
            TbGenVcMod.Margin = new Padding(0);
            TbGenVcMod.Name = "TbGenVcMod";
            TbGenVcMod.Size = new Size(63, 25);
            TbGenVcMod.TabIndex = 102;
            // 
            // TbGenVnMod
            // 
            TbGenVnMod.BackColor = Color.FromArgb(31, 45, 56);
            TbGenVnMod.BorderStyle = BorderStyle.FixedSingle;
            TbGenVnMod.Dock = DockStyle.Fill;
            TbGenVnMod.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenVnMod.ForeColor = Color.Lavender;
            TbGenVnMod.Location = new Point(32, 96);
            TbGenVnMod.Margin = new Padding(0);
            TbGenVnMod.Name = "TbGenVnMod";
            TbGenVnMod.Size = new Size(63, 25);
            TbGenVnMod.TabIndex = 103;
            // 
            // TbGenVcAng
            // 
            TbGenVcAng.BackColor = Color.FromArgb(31, 45, 56);
            TbGenVcAng.BorderStyle = BorderStyle.FixedSingle;
            TbGenVcAng.Dock = DockStyle.Fill;
            TbGenVcAng.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenVcAng.ForeColor = Color.Lavender;
            TbGenVcAng.Location = new Point(95, 71);
            TbGenVcAng.Margin = new Padding(0);
            TbGenVcAng.Name = "TbGenVcAng";
            TbGenVcAng.Size = new Size(41, 25);
            TbGenVcAng.TabIndex = 107;
            // 
            // TbGenVnAng
            // 
            TbGenVnAng.BackColor = Color.FromArgb(31, 45, 56);
            TbGenVnAng.BorderStyle = BorderStyle.FixedSingle;
            TbGenVnAng.Dock = DockStyle.Fill;
            TbGenVnAng.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenVnAng.ForeColor = Color.Lavender;
            TbGenVnAng.Location = new Point(95, 96);
            TbGenVnAng.Margin = new Padding(0);
            TbGenVnAng.Name = "TbGenVnAng";
            TbGenVnAng.Size = new Size(41, 25);
            TbGenVnAng.TabIndex = 108;
            // 
            // LbGenIa
            // 
            LbGenIa.BackColor = Color.FromArgb(31, 45, 56);
            LbGenIa.BorderStyle = BorderStyle.FixedSingle;
            LbGenIa.Dock = DockStyle.Fill;
            LbGenIa.FlatStyle = FlatStyle.Popup;
            LbGenIa.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            LbGenIa.Location = new Point(136, 21);
            LbGenIa.Margin = new Padding(0);
            LbGenIa.Name = "LbGenIa";
            LbGenIa.Size = new Size(27, 25);
            LbGenIa.TabIndex = 98;
            LbGenIa.Text = "Ia";
            LbGenIa.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LbGenIc
            // 
            LbGenIc.BackColor = Color.FromArgb(31, 45, 56);
            LbGenIc.BorderStyle = BorderStyle.FixedSingle;
            LbGenIc.Dock = DockStyle.Fill;
            LbGenIc.FlatStyle = FlatStyle.Popup;
            LbGenIc.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            LbGenIc.Location = new Point(136, 71);
            LbGenIc.Margin = new Padding(0);
            LbGenIc.Name = "LbGenIc";
            LbGenIc.Size = new Size(27, 25);
            LbGenIc.TabIndex = 100;
            LbGenIc.Text = "Ic";
            LbGenIc.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LbGenIn
            // 
            LbGenIn.BackColor = Color.FromArgb(31, 45, 56);
            LbGenIn.BorderStyle = BorderStyle.FixedSingle;
            LbGenIn.Dock = DockStyle.Fill;
            LbGenIn.FlatStyle = FlatStyle.Popup;
            LbGenIn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            LbGenIn.Location = new Point(136, 96);
            LbGenIn.Margin = new Padding(0);
            LbGenIn.Name = "LbGenIn";
            LbGenIn.Size = new Size(27, 25);
            LbGenIn.TabIndex = 101;
            LbGenIn.Text = "In";
            LbGenIn.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TbGenIaMod
            // 
            TbGenIaMod.BackColor = Color.FromArgb(31, 45, 56);
            TbGenIaMod.BorderStyle = BorderStyle.FixedSingle;
            TbGenIaMod.Dock = DockStyle.Fill;
            TbGenIaMod.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenIaMod.ForeColor = Color.Lavender;
            TbGenIaMod.Location = new Point(163, 21);
            TbGenIaMod.Margin = new Padding(0);
            TbGenIaMod.Name = "TbGenIaMod";
            TbGenIaMod.Size = new Size(69, 25);
            TbGenIaMod.TabIndex = 104;
            // 
            // TbGenIaAng
            // 
            TbGenIaAng.BackColor = Color.FromArgb(31, 45, 56);
            TbGenIaAng.BorderStyle = BorderStyle.FixedSingle;
            TbGenIaAng.Dock = DockStyle.Fill;
            TbGenIaAng.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenIaAng.ForeColor = Color.Lavender;
            TbGenIaAng.Location = new Point(232, 21);
            TbGenIaAng.Margin = new Padding(0);
            TbGenIaAng.Name = "TbGenIaAng";
            TbGenIaAng.Size = new Size(43, 25);
            TbGenIaAng.TabIndex = 109;
            // 
            // TbGenIbMod
            // 
            TbGenIbMod.BackColor = Color.FromArgb(31, 45, 56);
            TbGenIbMod.BorderStyle = BorderStyle.FixedSingle;
            TbGenIbMod.Dock = DockStyle.Fill;
            TbGenIbMod.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenIbMod.ForeColor = Color.Lavender;
            TbGenIbMod.Location = new Point(163, 46);
            TbGenIbMod.Margin = new Padding(0);
            TbGenIbMod.Name = "TbGenIbMod";
            TbGenIbMod.Size = new Size(69, 25);
            TbGenIbMod.TabIndex = 105;
            // 
            // TbGenIbAng
            // 
            TbGenIbAng.BackColor = Color.FromArgb(31, 45, 56);
            TbGenIbAng.BorderStyle = BorderStyle.FixedSingle;
            TbGenIbAng.Dock = DockStyle.Fill;
            TbGenIbAng.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenIbAng.ForeColor = Color.Lavender;
            TbGenIbAng.Location = new Point(232, 46);
            TbGenIbAng.Margin = new Padding(0);
            TbGenIbAng.Name = "TbGenIbAng";
            TbGenIbAng.Size = new Size(43, 25);
            TbGenIbAng.TabIndex = 110;
            // 
            // TbGenIcMod
            // 
            TbGenIcMod.BackColor = Color.FromArgb(31, 45, 56);
            TbGenIcMod.BorderStyle = BorderStyle.FixedSingle;
            TbGenIcMod.Dock = DockStyle.Fill;
            TbGenIcMod.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenIcMod.ForeColor = Color.Lavender;
            TbGenIcMod.Location = new Point(163, 71);
            TbGenIcMod.Margin = new Padding(0);
            TbGenIcMod.Name = "TbGenIcMod";
            TbGenIcMod.Size = new Size(69, 25);
            TbGenIcMod.TabIndex = 106;
            // 
            // TbGenIcAng
            // 
            TbGenIcAng.BackColor = Color.FromArgb(31, 45, 56);
            TbGenIcAng.BorderStyle = BorderStyle.FixedSingle;
            TbGenIcAng.Dock = DockStyle.Fill;
            TbGenIcAng.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenIcAng.ForeColor = Color.Lavender;
            TbGenIcAng.Location = new Point(232, 71);
            TbGenIcAng.Margin = new Padding(0);
            TbGenIcAng.Name = "TbGenIcAng";
            TbGenIcAng.Size = new Size(43, 25);
            TbGenIcAng.TabIndex = 112;
            // 
            // TbGenInMod
            // 
            TbGenInMod.BackColor = Color.FromArgb(31, 45, 56);
            TbGenInMod.BorderStyle = BorderStyle.FixedSingle;
            TbGenInMod.Dock = DockStyle.Fill;
            TbGenInMod.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenInMod.ForeColor = Color.Lavender;
            TbGenInMod.Location = new Point(163, 96);
            TbGenInMod.Margin = new Padding(0);
            TbGenInMod.Name = "TbGenInMod";
            TbGenInMod.Size = new Size(69, 25);
            TbGenInMod.TabIndex = 113;
            // 
            // TbGenInAng
            // 
            TbGenInAng.BackColor = Color.FromArgb(31, 45, 56);
            TbGenInAng.BorderStyle = BorderStyle.FixedSingle;
            TbGenInAng.Dock = DockStyle.Fill;
            TbGenInAng.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenInAng.ForeColor = Color.Lavender;
            TbGenInAng.Location = new Point(232, 96);
            TbGenInAng.Margin = new Padding(0);
            TbGenInAng.Name = "TbGenInAng";
            TbGenInAng.Size = new Size(43, 25);
            TbGenInAng.TabIndex = 111;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(31, 45, 56);
            label5.BorderStyle = BorderStyle.FixedSingle;
            label5.Dock = DockStyle.Fill;
            label5.FlatStyle = FlatStyle.Popup;
            label5.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label5.Location = new Point(163, 0);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(69, 21);
            label5.TabIndex = 114;
            label5.Text = "Modulo";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.BackColor = Color.FromArgb(31, 45, 56);
            label17.BorderStyle = BorderStyle.FixedSingle;
            label17.Dock = DockStyle.Fill;
            label17.FlatStyle = FlatStyle.Popup;
            label17.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label17.Location = new Point(232, 0);
            label17.Margin = new Padding(0);
            label17.Name = "label17";
            label17.Size = new Size(43, 21);
            label17.TabIndex = 115;
            label17.Text = "Ang";
            label17.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LbGenIb
            // 
            LbGenIb.BackColor = Color.FromArgb(31, 45, 56);
            LbGenIb.BorderStyle = BorderStyle.FixedSingle;
            LbGenIb.Dock = DockStyle.Fill;
            LbGenIb.FlatStyle = FlatStyle.Popup;
            LbGenIb.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            LbGenIb.Location = new Point(136, 46);
            LbGenIb.Margin = new Padding(0);
            LbGenIb.Name = "LbGenIb";
            LbGenIb.Size = new Size(27, 25);
            LbGenIb.TabIndex = 99;
            LbGenIb.Text = "Ib";
            LbGenIb.TextAlign = ContentAlignment.MiddleCenter;
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
            PnVt.Location = new Point(20, 603);
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
            // PnGeneral
            // 
            PnGeneral.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PnGeneral.BackColor = Color.FromArgb(31, 45, 56);
            PnGeneral.Controls.Add(groupBox2);
            PnGeneral.Location = new Point(369, 603);
            PnGeneral.Name = "PnGeneral";
            PnGeneral.Padding = new Padding(5);
            PnGeneral.Size = new Size(303, 301);
            PnGeneral.TabIndex = 2;
            PnGeneral.Visible = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tableLayoutPanel3);
            groupBox2.Font = new Font("Segoe UI", 11F);
            groupBox2.ForeColor = Color.Lavender;
            groupBox2.Location = new Point(8, 8);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(287, 285);
            groupBox2.TabIndex = 88;
            groupBox2.TabStop = false;
            groupBox2.Text = "Medições";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel1, 0, 1);
            tableLayoutPanel3.Controls.Add(panel5, 0, 2);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel6, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 23);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.Padding = new Padding(0, 6, 0, 0);
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 131F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel3.Size = new Size(281, 259);
            tableLayoutPanel3.TabIndex = 94;
            // 
            // panel5
            // 
            panel5.Controls.Add(tableLayoutPanel4);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(3, 172);
            panel5.Name = "panel5";
            panel5.Size = new Size(275, 84);
            panel5.TabIndex = 94;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(label18, 0, 0);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(0, 0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.Size = new Size(275, 84);
            tableLayoutPanel4.TabIndex = 3;
            // 
            // label18
            // 
            label18.BackColor = Color.FromArgb(31, 45, 56);
            label18.BorderStyle = BorderStyle.FixedSingle;
            label18.Dock = DockStyle.Fill;
            label18.FlatStyle = FlatStyle.Popup;
            label18.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            label18.Location = new Point(0, 0);
            label18.Margin = new Padding(0);
            label18.Name = "label18";
            label18.Size = new Size(275, 23);
            label18.TabIndex = 35;
            label18.Text = "Desbalaço";
            label18.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.AutoSize = true;
            tableLayoutPanel5.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54.6391754F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.3608246F));
            tableLayoutPanel5.Controls.Add(TbGenUnbalanceI, 1, 1);
            tableLayoutPanel5.Controls.Add(TbGenUnbalanceV, 1, 0);
            tableLayoutPanel5.Controls.Add(label19, 0, 0);
            tableLayoutPanel5.Controls.Add(label20, 0, 1);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 26);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 3;
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.Size = new Size(269, 55);
            tableLayoutPanel5.TabIndex = 36;
            // 
            // TbGenUnbalanceI
            // 
            TbGenUnbalanceI.BackColor = Color.FromArgb(31, 45, 56);
            TbGenUnbalanceI.BorderStyle = BorderStyle.FixedSingle;
            TbGenUnbalanceI.Dock = DockStyle.Fill;
            TbGenUnbalanceI.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenUnbalanceI.ForeColor = Color.Lavender;
            TbGenUnbalanceI.Location = new Point(146, 25);
            TbGenUnbalanceI.Margin = new Padding(0);
            TbGenUnbalanceI.Name = "TbGenUnbalanceI";
            TbGenUnbalanceI.Size = new Size(123, 25);
            TbGenUnbalanceI.TabIndex = 93;
            // 
            // TbGenUnbalanceV
            // 
            TbGenUnbalanceV.BackColor = Color.FromArgb(31, 45, 56);
            TbGenUnbalanceV.BorderStyle = BorderStyle.FixedSingle;
            TbGenUnbalanceV.Dock = DockStyle.Fill;
            TbGenUnbalanceV.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TbGenUnbalanceV.ForeColor = Color.Lavender;
            TbGenUnbalanceV.Location = new Point(146, 0);
            TbGenUnbalanceV.Margin = new Padding(0);
            TbGenUnbalanceV.Name = "TbGenUnbalanceV";
            TbGenUnbalanceV.Size = new Size(123, 25);
            TbGenUnbalanceV.TabIndex = 92;
            // 
            // label19
            // 
            label19.BackColor = Color.FromArgb(31, 45, 56);
            label19.BorderStyle = BorderStyle.FixedSingle;
            label19.Dock = DockStyle.Fill;
            label19.FlatStyle = FlatStyle.Popup;
            label19.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label19.Location = new Point(0, 0);
            label19.Margin = new Padding(0);
            label19.Name = "label19";
            label19.Size = new Size(146, 25);
            label19.TabIndex = 36;
            label19.Text = "Tensão";
            label19.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            label20.BackColor = Color.FromArgb(31, 45, 56);
            label20.BorderStyle = BorderStyle.FixedSingle;
            label20.FlatStyle = FlatStyle.Popup;
            label20.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label20.Location = new Point(0, 25);
            label20.Margin = new Padding(0);
            label20.Name = "label20";
            label20.Size = new Size(146, 25);
            label20.TabIndex = 37;
            label20.Text = "Corrente";
            label20.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.AutoSize = true;
            tableLayoutPanel6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel6.Controls.Add(CbGenMeasures, 0, 0);
            tableLayoutPanel6.Controls.Add(BtnAbcOr012, 1, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(0, 6);
            tableLayoutPanel6.Margin = new Padding(0);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Size = new Size(281, 32);
            tableLayoutPanel6.TabIndex = 5;
            // 
            // CbGenMeasures
            // 
            CbGenMeasures.BackColor = Color.FromArgb(31, 45, 56);
            CbGenMeasures.Dock = DockStyle.Fill;
            CbGenMeasures.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            CbGenMeasures.ForeColor = Color.Lavender;
            CbGenMeasures.FormattingEnabled = true;
            CbGenMeasures.Items.AddRange(new object[] { "RMS", "Componente Simétrica", "Fundamental", "2º Harmônico", "3º Harmônico", "4º Harmônico", "5º Harmônico", "6º Harmônico", "7º Harmônico", "8º Harmônico", "9º Harmônico", "10º Harmônico", "11º Harmônico", "12º Harmônico", "13º Harmônico", "14º Harmônico", "15º Harmônico", "16º Harmônico", "17º Harmônico", "18º Harmônico", "19º Harmônico", "20º Harmônico", "21º Harmônico", "22º Harmônico", "23º Harmônico", "24º Harmônico" });
            CbGenMeasures.Location = new Point(5, 0);
            CbGenMeasures.Margin = new Padding(5, 0, 5, 0);
            CbGenMeasures.Name = "CbGenMeasures";
            CbGenMeasures.Size = new Size(214, 29);
            CbGenMeasures.TabIndex = 87;
            CbGenMeasures.SelectedIndexChanged += CbGenMeasures_SelectedIndexChanged;
            // 
            // BtnAbcOr012
            // 
            BtnAbcOr012.AutoSize = true;
            BtnAbcOr012.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnAbcOr012.Dock = DockStyle.Fill;
            BtnAbcOr012.FlatStyle = FlatStyle.Popup;
            BtnAbcOr012.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnAbcOr012.Location = new Point(227, 3);
            BtnAbcOr012.Name = "BtnAbcOr012";
            BtnAbcOr012.Size = new Size(51, 26);
            BtnAbcOr012.TabIndex = 6;
            BtnAbcOr012.Text = "012";
            BtnAbcOr012.UseVisualStyleBackColor = true;
            BtnAbcOr012.Click += button1_Click;
            // 
            // MonitorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1068, 1100);
            Controls.Add(PnGeneral);
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
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            PnVt.ResumeLayout(false);
            PnVt.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            PnGeneral.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
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
        private Button BtnDel;
        private Button BtnSaveWaveForm;
        private Button BtnConfig;
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
        private Panel PnGeneral;
        private GroupBox groupBox2;
        private ComboBox CbGenMeasures;
        private Label label7;
        private Label label6;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox TbGenVaAng;
        private Label LbGenVa;
        private TextBox TbGenVaMod;
        private TextBox TbGenVbMod;
        private Label LbGenVb;
        private TextBox TbGenVbAng;
        private TextBox TbGenInMod;
        private Label LbGenIn;
        private Label LbGenVc;
        private Label LbGenVn;
        private Label LbGenIa;
        private Label LbGenIb;
        private Label LbGenIc;
        private TextBox TbGenVcMod;
        private TextBox TbGenVnMod;
        private TextBox TbGenIaMod;
        private TextBox TbGenIbMod;
        private TextBox TbGenIcMod;
        private TextBox TbGenVcAng;
        private TextBox TbGenVnAng;
        private TextBox TbGenIaAng;
        private TextBox TbGenIbAng;
        private TextBox TbGenInAng;
        private TextBox TbGenIcAng;
        private Label label5;
        private Label label17;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel panel5;
        private Label label18;
        private Label label19;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        private TextBox TbGenUnbalanceI;
        private TextBox TbGenUnbalanceV;
        private Label label20;
        private TableLayoutPanel tableLayoutPanel6;
        private Button BtnAbcOr012;
    }
}