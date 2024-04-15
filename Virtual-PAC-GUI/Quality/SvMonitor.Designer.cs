namespace Quality
{
    partial class SvMonitor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LbVtcd = new Label();
            LbVtld = new Label();
            label1 = new Label();
            label2 = new Label();
            PnMain = new Panel();
            label10 = new Label();
            LbFluctiations = new Label();
            label8 = new Label();
            LbUnbalance = new Label();
            label6 = new Label();
            LbTransient = new Label();
            label4 = new Label();
            LbHarm = new Label();
            LbSVID = new Label();
            PnMain.SuspendLayout();
            SuspendLayout();
            // 
            // LbVtcd
            // 
            LbVtcd.Image = Properties.Resources.running;
            LbVtcd.Location = new Point(3, 41);
            LbVtcd.Margin = new Padding(5, 0, 5, 0);
            LbVtcd.Name = "LbVtcd";
            LbVtcd.Size = new Size(28, 28);
            LbVtcd.TabIndex = 0;
            // 
            // LbVtld
            // 
            LbVtld.Image = Properties.Resources.stoped;
            LbVtld.Location = new Point(3, 72);
            LbVtld.Margin = new Padding(5, 0, 5, 0);
            LbVtld.Name = "LbVtld";
            LbVtld.Size = new Size(28, 28);
            LbVtld.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            label1.Location = new Point(31, 43);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(54, 23);
            label1.TabIndex = 2;
            label1.Text = "VTCD";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            label2.Location = new Point(31, 74);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(54, 23);
            label2.TabIndex = 3;
            label2.Text = "VTCD";
            // 
            // PnMain
            // 
            PnMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            PnMain.BackColor = Color.FromArgb(40, 58, 73);
            PnMain.Controls.Add(label10);
            PnMain.Controls.Add(LbFluctiations);
            PnMain.Controls.Add(label8);
            PnMain.Controls.Add(LbUnbalance);
            PnMain.Controls.Add(label6);
            PnMain.Controls.Add(LbTransient);
            PnMain.Controls.Add(label4);
            PnMain.Controls.Add(LbHarm);
            PnMain.Controls.Add(LbSVID);
            PnMain.Controls.Add(label1);
            PnMain.Controls.Add(label2);
            PnMain.Controls.Add(LbVtcd);
            PnMain.Controls.Add(LbVtld);
            PnMain.Location = new Point(5, 5);
            PnMain.Margin = new Padding(0);
            PnMain.Name = "PnMain";
            PnMain.Padding = new Padding(5);
            PnMain.Size = new Size(155, 228);
            PnMain.TabIndex = 4;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            label10.Location = new Point(31, 198);
            label10.Margin = new Padding(5, 0, 5, 0);
            label10.Name = "label10";
            label10.Size = new Size(87, 23);
            label10.TabIndex = 12;
            label10.Text = "Flutuação";
            // 
            // LbFluctiations
            // 
            LbFluctiations.Image = Properties.Resources.running;
            LbFluctiations.Location = new Point(3, 196);
            LbFluctiations.Margin = new Padding(5, 0, 5, 0);
            LbFluctiations.Name = "LbFluctiations";
            LbFluctiations.Size = new Size(28, 28);
            LbFluctiations.TabIndex = 11;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            label8.Location = new Point(31, 167);
            label8.Margin = new Padding(5, 0, 5, 0);
            label8.Name = "label8";
            label8.Size = new Size(101, 23);
            label8.TabIndex = 10;
            label8.Text = "Desbalanço";
            // 
            // LbUnbalance
            // 
            LbUnbalance.Image = Properties.Resources.running;
            LbUnbalance.Location = new Point(3, 165);
            LbUnbalance.Margin = new Padding(5, 0, 5, 0);
            LbUnbalance.Name = "LbUnbalance";
            LbUnbalance.Size = new Size(28, 28);
            LbUnbalance.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            label6.Location = new Point(31, 136);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(103, 23);
            label6.TabIndex = 8;
            label6.Text = "Transitórios";
            // 
            // LbTransient
            // 
            LbTransient.Image = Properties.Resources.running;
            LbTransient.Location = new Point(3, 134);
            LbTransient.Margin = new Padding(5, 0, 5, 0);
            LbTransient.Name = "LbTransient";
            LbTransient.Size = new Size(28, 28);
            LbTransient.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            label4.Location = new Point(31, 105);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(105, 23);
            label4.TabIndex = 6;
            label4.Text = "Harmônicos";
            // 
            // LbHarm
            // 
            LbHarm.Image = Properties.Resources.running;
            LbHarm.Location = new Point(3, 103);
            LbHarm.Margin = new Padding(5, 0, 5, 0);
            LbHarm.Name = "LbHarm";
            LbHarm.Size = new Size(28, 28);
            LbHarm.TabIndex = 5;
            // 
            // LbSVID
            // 
            LbSVID.BackColor = Color.FromArgb(31, 45, 56);
            LbSVID.Dock = DockStyle.Top;
            LbSVID.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            LbSVID.Location = new Point(5, 5);
            LbSVID.Margin = new Padding(3, 3, 3, 0);
            LbSVID.Name = "LbSVID";
            LbSVID.Size = new Size(145, 32);
            LbSVID.TabIndex = 4;
            LbSVID.Text = "SV ID";
            LbSVID.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SvMonitor
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(31, 45, 56);
            Controls.Add(PnMain);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = Color.Lavender;
            Margin = new Padding(5);
            Name = "SvMonitor";
            Padding = new Padding(5);
            Size = new Size(165, 238);
            PnMain.ResumeLayout(false);
            PnMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label LbVtcd;
        private Label LbVtld;
        private Label label1;
        private Label label2;
        private Panel PnMain;
        private Label label6;
        private Label LbTransient;
        private Label label4;
        private Label LbHarm;
        private Label LbSVID;
        private Label label10;
        private Label LbFluctiations;
        private Label label8;
        private Label LbUnbalance;
    }
}
