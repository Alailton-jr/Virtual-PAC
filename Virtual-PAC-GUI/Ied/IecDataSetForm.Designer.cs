namespace Ied
{
    partial class IecDataSetForm
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
            PnDataSet = new Panel();
            DgvDs = new DataGridView();
            Column1 = new DataGridViewButtonColumn();
            Nome = new DataGridViewTextBoxColumn();
            Descrição = new DataGridViewTextBoxColumn();
            panel3 = new Panel();
            TvDs = new TreeView();
            panel2 = new Panel();
            BtnRemoveDs = new Button();
            BtnAddDs = new Button();
            panel1.SuspendLayout();
            PnDataSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvDs).BeginInit();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(PnDataSet);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1044, 577);
            panel1.TabIndex = 1;
            // 
            // PnDataSet
            // 
            PnDataSet.Controls.Add(DgvDs);
            PnDataSet.Dock = DockStyle.Fill;
            PnDataSet.Location = new Point(0, 97);
            PnDataSet.Margin = new Padding(0);
            PnDataSet.Name = "PnDataSet";
            PnDataSet.Padding = new Padding(40, 0, 20, 40);
            PnDataSet.Size = new Size(631, 480);
            PnDataSet.TabIndex = 1;
            // 
            // DgvDs
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(31, 45, 56);
            DgvDs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DgvDs.BackgroundColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.Lavender;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DgvDs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DgvDs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvDs.Columns.AddRange(new DataGridViewColumn[] { Column1, Nome, Descrição });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = Color.Lavender;
            dataGridViewCellStyle5.SelectionBackColor = Color.SkyBlue;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.ControlLightLight;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            DgvDs.DefaultCellStyle = dataGridViewCellStyle5;
            DgvDs.Dock = DockStyle.Fill;
            DgvDs.EditMode = DataGridViewEditMode.EditOnEnter;
            DgvDs.EnableHeadersVisualStyles = false;
            DgvDs.GridColor = Color.Lavender;
            DgvDs.Location = new Point(40, 0);
            DgvDs.Name = "DgvDs";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Lavender;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            DgvDs.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            DgvDs.RowHeadersVisible = false;
            DgvDs.RowHeadersWidth = 51;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(31, 45, 56);
            dataGridViewCellStyle7.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = Color.Lavender;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.HotTrack;
            dataGridViewCellStyle7.SelectionForeColor = Color.AliceBlue;
            DgvDs.RowsDefaultCellStyle = dataGridViewCellStyle7;
            DgvDs.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DgvDs.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(31, 45, 56);
            DgvDs.RowTemplate.DefaultCellStyle.ForeColor = Color.Lavender;
            DgvDs.RowTemplate.Height = 25;
            DgvDs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvDs.Size = new Size(571, 440);
            DgvDs.TabIndex = 0;
            DgvDs.CellClick += DgvDs_CellContentClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.Width = 40;
            // 
            // Nome
            // 
            Nome.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Nome.DefaultCellStyle = dataGridViewCellStyle3;
            Nome.HeaderText = "Nome";
            Nome.MinimumWidth = 6;
            Nome.Name = "Nome";
            Nome.Width = 200;
            // 
            // Descrição
            // 
            Descrição.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Descrição.DefaultCellStyle = dataGridViewCellStyle4;
            Descrição.HeaderText = "Descrição";
            Descrição.MinimumWidth = 6;
            Descrição.Name = "Descrição";
            // 
            // panel3
            // 
            panel3.Controls.Add(TvDs);
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(631, 97);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(0, 0, 40, 40);
            panel3.Size = new Size(413, 480);
            panel3.TabIndex = 1;
            // 
            // TvDs
            // 
            TvDs.BackColor = Color.FromArgb(31, 45, 56);
            TvDs.BorderStyle = BorderStyle.None;
            TvDs.CheckBoxes = true;
            TvDs.Dock = DockStyle.Fill;
            TvDs.ForeColor = Color.Lavender;
            TvDs.FullRowSelect = true;
            TvDs.LineColor = Color.Lavender;
            TvDs.Location = new Point(0, 0);
            TvDs.Name = "TvDs";
            TvDs.PathSeparator = "$";
            TvDs.Size = new Size(373, 440);
            TvDs.TabIndex = 0;
            TvDs.AfterCheck += TvDs_AfterCheck;
            // 
            // panel2
            // 
            panel2.Controls.Add(BtnRemoveDs);
            panel2.Controls.Add(BtnAddDs);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1044, 97);
            panel2.TabIndex = 0;
            // 
            // BtnRemoveDs
            // 
            BtnRemoveDs.FlatAppearance.BorderColor = Color.Black;
            BtnRemoveDs.FlatStyle = FlatStyle.Flat;
            BtnRemoveDs.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnRemoveDs.ForeColor = Color.Lavender;
            BtnRemoveDs.Location = new Point(578, 24);
            BtnRemoveDs.Margin = new Padding(3, 2, 3, 2);
            BtnRemoveDs.Name = "BtnRemoveDs";
            BtnRemoveDs.Size = new Size(194, 54);
            BtnRemoveDs.TabIndex = 6;
            BtnRemoveDs.Text = "Remover DataSet";
            BtnRemoveDs.UseVisualStyleBackColor = true;
            BtnRemoveDs.Click += BtnRemoveDs_Click;
            // 
            // BtnAddDs
            // 
            BtnAddDs.FlatAppearance.BorderColor = Color.Black;
            BtnAddDs.FlatStyle = FlatStyle.Flat;
            BtnAddDs.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnAddDs.ForeColor = Color.Lavender;
            BtnAddDs.Location = new Point(246, 24);
            BtnAddDs.Margin = new Padding(3, 2, 3, 2);
            BtnAddDs.Name = "BtnAddDs";
            BtnAddDs.Size = new Size(194, 54);
            BtnAddDs.TabIndex = 5;
            BtnAddDs.Text = "Criar Novo DataSet";
            BtnAddDs.UseVisualStyleBackColor = true;
            BtnAddDs.Click += BtnAddDs_Click;
            // 
            // IecDataSetForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1044, 577);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "IecDataSetForm";
            Text = "IecDataSetForm";
            FormClosing += IecDataSetForm_FormClosing;
            panel1.ResumeLayout(false);
            PnDataSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DgvDs).EndInit();
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel PnDataSet;
        private Panel panel2;
        private Button BtnRemoveDs;
        private Button BtnAddDs;
        private DataGridView DgvDs;
        private DataGridViewCheckBoxColumn Habilitado;
        private Panel panel3;
        private Splitter splitter1;
        private TreeView TvDs;
        private DataGridViewButtonColumn Column1;
        private DataGridViewTextBoxColumn Nome;
        private DataGridViewTextBoxColumn Descrição;
    }
}