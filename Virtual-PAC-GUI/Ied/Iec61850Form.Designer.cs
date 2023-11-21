namespace Ied
{
    partial class Iec61850Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Iec61850Form));
            panel1 = new Panel();
            button2 = new Button();
            BtnSv = new Button();
            BtnGo = new Button();
            BtnDataSet = new Button();
            panel3 = new Panel();
            BtnIecHome = new Button();
            panel2 = new Panel();
            PnChields = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.BackColor = Color.FromArgb(1, 22, 39);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(BtnSv);
            panel1.Controls.Add(BtnGo);
            panel1.Controls.Add(BtnDataSet);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(BtnIecHome);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(0, 15, 0, 0);
            panel1.Size = new Size(200, 616);
            panel1.TabIndex = 1;
            panel1.MouseDown += panelTitleBar_MouseDown;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Top;
            button2.FlatAppearance.MouseDownBackColor = Color.Black;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            button2.ForeColor = Color.Lavender;
            button2.Location = new Point(0, 376);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(200, 45);
            button2.TabIndex = 6;
            button2.Text = "Reports";
            button2.UseVisualStyleBackColor = true;
            // 
            // BtnSv
            // 
            BtnSv.Dock = DockStyle.Top;
            BtnSv.FlatAppearance.BorderSize = 0;
            BtnSv.FlatStyle = FlatStyle.Flat;
            BtnSv.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSv.ForeColor = Color.Lavender;
            BtnSv.Image = (Image)resources.GetObject("BtnSv.Image");
            BtnSv.ImageAlign = ContentAlignment.MiddleLeft;
            BtnSv.Location = new Point(0, 294);
            BtnSv.Margin = new Padding(3, 2, 3, 2);
            BtnSv.Name = "BtnSv";
            BtnSv.Size = new Size(200, 82);
            BtnSv.TabIndex = 10;
            BtnSv.Text = "    Sampled\r\n    Value";
            BtnSv.UseVisualStyleBackColor = true;
            BtnSv.Click += BtnSv_Click;
            // 
            // BtnGo
            // 
            BtnGo.Dock = DockStyle.Top;
            BtnGo.FlatAppearance.BorderSize = 0;
            BtnGo.FlatStyle = FlatStyle.Flat;
            BtnGo.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            BtnGo.ForeColor = Color.Lavender;
            BtnGo.Image = (Image)resources.GetObject("BtnGo.Image");
            BtnGo.ImageAlign = ContentAlignment.MiddleLeft;
            BtnGo.Location = new Point(0, 212);
            BtnGo.Margin = new Padding(3, 2, 3, 2);
            BtnGo.Name = "BtnGo";
            BtnGo.Size = new Size(200, 82);
            BtnGo.TabIndex = 9;
            BtnGo.Text = "  GOOSE";
            BtnGo.UseVisualStyleBackColor = true;
            BtnGo.Click += BtnGo_Click;
            // 
            // BtnDataSet
            // 
            BtnDataSet.Dock = DockStyle.Top;
            BtnDataSet.FlatAppearance.BorderSize = 0;
            BtnDataSet.FlatStyle = FlatStyle.Flat;
            BtnDataSet.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            BtnDataSet.ForeColor = Color.Lavender;
            BtnDataSet.Image = (Image)resources.GetObject("BtnDataSet.Image");
            BtnDataSet.ImageAlign = ContentAlignment.MiddleLeft;
            BtnDataSet.Location = new Point(0, 130);
            BtnDataSet.Margin = new Padding(3, 2, 3, 2);
            BtnDataSet.Name = "BtnDataSet";
            BtnDataSet.Size = new Size(200, 82);
            BtnDataSet.TabIndex = 8;
            BtnDataSet.Text = "   DataSets";
            BtnDataSet.UseVisualStyleBackColor = true;
            BtnDataSet.Click += BtnDataSet_Click;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 98);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(200, 32);
            panel3.TabIndex = 12;
            // 
            // BtnIecHome
            // 
            BtnIecHome.Dock = DockStyle.Top;
            BtnIecHome.FlatAppearance.BorderSize = 0;
            BtnIecHome.FlatStyle = FlatStyle.Flat;
            BtnIecHome.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            BtnIecHome.ForeColor = Color.Lavender;
            BtnIecHome.ImageAlign = ContentAlignment.MiddleLeft;
            BtnIecHome.Location = new Point(0, 47);
            BtnIecHome.Margin = new Padding(3, 2, 3, 2);
            BtnIecHome.Name = "BtnIecHome";
            BtnIecHome.Size = new Size(200, 51);
            BtnIecHome.TabIndex = 11;
            BtnIecHome.Text = "IEC 61850";
            BtnIecHome.UseVisualStyleBackColor = true;
            BtnIecHome.Click += IecHome_Click;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 15);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(200, 32);
            panel2.TabIndex = 3;
            // 
            // PnChields
            // 
            PnChields.Dock = DockStyle.Fill;
            PnChields.Location = new Point(200, 0);
            PnChields.Name = "PnChields";
            PnChields.Size = new Size(1064, 616);
            PnChields.TabIndex = 2;
            // 
            // Iec61850Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1264, 616);
            Controls.Add(PnChields);
            Controls.Add(panel1);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Iec61850Form";
            Text = "Iec61850Form";
            Load += Iec61850Form_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Button button2;
        private Panel PnChields;
        private Button BtnDataSet;
        private Button BtnGo;
        private Button BtnSv;
        private Panel panel3;
        private Button BtnIecHome;
    }
}