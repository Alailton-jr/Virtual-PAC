namespace TestSet
{
    partial class TransientForm
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
            PnSv = new Panel();
            panel5 = new Panel();
            label10 = new Label();
            TPnSV = new TableLayoutPanel();
            BtnLoadFile = new Button();
            openFileDialog1 = new OpenFileDialog();
            PltMain = new OxyPlot.WindowsForms.PlotView();
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            ChkVn = new CheckBox();
            ChkVc = new CheckBox();
            ChkVb = new CheckBox();
            ChkVa = new CheckBox();
            ChkIn = new CheckBox();
            ChkIc = new CheckBox();
            ChkIb = new CheckBox();
            CbVn = new ComboBox();
            CbVc = new ComboBox();
            CbVb = new ComboBox();
            CbVa = new ComboBox();
            CbIn = new ComboBox();
            CbIc = new ComboBox();
            CbIb = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            CbIa = new ComboBox();
            ChkIa = new CheckBox();
            label9 = new Label();
            TbFileName = new TextBox();
            label11 = new Label();
            TbNDados = new TextBox();
            CbkPlotRMS = new CheckBox();
            BtnStart = new Button();
            CbxLoop = new CheckBox();
            PnSv.SuspendLayout();
            panel5.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // PnSv
            // 
            PnSv.Controls.Add(panel5);
            PnSv.Controls.Add(TPnSV);
            PnSv.Dock = DockStyle.Top;
            PnSv.Location = new Point(0, 0);
            PnSv.Name = "PnSv";
            PnSv.Size = new Size(1072, 91);
            PnSv.TabIndex = 11;
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
            // BtnLoadFile
            // 
            BtnLoadFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnLoadFile.FlatAppearance.BorderColor = Color.White;
            BtnLoadFile.FlatAppearance.BorderSize = 0;
            BtnLoadFile.FlatStyle = FlatStyle.Popup;
            BtnLoadFile.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnLoadFile.Location = new Point(858, 150);
            BtnLoadFile.Margin = new Padding(3, 2, 3, 2);
            BtnLoadFile.Name = "BtnLoadFile";
            BtnLoadFile.Size = new Size(202, 33);
            BtnLoadFile.TabIndex = 12;
            BtnLoadFile.Text = "Abrir Arquivo";
            BtnLoadFile.UseVisualStyleBackColor = true;
            BtnLoadFile.Click += BtnLoadFile_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // PltMain
            // 
            PltMain.Dock = DockStyle.Fill;
            PltMain.Location = new Point(10, 10);
            PltMain.Name = "PltMain";
            PltMain.PanCursor = Cursors.Hand;
            PltMain.Size = new Size(616, 309);
            PltMain.TabIndex = 13;
            PltMain.Text = "plotView1";
            PltMain.ZoomHorizontalCursor = Cursors.SizeWE;
            PltMain.ZoomRectangleCursor = Cursors.SizeNWSE;
            PltMain.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(31, 45, 56);
            panel1.Controls.Add(PltMain);
            panel1.Location = new Point(424, 217);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(10);
            panel1.Size = new Size(636, 329);
            panel1.TabIndex = 14;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(ChkVn, 2, 7);
            tableLayoutPanel1.Controls.Add(ChkVc, 2, 6);
            tableLayoutPanel1.Controls.Add(ChkVb, 2, 5);
            tableLayoutPanel1.Controls.Add(ChkVa, 2, 4);
            tableLayoutPanel1.Controls.Add(ChkIn, 2, 3);
            tableLayoutPanel1.Controls.Add(ChkIc, 2, 2);
            tableLayoutPanel1.Controls.Add(ChkIb, 2, 1);
            tableLayoutPanel1.Controls.Add(CbVn, 1, 7);
            tableLayoutPanel1.Controls.Add(CbVc, 1, 6);
            tableLayoutPanel1.Controls.Add(CbVb, 1, 5);
            tableLayoutPanel1.Controls.Add(CbVa, 1, 4);
            tableLayoutPanel1.Controls.Add(CbIn, 1, 3);
            tableLayoutPanel1.Controls.Add(CbIc, 1, 2);
            tableLayoutPanel1.Controls.Add(CbIb, 1, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(label5, 0, 4);
            tableLayoutPanel1.Controls.Add(label6, 0, 5);
            tableLayoutPanel1.Controls.Add(label7, 0, 6);
            tableLayoutPanel1.Controls.Add(label8, 0, 7);
            tableLayoutPanel1.Controls.Add(CbIa, 1, 0);
            tableLayoutPanel1.Controls.Add(ChkIa, 2, 0);
            tableLayoutPanel1.Location = new Point(34, 217);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(372, 329);
            tableLayoutPanel1.TabIndex = 15;
            // 
            // ChkVn
            // 
            ChkVn.AutoSize = true;
            ChkVn.Dock = DockStyle.Fill;
            ChkVn.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            ChkVn.Location = new Point(354, 291);
            ChkVn.Name = "ChkVn";
            ChkVn.Size = new Size(14, 34);
            ChkVn.TabIndex = 23;
            ChkVn.Text = "ChkVn";
            ChkVn.UseVisualStyleBackColor = true;
            ChkVn.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // ChkVc
            // 
            ChkVc.AutoSize = true;
            ChkVc.Dock = DockStyle.Fill;
            ChkVc.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            ChkVc.Location = new Point(354, 250);
            ChkVc.Name = "ChkVc";
            ChkVc.Size = new Size(14, 34);
            ChkVc.TabIndex = 22;
            ChkVc.Text = "ChkVc";
            ChkVc.UseVisualStyleBackColor = true;
            ChkVc.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // ChkVb
            // 
            ChkVb.AutoSize = true;
            ChkVb.Dock = DockStyle.Fill;
            ChkVb.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            ChkVb.Location = new Point(354, 209);
            ChkVb.Name = "ChkVb";
            ChkVb.Size = new Size(14, 34);
            ChkVb.TabIndex = 21;
            ChkVb.Text = "ChkVb";
            ChkVb.UseVisualStyleBackColor = true;
            ChkVb.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // ChkVa
            // 
            ChkVa.AutoSize = true;
            ChkVa.Dock = DockStyle.Fill;
            ChkVa.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            ChkVa.Location = new Point(354, 168);
            ChkVa.Name = "ChkVa";
            ChkVa.Size = new Size(14, 34);
            ChkVa.TabIndex = 20;
            ChkVa.UseVisualStyleBackColor = true;
            ChkVa.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // ChkIn
            // 
            ChkIn.AutoSize = true;
            ChkIn.Dock = DockStyle.Fill;
            ChkIn.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            ChkIn.Location = new Point(354, 127);
            ChkIn.Name = "ChkIn";
            ChkIn.Size = new Size(14, 34);
            ChkIn.TabIndex = 19;
            ChkIn.Text = "ChkIn";
            ChkIn.UseVisualStyleBackColor = true;
            ChkIn.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // ChkIc
            // 
            ChkIc.AutoSize = true;
            ChkIc.Dock = DockStyle.Fill;
            ChkIc.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            ChkIc.Location = new Point(354, 86);
            ChkIc.Name = "ChkIc";
            ChkIc.Size = new Size(14, 34);
            ChkIc.TabIndex = 18;
            ChkIc.UseVisualStyleBackColor = true;
            ChkIc.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // ChkIb
            // 
            ChkIb.AutoSize = true;
            ChkIb.Dock = DockStyle.Fill;
            ChkIb.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            ChkIb.Location = new Point(354, 45);
            ChkIb.Name = "ChkIb";
            ChkIb.Size = new Size(14, 34);
            ChkIb.TabIndex = 17;
            ChkIb.UseVisualStyleBackColor = true;
            ChkIb.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // CbVn
            // 
            CbVn.BackColor = Color.FromArgb(31, 45, 56);
            CbVn.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            CbVn.ForeColor = Color.Lavender;
            CbVn.FormattingEnabled = true;
            CbVn.Location = new Point(179, 291);
            CbVn.Name = "CbVn";
            CbVn.Size = new Size(165, 33);
            CbVn.TabIndex = 15;
            CbVn.SelectedIndexChanged += CbIa_SelectedIndexChanged;
            // 
            // CbVc
            // 
            CbVc.BackColor = Color.FromArgb(31, 45, 56);
            CbVc.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            CbVc.ForeColor = Color.Lavender;
            CbVc.FormattingEnabled = true;
            CbVc.Location = new Point(179, 250);
            CbVc.Name = "CbVc";
            CbVc.Size = new Size(165, 33);
            CbVc.TabIndex = 14;
            CbVc.SelectedIndexChanged += CbIa_SelectedIndexChanged;
            // 
            // CbVb
            // 
            CbVb.BackColor = Color.FromArgb(31, 45, 56);
            CbVb.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            CbVb.ForeColor = Color.Lavender;
            CbVb.FormattingEnabled = true;
            CbVb.Location = new Point(179, 209);
            CbVb.Name = "CbVb";
            CbVb.Size = new Size(165, 33);
            CbVb.TabIndex = 13;
            CbVb.SelectedIndexChanged += CbIa_SelectedIndexChanged;
            // 
            // CbVa
            // 
            CbVa.BackColor = Color.FromArgb(31, 45, 56);
            CbVa.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            CbVa.ForeColor = Color.Lavender;
            CbVa.FormattingEnabled = true;
            CbVa.Location = new Point(179, 168);
            CbVa.Name = "CbVa";
            CbVa.Size = new Size(165, 33);
            CbVa.TabIndex = 12;
            CbVa.SelectedIndexChanged += CbIa_SelectedIndexChanged;
            // 
            // CbIn
            // 
            CbIn.BackColor = Color.FromArgb(31, 45, 56);
            CbIn.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            CbIn.ForeColor = Color.Lavender;
            CbIn.FormattingEnabled = true;
            CbIn.Location = new Point(179, 127);
            CbIn.Name = "CbIn";
            CbIn.Size = new Size(165, 33);
            CbIn.TabIndex = 11;
            CbIn.SelectedIndexChanged += CbIa_SelectedIndexChanged;
            // 
            // CbIc
            // 
            CbIc.BackColor = Color.FromArgb(31, 45, 56);
            CbIc.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            CbIc.ForeColor = Color.Lavender;
            CbIc.FormattingEnabled = true;
            CbIc.Location = new Point(179, 86);
            CbIc.Name = "CbIc";
            CbIc.Size = new Size(165, 33);
            CbIc.TabIndex = 10;
            CbIc.SelectedIndexChanged += CbIa_SelectedIndexChanged;
            // 
            // CbIb
            // 
            CbIb.BackColor = Color.FromArgb(31, 45, 56);
            CbIb.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            CbIb.ForeColor = Color.Lavender;
            CbIb.FormattingEnabled = true;
            CbIb.Location = new Point(179, 45);
            CbIb.Name = "CbIb";
            CbIb.Size = new Size(165, 33);
            CbIb.TabIndex = 9;
            CbIb.SelectedIndexChanged += CbIa_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(4, 1);
            label1.Name = "label1";
            label1.Size = new Size(168, 40);
            label1.TabIndex = 0;
            label1.Text = "Current Ia";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(4, 42);
            label2.Name = "label2";
            label2.Size = new Size(168, 40);
            label2.TabIndex = 1;
            label2.Text = "Current Ib";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(4, 83);
            label3.Name = "label3";
            label3.Size = new Size(168, 40);
            label3.TabIndex = 2;
            label3.Text = "Current Ic";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(4, 124);
            label4.Name = "label4";
            label4.Size = new Size(168, 40);
            label4.TabIndex = 3;
            label4.Text = "Current In";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(4, 165);
            label5.Name = "label5";
            label5.Size = new Size(168, 40);
            label5.TabIndex = 4;
            label5.Text = "Voltage Va";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(4, 206);
            label6.Name = "label6";
            label6.Size = new Size(168, 40);
            label6.TabIndex = 5;
            label6.Text = "Voltage Vb";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(4, 247);
            label7.Name = "label7";
            label7.Size = new Size(168, 40);
            label7.TabIndex = 6;
            label7.Text = "Voltage Vc";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.Dock = DockStyle.Fill;
            label8.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(4, 288);
            label8.Name = "label8";
            label8.Size = new Size(168, 40);
            label8.TabIndex = 7;
            label8.Text = "Voltage Vn";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CbIa
            // 
            CbIa.BackColor = Color.FromArgb(31, 45, 56);
            CbIa.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            CbIa.ForeColor = Color.Lavender;
            CbIa.FormattingEnabled = true;
            CbIa.Location = new Point(179, 4);
            CbIa.Name = "CbIa";
            CbIa.Size = new Size(168, 33);
            CbIa.TabIndex = 8;
            CbIa.SelectedIndexChanged += CbIa_SelectedIndexChanged;
            // 
            // ChkIa
            // 
            ChkIa.AutoSize = true;
            ChkIa.Dock = DockStyle.Fill;
            ChkIa.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            ChkIa.Location = new Point(354, 4);
            ChkIa.Name = "ChkIa";
            ChkIa.Size = new Size(14, 34);
            ChkIa.TabIndex = 16;
            ChkIa.UseVisualStyleBackColor = true;
            ChkIa.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label9
            // 
            label9.BackColor = Color.FromArgb(31, 45, 56);
            label9.BorderStyle = BorderStyle.FixedSingle;
            label9.FlatStyle = FlatStyle.Popup;
            label9.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(36, 112);
            label9.Name = "label9";
            label9.Size = new Size(165, 31);
            label9.TabIndex = 16;
            label9.Text = "Nome do Arquivo";
            label9.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TbFileName
            // 
            TbFileName.BackColor = Color.FromArgb(31, 45, 56);
            TbFileName.BorderStyle = BorderStyle.FixedSingle;
            TbFileName.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            TbFileName.ForeColor = Color.Lavender;
            TbFileName.Location = new Point(201, 112);
            TbFileName.Name = "TbFileName";
            TbFileName.Size = new Size(859, 31);
            TbFileName.TabIndex = 17;
            // 
            // label11
            // 
            label11.BackColor = Color.FromArgb(31, 45, 56);
            label11.BorderStyle = BorderStyle.FixedSingle;
            label11.FlatStyle = FlatStyle.Popup;
            label11.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(36, 151);
            label11.Name = "label11";
            label11.Size = new Size(100, 31);
            label11.TabIndex = 18;
            label11.Text = "Nº Dados";
            label11.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TbNDados
            // 
            TbNDados.BackColor = Color.FromArgb(31, 45, 56);
            TbNDados.BorderStyle = BorderStyle.FixedSingle;
            TbNDados.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            TbNDados.ForeColor = Color.Lavender;
            TbNDados.Location = new Point(136, 151);
            TbNDados.Name = "TbNDados";
            TbNDados.Size = new Size(70, 31);
            TbNDados.TabIndex = 19;
            TbNDados.TextAlign = HorizontalAlignment.Center;
            // 
            // CbkPlotRMS
            // 
            CbkPlotRMS.AutoSize = true;
            CbkPlotRMS.BackColor = Color.FromArgb(31, 45, 56);
            CbkPlotRMS.FlatAppearance.BorderColor = Color.Lavender;
            CbkPlotRMS.FlatAppearance.BorderSize = 20;
            CbkPlotRMS.FlatStyle = FlatStyle.Flat;
            CbkPlotRMS.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            CbkPlotRMS.Location = new Point(213, 152);
            CbkPlotRMS.Name = "CbkPlotRMS";
            CbkPlotRMS.Padding = new Padding(5, 0, 0, 0);
            CbkPlotRMS.Size = new Size(106, 29);
            CbkPlotRMS.TabIndex = 22;
            CbkPlotRMS.Text = "Plot RMS";
            CbkPlotRMS.UseVisualStyleBackColor = false;
            CbkPlotRMS.CheckedChanged += CbkPlotRMS_CheckedChanged;
            // 
            // BtnStart
            // 
            BtnStart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnStart.FlatAppearance.BorderColor = Color.White;
            BtnStart.FlatAppearance.BorderSize = 0;
            BtnStart.FlatStyle = FlatStyle.Popup;
            BtnStart.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnStart.Location = new Point(435, 560);
            BtnStart.Margin = new Padding(3, 2, 3, 2);
            BtnStart.Name = "BtnStart";
            BtnStart.Size = new Size(202, 33);
            BtnStart.TabIndex = 23;
            BtnStart.Text = "Start Test";
            BtnStart.UseVisualStyleBackColor = true;
            BtnStart.Click += BtnStart_Click;
            // 
            // CbxLoop
            // 
            CbxLoop.AutoSize = true;
            CbxLoop.BackColor = Color.FromArgb(31, 45, 56);
            CbxLoop.FlatAppearance.BorderColor = Color.Lavender;
            CbxLoop.FlatAppearance.BorderSize = 20;
            CbxLoop.FlatStyle = FlatStyle.Flat;
            CbxLoop.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            CbxLoop.Location = new Point(327, 152);
            CbxLoop.Name = "CbxLoop";
            CbxLoop.Padding = new Padding(5, 0, 0, 0);
            CbxLoop.Size = new Size(136, 29);
            CbxLoop.TabIndex = 24;
            CbxLoop.Text = "Run on Loop";
            CbxLoop.UseVisualStyleBackColor = false;
            CbxLoop.CheckedChanged += CbxLoop_CheckedChanged;
            // 
            // TransientForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1072, 614);
            Controls.Add(CbxLoop);
            Controls.Add(BtnStart);
            Controls.Add(CbkPlotRMS);
            Controls.Add(TbNDados);
            Controls.Add(label11);
            Controls.Add(TbFileName);
            Controls.Add(label9);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel1);
            Controls.Add(BtnLoadFile);
            Controls.Add(PnSv);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Name = "TransientForm";
            Text = "TransientForm";
            PnSv.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel PnSv;
        private Panel panel5;
        private Label label10;
        private TableLayoutPanel TPnSV;
        private Button BtnLoadFile;
        private OpenFileDialog openFileDialog1;
        private OxyPlot.WindowsForms.PlotView PltMain;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private ComboBox CbVc;
        private ComboBox CbVb;
        private ComboBox CbVa;
        private ComboBox CbIn;
        private ComboBox CbIc;
        private ComboBox CbIb;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private ComboBox CbIa;
        private ComboBox CbVn;
        private Label label9;
        private TextBox TbFileName;
        private CheckBox ChkIa;
        private Label label11;
        private TextBox TbNDados;
        private CheckBox ChkVn;
        private CheckBox ChkVc;
        private CheckBox ChkVb;
        private CheckBox ChkVa;
        private CheckBox ChkIn;
        private CheckBox ChkIc;
        private CheckBox ChkIb;
        private CheckBox CbkPlotRMS;
        private Button BtnStart;
        private CheckBox CbxLoop;
    }
}