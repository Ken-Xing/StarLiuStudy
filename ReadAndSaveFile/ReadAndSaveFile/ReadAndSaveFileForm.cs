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
            string findDataSql = "Exec sp_CheckStudentAdmissionInfoEmailIsExists @Email = @Email";
            DataTable dublicateDataTable = new DataTable();
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
                            this._cSVFileHelper.GetDuplicatedAndDuplicateedData(findDataSql, this._connStr);

                            if (dublicateDataTable.Rows.Count > 0)
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
        }

        /// <summary>
        /// Re-read the contents of the file
        /// </summary>
        /// <param name="sender">The btnRefresh_Click object itself</param>
        /// <param name="e">Record additional information for clicking btnRefresh</param>
        private void btnRefresh_Click(object sender, EventArgs e)
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


        /// <summary>
        /// Display cells and logs that do not conform to the rules
        /// </summary>
        private void displayErrorCellAndLog()
        {
            StringBuilder stringBuilder = new StringBuilder();
            //Change the background color of cells whose content does not conform to the rules
            for (int i = 0; i < this._cSVFileHelper.ErrorRow.Count; i++)
            {
                this.dgvDataTable.Rows[this._cSVFileHelper.ErrorRow[i]].Cells[this._cSVFileHelper.ErrorColumn[i]].Style.BackColor = Color.Red;
                stringBuilder.Append(this._cSVFileHelper.ErrorMessage[i].ToString() + "\r\n");
            }

            this.dgvDataTable.FirstDisplayedScrollingRowIndex = this._cSVFileHelper.ErrorRow[0];
            //Output the specific cell information of the specific row that does not conform to the rule
            this.txtErrorLog.Text = stringBuilder.ToString();
        }
    }
}
