namespace TestSet
{
    partial class SequencerResults
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
            tableLayoutPanel1 = new TableLayoutPanel();
            BtnStop = new Button();
            BtnStart = new Button();
            plotView2 = new OxyPlot.WindowsForms.PlotView();
            PltViewValues = new OxyPlot.WindowsForms.PlotView();
            panel2 = new Panel();
            TimeGraphUpdate = new System.Windows.Forms.Timer(components);
            TimeGetRes = new System.Windows.Forms.Timer(components);
            RichTExtBoxDebug = new RichTextBox();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(RichTExtBoxDebug);
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Controls.Add(plotView2);
            panel1.Controls.Add(PltViewValues);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(40);
            panel1.Size = new Size(1166, 682);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(BtnStop, 0, 0);
            tableLayoutPanel1.Controls.Add(BtnStart, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(40, 458);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1086, 62);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // BtnStop
            // 
            BtnStop.Dock = DockStyle.Fill;
            BtnStop.FlatAppearance.BorderSize = 0;
            BtnStop.FlatStyle = FlatStyle.Flat;
            BtnStop.Font = new Font("Segoe UI Emoji", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnStop.ForeColor = Color.Lavender;
            BtnStop.Location = new Point(546, 3);
            BtnStop.Name = "BtnStop";
            BtnStop.Size = new Size(537, 56);
            BtnStop.TabIndex = 3;
            BtnStop.Text = "Parar";
            BtnStop.UseVisualStyleBackColor = true;
            BtnStop.Click += BtnStop_Click;
            // 
            // BtnStart
            // 
            BtnStart.Dock = DockStyle.Fill;
            BtnStart.FlatAppearance.BorderSize = 0;
            BtnStart.FlatStyle = FlatStyle.Flat;
            BtnStart.Font = new Font("Segoe UI Emoji", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnStart.ForeColor = Color.Lavender;
            BtnStart.Location = new Point(3, 3);
            BtnStart.Name = "BtnStart";
            BtnStart.Size = new Size(537, 56);
            BtnStart.TabIndex = 2;
            BtnStart.Text = "Iniciar";
            BtnStart.UseVisualStyleBackColor = true;
            BtnStart.Click += BtnStart_Click;
            // 
            // plotView2
            // 
            plotView2.Dock = DockStyle.Top;
            plotView2.Location = new Point(40, 407);
            plotView2.Name = "plotView2";
            plotView2.PanCursor = Cursors.Hand;
            plotView2.Size = new Size(1086, 51);
            plotView2.TabIndex = 1;
            plotView2.Text = "plotView2";
            plotView2.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView2.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView2.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // PltViewValues
            // 
            PltViewValues.Dock = DockStyle.Top;
            PltViewValues.Location = new Point(40, 101);
            PltViewValues.Name = "PltViewValues";
            PltViewValues.PanCursor = Cursors.Hand;
            PltViewValues.Size = new Size(1086, 306);
            PltViewValues.TabIndex = 0;
            PltViewValues.Text = "plotView1";
            PltViewValues.ZoomHorizontalCursor = Cursors.SizeWE;
            PltViewValues.ZoomRectangleCursor = Cursors.SizeNWSE;
            PltViewValues.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(40, 40);
            panel2.Name = "panel2";
            panel2.Size = new Size(1086, 61);
            panel2.TabIndex = 0;
            // 
            // TimeGraphUpdate
            // 
            TimeGraphUpdate.Interval = 500;
            TimeGraphUpdate.Tick += TimeGraphUpdate_Tick;
            // 
            // RichTExtBoxDebug
            // 
            RichTExtBoxDebug.Location = new Point(328, 553);
            RichTExtBoxDebug.Name = "RichTExtBoxDebug";
            RichTExtBoxDebug.Size = new Size(538, 108);
            RichTExtBoxDebug.TabIndex = 3;
            RichTExtBoxDebug.Text = "";
            // 
            // SequencerResults
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1166, 682);
            Controls.Add(panel1);
            ForeColor = Color.Lavender;
            Name = "SequencerResults";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SequencerResults";
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private OxyPlot.WindowsForms.PlotView plotView2;
        private OxyPlot.WindowsForms.PlotView PltViewValues;
        private Panel panel2;
        private Button BtnStop;
        private Button BtnStart;
        private System.Windows.Forms.Timer TimeGraphUpdate;
        private System.Windows.Forms.Timer TimeGetRes;
        private RichTextBox RichTExtBoxDebug;
    }
}