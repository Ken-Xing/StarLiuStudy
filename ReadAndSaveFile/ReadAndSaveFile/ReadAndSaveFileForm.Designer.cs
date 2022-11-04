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
            this.fltLabelColor = new System.Windows.Forms.FlowLayoutPanel();
            this.lblErrorContentColor = new System.Windows.Forms.Label();
            this.lblPartDuplicateColor = new System.Windows.Forms.Label();
            this.lblDuplicateDataBaseDataColor = new System.Windows.Forms.Label();
            this.lblEmptyContentColor = new System.Windows.Forms.Label();
            this.lblDataTypeErrorColor = new System.Windows.Forms.Label();
            this.lblCharterLengthErrorColor = new System.Windows.Forms.Label();
            this.lblRowDuplicateColor = new System.Windows.Forms.Label();
            this.lblPossibleDuplicateColor = new System.Windows.Forms.Label();
            this.flpnlLabelText = new System.Windows.Forms.FlowLayoutPanel();
            this.lblErrorContent = new System.Windows.Forms.Label();
            this.lblPartDuplicate = new System.Windows.Forms.Label();
            this.lblDuplicateDataBaseData = new System.Windows.Forms.Label();
            this.lblEmptyContent = new System.Windows.Forms.Label();
            this.lblDataTypeError = new System.Windows.Forms.Label();
            this.lblCharacterLengthError = new System.Windows.Forms.Label();
            this.lblRowDuplicate = new System.Windows.Forms.Label();
            this.lblPossibleDuplicate = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataTable)).BeginInit();
            this.pnlReadSaveRefreshButton.SuspendLayout();
            this.fltLabelColor.SuspendLayout();
            this.flpnlLabelText.SuspendLayout();
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
            this.btnReadFile.Location = new System.Drawing.Point(213, 3);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(75, 22);
            this.btnReadFile.TabIndex = 26;
            this.btnReadFile.Text = "Open file";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // fltLabelColor
            // 
            this.fltLabelColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fltLabelColor.Controls.Add(this.lblErrorContentColor);
            this.fltLabelColor.Controls.Add(this.lblPartDuplicateColor);
            this.fltLabelColor.Controls.Add(this.lblDuplicateDataBaseDataColor);
            this.fltLabelColor.Controls.Add(this.lblEmptyContentColor);
            this.fltLabelColor.Controls.Add(this.lblDataTypeErrorColor);
            this.fltLabelColor.Controls.Add(this.lblCharterLengthErrorColor);
            this.fltLabelColor.Controls.Add(this.lblRowDuplicateColor);
            this.fltLabelColor.Controls.Add(this.lblPossibleDuplicateColor);
            this.fltLabelColor.Location = new System.Drawing.Point(732, 251);
            this.fltLabelColor.Name = "fltLabelColor";
            this.fltLabelColor.Size = new System.Drawing.Size(56, 96);
            this.fltLabelColor.TabIndex = 31;
            // 
            // lblErrorContentColor
            // 
            this.lblErrorContentColor.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblErrorContentColor.Location = new System.Drawing.Point(3, 0);
            this.lblErrorContentColor.Name = "lblErrorContentColor";
            this.lblErrorContentColor.Size = new System.Drawing.Size(50, 12);
            this.lblErrorContentColor.TabIndex = 40;
            // 
            // lblPartDuplicateColor
            // 
            this.lblPartDuplicateColor.BackColor = System.Drawing.Color.Gray;
            this.lblPartDuplicateColor.Location = new System.Drawing.Point(3, 12);
            this.lblPartDuplicateColor.Name = "lblPartDuplicateColor";
            this.lblPartDuplicateColor.Size = new System.Drawing.Size(50, 12);
            this.lblPartDuplicateColor.TabIndex = 41;
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
            // lblRowDuplicateColor
            // 
            this.lblRowDuplicateColor.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.lblRowDuplicateColor.Location = new System.Drawing.Point(3, 72);
            this.lblRowDuplicateColor.Name = "lblRowDuplicateColor";
            this.lblRowDuplicateColor.Size = new System.Drawing.Size(50, 12);
            this.lblRowDuplicateColor.TabIndex = 45;
            // 
            // lblPossibleDuplicateColor
            // 
            this.lblPossibleDuplicateColor.BackColor = System.Drawing.Color.Yellow;
            this.lblPossibleDuplicateColor.Location = new System.Drawing.Point(3, 84);
            this.lblPossibleDuplicateColor.Name = "lblPossibleDuplicateColor";
            this.lblPossibleDuplicateColor.Size = new System.Drawing.Size(50, 12);
            this.lblPossibleDuplicateColor.TabIndex = 46;
            // 
            // flpnlLabelText
            // 
            this.flpnlLabelText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flpnlLabelText.Controls.Add(this.lblErrorContent);
            this.flpnlLabelText.Controls.Add(this.lblPartDuplicate);
            this.flpnlLabelText.Controls.Add(this.lblDuplicateDataBaseData);
            this.flpnlLabelText.Controls.Add(this.lblEmptyContent);
            this.flpnlLabelText.Controls.Add(this.lblDataTypeError);
            this.flpnlLabelText.Controls.Add(this.lblCharacterLengthError);
            this.flpnlLabelText.Controls.Add(this.lblRowDuplicate);
            this.flpnlLabelText.Controls.Add(this.lblPossibleDuplicate);
            this.flpnlLabelText.Location = new System.Drawing.Point(574, 251);
            this.flpnlLabelText.Name = "flpnlLabelText";
            this.flpnlLabelText.Size = new System.Drawing.Size(160, 96);
            this.flpnlLabelText.TabIndex = 32;
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
            // lblPartDuplicate
            // 
            this.lblPartDuplicate.AutoSize = true;
            this.lblPartDuplicate.Location = new System.Drawing.Point(3, 12);
            this.lblPartDuplicate.Name = "lblPartDuplicate";
            this.lblPartDuplicate.Size = new System.Drawing.Size(89, 12);
            this.lblPartDuplicate.TabIndex = 1;
            this.lblPartDuplicate.Text = "Part duplicate";
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
            // lblRowDuplicate
            // 
            this.lblRowDuplicate.AutoSize = true;
            this.lblRowDuplicate.Location = new System.Drawing.Point(3, 72);
            this.lblRowDuplicate.Name = "lblRowDuplicate";
            this.lblRowDuplicate.Size = new System.Drawing.Size(83, 12);
            this.lblRowDuplicate.TabIndex = 6;
            this.lblRowDuplicate.Text = "Row duplicate";
            // 
            // lblPossibleDuplicate
            // 
            this.lblPossibleDuplicate.AutoSize = true;
            this.lblPossibleDuplicate.Location = new System.Drawing.Point(3, 84);
            this.lblPossibleDuplicate.Name = "lblPossibleDuplicate";
            this.lblPossibleDuplicate.Size = new System.Drawing.Size(113, 12);
            this.lblPossibleDuplicate.TabIndex = 7;
            this.lblPossibleDuplicate.Text = "Possible duplicate";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Message});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(574, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(226, 245);
            this.listView1.TabIndex = 33;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Message
            // 
            this.Message.Text = "Message";
            this.Message.Width = 201;
            // 
            // ReadAndSaveFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.flpnlLabelText);
            this.Controls.Add(this.fltLabelColor);
            this.Controls.Add(this.pnlReadSaveRefreshButton);
            this.Controls.Add(this.dgvDataTable);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "ReadAndSaveFileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReadAndSaveFileForm";
            this.Load += new System.EventHandler(this.ReadAndSaveFileForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataTable)).EndInit();
            this.pnlReadSaveRefreshButton.ResumeLayout(false);
            this.fltLabelColor.ResumeLayout(false);
            this.flpnlLabelText.ResumeLayout(false);
            this.flpnlLabelText.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvDataTable;
        private System.Windows.Forms.Panel pnlReadSaveRefreshButton;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.FlowLayoutPanel fltLabelColor;
        private System.Windows.Forms.Label lblErrorContentColor;
        private System.Windows.Forms.Label lblPartDuplicateColor;
        private System.Windows.Forms.Label lblDuplicateDataBaseDataColor;
        private System.Windows.Forms.Label lblEmptyContentColor;
        private System.Windows.Forms.Label lblDataTypeErrorColor;
        private System.Windows.Forms.Label lblCharterLengthErrorColor;
        private System.Windows.Forms.FlowLayoutPanel flpnlLabelText;
        private System.Windows.Forms.Label lblErrorContent;
        private System.Windows.Forms.Label lblPartDuplicate;
        private System.Windows.Forms.Label lblDuplicateDataBaseData;
        private System.Windows.Forms.Label lblEmptyContent;
        private System.Windows.Forms.Label lblDataTypeError;
        private System.Windows.Forms.Label lblCharacterLengthError;
        private System.Windows.Forms.Label lblRowDuplicateColor;
        private System.Windows.Forms.Label lblRowDuplicate;
        private System.Windows.Forms.Label lblPossibleDuplicateColor;
        private System.Windows.Forms.Label lblPossibleDuplicate;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Message;
    }
}