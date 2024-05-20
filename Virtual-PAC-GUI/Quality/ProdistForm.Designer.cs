namespace Quality
{
    partial class ProdistForm
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
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            panel1 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            checkBox5 = new CheckBox();
            checkBox4 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            CbxVarVolt = new CheckBox();
            PnMain = new Panel();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(PnMain, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 46.42857F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 53.57143F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 484F));
            tableLayoutPanel1.Size = new Size(1068, 636);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 30);
            label1.TabIndex = 0;
            label1.Text = "PRODIST";
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(tableLayoutPanel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 73);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(10, 0, 10, 0);
            panel1.Size = new Size(1062, 75);
            panel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(checkBox5, 0, 1);
            tableLayoutPanel2.Controls.Add(checkBox4, 0, 1);
            tableLayoutPanel2.Controls.Add(checkBox3, 3, 0);
            tableLayoutPanel2.Controls.Add(checkBox2, 2, 0);
            tableLayoutPanel2.Controls.Add(checkBox1, 1, 0);
            tableLayoutPanel2.Controls.Add(CbxVarVolt, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Left;
            tableLayoutPanel2.Location = new Point(10, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 52.63158F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 47.36842F));
            tableLayoutPanel2.Size = new Size(595, 75);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // checkBox5
            // 
            checkBox5.Appearance = Appearance.Button;
            checkBox5.AutoSize = true;
            checkBox5.Dock = DockStyle.Fill;
            checkBox5.FlatAppearance.CheckedBackColor = Color.FromArgb(31, 45, 56);
            checkBox5.FlatStyle = FlatStyle.Flat;
            checkBox5.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            checkBox5.Location = new Point(3, 42);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(146, 30);
            checkBox5.TabIndex = 6;
            checkBox5.Text = "Flutuação de Tensão";
            checkBox5.TextAlign = ContentAlignment.MiddleCenter;
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.Appearance = Appearance.Button;
            checkBox4.AutoSize = true;
            checkBox4.Dock = DockStyle.Fill;
            checkBox4.FlatAppearance.CheckedBackColor = Color.FromArgb(31, 45, 56);
            checkBox4.FlatStyle = FlatStyle.Flat;
            checkBox4.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            checkBox4.Location = new Point(155, 42);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(163, 30);
            checkBox4.TabIndex = 5;
            checkBox4.Text = "Variação de Frequência";
            checkBox4.TextAlign = ContentAlignment.MiddleCenter;
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.Appearance = Appearance.Button;
            checkBox3.AutoSize = true;
            checkBox3.Dock = DockStyle.Fill;
            checkBox3.FlatAppearance.CheckedBackColor = Color.FromArgb(31, 45, 56);
            checkBox3.FlatStyle = FlatStyle.Flat;
            checkBox3.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            checkBox3.Location = new Point(424, 3);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(168, 33);
            checkBox3.TabIndex = 4;
            checkBox3.Text = "Desequilíbrio de Tensão";
            checkBox3.TextAlign = ContentAlignment.MiddleCenter;
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.Appearance = Appearance.Button;
            checkBox2.AutoSize = true;
            checkBox2.Dock = DockStyle.Fill;
            checkBox2.FlatAppearance.CheckedBackColor = Color.FromArgb(31, 45, 56);
            checkBox2.FlatStyle = FlatStyle.Flat;
            checkBox2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            checkBox2.Location = new Point(324, 3);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(94, 33);
            checkBox2.TabIndex = 3;
            checkBox2.Text = "Harmônicos";
            checkBox2.TextAlign = ContentAlignment.MiddleCenter;
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.Appearance = Appearance.Button;
            checkBox1.AutoSize = true;
            checkBox1.Dock = DockStyle.Fill;
            checkBox1.FlatAppearance.CheckedBackColor = Color.FromArgb(31, 45, 56);
            checkBox1.FlatStyle = FlatStyle.Flat;
            checkBox1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            checkBox1.Location = new Point(155, 3);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(163, 33);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "Fator de Potência";
            checkBox1.TextAlign = ContentAlignment.MiddleCenter;
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // CbxVarVolt
            // 
            CbxVarVolt.Appearance = Appearance.Button;
            CbxVarVolt.AutoSize = true;
            CbxVarVolt.Dock = DockStyle.Fill;
            CbxVarVolt.FlatAppearance.CheckedBackColor = Color.FromArgb(31, 45, 56);
            CbxVarVolt.FlatStyle = FlatStyle.Flat;
            CbxVarVolt.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            CbxVarVolt.Location = new Point(3, 3);
            CbxVarVolt.Name = "CbxVarVolt";
            CbxVarVolt.Size = new Size(146, 33);
            CbxVarVolt.TabIndex = 1;
            CbxVarVolt.Text = "Variação de Tensão";
            CbxVarVolt.TextAlign = ContentAlignment.MiddleCenter;
            CbxVarVolt.UseVisualStyleBackColor = true;
            CbxVarVolt.CheckStateChanged += CbxVarVolt_CheckStateChanged;
            // 
            // PnMain
            // 
            PnMain.Dock = DockStyle.Fill;
            PnMain.Location = new Point(3, 154);
            PnMain.Name = "PnMain";
            PnMain.Size = new Size(1062, 479);
            PnMain.TabIndex = 3;
            // 
            // ProdistForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1068, 636);
            Controls.Add(tableLayoutPanel1);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProdistForm";
            Text = "ProdistForm";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Panel panel1;
        private CheckBox CbxVarVolt;
        private TableLayoutPanel tableLayoutPanel2;
        private CheckBox checkBox5;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Panel PnMain;
    }
}