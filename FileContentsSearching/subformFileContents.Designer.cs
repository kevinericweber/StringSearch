namespace StringSearch.FileContentsSearching
{
    partial class subformFileContents
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboSearchType = new System.Windows.Forms.ComboBox();
            this.txtSearchValue = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboLocation = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboMaxFilesize = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFilenameRegex = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Base Directory";
            // 
            // comboSearchType
            // 
            this.comboSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSearchType.FormattingEnabled = true;
            this.comboSearchType.Items.AddRange(new object[] {
            "Text Search for",
            "Regex Search for"});
            this.comboSearchType.Location = new System.Drawing.Point(12, 39);
            this.comboSearchType.Name = "comboSearchType";
            this.comboSearchType.Size = new System.Drawing.Size(126, 21);
            this.comboSearchType.TabIndex = 2;
            // 
            // txtSearchValue
            // 
            this.txtSearchValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchValue.Location = new System.Drawing.Point(144, 39);
            this.txtSearchValue.Name = "txtSearchValue";
            this.txtSearchValue.Size = new System.Drawing.Size(181, 20);
            this.txtSearchValue.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(296, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 19);
            this.button1.TabIndex = 1;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // comboLocation
            // 
            this.comboLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboLocation.FormattingEnabled = true;
            this.comboLocation.Location = new System.Drawing.Point(104, 12);
            this.comboLocation.Name = "comboLocation";
            this.comboLocation.Size = new System.Drawing.Size(186, 21);
            this.comboLocation.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(231, 76);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(94, 33);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Max Filesize To Search";
            // 
            // comboMaxFilesize
            // 
            this.comboMaxFilesize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMaxFilesize.FormattingEnabled = true;
            this.comboMaxFilesize.Location = new System.Drawing.Point(184, 31);
            this.comboMaxFilesize.Name = "comboMaxFilesize";
            this.comboMaxFilesize.Size = new System.Drawing.Size(121, 21);
            this.comboMaxFilesize.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Only Scan Files With Regex Name";
            // 
            // txtFilenameRegex
            // 
            this.txtFilenameRegex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilenameRegex.Enabled = false;
            this.txtFilenameRegex.Location = new System.Drawing.Point(184, 62);
            this.txtFilenameRegex.Name = "txtFilenameRegex";
            this.txtFilenameRegex.Size = new System.Drawing.Size(120, 20);
            this.txtFilenameRegex.TabIndex = 6;
            this.txtFilenameRegex.Text = ".*";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboMaxFilesize);
            this.groupBox1.Controls.Add(this.txtFilenameRegex);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 98);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advanced Options";
            // 
            // subformFileContents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(334, 233);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.comboLocation);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtSearchValue);
            this.Controls.Add(this.comboSearchType);
            this.Controls.Add(this.label1);
            this.Name = "subformFileContents";
            this.Text = "File Contents Search (Subwindow)";
            this.Load += new System.EventHandler(this.subformFileContents_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboSearchType;
        private System.Windows.Forms.TextBox txtSearchValue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboLocation;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboMaxFilesize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFilenameRegex;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}