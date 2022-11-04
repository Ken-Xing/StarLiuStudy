using DbBase;
using ReadAndSaveCSVFile;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private bool _isCheckPass = true;
        private string lstvlog = string.Empty;
        #endregion

        public ReadAndSaveFileForm()
        {
            InitializeComponent();
        }

        private void ReadAndSaveFileForm_Load(object sender, EventArgs e)
        {
            //Hide label
            HideAllLabel();
            this.btnValidate.Enabled = false;
            this.btnReload.Enabled = false;
            this.btnSaveFile.Enabled = false;
            this.UpdateInputVisibleState(false);

        }

        /// <summary>
        /// Hide all labels
        /// </summary>
        private void HideAllLabel()
        {
            this.lblCharacterLengthError.Hide();
            this.lblDataTypeError.Hide();
            this.lblPartDuplicate.Hide();
            this.lblErrorContent.Hide();
            this.lblDuplicateDataBaseData.Hide();
            this.lblEmptyContent.Hide();
            this.lblRowDuplicate.Hide();
            this.lblPossibleDuplicate.Hide();
            this.lblDuplicateDataBaseDataColor.Hide();
            this.lblCharterLengthErrorColor.Hide();
            this.lblDataTypeErrorColor.Hide();
            this.lblPartDuplicateColor.Hide();
            this.lblErrorContentColor.Hide();
            this.lblEmptyContentColor.Hide();
            this.lblRowDuplicateColor.Hide();
            this.lblPossibleDuplicateColor.Hide();

        }

        /// <summary>
        /// Reads the file and displays its contents
        /// </summary>
        /// <param name="sender">The btnReadFile_Click object itself</param>
        /// <param name="e">Record additional information for clicking btnReadFile</param>
        private void btnReadFile_Click(object sender, EventArgs e)
        {
            //connection string
            this._connStr = "Server = 192.168.0.236,1433; Initial Catalog = ITLTest;user Id = EKSDBUser; Password = qwe123!@#";
            //Sql commandtext

            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (this.dgvDataTable.Columns.Contains("Select"))
            {
                this.dgvDataTable.Columns.Clear();
            }

            //Determine whether the OK button is clicked
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get file Path
                this._filePath = openFileDialog.FileName;

                if (this._filePath != string.Empty && Path.GetExtension(this._filePath) == ".csv")
                {
                    try
                    {   //Hide input
                        this.UpdateInputVisibleState(false);
                        //Hide button
                        this._cSVFileHelper.ClearErrorCellInformationList();
                        this._cSVFileHelper.ErrorLog.Clear();
                        //Read file content and dispaly
                        this.ReadFileContentAndDispaly();
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
                this.btnValidate.Enabled = true;
                this.btnSaveFile.Enabled = true;
                this.btnReload.Enabled = true;
            }
        }

        /// <summary>
        /// Save file content data 
        /// </summary>
        /// <param name="sender">The btnSaveFile_Click object itself</param>
        /// <param name="e">Record additional information for clicking btnSaveFile</param>
        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            //Connection string
            this._connStr = "Server = 192.168.0.236,1433; Initial Catalog = ITLTest;user Id = EKSDBUser; Password = qwe123!@#";
            //Sql commandtext          
            string updateDataSql = "EXEC sp_UpdateStudentAdmissionInfoByEmail @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
            string saveSql = "EXEC sp_AddStudentAdmissionInfoDetial @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
            string overwriteSql = "EXEC sp_OverwriteStudentAdmissionInfoEmail @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
            //int nameMaxLength = 64;
            //int emailMaxLength = 64;
            DataGridViewButtonColumn btnOverwrite = new DataGridViewButtonColumn();
            DataGridViewButtonColumn btnSave = new DataGridViewButtonColumn();
            DataGridViewCheckBoxColumn chkSelect = new DataGridViewCheckBoxColumn();
            btnOverwrite.Name = "Overwrite";
            btnSave.Name = "Save";
            chkSelect.DataPropertyName = "isChecked";
            chkSelect.Name = "Select";
            chkSelect.TrueValue = true;
            chkSelect.FalseValue = false;
            chkSelect.Resizable = DataGridViewTriState.True;
            btnSave.DefaultCellStyle.NullValue = "Save";
            btnOverwrite.DefaultCellStyle.NullValue = "Overwrite";
            chkSelect.DefaultCellStyle.NullValue = "Select";

            if (this._cSVFileHelper.CsvContentDataTable != null)
            {
                //Determine whether the check passes
                if (this._isCheckPass)
                {
                    try
                    {
                        if (this._cSVFileHelper.PossibleDuplicateDataTable.Rows.Count > 0)
                        {
                            MessageBox.Show("If suspected duplicate data is found, please deal with suspected duplicate data!");
                            this.dgvDataTable.DataSource = this._cSVFileHelper.PossibleDuplicateDataTable.Copy();
                            this.UpdateInputVisibleState(false);
                            this.dgvDataTable.Columns.Add(btnSave);
                            this.dgvDataTable.Columns.Add(btnOverwrite);

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
                            //Add a row number to the column header
                            for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
                            {
                                this.dgvDataTable.Rows[i].HeaderCell.Value = (i + 1).ToString();
                            }
                            //Disallow sorting
                            for (int i = 0; i < this.dgvDataTable.Columns.Count; i++)
                            {
                                this.dgvDataTable.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                            }

                            this.btnSaveFile.Enabled = false; ;
                        }
                        else
                        {
                            if (this._cSVFileHelper.DuplicateDataTable.Rows.Count > 0)
                            {
                                DialogResult dialogResult = MessageBox.Show("The data in the file and the data in the database are duplicated.Do you override duplicate data?", "File content duplication prompts", MessageBoxButtons.YesNo);
                                //Duplicate data is overwritten if it exists
                                if (dialogResult.Equals(DialogResult.Yes))
                                {
                                    //save not duplicate data and update duplicate data
                                    if (this.UpdateDupicateAndSaveNotDuplicateData(saveSql, overwriteSql, updateDataSql))
                                    {
                                        this.UpdateInputVisibleState(false);
                                        this._filePath = string.Empty;
                                        this._cSVFileHelper.DestroyCSVContentDataTable();
                                        this.btnSaveFile.Enabled = false;
                                        this.btnReload.Enabled = false;
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
                                    //Save and ovwewrite data
                                    if (this.UpdateDupicateAndSaveNotDuplicateData(saveSql, overwriteSql))
                                    {
                                        this._filePath = string.Empty;
                                        this._cSVFileHelper.DestroyCSVContentDataTable();
                                        this.UpdateInputVisibleState(false);
                                        this.btnSaveFile.Enabled = false;
                                        this.btnReload.Enabled = false;
                                        this.dgvDataTable.DataSource = null;
                                        MessageBox.Show("Success");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Fail");
                                    }
                                }
                            }
                            //Save the contents of data that is not duplicated
                            else
                            {
                                try
                                {
                                    //Save and ovwewrite data
                                    if (this.UpdateDupicateAndSaveNotDuplicateData(saveSql, overwriteSql))
                                    {
                                        this._filePath = string.Empty;
                                        this._cSVFileHelper.DestroyCSVContentDataTable();
                                        this.btnSaveFile.Enabled = false;
                                        this.btnReload.Enabled = false;
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
                    MessageBox.Show("Please process the error entries in the log window first");
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
            this.btnSaveFile.Enabled = true;
            if (this.dgvDataTable.Columns.Contains("Select"))
            {
                this.dgvDataTable.Columns.Clear();
            }
            //Re read file contents
            if (this._filePath != string.Empty && Path.GetExtension(this._filePath) == ".csv")
            {
                try
                {
                    //Hide input
                    this.UpdateInputVisibleState(false);
                    //Hide button
                    this._cSVFileHelper.ClearErrorCellInformationList();
                    this._cSVFileHelper.ErrorLog.Clear();
                    //Read file content and dispaly
                    this.ReadFileContentAndDispaly();
                    MessageBox.Show("Refresh successful");
                }
                catch (ReadAndSaveFileException ex)
                {
                    MessageBox.Show(ex.ExceptionMessage.ToString());
                }
            }
        }

        /// <summary>
        /// Triggered when the button in the cell is clicked
        /// </summary>
        /// <param name="sender">The dgvDataTable_CellContentClick object itself</param>
        /// <param name="e">Records additional information when the contents of a cell are clicked</param>
        private void dgvDataTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectRowIndex = this.dgvDataTable.SelectedCells[0].RowIndex;
            //Overwrite Possible duplicate data
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
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
                //Add a row number to the column head
                for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
                {
                    this.dgvDataTable.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }

                if (this.dgvDataTable.Rows.Count == 0)
                {
                    this.dgvDataTable.Columns.Clear();
                }
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
            string overwriteSql = "EXEC sp_OverwriteStudentAdmissionInfoEmail @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
            string updateDataSql = "EXEC sp_UpdateStudentAdmissionInfoByEmail @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";
            string saveSql = "EXEC sp_AddStudentAdmissionInfoDetial @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";

            if (this._cSVFileHelper.DuplicateDataTable.Rows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("The data in the file and the data in the database are duplicated.Do you override duplicate data?", "File content duplication prompts", MessageBoxButtons.YesNo);
                //Duplicate data is overwritten if it exists
                if (dialogResult.Equals(DialogResult.Yes))
                {
                    //Save not duplicate data and update duplicate data
                    if (this.UpdateDupicateAndSaveNotDuplicateData(saveSql, overwriteSql, updateDataSql))
                    {
                        this._filePath = string.Empty;
                        this._cSVFileHelper.DestroyCSVContentDataTable();
                        this.btnSaveFile.Enabled = false;
                        this.btnReload.Enabled = false;
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
                    //Save and overwrite data
                    if (this.UpdateDupicateAndSaveNotDuplicateData(saveSql, overwriteSql))
                    {
                        this._filePath = string.Empty;
                        this._cSVFileHelper.DestroyCSVContentDataTable();
                        this.btnSaveFile.Enabled = false;
                        this.btnReload.Enabled = false;
                        this.dgvDataTable.DataSource = null;
                        MessageBox.Show("Success");
                    }
                    else
                    {
                        MessageBox.Show("Fail");
                    }
                }
            }
            //Save the contents of data that is not duplicated
            else
            {
                try
                {
                    //Save and overwritedata
                    if (this.UpdateDupicateAndSaveNotDuplicateData(saveSql, overwriteSql))
                    {
                        this._filePath = string.Empty;
                        this._cSVFileHelper.DestroyCSVContentDataTable();
                        this.btnSaveFile.Enabled = false;
                        this.btnReload.Enabled = false;
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
        }

        /// <summary>
        /// Mouse click events in dgvDataTable
        /// </summary>
        /// <param name="sender">The dgvDataTable_CellMouseClick object itself</param>
        /// <param name="e">Record additional information for the clicked cell</param>
        private void dgvDataTable_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int checkboxIndex = 0;

            //Check box exists
            if (this.dgvDataTable.Columns.Contains("Select"))
            {
                for (int i = 0; i < dgvDataTable.Columns.Count; i++)
                {
                    if (this.dgvDataTable.Columns[i].Name.Equals("Select"))
                    {
                        checkboxIndex = i;
                    }
                }

                //It takes effect only when the check box is clicked
                if (e.RowIndex != -1 && e.ColumnIndex != -1 && e.ColumnIndex == 0)
                {
                    //Changes the value of the checkbox cell if it exists
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
        }

        /// <summary>
        /// Occurs when you need to format the contents of a cell.
        /// </summary>
        /// <param name="sender">The dgvDataTable_CellFormatting object itself</param>
        /// <param name="e">Record additional information that changes cell content</param>
        private void dgvDataTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //Triggered when the value of the checkbox is changed
            if (this.dgvDataTable.Columns[e.ColumnIndex].Name.Equals("Select"))
            {
                if (e.Value == null)
                {
                    e.Value = false;
                }
            }
        }

        ///// <summary>
        ///// Do not select all rows
        ///// </summary>
        ///// <param name="sender">The btnNotSelectAll_Click object itself</param>
        ///// <param name="e">Record additional information for clicking btnNotSelect</param>
        //private void btnNotSelectAll_Click(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < dgvDataTable.Rows.Count; i++)
        //    {
        //        //Don't select all rows
        //        this.dgvDataTable.Rows[i].Cells[0].Value = false;
        //    }
        //}

        ///// <summary>
        ///// Select all rows
        ///// </summary>
        ///// <param name="sender">The btnSelectAll_Click object itself</param>
        ///// <param name="e">Record additional information for clicking btnSelectAll</param>
        //private void btnSelectAll_Click(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < dgvDataTable.Rows.Count; i++)
        //    {
        //        //Select all rows
        //        this.dgvDataTable.Rows[i].Cells[0].Value = true;
        //    }
        //}

        ///// <summary>
        ///// Saving Data in Batches
        ///// </summary>
        ///// <param name="sender">The btnBatchsSaving_Click object itself</param>
        ///// <param name="e">Record additional information for clicking btnBatchsSaving</param>
        //private void btnBatchsSaving_Click(object sender, EventArgs e)
        //{
        //    //Check that rows are selected
        //    if (this.CheckIsSelectedRow())
        //    {
        //        for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
        //        {
        //            //Determines whether the checkbox is selected
        //            if ((bool)this.dgvDataTable.Rows[i].Cells[0].Value == true)
        //            {
        //                this._cSVFileHelper.NotDuplicateDataTable.Rows.Add(this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i].ItemArray);
        //                //this._stringBuilderLog.Append(string.Format("The {0} line is marked as rewritten", i+1)+"\r\n");
        //                this._cSVFileHelper.PossibleDuplicateDataTable.Rows.RemoveAt(i);
        //                this.dgvDataTable.Rows.RemoveAt(i);
        //                i--;
        //            }
        //        }
        //        //Add a row number to the column head
        //        for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
        //        {
        //            this.dgvDataTable.Rows[i].HeaderCell.Value = (i + 1).ToString();
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please select the row to save");
        //    }

        //    if (this.dgvDataTable.Rows.Count == 0)
        //    {
        //        this.dgvDataTable.Columns.Clear();
        //        this.UpdateButtonVisibleState(false);
        //    }
        //}

        ///// <summary>
        ///// Saving Data in Batches
        ///// </summary>
        ///// <param name="sender">The btnBatchOverwrite_Click object itself</param>
        ///// <param name="e">Record additional information for clicking btnBatchOverwrite</param>
        //private void btnBatchOverwrite_Click(object sender, EventArgs e)
        //{
        //    //Check that rows are selected
        //    if (this.CheckIsSelectedRow())
        //    {
        //        for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
        //        {
        //            //Determines whether the checkbox is selected
        //            if ((bool)this.dgvDataTable.Rows[i].Cells[0].Value == true)
        //            {
        //                this._cSVFileHelper.OverwriteDataTable.Rows.Add(this._cSVFileHelper.PossibleDuplicateDataTable.Rows[i].ItemArray);
        //                //this._stringBuilderLog.Append(string.Format("The {0} line is marked as rewritten", i+1)+"\r\n");
        //                this._cSVFileHelper.PossibleDuplicateDataTable.Rows.RemoveAt(i);
        //                this.dgvDataTable.Rows.RemoveAt(i);
        //                i--;
        //            }
        //        }
        //        //Add a row number to the column head
        //        for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
        //        {
        //            this.dgvDataTable.Rows[i].HeaderCell.Value = (i + 1).ToString();
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please select the row to overwrite.");
        //    }

        //    if (this.dgvDataTable.Rows.Count == 0)
        //    {
        //        this.dgvDataTable.Columns.Clear();
        //        this.UpdateButtonVisibleState(false);
        //    }
        //}

        #region private method
        /// <summary>
        /// Change the visible state  of the input box
        /// </summary>
        /// <param name="visibleState">The state of being visible or not</param>
        private void UpdateInputVisibleState(bool visibleState)
        {

            //Hidden controls
            if (visibleState == false)
            {

            }
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
                    //As long as any row in the table is selected, true is returned.
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
            //Sort by row
            this._cSVFileHelper.ErrorCellInformationList = this._cSVFileHelper.ErrorCellInformationList.OrderBy(e => e.ErrorRow).ToList();

            for (int i = 0; i < this._cSVFileHelper.ErrorCellInformationList.Count; i++)
            {
                switch (this._cSVFileHelper.ErrorCellInformationList[i].ErrorType)
                {
                    //If the cell content is empty
                    case ErrorCellInformation._errorTypeEnum.emptyContent:
                        this.dgvDataTable.Rows[this._cSVFileHelper.ErrorCellInformationList[i].ErrorRow].Cells[this._cSVFileHelper.ErrorCellInformationList[i].ErrorColumn].Style.BackColor = Color.Red;
                        this.lblEmptyContent.Show();
                        this.lblEmptyContentColor.Show();
                        break;
                    //If the contents of the file are duplicated
                    case ErrorCellInformation._errorTypeEnum.partduplicate:
                        this.dgvDataTable.Rows[this._cSVFileHelper.ErrorCellInformationList[i].ErrorRow].Cells[this._cSVFileHelper.ErrorCellInformationList[i].ErrorColumn].Style.BackColor = Color.Gray;
                        this.lblPartDuplicate.Show();
                        this.lblPartDuplicateColor.Show();
                        break;
                    //If the cell content duplicates the database content
                    case ErrorCellInformation._errorTypeEnum.duplicateDbContent:
                        this.dgvDataTable.Rows[this._cSVFileHelper.ErrorCellInformationList[i].ErrorRow].Cells[this._cSVFileHelper.ErrorCellInformationList[i].ErrorColumn].Style.BackColor = Color.LightYellow;
                        this.lblDuplicateDataBaseData.Show();
                        this.lblDuplicateDataBaseDataColor.Show();
                        break;
                    //If the data type of the content in the cell is wrong
                    case ErrorCellInformation._errorTypeEnum.filedTypeError:
                        this.dgvDataTable.Rows[this._cSVFileHelper.ErrorCellInformationList[i].ErrorRow].Cells[this._cSVFileHelper.ErrorCellInformationList[i].ErrorColumn].Style.BackColor = Color.Orange;
                        this.lblDataTypeError.Show();
                        this.lblDataTypeErrorColor.Show();
                        break;
                    //If the character length of the cell content is not within the rule
                    case ErrorCellInformation._errorTypeEnum.characterLengthError:
                        this.dgvDataTable.Rows[this._cSVFileHelper.ErrorCellInformationList[i].ErrorRow].Cells[this._cSVFileHelper.ErrorCellInformationList[i].ErrorColumn].Style.BackColor = Color.LightPink;
                        this.lblCharacterLengthError.Show();
                        this.lblCharterLengthErrorColor.Show();
                        break;
                    //If the cell content is not the specified content
                    case ErrorCellInformation._errorTypeEnum.contentError:
                        this.dgvDataTable.Rows[this._cSVFileHelper.ErrorCellInformationList[i].ErrorRow].Cells[this._cSVFileHelper.ErrorCellInformationList[i].ErrorColumn].Style.BackColor = Color.LightSteelBlue;
                        this.lblErrorContent.Show();
                        this.lblErrorContentColor.Show();
                        break;
                    case ErrorCellInformation._errorTypeEnum.possibleDuplicate:
                        this.dgvDataTable.Rows[this._cSVFileHelper.ErrorCellInformationList[i].ErrorRow].Cells[this._cSVFileHelper.ErrorCellInformationList[i].ErrorColumn].Style.BackColor = Color.Yellow;
                        this.lblPossibleDuplicate.Show();
                        this.lblPossibleDuplicateColor.Show();
                        break;
                    case ErrorCellInformation._errorTypeEnum.rowDuplicate:
                        this.dgvDataTable.Rows[this._cSVFileHelper.ErrorCellInformationList[i].ErrorRow].Cells[this._cSVFileHelper.ErrorCellInformationList[i].ErrorColumn].Style.BackColor = Color.MediumSlateBlue;
                        this.lblRowDuplicate.Show();
                        this.lblRowDuplicateColor.Show();
                        break;
                }

                //Displays a specific error message for a specific cell
                //stringBuider.Append(this._cSVFileHelper.ErrorLog[i].ToString() + "\r\n");
                //this.lstvlog.Text = stringBuider.ToString() + this._cSVFileHelper.ErrorLog;
                //this.dgvDataTable.FirstDisplayedScrollingRowIndex = this._cSVFileHelper.ErrorCellInformationList[0].ErrorRow;
            }

            //this._cSVFileHelper.ClearErrorCellInformationList();
        }


        /// <summary>
        /// Display log
        /// </summary>
        private void DisplayErorrLog()
        {
            if (this._cSVFileHelper.ErrorLog.ToString() != string.Empty)
            {
                //this.lstvlog.Text += this._cSVFileHelper.ErrorLog.ToString();
            }
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
            //Open connection
            dbHelper.OpenDbConnection();
            SqlCommand sqlCommand = new SqlCommand(saveSql, dbHelper.SqlCon);
            //Open transaction
            SqlTransaction sqlTransaction = dbHelper.SqlCon.BeginTransaction();
            sqlCommand.Transaction = sqlTransaction;
            bool result = true;

            try
            {
                //Save not duplicate data
                if (this._cSVFileHelper.NotDuplicateDataTable.Rows.Count > 0)
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
                //Overwrite Data
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
            //Open connection
            dbHelper.OpenDbConnection();
            SqlCommand sqlCommand = new SqlCommand(saveSql, dbHelper.SqlCon);
            //Open transaction
            SqlTransaction sqlTransaction = dbHelper.SqlCon.BeginTransaction();
            sqlCommand.Transaction = sqlTransaction;
            bool result = true;

            try
            {
                //Save not duplicate data
                if (this._cSVFileHelper.NotDuplicateDataTable.Rows.Count > 0)
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
                //Overwrite data
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
                //Update duplicate data
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

        /// <summary>
        /// Read and display the contents of the file
        /// </summary>
        /// <param name="findDuplicateDataSql">Find dupliacte data sql command text</param>
        /// <param name="findPossibleDuplicateDataSql">Find possible dupliacte data sql command text</param>
        private void ReadFileContentAndDispaly()
        {
            //Get file content          
            this._isCheckPass = this._cSVFileHelper.GetFileDataToDataTableAndCheckDataTable(this._filePath);
            //Display csv file datatable
            this.dgvDataTable.DataSource = this._cSVFileHelper.CsvContentDataTable.Copy();
            //Display error messages
            this.DisplayErorrLog();
            this.displayErrorCellAndLog();

            //Add a row number to the column head
            for (int i = 0; i < this.dgvDataTable.Rows.Count; i++)
            {
                this.dgvDataTable.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            //Sort not allowed
            for (int i = 0; i < this.dgvDataTable.Columns.Count; i++)
            {
                this.dgvDataTable.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        #endregion

        private void btnValidate_Click(object sender, EventArgs e)
        {
            //string findDuplicateDataSql = "Exec sp_CheckStudentAdmissionInfoEmailIsExists @Email = @Email";
            //string findPossibleDuplicateDataSql = "Exec sp_CheckStudentAdmissionInfoDetailIsMatch @Name = @Name, @Age = @Age, @Sex = @Sex";


            //Check that the file content matches the data table structure
            if (this._cSVFileHelper.CheckIsMatch())
            {
                if (this._cSVFileHelper.CheckDataIsDuplicated())
                {
                    //this._cSVFileHelper.SiftedFileContents(findDuplicateDataSql, findPossibleDuplicateDataSql, this._connStr);
                }
                else
                {
                    this._isCheckPass = false;
                }
            }
            else
            {
                this._cSVFileHelper.CheckDataIsDuplicated();
                this._isCheckPass = false;
            }
            this.displayErrorCellAndLog();
            listView1.BeginUpdate();

            for (int i = 0; i < this._cSVFileHelper.ErrorLog.Count; i++)
            {
                int column = i + 1;

                listView1.Items.Add(this._cSVFileHelper.ErrorLog[i].ToString());

            }


            listView1.EndUpdate();
        }
    }
}


