using DbBase;
using ReadAndSaveFile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ReadAndSaveCSVFile
{
    public class CSVFileHelper
    {
        #region member
        private DataTable _csvContentDataTable = null;
        private DataTable _duplicateDataTable = null;
        private DataTable _notDuplicateDataTable = null;
        private DataTable _possibleDuplicateDataTable = null;
        private List<ErrorCellInformation> _errorCellInformationList = new List<ErrorCellInformation>();
        private DataTable _overwriteDataTable = null;
        private List<string> _errorlog = new List<string>();
        #endregion

        /// <summary>
        /// Read-only this._duplicateDataTable
        /// </summary>
        public DataTable DuplicateDataTable
        {
            get
            {
                return this._duplicateDataTable;
            }
        }

        /// <summary>
        /// Initialize this._notDuplicateDataTable
        /// </summary>
        public DataTable NotDuplicateDataTable
        {
            get
            {
                return this._notDuplicateDataTable;
            }
            set
            {
                this._notDuplicateDataTable = value;

            }
        }

        /// <summary>
        /// Initialize this._possibleDuplicateDataTable
        /// </summary>
        public DataTable PossibleDuplicateDataTable
        {
            get
            {
                return this._possibleDuplicateDataTable;
            }
            set
            {
                this._notDuplicateDataTable = value;
            }
        }

        /// <summary>
        /// Initialize this._overwriteDataTable
        /// </summary>
        public DataTable OverwriteDataTable
        {
            get
            {
                return this._overwriteDataTable;
            }
            set
            {
                this._overwriteDataTable = value;
            }
        }

        /// <summary>
        /// Read-only this._csvContentDataTable
        /// </summary>
        public DataTable CsvContentDataTable
        {
            get
            {
                return this._csvContentDataTable;
            }
        }

        /// <summary>
        /// Initialize this._errorCellInformationList
        /// </summary>
        public List<ErrorCellInformation> ErrorCellInformationList
        {
            get
            {
                return this._errorCellInformationList;
            }
            set
            {
                this._errorCellInformationList = value;
            }
        }

        /// <summary>
        /// Read-only this._errorlog
        /// </summary>
        public List<string> ErrorLog
        {
            get
            {
                return this._errorlog;
            }
        }

        /// <summary>
        /// Destroying member this._cSvContentDataTable
        /// </summary>
        public void DestroyCSVContentDataTable()
        {
            this._csvContentDataTable = null;
        }

        /// <summary>
        /// clear member this._errorCellInformationList
        /// </summary>
        public void ClearErrorCellInformationList()
        {
            this._errorCellInformationList.Clear();
        }

        /// <summary>
        /// Check the contents of a file when it is read
        /// </summary>
        /// <param name="filePath">The path to the file</param>
        /// <param name="findDuplicateDataSql">Find duplicate data sql command text</param>
        /// <param name="findPossibleDuplicateDataSql">Find possible duplicate data sql command text</param>
        /// <param name="connStr">Connection string</param>
        /// <returns>Returns true if the check passed, false otherwise</returns>
        /// <exception cref="ReadAndSaveFileException"></exception>
        public bool GetFileDataToDataTableAndCheckDataTable(string filepath)
        {
            bool isCheckPass = true;
            string filecontent = string.Empty;
            bool isFirst = true;
            string[] arr = null;
            DataRow dataRow = null;
            this._csvContentDataTable = new DataTable();
            StreamReader streamReader = null;

            try
            {
                streamReader = new StreamReader(filepath);
                if (streamReader != null)
                {
                    //Read line by line
                    while ((filecontent = streamReader.ReadLine()) != null)
                    {
                        arr = filecontent.Split(new char[] { ',' });

                        if (isFirst)
                        {
                            //Get the column name
                            foreach (string item in arr)
                            {

                                this._csvContentDataTable.Columns.Add(item);
                            }

                            //Disable reading column names after all column names have been read
                            isFirst = false;
                        }
                        else
                        {
                            dataRow = this._csvContentDataTable.NewRow();
                            //Get the data row
                            for (int i = 0; i < this._csvContentDataTable.Columns.Count; i++)
                            {
                                if (i < arr.Length)
                                {
                                    dataRow[i] = arr[i];
                                }
                                else
                                {
                                    dataRow[i] = string.Empty;
                                }
                            }

                            this._csvContentDataTable.Rows.Add(dataRow);
                        }
                    }
                }
            }
            //This exception is thrown when a file is occupied
            catch (IOException)
            {
                throw new ReadAndSaveFileException("The file is occupied by another program");
            }
            //This exception is thrown when duplicate columns appear in a file
            catch (DuplicateNameException)
            {
                throw new ReadAndSaveFileException("Duplicate column names are not allowed");
            }
            catch
            {
                throw;
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }

            }

            //Returns true if the check passed, false otherwise
            return isCheckPass;
        }


        /// <summary>
        /// Check if the contents of the file are duplicated
        /// </summary>
        /// <returns>Returns false if the contents of the file are repeated and true if they are not</returns>
        public bool CheckDataIsDuplicated()
        {
            bool isChrckPass = true;
            int nameColumnIndex = -1;
            int ageColumnIndex = -1;
            int sexColumnIndex = -1;
            int emailColumnIndex = -1;
            List<string> emailColumnAllContent = new List<string>();
            List<string> duplicateEmail = new List<string>();
            DataTable notDuplicateTable = new DataTable();
            //Add column
            this._csvContentDataTable.Columns.Add("columnState");

            for (int i = 0; i < this._csvContentDataTable.Rows.Count; i++)
            {
                this._csvContentDataTable.Rows[i]["columnState"] = 0;
            }

            for (int i = 0; i < this._csvContentDataTable.Rows.Count; i++)
            {
                emailColumnAllContent.Add(this._csvContentDataTable.Rows[i]["Email"].ToString());
            }

            //Get Columns Index
            for (int i = 0; i < this._csvContentDataTable.Columns.Count; i++)
            {
                if (this._csvContentDataTable.Columns[i].ColumnName.Equals("Name"))
                {
                    nameColumnIndex = i;
                }
                else if (this._csvContentDataTable.Columns[i].ColumnName.Equals("Age"))
                {
                    ageColumnIndex = i;
                }
                else if (this._csvContentDataTable.Columns[i].ColumnName.Equals("Sex"))
                {
                    sexColumnIndex = i;
                }
                else if (this._csvContentDataTable.Columns[i].ColumnName.Equals("Email"))
                {
                    emailColumnIndex = i;
                }
            }

            for (int i = 0; i < this._csvContentDataTable.Rows.Count; i++)
            {
                for (int j = 0; j < this._csvContentDataTable.Rows.Count; j++)
                {
                    if ((this._csvContentDataTable.Rows[i]["Name"] != this._csvContentDataTable.Rows[j]["Name"] || this._csvContentDataTable.Rows[i]["Age"]!= this._csvContentDataTable.Rows[j]["Age"] || this._csvContentDataTable.Rows[i]["Sex"]!=this._csvContentDataTable.Rows[j]["Sex"]) && this._csvContentDataTable.Rows[i]["Email"].Equals(this._csvContentDataTable.Rows[j]["Email"]))
                    {
                        isChrckPass = false;
                        if (this._csvContentDataTable.Rows[j]["columnState"].Equals("1"))
                        {
                            this.AddErrorCellInformation(emailColumnIndex, i, ErrorCellInformation._errorTypeEnum.partduplicate);
                            this._errorlog.Add(string.Format("No.{0} row content duplicate", i));
                            continue;
                        }
                        else if (this._csvContentDataTable.Rows[j]["columnState"].Equals("0"))
                        {
                            this._csvContentDataTable.Rows[i]["columnState"] = 1;
                        }
                    }
                }
            }

            this._csvContentDataTable.Columns.Remove("columnState");
            this._csvContentDataTable.Columns.Add("columnState");
            for (int i = 0; i < this._csvContentDataTable.Rows.Count; i++)
            {
                this._csvContentDataTable.Rows[i]["columnState"] = 0;
            }

            for (int i = 0; i < this._csvContentDataTable.Rows.Count; i++)
            {
                for (int j = 0; j < this._csvContentDataTable.Rows.Count; j++)
                {
                    if (this._csvContentDataTable.Rows[i]["Name"].Equals(this._csvContentDataTable.Rows[j]["Name"]) && this._csvContentDataTable.Rows[i]["Age"].Equals(this._csvContentDataTable.Rows[j]["Age"]) && this._csvContentDataTable.Rows[i]["Sex"].Equals(this._csvContentDataTable.Rows[j]["Sex"]) && this._csvContentDataTable.Rows[i]["Email"] != this._csvContentDataTable.Rows[j]["Email"])
                    {
                        isChrckPass = false;
                        if (this._csvContentDataTable.Rows[j]["columnState"].Equals("1"))
                        {
                            this.AddErrorCellInformation(nameColumnIndex, i, ErrorCellInformation._errorTypeEnum.possibleDuplicate);
                            this.AddErrorCellInformation(ageColumnIndex, i, ErrorCellInformation._errorTypeEnum.possibleDuplicate);
                            this.AddErrorCellInformation(sexColumnIndex, i, ErrorCellInformation._errorTypeEnum.possibleDuplicate);

                            this._errorlog.Add(string.Format("No.{0} row content duplicate", i));
                            continue;
                        }
                        else if (this._csvContentDataTable.Rows[j]["columnState"].Equals("0"))
                        {
                            this._csvContentDataTable.Rows[i]["columnState"] = 1;
                        }
                    }
                }
            }

            this._csvContentDataTable.Columns.Remove("columnState");
            this._csvContentDataTable.Columns.Add("columnState");
            for (int i = 0; i < this._csvContentDataTable.Rows.Count; i++)
            {
                this._csvContentDataTable.Rows[i]["columnState"] = 0;
            }

            for (int i = 0; i < this._csvContentDataTable.Rows.Count; i++)
            {
                for (int j = 0; j < this._csvContentDataTable.Rows.Count; j++)
                {
                    if (this._csvContentDataTable.Rows[i]["Name"].Equals(this._csvContentDataTable.Rows[j]["Name"]) && this._csvContentDataTable.Rows[i]["Age"].Equals(this._csvContentDataTable.Rows[j]["Age"]) && this._csvContentDataTable.Rows[i]["Sex"].Equals(this._csvContentDataTable.Rows[j]["Sex"]) && this._csvContentDataTable.Rows[i]["Email"].Equals(this._csvContentDataTable.Rows[j]["Email"]))
                    {
                        isChrckPass = false;
                        //Get duplicae row index 
                        if (this._csvContentDataTable.Rows[j]["columnState"].Equals("1"))
                        {
                            this.AddErrorCellInformation(nameColumnIndex, i, ErrorCellInformation._errorTypeEnum.rowDuplicate);
                            this.AddErrorCellInformation(ageColumnIndex, i, ErrorCellInformation._errorTypeEnum.rowDuplicate);
                            this.AddErrorCellInformation(sexColumnIndex, i, ErrorCellInformation._errorTypeEnum.rowDuplicate);
                            this.AddErrorCellInformation(emailColumnIndex, i, ErrorCellInformation._errorTypeEnum.rowDuplicate);
                            this._errorlog.Add(string.Format("No.{0} row content duplicate", i));
                            continue;
                        }
                        else if (this._csvContentDataTable.Rows[j]["columnState"].Equals("0"))
                        {
                            this._csvContentDataTable.Rows[i]["columnState"] = 1;
                        }
                    }
                }
            }
            return isChrckPass;
        }

        /// <summary>
        /// Filter the contents of the file
        /// </summary>
        /// <param name="findDuplicateDataSql">Find duplicate data sql command text</param>
        /// <param name="findPossibleDuplicateDataSql">Find possible duplicate data sql command text</param>
        /// <param name="connStr">Connection string</param>
        public void SiftedFileContents(string findDuplicateDataSql, string findPossibleDuplicateDataSql, string connStr)
        {
            List<SqlParameter> findPossibleDuplicateSqlParameter = new List<SqlParameter>();
            List<SqlParameter> findDuplicatesqlParameter = new List<SqlParameter>();
            DbHelper dbhelper = new DbHelper(connStr);
            int columnIndex = 0;
            this._notDuplicateDataTable = this._csvContentDataTable.Clone();
            this._possibleDuplicateDataTable = this._csvContentDataTable.Clone();
            this._duplicateDataTable = this._csvContentDataTable.Clone();
            this._overwriteDataTable = this._csvContentDataTable.Clone();
            dbhelper.OpenDbConnection();

            //Get the index of the email column
            for (int i = 0; i < this._csvContentDataTable.Columns.Count; i++)
            {
                if (this._csvContentDataTable.Columns[i].ColumnName.Equals("Email"))
                {
                    columnIndex = i;
                }
            }

            try
            {
                //Mark each row of data
                for (int i = 0; i < this._csvContentDataTable.Rows.Count; i++)
                {
                    findDuplicatesqlParameter.Add(new SqlParameter("@Name", this._csvContentDataTable.Rows[i]["Name"]));
                    findDuplicatesqlParameter.Add(new SqlParameter("@Age", this._csvContentDataTable.Rows[i]["Age"]));
                    findDuplicatesqlParameter.Add(new SqlParameter("@Sex", this._csvContentDataTable.Rows[i]["Sex"]));
                    findDuplicatesqlParameter.Add(new SqlParameter("@Email", this._csvContentDataTable.Rows[i]["Email"]));

                    //Get duplicate data
                    if (dbhelper.CheckDataIsExists(findDuplicateDataSql, findDuplicatesqlParameter))
                    {
                        this._duplicateDataTable.Rows.Add(this._csvContentDataTable.Rows[i].ItemArray);
                        this.AddErrorCellInformation(columnIndex, i, ErrorCellInformation._errorTypeEnum.duplicateDbContent);
                        this._errorlog.Add(string.Format("The {0} row of column Email duplicates the database content", i + 1));
                    }
                    //Get possible duplicate data
                    else if (dbhelper.CheckDataIsExists(findPossibleDuplicateDataSql, findDuplicatesqlParameter))
                    {
                        this._possibleDuplicateDataTable.Rows.Add(this._csvContentDataTable.Rows[i].ItemArray);
                    }
                    //Get not duplicate data
                    else
                    {
                        this._notDuplicateDataTable.Rows.Add(this._csvContentDataTable.Rows[i].ItemArray);
                    }
                    //Clear Paramete
                    findDuplicatesqlParameter.Clear();
                    findPossibleDuplicateSqlParameter.Clear();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                //Close database connection
                dbhelper.CloseDbConnection();
            }
        }

        /// <summary>
        /// Details of the error cell
        /// </summary>
        /// <param name="errorColumn"> The location of the error column</param>
        /// <param name="errorRow"> Location of the error line</param>
        /// <param name="errorTypeNumber">Error type value</param>
        /// <param name="errorMessage"> Error message</param>

        private void AddErrorCellInformation(int errorColumn, int errorRow, ErrorCellInformation._errorTypeEnum errorTypeNumber)
        {
            ErrorCellInformation errorCellInformation = new ErrorCellInformation();
            errorCellInformation.ErrorColumn = errorColumn;
            errorCellInformation.ErrorRow = errorRow;
            errorCellInformation.ErrorType = errorTypeNumber;
            //Adds the information to the List
            this._errorCellInformationList.Add(errorCellInformation);
        }

        /// <summary>
        /// Checks for a match with the data structure of the data table
        /// </summary>
        /// <param name="nameMaxLength">name MaxLength</param>
        /// <param name="emailMaxLength">email MaxLength</param>
        /// <returns>Returns true if the match is successful and false otherwise</returns>
        public bool CheckIsMatch()
        {
            int nameMaxLength = 64;
            int emailMaxLength = 64;
            bool result = true;
            //All columns of the csv file
            List<string> fileAllColumn = new List<string>();
            //All column names
            List<string> allColumn = new List<string>();
            allColumn.Add("Name");
            allColumn.Add("Age");
            allColumn.Add("Email");
            allColumn.Add("Sex");
            Regex emailRegex = new Regex("^([a-z0-9A-Z]+[-|\\.]?)+[a-z0-9A-Z]@([a-z0-9A-Z]+(-[a-z0-9A-Z]+)?\\.)+[a-zA-Z]{2,}$");
            int number = 0;
            string name = string.Empty;
            List<string> notMatchColumnsList = null;
            StringBuilder notMatchColumns = null;
            List<string> lackColumnList = null;

            if (this._csvContentDataTable.Columns.Count >= 3 && this._csvContentDataTable.Columns.Count <= 6)
            {
                if (nameMaxLength > 0)
                {
                    if (emailMaxLength > 0)
                    {
                        //Get all the column names
                        for (int i = 0; i < this._csvContentDataTable.Columns.Count; i++)
                        {
                            fileAllColumn.Add(this._csvContentDataTable.Columns[i].ColumnName);
                        }
                        //Check whether the column name exists
                        if (fileAllColumn.All(file => allColumn.Any(all => all.Trim().Equals(file))))
                        {
                            //Whether required column names are included
                            if (!allColumn.Except(fileAllColumn).Any())
                            {
                                //Judge whether the required contents comply with the rules
                                for (int i = 0; i < this._csvContentDataTable.Rows.Count; i++)
                                {
                                    ////Determines if the content is empty
                                    if ((this._csvContentDataTable.Rows[i]["Email"].ToString().Length > 0 && this._csvContentDataTable.Rows[i]["Email"].ToString().Trim() == string.Empty) || this._csvContentDataTable.Rows[i]["Email"].ToString() == string.Empty)
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Email"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.emptyContent);
                                                this._errorlog.Add(string.Format("The {0} row of the email column cannot be empty.", i + 1));
                                                result = false;
                                            }
                                        }
                                    }
                                    //Determines whether the length of the string conforms to the rule
                                    else if (this._csvContentDataTable.Rows[i]["Email"].ToString().Length > emailMaxLength)
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Email"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.characterLengthError);
                                                this._errorlog.Add(string.Format("The {0} Row of the email column must be longer than {1} characters.", i + 1, emailMaxLength));
                                                result = false;
                                            }
                                        }
                                    }
                                    //Determine if the email format conforms to the rules
                                    else if (!emailRegex.IsMatch(this._csvContentDataTable.Rows[i]["Email"].ToString()))
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Email"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.contentError);
                                                this._errorlog.Add(string.Format("The content in the {0} row of the email column does not conform to the email rules.", i + 1));
                                                result = false;
                                            }
                                        }
                                    }
                                    //Determines if the content is empty
                                    if ((this._csvContentDataTable.Rows[i]["Name"].ToString().Length > 0 && (this._csvContentDataTable.Rows[i]["Name"].ToString().Trim() == string.Empty) || this._csvContentDataTable.Rows[i]["Name"].ToString() == string.Empty))
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Name"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.emptyContent);
                                                this._errorlog.Add(string.Format("The {0} row of the Name column cannot be empty.", i + 1));
                                                result = false;
                                            }
                                        }
                                    }
                                    //Determines whether the length of the string conforms to the rule
                                    else if (this._csvContentDataTable.Rows[i]["Name"].ToString().Length > nameMaxLength)
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Name"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.characterLengthError);
                                                this._errorlog.Add(string.Format("The {0} rows of the Name column must be longer than {1} characters.", i + 1, nameMaxLength));
                                                result = false;
                                            }
                                        }
                                    }

                                    try
                                    {
                                        number = Convert.ToInt32(this._csvContentDataTable.Rows[i]["Age"]);
                                        //Judge whether the value is within the range
                                        if (number < 0 || number > 100)
                                        {
                                            for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                            {
                                                if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Age"))
                                                {
                                                    this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.contentError);
                                                    this._errorlog.Add(string.Format("The {0} row in column a must be greater than 0 and less than 100.", i + 1));
                                                    result = false;
                                                }
                                            }
                                        }
                                    }
                                    //The exception is raised when the value is null
                                    catch (InvalidCastException)
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Age"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.filedTypeError);
                                                this._errorlog.Add(string.Format("The {0} row of the Age column cannot be empty.", i + 1));
                                                result = false;
                                            }
                                        }
                                    }
                                    //This exception is raised when format conversion fails
                                    catch (FormatException)
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Age"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.filedTypeError);
                                                this._errorlog.Add(string.Format("The {0} row of column a must be a number.", i + 1));
                                                result = false;
                                            }
                                        }
                                    }

                                    //Check if there is a column name 'Sex'
                                    if (this._csvContentDataTable.Rows[i]["Sex"].ToString().Trim() == string.Empty)
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Sex"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.emptyContent);
                                                this._errorlog.Add(string.Format("The {0} row of the sex column cannot be empty.", i + 1));
                                                result = false;
                                            }
                                        }
                                    }
                                    //Check whether the value is mae or fmale
                                    else if (this._csvContentDataTable.Rows[i]["Sex"].ToString().ToLower() != "female" && this._csvContentDataTable.Rows[i]["Sex"].ToString().ToLower() != "male")
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Sex"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.contentError);
                                                this._errorlog.Add(string.Format("The {0} row of the sex column must be either male or female.", i + 1));
                                                result = false;
                                            }
                                        }
                                    }
                                }

                                return result;
                            }
                            else
                            {
                                //Get missing columns
                                lackColumnList = allColumn.Where(a => !fileAllColumn.Exists(f => a.Contains(f))).ToList();

                                for (int i = 0; i < lackColumnList.Count; i++)
                                {
                                    this._errorlog.Add(string.Format("The necessary {0} column is missing.", lackColumnList[i].ToString()) + "\r\n");
                                }

                                return false;
                            }
                        }
                        else
                        {
                            //Get the columns that do not match the data table fields
                            notMatchColumnsList = fileAllColumn.Except(allColumn).ToList();
                            notMatchColumns = new StringBuilder();

                            for (int i = 0; i < notMatchColumnsList.Count; i++)
                            {
                                this._errorlog.Add(string.Format("A column name that does not match the specified column name {0}.", notMatchColumnsList[i].ToString()) + "\r\n");
                            }

                            return false;
                        }
                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    return result;
                }
            }
            else
            {
                this._errorlog.Add("The number of columns in the file cannot be less than three and more than six." + "\r\n");

                return false;
            }
        }
    }

}