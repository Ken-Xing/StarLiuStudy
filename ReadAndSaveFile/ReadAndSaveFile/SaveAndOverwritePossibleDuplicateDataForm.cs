using DbBase;
using ReadAndSaveCSVFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ReadAndSaveFile
{
    public partial class SaveAndOverwritePossibleDuplicateDataForm : Form
    {
        #region member
        private CSVFileHelper _cSVFileHelper = null;
        private string _connStr = string.Empty;
        #endregion

        public SaveAndOverwritePossibleDuplicateDataForm()
        {
            InitializeComponent();
        }
        public SaveAndOverwritePossibleDuplicateDataForm(CSVFileHelper cSVFileHelper, string connStr)
        {
            InitializeComponent();
            this._cSVFileHelper = cSVFileHelper;
            this._connStr = connStr;
        }
        private void SaveAndOverwritePossibleDuplicateDataForm_Load(object sender, EventArgs e)
        {

            this.dgvDataTable.DataSource = this._cSVFileHelper.PossibleDuplicateDataTable;
            DataGridViewButtonColumn btnOverwrite = new DataGridViewButtonColumn();
            DataGridViewButtonColumn btnSave = new DataGridViewButtonColumn();
            DataGridViewCheckBoxColumn chkSelect = new DataGridViewCheckBoxColumn();
            chkSelect.Name = "Select";
            chkSelect.TrueValue = true;
            chkSelect.FalseValue = false;
            chkSelect.DataPropertyName = "isChecked";
            chkSelect.Resizable = DataGridViewTriState.False;
            btnOverwrite.Name = "Overwrite";
            btnSave.Name = "Save";
            btnSave.DefaultCellStyle.NullValue = "Save";
            btnOverwrite.DefaultCellStyle.NullValue = "Overwrite";
            chkSelect.DefaultCellStyle.NullValue = "Select";
            if (this.dgvDataTable.Rows.Count > 1)
            {
                this.dgvDataTable.Columns.Insert(0, chkSelect);
            }

            this.dgvDataTable.Columns.Add(btnSave);
            this.dgvDataTable.Columns.Add(btnOverwrite);

        }

        private void dgvDataTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            string saveSql = "EXEC sp_AddStudentAdmissionInfoDetial  @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
            string overwriteSql = "EXEC sp_OverwriteStudentAdmissionInfoSnumber @Name = @Name, @Age = @Age, @Sex = @Sex, @Email= @Email";
            int selectRowIndex = this.dgvDataTable.SelectedCells[0].RowIndex;
            DbHelper dbHelper = new DbHelper(this._connStr);


            //Overwrite Possible duplicate data
            if (this.dgvDataTable.Columns[e.ColumnIndex].Name.Equals("Overwrite"))
            {
                sqlParametersList.Add(new SqlParameter("@Name", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[selectRowIndex]["Name"]));
                sqlParametersList.Add(new SqlParameter("@Age", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[selectRowIndex]["Age"]));
                sqlParametersList.Add(new SqlParameter("@Sex", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[selectRowIndex]["Sex"]));
                sqlParametersList.Add(new SqlParameter("@Email", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[selectRowIndex]["Email"]));

                try
                {
                    if (dbHelper.SaveModifyDeleteData(overwriteSql, sqlParametersList))
                    {
                        MessageBox.Show("Success");
                        this.dgvDataTable.Rows.RemoveAt(selectRowIndex);
                        //this._log.Append(DateTime.Now.ToString("HH:mm:ss") + " " + "Overwrite data successfully!" + "\r\n");
                        //this.txtLog.Text = this._log.ToString();
                    }
                    else
                    {
                        MessageBox.Show("fail");
                    }

                }
                catch
                {

                    MessageBox.Show("fail");
                }
            }
            //Save as new data
            else if (this.dgvDataTable.Columns[e.ColumnIndex].Name.Equals("Save"))
            {
                sqlParametersList.Add(new SqlParameter("@Name", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[selectRowIndex]["Name"]));
                sqlParametersList.Add(new SqlParameter("@Age", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[selectRowIndex]["Age"]));
                sqlParametersList.Add(new SqlParameter("@Sex", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[selectRowIndex]["Sex"]));
                sqlParametersList.Add(new SqlParameter("@Email", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[selectRowIndex]["Email"]));

                try
                {
                    if (dbHelper.SaveModifyDeleteData(saveSql, sqlParametersList))
                    {
                        MessageBox.Show("Success");
                        this.dgvDataTable.Rows.RemoveAt(selectRowIndex);
                        //this._log.Append(DateTime.Now.ToString("HH:mm:ss") + " " + "Save data successfully!" + "\r\n");
                        //this.txtLog.Text = this._log.ToString();
                    }
                    else
                    {
                        MessageBox.Show("fail");
                    }

                }
                catch
                {

                    MessageBox.Show("fail");
                }
            }

            sqlParametersList.Clear();
        }

        private void dgvDataTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (this.dgvDataTable.Columns[e.ColumnIndex].Name == "Select")
            {
                if (e.Value == null) e.Value = false;
            }
        }

        private void dgvDataTable_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if ((bool)dgvDataTable.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
                {
                    this.dgvDataTable.Rows[e.RowIndex].Cells[0].Value = false;
                }
                else
                {
                    this.dgvDataTable.Rows[e.RowIndex].Cells[0].Value = true;
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            //Select all
            for (int i = 0; i < dgvDataTable.Rows.Count; i++)
            {
                this.dgvDataTable.Rows[i].Cells[0].Value = true;
            }
        }

        /// <summary>
        /// Rewriting potentially duplicate data in bulk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_NotSelectAll_Click(object sender, EventArgs e)
        {
            //Not select all
            for (int i = 0; i < dgvDataTable.Rows.Count; i++)
            {
                this.dgvDataTable.Rows[i].Cells[0].Value = false;
            }
        }

        /// <summary>
        /// Store potentially duplicate data in bulk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBatchsSaving_Click(object sender, EventArgs e)
        {
            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            string saveSql = "EXEC sp_AddStudentAdmissionInfoDetial  @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
            DbHelper dbHelper = new DbHelper(this._connStr);
            dbHelper.OpenDbConnection();
            SqlTransaction sqlTransaction = dbHelper.SqlCon.BeginTransaction();
            SqlCommand sqlCommand = new SqlCommand(saveSql, dbHelper.SqlCon);
            sqlCommand.Transaction = sqlTransaction;
            bool result = false;


            for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
            {
                if (this.dgvDataTable.Rows[i].Cells[0].Equals(null))
                {
                    MessageBox.Show("Not all rows were selected");
                }
                else
                {


                    try
                    {
                        for (int j = 0; j < this.dgvDataTable.Rows.Count; j++)
                        {

                            sqlParametersList.Add(new SqlParameter("@Name", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[j]["Name"]));
                            sqlParametersList.Add(new SqlParameter("@Age", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[j]["Age"]));
                            sqlParametersList.Add(new SqlParameter("@Sex", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[j]["Sex"]));
                            sqlParametersList.Add(new SqlParameter("@Email", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[j]["Email"]));

                            result = dbHelper.SaveModifyDeleteBulkData(saveSql, sqlParametersList, sqlCommand);
                            sqlParametersList.Clear();

                            if (result == false)
                            {
                                sqlTransaction.Rollback();
                                break;
                            }
                        }
                        sqlTransaction.Commit();
                    }
                    catch
                    {
                        MessageBox.Show("fail");

                    }
                    finally
                    {
                        dbHelper.CloseDbConnection();
                    }

                    MessageBox.Show("Success");
                }
            }

        }

        private void btnBatchRewrite_Click(object sender, EventArgs e)
        {
            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            string overwriteSql = "EXEC sp_OverwriteStudentAdmissionInfoSnumber @Name = @Name, @Age = @Age, @Sex = @Sex, @Email= @Email";
            DbHelper dbHelper = new DbHelper(this._connStr);
            dbHelper.OpenDbConnection();
            SqlTransaction sqlTransaction = dbHelper.SqlCon.BeginTransaction();
            SqlCommand sqlCommand = new SqlCommand(overwriteSql, dbHelper.SqlCon);
            sqlCommand.Transaction = sqlTransaction;
            bool result = false;

            for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
            {
                if (this.dgvDataTable.Rows[i].Cells[0].Equals(null))
                {
                    MessageBox.Show("Not all rows were selected");
                }
                else
                {
                    try
                    {
                        for (int j = 0; i < this.dgvDataTable.Rows.Count; i++)
                        {
                            if ((bool)this.dgvDataTable.Rows[i].Cells[0].Value == true)
                            {
                                sqlParametersList.Add(new SqlParameter("@Name", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i]["Name"]));
                                sqlParametersList.Add(new SqlParameter("@Age", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i]["Age"]));
                                sqlParametersList.Add(new SqlParameter("@Sex", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i]["Sex"]));
                                sqlParametersList.Add(new SqlParameter("@Email", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i]["Email"]));
                            }
                            else
                            {
                                MessageBox.Show("Not all rows were selected!");
                                break;
                            }

                            result = dbHelper.SaveModifyDeleteBulkData(overwriteSql, sqlParametersList, sqlCommand);
                            sqlParametersList.Clear();

                            if (result == false)
                            {
                                sqlTransaction.Rollback();
                                break;
                            }

                        }

                        sqlTransaction.Commit();
                    }
                    catch
                    {
                        MessageBox.Show("fail");

                    }
                    finally
                    {
                        dbHelper.CloseDbConnection();
                    }

                    MessageBox.Show("Success");

                    this.Close();
                }
            }
        }
    }
}

