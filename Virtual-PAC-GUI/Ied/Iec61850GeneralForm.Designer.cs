namespace Ied
{
    partial class Iec61850GeneralForm
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
            BtnImportScl = new Button();
            panel1 = new Panel();
            DgvSend = new DataGridView();
            Column1 = new DataGridViewButtonColumn();
            Nome = new DataGridViewTextBoxColumn();
            Descrição = new DataGridViewTextBoxColumn();
            label1 = new Label();
            panel2 = new Panel();
            panel4 = new Panel();
            BtnExport = new Button();
            panel5 = new Panel();
            panel3 = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvSend).BeginInit();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // BtnImportScl
            // 
            BtnImportScl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnImportScl.FlatAppearance.BorderColor = Color.White;
            BtnImportScl.FlatAppearance.BorderSize = 0;
            BtnImportScl.FlatStyle = FlatStyle.Popup;
            BtnImportScl.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnImportScl.Location = new Point(77, 313);
            BtnImportScl.Margin = new Padding(3, 2, 3, 2);
            BtnImportScl.Name = "BtnImportScl";
            BtnImportScl.Size = new Size(200, 33);
            BtnImportScl.TabIndex = 7;
            BtnImportScl.Text = "Importar SCL";
            BtnImportScl.UseVisualStyleBackColor = true;
            BtnImportScl.Click += BtnImportScl_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(DgvSend);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(70, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(20);
            panel1.Size = new Size(630, 616);
            panel1.TabIndex = 8;
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
            DgvSend.Columns.AddRange(new DataGridViewColumn[] { Column1, Nome, Descrição });
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
            DgvSend.Location = new Point(20, 81);
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
            DgvSend.Size = new Size(590, 515);
            DgvSend.TabIndex = 2;
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
            Nome.HeaderText = "Nome";
            Nome.MinimumWidth = 6;
            Nome.Name = "Nome";
            Nome.ReadOnly = true;
            // 
            // Descrição
            // 
            Descrição.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Descrição.DefaultCellStyle = dataGridViewCellStyle4;
            Descrição.HeaderText = "Caminho";
            Descrição.MinimumWidth = 6;
            Descrição.Name = "Descrição";
            Descrição.ReadOnly = true;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(20, 20);
            label1.Name = "label1";
            label1.Size = new Size(590, 61);
            label1.TabIndex = 3;
            label1.Text = "Arquivos Salvos";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel1);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1060, 616);
            panel2.TabIndex = 9;
            // 
            // panel4
            // 
            panel4.Controls.Add(BtnExport);
            panel4.Controls.Add(BtnImportScl);
            panel4.Controls.Add(panel5);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(700, 0);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(80, 20, 80, 20);
            panel4.Size = new Size(360, 616);
            panel4.TabIndex = 10;
            // 
            // BtnExport
            // 
            BtnExport.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnExport.FlatAppearance.BorderColor = Color.White;
            BtnExport.FlatAppearance.BorderSize = 0;
            BtnExport.FlatStyle = FlatStyle.Popup;
            BtnExport.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnExport.Location = new Point(77, 226);
            BtnExport.Margin = new Padding(3, 2, 3, 2);
            BtnExport.Name = "BtnExport";
            BtnExport.Size = new Size(200, 33);
            BtnExport.TabIndex = 9;
            BtnExport.Text = "Exportar SCL";
            BtnExport.UseVisualStyleBackColor = true;
            BtnExport.Click += BtnExport_Click;
            // 
            // panel5
            // 
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(80, 20);
            panel5.Name = "panel5";
            panel5.Size = new Size(200, 100);
            panel5.TabIndex = 8;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(70, 616);
            panel3.TabIndex = 9;
            // 
            // Iec61850GeneralForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1060, 616);
            Controls.Add(panel2);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Name = "Iec61850GeneralForm";
            Text = "Iec61850GeneralForm";
            Load += Iec61850GeneralForm_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DgvSend).EndInit();
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button BtnImportScl;
        private Panel panel1;
        private DataGridView DgvSend;
        private Panel panel2;
        private Panel panel3;
        private DataGridViewButtonColumn Column1;
        private DataGridViewTextBoxColumn Nome;
        private DataGridViewTextBoxColumn Descrição;
        private Label label1;
        private Panel panel4;
        private Button BtnExport;
        private Panel panel5;
    }
}