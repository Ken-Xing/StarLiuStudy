namespace ReadAndSaveFile
{
    partial class ReadAndSaveFileForm
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
            this.dgvDataTable = new System.Windows.Forms.DataGridView();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.pnlReadSaveRefreshButton = new System.Windows.Forms.Panel();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.pnlOperatingCell = new System.Windows.Forms.Panel();
            this.btnBatchOverwrite = new System.Windows.Forms.Button();
            this.btnBatchsSaving = new System.Windows.Forms.Button();
            this.btnNotSelectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtErrorContent = new System.Windows.Forms.TextBox();
            this.txtCharacterLengthError = new System.Windows.Forms.TextBox();
            this.txtFiledTypeError = new System.Windows.Forms.TextBox();
            this.txtDuplicateDbContent = new System.Windows.Forms.TextBox();
            this.txtDuplicateContent = new System.Windows.Forms.TextBox();
            this.txtEmptyContent = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataTable)).BeginInit();
            this.pnlReadSaveRefreshButton.SuspendLayout();
            this.pnlOperatingCell.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDataTable
            // 
            this.dgvDataTable.AllowUserToAddRows = false;
            this.dgvDataTable.AllowUserToDeleteRows = false;
            this.dgvDataTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataTable.Location = new System.Drawing.Point(-1, 0);
            this.dgvDataTable.Name = "dgvDataTable";
            this.dgvDataTable.ReadOnly = true;
            this.dgvDataTable.RowHeadersWidth = 51;
            this.dgvDataTable.RowTemplate.Height = 23;
            this.dgvDataTable.Size = new System.Drawing.Size(569, 347);
            this.dgvDataTable.TabIndex = 0;
            this.dgvDataTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDataTable_CellContentClick);
            this.dgvDataTable.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDataTable_CellFormatting);
            this.dgvDataTable.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDataTable_CellMouseClick);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(568, 0);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(232, 347);
            this.txtLog.TabIndex = 4;
            this.txtLog.WordWrap = false;
            // 
            // pnlReadSaveRefreshButton
            // 
            this.pnlReadSaveRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlReadSaveRefreshButton.Controls.Add(this.btnSaveFile);
            this.pnlReadSaveRefreshButton.Controls.Add(this.btnNext);
            this.pnlReadSaveRefreshButton.Controls.Add(this.btnRefresh);
            this.pnlReadSaveRefreshButton.Controls.Add(this.btnReadFile);
            this.pnlReadSaveRefreshButton.Location = new System.Drawing.Point(0, 416);
            this.pnlReadSaveRefreshButton.Name = "pnlReadSaveRefreshButton";
            this.pnlReadSaveRefreshButton.Size = new System.Drawing.Size(800, 34);
            this.pnlReadSaveRefreshButton.TabIndex = 26;
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSaveFile.Location = new System.Drawing.Point(403, 7);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(78, 21);
            this.btnSaveFile.TabIndex = 29;
            this.btnSaveFile.Text = "Save file";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnNext.Location = new System.Drawing.Point(489, 6);
            this.btnNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 22);
            this.btnNext.TabIndex = 28;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRefresh.Location = new System.Drawing.Point(320, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 22);
            this.btnRefresh.TabIndex = 27;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnReadFile
            // 
            this.btnReadFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnReadFile.AutoEllipsis = true;
            this.btnReadFile.Location = new System.Drawing.Point(237, 6);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(75, 22);
            this.btnReadFile.TabIndex = 26;
            this.btnReadFile.Text = "Open file";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // pnlOperatingCell
            // 
            this.pnlOperatingCell.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pnlOperatingCell.Controls.Add(this.btnBatchOverwrite);
            this.pnlOperatingCell.Controls.Add(this.btnBatchsSaving);
            this.pnlOperatingCell.Controls.Add(this.btnNotSelectAll);
            this.pnlOperatingCell.Controls.Add(this.btnSelectAll);
            this.pnlOperatingCell.Location = new System.Drawing.Point(3, 3);
            this.pnlOperatingCell.Name = "pnlOperatingCell";
            this.pnlOperatingCell.Size = new System.Drawing.Size(797, 32);
            this.pnlOperatingCell.TabIndex = 28;
            // 
            // btnBatchOverwrite
            // 
            this.btnBatchOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBatchOverwrite.Location = new System.Drawing.Point(589, 5);
            this.btnBatchOverwrite.Name = "btnBatchOverwrite";
            this.btnBatchOverwrite.Size = new System.Drawing.Size(136, 23);
            this.btnBatchOverwrite.TabIndex = 19;
            this.btnBatchOverwrite.Text = "BatchOverwrite";
            this.btnBatchOverwrite.UseVisualStyleBackColor = true;
            this.btnBatchOverwrite.Click += new System.EventHandler(this.btnBatchOverwrite_Click);
            // 
            // btnBatchsSaving
            // 
            this.btnBatchsSaving.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBatchsSaving.Location = new System.Drawing.Point(416, 5);
            this.btnBatchsSaving.Name = "btnBatchsSaving";
            this.btnBatchsSaving.Size = new System.Drawing.Size(136, 23);
            this.btnBatchsSaving.TabIndex = 18;
            this.btnBatchsSaving.Text = "BatchsSaving";
            this.btnBatchsSaving.UseVisualStyleBackColor = true;
            this.btnBatchsSaving.Click += new System.EventHandler(this.btnBatchsSaving_Click);
            // 
            // btnNotSelectAll
            // 
            this.btnNotSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnNotSelectAll.Location = new System.Drawing.Point(243, 5);
            this.btnNotSelectAll.Name = "btnNotSelectAll";
            this.btnNotSelectAll.Size = new System.Drawing.Size(136, 23);
            this.btnNotSelectAll.TabIndex = 17;
            this.btnNotSelectAll.Text = "Deselect All";
            this.btnNotSelectAll.UseVisualStyleBackColor = true;
            this.btnNotSelectAll.Click += new System.EventHandler(this.btnNotSelectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSelectAll.Location = new System.Drawing.Point(70, 5);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(136, 23);
            this.btnSelectAll.TabIndex = 16;
            this.btnSelectAll.Text = "Select all";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.flowLayoutPanel1.Controls.Add(this.pnlOperatingCell);
            this.flowLayoutPanel1.Controls.Add(this.txtErrorContent);
            this.flowLayoutPanel1.Controls.Add(this.txtCharacterLengthError);
            this.flowLayoutPanel1.Controls.Add(this.txtFiledTypeError);
            this.flowLayoutPanel1.Controls.Add(this.txtDuplicateDbContent);
            this.flowLayoutPanel1.Controls.Add(this.txtDuplicateContent);
            this.flowLayoutPanel1.Controls.Add(this.txtEmptyContent);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 353);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 57);
            this.flowLayoutPanel1.TabIndex = 29;
            // 
            // txtErrorContent
            // 
            this.txtErrorContent.Location = new System.Drawing.Point(3, 41);
            this.txtErrorContent.Name = "txtErrorContent";
            this.txtErrorContent.ReadOnly = true;
            this.txtErrorContent.Size = new System.Drawing.Size(175, 21);
            this.txtErrorContent.TabIndex = 23;
            // 
            // txtCharacterLengthError
            // 
            this.txtCharacterLengthError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCharacterLengthError.Location = new System.Drawing.Point(184, 41);
            this.txtCharacterLengthError.Name = "txtCharacterLengthError";
            this.txtCharacterLengthError.ReadOnly = true;
            this.txtCharacterLengthError.Size = new System.Drawing.Size(175, 21);
            this.txtCharacterLengthError.TabIndex = 22;
            // 
            // txtFiledTypeError
            // 
            this.txtFiledTypeError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFiledTypeError.Location = new System.Drawing.Point(365, 41);
            this.txtFiledTypeError.Name = "txtFiledTypeError";
            this.txtFiledTypeError.ReadOnly = true;
            this.txtFiledTypeError.Size = new System.Drawing.Size(175, 21);
            this.txtFiledTypeError.TabIndex = 21;
            // 
            // txtDuplicateDbContent
            // 
            this.txtDuplicateDbContent.Location = new System.Drawing.Point(546, 41);
            this.txtDuplicateDbContent.Name = "txtDuplicateDbContent";
            this.txtDuplicateDbContent.ReadOnly = true;
            this.txtDuplicateDbContent.Size = new System.Drawing.Size(175, 21);
            this.txtDuplicateDbContent.TabIndex = 20;
            // 
            // txtDuplicateContent
            // 
            this.txtDuplicateContent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDuplicateContent.Location = new System.Drawing.Point(3, 68);
            this.txtDuplicateContent.Name = "txtDuplicateContent";
            this.txtDuplicateContent.ReadOnly = true;
            this.txtDuplicateContent.Size = new System.Drawing.Size(175, 21);
            this.txtDuplicateContent.TabIndex = 19;
            // 
            // txtEmptyContent
            // 
            this.txtEmptyContent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtEmptyContent.Location = new System.Drawing.Point(184, 68);
            this.txtEmptyContent.Name = "txtEmptyContent";
            this.txtEmptyContent.ReadOnly = true;
            this.txtEmptyContent.Size = new System.Drawing.Size(175, 21);
            this.txtEmptyContent.TabIndex = 18;
            // 
            // ReadAndSaveFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.pnlReadSaveRefreshButton);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.dgvDataTable);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "ReadAndSaveFileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReadAndSaveFileForm";
            this.Load += new System.EventHandler(this.ReadAndSaveFileForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataTable)).EndInit();
            this.pnlReadSaveRefreshButton.ResumeLayout(false);
            this.pnlOperatingCell.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvDataTable;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Panel pnlReadSaveRefreshButton;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.Panel pnlOperatingCell;
        private System.Windows.Forms.Button btnBatchOverwrite;
        private System.Windows.Forms.Button btnBatchsSaving;
        private System.Windows.Forms.Button btnNotSelectAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox txtErrorContent;
        private System.Windows.Forms.TextBox txtCharacterLengthError;
        private System.Windows.Forms.TextBox txtFiledTypeError;
        private System.Windows.Forms.TextBox txtDuplicateDbContent;
        private System.Windows.Forms.TextBox txtDuplicateContent;
        private System.Windows.Forms.TextBox txtEmptyContent;
    }
}