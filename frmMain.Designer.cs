namespace StringSearch
{
    partial class frmMain
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
            this.comboOverallSearchType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listResults = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressMainStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.lblMainStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboOverallSearchType
            // 
            this.comboOverallSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOverallSearchType.FormattingEnabled = true;
            this.comboOverallSearchType.Location = new System.Drawing.Point(135, 12);
            this.comboOverallSearchType.Name = "comboOverallSearchType";
            this.comboOverallSearchType.Size = new System.Drawing.Size(143, 21);
            this.comboOverallSearchType.TabIndex = 0;
            this.comboOverallSearchType.SelectedIndexChanged += new System.EventHandler(this.comboOverallSearchType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search Type";
            // 
            // listResults
            // 
            this.listResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listResults.FormattingEnabled = true;
            this.listResults.Location = new System.Drawing.Point(380, 45);
            this.listResults.Name = "listResults";
            this.listResults.Size = new System.Drawing.Size(424, 511);
            this.listResults.Sorted = true;
            this.listResults.TabIndex = 2;
            this.listResults.DoubleClick += new System.EventHandler(this.listResults_DoubleClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressMainStatus,
            this.lblMainStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 561);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(816, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressMainStatus
            // 
            this.progressMainStatus.Name = "progressMainStatus";
            this.progressMainStatus.Size = new System.Drawing.Size(100, 16);
            this.progressMainStatus.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressMainStatus.Visible = false;
            // 
            // lblMainStatus
            // 
            this.lblMainStatus.Name = "lblMainStatus";
            this.lblMainStatus.Size = new System.Drawing.Size(118, 17);
            this.lblMainStatus.Text = "toolStripStatusLabel1";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 583);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listResults);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboOverallSearchType);
            this.Name = "frmMain";
            this.Text = "String Search";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboOverallSearchType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listResults;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressMainStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblMainStatus;
    }
}

