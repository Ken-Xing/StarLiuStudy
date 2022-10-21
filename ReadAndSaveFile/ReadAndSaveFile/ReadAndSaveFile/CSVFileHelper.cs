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

        /// <summary>
        /// Gets the contents of the file and converts them to a DataTable
        /// </summary>
        /// <param name="filePath">The path to the file</param>
        /// <returns>datatable is returned when the file's data is retrieved successfully</returns>
        public void GetFileDataToDataTable(StreamReader streamReader)
        {
            string filecontent = string.Empty;
            bool isFirst = true;
            string[] arr = null;
            DataRow dataRow = null;
            this._csvContentDataTable = new DataTable();

            try
            {
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

                                this._csvContentDataTable.Columns.Add(item.Trim());
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
                                if (i < arr.Length && arr[i] == string.Empty)
                                {
                                    dataRow[i] = DBNull.Value;
                                }
                                else if (i < arr.Length && !(this._csvContentDataTable.Columns[i].ColumnName.Equals("AdmissionDate")))
                                {

                                    dataRow[i] = arr[i];
                                }
                                else if (i < arr.Length && this._csvContentDataTable.Columns[i].ColumnName.Equals("AdmissionDate"))
                                {
                                    try
                                    {
                                        dataRow[i] = Convert.ToDateTime(arr[i].ToString()).ToString("MM-dd-yyyy");
                                    }
                                    catch (FormatException)
                                    {
                                        dataRow[i] = DBNull.Value;
                                    }
                                }
                                else
                                {
                                    dataRow[i] = DBNull.Value;
                                }
                            }

                            this._csvContentDataTable.Rows.Add(dataRow);
                        }
                    }
                }

            }
            catch (DuplicateNameException)
            {
                throw new ReadAndSaveFileException("Duplicate column names are not allowed");
            }
            catch
            {
                throw;
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
        /// <returns>If the data is duplicated, duplicate data is returned</returns>
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

        /// <summary>
        /// Checks for a match with the data structure of the data table
        /// </summary>
        /// <param name="nameMaxLength">name MaxLength</param>
        /// <param name="maximumAge">maximum Age</param>
        /// <param name="minimumAge">minimum Age</param>
        /// <param name="maximumSNumber">maximum SNumber</param>
        /// <param name="minimumSNumber">minimum SNumber</param>
        /// <param name="minimumAdmissionDate">minimum AdmissionDate</param>
        /// <param name="maximumAdmissionDate">maximum AdmissionDate</param>
        /// <returns>Returns true if the match is successful and false otherwise</returns>
        /// <exception cref="ReadAndSaveFileException"></exception>
        public bool CheckIsMatch(int nameMaxLength, int maximumAge, int minimumAge, int maximumSNumber, int minimumSNumber,int emailMaxLength,DateTime minimumAdmissionDate, DateTime maximumAdmissionDate)
        {
            //All columns of the csv file
            List<string> fileAllColumn = new List<string>();
            //The required column names
            List<string> requireColumn = new List<string>();
            //All column names
            List<string> allColumn = new List<string>();
            allColumn.Add("Name");
            allColumn.Add("Age");
            allColumn.Add("Snumber");
            allColumn.Add("AdmissionDate");
            allColumn.Add("Email");
            allColumn.Add("Sex");
            requireColumn.Add("Name");
            requireColumn.Add("Age");
            requireColumn.Add("Snumber");
            Regex emailRegex = new Regex("^([a-z0-9A-Z]+[-|\\.]?)+[a-z0-9A-Z]@([a-z0-9A-Z]+(-[a-z0-9A-Z]+)?\\.)+[a-zA-Z]{2,}$");
            DateTime admissionDate = new DateTime();
            int number = 0;
            string name = string.Empty;
            List<string> notMatchColumnsList = null;
            StringBuilder notMatchColumns = null;

            if (this._csvContentDataTable.Columns.Count >= 3 && this._csvContentDataTable.Columns.Count <= 6)
            {
                if (nameMaxLength > 0)
                {
                    if (maximumAge > minimumAge && (maximumAge > 0 && minimumAge > 0))
                    {
                        if (maximumSNumber > minimumSNumber && (maximumSNumber > 0 && minimumSNumber > 0))
                        {
                            if (emailMaxLength > 0)
                            {
                                if (maximumAdmissionDate > minimumAdmissionDate && (maximumAdmissionDate != DateTime.MinValue && minimumAdmissionDate != DateTime.MinValue))
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
                                                if (!(this._csvContentDataTable.Rows[i]["Name"].ToString().Length > 0))
                                                {
                                                    throw new ReadAndSaveFileException("The Name column cannot be empty");
                                                }
                                                else if (this._csvContentDataTable.Rows[i]["Name"].ToString().Length > nameMaxLength)
                                                {
                                                    throw new ReadAndSaveFileException("The character length of the name column exceeds the maximum allowed length");
                                                }

                                                try
                                                {
                                                    number = Convert.ToInt32(this._csvContentDataTable.Rows[i]["Age"]);
                                                }
                                                catch (InvalidCastException)
                                                {
                                                    throw new ReadAndSaveFileException("The Age column cannot be empty");
                                                }
                                                catch (FormatException)
                                                {
                                                    throw new ReadAndSaveFileException("The contents of the age column must be numeric");
                                                }

                                                if (Convert.ToInt32(this._csvContentDataTable.Rows[i]["Age"].ToString()) < minimumAge)
                                                {
                                                    throw new ReadAndSaveFileException(String.Format("The Age column cannot have a content value less than {0}", minimumAge));
                                                }
                                                else if (Convert.ToInt32(this._csvContentDataTable.Rows[i]["Age"].ToString()) > maximumAge)
                                                {
                                                    throw new ReadAndSaveFileException(String.Format("The Age column cannot have a content value that exceeds {0}", maximumAge));
                                                }

                                                //If the conversion to int fails, an exception is thrown
                                                try
                                                {
                                                    number = Convert.ToInt32(this._csvContentDataTable.Rows[i]["Snumber"]);
                                                }
                                                catch (InvalidCastException)
                                                {
                                                    throw new ReadAndSaveFileException("The Age Snumber cannot be empty");
                                                }
                                                catch (FormatException)
                                                {
                                                    throw new ReadAndSaveFileException("The contents of the Snumber column must be numeric");
                                                }
                                                if (Convert.ToInt32(this._csvContentDataTable.Rows[i]["Snumber".Trim()].ToString()) < minimumSNumber)
                                                {
                                                    throw new ReadAndSaveFileException(String.Format("The Snumber column cannot have a content value less than {0}", minimumSNumber));
                                                }
                                                else if (Convert.ToInt32(this._csvContentDataTable.Rows[i]["Snumber".Trim()].ToString()) > maximumSNumber)
                                                {
                                                    throw new ReadAndSaveFileException(String.Format("The Snumber column cannot have a content value that exceeds {0}", maximumSNumber));
                                                }
                                            }
                                            //Judge whether the contents of non mandatory columns comply with the rules
                                            for (int i = 0; i < this._csvContentDataTable.Columns.Count; i++)
                                            {
                                                //Check if there is a column named 'CreateDate'
                                                if (this._csvContentDataTable.Columns[i].ColumnName.Equals("AdmissionDate"))
                                                {
                                                    for (int j = 0; j < this._csvContentDataTable.Rows.Count; j++)
                                                    {
                                                        if (this._csvContentDataTable.Rows[j]["AdmissionDate"].ToString().Trim() != string.Empty)
                                                        {
                                                            //If the conversion to DateTime fails, an exception is thrown
                                                            try
                                                            {
                                                                admissionDate = (Convert.ToDateTime(this._csvContentDataTable.Rows[j]["AdmissionDate"].ToString()));
                                                            }
                                                            catch (FormatException)
                                                            {
                                                                throw new ReadAndSaveFileException("The content of the AdmissionDate column must be in the datetime format");
                                                            }
                                                            if (Convert.ToDateTime(this._csvContentDataTable.Rows[j]["AdmissionDate"].ToString()) < minimumAdmissionDate)
                                                            {
                                                                throw new ReadAndSaveFileException(String.Format("The AdmissionDate column cannot have a date earlier than {0}", minimumAdmissionDate.ToString("yyyy-MM-dd")));
                                                            }
                                                            else if (Convert.ToDateTime(this._csvContentDataTable.Rows[j]["AdmissionDate"].ToString()) > maximumAdmissionDate)
                                                            {
                                                                throw new ReadAndSaveFileException(string.Format("The AdmissionDate column cannot have a date later than {0}", maximumAdmissionDate.ToString("yyyy-MM-dd")));
                                                            }
                                                        }
                                                    }
                                                }

                                                //Check if there is a column named 'Sex'
                                                if (this._csvContentDataTable.Columns[i].ColumnName.Equals("Sex"))
                                                {
                                                    for (int j = 0; j < this._csvContentDataTable.Rows.Count; j++)
                                                    {
                                                        if (this._csvContentDataTable.Rows[j]["Sex"].ToString().Trim() != string.Empty && this._csvContentDataTable.Rows[j]["Sex"].ToString().ToLower() != "female" && this._csvContentDataTable.Rows[j]["Sex"].ToString().ToLower() != "male")
                                                        {
                                                            throw new ReadAndSaveFileException("The Sex column can only be female or male");
                                                        }
                                                    }
                                                }

                                                //Check if there is a column named 'Email'
                                                if (this._csvContentDataTable.Columns[i].ColumnName.Equals("Email".Trim()))
                                                {
                                                    for (int j = 0; j < this._csvContentDataTable.Rows.Count; j++)
                                                    {
                                                        if ((this._csvContentDataTable.Rows[j]["Email".Trim()].ToString() != string.Empty && (this._csvContentDataTable.Rows[j]["Email".Trim()].ToString().Length > emailMaxLength) || (this._csvContentDataTable.Rows[j]["Email".Trim()].ToString() != string.Empty && !emailRegex.IsMatch(this._csvContentDataTable.Rows[j]["Email".Trim()].ToString()))))
                                                        {
                                                            if (this._csvContentDataTable.Rows[j]["Email"].ToString().Length > emailMaxLength)
                                                            {
                                                                throw new ReadAndSaveFileException(string.Format("The character length of the name column exceeds the maximum allowed length {0}", emailMaxLength));
                                                            }
                                                            else if (!emailRegex.IsMatch(this._csvContentDataTable.Rows[j]["Email"].ToString()))
                                                            {
                                                                throw new ReadAndSaveFileException("The content of the Email column is not formatted properly");
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            return true;
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
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new ReadAndSaveFileException("The number of columns in the file cannot be less than three and more than six");
            }
        }
    }
}