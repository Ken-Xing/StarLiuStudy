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
            this.btnReadFile = new System.Windows.Forms.Button();
            this.dgvDataTable = new System.Windows.Forms.DataGridView();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtEmptyContent = new System.Windows.Forms.TextBox();
            this.txtDuplicateContent = new System.Windows.Forms.TextBox();
            this.txtDuplicateDbContent = new System.Windows.Forms.TextBox();
            this.txtFiledTypeError = new System.Windows.Forms.TextBox();
            this.txtCharacterLengthError = new System.Windows.Forms.TextBox();
            this.txtErrorContent = new System.Windows.Forms.TextBox();
            this.btnBatchOverwrite = new System.Windows.Forms.Button();
            this.btnBatchsSaving = new System.Windows.Forms.Button();
            this.btnNotSelectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataTable)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReadFile
            // 
            this.btnReadFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnReadFile.AutoEllipsis = true;
            this.btnReadFile.Location = new System.Drawing.Point(281, 416);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(75, 23);
            this.btnReadFile.TabIndex = 2;
            this.btnReadFile.Text = "Open file";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // dgvDataTable
            // 
            this.dgvDataTable.AllowUserToAddRows = false;
            this.dgvDataTable.AllowUserToDeleteRows = false;
            this.dgvDataTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataTable.Location = new System.Drawing.Point(-7, 0);
            this.dgvDataTable.Name = "dgvDataTable";
            this.dgvDataTable.ReadOnly = true;
            this.dgvDataTable.RowHeadersWidth = 51;
            this.dgvDataTable.RowTemplate.Height = 23;
            this.dgvDataTable.Size = new System.Drawing.Size(569, 372);
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
            this.txtLog.Size = new System.Drawing.Size(232, 372);
            this.txtLog.TabIndex = 4;
            this.txtLog.WordWrap = false;
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSaveFile.BackColor = System.Drawing.SystemColors.Menu;
            this.btnSaveFile.Location = new System.Drawing.Point(443, 416);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(75, 22);
            this.btnSaveFile.TabIndex = 3;
            this.btnSaveFile.Text = "Save file";
            this.btnSaveFile.UseVisualStyleBackColor = false;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRefresh.Location = new System.Drawing.Point(362, 415);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtEmptyContent
            // 
            this.txtEmptyContent.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtEmptyContent.Location = new System.Drawing.Point(27, 386);
            this.txtEmptyContent.Name = "txtEmptyContent";
            this.txtEmptyContent.ReadOnly = true;
            this.txtEmptyContent.Size = new System.Drawing.Size(121, 21);
            this.txtEmptyContent.TabIndex = 6;
            // 
            // txtDuplicateContent
            // 
            this.txtDuplicateContent.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtDuplicateContent.Location = new System.Drawing.Point(154, 386);
            this.txtDuplicateContent.Name = "txtDuplicateContent";
            this.txtDuplicateContent.ReadOnly = true;
            this.txtDuplicateContent.Size = new System.Drawing.Size(121, 21);
            this.txtDuplicateContent.TabIndex = 7;
            // 
            // txtDuplicateDbContent
            // 
            this.txtDuplicateDbContent.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtDuplicateDbContent.Location = new System.Drawing.Point(281, 386);
            this.txtDuplicateDbContent.Name = "txtDuplicateDbContent";
            this.txtDuplicateDbContent.ReadOnly = true;
            this.txtDuplicateDbContent.Size = new System.Drawing.Size(121, 21);
            this.txtDuplicateDbContent.TabIndex = 8;
            // 
            // txtFiledTypeError
            // 
            this.txtFiledTypeError.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtFiledTypeError.Location = new System.Drawing.Point(408, 386);
            this.txtFiledTypeError.Name = "txtFiledTypeError";
            this.txtFiledTypeError.ReadOnly = true;
            this.txtFiledTypeError.Size = new System.Drawing.Size(121, 21);
            this.txtFiledTypeError.TabIndex = 9;
            // 
            // txtCharacterLengthError
            // 
            this.txtCharacterLengthError.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtCharacterLengthError.Location = new System.Drawing.Point(535, 386);
            this.txtCharacterLengthError.Name = "txtCharacterLengthError";
            this.txtCharacterLengthError.ReadOnly = true;
            this.txtCharacterLengthError.Size = new System.Drawing.Size(121, 21);
            this.txtCharacterLengthError.TabIndex = 10;
            // 
            // txtErrorContent
            // 
            this.txtErrorContent.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtErrorContent.Location = new System.Drawing.Point(662, 386);
            this.txtErrorContent.Name = "txtErrorContent";
            this.txtErrorContent.ReadOnly = true;
            this.txtErrorContent.Size = new System.Drawing.Size(117, 21);
            this.txtErrorContent.TabIndex = 11;
            // 
            // btnBatchOverwrite
            // 
            this.btnBatchOverwrite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBatchOverwrite.Location = new System.Drawing.Point(587, 384);
            this.btnBatchOverwrite.Name = "btnBatchOverwrite";
            this.btnBatchOverwrite.Size = new System.Drawing.Size(136, 23);
            this.btnBatchOverwrite.TabIndex = 15;
            this.btnBatchOverwrite.Text = "BatchOverwrite";
            this.btnBatchOverwrite.UseVisualStyleBackColor = true;
            this.btnBatchOverwrite.Click += new System.EventHandler(this.btnBatchOverwrite_Click);
            // 
            // btnBatchsSaving
            // 
            this.btnBatchsSaving.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBatchsSaving.Location = new System.Drawing.Point(427, 384);
            this.btnBatchsSaving.Name = "btnBatchsSaving";
            this.btnBatchsSaving.Size = new System.Drawing.Size(136, 23);
            this.btnBatchsSaving.TabIndex = 14;
            this.btnBatchsSaving.Text = "BatchsSaving";
            this.btnBatchsSaving.UseVisualStyleBackColor = true;
            this.btnBatchsSaving.Click += new System.EventHandler(this.btnBatchsSaving_Click);
            // 
            // btnNotSelectAll
            // 
            this.btnNotSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNotSelectAll.Location = new System.Drawing.Point(235, 384);
            this.btnNotSelectAll.Name = "btnNotSelectAll";
            this.btnNotSelectAll.Size = new System.Drawing.Size(136, 23);
            this.btnNotSelectAll.TabIndex = 13;
            this.btnNotSelectAll.Text = "Not select all";
            this.btnNotSelectAll.UseVisualStyleBackColor = true;
            this.btnNotSelectAll.Click += new System.EventHandler(this.btnNotSelectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectAll.Location = new System.Drawing.Point(67, 384);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(136, 23);
            this.btnSelectAll.TabIndex = 12;
            this.btnSelectAll.Text = "Select all";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // ReadAndSaveFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBatchOverwrite);
            this.Controls.Add(this.btnBatchsSaving);
            this.Controls.Add(this.btnNotSelectAll);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.txtErrorContent);
            this.Controls.Add(this.txtCharacterLengthError);
            this.Controls.Add(this.txtFiledTypeError);
            this.Controls.Add(this.txtDuplicateDbContent);
            this.Controls.Add(this.txtDuplicateContent);
            this.Controls.Add(this.txtEmptyContent);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.btnReadFile);
            this.Controls.Add(this.dgvDataTable);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "ReadAndSaveFileForm";
            this.Text = "ReadAndSaveFileForm";
            this.Load += new System.EventHandler(this.ReadAndSaveFileForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.DataGridView dgvDataTable;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtEmptyContent;
        private System.Windows.Forms.TextBox txtDuplicateContent;
        private System.Windows.Forms.TextBox txtDuplicateDbContent;
        private System.Windows.Forms.TextBox txtFiledTypeError;
        private System.Windows.Forms.TextBox txtCharacterLengthError;
        private System.Windows.Forms.TextBox txtErrorContent;
        private System.Windows.Forms.Button btnBatchOverwrite;
        private System.Windows.Forms.Button btnBatchsSaving;
        private System.Windows.Forms.Button btnNotSelectAll;
        private System.Windows.Forms.Button btnSelectAll;
    }
}