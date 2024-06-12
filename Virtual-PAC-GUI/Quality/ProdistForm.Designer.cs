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
            components = new System.ComponentModel.Container();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            panel1 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            CbxFluct = new CheckBox();
            CbxUnbalance = new CheckBox();
            CbxHarmonico = new CheckBox();
            CbxFP = new CheckBox();
            CbxVarVolt = new CheckBox();
            PnMain = new Panel();
            PbProg = new ProgressBar();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            tmUpt = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            PnMain.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(PnMain, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 61F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 477F));
            tableLayoutPanel1.Size = new Size(1068, 636);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Padding = new Padding(0, 0, 0, 12);
            label1.Size = new Size(100, 42);
            label1.TabIndex = 0;
            label1.Text = "PRODIST";
            // 
            // panel1
            // 
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(tableLayoutPanel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 45);
            panel1.Margin = new Padding(3, 3, 3, 12);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(10, 0, 10, 0);
            panel1.Size = new Size(1062, 46);
            panel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 5;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Controls.Add(CbxFluct, 5, 0);
            tableLayoutPanel2.Controls.Add(CbxUnbalance, 3, 0);
            tableLayoutPanel2.Controls.Add(CbxHarmonico, 2, 0);
            tableLayoutPanel2.Controls.Add(CbxFP, 1, 0);
            tableLayoutPanel2.Controls.Add(CbxVarVolt, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Left;
            tableLayoutPanel2.Location = new Point(10, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 52.63158F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 47.36842F));
            tableLayoutPanel2.Size = new Size(719, 46);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // CbxFluct
            // 
            CbxFluct.Appearance = Appearance.Button;
            CbxFluct.AutoSize = true;
            CbxFluct.Dock = DockStyle.Fill;
            CbxFluct.FlatAppearance.CheckedBackColor = Color.FromArgb(31, 45, 56);
            CbxFluct.FlatStyle = FlatStyle.Flat;
            CbxFluct.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            CbxFluct.Location = new Point(555, 3);
            CbxFluct.Name = "CbxFluct";
            CbxFluct.Size = new Size(161, 40);
            CbxFluct.TabIndex = 6;
            CbxFluct.Text = "Flutuação de Tensão";
            CbxFluct.TextAlign = ContentAlignment.MiddleCenter;
            CbxFluct.UseVisualStyleBackColor = true;
            // 
            // CbxUnbalance
            // 
            CbxUnbalance.Appearance = Appearance.Button;
            CbxUnbalance.AutoSize = true;
            CbxUnbalance.Dock = DockStyle.Fill;
            CbxUnbalance.FlatAppearance.CheckedBackColor = Color.FromArgb(31, 45, 56);
            CbxUnbalance.FlatStyle = FlatStyle.Flat;
            CbxUnbalance.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            CbxUnbalance.Location = new Point(381, 3);
            CbxUnbalance.Name = "CbxUnbalance";
            CbxUnbalance.Size = new Size(168, 40);
            CbxUnbalance.TabIndex = 4;
            CbxUnbalance.Text = "Desequilíbrio de Tensão";
            CbxUnbalance.TextAlign = ContentAlignment.MiddleCenter;
            CbxUnbalance.UseVisualStyleBackColor = true;
            // 
            // CbxHarmonico
            // 
            CbxHarmonico.Appearance = Appearance.Button;
            CbxHarmonico.AutoSize = true;
            CbxHarmonico.Dock = DockStyle.Fill;
            CbxHarmonico.FlatAppearance.CheckedBackColor = Color.FromArgb(31, 45, 56);
            CbxHarmonico.FlatStyle = FlatStyle.Flat;
            CbxHarmonico.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            CbxHarmonico.Location = new Point(281, 3);
            CbxHarmonico.Name = "CbxHarmonico";
            CbxHarmonico.Size = new Size(94, 40);
            CbxHarmonico.TabIndex = 3;
            CbxHarmonico.Text = "Harmônicos";
            CbxHarmonico.TextAlign = ContentAlignment.MiddleCenter;
            CbxHarmonico.UseVisualStyleBackColor = true;
            // 
            // CbxFP
            // 
            CbxFP.Appearance = Appearance.Button;
            CbxFP.AutoSize = true;
            CbxFP.Dock = DockStyle.Fill;
            CbxFP.FlatAppearance.CheckedBackColor = Color.FromArgb(31, 45, 56);
            CbxFP.FlatStyle = FlatStyle.Flat;
            CbxFP.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            CbxFP.Location = new Point(147, 3);
            CbxFP.Name = "CbxFP";
            CbxFP.Size = new Size(128, 40);
            CbxFP.TabIndex = 2;
            CbxFP.Text = "Fator de Potência";
            CbxFP.TextAlign = ContentAlignment.MiddleCenter;
            CbxFP.UseVisualStyleBackColor = true;
            // 
            // CbxVarVolt
            // 
            CbxVarVolt.Appearance = Appearance.Button;
            CbxVarVolt.AutoSize = true;
            CbxVarVolt.Checked = true;
            CbxVarVolt.CheckState = CheckState.Checked;
            CbxVarVolt.Dock = DockStyle.Fill;
            CbxVarVolt.FlatAppearance.CheckedBackColor = Color.FromArgb(31, 45, 56);
            CbxVarVolt.FlatStyle = FlatStyle.Flat;
            CbxVarVolt.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            CbxVarVolt.Location = new Point(3, 3);
            CbxVarVolt.Name = "CbxVarVolt";
            CbxVarVolt.Size = new Size(138, 40);
            CbxVarVolt.TabIndex = 1;
            CbxVarVolt.Text = "Variação de Tensão";
            CbxVarVolt.TextAlign = ContentAlignment.MiddleCenter;
            CbxVarVolt.UseVisualStyleBackColor = true;
            CbxVarVolt.CheckStateChanged += CbxVarVolt_CheckStateChanged;
            // 
            // PnMain
            // 
            PnMain.Controls.Add(PbProg);
            PnMain.Dock = DockStyle.Fill;
            PnMain.Location = new Point(3, 106);
            PnMain.Name = "PnMain";
            PnMain.Size = new Size(1062, 527);
            PnMain.TabIndex = 3;
            // 
            // PbProg
            // 
            PbProg.Location = new Point(732, 3);
            PbProg.Name = "PbProg";
            PbProg.Size = new Size(321, 16);
            PbProg.TabIndex = 0;
            PbProg.Visible = false;
            // 
            // tmUpt
            // 
            tmUpt.Tick += tmUpt_Tick;
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
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            PnMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Panel panel1;
        private CheckBox CbxVarVolt;
        private TableLayoutPanel tableLayoutPanel2;
        private CheckBox CbxFluct;
        private CheckBox CbxUnbalance;
        private CheckBox CbxHarmonico;
        private CheckBox CbxFP;
        private Panel PnMain;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ProgressBar PbProg;
        private System.Windows.Forms.Timer tmUpt;
    }
}