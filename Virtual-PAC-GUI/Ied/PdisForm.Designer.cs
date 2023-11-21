namespace Ied
{
    partial class PdisForm
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
            PnMain = new Panel();
            PnPlot = new Panel();
            tableLayoutPanel6 = new TableLayoutPanel();
            panel11 = new Panel();
            PltPlot = new OxyPlot.WindowsForms.PlotView();
            panel12 = new Panel();
            PnConfig = new Panel();
            Pn50pConfig = new TableLayoutPanel();
            panel8 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            TbDelayZ3 = new TextBox();
            label5 = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            TbDelayZ2 = new TextBox();
            label3 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            TbDelayZ1 = new TextBox();
            label4 = new Label();
            CbTypeZ3 = new ComboBox();
            CbTypeZ2 = new ComboBox();
            CbTypeZ1 = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            CbZoneEnabled3 = new CheckBox();
            CbZoneEnabled2 = new CheckBox();
            CbZoneEnabled1 = new CheckBox();
            Tlp50pPickupL3 = new TableLayoutPanel();
            TbAjusteZ3 = new TextBox();
            Lb50pCurrentL3 = new Label();
            label6 = new Label();
            label15 = new Label();
            Tlp50pPickupL1 = new TableLayoutPanel();
            TbAjusteZ1 = new TextBox();
            Lb50pCurrentL1 = new Label();
            Tlp50pTripL1 = new TableLayoutPanel();
            TbAngleZ1 = new TextBox();
            Lb50pTimeL1 = new Label();
            Tlp50pTripL2 = new TableLayoutPanel();
            TbAngleZ2 = new TextBox();
            Lb50pTimeL2 = new Label();
            Tlp50pPickupL2 = new TableLayoutPanel();
            TbAjusteZ2 = new TextBox();
            Lb50pCurrentL2 = new Label();
            Tlp50pTripL3 = new TableLayoutPanel();
            TbAngleZ3 = new TextBox();
            Lb50pTimeL3 = new Label();
            panel9 = new Panel();
            tableLayoutPanel5 = new TableLayoutPanel();
            groupBox4 = new GroupBox();
            Rb50pPu = new RadioButton();
            Rb50pAmpere = new RadioButton();
            groupBox5 = new GroupBox();
            Rb50pCycle = new RadioButton();
            Rb50pSeconds = new RadioButton();
            PnTitle = new Panel();
            LbTitle = new Label();
            PnMain.SuspendLayout();
            PnPlot.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            panel11.SuspendLayout();
            PnConfig.SuspendLayout();
            Pn50pConfig.SuspendLayout();
            panel8.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            Tlp50pPickupL3.SuspendLayout();
            Tlp50pPickupL1.SuspendLayout();
            Tlp50pTripL1.SuspendLayout();
            Tlp50pTripL2.SuspendLayout();
            Tlp50pPickupL2.SuspendLayout();
            Tlp50pTripL3.SuspendLayout();
            panel9.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            PnTitle.SuspendLayout();
            SuspendLayout();
            // 
            // PnMain
            // 
            PnMain.AutoScroll = true;
            PnMain.AutoSize = true;
            PnMain.Controls.Add(PnPlot);
            PnMain.Controls.Add(PnConfig);
            PnMain.Controls.Add(PnTitle);
            PnMain.Dock = DockStyle.Fill;
            PnMain.Location = new Point(0, 0);
            PnMain.Name = "PnMain";
            PnMain.Size = new Size(1060, 616);
            PnMain.TabIndex = 2;
            // 
            // PnPlot
            // 
            PnPlot.Controls.Add(tableLayoutPanel6);
            PnPlot.Dock = DockStyle.Fill;
            PnPlot.Location = new Point(0, 317);
            PnPlot.Margin = new Padding(3, 4, 3, 4);
            PnPlot.Name = "PnPlot";
            PnPlot.Padding = new Padding(15, 10, 23, 27);
            PnPlot.Size = new Size(1060, 299);
            PnPlot.TabIndex = 2;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.6888447F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 81.31116F));
            tableLayoutPanel6.Controls.Add(panel11, 1, 0);
            tableLayoutPanel6.Controls.Add(panel12, 0, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(15, 10);
            tableLayoutPanel6.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(1022, 262);
            tableLayoutPanel6.TabIndex = 2;
            // 
            // panel11
            // 
            panel11.Controls.Add(PltPlot);
            panel11.Dock = DockStyle.Fill;
            panel11.Location = new Point(194, 4);
            panel11.Margin = new Padding(3, 4, 3, 4);
            panel11.Name = "panel11";
            panel11.Size = new Size(825, 254);
            panel11.TabIndex = 2;
            // 
            // PltPlot
            // 
            PltPlot.Dock = DockStyle.Fill;
            PltPlot.Location = new Point(0, 0);
            PltPlot.Margin = new Padding(3, 4, 3, 4);
            PltPlot.Name = "PltPlot";
            PltPlot.PanCursor = Cursors.Hand;
            PltPlot.Size = new Size(825, 254);
            PltPlot.TabIndex = 0;
            PltPlot.Text = "plotView1";
            PltPlot.ZoomHorizontalCursor = Cursors.SizeWE;
            PltPlot.ZoomRectangleCursor = Cursors.SizeNWSE;
            PltPlot.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // panel12
            // 
            panel12.Dock = DockStyle.Fill;
            panel12.Location = new Point(3, 4);
            panel12.Margin = new Padding(3, 4, 3, 4);
            panel12.Name = "panel12";
            panel12.Size = new Size(185, 254);
            panel12.TabIndex = 3;
            // 
            // PnConfig
            // 
            PnConfig.Controls.Add(Pn50pConfig);
            PnConfig.Dock = DockStyle.Top;
            PnConfig.Location = new Point(0, 90);
            PnConfig.Margin = new Padding(3, 4, 3, 4);
            PnConfig.Name = "PnConfig";
            PnConfig.Size = new Size(1060, 227);
            PnConfig.TabIndex = 1;
            // 
            // Pn50pConfig
            // 
            Pn50pConfig.ColumnCount = 2;
            Pn50pConfig.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.8538008F));
            Pn50pConfig.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85.1462F));
            Pn50pConfig.Controls.Add(panel8, 1, 0);
            Pn50pConfig.Controls.Add(panel9, 0, 0);
            Pn50pConfig.Dock = DockStyle.Fill;
            Pn50pConfig.Location = new Point(0, 0);
            Pn50pConfig.Margin = new Padding(3, 4, 3, 4);
            Pn50pConfig.Name = "Pn50pConfig";
            Pn50pConfig.RowCount = 1;
            Pn50pConfig.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Pn50pConfig.RowStyles.Add(new RowStyle(SizeType.Absolute, 189F));
            Pn50pConfig.Size = new Size(1060, 227);
            Pn50pConfig.TabIndex = 10;
            // 
            // panel8
            // 
            panel8.Controls.Add(tableLayoutPanel1);
            panel8.Dock = DockStyle.Fill;
            panel8.Location = new Point(160, 4);
            panel8.Margin = new Padding(3, 4, 3, 4);
            panel8.Name = "panel8";
            panel8.Size = new Size(897, 219);
            panel8.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 137F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 3, 4);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 2, 4);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 4);
            tableLayoutPanel1.Controls.Add(CbTypeZ3, 3, 1);
            tableLayoutPanel1.Controls.Add(CbTypeZ2, 2, 1);
            tableLayoutPanel1.Controls.Add(CbTypeZ1, 1, 1);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 4);
            tableLayoutPanel1.Controls.Add(CbZoneEnabled3, 3, 0);
            tableLayoutPanel1.Controls.Add(CbZoneEnabled2, 2, 0);
            tableLayoutPanel1.Controls.Add(CbZoneEnabled1, 1, 0);
            tableLayoutPanel1.Controls.Add(Tlp50pPickupL3, 3, 2);
            tableLayoutPanel1.Controls.Add(label6, 0, 2);
            tableLayoutPanel1.Controls.Add(label15, 0, 3);
            tableLayoutPanel1.Controls.Add(Tlp50pPickupL1, 1, 2);
            tableLayoutPanel1.Controls.Add(Tlp50pTripL1, 1, 3);
            tableLayoutPanel1.Controls.Add(Tlp50pTripL2, 2, 3);
            tableLayoutPanel1.Controls.Add(Tlp50pPickupL2, 2, 2);
            tableLayoutPanel1.Controls.Add(Tlp50pTripL3, 4, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 41F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 47F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 12F));
            tableLayoutPanel1.Size = new Size(897, 219);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.7894745F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.2105274F));
            tableLayoutPanel4.Controls.Add(TbDelayZ3, 0, 0);
            tableLayoutPanel4.Controls.Add(label5, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(646, 175);
            tableLayoutPanel4.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(247, 39);
            tableLayoutPanel4.TabIndex = 22;
            // 
            // TbDelayZ3
            // 
            TbDelayZ3.BackColor = Color.FromArgb(31, 45, 56);
            TbDelayZ3.Dock = DockStyle.Fill;
            TbDelayZ3.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            TbDelayZ3.ForeColor = Color.Lavender;
            TbDelayZ3.Location = new Point(3, 4);
            TbDelayZ3.Margin = new Padding(3, 4, 3, 4);
            TbDelayZ3.Name = "TbDelayZ3";
            TbDelayZ3.Size = new Size(181, 32);
            TbDelayZ3.TabIndex = 0;
            TbDelayZ3.Text = "0";
            TbDelayZ3.TextAlign = HorizontalAlignment.Center;
            TbDelayZ3.Validated += TbAjusteZ1_Validated;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(190, 0);
            label5.MinimumSize = new Size(52, 0);
            label5.Name = "label5";
            label5.Size = new Size(54, 39);
            label5.TabIndex = 1;
            label5.Text = "[s]";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.7894745F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.2105274F));
            tableLayoutPanel3.Controls.Add(TbDelayZ2, 0, 0);
            tableLayoutPanel3.Controls.Add(label3, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(394, 175);
            tableLayoutPanel3.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(245, 39);
            tableLayoutPanel3.TabIndex = 22;
            // 
            // TbDelayZ2
            // 
            TbDelayZ2.BackColor = Color.FromArgb(31, 45, 56);
            TbDelayZ2.Dock = DockStyle.Fill;
            TbDelayZ2.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            TbDelayZ2.ForeColor = Color.Lavender;
            TbDelayZ2.Location = new Point(3, 4);
            TbDelayZ2.Margin = new Padding(3, 4, 3, 4);
            TbDelayZ2.Name = "TbDelayZ2";
            TbDelayZ2.Size = new Size(179, 32);
            TbDelayZ2.TabIndex = 0;
            TbDelayZ2.Text = "0";
            TbDelayZ2.TextAlign = HorizontalAlignment.Center;
            TbDelayZ2.Validated += TbAjusteZ1_Validated;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(188, 0);
            label3.MinimumSize = new Size(52, 0);
            label3.Name = "label3";
            label3.Size = new Size(54, 39);
            label3.TabIndex = 1;
            label3.Text = "[s]";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.7894745F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.2105274F));
            tableLayoutPanel2.Controls.Add(TbDelayZ1, 0, 0);
            tableLayoutPanel2.Controls.Add(label4, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(142, 175);
            tableLayoutPanel2.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(245, 39);
            tableLayoutPanel2.TabIndex = 21;
            // 
            // TbDelayZ1
            // 
            TbDelayZ1.BackColor = Color.FromArgb(31, 45, 56);
            TbDelayZ1.Dock = DockStyle.Fill;
            TbDelayZ1.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            TbDelayZ1.ForeColor = Color.Lavender;
            TbDelayZ1.Location = new Point(3, 4);
            TbDelayZ1.Margin = new Padding(3, 4, 3, 4);
            TbDelayZ1.Name = "TbDelayZ1";
            TbDelayZ1.Size = new Size(179, 32);
            TbDelayZ1.TabIndex = 0;
            TbDelayZ1.Text = "0";
            TbDelayZ1.TextAlign = HorizontalAlignment.Center;
            TbDelayZ1.Validated += TbAjusteZ1_Validated;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(188, 0);
            label4.MinimumSize = new Size(52, 0);
            label4.Name = "label4";
            label4.Size = new Size(54, 39);
            label4.TabIndex = 1;
            label4.Text = "[s]";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CbTypeZ3
            // 
            CbTypeZ3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CbTypeZ3.BackColor = Color.FromArgb(31, 45, 56);
            CbTypeZ3.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            CbTypeZ3.ForeColor = Color.Lavender;
            CbTypeZ3.FormattingEnabled = true;
            CbTypeZ3.Items.AddRange(new object[] { "Admitância", "Impedância", "Reatância" });
            CbTypeZ3.Location = new Point(646, 37);
            CbTypeZ3.Name = "CbTypeZ3";
            CbTypeZ3.Size = new Size(247, 35);
            CbTypeZ3.TabIndex = 30;
            CbTypeZ3.Text = "Reatância";
            CbTypeZ3.TextChanged += TbAjusteZ1_Validated;
            // 
            // CbTypeZ2
            // 
            CbTypeZ2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CbTypeZ2.BackColor = Color.FromArgb(31, 45, 56);
            CbTypeZ2.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            CbTypeZ2.ForeColor = Color.Lavender;
            CbTypeZ2.FormattingEnabled = true;
            CbTypeZ2.Items.AddRange(new object[] { "Admitância", "Impedância", "Reatância" });
            CbTypeZ2.Location = new Point(394, 37);
            CbTypeZ2.Name = "CbTypeZ2";
            CbTypeZ2.Size = new Size(245, 35);
            CbTypeZ2.TabIndex = 30;
            CbTypeZ2.Text = "Impedância";
            CbTypeZ2.TextChanged += TbAjusteZ1_Validated;
            // 
            // CbTypeZ1
            // 
            CbTypeZ1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CbTypeZ1.BackColor = Color.FromArgb(31, 45, 56);
            CbTypeZ1.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            CbTypeZ1.ForeColor = Color.Lavender;
            CbTypeZ1.FormattingEnabled = true;
            CbTypeZ1.Items.AddRange(new object[] { "Admitância", "Impedância", "Reatância" });
            CbTypeZ1.Location = new Point(142, 37);
            CbTypeZ1.Name = "CbTypeZ1";
            CbTypeZ1.Size = new Size(245, 35);
            CbTypeZ1.TabIndex = 29;
            CbTypeZ1.Text = "Adimitância";
            CbTypeZ1.TextChanged += TbAjusteZ1_Validated;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(4, 34);
            label2.Name = "label2";
            label2.Size = new Size(131, 41);
            label2.TabIndex = 30;
            label2.Text = "Tipo";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(4, 171);
            label1.Name = "label1";
            label1.Size = new Size(131, 47);
            label1.TabIndex = 26;
            label1.Text = "Delay";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CbZoneEnabled3
            // 
            CbZoneEnabled3.AutoSize = true;
            CbZoneEnabled3.Dock = DockStyle.Fill;
            CbZoneEnabled3.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 128);
            CbZoneEnabled3.Font = new Font("Trebuchet MS", 13F, FontStyle.Bold, GraphicsUnit.Point);
            CbZoneEnabled3.Location = new Point(646, 5);
            CbZoneEnabled3.Margin = new Padding(3, 4, 3, 4);
            CbZoneEnabled3.Name = "CbZoneEnabled3";
            CbZoneEnabled3.Size = new Size(247, 24);
            CbZoneEnabled3.TabIndex = 25;
            CbZoneEnabled3.Text = "Zona 3";
            CbZoneEnabled3.TextAlign = ContentAlignment.MiddleCenter;
            CbZoneEnabled3.UseVisualStyleBackColor = true;
            CbZoneEnabled3.CheckedChanged += TbAjusteZ1_Validated;
            // 
            // CbZoneEnabled2
            // 
            CbZoneEnabled2.AutoSize = true;
            CbZoneEnabled2.Dock = DockStyle.Fill;
            CbZoneEnabled2.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 128);
            CbZoneEnabled2.Font = new Font("Trebuchet MS", 13F, FontStyle.Bold, GraphicsUnit.Point);
            CbZoneEnabled2.Location = new Point(394, 5);
            CbZoneEnabled2.Margin = new Padding(3, 4, 3, 4);
            CbZoneEnabled2.Name = "CbZoneEnabled2";
            CbZoneEnabled2.Size = new Size(245, 24);
            CbZoneEnabled2.TabIndex = 24;
            CbZoneEnabled2.Text = "Zona 2";
            CbZoneEnabled2.TextAlign = ContentAlignment.MiddleCenter;
            CbZoneEnabled2.UseVisualStyleBackColor = true;
            CbZoneEnabled2.CheckedChanged += TbAjusteZ1_Validated;
            // 
            // CbZoneEnabled1
            // 
            CbZoneEnabled1.AutoSize = true;
            CbZoneEnabled1.Checked = true;
            CbZoneEnabled1.CheckState = CheckState.Checked;
            CbZoneEnabled1.Dock = DockStyle.Fill;
            CbZoneEnabled1.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 128);
            CbZoneEnabled1.Font = new Font("Trebuchet MS", 13F, FontStyle.Bold, GraphicsUnit.Point);
            CbZoneEnabled1.Location = new Point(142, 5);
            CbZoneEnabled1.Margin = new Padding(3, 4, 3, 4);
            CbZoneEnabled1.Name = "CbZoneEnabled1";
            CbZoneEnabled1.Size = new Size(245, 24);
            CbZoneEnabled1.TabIndex = 0;
            CbZoneEnabled1.Text = "Zona 1";
            CbZoneEnabled1.TextAlign = ContentAlignment.MiddleCenter;
            CbZoneEnabled1.UseVisualStyleBackColor = true;
            CbZoneEnabled1.CheckedChanged += TbAjusteZ1_Validated;
            // 
            // Tlp50pPickupL3
            // 
            Tlp50pPickupL3.ColumnCount = 2;
            Tlp50pPickupL3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.7894745F));
            Tlp50pPickupL3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.2105255F));
            Tlp50pPickupL3.Controls.Add(TbAjusteZ3, 0, 0);
            Tlp50pPickupL3.Controls.Add(Lb50pCurrentL3, 1, 0);
            Tlp50pPickupL3.Dock = DockStyle.Fill;
            Tlp50pPickupL3.Location = new Point(646, 80);
            Tlp50pPickupL3.Margin = new Padding(3, 4, 3, 4);
            Tlp50pPickupL3.Name = "Tlp50pPickupL3";
            Tlp50pPickupL3.RowCount = 1;
            Tlp50pPickupL3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Tlp50pPickupL3.Size = new Size(247, 38);
            Tlp50pPickupL3.TabIndex = 19;
            // 
            // TbAjusteZ3
            // 
            TbAjusteZ3.BackColor = Color.FromArgb(31, 45, 56);
            TbAjusteZ3.Dock = DockStyle.Fill;
            TbAjusteZ3.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            TbAjusteZ3.ForeColor = Color.Lavender;
            TbAjusteZ3.Location = new Point(3, 4);
            TbAjusteZ3.Margin = new Padding(3, 4, 3, 4);
            TbAjusteZ3.Name = "TbAjusteZ3";
            TbAjusteZ3.Size = new Size(181, 32);
            TbAjusteZ3.TabIndex = 0;
            TbAjusteZ3.Text = "0";
            TbAjusteZ3.TextAlign = HorizontalAlignment.Center;
            TbAjusteZ3.Validated += TbAjusteZ1_Validated;
            // 
            // Lb50pCurrentL3
            // 
            Lb50pCurrentL3.AutoSize = true;
            Lb50pCurrentL3.Dock = DockStyle.Fill;
            Lb50pCurrentL3.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Lb50pCurrentL3.Location = new Point(190, 0);
            Lb50pCurrentL3.MinimumSize = new Size(52, 0);
            Lb50pCurrentL3.Name = "Lb50pCurrentL3";
            Lb50pCurrentL3.Size = new Size(54, 38);
            Lb50pCurrentL3.TabIndex = 1;
            Lb50pCurrentL3.Text = "[Ω]";
            Lb50pCurrentL3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(4, 76);
            label6.Name = "label6";
            label6.Size = new Size(131, 46);
            label6.TabIndex = 8;
            label6.Text = "Impedância Ajuste";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Dock = DockStyle.Fill;
            label15.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label15.Location = new Point(4, 123);
            label15.Name = "label15";
            label15.Size = new Size(131, 47);
            label15.TabIndex = 9;
            label15.Text = "Angulo";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Tlp50pPickupL1
            // 
            Tlp50pPickupL1.ColumnCount = 2;
            Tlp50pPickupL1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.7894745F));
            Tlp50pPickupL1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.2105255F));
            Tlp50pPickupL1.Controls.Add(TbAjusteZ1, 0, 0);
            Tlp50pPickupL1.Controls.Add(Lb50pCurrentL1, 1, 0);
            Tlp50pPickupL1.Dock = DockStyle.Fill;
            Tlp50pPickupL1.Location = new Point(142, 80);
            Tlp50pPickupL1.Margin = new Padding(3, 4, 3, 4);
            Tlp50pPickupL1.Name = "Tlp50pPickupL1";
            Tlp50pPickupL1.RowCount = 1;
            Tlp50pPickupL1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Tlp50pPickupL1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Tlp50pPickupL1.Size = new Size(245, 38);
            Tlp50pPickupL1.TabIndex = 18;
            // 
            // TbAjusteZ1
            // 
            TbAjusteZ1.BackColor = Color.FromArgb(31, 45, 56);
            TbAjusteZ1.Dock = DockStyle.Fill;
            TbAjusteZ1.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            TbAjusteZ1.ForeColor = Color.Lavender;
            TbAjusteZ1.Location = new Point(3, 4);
            TbAjusteZ1.Margin = new Padding(3, 4, 3, 4);
            TbAjusteZ1.Name = "TbAjusteZ1";
            TbAjusteZ1.Size = new Size(179, 32);
            TbAjusteZ1.TabIndex = 0;
            TbAjusteZ1.Text = "0";
            TbAjusteZ1.TextAlign = HorizontalAlignment.Center;
            TbAjusteZ1.Validated += TbAjusteZ1_Validated;
            // 
            // Lb50pCurrentL1
            // 
            Lb50pCurrentL1.AutoSize = true;
            Lb50pCurrentL1.Dock = DockStyle.Fill;
            Lb50pCurrentL1.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Lb50pCurrentL1.Location = new Point(188, 0);
            Lb50pCurrentL1.MinimumSize = new Size(52, 0);
            Lb50pCurrentL1.Name = "Lb50pCurrentL1";
            Lb50pCurrentL1.Size = new Size(54, 38);
            Lb50pCurrentL1.TabIndex = 1;
            Lb50pCurrentL1.Text = "[Ω]";
            Lb50pCurrentL1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Tlp50pTripL1
            // 
            Tlp50pTripL1.ColumnCount = 2;
            Tlp50pTripL1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.7894745F));
            Tlp50pTripL1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.2105274F));
            Tlp50pTripL1.Controls.Add(TbAngleZ1, 0, 0);
            Tlp50pTripL1.Controls.Add(Lb50pTimeL1, 1, 0);
            Tlp50pTripL1.Dock = DockStyle.Fill;
            Tlp50pTripL1.Location = new Point(142, 127);
            Tlp50pTripL1.Margin = new Padding(3, 4, 3, 4);
            Tlp50pTripL1.Name = "Tlp50pTripL1";
            Tlp50pTripL1.RowCount = 1;
            Tlp50pTripL1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Tlp50pTripL1.Size = new Size(245, 39);
            Tlp50pTripL1.TabIndex = 20;
            // 
            // TbAngleZ1
            // 
            TbAngleZ1.BackColor = Color.FromArgb(31, 45, 56);
            TbAngleZ1.Dock = DockStyle.Fill;
            TbAngleZ1.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            TbAngleZ1.ForeColor = Color.Lavender;
            TbAngleZ1.Location = new Point(3, 4);
            TbAngleZ1.Margin = new Padding(3, 4, 3, 4);
            TbAngleZ1.Name = "TbAngleZ1";
            TbAngleZ1.Size = new Size(179, 32);
            TbAngleZ1.TabIndex = 0;
            TbAngleZ1.Text = "0";
            TbAngleZ1.TextAlign = HorizontalAlignment.Center;
            TbAngleZ1.Validated += TbAjusteZ1_Validated;
            // 
            // Lb50pTimeL1
            // 
            Lb50pTimeL1.AutoSize = true;
            Lb50pTimeL1.Dock = DockStyle.Fill;
            Lb50pTimeL1.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Lb50pTimeL1.Location = new Point(188, 0);
            Lb50pTimeL1.MinimumSize = new Size(52, 0);
            Lb50pTimeL1.Name = "Lb50pTimeL1";
            Lb50pTimeL1.Size = new Size(54, 39);
            Lb50pTimeL1.TabIndex = 1;
            Lb50pTimeL1.Text = "[°]";
            Lb50pTimeL1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Tlp50pTripL2
            // 
            Tlp50pTripL2.ColumnCount = 2;
            Tlp50pTripL2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.7894745F));
            Tlp50pTripL2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.2105255F));
            Tlp50pTripL2.Controls.Add(TbAngleZ2, 0, 0);
            Tlp50pTripL2.Controls.Add(Lb50pTimeL2, 1, 0);
            Tlp50pTripL2.Dock = DockStyle.Fill;
            Tlp50pTripL2.Location = new Point(394, 127);
            Tlp50pTripL2.Margin = new Padding(3, 4, 3, 4);
            Tlp50pTripL2.Name = "Tlp50pTripL2";
            Tlp50pTripL2.RowCount = 1;
            Tlp50pTripL2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Tlp50pTripL2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Tlp50pTripL2.Size = new Size(245, 39);
            Tlp50pTripL2.TabIndex = 21;
            // 
            // TbAngleZ2
            // 
            TbAngleZ2.BackColor = Color.FromArgb(31, 45, 56);
            TbAngleZ2.Dock = DockStyle.Fill;
            TbAngleZ2.Enabled = false;
            TbAngleZ2.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            TbAngleZ2.ForeColor = Color.Lavender;
            TbAngleZ2.Location = new Point(3, 4);
            TbAngleZ2.Margin = new Padding(3, 4, 3, 4);
            TbAngleZ2.Name = "TbAngleZ2";
            TbAngleZ2.Size = new Size(179, 32);
            TbAngleZ2.TabIndex = 0;
            TbAngleZ2.Text = "0";
            TbAngleZ2.TextAlign = HorizontalAlignment.Center;
            TbAngleZ2.Validated += TbAjusteZ1_Validated;
            // 
            // Lb50pTimeL2
            // 
            Lb50pTimeL2.AutoSize = true;
            Lb50pTimeL2.Dock = DockStyle.Fill;
            Lb50pTimeL2.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Lb50pTimeL2.Location = new Point(188, 0);
            Lb50pTimeL2.MinimumSize = new Size(52, 0);
            Lb50pTimeL2.Name = "Lb50pTimeL2";
            Lb50pTimeL2.Size = new Size(54, 39);
            Lb50pTimeL2.TabIndex = 1;
            Lb50pTimeL2.Text = "[°]";
            Lb50pTimeL2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Tlp50pPickupL2
            // 
            Tlp50pPickupL2.ColumnCount = 2;
            Tlp50pPickupL2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.7894745F));
            Tlp50pPickupL2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.2105255F));
            Tlp50pPickupL2.Controls.Add(TbAjusteZ2, 0, 0);
            Tlp50pPickupL2.Controls.Add(Lb50pCurrentL2, 1, 0);
            Tlp50pPickupL2.Dock = DockStyle.Fill;
            Tlp50pPickupL2.Location = new Point(394, 80);
            Tlp50pPickupL2.Margin = new Padding(3, 4, 3, 4);
            Tlp50pPickupL2.Name = "Tlp50pPickupL2";
            Tlp50pPickupL2.RowCount = 1;
            Tlp50pPickupL2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Tlp50pPickupL2.Size = new Size(245, 38);
            Tlp50pPickupL2.TabIndex = 22;
            // 
            // TbAjusteZ2
            // 
            TbAjusteZ2.BackColor = Color.FromArgb(31, 45, 56);
            TbAjusteZ2.Dock = DockStyle.Fill;
            TbAjusteZ2.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            TbAjusteZ2.ForeColor = Color.Lavender;
            TbAjusteZ2.Location = new Point(3, 4);
            TbAjusteZ2.Margin = new Padding(3, 4, 3, 4);
            TbAjusteZ2.Name = "TbAjusteZ2";
            TbAjusteZ2.Size = new Size(179, 32);
            TbAjusteZ2.TabIndex = 0;
            TbAjusteZ2.Text = "0";
            TbAjusteZ2.TextAlign = HorizontalAlignment.Center;
            TbAjusteZ2.Validated += TbAjusteZ1_Validated;
            // 
            // Lb50pCurrentL2
            // 
            Lb50pCurrentL2.AutoSize = true;
            Lb50pCurrentL2.Dock = DockStyle.Fill;
            Lb50pCurrentL2.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Lb50pCurrentL2.Location = new Point(188, 0);
            Lb50pCurrentL2.MinimumSize = new Size(52, 0);
            Lb50pCurrentL2.Name = "Lb50pCurrentL2";
            Lb50pCurrentL2.Size = new Size(54, 38);
            Lb50pCurrentL2.TabIndex = 1;
            Lb50pCurrentL2.Text = "[Ω]";
            Lb50pCurrentL2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Tlp50pTripL3
            // 
            Tlp50pTripL3.ColumnCount = 2;
            Tlp50pTripL3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.7894745F));
            Tlp50pTripL3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.2105255F));
            Tlp50pTripL3.Controls.Add(TbAngleZ3, 0, 0);
            Tlp50pTripL3.Controls.Add(Lb50pTimeL3, 1, 0);
            Tlp50pTripL3.Dock = DockStyle.Fill;
            Tlp50pTripL3.Location = new Point(646, 127);
            Tlp50pTripL3.Margin = new Padding(3, 4, 3, 4);
            Tlp50pTripL3.Name = "Tlp50pTripL3";
            Tlp50pTripL3.RowCount = 1;
            Tlp50pTripL3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Tlp50pTripL3.Size = new Size(247, 39);
            Tlp50pTripL3.TabIndex = 23;
            // 
            // TbAngleZ3
            // 
            TbAngleZ3.BackColor = Color.FromArgb(31, 45, 56);
            TbAngleZ3.Dock = DockStyle.Fill;
            TbAngleZ3.Enabled = false;
            TbAngleZ3.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            TbAngleZ3.ForeColor = Color.Lavender;
            TbAngleZ3.Location = new Point(3, 4);
            TbAngleZ3.Margin = new Padding(3, 4, 3, 4);
            TbAngleZ3.Name = "TbAngleZ3";
            TbAngleZ3.Size = new Size(181, 32);
            TbAngleZ3.TabIndex = 0;
            TbAngleZ3.Text = "0";
            TbAngleZ3.TextAlign = HorizontalAlignment.Center;
            TbAngleZ3.Validated += TbAjusteZ1_Validated;
            // 
            // Lb50pTimeL3
            // 
            Lb50pTimeL3.AutoSize = true;
            Lb50pTimeL3.Dock = DockStyle.Fill;
            Lb50pTimeL3.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Lb50pTimeL3.Location = new Point(190, 0);
            Lb50pTimeL3.MinimumSize = new Size(52, 0);
            Lb50pTimeL3.Name = "Lb50pTimeL3";
            Lb50pTimeL3.Size = new Size(54, 39);
            Lb50pTimeL3.TabIndex = 1;
            Lb50pTimeL3.Text = "[°]";
            Lb50pTimeL3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel9
            // 
            panel9.Controls.Add(tableLayoutPanel5);
            panel9.Dock = DockStyle.Fill;
            panel9.Location = new Point(3, 4);
            panel9.Margin = new Padding(3, 4, 3, 4);
            panel9.Name = "panel9";
            panel9.Size = new Size(151, 219);
            panel9.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Controls.Add(groupBox4, 0, 0);
            tableLayoutPanel5.Controls.Add(groupBox5, 0, 1);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(0, 0);
            tableLayoutPanel5.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(151, 219);
            tableLayoutPanel5.TabIndex = 11;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(Rb50pPu);
            groupBox4.Controls.Add(Rb50pAmpere);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Font = new Font("Trebuchet MS", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox4.ForeColor = Color.Lavender;
            groupBox4.Location = new Point(3, 4);
            groupBox4.Margin = new Padding(3, 4, 3, 4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(3, 4, 3, 4);
            groupBox4.Size = new Size(145, 101);
            groupBox4.TabIndex = 6;
            groupBox4.TabStop = false;
            groupBox4.Text = "Impedance";
            // 
            // Rb50pPu
            // 
            Rb50pPu.AutoSize = true;
            Rb50pPu.Font = new Font("Trebuchet MS", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Rb50pPu.Location = new Point(7, 55);
            Rb50pPu.Margin = new Padding(3, 4, 3, 4);
            Rb50pPu.Name = "Rb50pPu";
            Rb50pPu.Size = new Size(42, 22);
            Rb50pPu.TabIndex = 6;
            Rb50pPu.Text = "Pu";
            Rb50pPu.UseVisualStyleBackColor = true;
            // 
            // Rb50pAmpere
            // 
            Rb50pAmpere.AutoSize = true;
            Rb50pAmpere.Checked = true;
            Rb50pAmpere.Font = new Font("Trebuchet MS", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Rb50pAmpere.Location = new Point(7, 31);
            Rb50pAmpere.Margin = new Padding(3, 4, 3, 4);
            Rb50pAmpere.Name = "Rb50pAmpere";
            Rb50pAmpere.Size = new Size(54, 22);
            Rb50pAmpere.TabIndex = 5;
            Rb50pAmpere.TabStop = true;
            Rb50pAmpere.Text = "Ohm";
            Rb50pAmpere.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(Rb50pCycle);
            groupBox5.Controls.Add(Rb50pSeconds);
            groupBox5.Dock = DockStyle.Fill;
            groupBox5.Font = new Font("Trebuchet MS", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox5.ForeColor = Color.Lavender;
            groupBox5.Location = new Point(3, 113);
            groupBox5.Margin = new Padding(3, 4, 3, 4);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(3, 4, 3, 4);
            groupBox5.Size = new Size(145, 102);
            groupBox5.TabIndex = 9;
            groupBox5.TabStop = false;
            groupBox5.Text = "Time";
            // 
            // Rb50pCycle
            // 
            Rb50pCycle.AutoSize = true;
            Rb50pCycle.Font = new Font("Trebuchet MS", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Rb50pCycle.Location = new Point(7, 53);
            Rb50pCycle.Margin = new Padding(3, 4, 3, 4);
            Rb50pCycle.Name = "Rb50pCycle";
            Rb50pCycle.Size = new Size(59, 22);
            Rb50pCycle.TabIndex = 6;
            Rb50pCycle.Text = "Cycle";
            Rb50pCycle.UseVisualStyleBackColor = true;
            // 
            // Rb50pSeconds
            // 
            Rb50pSeconds.AutoSize = true;
            Rb50pSeconds.Checked = true;
            Rb50pSeconds.Font = new Font("Trebuchet MS", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Rb50pSeconds.Location = new Point(7, 30);
            Rb50pSeconds.Margin = new Padding(3, 4, 3, 4);
            Rb50pSeconds.Name = "Rb50pSeconds";
            Rb50pSeconds.Size = new Size(76, 22);
            Rb50pSeconds.TabIndex = 5;
            Rb50pSeconds.TabStop = true;
            Rb50pSeconds.Text = "Seconds";
            Rb50pSeconds.UseVisualStyleBackColor = true;
            // 
            // PnTitle
            // 
            PnTitle.Controls.Add(LbTitle);
            PnTitle.Dock = DockStyle.Top;
            PnTitle.Location = new Point(0, 0);
            PnTitle.Margin = new Padding(3, 4, 3, 4);
            PnTitle.Name = "PnTitle";
            PnTitle.Size = new Size(1060, 90);
            PnTitle.TabIndex = 0;
            // 
            // LbTitle
            // 
            LbTitle.Dock = DockStyle.Fill;
            LbTitle.Font = new Font("Trebuchet MS", 17.25F, FontStyle.Regular, GraphicsUnit.Point);
            LbTitle.Location = new Point(0, 0);
            LbTitle.Name = "LbTitle";
            LbTitle.Size = new Size(1060, 90);
            LbTitle.TabIndex = 2;
            LbTitle.Text = "Proteção de Distância";
            LbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PdisForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1060, 616);
            Controls.Add(PnMain);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Name = "PdisForm";
            Text = "PdisForm";
            Load += PdisForm_Load;
            PnMain.ResumeLayout(false);
            PnPlot.ResumeLayout(false);
            tableLayoutPanel6.ResumeLayout(false);
            panel11.ResumeLayout(false);
            PnConfig.ResumeLayout(false);
            Pn50pConfig.ResumeLayout(false);
            panel8.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            Tlp50pPickupL3.ResumeLayout(false);
            Tlp50pPickupL3.PerformLayout();
            Tlp50pPickupL1.ResumeLayout(false);
            Tlp50pPickupL1.PerformLayout();
            Tlp50pTripL1.ResumeLayout(false);
            Tlp50pTripL1.PerformLayout();
            Tlp50pTripL2.ResumeLayout(false);
            Tlp50pTripL2.PerformLayout();
            Tlp50pPickupL2.ResumeLayout(false);
            Tlp50pPickupL2.PerformLayout();
            Tlp50pTripL3.ResumeLayout(false);
            Tlp50pTripL3.PerformLayout();
            panel9.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            PnTitle.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel PnMain;
        private Panel PnPlot;
        private Panel PnConfig;
        private TableLayoutPanel Pn50pConfig;
        private Panel panel8;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private CheckBox CbZoneEnabled3;
        private CheckBox CbZoneEnabled2;
        private CheckBox CbZoneEnabled1;
        private TableLayoutPanel Tlp50pPickupL3;
        private TextBox TbAjusteZ3;
        private Label Lb50pCurrentL3;
        private Label label6;
        private Label label15;
        private TableLayoutPanel Tlp50pPickupL1;
        private TextBox TbAjusteZ1;
        private Label Lb50pCurrentL1;
        private TableLayoutPanel Tlp50pTripL1;
        private TextBox TbAngleZ1;
        private Label Lb50pTimeL1;
        private TableLayoutPanel Tlp50pTripL2;
        private TextBox TbAngleZ2;
        private Label Lb50pTimeL2;
        private TableLayoutPanel Tlp50pPickupL2;
        private TextBox TbAjusteZ2;
        private Label Lb50pCurrentL2;
        private TableLayoutPanel Tlp50pTripL3;
        private TextBox TbAngleZ3;
        private Label Lb50pTimeL3;
        private Panel panel9;
        private Panel PnTitle;
        private Label LbTitle;
        private Label label2;
        private ComboBox CbTypeZ3;
        private ComboBox CbTypeZ2;
        private ComboBox CbTypeZ1;
        private TableLayoutPanel tableLayoutPanel4;
        private TextBox TbDelayZ3;
        private Label label5;
        private TableLayoutPanel tableLayoutPanel3;
        private TextBox TbDelayZ2;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox TbDelayZ1;
        private Label label4;
        private TableLayoutPanel tableLayoutPanel6;
        private Panel panel11;
        private OxyPlot.WindowsForms.PlotView PltPlot;
        private Panel panel12;
        private TableLayoutPanel tableLayoutPanel5;
        private GroupBox groupBox4;
        private RadioButton Rb50pPu;
        private RadioButton Rb50pAmpere;
        private GroupBox groupBox5;
        private RadioButton Rb50pCycle;
        private RadioButton Rb50pSeconds;
    }
}