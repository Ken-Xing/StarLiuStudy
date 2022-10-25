using ReadAndSaveCSVFile;
using System;
using System.Data;
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
                        this.dgvDataTable.DataSource = this._cSVFileHelper.CsvContentDataTable;

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
            else
            {
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
            DbBase.DbHelper dbhelper = new DbBase.DbHelper(this._connStr);
            string fileContent = string.Empty;
            int nameMaxLength = 64;
            int emailMaxLength = 64;
            string targetTable = "StudentAdmissionInfo";
            string findDuplicateDataSql = "Exec sp_CheckStudentAdmissionInfoEmailIsExists @Email = @Email";
            string findPossibleDuplicateDataSql = "Exec sp_CheckStudentAdmissionInforDetailIsMatch @Name = @Name, @Age = @Age, @Sex = @Sex";
            DataTable duplicateDataTable = new DataTable();
            DataTable notDublicateDataTable = new DataTable();
            string updateDataSql = "EXEC sp_UpdateStudentAdmissionInfoBySnmber @Name = @Name, @Age = @Age, @Sex = @Sex, @Email = @Email";


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
                                

                                //PossibleDuplicateDateForm possibleDuplicateDateForm = new PossibleDuplicateDateForm();
                               
                                //possibleDuplicateDateForm.ShowDialog();

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
                        MessageBox.Show("The file content does not conform to the rules.  See the error log output window for details");
                    }
                }
                //Catch custom exceptions
                catch (ReadAndSaveFileException ex)
                {
                    MessageBox.Show(ex.ExceptionMessage.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please select the file you want to open!");
            }
        }

        private void ReadAndSaveFileForm_Load(object sender, EventArgs e)
        {
            //Disable the savefile button when the form is loaded
            this.btnSaveFile.Enabled = false;
            this.btnRefresh.Enabled = false;
            //Change the background color of the input box
            this.txtEmptyContent.Text = "EmptyContent";
            this.txtDuplicateContent.Text = "DuplicateContent";
            this.txtDuplicateDbContent.Text = "DuplicateDatabaseContent";
            this.txtErrorContent.Text = "ErrorContent";
            this.txtCharacterLengthError.Text = "CharacterLengthError";
            this.txtFiledTypeError.Text = "FiledTypeError";
            this.txtEmptyContent.BackColor = Color.Red;
            this.txtDuplicateContent.BackColor = Color.Gray;
            this.txtDuplicateDbContent.BackColor = Color.LightYellow;
            this.txtCharacterLengthError.BackColor = Color.LightPink;
            this.txtFiledTypeError.BackColor = Color.Orange;
            this.txtErrorContent.BackColor = Color.LightSteelBlue;

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


        /// <summary>
        /// Display cells and logs that do not conform to the rules
        /// </summary>
        private void displayErrorCellAndLog()
        {
            ErrorCellInformation errorCellInformation = new ErrorCellInformation();
            StringBuilder stringBuilder = new StringBuilder();

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

                stringBuilder.Append(this._cSVFileHelper.ErrorCellInformationList[i].ErrorMessage.ToString() + "\r\n");
            }

            this.dgvDataTable.FirstDisplayedScrollingRowIndex = this._cSVFileHelper.ErrorCellInformationList[0].ErrorRow;
            //Output the specific cell information of the specific row that does not conform to the rule
            this.txtErrorLog.Text = stringBuilder.ToString();
            this._cSVFileHelper.DestroyErrorCellInformation();
        }
    }
}
