using ReadAndSaveCSVFile;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
namespace ReadAndSaveFile
{
    public partial class ReadAndSaveFileForm : Form
    {
        #region member
        private string _filePath = string.Empty;
        private StreamReader _streamReader = null;
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
                        this._streamReader = new StreamReader(this._filePath);
                        //Get file content
                        this._cSVFileHelper.GetFileDataToDataTable(this._streamReader);
                        //Display csv file datatable
                        this.dgvDataTable.DataSource = this._cSVFileHelper.CsvContentDataTable.Copy();
                    }
                    catch (IOException)
                    {
                        throw new ReadAndSaveFileException("The file is occupied by another program");
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

                if (this._streamReader != null)
                {
                    this._streamReader.Close();
                }
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
            int maximumAge = 35;
            int minimumAge = 10;
            int maximumSNumber = 202299999;
            int minimumSNumber = 202200000;
            int emailMaxLength = 64;
            DateTime minimumDate = new DateTime(2022, 1, 3);
            DateTime maxminumDate = new DateTime(2022, 12, 25);
            string targetTable = "StudentAdmissionInfo";
            string findDataSql = "EXEC sp_CheckStudentAdmissionInfoSnumberIsExists @Snumber = @Snumber";
            DataTable dublicateDataTable = new DataTable();
            DataTable notDublicateDataTable = new DataTable();
            string updateDataSql = "EXEC sp_UpdateStudentAdmissionInfoBySnmber @Name = @Name, @Age = @Age, @Snumber = @Snumber, @Sex = @Sex, @Email = @Email, @AdmissionDate = @AdmissionDate";
            ReadAndSaveFileDbHelper readAndSaveFileDbHelper = new ReadAndSaveFileDbHelper(this._connStr);

            if (this._cSVFileHelper.CsvContentDataTable != null)
            {
                //Get the file content data
                try
                {
                    //Determines if it matches the structure of the data table in the database
                    if (this._cSVFileHelper.CheckIsMatch(nameMaxLength, maximumAge, minimumAge, maximumSNumber, minimumSNumber, emailMaxLength, minimumDate, maxminumDate))
                    {
                        //Check if the file contains duplicate content
                        if (this._cSVFileHelper.CheckCSVDataIsRepeated())
                        {
                            //Get duplicate and non-duplicate data
                            this._cSVFileHelper.GetReapeatedAndNotRepeatedData(findDataSql, this._connStr);
                            notDublicateDataTable = this._cSVFileHelper.NotDuplicateDataTable.Copy();
                            dublicateDataTable = this._cSVFileHelper.DuplicateDataTable.Copy();

                            if (dublicateDataTable.Rows.Count > 0)
                            {
                                DialogResult dialogResult = MessageBox.Show("The data in the file and the data in the database are duplicated.Do you override duplicate data?", "File content duplication prompts", MessageBoxButtons.YesNo);
                                //Duplicate data is overwritten if it exists
                                if (dialogResult.Equals(DialogResult.Yes))
                                {
                                    //Insert non-duplicate data and update duplicate data
                                    if (readAndSaveFileDbHelper.SaveAndUpdateBulkData(updateDataSql, dublicateDataTable, notDublicateDataTable, targetTable))
                                    {
                                        this._filePath = string.Empty;
                                        this._cSVFileHelper.DestroyCSVContentDataTable();
                                        this.dgvDataTable.DataSource = null;
                                        this._streamReader.Close();
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
                                        this._streamReader.Close();
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
                                    this._streamReader.Close();
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
                            MessageBox.Show("");
                        }
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
    }
}
