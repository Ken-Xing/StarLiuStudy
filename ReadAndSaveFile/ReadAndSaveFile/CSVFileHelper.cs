using DbBase;
using ReadAndSaveFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ReadAndSaveCSVFile
{
    public class CSVFileHelper
    {
        #region member
        private DataTable _csvContentDataTable = null;
        private DataTable _duplicateDataTable = null;
        private DataTable _notDuplicateDataTable = null;
        private DataTable _possibleDuplicateDataTable = null;

        List<ErrorCellInformation> _errorCellInformationList = new List<ErrorCellInformation>();
        #endregion


        /// <summary>
        /// 
        /// </summary>
        public DataTable DuplicateDataTable
        {
            get
            {
                return this._duplicateDataTable;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable NotDuplicateDataTable
        {
            get
            {
                return this._notDuplicateDataTable;
            }
        }

        public DataTable PossibleDuplicateDataTable
        {
            get
            {
                return this._possibleDuplicateDataTable;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable CsvContentDataTable
        {
            get
            {
                return this._csvContentDataTable;
            }
        }

        public List<ErrorCellInformation> ErrorCellInformationList
        {
            get
            {
                return this._errorCellInformationList;
            }
        }

        /// <summary>
        /// Gets the contents of the file and converts them to a DataTable
        /// </summary>
        /// <param name="filePath">The path to the file</param>
        /// <returns>datatable is returned when the file's data is retrieved successfully</returns>
        public void GetFileDataToDataTable(string filepath)
        {
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
            catch (IOException)
            {
                throw new ReadAndSaveFileException("The file is occupied by another program");
            }
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
        }

        /// <summary>
        /// Destroying member _cSvContentDataTable
        /// </summary>
        public void DestroyCSVContentDataTable()
        {
            this._csvContentDataTable = null;
        }

        public void DestroyErrorCellInformation()
        {
            this._errorCellInformationList.Clear();
        }

        /// <summary>
        /// Check if the contents of the file are duplicated
        /// </summary>
        /// <param name="dataTable">The contents of a file</param>
        /// <returns>Returns true if the contents of the file are repeated and false if they are not</returns>
        public bool CheckCSVDataIsDuplicated()
        {
            List<string> columnAllContent = new List<string>();
            List<string> duplicateContentList = new List<string>();
            int columnIndex = 0;
            int time = 0;
            bool isFirst = true;
            int FirstMatch = 0;

            for (int i = 0; i < this._csvContentDataTable.Rows.Count; i++)
            {
                columnAllContent.Add(this._csvContentDataTable.Rows[i]["Email"].ToString());
            }

            //Determine if the content is duplicate
            if (columnAllContent.Distinct().Count() == columnAllContent.Count)
            {
                return true;
            }
            else
            {
                //Get duplicates in the same column
                duplicateContentList = columnAllContent.GroupBy(x => x).Where(x => x.Count() > 1).Select(x => x.Key).ToList();

                //Gets the position of the duplicate column in the datatable
                for (int i = 0; i < this._csvContentDataTable.Columns.Count; i++)
                {
                    if (this._csvContentDataTable.Columns[i].ColumnName.Equals("Email"))
                    {
                        columnIndex = i;
                    }
                }

                //Get the location of the duplicate content
                for (int i = 0; i < this._csvContentDataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < duplicateContentList.Count; j++)
                    {
                        if (this._csvContentDataTable.Rows[i]["Email"].Equals(duplicateContentList[j].ToString()))
                        {

                            if (time != j)
                            {
                                time = j;
                                isFirst = true;
                            }

                            if (time == j)
                            {
                                //Gets the position of the first matched element
                                if (isFirst)
                                {
                                    FirstMatch = i;
                                    isFirst = false;
                                }
                                //Not the first match
                                else
                                {
                                    this.AddErrorCellInformation(columnIndex, i, ErrorCellInformation._errorTypeEnum.duplicateContent, string.Format("The {0} row of the Email column repeats the {1} row", i + 1, FirstMatch + 1));
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the Duplicate and not Duplicate Data
        /// </summary>
        /// <param name="this._csvContentDataTable">The contents of a file</param>
        /// <param name="findDuplicateDataSql">Sql command statement</param>
        public void SiftFileContents(string findDuplicateDataSql,string finsPossibleDuplicateDataSql, string connStr)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            DbBase.DbHelper dbhelper = new DbBase.DbHelper(connStr);
            List<string> list = new List<string>();
            //The structure of the clone table

            this._duplicateDataTable = this._csvContentDataTable.Clone();
            this._possibleDuplicateDataTable = this._csvContentDataTable.Clone();

            StringBuilder stringBuilder = new StringBuilder();
            int columnIndex = 0;


            for (int i = 0; i < this._csvContentDataTable.Columns.Count; i++)
            {
                if (this._csvContentDataTable.Columns[i].ColumnName.Equals("Email"))
                {
                    columnIndex = i;
                }
            }
            //The loop gets repeated datat
            for (int i = 0; i < this._csvContentDataTable.Rows.Count; i++)
            {
                sqlParameterList.Add(new SqlParameter("@Email", this._csvContentDataTable.Rows[i]["Email"]));
                //Results are obtained for whether the data is duplicated
                if (dbhelper.CheckDataIsExists(findDuplicateDataSql, sqlParameterList))
                {
                    this._duplicateDataTable.Rows.Add(this._csvContentDataTable.Rows[i].ItemArray);
                }
                else
                {
                    this._possibleDuplicateDataTable.Rows.Add(this._csvContentDataTable.Rows[i].ItemArray);
                }

                sqlParameterList.Clear();
            }

            if (this._possibleDuplicateDataTable.Rows.Count > 0)
            {
                
                this._notDuplicateDataTable = this._possibleDuplicateDataTable.Clone();

                for (int i = 0; i < this._possibleDuplicateDataTable.Rows.Count; i++)
                {
                    sqlParameterList.Add(new SqlParameter("@Name", this._possibleDuplicateDataTable.Rows[i]["Name"]));
                    sqlParameterList.Add(new SqlParameter("@Age", this._possibleDuplicateDataTable.Rows[i]["Age"]));
                    sqlParameterList.Add(new SqlParameter("@Sex", this._possibleDuplicateDataTable.Rows[i]["Sex"]));

                    if (!dbhelper.CheckDataIsExists(finsPossibleDuplicateDataSql,sqlParameterList))
                    {
                        this._notDuplicateDataTable.Rows.Add(this._possibleDuplicateDataTable.Rows[i].ItemArray);
                        this._possibleDuplicateDataTable.Rows.Remove(this._possibleDuplicateDataTable.Rows[i]);
                        i--;
                    }
                    sqlParameterList.Clear();
                }
            }
        }





        public bool SaveAndUpdateData(string connStr, string sql, DataTable duplicateDataTable, DataTable notDuplicateTable, string targetTable)
        {
            bool result = false;
            DbHelper dbHelper = new DbHelper(connStr);
            dbHelper.OpenDbConnection();
            SqlTransaction sqlTransaction = dbHelper.SqlCon.BeginTransaction();
            List<SqlParameter> sqlParmeter = null;

            try
            {
                //Save data
                dbHelper.SaveBulkNotDuplicateData(notDuplicateTable, targetTable);

                for (int i = 0; i < duplicateDataTable.Rows.Count; i++)
                {
                    sqlParmeter = new List<SqlParameter>
                    {
                      new SqlParameter("@Name", duplicateDataTable.Rows[i]["Name"]),
                      new SqlParameter("@Age", duplicateDataTable.Rows[i]["Age"]),
                      new SqlParameter("@Snumber", duplicateDataTable.Rows[i]["Snumber"]),


                    };

                    //Update data
                    result = dbHelper.SaveModifyDeleteData(sql, sqlParmeter);
                    sqlParmeter.Clear();

                    if (result == false)
                    {
                        throw new Exception();
                    }
                }

                //Commit the transaction
                sqlTransaction.Commit();
            }
            catch
            {
                //Rollback the transaction
                sqlTransaction.Rollback();
                throw;
            }

            return true;
        }

        /// <summary>
        /// Details of the error cell
        /// </summary>
        /// <param name="errorColumn"> The location of the error column</param>
        /// <param name="errorRow"> Location of the error line</param>
        /// <param name="errorMessage"> Error message</param>
        private void AddErrorCellInformation(int errorColumn, int errorRow, ErrorCellInformation._errorTypeEnum errorTypeNumber, string errorMessage)
        {
            ErrorCellInformation errorCellInformation = new ErrorCellInformation();
            errorCellInformation.ErrorColumn = errorColumn;
            errorCellInformation.ErrorRow = errorRow;
            errorCellInformation.ErrorMessage = errorMessage;
            errorCellInformation.ErrorType = errorTypeNumber;

            this._errorCellInformationList.Add(errorCellInformation);

            errorCellInformation.CompareTo(errorCellInformation);

        }

        /// <summary>
        /// Checks for a match with the data structure of the data table
        /// </summary>
        /// <param name="nameMaxLength">name MaxLength</param>
        /// <param name="emailMaxLength">email MaxLength</param>
        /// <returns>Returns true if the match is successful and false otherwise</returns>
        /// <exception cref="ReadAndSaveFileException"></exception>
        public bool CheckIsMatch(int nameMaxLength, int emailMaxLength)
        {

            bool result = true;
            //All columns of the csv file
            List<string> fileAllColumn = new List<string>();
            //The required column names
            List<string> requireColumn = new List<string>();
            //All column names
            List<string> allColumn = new List<string>();
            allColumn.Add("Name");
            allColumn.Add("Age");
            allColumn.Add("Email");
            allColumn.Add("Sex");
            requireColumn.Add("Name");
            requireColumn.Add("Age");
            requireColumn.Add("Email");
            Regex emailRegex = new Regex("^([a-z0-9A-Z]+[-|\\.]?)+[a-z0-9A-Z]@([a-z0-9A-Z]+(-[a-z0-9A-Z]+)?\\.)+[a-zA-Z]{2,}$");
            int number = 0;
            string name = string.Empty;
            List<string> notMatchColumnsList = null;
            StringBuilder notMatchColumns = null;

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
                            if (!requireColumn.Except(fileAllColumn).Any())
                            {
                                //Judge whether the required contents comply with the rules
                                for (int i = 0; i < this._csvContentDataTable.Rows.Count; i++)
                                {
                                    if ((this._csvContentDataTable.Rows[i]["Email"].ToString().Length > 0 && this._csvContentDataTable.Rows[i]["Email"].ToString().Trim() == string.Empty) || this._csvContentDataTable.Rows[i]["Email"].ToString() == string.Empty)
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Email"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.emptyContent, string.Format("The {0} row of the email column cannot be empty", j + 1));
                                                result = false;
                                            }
                                        }
                                    }
                                    else if (this._csvContentDataTable.Rows[i]["Email"].ToString().Length > emailMaxLength)
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Eamil"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.characterLengthError, string.Format("The {0} line of the email column must be longer than {1} characters", j + 1, emailMaxLength));
                                                result = false;
                                            }
                                        }

                                    }
                                    else if (!emailRegex.IsMatch(this._csvContentDataTable.Rows[i]["Email"].ToString()))
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Email"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.contentError, string.Format("The content in the {0} row of the email column does not conform to the email rules", j + 1));
                                                result = false;
                                            }
                                        }
                                    }


                                    if ((this._csvContentDataTable.Rows[i]["Name"].ToString().Length > 0 && this._csvContentDataTable.Rows[i]["Name"].ToString().Trim() == string.Empty) || this._csvContentDataTable.Rows[i]["Name"].ToString() == string.Empty)
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Name"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.emptyContent, string.Format("The {0} row of the Name column cannot be empty", i + 1));
                                                result = false;
                                            }
                                        }
                                    }
                                    else if (this._csvContentDataTable.Rows[i]["Name"].ToString().Length > nameMaxLength)
                                    {

                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Name"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.characterLengthError, string.Format("The {0} line of the Name column must be longer than {2} characters", +1, nameMaxLength));
                                                result = false;
                                            }
                                        }
                                    }
                                    try
                                    {
                                        number = Convert.ToInt32(this._csvContentDataTable.Rows[i]["Age"]);
                                    }
                                    catch (InvalidCastException)
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Age"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.filedTypeError, string.Format("The {0} row of the Age column cannot be empty", +1));
                                                result = false;
                                            }
                                        }
                                    }
                                    catch (FormatException)
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Columns.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Columns[j].ColumnName.Equals("Age"))
                                            {
                                                this.AddErrorCellInformation(j, i, ErrorCellInformation._errorTypeEnum.filedTypeError, string.Format("The {0} and fifth Age must be numbers", i + 1));
                                                result = false;
                                            }
                                        }
                                    }
                                }
                                //Judge whether the contents of non mandatory columns comply with the rules
                                for (int i = 0; i < this._csvContentDataTable.Columns.Count; i++)
                                {
                                    //Check if there is a column name 'Sex'
                                    if (this._csvContentDataTable.Columns[i].ColumnName.Equals("Sex"))
                                    {
                                        for (int j = 0; j < this._csvContentDataTable.Rows.Count; j++)
                                        {
                                            if (this._csvContentDataTable.Rows[j]["Sex"].ToString().Trim() == string.Empty)
                                            {
                                                this.AddErrorCellInformation(i, j, ErrorCellInformation._errorTypeEnum.emptyContent, string.Format("The {0} row of the sex column cannot be empty", j + 1));
                                                result = false;
                                            }
                                            else if (this._csvContentDataTable.Rows[j]["Sex"].ToString().ToLower() != "female" && this._csvContentDataTable.Rows[j]["Sex"].ToString().ToLower() != "male")
                                            {
                                                this.AddErrorCellInformation(i, j, ErrorCellInformation._errorTypeEnum.contentError, string.Format("The {0} row of the sex column must be either male or female", j + 1));
                                                result = false;
                                            }
                                        }
                                    }
                                }
                                return result;
                            }
                            else
                            {
                                throw new ReadAndSaveFileException("Missing necessary columns");
                            }
                        }
                        else
                        {
                            //Get the columns that do not match the data table fields
                            notMatchColumnsList = fileAllColumn.Except(allColumn).ToList();
                            notMatchColumns = new StringBuilder();

                            for (int i = 0; i < notMatchColumnsList.Count; i++)
                            {
                                if (notMatchColumnsList.Count > i + 1)
                                {
                                    notMatchColumns.Append(notMatchColumnsList[i].ToString() + ",");
                                }
                                else if (notMatchColumnsList.Count == i + 1)
                                {
                                    notMatchColumns.Append(notMatchColumnsList[i].ToString() + "");
                                }
                            }

                            throw new ReadAndSaveFileException(String.Format("A column name that does not match the specified column name {0}", notMatchColumns.ToString()));
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
                throw new ReadAndSaveFileException("The number of columns in the file cannot be less than three and more than six");
            }
        }
    }
}