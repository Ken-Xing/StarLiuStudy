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
            this.pnlReadSaveRefreshButton = new System.Windows.Forms.Panel();
            this.btnValidate = new System.Windows.Forms.Button();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.lstvLog = new System.Windows.Forms.ListView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblErrorContentColor = new System.Windows.Forms.Label();
            this.lblDuplicateContentColor = new System.Windows.Forms.Label();
            this.lblDuplicateDataBaseDataColor = new System.Windows.Forms.Label();
            this.lblEmptyContentColor = new System.Windows.Forms.Label();
            this.lblDataTypeErrorColor = new System.Windows.Forms.Label();
            this.lblCharterLengthErrorColor = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblErrorContent = new System.Windows.Forms.Label();
            this.lblDuplicateContent = new System.Windows.Forms.Label();
            this.lblDuplicateDataBaseData = new System.Windows.Forms.Label();
            this.lblEmptyContent = new System.Windows.Forms.Label();
            this.lblDataTypeError = new System.Windows.Forms.Label();
            this.lblCharacterLengthError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataTable)).BeginInit();
            this.pnlReadSaveRefreshButton.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
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
            // pnlReadSaveRefreshButton
            // 
            this.pnlReadSaveRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlReadSaveRefreshButton.Controls.Add(this.btnValidate);
            this.pnlReadSaveRefreshButton.Controls.Add(this.btnSaveFile);
            this.pnlReadSaveRefreshButton.Controls.Add(this.btnReload);
            this.pnlReadSaveRefreshButton.Controls.Add(this.btnReadFile);
            this.pnlReadSaveRefreshButton.Location = new System.Drawing.Point(0, 416);
            this.pnlReadSaveRefreshButton.Name = "pnlReadSaveRefreshButton";
            this.pnlReadSaveRefreshButton.Size = new System.Drawing.Size(800, 34);
            this.pnlReadSaveRefreshButton.TabIndex = 26;
            // 
            // btnValidate
            // 
            this.btnValidate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnValidate.Location = new System.Drawing.Point(378, 6);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(78, 21);
            this.btnValidate.TabIndex = 30;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSaveFile.Location = new System.Drawing.Point(294, 6);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(78, 21);
            this.btnSaveFile.TabIndex = 29;
            this.btnSaveFile.Text = "Save file";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // btnReload
            // 
            this.btnReload.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnReload.Location = new System.Drawing.Point(462, 5);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(78, 22);
            this.btnReload.TabIndex = 27;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnReadFile
            // 
            this.btnReadFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnReadFile.AutoEllipsis = true;
            this.btnReadFile.Location = new System.Drawing.Point(213, 5);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(75, 22);
            this.btnReadFile.TabIndex = 26;
            this.btnReadFile.Text = "Open file";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // lstvLog
            // 
            this.lstvLog.HideSelection = false;
            this.lstvLog.Location = new System.Drawing.Point(574, 0);
            this.lstvLog.Name = "lstvLog";
            this.lstvLog.Size = new System.Drawing.Size(214, 267);
            this.lstvLog.TabIndex = 30;
            this.lstvLog.UseCompatibleStateImageBehavior = false;
            this.lstvLog.SelectedIndexChanged += new System.EventHandler(this.lstvLog_SelectedIndexChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lblErrorContentColor);
            this.flowLayoutPanel1.Controls.Add(this.lblDuplicateContentColor);
            this.flowLayoutPanel1.Controls.Add(this.lblDuplicateDataBaseDataColor);
            this.flowLayoutPanel1.Controls.Add(this.lblEmptyContentColor);
            this.flowLayoutPanel1.Controls.Add(this.lblDataTypeErrorColor);
            this.flowLayoutPanel1.Controls.Add(this.lblCharterLengthErrorColor);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(732, 273);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(56, 74);
            this.flowLayoutPanel1.TabIndex = 31;
            // 
            // lblErrorContentColor
            // 
            this.lblErrorContentColor.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblErrorContentColor.Location = new System.Drawing.Point(3, 0);
            this.lblErrorContentColor.Name = "lblErrorContentColor";
            this.lblErrorContentColor.Size = new System.Drawing.Size(50, 12);
            this.lblErrorContentColor.TabIndex = 40;
            // 
            // lblDuplicateContentColor
            // 
            this.lblDuplicateContentColor.BackColor = System.Drawing.Color.Gray;
            this.lblDuplicateContentColor.Location = new System.Drawing.Point(3, 12);
            this.lblDuplicateContentColor.Name = "lblDuplicateContentColor";
            this.lblDuplicateContentColor.Size = new System.Drawing.Size(50, 12);
            this.lblDuplicateContentColor.TabIndex = 41;
            // 
            // lblDuplicateDataBaseDataColor
            // 
            this.lblDuplicateDataBaseDataColor.BackColor = System.Drawing.Color.LightYellow;
            this.lblDuplicateDataBaseDataColor.Location = new System.Drawing.Point(3, 24);
            this.lblDuplicateDataBaseDataColor.Name = "lblDuplicateDataBaseDataColor";
            this.lblDuplicateDataBaseDataColor.Size = new System.Drawing.Size(50, 12);
            this.lblDuplicateDataBaseDataColor.TabIndex = 42;
            // 
            // lblEmptyContentColor
            // 
            this.lblEmptyContentColor.BackColor = System.Drawing.Color.Red;
            this.lblEmptyContentColor.Location = new System.Drawing.Point(3, 36);
            this.lblEmptyContentColor.Name = "lblEmptyContentColor";
            this.lblEmptyContentColor.Size = new System.Drawing.Size(50, 12);
            this.lblEmptyContentColor.TabIndex = 39;
            // 
            // lblDataTypeErrorColor
            // 
            this.lblDataTypeErrorColor.BackColor = System.Drawing.Color.Orange;
            this.lblDataTypeErrorColor.Location = new System.Drawing.Point(3, 48);
            this.lblDataTypeErrorColor.Name = "lblDataTypeErrorColor";
            this.lblDataTypeErrorColor.Size = new System.Drawing.Size(50, 12);
            this.lblDataTypeErrorColor.TabIndex = 43;
            // 
            // lblCharterLengthErrorColor
            // 
            this.lblCharterLengthErrorColor.BackColor = System.Drawing.Color.LightPink;
            this.lblCharterLengthErrorColor.Location = new System.Drawing.Point(3, 60);
            this.lblCharterLengthErrorColor.Name = "lblCharterLengthErrorColor";
            this.lblCharterLengthErrorColor.Size = new System.Drawing.Size(50, 12);
            this.lblCharterLengthErrorColor.TabIndex = 44;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.lblErrorContent);
            this.flowLayoutPanel2.Controls.Add(this.lblDuplicateContent);
            this.flowLayoutPanel2.Controls.Add(this.lblDuplicateDataBaseData);
            this.flowLayoutPanel2.Controls.Add(this.lblEmptyContent);
            this.flowLayoutPanel2.Controls.Add(this.lblDataTypeError);
            this.flowLayoutPanel2.Controls.Add(this.lblCharacterLengthError);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(574, 273);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(160, 74);
            this.flowLayoutPanel2.TabIndex = 32;
            // 
            // lblErrorContent
            // 
            this.lblErrorContent.AutoSize = true;
            this.lblErrorContent.Location = new System.Drawing.Point(3, 0);
            this.lblErrorContent.Name = "lblErrorContent";
            this.lblErrorContent.Size = new System.Drawing.Size(83, 12);
            this.lblErrorContent.TabIndex = 0;
            this.lblErrorContent.Text = "Error content";
            // 
            // lblDuplicateContent
            // 
            this.lblDuplicateContent.AutoSize = true;
            this.lblDuplicateContent.Location = new System.Drawing.Point(3, 12);
            this.lblDuplicateContent.Name = "lblDuplicateContent";
            this.lblDuplicateContent.Size = new System.Drawing.Size(107, 12);
            this.lblDuplicateContent.TabIndex = 1;
            this.lblDuplicateContent.Text = "Duplicate content";
            // 
            // lblDuplicateDataBaseData
            // 
            this.lblDuplicateDataBaseData.AutoSize = true;
            this.lblDuplicateDataBaseData.Location = new System.Drawing.Point(3, 24);
            this.lblDuplicateDataBaseData.Name = "lblDuplicateDataBaseData";
            this.lblDuplicateDataBaseData.Size = new System.Drawing.Size(149, 12);
            this.lblDuplicateDataBaseData.TabIndex = 2;
            this.lblDuplicateDataBaseData.Text = "Duplicate database data ";
            // 
            // lblEmptyContent
            // 
            this.lblEmptyContent.AutoSize = true;
            this.lblEmptyContent.Location = new System.Drawing.Point(3, 36);
            this.lblEmptyContent.Name = "lblEmptyContent";
            this.lblEmptyContent.Size = new System.Drawing.Size(83, 12);
            this.lblEmptyContent.TabIndex = 3;
            this.lblEmptyContent.Text = "Empty content";
            // 
            // lblDataTypeError
            // 
            this.lblDataTypeError.AutoSize = true;
            this.lblDataTypeError.Location = new System.Drawing.Point(3, 48);
            this.lblDataTypeError.Name = "lblDataTypeError";
            this.lblDataTypeError.Size = new System.Drawing.Size(113, 12);
            this.lblDataTypeError.TabIndex = 4;
            this.lblDataTypeError.Text = "Content type error";
            // 
            // lblCharacterLengthError
            // 
            this.lblCharacterLengthError.AutoSize = true;
            this.lblCharacterLengthError.Location = new System.Drawing.Point(3, 60);
            this.lblCharacterLengthError.Name = "lblCharacterLengthError";
            this.lblCharacterLengthError.Size = new System.Drawing.Size(125, 12);
            this.lblCharacterLengthError.TabIndex = 5;
            this.lblCharacterLengthError.Text = "Content length error";
            // 
            // ReadAndSaveFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lstvLog);
            this.Controls.Add(this.pnlReadSaveRefreshButton);
            this.Controls.Add(this.dgvDataTable);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "ReadAndSaveFileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReadAndSaveFileForm";
            this.Load += new System.EventHandler(this.ReadAndSaveFileForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataTable)).EndInit();
            this.pnlReadSaveRefreshButton.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvDataTable;
        private System.Windows.Forms.Panel pnlReadSaveRefreshButton;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.ListView lstvLog;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblErrorContentColor;
        private System.Windows.Forms.Label lblDuplicateContentColor;
        private System.Windows.Forms.Label lblDuplicateDataBaseDataColor;
        private System.Windows.Forms.Label lblEmptyContentColor;
        private System.Windows.Forms.Label lblDataTypeErrorColor;
        private System.Windows.Forms.Label lblCharterLengthErrorColor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label lblErrorContent;
        private System.Windows.Forms.Label lblDuplicateContent;
        private System.Windows.Forms.Label lblDuplicateDataBaseData;
        private System.Windows.Forms.Label lblEmptyContent;
        private System.Windows.Forms.Label lblDataTypeError;
        private System.Windows.Forms.Label lblCharacterLengthError;
    }
}