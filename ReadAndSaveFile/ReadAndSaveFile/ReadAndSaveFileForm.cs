using DbBase;
using ReadAndSaveCSVFile;
using System;
using System.Collections.Generic;
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
            this.btnNext.Visible = false;
        }

        /// <summary>
        /// Reads the file and displays its contents
        /// </summary>
        /// <param name="sender">The btnReadFile_Click object itself</param>
        /// <param name="e">Record additional information for clicking btnReadFile</param>
        private void btnReadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            this.dgvDataTable.Columns.Clear();
            this.UpdateButtonVisibleState(false);
            this.btnNext.Visible = false;

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
            string findPossibleDuplicateDataSql = "Exec sp_CheckStudentAdmissionInfoDetailIsMatch @Name = @Name, @Age = @Age, @Sex = @Sex";
            string updateDataSql = "EXEC sp_UpdateStudentAdmissionInfoByEmail @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
            string saveSql = "EXEC sp_AddStudentAdmissionInfoDetial  @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
            string overwriteSql = "EXEC  sp_OverwriteStudentAdmissionInfoEmail @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
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
                                MessageBox.Show("If suspected duplicate data is found, please deal with suspected duplicate data!");
                                this.dgvDataTable.DataSource = this._cSVFileHelper.PossibleDuplicateDataTable.Copy();
                                this.UpdateInputVisibleState(false);
                                this.dgvDataTable.Columns.Add(btnSave);
                                this.dgvDataTable.Columns.Add(btnOverwrite);
                                this.txtLog.Text = string.Empty;

                                //Add checkbox and buttons
                                if (this.dgvDataTable.Rows.Count > 0)
                                {
                                    //Insert the checkbox in the first column
                                    this.dgvDataTable.Columns.Insert(0, chkSelect);
                                }

                                for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
                                {
                                    this.dgvDataTable.Rows[i].Cells[0].Value = false;
                                }

                                this.UpdateButtonVisibleState(true);
                                this.btnSaveFile.Enabled = false;
                                this.btnNext.Visible=true;
                                this.btnNext.Enabled=false;
                            }
                            else
                            {
                                if (this._cSVFileHelper.DuplicateDataTable.Rows.Count > 0)
                                {
                                    DialogResult dialogResult = MessageBox.Show("The data in the file and the data in the database are duplicated.Do you override duplicate data?", "File content duplication prompts", MessageBoxButtons.YesNo);
                                    //Duplicate data is overwritten if it exists
                                    if (dialogResult.Equals(DialogResult.Yes))
                                    {
                                        //Insert non-duplicate data and update duplicate data
                                        if (this.UpdateDupicateAndSaveNotDuplicateData(saveSql, overwriteSql, updateDataSql))
                                        {
                                            this._filePath = string.Empty;
                                            this._cSVFileHelper.DestroyCSVContentDataTable();        
                                            this.btnSaveFile.Enabled = false;
                                            this.btnRefresh.Enabled = false;
                                            this.txtLog.Text = string.Empty;
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
                                            if (this.UpdateDupicateAndSaveNotDuplicateData(saveSql, overwriteSql))
                                            {
                                                this._filePath = string.Empty;
                                                this._cSVFileHelper.DestroyCSVContentDataTable();
                                                this.btnSaveFile.Enabled = false;
                                                this.btnRefresh.Enabled = false;
                                                this.txtLog.Text = string.Empty;
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
                                //Save the contents of data that is not duplicated
                                else
                                {
                                    if (this._cSVFileHelper.NotDuplicateDataTable != null)
                                    {
                                        try
                                        {
                                            if (this.UpdateDupicateAndSaveNotDuplicateData(saveSql, overwriteSql))
                                            {
                                                this._filePath = string.Empty;
                                                this._cSVFileHelper.DestroyCSVContentDataTable();
                                                this.btnSaveFile.Enabled = false;
                                                this.btnRefresh.Enabled = false;
                                                this.txtLog.Text = string.Empty;
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
                        }
                        else
                        {
                            MessageBox.Show("Please deal with duplicate data in the file");
                            this.btnSaveFile.Enabled = false;
                            this.UpdateInputVisibleState(true);
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
            this.dgvDataTable.DataSource=null;
            this.dgvDataTable.Columns.Clear();
            this.btnSaveFile.Enabled = true;
            this.txtLog.Text = string.Empty; 
            this.btnNext.Visible = false;
            this.UpdateButtonVisibleState(false);

            if (this._filePath != string.Empty)
            {
                try
                {
                    //Get file content
                    this._cSVFileHelper.GetFileDataToDataTable(this._filePath);
                    //Display csv file datatable
                    this.dgvDataTable.DataSource = this._cSVFileHelper.CsvContentDataTable;
                    MessageBox.Show("Refresh the success");
                }
                catch (ReadAndSaveFileException ex)
                {
                    MessageBox.Show(ex.ExceptionMessage.ToString());
                }
                catch
                {
                    MessageBox.Show("Refresh the fail");
                }
            }
        }



        /// <summary>
        /// Cell click event
        /// </summary>
        /// <param name="sender">The dgvDataTable_CellContentClick object itself</param>
        /// <param name="e">Records additional information when the contents of a cell are clicked</param>
        private void dgvDataTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectRowIndex = this.dgvDataTable.SelectedCells[0].RowIndex;
            //Overwrite Possible duplicate data
            if (this.dgvDataTable.Columns[e.ColumnIndex].Name.Equals("Overwrite"))
            {
                this._cSVFileHelper.OverwriteDataTable.Rows.Add(this._cSVFileHelper.PossibleDuplicateDataTable.Rows[selectRowIndex].ItemArray);
                this._cSVFileHelper.PossibleDuplicateDataTable.Rows.RemoveAt(selectRowIndex);
                this.dgvDataTable.Rows.RemoveAt(selectRowIndex);
            }
            //Save as new data
            else if (this.dgvDataTable.Columns[e.ColumnIndex].Name.Equals("Save"))
            {
                this._cSVFileHelper.NotDuplicateDataTable.Rows.Add(this._cSVFileHelper.PossibleDuplicateDataTable.Rows[selectRowIndex].ItemArray);
                this._cSVFileHelper.PossibleDuplicateDataTable.Rows.RemoveAt(selectRowIndex);
                this.dgvDataTable.Rows.RemoveAt(selectRowIndex);
            }

            if (this.dgvDataTable.Rows.Count == 0)
            {
                this.btnNext.Enabled = true;
                this.dgvDataTable.Columns.Clear();
                this.UpdateButtonVisibleState(false);
            }
        }

        /// <summary>
        /// When there is potentially duplicate data is the enable button
        /// Save file content data 
        /// </summary>
        /// <param name="sender">The btnNext_Click object itself</param>
        /// <param name="e">Record additional information for clicking btnNext</param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            string overwriteSql = "EXEC  sp_OverwriteStudentAdmissionInfoEmail @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
            string updateDataSql = "EXEC sp_UpdateStudentAdmissionInfoByEmail @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
            string saveSql = "EXEC sp_AddStudentAdmissionInfoDetial  @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";

            if (this._cSVFileHelper.DuplicateDataTable.Rows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("The data in the file and the data in the database are duplicated.Do you override duplicate data?", "File content duplication prompts", MessageBoxButtons.YesNo);
                //Duplicate data is overwritten if it exists
                if (dialogResult.Equals(DialogResult.Yes))
                {
                    //Insert non-duplicate data and update duplicate data
                    if (this.UpdateDupicateAndSaveNotDuplicateData(saveSql, overwriteSql, updateDataSql))
                    {
                        this._filePath = string.Empty;
                        this._cSVFileHelper.DestroyCSVContentDataTable();
                        this.btnNext.Visible = false;
                        this.btnSaveFile.Enabled = false;
                        this.btnRefresh.Enabled = false;
                        this.txtLog.Text = string.Empty;
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
                        if (this.UpdateDupicateAndSaveNotDuplicateData(saveSql, overwriteSql))
                        {
                            this._filePath = string.Empty;
                            this._cSVFileHelper.DestroyCSVContentDataTable();
                            this.btnNext.Visible = false;
                            this.btnSaveFile.Enabled = false;
                            this.btnRefresh.Enabled = false;
                            this.txtLog.Text = string.Empty;
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
            //Save the contents of data that is not duplicated
            else
            {
                if (this._cSVFileHelper.NotDuplicateDataTable != null)
                {
                    try
                    {
                        if (this.UpdateDupicateAndSaveNotDuplicateData(saveSql, overwriteSql))
                        {
                            this._filePath = string.Empty;
                            this._cSVFileHelper.DestroyCSVContentDataTable();
                            this.btnNext.Visible = false;
                            this.btnSaveFile.Enabled = false;
                            this.btnRefresh.Enabled = false;
                            this.txtLog.Text = string.Empty;
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

        /// <summary>
        /// Mouse click events in dgvDataTable
        /// </summary>
        /// <param name="sender">The dgvDataTable_CellMouseClick object itself</param>
        /// <param name="e">Record additional information for the clicked cell</param>
        private void dgvDataTable_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int btnSaveIndex = 0;
            int btnOverwriteIndex = 0;
            int checkboxIndex = 0;


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
                else if (this.dgvDataTable.Columns[i].Name.Equals("Select"))
                {
                    checkboxIndex = i;
                }
            }

            if ((e.ColumnIndex == btnOverwriteIndex || e.ColumnIndex==btnSaveIndex || e.ColumnIndex==checkboxIndex) && e.ColumnIndex != -1 && e.RowIndex != -1)
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
        /// <param name="sender">The dgvDataTable_CellFormatting object itself</param>
        /// <param name="e">Record additional information that changes cell content</param>
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
        /// <param name="sender">The btnNotSelectAll_Click object itself</param>
        /// <param name="e">Record additional information for clicking btnNotSelect</param>
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
        /// <param name="sender">The btnSelectAll_Click object itself</param>
        /// <param name="e">Record additional information for clicking btnSelectAll</param>
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
        /// <param name="sender">The btnBatchsSaving_Click object itself</param>
        /// <param name="e">Record additional information for clicking btnBatchsSaving</param>
        private void btnBatchsSaving_Click(object sender, EventArgs e)
        {
            //Check that rows are selected
            if (this.CheckIsSelectedRow())
            {
                for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
                {
                    if ((bool)this.dgvDataTable.Rows[i].Cells[0].Value == true)
                    {
                        this._cSVFileHelper.NotDuplicateDataTable.Rows.Add(this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i].ItemArray);
                        this._cSVFileHelper.PossibleDuplicateDataTable.Rows.RemoveAt(i);
                        this.dgvDataTable.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select the row to save");
            }
            if (this.dgvDataTable.Rows.Count == 0)
            {
                this.btnNext.Enabled = true;
                this.dgvDataTable.Columns.Clear();
                this.UpdateButtonVisibleState(false);
            }
        }

        /// <summary>
        /// Saving Data in Batches
        /// </summary>
        /// <param name="sender">The btnBatchOverwrite_Click object itself</param>
        /// <param name="e">Record additional information for clicking btnBatchOverwrite</param>
        private void btnBatchOverwrite_Click(object sender, EventArgs e)
        {
            //Check that rows are selected
            if (this.CheckIsSelectedRow())
            {
                for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
                {
                    if ((bool)this.dgvDataTable.Rows[i].Cells[0].Value == true)
                    {
                        this._cSVFileHelper.OverwriteDataTable.Rows.Add(this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i].ItemArray);
                        this._cSVFileHelper.PossibleDuplicateDataTable.Rows.RemoveAt(i);
                        this.dgvDataTable.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select the row to overwrite");
            }

            if (this.dgvDataTable.Rows.Count == 0)
            {
                this.btnNext.Enabled = true;
                this.dgvDataTable.Columns.Clear();
                this.UpdateButtonVisibleState(false);
            }
        }

        #region private method
        /// <summary>
        /// Change the visible state  of the input box
        /// </summary>
        /// <param name="visibleState">The state of being visible or not</param>
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
        /// <param name="visibleState">The state of being visible or not</param>
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
            StringBuilder stringBuider = new StringBuilder();
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
                stringBuider.Append(this._cSVFileHelper.ErrorCellInformationList[i].ErrorMessage.ToString() + "\r\n");
                this.txtLog.Text =stringBuider.ToString();
                this.dgvDataTable.FirstDisplayedScrollingRowIndex=this._cSVFileHelper.ErrorCellInformationList[0].ErrorRow;
            }

            this._cSVFileHelper.DestroyErrorCellInformation();
        }

        /// <summary>
        /// Update dupicate data and not duplicate data
        /// </summary>
        /// <param name="saveSql">save commandtext</param>
        /// <param name="overwriteSql">Overwrite commandtext</param>
        /// <returns>Returns true on success and false on failure</returns>
        private bool UpdateDupicateAndSaveNotDuplicateData(string saveSql, string overwriteSql)
        {
            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            DbHelper dbHelper = new DbHelper(this._connStr);
            dbHelper.OpenDbConnection();
            SqlCommand sqlCommand = new SqlCommand(saveSql, dbHelper.SqlCon);
            SqlTransaction sqlTransaction = dbHelper.SqlCon.BeginTransaction();
            sqlCommand.Transaction = sqlTransaction;
            bool result = false;

            try
            {
                if (this._cSVFileHelper.NotDuplicateDataTable.Rows.Count>0)
                {
                    for (int i = 0; i < this._cSVFileHelper.NotDuplicateDataTable.Rows.Count; i++)
                    {
                        sqlParametersList.Add(new SqlParameter("@Name", this._cSVFileHelper.NotDuplicateDataTable.Rows[i]["Name"]));
                        sqlParametersList.Add(new SqlParameter("@Age", this._cSVFileHelper.NotDuplicateDataTable.Rows[i]["Age"]));
                        sqlParametersList.Add(new SqlParameter("@Sex", this._cSVFileHelper.NotDuplicateDataTable.Rows[i]["Sex"]));
                        sqlParametersList.Add(new SqlParameter("@Email", this._cSVFileHelper.NotDuplicateDataTable.Rows[i]["Email"]));
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

                if (this._cSVFileHelper.OverwriteDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < this._cSVFileHelper.OverwriteDataTable.Rows.Count; i++)
                    {
                        sqlParametersList.Add(new SqlParameter("@Name", this._cSVFileHelper.OverwriteDataTable.Rows[i]["Name"]));
                        sqlParametersList.Add(new SqlParameter("@Age", this._cSVFileHelper.OverwriteDataTable.Rows[i]["Age"]));
                        sqlParametersList.Add(new SqlParameter("@Sex", this._cSVFileHelper.OverwriteDataTable.Rows[i]["Sex"]));
                        sqlParametersList.Add(new SqlParameter("@Email", this._cSVFileHelper.OverwriteDataTable.Rows[i]["Email"]));
                        //Save Data
                        result = dbHelper.SaveModifyDeleteData(overwriteSql, sqlParametersList, sqlTransaction, sqlCommand);

                        if (result == false)
                        {
                            //Data rollback
                            sqlTransaction.Rollback();
                            return false;
                        }

                        sqlParametersList.Clear();
                    }
                }
                //Transaction commit
                sqlTransaction.Commit();
            }
            catch
            {
                throw;
            }
            finally
            {
                //Closing the database Connection
                dbHelper.CloseDbConnection();
            }

            return true;
        }

        /// <summary>
        /// Update dupicate data and not duplicate data
        /// </summary>
        /// <param name="saveSql">Save data commandtext</param>
        /// <param name="overwriteSql">Overwrite data commandtext</param>
        /// <param name="updateSql">Update date commandtext</param>
        /// <returns>Returns true on success and false on failure</returns>
        private bool UpdateDupicateAndSaveNotDuplicateData(string saveSql, string overwriteSql, string updateSql)
        {
            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            DbHelper dbHelper = new DbHelper(this._connStr);
            dbHelper.OpenDbConnection();
            SqlCommand sqlCommand = new SqlCommand(saveSql, dbHelper.SqlCon);
            SqlTransaction sqlTransaction = dbHelper.SqlCon.BeginTransaction();
            sqlCommand.Transaction = sqlTransaction;
            bool result = false;

            try
            {
                if (this._cSVFileHelper.NotDuplicateDataTable.Rows.Count>0)
                {
                    for (int i = 0; i < this._cSVFileHelper.NotDuplicateDataTable.Rows.Count; i++)
                    {
                        sqlParametersList.Add(new SqlParameter("@Name", this._cSVFileHelper.NotDuplicateDataTable.Rows[i]["Name"]));
                        sqlParametersList.Add(new SqlParameter("@Age", this._cSVFileHelper.NotDuplicateDataTable.Rows[i]["Age"]));
                        sqlParametersList.Add(new SqlParameter("@Sex", this._cSVFileHelper.NotDuplicateDataTable.Rows[i]["Sex"]));
                        sqlParametersList.Add(new SqlParameter("@Email", this._cSVFileHelper.NotDuplicateDataTable.Rows[i]["Email"]));
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

                if (this._cSVFileHelper.OverwriteDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < this._cSVFileHelper.OverwriteDataTable.Rows.Count; i++)
                    {
                        sqlParametersList.Add(new SqlParameter("@Name", this._cSVFileHelper.OverwriteDataTable.Rows[i]["Name"]));
                        sqlParametersList.Add(new SqlParameter("@Age", this._cSVFileHelper.OverwriteDataTable.Rows[i]["Age"]));
                        sqlParametersList.Add(new SqlParameter("@Sex", this._cSVFileHelper.OverwriteDataTable.Rows[i]["Sex"]));
                        sqlParametersList.Add(new SqlParameter("@Email", this._cSVFileHelper.OverwriteDataTable.Rows[i]["Email"]));
                        //Save Data
                        result = dbHelper.SaveModifyDeleteData(overwriteSql, sqlParametersList, sqlTransaction, sqlCommand);

                        if (result == false)
                        {
                            //Data rollback
                            sqlTransaction.Rollback();
                            return false;
                        }

                        sqlParametersList.Clear();
                    }
                }

                for (int i = 0; i < this._cSVFileHelper.DuplicateDataTable.Rows.Count; i++)
                {
                    sqlParametersList.Add(new SqlParameter("@Name", this._cSVFileHelper.DuplicateDataTable.Rows[i]["Name"]));
                    sqlParametersList.Add(new SqlParameter("@Age", this._cSVFileHelper.DuplicateDataTable.Rows[i]["Age"]));
                    sqlParametersList.Add(new SqlParameter("@Sex", this._cSVFileHelper.DuplicateDataTable.Rows[i]["Sex"]));
                    sqlParametersList.Add(new SqlParameter("@Email", this._cSVFileHelper.DuplicateDataTable.Rows[i]["Email"]));
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
                //Transaction commit
                sqlTransaction.Commit();
            }
            catch
            {
                throw;
            }
            finally
            {
                //Closing the database Connection
                dbHelper.CloseDbConnection();
            }

            return true;
        }
        #endregion
    }
}
