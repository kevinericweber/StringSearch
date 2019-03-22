namespace StringSearch.SqlSearching
{
    partial class subformSQLSearch
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.comboLocation = new System.Windows.Forms.ComboBox();
            this.txtSearchValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkCopyToClipboard = new System.Windows.Forms.CheckBox();
            this.txtDatabaseNameRegex = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkIndexes = new System.Windows.Forms.CheckBox();
            this.checkViewDefinitions = new System.Windows.Forms.CheckBox();
            this.checkViewNames = new System.Windows.Forms.CheckBox();
            this.checkTableNames = new System.Windows.Forms.CheckBox();
            this.checkTableColumns = new System.Windows.Forms.CheckBox();
            this.checkStoredProcName = new System.Windows.Forms.CheckBox();
            this.checkStoredProcContents = new System.Windows.Forms.CheckBox();
            this.checkUserLogins = new System.Windows.Forms.CheckBox();
            this.checkJobNames = new System.Windows.Forms.CheckBox();
            this.checkJobSteps = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(228, 74);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(94, 33);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // comboLocation
            // 
            this.comboLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboLocation.FormattingEnabled = true;
            this.comboLocation.Location = new System.Drawing.Point(136, 12);
            this.comboLocation.Name = "comboLocation";
            this.comboLocation.Size = new System.Drawing.Size(186, 21);
            this.comboLocation.TabIndex = 0;
            // 
            // txtSearchValue
            // 
            this.txtSearchValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchValue.Location = new System.Drawing.Point(136, 39);
            this.txtSearchValue.Name = "txtSearchValue";
            this.txtSearchValue.Size = new System.Drawing.Size(186, 20);
            this.txtSearchValue.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Server Instance";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkCopyToClipboard);
            this.groupBox1.Controls.Add(this.txtDatabaseNameRegex);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.checkIndexes);
            this.groupBox1.Controls.Add(this.checkViewDefinitions);
            this.groupBox1.Controls.Add(this.checkViewNames);
            this.groupBox1.Controls.Add(this.checkTableNames);
            this.groupBox1.Controls.Add(this.checkTableColumns);
            this.groupBox1.Controls.Add(this.checkStoredProcName);
            this.groupBox1.Controls.Add(this.checkStoredProcContents);
            this.groupBox1.Controls.Add(this.checkUserLogins);
            this.groupBox1.Controls.Add(this.checkJobNames);
            this.groupBox1.Controls.Add(this.checkJobSteps);
            this.groupBox1.Location = new System.Drawing.Point(12, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 334);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advanced Options";
            // 
            // checkCopyToClipboard
            // 
            this.checkCopyToClipboard.AutoSize = true;
            this.checkCopyToClipboard.Location = new System.Drawing.Point(11, 304);
            this.checkCopyToClipboard.Name = "checkCopyToClipboard";
            this.checkCopyToClipboard.Size = new System.Drawing.Size(205, 17);
            this.checkCopyToClipboard.TabIndex = 14;
            this.checkCopyToClipboard.Text = "Copy Script to Clipboard (Do Not Run)";
            this.checkCopyToClipboard.UseVisualStyleBackColor = true;
            // 
            // txtDatabaseNameRegex
            // 
            this.txtDatabaseNameRegex.Enabled = false;
            this.txtDatabaseNameRegex.Location = new System.Drawing.Point(162, 266);
            this.txtDatabaseNameRegex.Name = "txtDatabaseNameRegex";
            this.txtDatabaseNameRegex.Size = new System.Drawing.Size(100, 20);
            this.txtDatabaseNameRegex.TabIndex = 13;
            this.txtDatabaseNameRegex.Text = ".*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Database Name Regex";
            // 
            // checkIndexes
            // 
            this.checkIndexes.AutoSize = true;
            this.checkIndexes.Location = new System.Drawing.Point(11, 237);
            this.checkIndexes.Name = "checkIndexes";
            this.checkIndexes.Size = new System.Drawing.Size(169, 17);
            this.checkIndexes.TabIndex = 12;
            this.checkIndexes.Text = "Indexes (Names and Columns)";
            this.checkIndexes.UseVisualStyleBackColor = true;
            // 
            // checkViewDefinitions
            // 
            this.checkViewDefinitions.AutoSize = true;
            this.checkViewDefinitions.Location = new System.Drawing.Point(11, 214);
            this.checkViewDefinitions.Name = "checkViewDefinitions";
            this.checkViewDefinitions.Size = new System.Drawing.Size(101, 17);
            this.checkViewDefinitions.TabIndex = 11;
            this.checkViewDefinitions.Text = "View Definitions";
            this.checkViewDefinitions.UseVisualStyleBackColor = true;
            // 
            // checkViewNames
            // 
            this.checkViewNames.AutoSize = true;
            this.checkViewNames.Location = new System.Drawing.Point(11, 191);
            this.checkViewNames.Name = "checkViewNames";
            this.checkViewNames.Size = new System.Drawing.Size(85, 17);
            this.checkViewNames.TabIndex = 10;
            this.checkViewNames.Text = "View Names";
            this.checkViewNames.UseVisualStyleBackColor = true;
            // 
            // checkTableNames
            // 
            this.checkTableNames.AutoSize = true;
            this.checkTableNames.Location = new System.Drawing.Point(11, 168);
            this.checkTableNames.Name = "checkTableNames";
            this.checkTableNames.Size = new System.Drawing.Size(89, 17);
            this.checkTableNames.TabIndex = 9;
            this.checkTableNames.Text = "Table Names";
            this.checkTableNames.UseVisualStyleBackColor = true;
            // 
            // checkTableColumns
            // 
            this.checkTableColumns.AutoSize = true;
            this.checkTableColumns.Location = new System.Drawing.Point(11, 145);
            this.checkTableColumns.Name = "checkTableColumns";
            this.checkTableColumns.Size = new System.Drawing.Size(182, 17);
            this.checkTableColumns.TabIndex = 8;
            this.checkTableColumns.Text = "Tables with Column that matches";
            this.checkTableColumns.UseVisualStyleBackColor = true;
            // 
            // checkStoredProcName
            // 
            this.checkStoredProcName.AutoSize = true;
            this.checkStoredProcName.Location = new System.Drawing.Point(11, 122);
            this.checkStoredProcName.Name = "checkStoredProcName";
            this.checkStoredProcName.Size = new System.Drawing.Size(145, 17);
            this.checkStoredProcName.TabIndex = 7;
            this.checkStoredProcName.Text = "Stored Procedure Names";
            this.checkStoredProcName.UseVisualStyleBackColor = true;
            // 
            // checkStoredProcContents
            // 
            this.checkStoredProcContents.AutoSize = true;
            this.checkStoredProcContents.Location = new System.Drawing.Point(11, 99);
            this.checkStoredProcContents.Name = "checkStoredProcContents";
            this.checkStoredProcContents.Size = new System.Drawing.Size(154, 17);
            this.checkStoredProcContents.TabIndex = 6;
            this.checkStoredProcContents.Text = "Stored Procedure Contents";
            this.checkStoredProcContents.UseVisualStyleBackColor = true;
            // 
            // checkUserLogins
            // 
            this.checkUserLogins.AutoSize = true;
            this.checkUserLogins.Location = new System.Drawing.Point(11, 76);
            this.checkUserLogins.Name = "checkUserLogins";
            this.checkUserLogins.Size = new System.Drawing.Size(82, 17);
            this.checkUserLogins.TabIndex = 5;
            this.checkUserLogins.Text = "User Logins";
            this.checkUserLogins.UseVisualStyleBackColor = true;
            // 
            // checkJobNames
            // 
            this.checkJobNames.AutoSize = true;
            this.checkJobNames.Location = new System.Drawing.Point(11, 53);
            this.checkJobNames.Name = "checkJobNames";
            this.checkJobNames.Size = new System.Drawing.Size(79, 17);
            this.checkJobNames.TabIndex = 4;
            this.checkJobNames.Text = "Job Names";
            this.checkJobNames.UseVisualStyleBackColor = true;
            // 
            // checkJobSteps
            // 
            this.checkJobSteps.AutoSize = true;
            this.checkJobSteps.Location = new System.Drawing.Point(11, 30);
            this.checkJobSteps.Name = "checkJobSteps";
            this.checkJobSteps.Size = new System.Drawing.Size(73, 17);
            this.checkJobSteps.TabIndex = 3;
            this.checkJobSteps.Text = "Job Steps";
            this.checkJobSteps.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Search Term";
            // 
            // subformSQLSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(334, 606);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.comboLocation);
            this.Controls.Add(this.txtSearchValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "subformSQLSearch";
            this.Text = "SQL Search (Subwindow)";
            this.Load += new System.EventHandler(this.subformSQLSearch_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox comboLocation;
        private System.Windows.Forms.TextBox txtSearchValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkCopyToClipboard;
        private System.Windows.Forms.TextBox txtDatabaseNameRegex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkIndexes;
        private System.Windows.Forms.CheckBox checkViewDefinitions;
        private System.Windows.Forms.CheckBox checkViewNames;
        private System.Windows.Forms.CheckBox checkTableNames;
        private System.Windows.Forms.CheckBox checkTableColumns;
        private System.Windows.Forms.CheckBox checkStoredProcName;
        private System.Windows.Forms.CheckBox checkStoredProcContents;
        private System.Windows.Forms.CheckBox checkUserLogins;
        private System.Windows.Forms.CheckBox checkJobNames;
        private System.Windows.Forms.CheckBox checkJobSteps;
        private System.Windows.Forms.Label label2;
    }
}