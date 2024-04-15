namespace Quality
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
            TbUser = new TextBox();
            label9 = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            TbPort = new TextBox();
            TbIpAddress = new TextBox();
            TbPassword = new TextBox();
            BtnConnect = new Button();
            label4 = new Label();
            button1 = new Button();
            button2 = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // TbUser
            // 
            TbUser.BackColor = Color.FromArgb(31, 45, 56);
            TbUser.BorderStyle = BorderStyle.FixedSingle;
            TbUser.Dock = DockStyle.Fill;
            TbUser.Font = new Font("Segoe UI", 13F);
            TbUser.ForeColor = Color.Lavender;
            TbUser.Location = new Point(97, 5);
            TbUser.Margin = new Padding(0, 5, 0, 5);
            TbUser.Name = "TbUser";
            TbUser.Size = new Size(316, 31);
            TbUser.TabIndex = 19;
            TbUser.TextAlign = HorizontalAlignment.Center;
            TbUser.Validated += TbUser_TextChanged;
            // 
            // label9
            // 
            label9.BackColor = Color.FromArgb(31, 45, 56);
            label9.BorderStyle = BorderStyle.FixedSingle;
            label9.FlatStyle = FlatStyle.Popup;
            label9.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold);
            label9.Location = new Point(0, 87);
            label9.Margin = new Padding(0, 5, 0, 5);
            label9.Name = "label9";
            label9.Size = new Size(96, 31);
            label9.TabIndex = 18;
            label9.Text = "IP";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(31, 45, 56);
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.FlatStyle = FlatStyle.Popup;
            label1.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold);
            label1.Location = new Point(0, 128);
            label1.Margin = new Padding(0, 5, 0, 5);
            label1.Name = "label1";
            label1.Size = new Size(96, 31);
            label1.TabIndex = 20;
            label1.Text = "Porta";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(31, 45, 56);
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.FlatStyle = FlatStyle.Popup;
            label2.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold);
            label2.Location = new Point(0, 5);
            label2.Margin = new Padding(0, 5, 0, 5);
            label2.Name = "label2";
            label2.Size = new Size(96, 31);
            label2.TabIndex = 21;
            label2.Text = "Usuário";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(31, 45, 56);
            label3.BorderStyle = BorderStyle.FixedSingle;
            label3.FlatStyle = FlatStyle.Popup;
            label3.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold);
            label3.Location = new Point(0, 46);
            label3.Margin = new Padding(0, 5, 0, 5);
            label3.Name = "label3";
            label3.Size = new Size(96, 31);
            label3.TabIndex = 22;
            label3.Text = "Senha";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.5294113F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76.47059F));
            tableLayoutPanel1.Controls.Add(TbPort, 1, 3);
            tableLayoutPanel1.Controls.Add(TbIpAddress, 1, 2);
            tableLayoutPanel1.Controls.Add(TbPassword, 1, 1);
            tableLayoutPanel1.Controls.Add(label2, 0, 0);
            tableLayoutPanel1.Controls.Add(label3, 0, 1);
            tableLayoutPanel1.Controls.Add(TbUser, 1, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 3);
            tableLayoutPanel1.Controls.Add(label9, 0, 2);
            tableLayoutPanel1.Location = new Point(13, 67);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(413, 164);
            tableLayoutPanel1.TabIndex = 24;
            // 
            // TbPort
            // 
            TbPort.BackColor = Color.FromArgb(31, 45, 56);
            TbPort.BorderStyle = BorderStyle.FixedSingle;
            TbPort.Dock = DockStyle.Fill;
            TbPort.Font = new Font("Segoe UI", 13F);
            TbPort.ForeColor = Color.Lavender;
            TbPort.Location = new Point(97, 128);
            TbPort.Margin = new Padding(0, 5, 0, 5);
            TbPort.Name = "TbPort";
            TbPort.Size = new Size(316, 31);
            TbPort.TabIndex = 25;
            TbPort.TextAlign = HorizontalAlignment.Center;
            TbPort.Validated += TbUser_TextChanged;
            // 
            // TbIpAddress
            // 
            TbIpAddress.BackColor = Color.FromArgb(31, 45, 56);
            TbIpAddress.BorderStyle = BorderStyle.FixedSingle;
            TbIpAddress.Dock = DockStyle.Fill;
            TbIpAddress.Font = new Font("Segoe UI", 13F);
            TbIpAddress.ForeColor = Color.Lavender;
            TbIpAddress.Location = new Point(97, 87);
            TbIpAddress.Margin = new Padding(0, 5, 0, 5);
            TbIpAddress.Name = "TbIpAddress";
            TbIpAddress.Size = new Size(316, 31);
            TbIpAddress.TabIndex = 24;
            TbIpAddress.TextAlign = HorizontalAlignment.Center;
            TbIpAddress.Validated += TbUser_TextChanged;
            // 
            // TbPassword
            // 
            TbPassword.BackColor = Color.FromArgb(31, 45, 56);
            TbPassword.BorderStyle = BorderStyle.FixedSingle;
            TbPassword.Dock = DockStyle.Fill;
            TbPassword.Font = new Font("Segoe UI", 13F);
            TbPassword.ForeColor = Color.Lavender;
            TbPassword.Location = new Point(97, 46);
            TbPassword.Margin = new Padding(0, 5, 0, 5);
            TbPassword.Name = "TbPassword";
            TbPassword.Size = new Size(316, 31);
            TbPassword.TabIndex = 23;
            TbPassword.TextAlign = HorizontalAlignment.Center;
            TbPassword.Validated += TbUser_TextChanged;
            // 
            // BtnConnect
            // 
            BtnConnect.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnConnect.AutoSize = true;
            BtnConnect.FlatStyle = FlatStyle.Popup;
            BtnConnect.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnConnect.Location = new Point(97, 241);
            BtnConnect.Name = "BtnConnect";
            BtnConnect.Size = new Size(255, 33);
            BtnConnect.TabIndex = 25;
            BtnConnect.Text = "Conectar com Servidor";
            BtnConnect.UseVisualStyleBackColor = true;
            BtnConnect.Click += BtnConnect_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label4.Location = new Point(139, 27);
            label4.Name = "label4";
            label4.Size = new Size(160, 32);
            label4.TabIndex = 26;
            label4.Text = "Comunicação";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.AutoSize = true;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(13, 297);
            button1.Name = "button1";
            button1.Size = new Size(174, 33);
            button1.TabIndex = 27;
            button1.Text = "Salvar Configuração";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.AutoSize = true;
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.Location = new Point(232, 297);
            button2.Name = "button2";
            button2.Size = new Size(194, 33);
            button2.TabIndex = 28;
            button2.Text = "Carregar Configuração";
            button2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoSize = true;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(label4);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(BtnConnect);
            panel1.Location = new Point(306, 46);
            panel1.Margin = new Padding(10);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(10);
            panel1.Size = new Size(441, 343);
            panel1.TabIndex = 29;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1068, 636);
            panel2.TabIndex = 30;
            // 
            // GeneralForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1068, 636);
            Controls.Add(panel2);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Name = "GeneralForm";
            Text = "GeneralForm";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox TbUser;
        private Label label9;
        private Label label1;
        private Label label2;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox TbPort;
        private TextBox TbIpAddress;
        private TextBox TbPassword;
        private Button BtnConnect;
        private Label label4;
        private Button button1;
        private Button button2;
        private Panel panel1;
        private Panel panel2;
    }
}