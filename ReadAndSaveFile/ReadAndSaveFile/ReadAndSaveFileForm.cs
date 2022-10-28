using DbBase;
using ReadAndSaveCSVFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
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
            string findDuplicateDataSql = "Exec sp_CheckStudentAdmissionInfoEmailIsExists @Email = @Email";
            string findPossibleDuplicateDataSql = "Exec sp_CheckStudentAdmissionInforDetailIsMatch @Name = @Name, @Age = @Age, @Sex = @Sex";
            string updateDataSql = "EXEC sp_UpdateStudentAdmissionInfoByEmail @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
            string saveSql = "EXEC sp_AddStudentAdmissionInfoDetial  @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
            DbHelper dbhelper = new DbHelper(this._connStr);
            int nameMaxLength = 64;
            int emailMaxLength = 64;
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
                            this._cSVFileHelper.SiftedFileContents(findDuplicateDataSql, findPossibleDuplicateDataSql, this._connStr);
                            if (this._cSVFileHelper.PossibleDuplicateDataTable.Rows.Count > 0)
                            {
                                //
                                MessageBox.Show("If suspected duplicate data is found, please deal with suspected duplicate data!");
                                this.dgvDataTable.DataSource = this._cSVFileHelper.PossibleDuplicateDataTable.Copy();
                                this.dgvDataTable.Columns["State"].Visible = false;
                                this.UpdateInputVisibleState(false);
                                this.dgvDataTable.Columns.Add(btnSave);
                                this.dgvDataTable.Columns.Add(btnOverwrite);

                                //Add checkbox and buttons
                                if (this.dgvDataTable.Rows.Count > 1)
                                {
                                    //Insert the checkbox in the first column
                                    this.dgvDataTable.Columns.Insert(0, chkSelect);
                                }

                                for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
                                {
                                    this.dgvDataTable.Rows[i].Cells[0].Value = false;
                                }

                                this.UpdateButtonVisibleState(true);
                                this.btnSaveFile.Enabled=false;

                            }

                            if (this.dgvDataTable.Rows.Count == 0)
                            {
                                if (this._cSVFileHelper.DuplicateDataTable.Rows.Count > 0)
                                {
                                    DialogResult dialogResult = MessageBox.Show("The data in the file and the data in the database are duplicated.Do you override duplicate data?", "File content duplication prompts", MessageBoxButtons.YesNo);
                                    //Duplicate data is overwritten if it exists
                                    if (dialogResult.Equals(DialogResult.Yes))
                                    {
                                        //Insert non-duplicate data and update duplicate data
                                        if (this.UpdateDupicateAndSaveNotDuplicateData(updateDataSql, saveSql, this._cSVFileHelper.DuplicateDataTable, this._cSVFileHelper.NotDuplicateDataTable))
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
                                        if (this._cSVFileHelper.NotDuplicateDataTable != null)
                                        {
                                            //save file content data to database
                                            if (this.SaveUpdateDeleteBulkData(saveSql, this._cSVFileHelper.NotDuplicateDataTable, true))
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
                                        else
                                        {
                                            MessageBox.Show("There is no data that does not duplicate the database content");
                                        }

                                    }
                                }
                                //Holds the contents of data that is not duplicated
                                else
                                {
                                    if (this._cSVFileHelper.NotDuplicateDataTable != null)
                                    {

                                        try
                                        {
                                            if (this.SaveUpdateDeleteBulkData(saveSql, this._cSVFileHelper.NotDuplicateDataTable, true))
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
                                        catch
                                        {
                                            throw;
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("There is no data that does not duplicate the database content");
                                    }

                                }
                            }
                            //else
                            //{
                            //    this.displayErrorCellAndLog();
                            //}
                        }
                        else
                        {
                            MessageBox.Show("Please deal with duplicate data in the file");
                            this.btnSaveFile.Enabled = false;
                            this.displayErrorCellAndLog();
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
            int selectRowIndex = this.dgvDataTable.SelectedCells[0].RowIndex;
            //Overwrite Possible duplicate data
            if (this.dgvDataTable.Columns[e.ColumnIndex].Name.Equals("Overwrite"))
            {
                this._cSVFileHelper.PossibleDuplicateDataTable.Rows[selectRowIndex]["State"] = CSVFileHelper._state.overwrite;
                this.dgvDataTable.Rows.RemoveAt(selectRowIndex);
            }
            //Save as new data
            else if (this.dgvDataTable.Columns[e.ColumnIndex].Name.Equals("Save"))
            {
                this._cSVFileHelper.CsvContentDataTable.Rows[selectRowIndex]["State"] = CSVFileHelper._state.notDuplicate;
                this.dgvDataTable.Rows.RemoveAt(selectRowIndex);
            }

            if (this.dgvDataTable.Rows.Count == 0)
            {
                this.btnSaveFile.Enabled = true;
            }

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
            //Check that rows are selected
            if (this.CheckIsSelectedRow())
            {
                for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
                {
                    if ((bool)this.dgvDataTable.Rows[i].Cells[0].Value == true)
                    {
                        this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i]["state"] = CSVFileHelper._state.notDuplicate;
                        this.dgvDataTable.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (this.dgvDataTable.Rows.Count == 0)
            {
                this.btnSaveFile.Enabled = true;
            }
        }

        /// <summary>
        /// Saving Data in Batches
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBatchOverwrite_Click(object sender, EventArgs e)
        {
            //Check that rows are selected
            if (this.CheckIsSelectedRow())
            {
                for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
                {
                    if ((bool)this.dgvDataTable.Rows[i].Cells[0].Value == true)
                    {
                        this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i]["state"] = CSVFileHelper._state.overwrite;
                        this.dgvDataTable.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (this.dgvDataTable.Rows.Count == 0)
            {
                this.btnSaveFile.Enabled = true;
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
        /// <returns>If a row is selected ,it returns true; otherwise, it returns fasle</returns>
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
        /// Display cells and logs that do not conform to the rules
        /// </summary>
        private void displayErrorCellAndLog()
        {
            ErrorCellInformation errorCellInformation = new ErrorCellInformation();
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
            this.txtLog.Text = this._log.ToString();
            this._cSVFileHelper.DestroyErrorCellInformation();
        }

        ///
        private bool SaveUpdateDeleteBulkData(string sql, DataTable dataTable, bool isStartTransaction)
        {

            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            DbHelper dbHelper = new DbHelper(this._connStr);
            dbHelper.OpenDbConnection();
            SqlCommand sqlCommand = null;
            SqlTransaction sqlTransaction = null;
            bool result = false;

            if (dataTable.Rows.Count == 0)
            {
                return false;
            }
            //If you start a transaction
            if (isStartTransaction)
            {
                sqlCommand = new SqlCommand(sql, dbHelper.SqlCon);
                sqlTransaction = dbHelper.SqlCon.BeginTransaction();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        sqlParametersList.Add(new SqlParameter("@Name", dataTable.Rows[i]["Name"]));
                        sqlParametersList.Add(new SqlParameter("@Age", dataTable.Rows[i]["Age"]));
                        sqlParametersList.Add(new SqlParameter("@Sex", dataTable.Rows[i]["Sex"]));
                        sqlParametersList.Add(new SqlParameter("@Email", dataTable.Rows[i]["Email"]));
                        //Save Data
                        result = dbHelper.SaveModifyDeleteData(sql, sqlParametersList, sqlTransaction, sqlCommand);

                        if (result == false)
                        {
                            //Data rollback
                            sqlTransaction.Rollback();
                            return false;
                        }

                        sqlParametersList.Clear();
                    }
                    //Data Commit
                    sqlTransaction.Commit();
                }
                catch (Exception)
                {
                    throw;
                }
                return true;
            }
            else
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    sqlParametersList.Add(new SqlParameter("@Name", dataTable.Rows[i]["Name"]));
                    sqlParametersList.Add(new SqlParameter("@Age", dataTable.Rows[i]["Age"]));
                    sqlParametersList.Add(new SqlParameter("@Sex", dataTable.Rows[i]["Sex"]));
                    sqlParametersList.Add(new SqlParameter("@Email", dataTable.Rows[i]["Email"]));
                    //Save Data
                    result = dbHelper.SaveModifyDeleteData(sql, sqlParametersList);

                    if (result == false)
                    {
                        return false;
                    }

                    sqlParametersList.Clear();
                }

                return true;
            }
        }

        private bool UpdateDupicateAndSaveNotDuplicateData(string updateSql, string saveSql, DataTable dupicateDataTable, DataTable notDuplicateDataTable)
        {
            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            DbHelper dbHelper = new DbHelper(this._connStr);
            dbHelper.OpenDbConnection();
            SqlCommand sqlCommand = new SqlCommand(updateSql, dbHelper.SqlCon);
            SqlTransaction sqlTransaction = dbHelper.SqlCon.BeginTransaction();
            sqlCommand.Transaction = sqlTransaction;
            bool result = false;

            try
            {
                for (int i = 0; i < dupicateDataTable.Rows.Count; i++)
                {
                    sqlParametersList.Add(new SqlParameter("@Name", dupicateDataTable.Rows[i]["Name"]));
                    sqlParametersList.Add(new SqlParameter("@Age", dupicateDataTable.Rows[i]["Age"]));
                    sqlParametersList.Add(new SqlParameter("@Sex", dupicateDataTable.Rows[i]["Sex"]));
                    sqlParametersList.Add(new SqlParameter("@Email", dupicateDataTable.Rows[i]["Email"]));
                    //Save Data
                    result = dbHelper.SaveModifyDeleteData(updateSql, sqlParametersList, sqlTransaction, sqlCommand);

                    if (result == false)
                    {
                        //Data rollback
                        sqlTransaction.Rollback();
                        return false;
                    }

                    sqlParametersList.Clear();
                }
                if (notDuplicateDataTable != null)
                {
                    for (int i = 0; i < notDuplicateDataTable.Rows.Count; i++)
                    {
                        sqlParametersList.Add(new SqlParameter("@Name", notDuplicateDataTable.Rows[i]["Name"]));
                        sqlParametersList.Add(new SqlParameter("@Age", notDuplicateDataTable.Rows[i]["Age"]));
                        sqlParametersList.Add(new SqlParameter("@Sex", notDuplicateDataTable.Rows[i]["Sex"]));
                        sqlParametersList.Add(new SqlParameter("@Email", notDuplicateDataTable.Rows[i]["Email"]));
                        //Save Data
                        result = dbHelper.SaveModifyDeleteData(saveSql, sqlParametersList, sqlTransaction, sqlCommand);

                        if (result == false)
                        {
                            //Data rollback
                            sqlTransaction.Rollback();
                            return false;
                        }

                        sqlParametersList.Clear();
                    }

                }
                sqlTransaction.Commit();
            }
            catch
            {
                throw;
            }
            finally
            {
                dbHelper.CloseDbConnection();
            }

            return true;
        }
        #endregion
    }
}
