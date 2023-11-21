namespace Ied
{
    partial class PdirForm
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
            panel5 = new Panel();
            LbTitle = new Label();
            TbPickup = new TextBox();
            Lb50pCurrentL1 = new Label();
            TbRTorq = new TextBox();
            Lb50pTimeL1 = new Label();
            panel9 = new Panel();
            tableLayoutPanel5 = new TableLayoutPanel();
            groupBox4 = new GroupBox();
            Rb50pPu = new RadioButton();
            Rb50pAmpere = new RadioButton();
            label6 = new Label();
            PnProt50P = new Panel();
            panel7 = new Panel();
            panel6 = new Panel();
            Pn50pConfig = new TableLayoutPanel();
            panel8 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            comboBox1 = new ComboBox();
            label1 = new Label();
            label3 = new Label();
            label15 = new Label();
            Tlp50pPickupL1 = new TableLayoutPanel();
            Tlp50pTripL1 = new TableLayoutPanel();
            CbPolarization = new ComboBox();
            panel5.SuspendLayout();
            panel9.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            groupBox4.SuspendLayout();
            PnProt50P.SuspendLayout();
            panel6.SuspendLayout();
            Pn50pConfig.SuspendLayout();
            panel8.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            Tlp50pPickupL1.SuspendLayout();
            Tlp50pTripL1.SuspendLayout();
            SuspendLayout();
            // 
            // panel5
            // 
            panel5.Controls.Add(LbTitle);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Margin = new Padding(3, 4, 3, 4);
            panel5.Name = "panel5";
            panel5.Size = new Size(1060, 90);
            panel5.TabIndex = 0;
            // 
            // LbTitle
            // 
            LbTitle.Dock = DockStyle.Fill;
            LbTitle.Font = new Font("Trebuchet MS", 17.25F, FontStyle.Regular, GraphicsUnit.Point);
            LbTitle.Location = new Point(0, 0);
            LbTitle.Name = "LbTitle";
            LbTitle.Size = new Size(1060, 90);
            LbTitle.TabIndex = 2;
            LbTitle.Text = "Direcionalide de SobreCorrente";
            LbTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TbPickup
            // 
            TbPickup.BackColor = Color.FromArgb(31, 45, 56);
            TbPickup.Dock = DockStyle.Fill;
            TbPickup.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            TbPickup.ForeColor = Color.Lavender;
            TbPickup.Location = new Point(3, 4);
            TbPickup.Margin = new Padding(3, 4, 3, 4);
            TbPickup.Name = "TbPickup";
            TbPickup.Size = new Size(169, 32);
            TbPickup.TabIndex = 0;
            TbPickup.Text = "0";
            TbPickup.TextAlign = HorizontalAlignment.Center;
            // 
            // Lb50pCurrentL1
            // 
            Lb50pCurrentL1.AutoSize = true;
            Lb50pCurrentL1.Dock = DockStyle.Fill;
            Lb50pCurrentL1.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Lb50pCurrentL1.Location = new Point(178, 0);
            Lb50pCurrentL1.MinimumSize = new Size(52, 0);
            Lb50pCurrentL1.Name = "Lb50pCurrentL1";
            Lb50pCurrentL1.Size = new Size(52, 36);
            Lb50pCurrentL1.TabIndex = 1;
            Lb50pCurrentL1.Text = "[A]";
            Lb50pCurrentL1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TbRTorq
            // 
            TbRTorq.BackColor = Color.FromArgb(31, 45, 56);
            TbRTorq.Dock = DockStyle.Fill;
            TbRTorq.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            TbRTorq.ForeColor = Color.Lavender;
            TbRTorq.Location = new Point(3, 4);
            TbRTorq.Margin = new Padding(3, 4, 3, 4);
            TbRTorq.Name = "TbRTorq";
            TbRTorq.Size = new Size(169, 32);
            TbRTorq.TabIndex = 0;
            TbRTorq.Text = "0";
            TbRTorq.TextAlign = HorizontalAlignment.Center;
            // 
            // Lb50pTimeL1
            // 
            Lb50pTimeL1.AutoSize = true;
            Lb50pTimeL1.Dock = DockStyle.Fill;
            Lb50pTimeL1.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Lb50pTimeL1.Location = new Point(178, 0);
            Lb50pTimeL1.MinimumSize = new Size(52, 0);
            Lb50pTimeL1.Name = "Lb50pTimeL1";
            Lb50pTimeL1.Size = new Size(52, 36);
            Lb50pTimeL1.TabIndex = 1;
            Lb50pTimeL1.Text = "[°]";
            Lb50pTimeL1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel9
            // 
            panel9.Controls.Add(tableLayoutPanel5);
            panel9.Dock = DockStyle.Fill;
            panel9.Location = new Point(3, 4);
            panel9.Margin = new Padding(3, 4, 3, 4);
            panel9.Name = "panel9";
            panel9.Size = new Size(151, 219);
            panel9.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Controls.Add(groupBox4, 0, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(0, 0);
            tableLayoutPanel5.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(151, 219);
            tableLayoutPanel5.TabIndex = 10;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(Rb50pPu);
            groupBox4.Controls.Add(Rb50pAmpere);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Font = new Font("Trebuchet MS", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox4.ForeColor = Color.Lavender;
            groupBox4.Location = new Point(3, 4);
            groupBox4.Margin = new Padding(3, 4, 3, 4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(3, 4, 3, 4);
            groupBox4.Size = new Size(145, 101);
            groupBox4.TabIndex = 6;
            groupBox4.TabStop = false;
            groupBox4.Text = "Current";
            // 
            // Rb50pPu
            // 
            Rb50pPu.AutoSize = true;
            Rb50pPu.Font = new Font("Trebuchet MS", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Rb50pPu.Location = new Point(7, 55);
            Rb50pPu.Margin = new Padding(3, 4, 3, 4);
            Rb50pPu.Name = "Rb50pPu";
            Rb50pPu.Size = new Size(42, 22);
            Rb50pPu.TabIndex = 6;
            Rb50pPu.Text = "Pu";
            Rb50pPu.UseVisualStyleBackColor = true;
            // 
            // Rb50pAmpere
            // 
            Rb50pAmpere.AutoSize = true;
            Rb50pAmpere.Checked = true;
            Rb50pAmpere.Font = new Font("Trebuchet MS", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Rb50pAmpere.Location = new Point(7, 31);
            Rb50pAmpere.Margin = new Padding(3, 4, 3, 4);
            Rb50pAmpere.Name = "Rb50pAmpere";
            Rb50pAmpere.Size = new Size(73, 22);
            Rb50pAmpere.TabIndex = 5;
            Rb50pAmpere.TabStop = true;
            Rb50pAmpere.Text = "Ampère";
            Rb50pAmpere.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(4, 35);
            label6.Name = "label6";
            label6.Size = new Size(176, 44);
            label6.TabIndex = 8;
            label6.Text = "Pickup Current";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PnProt50P
            // 
            PnProt50P.AutoScroll = true;
            PnProt50P.AutoSize = true;
            PnProt50P.Controls.Add(panel7);
            PnProt50P.Controls.Add(panel6);
            PnProt50P.Controls.Add(panel5);
            PnProt50P.Dock = DockStyle.Fill;
            PnProt50P.Location = new Point(0, 0);
            PnProt50P.Name = "PnProt50P";
            PnProt50P.Size = new Size(1060, 616);
            PnProt50P.TabIndex = 1;
            // 
            // panel7
            // 
            panel7.Dock = DockStyle.Fill;
            panel7.Location = new Point(0, 317);
            panel7.Margin = new Padding(3, 4, 3, 4);
            panel7.Name = "panel7";
            panel7.Padding = new Padding(15, 27, 23, 27);
            panel7.Size = new Size(1060, 299);
            panel7.TabIndex = 2;
            // 
            // panel6
            // 
            panel6.Controls.Add(Pn50pConfig);
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(0, 90);
            panel6.Margin = new Padding(3, 4, 3, 4);
            panel6.Name = "panel6";
            panel6.Size = new Size(1060, 227);
            panel6.TabIndex = 1;
            // 
            // Pn50pConfig
            // 
            Pn50pConfig.ColumnCount = 2;
            Pn50pConfig.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.8538008F));
            Pn50pConfig.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85.1462F));
            Pn50pConfig.Controls.Add(panel8, 1, 0);
            Pn50pConfig.Controls.Add(panel9, 0, 0);
            Pn50pConfig.Dock = DockStyle.Fill;
            Pn50pConfig.Location = new Point(0, 0);
            Pn50pConfig.Margin = new Padding(3, 4, 3, 4);
            Pn50pConfig.Name = "Pn50pConfig";
            Pn50pConfig.RowCount = 1;
            Pn50pConfig.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Pn50pConfig.RowStyles.Add(new RowStyle(SizeType.Absolute, 189F));
            Pn50pConfig.Size = new Size(1060, 227);
            Pn50pConfig.TabIndex = 10;
            // 
            // panel8
            // 
            panel8.Controls.Add(tableLayoutPanel1);
            panel8.Dock = DockStyle.Fill;
            panel8.Location = new Point(160, 4);
            panel8.Margin = new Padding(3, 4, 3, 4);
            panel8.Name = "panel8";
            panel8.Size = new Size(897, 219);
            panel8.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 182F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(comboBox1, 1, 4);
            tableLayoutPanel1.Controls.Add(label1, 0, 4);
            tableLayoutPanel1.Controls.Add(label3, 0, 3);
            tableLayoutPanel1.Controls.Add(label6, 0, 1);
            tableLayoutPanel1.Controls.Add(label15, 0, 2);
            tableLayoutPanel1.Controls.Add(Tlp50pPickupL1, 1, 1);
            tableLayoutPanel1.Controls.Add(Tlp50pTripL1, 1, 2);
            tableLayoutPanel1.Controls.Add(CbPolarization, 1, 3);
            tableLayoutPanel1.Dock = DockStyle.Left;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 15.7894735F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 21.0526314F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 21.0526314F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 21.0526314F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 21.0526314F));
            tableLayoutPanel1.Size = new Size(422, 219);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.FromArgb(31, 45, 56);
            comboBox1.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.ForeColor = Color.Lavender;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Forward", "Reverse" });
            comboBox1.Location = new Point(187, 173);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(229, 35);
            comboBox1.TabIndex = 24;
            comboBox1.Text = "Forward";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(4, 170);
            label1.Name = "label1";
            label1.Size = new Size(176, 48);
            label1.TabIndex = 23;
            label1.Text = "Direction";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(4, 125);
            label3.Name = "label3";
            label3.Size = new Size(176, 44);
            label3.TabIndex = 21;
            label3.Text = "Polarization";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Dock = DockStyle.Fill;
            label15.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label15.Location = new Point(4, 80);
            label15.Name = "label15";
            label15.Size = new Size(176, 44);
            label15.TabIndex = 9;
            label15.Text = "Maximum Torque";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Tlp50pPickupL1
            // 
            Tlp50pPickupL1.ColumnCount = 2;
            Tlp50pPickupL1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.7894745F));
            Tlp50pPickupL1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.2105255F));
            Tlp50pPickupL1.Controls.Add(TbPickup, 0, 0);
            Tlp50pPickupL1.Controls.Add(Lb50pCurrentL1, 1, 0);
            Tlp50pPickupL1.Dock = DockStyle.Fill;
            Tlp50pPickupL1.Location = new Point(187, 39);
            Tlp50pPickupL1.Margin = new Padding(3, 4, 3, 4);
            Tlp50pPickupL1.Name = "Tlp50pPickupL1";
            Tlp50pPickupL1.RowCount = 1;
            Tlp50pPickupL1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Tlp50pPickupL1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Tlp50pPickupL1.Size = new Size(231, 36);
            Tlp50pPickupL1.TabIndex = 18;
            // 
            // Tlp50pTripL1
            // 
            Tlp50pTripL1.ColumnCount = 2;
            Tlp50pTripL1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.7894745F));
            Tlp50pTripL1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.2105274F));
            Tlp50pTripL1.Controls.Add(TbRTorq, 0, 0);
            Tlp50pTripL1.Controls.Add(Lb50pTimeL1, 1, 0);
            Tlp50pTripL1.Dock = DockStyle.Fill;
            Tlp50pTripL1.Location = new Point(187, 84);
            Tlp50pTripL1.Margin = new Padding(3, 4, 3, 4);
            Tlp50pTripL1.Name = "Tlp50pTripL1";
            Tlp50pTripL1.RowCount = 1;
            Tlp50pTripL1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Tlp50pTripL1.Size = new Size(231, 36);
            Tlp50pTripL1.TabIndex = 20;
            // 
            // CbPolarization
            // 
            CbPolarization.BackColor = Color.FromArgb(31, 45, 56);
            CbPolarization.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            CbPolarization.ForeColor = Color.Lavender;
            CbPolarization.FormattingEnabled = true;
            CbPolarization.Items.AddRange(new object[] { "90°", "60°", "30°" });
            CbPolarization.Location = new Point(187, 128);
            CbPolarization.Name = "CbPolarization";
            CbPolarization.Size = new Size(229, 35);
            CbPolarization.TabIndex = 22;
            CbPolarization.Text = "90°";
            // 
            // PdirForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1060, 616);
            Controls.Add(PnProt50P);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Name = "PdirForm";
            Text = "PdirForm";
            Load += PdirForm_Load;
            panel5.ResumeLayout(false);
            panel9.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            PnProt50P.ResumeLayout(false);
            panel6.ResumeLayout(false);
            Pn50pConfig.ResumeLayout(false);
            panel8.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            Tlp50pPickupL1.ResumeLayout(false);
            Tlp50pPickupL1.PerformLayout();
            Tlp50pTripL1.ResumeLayout(false);
            Tlp50pTripL1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel5;
        private Label LbTitle;
        private TextBox TbPickup;
        private Label Lb50pCurrentL1;
        private TextBox TbRTorq;
        private Label Lb50pTimeL1;
        private Panel panel9;
        private TableLayoutPanel tableLayoutPanel5;
        private GroupBox groupBox4;
        private RadioButton Rb50pPu;
        private RadioButton Rb50pAmpere;
        private Label label6;
        private Panel PnProt50P;
        private Panel panel7;
        private Panel panel6;
        private TableLayoutPanel Pn50pConfig;
        private Panel panel8;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label15;
        private TableLayoutPanel Tlp50pPickupL1;
        private TableLayoutPanel Tlp50pTripL1;
        private Label label3;
        private ComboBox CbPolarization;
        private ComboBox comboBox1;
        private Label label1;
    }
}