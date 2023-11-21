namespace Server
{
    partial class MmsForm
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
            TvData = new TreeView();
            SuspendLayout();
            // 
            // TvData
            // 
            TvData.Dock = DockStyle.Fill;
            TvData.Location = new Point(50, 50);
            TvData.Name = "TvData";
            TvData.Size = new Size(1162, 573);
            TvData.TabIndex = 0;
            // 
            // MmsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 58, 73);
            ClientSize = new Size(1262, 673);
            Controls.Add(TvData);
            ForeColor = Color.Lavender;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MmsForm";
            Padding = new Padding(50);
            Text = "mms";
            Load += MmsForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private TreeView TvData;
    }
}