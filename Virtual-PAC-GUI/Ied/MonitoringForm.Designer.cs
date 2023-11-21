namespace Ied
{
    partial class MonitoringForm
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
            panel4 = new Panel();
            panel5 = new Panel();
            BtnStartMonitor = new Button();
            panel2 = new Panel();
            DataGVPhasorsVoltage = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            label2 = new Label();
            panel3 = new Panel();
            DataGVPhasorsCurrent = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            label1 = new Label();
            TimerMeasurments = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGVPhasorsVoltage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DataGVPhasorsCurrent).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1264, 616);
            panel1.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Controls.Add(panel5);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(496, 0);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(768, 616);
            panel4.TabIndex = 3;
            // 
            // panel5
            // 
            panel5.Controls.Add(BtnStartMonitor);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Margin = new Padding(3, 2, 3, 2);
            panel5.Name = "panel5";
            panel5.Padding = new Padding(105, 34, 105, 0);
            panel5.Size = new Size(768, 123);
            panel5.TabIndex = 0;
            // 
            // BtnStartMonitor
            // 
            BtnStartMonitor.Dock = DockStyle.Top;
            BtnStartMonitor.FlatAppearance.BorderSize = 0;
            BtnStartMonitor.FlatStyle = FlatStyle.Flat;
            BtnStartMonitor.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            BtnStartMonitor.Location = new Point(105, 34);
            BtnStartMonitor.Margin = new Padding(3, 2, 3, 2);
            BtnStartMonitor.Name = "BtnStartMonitor";
            BtnStartMonitor.Size = new Size(558, 48);
            BtnStartMonitor.TabIndex = 5;
            BtnStartMonitor.Text = "Iniciar";
            BtnStartMonitor.UseVisualStyleBackColor = true;
            BtnStartMonitor.Click += BtnStartMonitor_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(DataGVPhasorsVoltage);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(DataGVPhasorsCurrent);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(26, 34, 0, 0);
            panel2.Size = new Size(496, 616);
            panel2.TabIndex = 2;
            // 
            // DataGVPhasorsVoltage
            // 
            DataGVPhasorsVoltage.BackgroundColor = Color.FromArgb(40, 58, 73);
            DataGVPhasorsVoltage.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGVPhasorsVoltage.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5 });
            DataGVPhasorsVoltage.Dock = DockStyle.Top;
            DataGVPhasorsVoltage.GridColor = SystemColors.ActiveCaption;
            DataGVPhasorsVoltage.Location = new Point(26, 248);
            DataGVPhasorsVoltage.Margin = new Padding(3, 2, 3, 2);
            DataGVPhasorsVoltage.Name = "DataGVPhasorsVoltage";
            DataGVPhasorsVoltage.RowHeadersWidth = 51;
            DataGVPhasorsVoltage.RowTemplate.Height = 29;
            DataGVPhasorsVoltage.Size = new Size(470, 123);
            DataGVPhasorsVoltage.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Column1";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Column2";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Column3";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Column4";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.Width = 125;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Column5";
            dataGridViewTextBoxColumn5.MinimumWidth = 6;
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.Width = 125;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(26, 213);
            label2.Name = "label2";
            label2.Size = new Size(470, 35);
            label2.TabIndex = 4;
            label2.Text = "Tensão";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(26, 191);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(470, 22);
            panel3.TabIndex = 2;
            // 
            // DataGVPhasorsCurrent
            // 
            DataGVPhasorsCurrent.BackgroundColor = Color.FromArgb(40, 58, 73);
            DataGVPhasorsCurrent.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGVPhasorsCurrent.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5 });
            DataGVPhasorsCurrent.Dock = DockStyle.Top;
            DataGVPhasorsCurrent.GridColor = SystemColors.ActiveCaption;
            DataGVPhasorsCurrent.Location = new Point(26, 69);
            DataGVPhasorsCurrent.Margin = new Padding(3, 2, 3, 2);
            DataGVPhasorsCurrent.Name = "DataGVPhasorsCurrent";
            DataGVPhasorsCurrent.RowHeadersWidth = 51;
            DataGVPhasorsCurrent.RowTemplate.Height = 29;
            DataGVPhasorsCurrent.Size = new Size(470, 122);
            DataGVPhasorsCurrent.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.HeaderText = "Column1";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.Width = 125;
            // 
            // Column2
            // 
            Column2.HeaderText = "Column2";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.Width = 125;
            // 
            // Column3
            // 
            Column3.HeaderText = "Column3";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.Width = 125;
            // 
            // Column4
            // 
            Column4.HeaderText = "Column4";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.Width = 125;
            // 
            // Column5
            // 
            Column5.HeaderText = "Column5";
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            Column5.Width = 125;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(26, 34);
            label1.Name = "label1";
            label1.Size = new Size(470, 35);
            label1.TabIndex = 3;
            label1.Text = "Corrente";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TimerMeasurments
            // 
            TimerMeasurments.Interval = 1000;
            // 
            // MonitoringForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(1264, 616);
            Controls.Add(panel1);
            ForeColor = Color.Lavender;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "MonitoringForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Monitoring";
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGVPhasorsVoltage).EndInit();
            ((System.ComponentModel.ISupportInitialize)DataGVPhasorsCurrent).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView DataGVPhasorsCurrent;
        private System.Windows.Forms.Timer TimerMeasurments;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridView DataGVPhasorsVoltage;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private Panel panel4;
        private Panel panel5;
        private Panel panel2;
        private Panel panel3;
        private Button BtnStartMonitor;
        private Label label2;
        private Label label1;
    }
}