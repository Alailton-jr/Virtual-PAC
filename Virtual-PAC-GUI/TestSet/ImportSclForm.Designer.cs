namespace TestSet
{
    partial class ImportSclForm
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportSclForm));
            panel1 = new Panel();
            dgvGse = new DataGridView();
            Escolher = new DataGridViewButtonColumn();
            IEDName = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            PnMove = new Panel();
            BtnExit = new Button();
            BtnResize = new Button();
            BtnMinimize = new Button();
            panel2 = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGse).BeginInit();
            PnMove.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(31, 45, 56);
            panel1.Controls.Add(dgvGse);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(15, 75);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(20);
            panel1.Size = new Size(1002, 523);
            panel1.TabIndex = 0;
            // 
            // dgvGse
            // 
            dgvGse.AllowUserToAddRows = false;
            dgvGse.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle1.ForeColor = Color.Lavender;
            dgvGse.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvGse.BackgroundColor = Color.FromArgb(31, 45, 56);
            dgvGse.BorderStyle = BorderStyle.None;
            dgvGse.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dgvGse.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.Lavender;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvGse.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvGse.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGse.Columns.AddRange(new DataGridViewColumn[] { Escolher, IEDName, Column1, Column2, Column3 });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = Color.Lavender;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvGse.DefaultCellStyle = dataGridViewCellStyle4;
            dgvGse.Dock = DockStyle.Fill;
            dgvGse.EnableHeadersVisualStyles = false;
            dgvGse.GridColor = Color.Lavender;
            dgvGse.Location = new Point(20, 20);
            dgvGse.Name = "dgvGse";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = Color.Lavender;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvGse.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvGse.RowHeadersVisible = false;
            dgvGse.RowHeadersWidth = 51;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle6.ForeColor = Color.Lavender;
            dgvGse.RowsDefaultCellStyle = dataGridViewCellStyle6;
            dgvGse.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(31, 45, 56);
            dgvGse.RowTemplate.DefaultCellStyle.ForeColor = Color.Lavender;
            dgvGse.RowTemplate.Height = 29;
            dgvGse.Size = new Size(962, 483);
            dgvGse.TabIndex = 0;
            dgvGse.CellClick += dgvGse_CellClick;
            // 
            // Escolher
            // 
            Escolher.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle3.ForeColor = Color.Lavender;
            Escolher.DefaultCellStyle = dataGridViewCellStyle3;
            Escolher.HeaderText = "Escolher";
            Escolher.MinimumWidth = 6;
            Escolher.Name = "Escolher";
            Escolher.Width = 90;
            // 
            // IEDName
            // 
            IEDName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            IEDName.HeaderText = "IEDName";
            IEDName.MinimumWidth = 6;
            IEDName.Name = "IEDName";
            IEDName.Resizable = DataGridViewTriState.True;
            IEDName.SortMode = DataGridViewColumnSortMode.NotSortable;
            IEDName.Width = 99;
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column1.HeaderText = "Control Block";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.Resizable = DataGridViewTriState.True;
            Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column1.Width = 123;
            // 
            // Column2
            // 
            Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column2.HeaderText = "DataSet";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.Resizable = DataGridViewTriState.True;
            Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column2.Width = 87;
            // 
            // Column3
            // 
            Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column3.HeaderText = "Description";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            // 
            // PnMove
            // 
            PnMove.BackColor = Color.FromArgb(1, 22, 39);
            PnMove.Controls.Add(BtnExit);
            PnMove.Controls.Add(BtnResize);
            PnMove.Controls.Add(BtnMinimize);
            PnMove.Dock = DockStyle.Top;
            PnMove.Location = new Point(15, 15);
            PnMove.Margin = new Padding(3, 4, 3, 4);
            PnMove.Name = "PnMove";
            PnMove.Size = new Size(1002, 60);
            PnMove.TabIndex = 10;
            PnMove.MouseDown += panelTitleBar_MouseDown;
            // 
            // BtnExit
            // 
            BtnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnExit.FlatAppearance.BorderSize = 0;
            BtnExit.FlatStyle = FlatStyle.Flat;
            BtnExit.Image = (Image)resources.GetObject("BtnExit.Image");
            BtnExit.Location = new Point(946, 1);
            BtnExit.Margin = new Padding(3, 4, 3, 4);
            BtnExit.Name = "BtnExit";
            BtnExit.Size = new Size(53, 55);
            BtnExit.TabIndex = 7;
            BtnExit.UseVisualStyleBackColor = true;
            BtnExit.Click += btnClose_Click;
            // 
            // BtnResize
            // 
            BtnResize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnResize.FlatAppearance.BorderSize = 0;
            BtnResize.FlatStyle = FlatStyle.Flat;
            BtnResize.Image = (Image)resources.GetObject("BtnResize.Image");
            BtnResize.Location = new Point(887, 1);
            BtnResize.Margin = new Padding(3, 4, 3, 4);
            BtnResize.Name = "BtnResize";
            BtnResize.Size = new Size(53, 55);
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
            BtnMinimize.Location = new Point(828, 1);
            BtnMinimize.Margin = new Padding(3, 4, 3, 4);
            BtnMinimize.Name = "BtnMinimize";
            BtnMinimize.Size = new Size(53, 55);
            BtnMinimize.TabIndex = 8;
            BtnMinimize.UseVisualStyleBackColor = true;
            BtnMinimize.Click += btnMinimize_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(1, 10, 30);
            panel2.Controls.Add(panel1);
            panel2.Controls.Add(PnMove);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(15);
            panel2.Size = new Size(1032, 613);
            panel2.TabIndex = 1;
            // 
            // ImportSclForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1032, 613);
            Controls.Add(panel2);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Name = "ImportSclForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ImportSclForm";
            Load += ImportSclForm_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvGse).EndInit();
            PnMove.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView dgvGse;
        private DataGridViewButtonColumn Escolher;
        private DataGridViewTextBoxColumn IEDName;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private Panel PnMove;
        private Button BtnExit;
        private Button BtnResize;
        private Button BtnMinimize;
        private Panel panel2;
    }
}