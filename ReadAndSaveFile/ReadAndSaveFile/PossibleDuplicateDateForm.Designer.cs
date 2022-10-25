namespace ReadAndSaveFile
{
    partial class PossibleDuplicateDateForm
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
            this.dgvDuplicateDataTable = new System.Windows.Forms.DataGridView();
            this.dgvPossibleDuplicateDataTable = new System.Windows.Forms.DataGridView();
            this.dgvNotDuplicateDataTable = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuplicateDataTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPossibleDuplicateDataTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotDuplicateDataTable)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDuplicateDataTable
            // 
            this.dgvDuplicateDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDuplicateDataTable.Location = new System.Drawing.Point(0, 1);
            this.dgvDuplicateDataTable.Name = "dgvDuplicateDataTable";
            this.dgvDuplicateDataTable.RowTemplate.Height = 23;
            this.dgvDuplicateDataTable.Size = new System.Drawing.Size(606, 126);
            this.dgvDuplicateDataTable.TabIndex = 0;
            // 
            // dgvPossibleDuplicateDataTable
            // 
            this.dgvPossibleDuplicateDataTable.AllowUserToAddRows = false;
            this.dgvPossibleDuplicateDataTable.AllowUserToDeleteRows = false;
            this.dgvPossibleDuplicateDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPossibleDuplicateDataTable.Location = new System.Drawing.Point(0, 123);
            this.dgvPossibleDuplicateDataTable.Name = "dgvPossibleDuplicateDataTable";
            this.dgvPossibleDuplicateDataTable.ReadOnly = true;
            this.dgvPossibleDuplicateDataTable.RowTemplate.Height = 23;
            this.dgvPossibleDuplicateDataTable.Size = new System.Drawing.Size(606, 126);
            this.dgvPossibleDuplicateDataTable.TabIndex = 1;
            // 
            // dgvNotDuplicateDataTable
            // 
            this.dgvNotDuplicateDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNotDuplicateDataTable.Location = new System.Drawing.Point(0, 249);
            this.dgvNotDuplicateDataTable.Name = "dgvNotDuplicateDataTable";
            this.dgvNotDuplicateDataTable.RowTemplate.Height = 23;
            this.dgvNotDuplicateDataTable.Size = new System.Drawing.Size(606, 126);
            this.dgvNotDuplicateDataTable.TabIndex = 2;
            // 
            // PossibleDuplicateDateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 409);
            this.Controls.Add(this.dgvNotDuplicateDataTable);
            this.Controls.Add(this.dgvPossibleDuplicateDataTable);
            this.Controls.Add(this.dgvDuplicateDataTable);
            this.Name = "PossibleDuplicateDateForm";
            this.Text = "PossibleDuplicateDateForm";
            this.Load += new System.EventHandler(this.PossibleDuplicateDateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuplicateDataTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPossibleDuplicateDataTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotDuplicateDataTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDuplicateDataTable;
        private System.Windows.Forms.DataGridView dgvPossibleDuplicateDataTable;
        private System.Windows.Forms.DataGridView dgvNotDuplicateDataTable;
    }
}