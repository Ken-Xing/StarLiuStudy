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
            this.txtErrorLog = new System.Windows.Forms.TextBox();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtEmptyContent = new System.Windows.Forms.TextBox();
            this.txtDuplicateContent = new System.Windows.Forms.TextBox();
            this.txtDuplicateDbContent = new System.Windows.Forms.TextBox();
            this.txtFiledTypeError = new System.Windows.Forms.TextBox();
            this.txtCharacterLengthError = new System.Windows.Forms.TextBox();
            this.txtErrorContent = new System.Windows.Forms.TextBox();
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
            this.dgvDataTable.Location = new System.Drawing.Point(0, 0);
            this.dgvDataTable.Name = "dgvDataTable";
            this.dgvDataTable.ReadOnly = true;
            this.dgvDataTable.RowHeadersWidth = 51;
            this.dgvDataTable.RowTemplate.Height = 23;
            this.dgvDataTable.Size = new System.Drawing.Size(569, 372);
            this.dgvDataTable.TabIndex = 0;
            // 
            // txtErrorLog
            // 
            this.txtErrorLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtErrorLog.Location = new System.Drawing.Point(568, 0);
            this.txtErrorLog.Multiline = true;
            this.txtErrorLog.Name = "txtErrorLog";
            this.txtErrorLog.ReadOnly = true;
            this.txtErrorLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtErrorLog.Size = new System.Drawing.Size(232, 372);
            this.txtErrorLog.TabIndex = 4;
            this.txtErrorLog.WordWrap = false;
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSaveFile.Location = new System.Drawing.Point(443, 415);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(75, 23);
            this.btnSaveFile.TabIndex = 3;
            this.btnSaveFile.Text = "Save file";
            this.btnSaveFile.UseVisualStyleBackColor = true;
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
            // ReadAndSaveFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtErrorContent);
            this.Controls.Add(this.txtCharacterLengthError);
            this.Controls.Add(this.txtFiledTypeError);
            this.Controls.Add(this.txtDuplicateDbContent);
            this.Controls.Add(this.txtDuplicateContent);
            this.Controls.Add(this.txtEmptyContent);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.txtErrorLog);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.btnReadFile);
            this.Controls.Add(this.dgvDataTable);
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
        private System.Windows.Forms.TextBox txtErrorLog;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtEmptyContent;
        private System.Windows.Forms.TextBox txtDuplicateContent;
        private System.Windows.Forms.TextBox txtDuplicateDbContent;
        private System.Windows.Forms.TextBox txtFiledTypeError;
        private System.Windows.Forms.TextBox txtCharacterLengthError;
        private System.Windows.Forms.TextBox txtErrorContent;
    }
}