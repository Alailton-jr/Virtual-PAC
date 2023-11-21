namespace Ied
{
    partial class IecDataSetConfForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IecDataSetConfForm));
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel4 = new Panel();
            DgvDs = new DataGridView();
            constrain = new DataGridViewTextBoxColumn();
            Data = new DataGridViewTextBoxColumn();
            groupBox2 = new GroupBox();
            LbDataItens = new Label();
            panel3 = new Panel();
            CbFilter = new ComboBox();
            TvIed = new TreeView();
            groupBox1 = new GroupBox();
            LbIedItens = new Label();
            panel6 = new Panel();
            BtnCancel = new Button();
            BtnSave = new Button();
            panel2 = new Panel();
            TbDesc = new TextBox();
            label1 = new Label();
            TbName = new TextBox();
            label3 = new Label();
            PnMove = new Panel();
            BtnResize = new Button();
            BtnMinimize = new Button();
            BtnExit = new Button();
            panel5 = new Panel();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvDs).BeginInit();
            groupBox2.SuspendLayout();
            panel3.SuspendLayout();
            groupBox1.SuspendLayout();
            panel6.SuspendLayout();
            panel2.SuspendLayout();
            PnMove.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(40, 58, 73);
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(PnMove);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1038, 603);
            panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panel4, 0, 0);
            tableLayoutPanel1.Controls.Add(panel3, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 166);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1038, 376);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(40, 58, 73);
            panel4.Controls.Add(DgvDs);
            panel4.Controls.Add(groupBox2);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(522, 3);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(40, 0, 40, 0);
            panel4.Size = new Size(513, 370);
            panel4.TabIndex = 3;
            // 
            // DgvDs
            // 
            DgvDs.AllowDrop = true;
            DgvDs.AllowUserToAddRows = false;
            DgvDs.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle1.ForeColor = Color.Lavender;
            DgvDs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DgvDs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DgvDs.BackgroundColor = Color.FromArgb(31, 45, 56);
            DgvDs.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            DgvDs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.Lavender;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle2.SelectionForeColor = Color.Lavender;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DgvDs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DgvDs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvDs.Columns.AddRange(new DataGridViewColumn[] { constrain, Data });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            DgvDs.DefaultCellStyle = dataGridViewCellStyle5;
            DgvDs.Dock = DockStyle.Fill;
            DgvDs.EnableHeadersVisualStyles = false;
            DgvDs.GridColor = Color.Lavender;
            DgvDs.Location = new Point(40, 88);
            DgvDs.Name = "DgvDs";
            DgvDs.ReadOnly = true;
            DgvDs.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Lavender;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            DgvDs.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            DgvDs.RowHeadersVisible = false;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle7.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = Color.Lavender;
            DgvDs.RowsDefaultCellStyle = dataGridViewCellStyle7;
            DgvDs.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(31, 45, 56);
            DgvDs.RowTemplate.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            DgvDs.RowTemplate.DefaultCellStyle.ForeColor = Color.Lavender;
            DgvDs.RowTemplate.Height = 25;
            DgvDs.RowTemplate.Resizable = DataGridViewTriState.False;
            DgvDs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvDs.Size = new Size(433, 282);
            DgvDs.TabIndex = 8;
            DgvDs.DragDrop += TvDs_DragDrop;
            DgvDs.DragEnter += TvDs_DragEnter;
            // 
            // constrain
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Lavender;
            constrain.DefaultCellStyle = dataGridViewCellStyle3;
            constrain.FillWeight = 36.54822F;
            constrain.HeaderText = "Restrição";
            constrain.Name = "constrain";
            constrain.ReadOnly = true;
            // 
            // Data
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = Color.Lavender;
            Data.DefaultCellStyle = dataGridViewCellStyle4;
            Data.FillWeight = 143.451782F;
            Data.HeaderText = "Dados";
            Data.Name = "Data";
            Data.ReadOnly = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(LbDataItens);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.Location = new Point(40, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(433, 88);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            // 
            // LbDataItens
            // 
            LbDataItens.Dock = DockStyle.Fill;
            LbDataItens.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LbDataItens.ForeColor = Color.Lavender;
            LbDataItens.Location = new Point(3, 19);
            LbDataItens.Name = "LbDataItens";
            LbDataItens.Size = new Size(427, 66);
            LbDataItens.TabIndex = 7;
            LbDataItens.Text = "Dados Mapeados";
            LbDataItens.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(40, 58, 73);
            panel3.Controls.Add(CbFilter);
            panel3.Controls.Add(TvIed);
            panel3.Controls.Add(groupBox1);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(3, 3);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(40, 0, 40, 0);
            panel3.Size = new Size(513, 370);
            panel3.TabIndex = 2;
            // 
            // CbFilter
            // 
            CbFilter.BackColor = Color.FromArgb(31, 45, 56);
            CbFilter.ForeColor = Color.Lavender;
            CbFilter.FormattingEnabled = true;
            CbFilter.Items.AddRange(new object[] { "ST", "MX", "DC", "CF" });
            CbFilter.Location = new Point(307, 94);
            CbFilter.Name = "CbFilter";
            CbFilter.Size = new Size(121, 23);
            CbFilter.TabIndex = 9;
            CbFilter.Text = "ST";
            CbFilter.TextChanged += CbFilter_TextChanged;
            // 
            // TvIed
            // 
            TvIed.AllowDrop = true;
            TvIed.BackColor = Color.FromArgb(31, 45, 56);
            TvIed.Dock = DockStyle.Fill;
            TvIed.ForeColor = Color.Lavender;
            TvIed.Location = new Point(40, 88);
            TvIed.Name = "TvIed";
            TvIed.Size = new Size(433, 282);
            TvIed.TabIndex = 1;
            TvIed.ItemDrag += TvIed_ItemDrag;
            TvIed.KeyDown += TvIed_KeyDown;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(LbIedItens);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(40, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(433, 88);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            // 
            // LbIedItens
            // 
            LbIedItens.Dock = DockStyle.Fill;
            LbIedItens.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LbIedItens.ForeColor = Color.Lavender;
            LbIedItens.Location = new Point(3, 19);
            LbIedItens.Name = "LbIedItens";
            LbIedItens.Size = new Size(427, 66);
            LbIedItens.TabIndex = 7;
            LbIedItens.Text = "IED Information";
            LbIedItens.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            panel6.Controls.Add(BtnCancel);
            panel6.Controls.Add(BtnSave);
            panel6.Dock = DockStyle.Bottom;
            panel6.Location = new Point(0, 542);
            panel6.Name = "panel6";
            panel6.Size = new Size(1038, 61);
            panel6.TabIndex = 11;
            // 
            // BtnCancel
            // 
            BtnCancel.FlatStyle = FlatStyle.Flat;
            BtnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnCancel.ForeColor = Color.Lavender;
            BtnCancel.Location = new Point(590, 16);
            BtnCancel.Margin = new Padding(3, 2, 3, 2);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(104, 37);
            BtnCancel.TabIndex = 11;
            BtnCancel.Text = "Cancelar";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // BtnSave
            // 
            BtnSave.FlatStyle = FlatStyle.Flat;
            BtnSave.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSave.ForeColor = Color.Lavender;
            BtnSave.Location = new Point(327, 16);
            BtnSave.Margin = new Padding(3, 2, 3, 2);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(104, 37);
            BtnSave.TabIndex = 10;
            BtnSave.Text = "Salvar";
            BtnSave.UseVisualStyleBackColor = true;
            BtnSave.Click += BtnSave_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(TbDesc);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(TbName);
            panel2.Controls.Add(label3);
            panel2.Dock = DockStyle.Top;
            panel2.ForeColor = Color.Lavender;
            panel2.Location = new Point(0, 50);
            panel2.Name = "panel2";
            panel2.Size = new Size(1038, 116);
            panel2.TabIndex = 1;
            // 
            // TbDesc
            // 
            TbDesc.BackColor = Color.FromArgb(31, 45, 56);
            TbDesc.BorderStyle = BorderStyle.None;
            TbDesc.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbDesc.ForeColor = Color.Lavender;
            TbDesc.Location = new Point(144, 62);
            TbDesc.Margin = new Padding(3, 2, 3, 2);
            TbDesc.Name = "TbDesc";
            TbDesc.Size = new Size(860, 25);
            TbDesc.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Lavender;
            label1.Location = new Point(40, 62);
            label1.Name = "label1";
            label1.Size = new Size(98, 25);
            label1.TabIndex = 5;
            label1.Text = "Descrição:";
            // 
            // TbName
            // 
            TbName.BackColor = Color.FromArgb(31, 45, 56);
            TbName.BorderStyle = BorderStyle.None;
            TbName.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbName.ForeColor = Color.Lavender;
            TbName.Location = new Point(115, 18);
            TbName.Margin = new Padding(3, 2, 3, 2);
            TbName.Name = "TbName";
            TbName.Size = new Size(889, 25);
            TbName.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Lavender;
            label3.Location = new Point(40, 18);
            label3.Name = "label3";
            label3.Size = new Size(67, 25);
            label3.TabIndex = 3;
            label3.Text = "Nome:";
            // 
            // PnMove
            // 
            PnMove.BackColor = Color.FromArgb(1, 22, 39);
            PnMove.Controls.Add(BtnResize);
            PnMove.Controls.Add(BtnMinimize);
            PnMove.Controls.Add(BtnExit);
            PnMove.Dock = DockStyle.Top;
            PnMove.Location = new Point(0, 0);
            PnMove.Name = "PnMove";
            PnMove.Size = new Size(1038, 50);
            PnMove.TabIndex = 9;
            PnMove.Click += btnMinimize_Click;
            PnMove.MouseDown += panelTitleBar_MouseDown;
            // 
            // BtnResize
            // 
            BtnResize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnResize.FlatAppearance.BorderSize = 0;
            BtnResize.FlatStyle = FlatStyle.Flat;
            BtnResize.Image = (Image)resources.GetObject("BtnResize.Image");
            BtnResize.Location = new Point(937, 3);
            BtnResize.Name = "BtnResize";
            BtnResize.Size = new Size(46, 41);
            BtnResize.TabIndex = 9;
            BtnResize.UseVisualStyleBackColor = true;
            BtnResize.Click += btnMaximize_Click;
            // 
            // BtnMinimize
            // 
            BtnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnMinimize.FlatAppearance.BorderSize = 0;
            BtnMinimize.FlatStyle = FlatStyle.Flat;
            BtnMinimize.Image = (Image)resources.GetObject("BtnMinimize.Image");
            BtnMinimize.Location = new Point(885, 3);
            BtnMinimize.Name = "BtnMinimize";
            BtnMinimize.Size = new Size(46, 41);
            BtnMinimize.TabIndex = 8;
            BtnMinimize.UseVisualStyleBackColor = true;
            // 
            // BtnExit
            // 
            BtnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnExit.FlatAppearance.BorderSize = 0;
            BtnExit.FlatStyle = FlatStyle.Flat;
            BtnExit.Image = (Image)resources.GetObject("BtnExit.Image");
            BtnExit.Location = new Point(989, 3);
            BtnExit.Name = "BtnExit";
            BtnExit.Size = new Size(46, 41);
            BtnExit.TabIndex = 7;
            BtnExit.UseVisualStyleBackColor = true;
            BtnExit.Click += btnClose_Click;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(31, 45, 56);
            panel5.Controls.Add(panel1);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Padding = new Padding(3);
            panel5.Size = new Size(1044, 609);
            panel5.TabIndex = 8;
            // 
            // IecDataSetConfForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1044, 609);
            Controls.Add(panel5);
            FormBorderStyle = FormBorderStyle.None;
            Name = "IecDataSetConfForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "IecDataSetForm";
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DgvDs).EndInit();
            groupBox2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            PnMove.ResumeLayout(false);
            panel5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel4;
        private Panel panel3;
        private TreeView TvIed;
        private Panel panel2;
        private GroupBox groupBox2;
        private Label LbDataItens;
        private GroupBox groupBox1;
        private Label LbIedItens;
        private TextBox TbDesc;
        private Label label1;
        private TextBox TbName;
        private Label label3;
        private Panel PnMove;
        private Button BtnResize;
        private Button BtnMinimize;
        private Button BtnExit;
        private Panel panel5;
        private ComboBox CbFilter;
        private DataGridView DgvDs;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel6;
        private Button BtnCancel;
        private Button BtnSave;
        private DataGridViewTextBoxColumn constrain;
        private DataGridViewTextBoxColumn Data;
    }
}