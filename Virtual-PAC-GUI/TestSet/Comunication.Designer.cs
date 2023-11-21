namespace TestSet
{
    partial class Comunication
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
            panel1 = new Panel();
            BtnConnect = new Button();
            panel2 = new Panel();
            groupBox1 = new GroupBox();
            TbPort = new TextBox();
            label4 = new Label();
            TbIP = new TextBox();
            label3 = new Label();
            TbSenha = new TextBox();
            label2 = new Label();
            TbName = new TextBox();
            label1 = new Label();
            TimerCon = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(BtnConnect);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(31, 22, 0, 0);
            panel1.Size = new Size(361, 614);
            panel1.TabIndex = 0;
            // 
            // BtnConnect
            // 
            BtnConnect.Dock = DockStyle.Top;
            BtnConnect.FlatAppearance.BorderSize = 0;
            BtnConnect.FlatStyle = FlatStyle.Flat;
            BtnConnect.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnConnect.Location = new Point(31, 279);
            BtnConnect.Margin = new Padding(3, 2, 3, 2);
            BtnConnect.Name = "BtnConnect";
            BtnConnect.Size = new Size(330, 48);
            BtnConnect.TabIndex = 3;
            BtnConnect.Text = "Conectar";
            BtnConnect.UseVisualStyleBackColor = true;
            BtnConnect.Click += BtnConnect_Click;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(31, 262);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(330, 17);
            panel2.TabIndex = 4;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(TbPort);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(TbIP);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(TbSenha);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(TbName);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.AliceBlue;
            groupBox1.Location = new Point(31, 22);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(330, 240);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Comunicação";
            // 
            // TbPort
            // 
            TbPort.BackColor = Color.FromArgb(31, 45, 56);
            TbPort.BorderStyle = BorderStyle.None;
            TbPort.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbPort.ForeColor = Color.Lavender;
            TbPort.Location = new Point(102, 189);
            TbPort.Margin = new Padding(3, 2, 3, 2);
            TbPort.Name = "TbPort";
            TbPort.Size = new Size(197, 25);
            TbPort.TabIndex = 8;
            TbPort.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.Lavender;
            label4.Location = new Point(37, 189);
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
            TbIP.Location = new Point(71, 146);
            TbIP.Margin = new Padding(3, 2, 3, 2);
            TbIP.Name = "TbIP";
            TbIP.Size = new Size(228, 25);
            TbIP.TabIndex = 6;
            TbIP.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Lavender;
            label3.Location = new Point(37, 146);
            label3.Name = "label3";
            label3.Size = new Size(28, 25);
            label3.TabIndex = 5;
            label3.Text = "IP";
            // 
            // TbSenha
            // 
            TbSenha.BackColor = Color.FromArgb(31, 45, 56);
            TbSenha.BorderStyle = BorderStyle.None;
            TbSenha.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbSenha.ForeColor = Color.Lavender;
            TbSenha.Location = new Point(112, 104);
            TbSenha.Margin = new Padding(3, 2, 3, 2);
            TbSenha.Name = "TbSenha";
            TbSenha.Size = new Size(186, 25);
            TbSenha.TabIndex = 4;
            TbSenha.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Lavender;
            label2.Location = new Point(37, 104);
            label2.Name = "label2";
            label2.Size = new Size(64, 25);
            label2.TabIndex = 3;
            label2.Text = "Senha";
            // 
            // TbName
            // 
            TbName.BackColor = Color.FromArgb(31, 45, 56);
            TbName.BorderStyle = BorderStyle.None;
            TbName.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbName.ForeColor = Color.Lavender;
            TbName.Location = new Point(112, 61);
            TbName.Margin = new Padding(3, 2, 3, 2);
            TbName.Name = "TbName";
            TbName.Size = new Size(186, 25);
            TbName.TabIndex = 2;
            TbName.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Lavender;
            label1.Location = new Point(37, 61);
            label1.Name = "label1";
            label1.Size = new Size(63, 25);
            label1.TabIndex = 1;
            label1.Text = "Nome";
            // 
            // TimerCon
            // 
            TimerCon.Interval = 1000;
            TimerCon.Tick += TimerConnection_Tick;
            // 
            // Comunication
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1072, 614);
            Controls.Add(panel1);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Comunication";
            Text = "Connection";
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox TbIP;
        private Label label3;
        private TextBox TbSenha;
        private Label label2;
        private TextBox TbName;
        private Button BtnConnect;
        private Panel panel2;
        private TextBox TbPort;
        private Label label4;
        private System.Windows.Forms.Timer TimerCon;
    }
}