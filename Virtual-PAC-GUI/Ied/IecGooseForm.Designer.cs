namespace Ied
{
    partial class IecGoose
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel2 = new Panel();
            DgvSend = new DataGridView();
            Column1 = new DataGridViewButtonColumn();
            Nome = new DataGridViewTextBoxColumn();
            Descrição = new DataGridViewTextBoxColumn();
            DataSet = new DataGridViewTextBoxColumn();
            panel3 = new Panel();
            TbGoId = new TextBox();
            label11 = new Label();
            CbDataSet = new ComboBox();
            label10 = new Label();
            TbConfRev = new TextBox();
            label9 = new Label();
            TbCbRef = new TextBox();
            label8 = new Label();
            TbMaxTime = new TextBox();
            label7 = new Label();
            TbMinTime = new TextBox();
            label6 = new Label();
            TbVlanPriority = new TextBox();
            label5 = new Label();
            TbVlanId = new TextBox();
            label4 = new Label();
            TbAppId = new TextBox();
            label3 = new Label();
            BtnRemove = new Button();
            BtnAdd = new Button();
            TbMacDst = new TextBox();
            label2 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvSend).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(9, 20, 9, 8);
            panel1.Size = new Size(1060, 616);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(9, 20);
            tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 66.6666641F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(1042, 588);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(DgvSend);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 2);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1036, 388);
            panel2.TabIndex = 0;
            // 
            // DgvSend
            // 
            DgvSend.AllowUserToAddRows = false;
            DgvSend.AllowUserToDeleteRows = false;
            DgvSend.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(31, 45, 56);
            DgvSend.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DgvSend.BackgroundColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.Lavender;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DgvSend.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DgvSend.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvSend.Columns.AddRange(new DataGridViewColumn[] { Column1, Nome, Descrição, DataSet });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = Color.Lavender;
            dataGridViewCellStyle5.SelectionBackColor = Color.SkyBlue;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.ControlLightLight;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            DgvSend.DefaultCellStyle = dataGridViewCellStyle5;
            DgvSend.Dock = DockStyle.Fill;
            DgvSend.EditMode = DataGridViewEditMode.EditOnEnter;
            DgvSend.EnableHeadersVisualStyles = false;
            DgvSend.GridColor = Color.Lavender;
            DgvSend.Location = new Point(118, 0);
            DgvSend.Name = "DgvSend";
            DgvSend.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Lavender;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            DgvSend.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            DgvSend.RowHeadersVisible = false;
            DgvSend.RowHeadersWidth = 51;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle7.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = Color.Lavender;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.HotTrack;
            dataGridViewCellStyle7.SelectionForeColor = Color.AliceBlue;
            DgvSend.RowsDefaultCellStyle = dataGridViewCellStyle7;
            DgvSend.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DgvSend.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(31, 45, 56);
            DgvSend.RowTemplate.DefaultCellStyle.ForeColor = Color.Lavender;
            DgvSend.RowTemplate.Height = 25;
            DgvSend.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvSend.Size = new Size(575, 386);
            DgvSend.TabIndex = 1;
            DgvSend.CellClick += DgvSend_CellClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 40;
            // 
            // Nome
            // 
            Nome.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Nome.DefaultCellStyle = dataGridViewCellStyle3;
            Nome.HeaderText = "Control Block Ref";
            Nome.MinimumWidth = 6;
            Nome.Name = "Nome";
            Nome.ReadOnly = true;
            // 
            // Descrição
            // 
            Descrição.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Descrição.DefaultCellStyle = dataGridViewCellStyle4;
            Descrição.HeaderText = "GOOSE ID";
            Descrição.MinimumWidth = 6;
            Descrição.Name = "Descrição";
            Descrição.ReadOnly = true;
            // 
            // DataSet
            // 
            DataSet.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataSet.HeaderText = "DataSet";
            DataSet.MinimumWidth = 6;
            DataSet.Name = "DataSet";
            DataSet.ReadOnly = true;
            // 
            // panel3
            // 
            panel3.Controls.Add(TbGoId);
            panel3.Controls.Add(label11);
            panel3.Controls.Add(CbDataSet);
            panel3.Controls.Add(label10);
            panel3.Controls.Add(TbConfRev);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(TbCbRef);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(TbMaxTime);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(TbMinTime);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(TbVlanPriority);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(TbVlanId);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(TbAppId);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(BtnRemove);
            panel3.Controls.Add(BtnAdd);
            panel3.Controls.Add(TbMacDst);
            panel3.Controls.Add(label2);
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(693, 0);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(9, 8, 9, 0);
            panel3.Size = new Size(341, 386);
            panel3.TabIndex = 3;
            // 
            // TbGoId
            // 
            TbGoId.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbGoId.BackColor = Color.FromArgb(31, 45, 56);
            TbGoId.BorderStyle = BorderStyle.None;
            TbGoId.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbGoId.ForeColor = Color.Lavender;
            TbGoId.Location = new Point(94, 41);
            TbGoId.Margin = new Padding(3, 2, 3, 2);
            TbGoId.Name = "TbGoId";
            TbGoId.Size = new Size(239, 25);
            TbGoId.TabIndex = 24;
            TbGoId.TextAlign = HorizontalAlignment.Center;
            TbGoId.KeyPress += TbCbRef_KeyPress;
            TbGoId.Validated += TbMacDst_Validated;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = Color.Lavender;
            label11.Location = new Point(26, 41);
            label11.Name = "label11";
            label11.Size = new Size(57, 25);
            label11.TabIndex = 23;
            label11.Text = "Go Id";
            // 
            // CbDataSet
            // 
            CbDataSet.BackColor = Color.FromArgb(31, 45, 56);
            CbDataSet.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            CbDataSet.ForeColor = Color.Lavender;
            CbDataSet.FormattingEnabled = true;
            CbDataSet.Location = new Point(117, 293);
            CbDataSet.Margin = new Padding(3, 2, 3, 2);
            CbDataSet.Name = "CbDataSet";
            CbDataSet.Size = new Size(213, 33);
            CbDataSet.TabIndex = 22;
            CbDataSet.KeyPress += TbCbRef_KeyPress;
            CbDataSet.Validated += TbMacDst_Validated;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label10.ForeColor = Color.Lavender;
            label10.Location = new Point(24, 296);
            label10.Name = "label10";
            label10.Size = new Size(77, 25);
            label10.TabIndex = 21;
            label10.Text = "DataSet";
            // 
            // TbConfRev
            // 
            TbConfRev.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbConfRev.BackColor = Color.FromArgb(31, 45, 56);
            TbConfRev.BorderStyle = BorderStyle.None;
            TbConfRev.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbConfRev.ForeColor = Color.Lavender;
            TbConfRev.Location = new Point(116, 230);
            TbConfRev.Margin = new Padding(3, 2, 3, 2);
            TbConfRev.Name = "TbConfRev";
            TbConfRev.Size = new Size(214, 25);
            TbConfRev.TabIndex = 20;
            TbConfRev.TextAlign = HorizontalAlignment.Center;
            TbConfRev.KeyPress += TbCbRef_KeyPress;
            TbConfRev.Validated += TbMacDst_Validated;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = Color.Lavender;
            label9.Location = new Point(24, 230);
            label9.Name = "label9";
            label9.Size = new Size(78, 25);
            label9.TabIndex = 19;
            label9.Text = "confRev";
            // 
            // TbCbRef
            // 
            TbCbRef.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbCbRef.BackColor = Color.FromArgb(31, 45, 56);
            TbCbRef.BorderStyle = BorderStyle.None;
            TbCbRef.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbCbRef.ForeColor = Color.Lavender;
            TbCbRef.Location = new Point(102, 10);
            TbCbRef.Margin = new Padding(3, 2, 3, 2);
            TbCbRef.Name = "TbCbRef";
            TbCbRef.Size = new Size(228, 25);
            TbCbRef.TabIndex = 18;
            TbCbRef.TextAlign = HorizontalAlignment.Center;
            TbCbRef.KeyPress += TbCbRef_KeyPress;
            TbCbRef.Validated += TbMacDst_Validated;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = Color.Lavender;
            label8.Location = new Point(24, 10);
            label8.Name = "label8";
            label8.Size = new Size(66, 25);
            label8.TabIndex = 17;
            label8.Text = "Cb Ref";
            // 
            // TbMaxTime
            // 
            TbMaxTime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbMaxTime.BackColor = Color.FromArgb(31, 45, 56);
            TbMaxTime.BorderStyle = BorderStyle.None;
            TbMaxTime.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbMaxTime.ForeColor = Color.Lavender;
            TbMaxTime.Location = new Point(133, 199);
            TbMaxTime.Margin = new Padding(3, 2, 3, 2);
            TbMaxTime.Name = "TbMaxTime";
            TbMaxTime.Size = new Size(197, 25);
            TbMaxTime.TabIndex = 16;
            TbMaxTime.TextAlign = HorizontalAlignment.Center;
            TbMaxTime.KeyPress += TbCbRef_KeyPress;
            TbMaxTime.Validated += TbMacDst_Validated;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = Color.Lavender;
            label7.Location = new Point(24, 199);
            label7.Name = "label7";
            label7.Size = new Size(94, 25);
            label7.TabIndex = 15;
            label7.Text = "Max Time";
            // 
            // TbMinTime
            // 
            TbMinTime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbMinTime.BackColor = Color.FromArgb(31, 45, 56);
            TbMinTime.BorderStyle = BorderStyle.None;
            TbMinTime.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbMinTime.ForeColor = Color.Lavender;
            TbMinTime.Location = new Point(130, 167);
            TbMinTime.Margin = new Padding(3, 2, 3, 2);
            TbMinTime.Name = "TbMinTime";
            TbMinTime.Size = new Size(200, 25);
            TbMinTime.TabIndex = 14;
            TbMinTime.TextAlign = HorizontalAlignment.Center;
            TbMinTime.KeyPress += TbCbRef_KeyPress;
            TbMinTime.Validated += TbMacDst_Validated;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = Color.Lavender;
            label6.Location = new Point(24, 167);
            label6.Name = "label6";
            label6.Size = new Size(91, 25);
            label6.TabIndex = 13;
            label6.Text = "Min Time";
            // 
            // TbVlanPriority
            // 
            TbVlanPriority.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbVlanPriority.BackColor = Color.FromArgb(31, 45, 56);
            TbVlanPriority.BorderStyle = BorderStyle.None;
            TbVlanPriority.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbVlanPriority.ForeColor = Color.Lavender;
            TbVlanPriority.Location = new Point(156, 136);
            TbVlanPriority.Margin = new Padding(3, 2, 3, 2);
            TbVlanPriority.Name = "TbVlanPriority";
            TbVlanPriority.Size = new Size(174, 25);
            TbVlanPriority.TabIndex = 12;
            TbVlanPriority.TextAlign = HorizontalAlignment.Center;
            TbVlanPriority.KeyPress += TbCbRef_KeyPress;
            TbVlanPriority.Validated += TbMacDst_Validated;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.Lavender;
            label5.Location = new Point(24, 136);
            label5.Name = "label5";
            label5.Size = new Size(117, 25);
            label5.TabIndex = 11;
            label5.Text = "vLan Priority";
            // 
            // TbVlanId
            // 
            TbVlanId.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbVlanId.BackColor = Color.FromArgb(31, 45, 56);
            TbVlanId.BorderStyle = BorderStyle.None;
            TbVlanId.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbVlanId.ForeColor = Color.Lavender;
            TbVlanId.Location = new Point(108, 104);
            TbVlanId.Margin = new Padding(3, 2, 3, 2);
            TbVlanId.Name = "TbVlanId";
            TbVlanId.Size = new Size(222, 25);
            TbVlanId.TabIndex = 10;
            TbVlanId.TextAlign = HorizontalAlignment.Center;
            TbVlanId.KeyPress += TbCbRef_KeyPress;
            TbVlanId.Validated += TbMacDst_Validated;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.Lavender;
            label4.Location = new Point(24, 104);
            label4.Name = "label4";
            label4.Size = new Size(72, 25);
            label4.TabIndex = 9;
            label4.Text = "vLan Id";
            // 
            // TbAppId
            // 
            TbAppId.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbAppId.BackColor = Color.FromArgb(31, 45, 56);
            TbAppId.BorderStyle = BorderStyle.None;
            TbAppId.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbAppId.ForeColor = Color.Lavender;
            TbAppId.Location = new Point(108, 262);
            TbAppId.Margin = new Padding(3, 2, 3, 2);
            TbAppId.Name = "TbAppId";
            TbAppId.Size = new Size(225, 25);
            TbAppId.TabIndex = 8;
            TbAppId.TextAlign = HorizontalAlignment.Center;
            TbAppId.KeyPress += TbCbRef_KeyPress;
            TbAppId.Validated += TbMacDst_Validated;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Lavender;
            label3.Location = new Point(24, 262);
            label3.Name = "label3";
            label3.Size = new Size(69, 25);
            label3.TabIndex = 7;
            label3.Text = "App ID";
            // 
            // BtnRemove
            // 
            BtnRemove.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnRemove.FlatAppearance.BorderSize = 0;
            BtnRemove.FlatStyle = FlatStyle.Popup;
            BtnRemove.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnRemove.Location = new Point(183, 344);
            BtnRemove.Margin = new Padding(3, 2, 3, 2);
            BtnRemove.Name = "BtnRemove";
            BtnRemove.Size = new Size(147, 33);
            BtnRemove.TabIndex = 6;
            BtnRemove.Text = "Remover";
            BtnRemove.UseVisualStyleBackColor = true;
            BtnRemove.Click += BtnRemove_Click;
            // 
            // BtnAdd
            // 
            BtnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            BtnAdd.FlatAppearance.BorderColor = Color.White;
            BtnAdd.FlatAppearance.BorderSize = 0;
            BtnAdd.FlatStyle = FlatStyle.Popup;
            BtnAdd.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnAdd.Location = new Point(11, 344);
            BtnAdd.Margin = new Padding(3, 2, 3, 2);
            BtnAdd.Name = "BtnAdd";
            BtnAdd.Size = new Size(147, 33);
            BtnAdd.TabIndex = 5;
            BtnAdd.Text = "Adicionar";
            BtnAdd.UseVisualStyleBackColor = true;
            BtnAdd.Click += BtnAdd_Click;
            // 
            // TbMacDst
            // 
            TbMacDst.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbMacDst.BackColor = Color.FromArgb(31, 45, 56);
            TbMacDst.BorderStyle = BorderStyle.None;
            TbMacDst.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbMacDst.ForeColor = Color.Lavender;
            TbMacDst.Location = new Point(117, 73);
            TbMacDst.Margin = new Padding(3, 2, 3, 2);
            TbMacDst.Name = "TbMacDst";
            TbMacDst.Size = new Size(213, 25);
            TbMacDst.TabIndex = 4;
            TbMacDst.TextAlign = HorizontalAlignment.Center;
            TbMacDst.KeyPress += TbCbRef_KeyPress;
            TbMacDst.Validated += TbMacDst_Validated;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Lavender;
            label2.Location = new Point(24, 73);
            label2.Name = "label2";
            label2.Size = new Size(80, 25);
            label2.TabIndex = 3;
            label2.Text = "Mac Dst";
            // 
            // label1
            // 
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Dock = DockStyle.Left;
            label1.FlatStyle = FlatStyle.Popup;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(118, 386);
            label1.TabIndex = 2;
            label1.Text = "Envio";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // IecGoose
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1060, 616);
            Controls.Add(panel1);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "IecGoose";
            Text = "IecGoose";
            Load += IecGoose_Load;
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DgvSend).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel2;
        private DataGridView DgvSend;
        private Panel panel3;
        private Label label1;
        private TextBox TbMacDst;
        private Label label2;
        private Button BtnRemove;
        private Button BtnAdd;
        private ComboBox CbDataSet;
        private Label label10;
        private TextBox TbConfRev;
        private Label label9;
        private TextBox TbCbRef;
        private Label label8;
        private TextBox TbMaxTime;
        private Label label7;
        private TextBox TbMinTime;
        private Label label6;
        private TextBox TbVlanPriority;
        private Label label5;
        private TextBox TbVlanId;
        private Label label4;
        private TextBox TbAppId;
        private Label label3;
        private TextBox TbGoId;
        private Label label11;
        private DataGridViewButtonColumn Column1;
        private DataGridViewTextBoxColumn Nome;
        private DataGridViewTextBoxColumn Descrição;
        private DataGridViewTextBoxColumn DataSet;
    }
}