namespace Server
{
    partial class DeviceRemoveForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceRemoveForm));
            panel3 = new Panel();
            panel1 = new Panel();
            panel2 = new Panel();
            PbInfo = new ProgressBar();
            LbInfo = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            TbTypes = new TextBox();
            label3 = new Label();
            label4 = new Label();
            CbVms = new ComboBox();
            BtnDelete = new Button();
            panel7 = new Panel();
            panel8 = new Panel();
            BtnMinimize = new Button();
            BtnExit = new Button();
            label1 = new Label();
            imageList1 = new ImageList(components);
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel8.SuspendLayout();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(1, 22, 39);
            panel3.Controls.Add(panel1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(6);
            panel3.Size = new Size(363, 450);
            panel3.TabIndex = 20;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(40, 58, 73);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Controls.Add(BtnDelete);
            panel1.Controls.Add(panel7);
            panel1.Controls.Add(panel8);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(6, 6);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(351, 438);
            panel1.TabIndex = 10;
            // 
            // panel2
            // 
            panel2.Controls.Add(PbInfo);
            panel2.Controls.Add(LbInfo);
            panel2.Location = new Point(12, 356);
            panel2.Name = "panel2";
            panel2.Size = new Size(328, 59);
            panel2.TabIndex = 17;
            // 
            // PbInfo
            // 
            PbInfo.Dock = DockStyle.Top;
            PbInfo.Location = new Point(0, 30);
            PbInfo.Name = "PbInfo";
            PbInfo.Size = new Size(328, 23);
            PbInfo.TabIndex = 15;
            // 
            // LbInfo
            // 
            LbInfo.Dock = DockStyle.Top;
            LbInfo.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            LbInfo.ForeColor = Color.RoyalBlue;
            LbInfo.Location = new Point(0, 0);
            LbInfo.Name = "LbInfo";
            LbInfo.Size = new Size(328, 30);
            LbInfo.TabIndex = 16;
            LbInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.BackgroundImageLayout = ImageLayout.Stretch;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 79F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.Controls.Add(TbTypes, 1, 3);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(CbVms, 1, 2);
            tableLayoutPanel1.Location = new Point(12, 139);
            tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(328, 70);
            tableLayoutPanel1.TabIndex = 14;
            // 
            // TbTypes
            // 
            TbTypes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TbTypes.BackColor = Color.FromArgb(31, 45, 56);
            TbTypes.BorderStyle = BorderStyle.None;
            TbTypes.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TbTypes.ForeColor = Color.Lavender;
            TbTypes.Location = new Point(82, 41);
            TbTypes.Margin = new Padding(3, 2, 3, 2);
            TbTypes.Name = "TbTypes";
            TbTypes.Size = new Size(243, 25);
            TbTypes.TabIndex = 17;
            TbTypes.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Lavender;
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(73, 39);
            label3.TabIndex = 15;
            label3.Text = "Nome";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.Lavender;
            label4.Location = new Point(3, 39);
            label4.Name = "label4";
            label4.Size = new Size(73, 31);
            label4.TabIndex = 16;
            label4.Text = "Tipo";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CbVms
            // 
            CbVms.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CbVms.BackColor = Color.FromArgb(31, 45, 56);
            CbVms.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            CbVms.ForeColor = Color.Lavender;
            CbVms.FormattingEnabled = true;
            CbVms.Items.AddRange(new object[] { "IED", "Merging Unit" });
            CbVms.Location = new Point(82, 3);
            CbVms.Name = "CbVms";
            CbVms.Size = new Size(243, 33);
            CbVms.TabIndex = 13;
            CbVms.Validated += CbVms_Validated;
            // 
            // BtnDelete
            // 
            BtnDelete.BackgroundImageLayout = ImageLayout.None;
            BtnDelete.FlatStyle = FlatStyle.Flat;
            BtnDelete.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnDelete.ForeColor = Color.Lavender;
            BtnDelete.Location = new Point(41, 293);
            BtnDelete.Margin = new Padding(3, 2, 3, 2);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Size = new Size(276, 40);
            BtnDelete.TabIndex = 11;
            BtnDelete.Text = "Deletar Virtual Machine";
            BtnDelete.UseVisualStyleBackColor = true;
            BtnDelete.Click += BtnDelete_Click;
            // 
            // panel7
            // 
            panel7.Dock = DockStyle.Top;
            panel7.Location = new Point(0, 45);
            panel7.Margin = new Padding(3, 2, 3, 2);
            panel7.Name = "panel7";
            panel7.Size = new Size(351, 69);
            panel7.TabIndex = 11;
            // 
            // panel8
            // 
            panel8.Controls.Add(BtnMinimize);
            panel8.Controls.Add(BtnExit);
            panel8.Dock = DockStyle.Top;
            panel8.Location = new Point(0, 0);
            panel8.Margin = new Padding(3, 2, 3, 2);
            panel8.Name = "panel8";
            panel8.Size = new Size(351, 45);
            panel8.TabIndex = 12;
            panel8.MouseDown += panelTitleBar_MouseDown;
            // 
            // BtnMinimize
            // 
            BtnMinimize.Dock = DockStyle.Right;
            BtnMinimize.FlatAppearance.BorderSize = 0;
            BtnMinimize.FlatStyle = FlatStyle.Flat;
            BtnMinimize.Image = (Image)resources.GetObject("BtnMinimize.Image");
            BtnMinimize.Location = new Point(259, 0);
            BtnMinimize.Name = "BtnMinimize";
            BtnMinimize.Size = new Size(46, 45);
            BtnMinimize.TabIndex = 13;
            BtnMinimize.UseVisualStyleBackColor = true;
            BtnMinimize.Click += BtnMinimize_Click;
            // 
            // BtnExit
            // 
            BtnExit.Dock = DockStyle.Right;
            BtnExit.FlatAppearance.BorderSize = 0;
            BtnExit.FlatStyle = FlatStyle.Flat;
            BtnExit.Image = (Image)resources.GetObject("BtnExit.Image");
            BtnExit.Location = new Point(305, 0);
            BtnExit.Name = "BtnExit";
            BtnExit.Size = new Size(46, 45);
            BtnExit.TabIndex = 12;
            BtnExit.UseVisualStyleBackColor = true;
            BtnExit.Click += BtnExit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(198, 30);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 19;
            label1.Text = "label1";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "buttonGreen.png");
            imageList1.Images.SetKeyName(1, "buttonRed.png");
            imageList1.Images.SetKeyName(2, "buttonYellow.png");
            // 
            // DeviceRemoveForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(363, 450);
            Controls.Add(panel3);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DeviceRemoveForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DeviceRemoveForm";
            Load += DeviceRemoveForm_Load;
            panel3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel8.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel3;
        private Panel panel1;
        private Panel panel2;
        private ProgressBar PbInfo;
        private Label LbInfo;
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox CbVms;
        private Label label3;
        private Label label4;
        private Button BtnDelete;
        private Panel panel7;
        private Panel panel8;
        private Button BtnMinimize;
        private Button BtnExit;
        private Label label1;
        private ImageList imageList1;
        private TextBox TbTypes;
    }
}