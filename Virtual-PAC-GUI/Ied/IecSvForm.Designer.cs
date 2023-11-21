namespace Ied
{
    partial class IecSvForm
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
            splitter1 = new Splitter();
            panel3 = new Panel();
            groupBox1 = new GroupBox();
            TbSvSync = new TextBox();
            label9 = new Label();
            label8 = new Label();
            TbVLanPriority = new TextBox();
            BtnImport = new Button();
            TbSvMacDest = new TextBox();
            label7 = new Label();
            label6 = new Label();
            TbSvRev = new TextBox();
            label5 = new Label();
            TbSVNoAsdu = new TextBox();
            label4 = new Label();
            TbSvAppID = new TextBox();
            label3 = new Label();
            TbSvVLan = new TextBox();
            label2 = new Label();
            TbSvRate = new TextBox();
            label1 = new Label();
            TbSvId = new TextBox();
            panel2 = new Panel();
            DgvSend = new DataGridView();
            Nome = new DataGridViewTextBoxColumn();
            Descrição = new DataGridViewTextBoxColumn();
            DataSet = new DataGridViewTextBoxColumn();
            doName = new DataGridViewTextBoxColumn();
            daName = new DataGridViewTextBoxColumn();
            Tipo = new DataGridViewTextBoxColumn();
            panel4 = new Panel();
            openFileDialog1 = new OpenFileDialog();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvSend).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(splitter1);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(panel4);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1060, 616);
            panel1.TabIndex = 0;
            // 
            // splitter1
            // 
            splitter1.Location = new Point(562, 0);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 520);
            splitter1.TabIndex = 0;
            splitter1.TabStop = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(groupBox1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(562, 0);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(10);
            panel3.Size = new Size(498, 520);
            panel3.TabIndex = 8;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(TbSvSync);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(TbVLanPriority);
            groupBox1.Controls.Add(BtnImport);
            groupBox1.Controls.Add(TbSvMacDest);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(TbSvRev);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(TbSVNoAsdu);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(TbSvAppID);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(TbSvVLan);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(TbSvRate);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(TbSvId);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.GhostWhite;
            groupBox1.Location = new Point(10, 10);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(478, 500);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Sampled Value";
            // 
            // TbSvSync
            // 
            TbSvSync.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TbSvSync.BackColor = Color.FromArgb(31, 45, 56);
            TbSvSync.BorderStyle = BorderStyle.None;
            TbSvSync.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSvSync.ForeColor = Color.Lavender;
            TbSvSync.Location = new Point(151, 360);
            TbSvSync.Margin = new Padding(3, 2, 3, 2);
            TbSvSync.Name = "TbSvSync";
            TbSvSync.Size = new Size(321, 25);
            TbSvSync.TabIndex = 18;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = Color.Lavender;
            label9.Location = new Point(23, 360);
            label9.Name = "label9";
            label9.Size = new Size(116, 25);
            label9.TabIndex = 17;
            label9.Text = "Sincronismo";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = Color.Lavender;
            label8.Location = new Point(23, 315);
            label8.Name = "label8";
            label8.Size = new Size(169, 25);
            label8.TabIndex = 16;
            label8.Text = "Virtual Lan Priority";
            // 
            // TbVLanPriority
            // 
            TbVLanPriority.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TbVLanPriority.BackColor = Color.FromArgb(31, 45, 56);
            TbVLanPriority.BorderStyle = BorderStyle.None;
            TbVLanPriority.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbVLanPriority.ForeColor = Color.Lavender;
            TbVLanPriority.Location = new Point(198, 315);
            TbVLanPriority.Margin = new Padding(3, 2, 3, 2);
            TbVLanPriority.Name = "TbVLanPriority";
            TbVLanPriority.Size = new Size(274, 25);
            TbVLanPriority.TabIndex = 15;
            // 
            // BtnImport
            // 
            BtnImport.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BtnImport.FlatAppearance.BorderColor = Color.Black;
            BtnImport.FlatAppearance.BorderSize = 0;
            BtnImport.FlatStyle = FlatStyle.Popup;
            BtnImport.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnImport.Location = new Point(138, 447);
            BtnImport.Margin = new Padding(3, 2, 3, 2);
            BtnImport.Name = "BtnImport";
            BtnImport.Size = new Size(206, 32);
            BtnImport.TabIndex = 6;
            BtnImport.Text = "Importar SCL";
            BtnImport.UseVisualStyleBackColor = true;
            BtnImport.Click += BtnImport_Click;
            // 
            // TbSvMacDest
            // 
            TbSvMacDest.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TbSvMacDest.BackColor = Color.FromArgb(31, 45, 56);
            TbSvMacDest.BorderStyle = BorderStyle.None;
            TbSvMacDest.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSvMacDest.ForeColor = Color.Lavender;
            TbSvMacDest.Location = new Point(151, 180);
            TbSvMacDest.Margin = new Padding(3, 2, 3, 2);
            TbSvMacDest.Name = "TbSvMacDest";
            TbSvMacDest.Size = new Size(321, 25);
            TbSvMacDest.TabIndex = 14;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = Color.Lavender;
            label7.Location = new Point(23, 225);
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
            label6.Location = new Point(23, 90);
            label6.Name = "label6";
            label6.Size = new Size(116, 25);
            label6.TabIndex = 11;
            label6.Text = "Sample Rate";
            // 
            // TbSvRev
            // 
            TbSvRev.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TbSvRev.BackColor = Color.FromArgb(31, 45, 56);
            TbSvRev.BorderStyle = BorderStyle.None;
            TbSvRev.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSvRev.ForeColor = Color.Lavender;
            TbSvRev.Location = new Point(100, 405);
            TbSvRev.Margin = new Padding(3, 2, 3, 2);
            TbSvRev.Name = "TbSvRev";
            TbSvRev.Size = new Size(372, 25);
            TbSvRev.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.Lavender;
            label5.Location = new Point(23, 135);
            label5.Name = "label5";
            label5.Size = new Size(79, 25);
            label5.TabIndex = 9;
            label5.Text = "NoAsdu";
            // 
            // TbSVNoAsdu
            // 
            TbSVNoAsdu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TbSVNoAsdu.BackColor = Color.FromArgb(31, 45, 56);
            TbSVNoAsdu.BorderStyle = BorderStyle.None;
            TbSVNoAsdu.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSVNoAsdu.ForeColor = Color.Lavender;
            TbSVNoAsdu.Location = new Point(108, 135);
            TbSVNoAsdu.Margin = new Padding(3, 2, 3, 2);
            TbSVNoAsdu.Name = "TbSVNoAsdu";
            TbSVNoAsdu.Size = new Size(364, 25);
            TbSVNoAsdu.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.Lavender;
            label4.Location = new Point(23, 405);
            label4.Name = "label4";
            label4.Size = new Size(75, 25);
            label4.TabIndex = 7;
            label4.Text = "Revisão";
            // 
            // TbSvAppID
            // 
            TbSvAppID.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TbSvAppID.BackColor = Color.FromArgb(31, 45, 56);
            TbSvAppID.BorderStyle = BorderStyle.None;
            TbSvAppID.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSvAppID.ForeColor = Color.Lavender;
            TbSvAppID.Location = new Point(104, 225);
            TbSvAppID.Margin = new Padding(3, 2, 3, 2);
            TbSvAppID.Name = "TbSvAppID";
            TbSvAppID.Size = new Size(368, 25);
            TbSvAppID.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Lavender;
            label3.Location = new Point(23, 270);
            label3.Name = "label3";
            label3.Size = new Size(103, 25);
            label3.TabIndex = 5;
            label3.Text = "Virtual Lan";
            // 
            // TbSvVLan
            // 
            TbSvVLan.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TbSvVLan.BackColor = Color.FromArgb(31, 45, 56);
            TbSvVLan.BorderStyle = BorderStyle.None;
            TbSvVLan.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSvVLan.ForeColor = Color.Lavender;
            TbSvVLan.Location = new Point(138, 270);
            TbSvVLan.Margin = new Padding(3, 2, 3, 2);
            TbSvVLan.Name = "TbSvVLan";
            TbSvVLan.Size = new Size(334, 25);
            TbSvVLan.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Lavender;
            label2.Location = new Point(23, 45);
            label2.Name = "label2";
            label2.Size = new Size(160, 25);
            label2.TabIndex = 3;
            label2.Text = "Sampled Value ID";
            // 
            // TbSvRate
            // 
            TbSvRate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TbSvRate.BackColor = Color.FromArgb(31, 45, 56);
            TbSvRate.BorderStyle = BorderStyle.None;
            TbSvRate.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSvRate.ForeColor = Color.Lavender;
            TbSvRate.Location = new Point(138, 90);
            TbSvRate.Margin = new Padding(3, 2, 3, 2);
            TbSvRate.Name = "TbSvRate";
            TbSvRate.Size = new Size(334, 25);
            TbSvRate.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Lavender;
            label1.Location = new Point(23, 180);
            label1.Name = "label1";
            label1.Size = new Size(117, 25);
            label1.TabIndex = 1;
            label1.Text = "Mac Destino";
            // 
            // TbSvId
            // 
            TbSvId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TbSvId.BackColor = Color.FromArgb(31, 45, 56);
            TbSvId.BorderStyle = BorderStyle.None;
            TbSvId.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSvId.ForeColor = Color.Lavender;
            TbSvId.Location = new Point(189, 45);
            TbSvId.Margin = new Padding(3, 2, 3, 2);
            TbSvId.Name = "TbSvId";
            TbSvId.Size = new Size(283, 25);
            TbSvId.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(DgvSend);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(10);
            panel2.Size = new Size(562, 520);
            panel2.TabIndex = 7;
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
            DgvSend.Columns.AddRange(new DataGridViewColumn[] { Nome, Descrição, DataSet, doName, daName, Tipo });
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
            DgvSend.Location = new Point(10, 10);
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
            DgvSend.Size = new Size(542, 500);
            DgvSend.TabIndex = 2;
            // 
            // Nome
            // 
            Nome.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Nome.DefaultCellStyle = dataGridViewCellStyle3;
            Nome.HeaderText = "LdInst";
            Nome.MinimumWidth = 6;
            Nome.Name = "Nome";
            Nome.ReadOnly = true;
            Nome.Width = 78;
            // 
            // Descrição
            // 
            Descrição.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Descrição.DefaultCellStyle = dataGridViewCellStyle4;
            Descrição.HeaderText = "LN Class";
            Descrição.MinimumWidth = 6;
            Descrição.Name = "Descrição";
            Descrição.ReadOnly = true;
            Descrição.Width = 93;
            // 
            // DataSet
            // 
            DataSet.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataSet.HeaderText = "LN Inst";
            DataSet.MinimumWidth = 6;
            DataSet.Name = "DataSet";
            DataSet.ReadOnly = true;
            DataSet.Width = 85;
            // 
            // doName
            // 
            doName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            doName.HeaderText = "DO Name";
            doName.Name = "doName";
            doName.ReadOnly = true;
            doName.Width = 102;
            // 
            // daName
            // 
            daName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            daName.HeaderText = "DA Name";
            daName.Name = "daName";
            daName.ReadOnly = true;
            daName.Width = 102;
            // 
            // Tipo
            // 
            Tipo.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Tipo.HeaderText = "Tipo";
            Tipo.Name = "Tipo";
            Tipo.ReadOnly = true;
            Tipo.Width = 65;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 520);
            panel4.Name = "panel4";
            panel4.Size = new Size(1060, 96);
            panel4.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // IecSvForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1060, 616);
            Controls.Add(panel1);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Name = "IecSvForm";
            Text = "IecSvForm";
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DgvSend).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button BtnImport;
        private Panel panel3;
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
        private TextBox TbSvVLan;
        private Label label2;
        private TextBox TbSvRate;
        private Label label1;
        private TextBox TbSvId;
        private Panel panel2;
        private Splitter splitter1;
        private Label label8;
        private TextBox TbVLanPriority;
        private TextBox TbSvSync;
        private Label label9;
        private DataGridView DgvSend;
        private Panel panel4;
        private OpenFileDialog openFileDialog1;
        private DataGridViewTextBoxColumn Nome;
        private DataGridViewTextBoxColumn Descrição;
        private DataGridViewTextBoxColumn DataSet;
        private DataGridViewTextBoxColumn doName;
        private DataGridViewTextBoxColumn daName;
        private DataGridViewTextBoxColumn Tipo;
    }
}