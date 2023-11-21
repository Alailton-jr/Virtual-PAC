namespace Ied
{
    partial class GeneralForm
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
            TbFrequency = new TextBox();
            label1 = new Label();
            TbCurNominal = new TextBox();
            label2 = new Label();
            groupBox1 = new GroupBox();
            TbPort = new TextBox();
            label4 = new Label();
            TbIP = new TextBox();
            label5 = new Label();
            groupBox2 = new GroupBox();
            label3 = new Label();
            TbVolNominal = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // TbFrequency
            // 
            TbFrequency.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbFrequency.BackColor = Color.FromArgb(31, 45, 56);
            TbFrequency.BorderStyle = BorderStyle.None;
            TbFrequency.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbFrequency.ForeColor = Color.Lavender;
            TbFrequency.Location = new Point(150, 54);
            TbFrequency.Margin = new Padding(3, 2, 3, 2);
            TbFrequency.Name = "TbFrequency";
            TbFrequency.Size = new Size(186, 25);
            TbFrequency.TabIndex = 4;
            TbFrequency.TextAlign = HorizontalAlignment.Center;
            TbFrequency.Validated += TbName_Validated;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Lavender;
            label1.Location = new Point(39, 54);
            label1.Name = "label1";
            label1.Size = new Size(105, 25);
            label1.TabIndex = 3;
            label1.Text = "Frequência";
            // 
            // TbCurNominal
            // 
            TbCurNominal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbCurNominal.BackColor = Color.FromArgb(31, 45, 56);
            TbCurNominal.BorderStyle = BorderStyle.None;
            TbCurNominal.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbCurNominal.ForeColor = Color.Lavender;
            TbCurNominal.Location = new Point(208, 100);
            TbCurNominal.Margin = new Padding(3, 2, 3, 2);
            TbCurNominal.Name = "TbCurNominal";
            TbCurNominal.Size = new Size(128, 25);
            TbCurNominal.TabIndex = 6;
            TbCurNominal.TextAlign = HorizontalAlignment.Center;
            TbCurNominal.Validated += TbName_Validated;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Lavender;
            label2.Location = new Point(39, 101);
            label2.Name = "label2";
            label2.Size = new Size(163, 25);
            label2.TabIndex = 5;
            label2.Text = "Corrente Nominal";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(TbPort);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(TbIP);
            groupBox1.Controls.Add(label5);
            groupBox1.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.AliceBlue;
            groupBox1.Location = new Point(39, 66);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(330, 193);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Comunicação";
            // 
            // TbPort
            // 
            TbPort.BackColor = Color.FromArgb(31, 45, 56);
            TbPort.BorderStyle = BorderStyle.None;
            TbPort.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbPort.ForeColor = Color.Lavender;
            TbPort.Location = new Point(94, 101);
            TbPort.Margin = new Padding(3, 2, 3, 2);
            TbPort.Name = "TbPort";
            TbPort.Size = new Size(197, 25);
            TbPort.TabIndex = 8;
            TbPort.TextAlign = HorizontalAlignment.Center;
            TbPort.Validated += TbName_Validated;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.Lavender;
            label4.Location = new Point(29, 100);
            label4.Name = "label4";
            label4.Size = new Size(56, 25);
            label4.TabIndex = 7;
            label4.Text = "Porta";
            // 
            // TbIP
            // 
            TbIP.BackColor = Color.FromArgb(31, 45, 56);
            TbIP.BorderStyle = BorderStyle.None;
            TbIP.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbIP.ForeColor = Color.Lavender;
            TbIP.Location = new Point(63, 58);
            TbIP.Margin = new Padding(3, 2, 3, 2);
            TbIP.Name = "TbIP";
            TbIP.Size = new Size(228, 25);
            TbIP.TabIndex = 6;
            TbIP.TextAlign = HorizontalAlignment.Center;
            TbIP.Validated += TbName_Validated;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.Lavender;
            label5.Location = new Point(29, 57);
            label5.Name = "label5";
            label5.Size = new Size(28, 25);
            label5.TabIndex = 5;
            label5.Text = "IP";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(TbVolNominal);
            groupBox2.Controls.Add(TbFrequency);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(TbCurNominal);
            groupBox2.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox2.ForeColor = Color.AliceBlue;
            groupBox2.Location = new Point(434, 66);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(359, 193);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            groupBox2.Text = "Parâmetros Elétricos";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Lavender;
            label3.Location = new Point(39, 140);
            label3.Name = "label3";
            label3.Size = new Size(147, 25);
            label3.TabIndex = 7;
            label3.Text = "Tensão Nominal";
            // 
            // TbVolNominal
            // 
            TbVolNominal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TbVolNominal.BackColor = Color.FromArgb(31, 45, 56);
            TbVolNominal.BorderStyle = BorderStyle.None;
            TbVolNominal.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbVolNominal.ForeColor = Color.Lavender;
            TbVolNominal.Location = new Point(192, 140);
            TbVolNominal.Margin = new Padding(3, 2, 3, 2);
            TbVolNominal.Name = "TbVolNominal";
            TbVolNominal.Size = new Size(144, 25);
            TbVolNominal.TabIndex = 8;
            TbVolNominal.TextAlign = HorizontalAlignment.Center;
            TbVolNominal.Validated += TbName_Validated;
            // 
            // GeneralForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1264, 616);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Name = "GeneralForm";
            Text = "GeneralForm";
            Load += GeneralForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox TbFrequency;
        private Label label1;
        private TextBox TbCurNominal;
        private Label label2;
        private GroupBox groupBox1;
        private TextBox TbPort;
        private Label label4;
        private TextBox TbIP;
        private Label label5;
        private GroupBox groupBox2;
        private Label label3;
        private TextBox TbVolNominal;
    }
}