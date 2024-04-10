namespace TestSet
{
    partial class continuous
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
            groupBox1 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            panel3 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            CmsVoltage = new ContextMenuStrip(components);
            toolStripMenuItem10 = new ToolStripMenuItem();
            toolStripMenuItem12 = new ToolStripMenuItem();
            toolStripMenuItem13 = new ToolStripMenuItem();
            TbVaMod = new TextBox();
            TbVaAng = new TextBox();
            TbVbMod = new TextBox();
            TbVbAng = new TextBox();
            TbVcMod = new TextBox();
            TbVcAng = new TextBox();
            TbVnAng = new TextBox();
            TbVnMod = new TextBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            panel2 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            CmsCurrent = new ContextMenuStrip(components);
            toolStripMenuItem11 = new ToolStripMenuItem();
            positivaToolStripMenuItem = new ToolStripMenuItem();
            negativaToolStripMenuItem = new ToolStripMenuItem();
            TbIaMod = new TextBox();
            TbIaAng = new TextBox();
            TbIbMod = new TextBox();
            TbIbAng = new TextBox();
            TbIcMod = new TextBox();
            TbIcAng = new TextBox();
            TbInAng = new TextBox();
            TbInMod = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            panel4 = new Panel();
            groupBox2 = new GroupBox();
            DgvPub = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            panel5 = new Panel();
            groupBox3 = new GroupBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            BtnStart = new Button();
            BtnUpdate = new Button();
            timerPub = new System.Windows.Forms.Timer(components);
            PnSv = new Panel();
            panel6 = new Panel();
            label13 = new Label();
            TPnSV = new TableLayoutPanel();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            CmsVoltage.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            CmsCurrent.SuspendLayout();
            panel4.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvPub).BeginInit();
            groupBox3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            PnSv.SuspendLayout();
            panel6.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 91);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(9, 30, 9, 0);
            panel1.Size = new Size(441, 523);
            panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel3);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.Lavender;
            groupBox1.Location = new Point(9, 30);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(9, 8, 9, 8);
            groupBox1.Size = new Size(423, 453);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Medidas";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(panel3, 0, 1);
            tableLayoutPanel3.Controls.Add(panel2, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(9, 33);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(405, 412);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.Controls.Add(tableLayoutPanel2);
            panel3.Location = new Point(3, 208);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(14, 15, 14, 15);
            panel3.Size = new Size(399, 174);
            panel3.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 44F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 235F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 91F));
            tableLayoutPanel2.ContextMenuStrip = CmsVoltage;
            tableLayoutPanel2.Controls.Add(TbVaMod, 1, 1);
            tableLayoutPanel2.Controls.Add(TbVaAng, 2, 1);
            tableLayoutPanel2.Controls.Add(TbVbMod, 1, 2);
            tableLayoutPanel2.Controls.Add(TbVbAng, 2, 2);
            tableLayoutPanel2.Controls.Add(TbVcMod, 1, 3);
            tableLayoutPanel2.Controls.Add(TbVcAng, 2, 3);
            tableLayoutPanel2.Controls.Add(TbVnAng, 2, 5);
            tableLayoutPanel2.Controls.Add(TbVnMod, 1, 5);
            tableLayoutPanel2.Controls.Add(label7, 0, 1);
            tableLayoutPanel2.Controls.Add(label8, 0, 2);
            tableLayoutPanel2.Controls.Add(label9, 0, 3);
            tableLayoutPanel2.Controls.Add(label10, 0, 5);
            tableLayoutPanel2.Controls.Add(label11, 1, 0);
            tableLayoutPanel2.Controls.Add(label12, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(14, 15);
            tableLayoutPanel2.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 6;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tableLayoutPanel2.Size = new Size(371, 148);
            tableLayoutPanel2.TabIndex = 5;
            // 
            // CmsVoltage
            // 
            CmsVoltage.BackColor = Color.FromArgb(40, 58, 73);
            CmsVoltage.ImageScalingSize = new Size(20, 20);
            CmsVoltage.Items.AddRange(new ToolStripItem[] { toolStripMenuItem10, toolStripMenuItem12, toolStripMenuItem13 });
            CmsVoltage.Name = "CmsCurrent";
            CmsVoltage.RenderMode = ToolStripRenderMode.System;
            CmsVoltage.Size = new Size(131, 76);
            CmsVoltage.Text = "Rotação";
            // 
            // toolStripMenuItem10
            // 
            toolStripMenuItem10.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripMenuItem10.ForeColor = Color.Lavender;
            toolStripMenuItem10.Name = "toolStripMenuItem10";
            toolStripMenuItem10.Size = new Size(130, 24);
            toolStripMenuItem10.Text = "Normal";
            toolStripMenuItem10.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem12
            // 
            toolStripMenuItem12.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripMenuItem12.ForeColor = Color.Lavender;
            toolStripMenuItem12.Name = "toolStripMenuItem12";
            toolStripMenuItem12.Size = new Size(130, 24);
            toolStripMenuItem12.Text = "Positiva";
            toolStripMenuItem12.Click += toolStripMenuItem2_Click;
            // 
            // toolStripMenuItem13
            // 
            toolStripMenuItem13.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripMenuItem13.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripMenuItem13.ForeColor = Color.Lavender;
            toolStripMenuItem13.Name = "toolStripMenuItem13";
            toolStripMenuItem13.Size = new Size(130, 24);
            toolStripMenuItem13.Text = "Negativa";
            toolStripMenuItem13.Click += toolStripMenuItem3_Click;
            // 
            // TbVaMod
            // 
            TbVaMod.BackColor = Color.FromArgb(31, 45, 56);
            TbVaMod.BorderStyle = BorderStyle.None;
            TbVaMod.ContextMenuStrip = CmsVoltage;
            TbVaMod.Dock = DockStyle.Fill;
            TbVaMod.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbVaMod.ForeColor = Color.Lavender;
            TbVaMod.Location = new Point(49, 29);
            TbVaMod.Margin = new Padding(3, 2, 3, 2);
            TbVaMod.Name = "TbVaMod";
            TbVaMod.Size = new Size(229, 25);
            TbVaMod.TabIndex = 7;
            TbVaMod.TextAlign = HorizontalAlignment.Center;
            // 
            // TbVaAng
            // 
            TbVaAng.BackColor = Color.FromArgb(31, 45, 56);
            TbVaAng.BorderStyle = BorderStyle.None;
            TbVaAng.ContextMenuStrip = CmsVoltage;
            TbVaAng.Dock = DockStyle.Fill;
            TbVaAng.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbVaAng.ForeColor = Color.Lavender;
            TbVaAng.Location = new Point(285, 29);
            TbVaAng.Margin = new Padding(3, 2, 3, 2);
            TbVaAng.Name = "TbVaAng";
            TbVaAng.Size = new Size(85, 25);
            TbVaAng.TabIndex = 8;
            TbVaAng.TextAlign = HorizontalAlignment.Center;
            // 
            // TbVbMod
            // 
            TbVbMod.BackColor = Color.FromArgb(31, 45, 56);
            TbVbMod.BorderStyle = BorderStyle.None;
            TbVbMod.ContextMenuStrip = CmsVoltage;
            TbVbMod.Dock = DockStyle.Fill;
            TbVbMod.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbVbMod.ForeColor = Color.Lavender;
            TbVbMod.Location = new Point(49, 59);
            TbVbMod.Margin = new Padding(3, 2, 3, 2);
            TbVbMod.Name = "TbVbMod";
            TbVbMod.Size = new Size(229, 25);
            TbVbMod.TabIndex = 10;
            TbVbMod.TextAlign = HorizontalAlignment.Center;
            // 
            // TbVbAng
            // 
            TbVbAng.BackColor = Color.FromArgb(31, 45, 56);
            TbVbAng.BorderStyle = BorderStyle.None;
            TbVbAng.ContextMenuStrip = CmsVoltage;
            TbVbAng.Dock = DockStyle.Fill;
            TbVbAng.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbVbAng.ForeColor = Color.Lavender;
            TbVbAng.Location = new Point(285, 59);
            TbVbAng.Margin = new Padding(3, 2, 3, 2);
            TbVbAng.Name = "TbVbAng";
            TbVbAng.Size = new Size(85, 25);
            TbVbAng.TabIndex = 12;
            TbVbAng.TextAlign = HorizontalAlignment.Center;
            // 
            // TbVcMod
            // 
            TbVcMod.BackColor = Color.FromArgb(31, 45, 56);
            TbVcMod.BorderStyle = BorderStyle.None;
            TbVcMod.ContextMenuStrip = CmsVoltage;
            TbVcMod.Dock = DockStyle.Fill;
            TbVcMod.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbVcMod.ForeColor = Color.Lavender;
            TbVcMod.Location = new Point(49, 89);
            TbVcMod.Margin = new Padding(3, 2, 3, 2);
            TbVcMod.Name = "TbVcMod";
            TbVcMod.Size = new Size(229, 25);
            TbVcMod.TabIndex = 13;
            TbVcMod.TextAlign = HorizontalAlignment.Center;
            // 
            // TbVcAng
            // 
            TbVcAng.BackColor = Color.FromArgb(31, 45, 56);
            TbVcAng.BorderStyle = BorderStyle.None;
            TbVcAng.ContextMenuStrip = CmsVoltage;
            TbVcAng.Dock = DockStyle.Fill;
            TbVcAng.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbVcAng.ForeColor = Color.Lavender;
            TbVcAng.Location = new Point(285, 89);
            TbVcAng.Margin = new Padding(3, 2, 3, 2);
            TbVcAng.Name = "TbVcAng";
            TbVcAng.Size = new Size(85, 25);
            TbVcAng.TabIndex = 14;
            TbVcAng.TextAlign = HorizontalAlignment.Center;
            // 
            // TbVnAng
            // 
            TbVnAng.BackColor = Color.FromArgb(31, 45, 56);
            TbVnAng.BorderStyle = BorderStyle.None;
            TbVnAng.ContextMenuStrip = CmsVoltage;
            TbVnAng.Dock = DockStyle.Fill;
            TbVnAng.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbVnAng.ForeColor = Color.Lavender;
            TbVnAng.Location = new Point(285, 120);
            TbVnAng.Margin = new Padding(3, 2, 3, 2);
            TbVnAng.Name = "TbVnAng";
            TbVnAng.Size = new Size(85, 25);
            TbVnAng.TabIndex = 11;
            TbVnAng.TextAlign = HorizontalAlignment.Center;
            // 
            // TbVnMod
            // 
            TbVnMod.BackColor = Color.FromArgb(31, 45, 56);
            TbVnMod.BorderStyle = BorderStyle.None;
            TbVnMod.ContextMenuStrip = CmsVoltage;
            TbVnMod.Dock = DockStyle.Fill;
            TbVnMod.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbVnMod.ForeColor = Color.Lavender;
            TbVnMod.Location = new Point(49, 120);
            TbVnMod.Margin = new Padding(3, 2, 3, 2);
            TbVnMod.Name = "TbVnMod";
            TbVnMod.Size = new Size(229, 25);
            TbVnMod.TabIndex = 9;
            TbVnMod.TextAlign = HorizontalAlignment.Center;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = Color.Lavender;
            label7.Location = new Point(4, 27);
            label7.Name = "label7";
            label7.Size = new Size(38, 29);
            label7.TabIndex = 3;
            label7.Text = "Va";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Dock = DockStyle.Fill;
            label8.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = Color.Lavender;
            label8.Location = new Point(4, 57);
            label8.Name = "label8";
            label8.Size = new Size(38, 29);
            label8.TabIndex = 15;
            label8.Text = "Vb";
            label8.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Dock = DockStyle.Fill;
            label9.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = Color.Lavender;
            label9.Location = new Point(4, 87);
            label9.Name = "label9";
            label9.Size = new Size(38, 29);
            label9.TabIndex = 16;
            label9.Text = "Vc";
            label9.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Dock = DockStyle.Fill;
            label10.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label10.ForeColor = Color.Lavender;
            label10.Location = new Point(4, 118);
            label10.Name = "label10";
            label10.Size = new Size(38, 29);
            label10.TabIndex = 17;
            label10.Text = "Vn";
            label10.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Dock = DockStyle.Fill;
            label11.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = Color.Lavender;
            label11.Location = new Point(49, 1);
            label11.Name = "label11";
            label11.Size = new Size(229, 25);
            label11.TabIndex = 18;
            label11.Text = "Tensão [V]";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Dock = DockStyle.Fill;
            label12.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label12.ForeColor = Color.Lavender;
            label12.Location = new Point(285, 1);
            label12.Name = "label12";
            label12.Size = new Size(85, 25);
            label12.TabIndex = 19;
            label12.Text = "Ângulo";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(tableLayoutPanel1);
            panel2.Location = new Point(3, 2);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(14, 15, 14, 15);
            panel2.Size = new Size(399, 178);
            panel2.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackgroundImageLayout = ImageLayout.Stretch;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 241F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 91F));
            tableLayoutPanel1.ContextMenuStrip = CmsCurrent;
            tableLayoutPanel1.Controls.Add(TbIaMod, 1, 1);
            tableLayoutPanel1.Controls.Add(TbIaAng, 2, 1);
            tableLayoutPanel1.Controls.Add(TbIbMod, 1, 2);
            tableLayoutPanel1.Controls.Add(TbIbAng, 2, 2);
            tableLayoutPanel1.Controls.Add(TbIcMod, 1, 3);
            tableLayoutPanel1.Controls.Add(TbIcAng, 2, 3);
            tableLayoutPanel1.Controls.Add(TbInAng, 2, 5);
            tableLayoutPanel1.Controls.Add(TbInMod, 1, 5);
            tableLayoutPanel1.Controls.Add(label1, 0, 1);
            tableLayoutPanel1.Controls.Add(label2, 0, 2);
            tableLayoutPanel1.Controls.Add(label3, 0, 3);
            tableLayoutPanel1.Controls.Add(label4, 0, 5);
            tableLayoutPanel1.Controls.Add(label5, 1, 0);
            tableLayoutPanel1.Controls.Add(label6, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(14, 15);
            tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tableLayoutPanel1.Size = new Size(371, 148);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // CmsCurrent
            // 
            CmsCurrent.BackColor = Color.FromArgb(40, 58, 73);
            CmsCurrent.ImageScalingSize = new Size(20, 20);
            CmsCurrent.Items.AddRange(new ToolStripItem[] { toolStripMenuItem11, positivaToolStripMenuItem, negativaToolStripMenuItem });
            CmsCurrent.Name = "CmsCurrent";
            CmsCurrent.RenderMode = ToolStripRenderMode.System;
            CmsCurrent.Size = new Size(131, 76);
            CmsCurrent.Text = "Rotação";
            // 
            // toolStripMenuItem11
            // 
            toolStripMenuItem11.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripMenuItem11.ForeColor = Color.Lavender;
            toolStripMenuItem11.Name = "toolStripMenuItem11";
            toolStripMenuItem11.Size = new Size(130, 24);
            toolStripMenuItem11.Text = "Normal";
            toolStripMenuItem11.Click += normalToolStripMenuItem_Click;
            // 
            // positivaToolStripMenuItem
            // 
            positivaToolStripMenuItem.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            positivaToolStripMenuItem.ForeColor = Color.Lavender;
            positivaToolStripMenuItem.Name = "positivaToolStripMenuItem";
            positivaToolStripMenuItem.Size = new Size(130, 24);
            positivaToolStripMenuItem.Text = "Positiva";
            positivaToolStripMenuItem.Click += RotPositiveoolStripMenuItem_Click;
            // 
            // negativaToolStripMenuItem
            // 
            negativaToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            negativaToolStripMenuItem.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            negativaToolStripMenuItem.ForeColor = Color.Lavender;
            negativaToolStripMenuItem.Name = "negativaToolStripMenuItem";
            negativaToolStripMenuItem.Size = new Size(130, 24);
            negativaToolStripMenuItem.Text = "Negativa";
            negativaToolStripMenuItem.Click += RotNegToolStripMenuItem_Click;
            // 
            // TbIaMod
            // 
            TbIaMod.BackColor = Color.FromArgb(31, 45, 56);
            TbIaMod.BorderStyle = BorderStyle.None;
            TbIaMod.ContextMenuStrip = CmsCurrent;
            TbIaMod.Dock = DockStyle.Fill;
            TbIaMod.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbIaMod.ForeColor = Color.Lavender;
            TbIaMod.Location = new Point(43, 29);
            TbIaMod.Margin = new Padding(3, 2, 3, 2);
            TbIaMod.Name = "TbIaMod";
            TbIaMod.Size = new Size(235, 25);
            TbIaMod.TabIndex = 7;
            TbIaMod.TextAlign = HorizontalAlignment.Center;
            // 
            // TbIaAng
            // 
            TbIaAng.BackColor = Color.FromArgb(31, 45, 56);
            TbIaAng.BorderStyle = BorderStyle.None;
            TbIaAng.ContextMenuStrip = CmsCurrent;
            TbIaAng.Dock = DockStyle.Fill;
            TbIaAng.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbIaAng.ForeColor = Color.Lavender;
            TbIaAng.Location = new Point(285, 29);
            TbIaAng.Margin = new Padding(3, 2, 3, 2);
            TbIaAng.Name = "TbIaAng";
            TbIaAng.Size = new Size(85, 25);
            TbIaAng.TabIndex = 8;
            TbIaAng.TextAlign = HorizontalAlignment.Center;
            // 
            // TbIbMod
            // 
            TbIbMod.BackColor = Color.FromArgb(31, 45, 56);
            TbIbMod.BorderStyle = BorderStyle.None;
            TbIbMod.ContextMenuStrip = CmsCurrent;
            TbIbMod.Dock = DockStyle.Fill;
            TbIbMod.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbIbMod.ForeColor = Color.Lavender;
            TbIbMod.Location = new Point(43, 59);
            TbIbMod.Margin = new Padding(3, 2, 3, 2);
            TbIbMod.Name = "TbIbMod";
            TbIbMod.Size = new Size(235, 25);
            TbIbMod.TabIndex = 10;
            TbIbMod.TextAlign = HorizontalAlignment.Center;
            // 
            // TbIbAng
            // 
            TbIbAng.BackColor = Color.FromArgb(31, 45, 56);
            TbIbAng.BorderStyle = BorderStyle.None;
            TbIbAng.ContextMenuStrip = CmsCurrent;
            TbIbAng.Dock = DockStyle.Fill;
            TbIbAng.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbIbAng.ForeColor = Color.Lavender;
            TbIbAng.Location = new Point(285, 59);
            TbIbAng.Margin = new Padding(3, 2, 3, 2);
            TbIbAng.Name = "TbIbAng";
            TbIbAng.Size = new Size(85, 25);
            TbIbAng.TabIndex = 12;
            TbIbAng.TextAlign = HorizontalAlignment.Center;
            // 
            // TbIcMod
            // 
            TbIcMod.BackColor = Color.FromArgb(31, 45, 56);
            TbIcMod.BorderStyle = BorderStyle.None;
            TbIcMod.ContextMenuStrip = CmsCurrent;
            TbIcMod.Dock = DockStyle.Fill;
            TbIcMod.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbIcMod.ForeColor = Color.Lavender;
            TbIcMod.Location = new Point(43, 89);
            TbIcMod.Margin = new Padding(3, 2, 3, 2);
            TbIcMod.Name = "TbIcMod";
            TbIcMod.Size = new Size(235, 25);
            TbIcMod.TabIndex = 13;
            TbIcMod.TextAlign = HorizontalAlignment.Center;
            // 
            // TbIcAng
            // 
            TbIcAng.BackColor = Color.FromArgb(31, 45, 56);
            TbIcAng.BorderStyle = BorderStyle.None;
            TbIcAng.ContextMenuStrip = CmsCurrent;
            TbIcAng.Dock = DockStyle.Fill;
            TbIcAng.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbIcAng.ForeColor = Color.Lavender;
            TbIcAng.Location = new Point(285, 89);
            TbIcAng.Margin = new Padding(3, 2, 3, 2);
            TbIcAng.Name = "TbIcAng";
            TbIcAng.Size = new Size(85, 25);
            TbIcAng.TabIndex = 14;
            TbIcAng.TextAlign = HorizontalAlignment.Center;
            // 
            // TbInAng
            // 
            TbInAng.BackColor = Color.FromArgb(31, 45, 56);
            TbInAng.BorderStyle = BorderStyle.None;
            TbInAng.ContextMenuStrip = CmsCurrent;
            TbInAng.Dock = DockStyle.Fill;
            TbInAng.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbInAng.ForeColor = Color.Lavender;
            TbInAng.Location = new Point(285, 120);
            TbInAng.Margin = new Padding(3, 2, 3, 2);
            TbInAng.Name = "TbInAng";
            TbInAng.Size = new Size(85, 25);
            TbInAng.TabIndex = 11;
            TbInAng.TextAlign = HorizontalAlignment.Center;
            // 
            // TbInMod
            // 
            TbInMod.BackColor = Color.FromArgb(31, 45, 56);
            TbInMod.BorderStyle = BorderStyle.None;
            TbInMod.ContextMenuStrip = CmsCurrent;
            TbInMod.Dock = DockStyle.Fill;
            TbInMod.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbInMod.ForeColor = Color.Lavender;
            TbInMod.Location = new Point(43, 120);
            TbInMod.Margin = new Padding(3, 2, 3, 2);
            TbInMod.Name = "TbInMod";
            TbInMod.Size = new Size(235, 25);
            TbInMod.TabIndex = 9;
            TbInMod.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Lavender;
            label1.Location = new Point(4, 27);
            label1.Name = "label1";
            label1.Size = new Size(32, 29);
            label1.TabIndex = 3;
            label1.Text = "Ia";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Lavender;
            label2.Location = new Point(4, 57);
            label2.Name = "label2";
            label2.Size = new Size(32, 29);
            label2.TabIndex = 15;
            label2.Text = "Ib";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Lavender;
            label3.Location = new Point(4, 87);
            label3.Name = "label3";
            label3.Size = new Size(32, 29);
            label3.TabIndex = 16;
            label3.Text = "Ic";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.Lavender;
            label4.Location = new Point(4, 118);
            label4.Name = "label4";
            label4.Size = new Size(32, 29);
            label4.TabIndex = 17;
            label4.Text = "In";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.Lavender;
            label5.Location = new Point(43, 1);
            label5.Name = "label5";
            label5.Size = new Size(235, 25);
            label5.TabIndex = 18;
            label5.Text = "Corrente [A]";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = Color.Lavender;
            label6.Location = new Point(285, 1);
            label6.Name = "label6";
            label6.Size = new Size(85, 25);
            label6.TabIndex = 19;
            label6.Text = "Ângulo";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            panel4.Controls.Add(groupBox2);
            panel4.Controls.Add(panel5);
            panel4.Controls.Add(groupBox3);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(441, 91);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(9, 30, 9, 0);
            panel4.Size = new Size(631, 523);
            panel4.TabIndex = 3;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(DgvPub);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox2.ForeColor = Color.Lavender;
            groupBox2.Location = new Point(9, 196);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(9, 8, 9, 8);
            groupBox2.Size = new Size(613, 287);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Publicando";
            // 
            // DgvPub
            // 
            DgvPub.BackgroundColor = Color.FromArgb(40, 58, 73);
            DgvPub.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvPub.Columns.AddRange(new DataGridViewColumn[] { Column1 });
            DgvPub.Dock = DockStyle.Top;
            DgvPub.Location = new Point(9, 33);
            DgvPub.Margin = new Padding(3, 2, 3, 2);
            DgvPub.Name = "DgvPub";
            DgvPub.RowHeadersWidth = 51;
            DgvPub.RowTemplate.Height = 29;
            DgvPub.Size = new Size(595, 226);
            DgvPub.TabIndex = 6;
            // 
            // Column1
            // 
            Column1.HeaderText = "Column1";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.Width = 125;
            // 
            // panel5
            // 
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(9, 152);
            panel5.Margin = new Padding(3, 2, 3, 2);
            panel5.Name = "panel5";
            panel5.Size = new Size(613, 44);
            panel5.TabIndex = 5;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(tableLayoutPanel4);
            groupBox3.Dock = DockStyle.Top;
            groupBox3.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox3.ForeColor = Color.Lavender;
            groupBox3.Location = new Point(9, 30);
            groupBox3.Margin = new Padding(9, 8, 9, 8);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 2, 3, 2);
            groupBox3.Size = new Size(613, 122);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Controle";
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel4.ColumnCount = 5;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28.6074F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 175F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.120945F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 175F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.2716522F));
            tableLayoutPanel4.Controls.Add(BtnStart, 1, 0);
            tableLayoutPanel4.Controls.Add(BtnUpdate, 3, 0);
            tableLayoutPanel4.Location = new Point(3, 50);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(607, 54);
            tableLayoutPanel4.TabIndex = 5;
            // 
            // BtnStart
            // 
            BtnStart.FlatAppearance.BorderSize = 0;
            BtnStart.FlatStyle = FlatStyle.Popup;
            BtnStart.Font = new Font("Segoe UI Emoji", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            BtnStart.ForeColor = Color.GhostWhite;
            BtnStart.Location = new Point(76, 2);
            BtnStart.Margin = new Padding(3, 2, 3, 2);
            BtnStart.Name = "BtnStart";
            BtnStart.Size = new Size(169, 39);
            BtnStart.TabIndex = 3;
            BtnStart.Text = "Iniciar";
            BtnStart.UseVisualStyleBackColor = true;
            BtnStart.Click += BtnStart_Click;
            // 
            // BtnUpdate
            // 
            BtnUpdate.FlatAppearance.BorderSize = 0;
            BtnUpdate.FlatStyle = FlatStyle.Popup;
            BtnUpdate.Font = new Font("Segoe UI Emoji", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            BtnUpdate.ForeColor = Color.GhostWhite;
            BtnUpdate.Location = new Point(348, 2);
            BtnUpdate.Margin = new Padding(3, 2, 3, 2);
            BtnUpdate.Name = "BtnUpdate";
            BtnUpdate.Size = new Size(169, 39);
            BtnUpdate.TabIndex = 4;
            BtnUpdate.Text = "Atualizar Valores";
            BtnUpdate.UseVisualStyleBackColor = true;
            BtnUpdate.Click += BtnUpdate_Click;
            // 
            // timerPub
            // 
            timerPub.Interval = 1500;
            // 
            // PnSv
            // 
            PnSv.Controls.Add(panel6);
            PnSv.Controls.Add(TPnSV);
            PnSv.Dock = DockStyle.Top;
            PnSv.Location = new Point(0, 0);
            PnSv.Name = "PnSv";
            PnSv.Size = new Size(1072, 91);
            PnSv.TabIndex = 11;
            // 
            // panel6
            // 
            panel6.Controls.Add(label13);
            panel6.Location = new Point(97, 12);
            panel6.Margin = new Padding(20, 3, 20, 3);
            panel6.Name = "panel6";
            panel6.Size = new Size(102, 61);
            panel6.TabIndex = 11;
            // 
            // label13
            // 
            label13.BackColor = Color.FromArgb(31, 45, 56);
            label13.Dock = DockStyle.Fill;
            label13.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
            label13.Location = new Point(0, 0);
            label13.Name = "label13";
            label13.Size = new Size(102, 61);
            label13.TabIndex = 10;
            label13.Text = "Sampled\r\nValue\r\nChannel\r\n";
            label13.TextAlign = ContentAlignment.MiddleCenter;
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
            // continuous
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1072, 614);
            Controls.Add(panel4);
            Controls.Add(panel1);
            Controls.Add(PnSv);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "continuous";
            Text = "continuous";
            FormClosing += continuous_FormClosing;
            Load += continuous_Load;
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            panel3.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            CmsVoltage.ResumeLayout(false);
            panel2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            CmsCurrent.ResumeLayout(false);
            panel4.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DgvPub).EndInit();
            groupBox3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            PnSv.ResumeLayout(false);
            panel6.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox TbIaMod;
        private TextBox TbIaAng;
        private TextBox TbIbMod;
        private TextBox TbIbAng;
        private TextBox TbIcMod;
        private TextBox TbIcAng;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label6;
        private TextBox TbInAng;
        private TextBox TbInMod;
        private Label label4;
        private Panel panel3;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox TbVaMod;
        private TextBox TbVaAng;
        private TextBox TbVbMod;
        private TextBox TbVbAng;
        private TextBox TbVcMod;
        private TextBox TbVcAng;
        private TextBox TbVnAng;
        private TextBox TbVnMod;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Panel panel4;
        private Panel panel5;
        private Button BtnUpdate;
        private Button BtnStart;
        private DataGridView DgvPub;
        private ContextMenuStrip MenuStripCurrent;
        private ToolStripMenuItem normalToolStripMenuItem;
        private ToolStripMenuItem rotaçãoPositivaToolStripMenuItem;
        private ToolStripMenuItem rotaçãoNegativaToolStripMenuItem;
        private ContextMenuStrip MenuStripVoltage;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ContextMenuStrip ContextMenuStripVoltage;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem toolStripMenuItem9;
        private ContextMenuStrip ContextMenuStripCurrent;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.Timer timerPub;
        private ContextMenuStrip CmsVoltage;
        private ToolStripMenuItem toolStripMenuItem10;
        private ToolStripMenuItem toolStripMenuItem12;
        private ToolStripMenuItem toolStripMenuItem13;
        private ContextMenuStrip CmsCurrent;
        private ToolStripMenuItem toolStripMenuItem11;
        private ToolStripMenuItem positivaToolStripMenuItem;
        private ToolStripMenuItem negativaToolStripMenuItem;
        private DataGridViewTextBoxColumn Column1;
        private Panel PnSv;
        private Panel panel6;
        private Label label13;
        private TableLayoutPanel TPnSV;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
    }
}