using DbBase;
using ReadAndSaveCSVFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace ReadAndSaveFile
{
    public partial class ReadAndSaveFileForm : Form
    {
        #region member
        private string _filePath = string.Empty;
        private string _connStr = string.Empty;
        private CSVFileHelper _cSVFileHelper = new CSVFileHelper();
        private StringBuilder _log = new StringBuilder();
        #endregion

        public ReadAndSaveFileForm()
        {
            InitializeComponent();
        }

        private void ReadAndSaveFileForm_Load(object sender, EventArgs e)
        {
            //Disable the savefile button when the form is loaded
            this.btnSaveFile.Enabled = false;
            this.btnRefresh.Enabled = false;
            this.txtEmptyContent.Text = "EmptyContent";
            this.txtDuplicateContent.Text = "DuplicateContent";
            this.txtDuplicateDbContent.Text = "DuplicateDatabaseContent";
            this.txtErrorContent.Text = "ErrorContent";
            this.txtCharacterLengthError.Text = "CharacterLengthError";
            this.txtFiledTypeError.Text = "FiledTypeError";
            //Change the background color of the input box
            this.txtEmptyContent.BackColor = Color.Red;
            this.txtDuplicateContent.BackColor = Color.Gray;
            this.txtDuplicateDbContent.BackColor = Color.LightYellow;
            this.txtCharacterLengthError.BackColor = Color.LightPink;
            this.txtFiledTypeError.BackColor = Color.Orange;
            this.txtErrorContent.BackColor = Color.LightSteelBlue;
            this.UpdateInputVisibleState(false);
            this.UpdateButtonVisibleState(false);
        }

        /// <summary>
        /// Reads the file and displays its contents
        /// </summary>
        /// <param name="sender">The btnReadFile_Click object itself</param>
        /// <param name="e">Record additional information for clicking btnReadFile</param>
        private void btnReadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get file Path
                this._filePath = openFileDialog.FileName;

                if (this._filePath != string.Empty && Path.GetExtension(this._filePath) == ".csv")
                {
                    try
                    {
                        //Get file content
                        this._cSVFileHelper.GetFileDataToDataTable(this._filePath);
                        //Display csv file datatable
                        this.dgvDataTable.DataSource = this._cSVFileHelper.CsvContentDataTable.Copy();

                    }
                    catch (ReadAndSaveFileException ex)
                    {
                        MessageBox.Show(ex.ExceptionMessage.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please select the extension '.csv' file.");
                }
            }
            if (this._cSVFileHelper.CsvContentDataTable != null)
            {
                this.btnSaveFile.Enabled = true;
                this.btnRefresh.Enabled = true;
            }
        }

        /// <summary>
        /// Save file content data 
        /// </summary>
        /// <param name="sender">The btnSaveFile_Click object itself</param>
        /// <param name="e">Record additional information for clicking btnSaveFile</param>
        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            this._connStr = "Server = 192.168.0.236,1433; Initial Catalog = ITLTest;user Id = EKSDBUser; Password = qwe123!@#;";
            string targetTable = "StudentAdmissionInfo";
            string findDuplicateDataSql = "Exec sp_CheckStudentAdmissionInfoEmailIsExists @Email = @Email";
            string findPossibleDuplicateDataSql = "Exec sp_CheckStudentAdmissionInforDetailIsMatch @Name = @Name, @Age = @Age, @Sex = @Sex";
            string updateDataSql = "EXEC sp_UpdateStudentAdmissionInfoBySnmber @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
            DbHelper dbhelper = new DbHelper(this._connStr);
            string fileContent = string.Empty;
            int nameMaxLength = 64;
            int emailMaxLength = 64;
            DataTable duplicateDataTable = new DataTable();
            DataTable notDublicateDataTable = new DataTable();
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

            if (this._cSVFileHelper.CsvContentDataTable != null)
            {
                //Get the file content data
                try
                {
                    //Determines if it matches the structure of the data table in the database
                    if (this._cSVFileHelper.CheckIsMatch(nameMaxLength, emailMaxLength))
                    {
                        //Check if the file contains duplicate content
                        if (this._cSVFileHelper.CheckCSVDataIsDuplicated())
                        {
                            //Get duplicate and non-duplicate data
                            this._cSVFileHelper.SiftFileContents(findDuplicateDataSql, findPossibleDuplicateDataSql, this._connStr);
                            if (this._cSVFileHelper.PossibleDuplicateDataTable.Rows.Count > 0)
                            {

                                MessageBox.Show("If suspected duplicate data is found, please deal with suspected duplicate data!");
                                this.UpdateInputVisibleState(false);
                                this.dgvDataTable.DataSource = this._cSVFileHelper.PossibleDuplicateDataTable;
                                this.dgvDataTable.Columns.Add(btnSave);
                                this.dgvDataTable.Columns.Add(btnOverwrite);

                                //Add checkbox and buttons
                                if (this.dgvDataTable.Rows.Count > 1)
                                {
                                    this.dgvDataTable.Columns.Insert(0, chkSelect);
                                }

                                for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
                                {
                                    this.dgvDataTable.Rows[i].Cells[0].Value = false;
                                }
                                if (this.dgvDataTable.Rows.Count > 0)
                                {
                                    this.btnSaveFile.Enabled = false;
                                }
                                this.UpdateButtonVisibleState(true);

                            }

                            if (this._cSVFileHelper.PossibleDuplicateDataTable.Rows.Count == 0)
                            {
                                if (this._cSVFileHelper.DuplicateDataTable.Rows.Count > 0)
                                {
                                    DialogResult dialogResult = MessageBox.Show("The data in the file and the data in the database are duplicated.Do you override duplicate data?", "File content duplication prompts", MessageBoxButtons.YesNo);
                                    //Duplicate data is overwritten if it exists
                                    if (dialogResult.Equals(DialogResult.Yes))
                                    {
                                        //Insert non-duplicate data and update duplicate data
                                        if (this._cSVFileHelper.SaveAndUpdateData(updateDataSql, updateDataSql, this._cSVFileHelper.DuplicateDataTable, this._cSVFileHelper.NotDuplicateDataTable, targetTable))
                                        {
                                            this._filePath = string.Empty;
                                            this._cSVFileHelper.DestroyCSVContentDataTable();
                                            this.dgvDataTable.DataSource = null;
                                            MessageBox.Show("Success");
                                        }
                                        else
                                        {
                                            MessageBox.Show("Fail");
                                        }
                                    }
                                    //Insert data that is not duplicated
                                    else
                                    {
                                        //save file content data to database
                                        if (dbhelper.SaveBulkNotDuplicateData(notDublicateDataTable, targetTable))
                                        {
                                            this._filePath = string.Empty;
                                            this._cSVFileHelper.DestroyCSVContentDataTable();
                                            this.dgvDataTable.DataSource = null;
                                            MessageBox.Show("Success");
                                        }
                                        else
                                        {
                                            MessageBox.Show("Fail");
                                        }
                                    }
                                }
                                //Holds the contents of data that is not duplicated
                                else
                                {
                                    if (dbhelper.SaveBulkNotDuplicateData(notDublicateDataTable, targetTable))
                                    {
                                        this._filePath = string.Empty;
                                        this._cSVFileHelper.DestroyCSVContentDataTable();
                                        this.dgvDataTable.DataSource = null;
                                        MessageBox.Show("Success");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Fail");
                                    }
                                }
                            }
                            else
                            {
                                this.displayErrorCellAndLog();
                            }
                        }
                    }
                    else
                    {
                        this.displayErrorCellAndLog();
                        this.UpdateInputVisibleState(true);
                        MessageBox.Show("The file content does not conform to the rules.  See the error log output window for details");
                    }
                }
                //Catch custom exceptions
                catch (ReadAndSaveFileException ex)
                {
                    MessageBox.Show(ex.ExceptionMessage.ToString());
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                MessageBox.Show("Please select the file you want to open!");
            }
        }

        /// <summary>
        /// Re-read the contents of the file
        /// </summary>
        /// <param name="sender">The btnRefresh_Click object itself</param>
        /// <param name="e">Record additional information for clicking btnRefresh</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this._filePath != null)
            {
                try
                {
                    //Get file content
                    this._cSVFileHelper.GetFileDataToDataTable(this._filePath);
                    //Display csv file datatable
                    this.dgvDataTable.DataSource = this._cSVFileHelper.CsvContentDataTable;
                }
                catch (ReadAndSaveFileException ex)
                {
                    MessageBox.Show(ex.ExceptionMessage.ToString());
                }
            }
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

        /// <summary>
        /// Mouse click events in dgvDataTable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDataTable_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int btnSaveIndex = 0;
            int btnOverwriteIndex = 0;

            for (int i = 0; i < dgvDataTable.Columns.Count; i++)
            {
                if (this.dgvDataTable.Columns[i].Name.Equals("Save"))
                {
                    btnSaveIndex = i;
                }
                else if (this.dgvDataTable.Columns[i].Name.Equals("Overwrite"))
                {
                    btnOverwriteIndex = i;
                }
            }

            if (e.RowIndex != -1 && e.ColumnIndex != -1 && e.ColumnIndex != btnOverwriteIndex && e.ColumnIndex != btnSaveIndex)
            {
                if ((bool)this.dgvDataTable.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
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
        /// Occurs when you need to format the contents of a cell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDataTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (this.dgvDataTable.Columns[e.ColumnIndex].Name.Equals("Select"))
            {
                if (e.Value == (null))
                {
                    e.Value = false;
                }
            }

        }

        /// <summary>
        /// Do not select all rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNotSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvDataTable.Rows.Count; i++)
            {
                this.dgvDataTable.Rows[i].Cells[0].Value = false;
            }
        }

        /// <summary>
        /// Select all rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvDataTable.Rows.Count; i++)
            {
                this.dgvDataTable.Rows[i].Cells[0].Value = true;
            }
        }

        /// <summary>
        /// Saving Data in Batches
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBatchsSaving_Click(object sender, EventArgs e)
        {
            string saveSql = "EXEC sp_AddStudentAdmissionInfoDetial  @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            DbHelper dbHelper = new DbHelper(this._connStr);
            dbHelper.OpenDbConnection();
            SqlCommand sqlCommand = new SqlCommand(saveSql, dbHelper.SqlCon);
            SqlTransaction sqlTransaction = dbHelper.SqlCon.BeginTransaction();
            bool result = false;


            //Check that rows are selected
            if (this.CheckIsSelectedRow())
            {
                try

                {
                    for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
                    {
                        if ((bool)this.dgvDataTable.Rows[i].Cells[0].Value == false)
                        {

                            sqlParametersList.Add(new SqlParameter("@Name", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i]["Name"]));
                            sqlParametersList.Add(new SqlParameter("@Age", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i]["Age"]));
                            sqlParametersList.Add(new SqlParameter("@Sex", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i]["Sex"]));
                            sqlParametersList.Add(new SqlParameter("@Email", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i]["Email"]));
                            //Save Data
                            result = this.SaveModifyDeleteData(saveSql, sqlParametersList, sqlTransaction, sqlCommand);

                            if (result == false)
                            {
                                //Data rollback
                                sqlTransaction.Rollback();
                                break;
                            }

                            sqlCommand.Parameters.Clear();
                            sqlParametersList.Clear();
                        }
                    }

                    if (result)
                    {
                        //Data Commit
                        sqlTransaction.Commit();

                        for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
                        {
                            if (dgvDataTable.Rows[i].Cells[0].Value.Equals(true))
                            {
                                //Example Delete the saved data
                                dgvDataTable.Rows.Remove(dgvDataTable.Rows[i]);
                                i--;
                            }
                        }

                        this._log.Append(DateTime.Now.ToString("HH:mm:ss") + "" + "save data successfully" + "\r\n");
                        this.txtLog.Text = this._log.ToString();

                        if (this.dgvDataTable.Rows.Count == 0)
                        {
                            this.btnSaveFile.Enabled = true;
                        }
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    dbHelper.CloseDbConnection();
                }
            }
            else
            {
                MessageBox.Show("No rows are selected");
            }
        }

        /// <summary>
        /// Saving Data in Batches
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBatchOverwrite_Click(object sender, EventArgs e)
        {
            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            string overwriteSql = "EXEC sp_OverwriteStudentAdmissionInfoSnumber @Name = @Name, @Age = @Age, @Sex = @Sex, @Email= @Email";
            DbHelper dbHelper = new DbHelper(this._connStr);
            dbHelper.OpenDbConnection();
            SqlCommand sqlCommand = new SqlCommand(overwriteSql, dbHelper.SqlCon);
            SqlTransaction sqlTransaction = dbHelper.SqlCon.BeginTransaction();
            bool result = false;

            if (this.CheckIsSelectedRow())
            {
                try

                {
                    for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
                    {


                        if ((bool)this.dgvDataTable.Rows[i].Cells[0].Value == false)
                        {
                            sqlParametersList.Add(new SqlParameter("@Name", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i]["Name"]));
                            sqlParametersList.Add(new SqlParameter("@Age", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i]["Age"]));
                            sqlParametersList.Add(new SqlParameter("@Sex", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i]["Sex"]));
                            sqlParametersList.Add(new SqlParameter("@Email", this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i]["Email"]));
                            //Overwrite Data
                            result = this.SaveModifyDeleteData(overwriteSql, sqlParametersList, sqlTransaction, sqlCommand);

                            if (result == false)
                            {
                                //Data rollback
                                sqlTransaction.Rollback();
                                break;
                            }

                            sqlCommand.Parameters.Clear();
                            sqlParametersList.Clear();
                        }
                    }

                    if (result)
                    {  //Data commit
                        sqlTransaction.Commit();
                        for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
                        {
                            if (dgvDataTable.Rows[i].Cells[0].Value.Equals(true))
                            {
                                dgvDataTable.Rows.Remove(dgvDataTable.Rows[i]);
                                i--;
                            }
                        }

                        this._log.Append(DateTime.Now.ToString("HH:mm:ss") + "" + "Overwrite data successfully" + "\r\n");
                        this.txtLog.Text = this._log.ToString();

                        if (this.dgvDataTable.Rows.Count == 0)
                        {
                            this.btnSaveFile.Enabled = true;
                        }
                    }

                }
                catch
                {
                    throw;
                }
                finally
                {
                    dbHelper.CloseDbConnection();
                }
            }
            else
            {
                MessageBox.Show("No rows are selected");
            }
        }


        #region private method
        /// <summary>
        /// Change the visible state  of the input box
        /// </summary>
        /// <param name="visibleState"></param>
        private void UpdateInputVisibleState(bool visibleState)
        {

            this.txtDuplicateDbContent.Visible = visibleState;
            this.txtCharacterLengthError.Visible = visibleState;
            this.txtDuplicateContent.Visible = visibleState;
            this.txtErrorContent.Visible = visibleState;
            this.txtFiledTypeError.Visible = visibleState;
            this.txtEmptyContent.Visible = visibleState;
        }

        /// <summary>
        /// Change the visible state  of the button
        /// </summary>
        /// <param name="visibleState"></param>
        private void UpdateButtonVisibleState(bool visibleState)
        {
            this.btnBatchOverwrite.Visible = visibleState;
            this.btnBatchsSaving.Visible = visibleState;
            this.btnNotSelectAll.Visible = visibleState;
            this.btnSelectAll.Visible = visibleState;

        }

        /// <summary>
        /// Check that the row is selected
        /// </summary>
        /// <returns></returns>
        private bool CheckIsSelectedRow()
        {

            for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
            {
                if (this.dgvDataTable.Rows[i].Cells[0].Value != null)
                {
                    if (this.dgvDataTable.Rows[i].Cells[0].Value.Equals(true))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParameterList"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="sqlCommand"></param>
        /// <returns></returns>
        private bool SaveModifyDeleteData(string sql, List<SqlParameter> sqlParameterList, SqlTransaction sqlTransaction, SqlCommand sqlCommand)
        {
            sqlCommand.Transaction = sqlTransaction;
            sqlCommand.Parameters.AddRange(sqlParameterList.ToArray());
            try
            {
                return sqlCommand.ExecuteNonQuery() > 0;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Display cells and logs that do not conform to the rules
        /// </summary>
        private void displayErrorCellAndLog()
        {
            ErrorCellInformation errorCellInformation = new ErrorCellInformation();

            //Change the cell background color to a different color by enumerating different values
            for (int i = 0; i < this._cSVFileHelper.ErrorCellInformationList.Count; i++)
            {
                switch (this._cSVFileHelper.ErrorCellInformationList[i].ErrorType)
                {
                    case ErrorCellInformation._errorTypeEnum.emptyContent:
                        this.dgvDataTable.Rows[this._cSVFileHelper.ErrorCellInformationList[i].ErrorRow].Cells[this._cSVFileHelper.ErrorCellInformationList[i].ErrorColumn].Style.BackColor = Color.Red;
                        break;
                    case ErrorCellInformation._errorTypeEnum.duplicateContent:
                        this.dgvDataTable.Rows[this._cSVFileHelper.ErrorCellInformationList[i].ErrorRow].Cells[this._cSVFileHelper.ErrorCellInformationList[i].ErrorColumn].Style.BackColor = Color.Gray;
                        break;
                    case ErrorCellInformation._errorTypeEnum.duplicateDbContent:
                        this.dgvDataTable.Rows[this._cSVFileHelper.ErrorCellInformationList[i].ErrorRow].Cells[this._cSVFileHelper.ErrorCellInformationList[i].ErrorColumn].Style.BackColor = Color.LightYellow;
                        break;
                    case ErrorCellInformation._errorTypeEnum.filedTypeError:
                        this.dgvDataTable.Rows[this._cSVFileHelper.ErrorCellInformationList[i].ErrorRow].Cells[this._cSVFileHelper.ErrorCellInformationList[i].ErrorColumn].Style.BackColor = Color.Orange;
                        break;
                    case ErrorCellInformation._errorTypeEnum.characterLengthError:
                        this.dgvDataTable.Rows[this._cSVFileHelper.ErrorCellInformationList[i].ErrorRow].Cells[this._cSVFileHelper.ErrorCellInformationList[i].ErrorColumn].Style.BackColor = Color.LightPink;
                        break;
                    case ErrorCellInformation._errorTypeEnum.contentError:
                        this.dgvDataTable.Rows[this._cSVFileHelper.ErrorCellInformationList[i].ErrorRow].Cells[this._cSVFileHelper.ErrorCellInformationList[i].ErrorColumn].Style.BackColor = Color.LightSteelBlue;
                        break;
                }
                //Displays a specific error message for a specific cell
                this._log.Append(this._cSVFileHelper.ErrorCellInformationList[i].ErrorMessage.ToString() + "\r\n");
                this.txtLog.Text = this._log.ToString();
            }

            //this.dgvDataTable.FirstDisplayedScrollingRowIndex = this._cSVFileHelper.ErrorCellInformationList[0].ErrorRow;
            //Output the specific cell information of the specific row that does not conform to the rule
            this.txtLog.Text = this._log.ToString();
            this._cSVFileHelper.DestroyErrorCellInformation();
        }
    }

    #endregion
}
