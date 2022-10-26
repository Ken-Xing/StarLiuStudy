namespace ReadAndSaveFile
{
    partial class SaveAndOverwritePossibleDuplicateDataForm
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
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btn_NotSelectAll = new System.Windows.Forms.Button();
            this.btnBatchsSaving = new System.Windows.Forms.Button();
            this.btnBatchRewrite = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataTable)).BeginInit();
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
            this.dgvDataTable.Location = new System.Drawing.Point(-2, 25);
            this.dgvDataTable.Name = "dgvDataTable";
            this.dgvDataTable.ReadOnly = true;
            this.dgvDataTable.RowTemplate.Height = 23;
            this.dgvDataTable.Size = new System.Drawing.Size(647, 376);
            this.dgvDataTable.TabIndex = 0;
            this.dgvDataTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDataTable_CellContentClick);
            this.dgvDataTable.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDataTable_CellFormatting);
            this.dgvDataTable.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDataTable_CellMouseClick);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectAll.Location = new System.Drawing.Point(3, 2);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "Select all";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btn_NotSelectAll
            // 
            this.btn_NotSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_NotSelectAll.Location = new System.Drawing.Point(84, 2);
            this.btn_NotSelectAll.Name = "btn_NotSelectAll";
            this.btn_NotSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btn_NotSelectAll.TabIndex = 2;
            this.btn_NotSelectAll.Text = "Not select all";
            this.btn_NotSelectAll.UseVisualStyleBackColor = true;
            this.btn_NotSelectAll.Click += new System.EventHandler(this.btn_NotSelectAll_Click);
            // 
            // btnBatchsSaving
            // 
            this.btnBatchsSaving.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBatchsSaving.Location = new System.Drawing.Point(165, 2);
            this.btnBatchsSaving.Name = "btnBatchsSaving";
            this.btnBatchsSaving.Size = new System.Drawing.Size(75, 23);
            this.btnBatchsSaving.TabIndex = 3;
            this.btnBatchsSaving.Text = "BatchsSaving";
            this.btnBatchsSaving.UseVisualStyleBackColor = true;
            this.btnBatchsSaving.Click += new System.EventHandler(this.btnBatchsSaving_Click);
            // 
            // btnBatchRewrite
            // 
            this.btnBatchRewrite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBatchRewrite.Location = new System.Drawing.Point(246, 2);
            this.btnBatchRewrite.Name = "btnBatchRewrite";
            this.btnBatchRewrite.Size = new System.Drawing.Size(75, 23);
            this.btnBatchRewrite.TabIndex = 4;
            this.btnBatchRewrite.Text = "BatchRewrite";
            this.btnBatchRewrite.UseVisualStyleBackColor = true;
            this.btnBatchRewrite.Click += new System.EventHandler(this.btnBatchRewrite_Click);
            // 
            // SaveAndOverwritePossibleDuplicateDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBatchRewrite);
            this.Controls.Add(this.btnBatchsSaving);
            this.Controls.Add(this.btn_NotSelectAll);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.dgvDataTable);
            this.Name = "SaveAndOverwritePossibleDuplicateDataForm";
            this.Text = "SaveAndOverwritePossibleDuplicateDataForm";
            this.Load += new System.EventHandler(this.SaveAndOverwritePossibleDuplicateDataForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDataTable;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btn_NotSelectAll;
        private System.Windows.Forms.Button btnBatchsSaving;
        private System.Windows.Forms.Button btnBatchRewrite;
    }
}