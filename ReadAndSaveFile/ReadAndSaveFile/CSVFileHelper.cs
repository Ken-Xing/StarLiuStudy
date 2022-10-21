using DbBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
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
        private List<int> _errorColumn = new List<int>();
        private List<int> _errorRow = new List<int>();
        private List<string> _errorMessage = new List<string>();
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

     
        public List<int> ErrorColumn
        {
            get
            {
                return this._errorColumn;
            }
        }

      
        public List<int> ErrorRow
        {
            get
            {
                return this._errorRow;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public List<string> ErrorMessage
        {
            get
            {
                return this._errorMessage;
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

        /// <summary>
        /// Check if the contents of the file are duplicated
        /// </summary>
        /// <param name="dataTable">The contents of a file</param>
        /// <returns>Returns true if the contents of the file are repeated and false if they are not</returns>
        public bool CheckCSVDataIsRepeated()
        {
            List<string> list = new List<string>();

            for (int i = 0; i < this._csvContentDataTable.Rows.Count; i++)
            {
                list.Add(this._csvContentDataTable.Rows[i]["Snumber"].ToString());
            }

            //Determine if the content is duplicate
            if (list.Distinct().Count() == list.Count)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the repeated and not repeated Data
        /// </summary>
        /// <param name="this._csvContentDataTable">The contents of a file</param>
        /// <param name="sql">Sql command statement</param>
        public void GetReapeatedAndNotRepeatedData(string sql, string connStr)
        {
            SqlParameter sqlParameter = new SqlParameter();
            DbBase.DbHelper dbhelper = new DbBase.DbHelper(connStr);
            bool isRepeated = false;
            List<string> list = new List<string>();
            //The structure of the clone table
            this._notDuplicateDataTable = this._csvContentDataTable.Clone();
            this._duplicateDataTable = this._csvContentDataTable.Clone();
            StringBuilder stringBuilder = new StringBuilder();

            //The loop gets repeated datat
            for (int i = 0; i < this._csvContentDataTable.Rows.Count; i++)
            {
                sqlParameter = new SqlParameter("@Snumber", this._csvContentDataTable.Rows[i]["Snumber"]);
                //Results are obtained for whether the data is duplicated
                isRepeated = dbhelper.CheckDataIsExists(sql, sqlParameter);


                if (isRepeated)
                {
                    this._duplicateDataTable.Rows.Add(this._csvContentDataTable.Rows[i].ItemArray);
                }
                else
                {
                    this._notDuplicateDataTable.Rows.Add(this._csvContentDataTable.Rows[i].ItemArray);
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
        /// 
        /// </summary>
        /// <param name="errorColumn"> The location of the error column</param>
        /// <param name="errorRow"> Location of the error line</param>
        /// <param name="errorMessage"> Error message</param>
        private void ErrorDatalInformation(int errorColumn, int errorRow, string errorMessage)
        {
            this._errorRow.Add(errorRow);
            this._errorColumn.Add(errorColumn);
            this._errorMessage.Add(errorMessage);
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
                                                this.ErrorDatalInformation(j, i, String.Format("The {0} row of the email column cannot be empty", j + 1));
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
                                                this.ErrorDatalInformation(j, i, String.Format("The {0} line of the email column must be longer than {1} characters", j + 1, emailMaxLength));
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
                                                this.ErrorDatalInformation(j, i, String.Format("The content in the {0} row of the email column does not conform to the email rules", j + 1));
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
                                                this.ErrorDatalInformation(j, i, String.Format("The {0} row of the Name column cannot be empty", i + 1));
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
                                                this.ErrorDatalInformation(j, i, String.Format("The {0} line of the Name column must be longer than {2} characters", +1, nameMaxLength));
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
                                                this.ErrorDatalInformation(j, i, String.Format("The {0} row of the Age column cannot be empty", +1));
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
                                                this.ErrorDatalInformation(j, i, String.Format("The {0} and fifth Age must be numbers", i + 1));
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
                                                this.ErrorDatalInformation(i, j, String.Format("The {0} row of the sex column cannot be empty", j + 1));
                                                result = false;
                                            }
                                            else if (this._csvContentDataTable.Rows[j]["Sex"].ToString().ToLower() != "female" && this._csvContentDataTable.Rows[j]["Sex"].ToString().ToLower() != "male")
                                            {
                                                this.ErrorDatalInformation(i, j, String.Format("The {0} row of the sex column must be either male or female", j + 1));
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