using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DbBase
{
    public class DbHelper
    {
        #region Member
        private string _connStr = string.Empty;
        private SqlConnection _sqlCon = null;
        #endregion

        /// <summary>
        /// Instantiating Sqlconnection object
        /// </summary>
        public string ConnStr
        {
            get
            {
                return this._connStr;
            }
            set
            {
                this._connStr = value;
                this._sqlCon = new SqlConnection(value);
            }
        }

        /// <summary>
        /// Read-only this._sqlCon
        /// </summary>
        public SqlConnection SqlCon
        {
            get
            {
                return this._sqlCon;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbHelper"/> class.
        /// </summary>
        /// <param name="connStr">Connection databse string</param>
        public DbHelper(string connStr)
        {
            this._connStr = connStr;
            this._sqlCon = new SqlConnection(connStr);
        }

        /// <summary>
        /// Open DataBase Connection
        /// </summary>
        public void OpenDbConnection()
        {
            //If the connection object is null
            if (this._sqlCon == null)
            {
                this._sqlCon = new SqlConnection(this._connStr);
                this._sqlCon.Open();
            }
            //If the connectionSate is close
            else if (this._sqlCon.State == ConnectionState.Closed)
            {
                this._sqlCon.Open();
            }
            //If the connectionSate is broken
            else if (this._sqlCon.State == ConnectionState.Broken)
            {
                this._sqlCon.Close();
                this._sqlCon.Open();
            }
        }

        /// <summary>
        /// Close DataBase Connection
        /// </summary>
        public void CloseDbConnection()
        {
            if (this._sqlCon != null)
            {
                if (this._sqlCon.State != ConnectionState.Closed)
                {
                    this._sqlCon.Close();
                }
            }

        }

        /// <summary>
        /// Save, modify, delete data
        /// </summary>
        /// <param name="sql">Sql command statement</param>
        /// <param name="sqlParameterList">Provide arguments for sqlcommand</param>
        /// <returns>Returns true if the opreate was successful, false otherwise</returns>
        public bool SaveModifyDeleteData(string sql, List<SqlParameter> sqlParameterList)
        {
            SqlCommand sqlCommand = new SqlCommand(sql, this._sqlCon);
            sqlCommand.Parameters.AddRange(sqlParameterList.ToArray());

            try
            {   // Return true or false
                return sqlCommand.ExecuteNonQuery() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// Use transactions to save, modify, and delete data
        /// </summary>
        /// <param name="sql">Sql command statement</param>
        /// <param name="sqlParameterList">Provide arguments for sqlcommand</param>
        /// <param name="sqlTransaction">Used to manipulate database transactions</param>
        /// <param name="sqlCommand">An object that operates on the database</param>
        /// <returns></returns>
        public bool SaveModifyDeleteData(string sql, List<SqlParameter> sqlParameterList, SqlTransaction sqlTransaction, SqlCommand sqlCommand)
        {
    
            sqlCommand.CommandText = sql;
            sqlCommand.Transaction = sqlTransaction;
            sqlCommand.Parameters.AddRange(sqlParameterList.ToArray());

            try
            {
                // Return true or false
                return sqlCommand.ExecuteNonQuery() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlCommand.Parameters.Clear();
            }
        }


        /// <summary>
        /// Save file contet data to database
        /// </summary>
        /// <param name="dataTable">This is a datatable of file content transformation</param>
        /// <returns>If the file content data is saved successfully, return true. If it fails, return false</returns>
        /// <exception cref="ReadAndSaveFileException">Throws an exception when the structure of the file does not conform to the rules</exception>
        public bool SaveBulkData(DataTable dataTable, string targetTable)
        {
            SqlBulkCopy sqlBulkCopy = null;

            try
            {
                sqlBulkCopy = new SqlBulkCopy(this._connStr, SqlBulkCopyOptions.FireTriggers);

                if (sqlBulkCopy != null && targetTable != string.Empty)
                {
                    //Target data table
                    sqlBulkCopy.DestinationTableName = targetTable.Trim();

                    //Map the corresponding fields in the data table
                    foreach (var column in dataTable.Columns)
                    {
                        sqlBulkCopy.ColumnMappings.Add(column.ToString(), column.ToString());
                    }

                    //Writes data to the target table
                    sqlBulkCopy.WriteToServer(dataTable);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                //Close sqlBulkCopy
                if (sqlBulkCopy != null)
                {
                    sqlBulkCopy.Close();
                }
            }
        }

        /// <summary>
        /// Check if the data exists
        /// </summary>
        /// <param name="sql">Sql command statement</param>
        /// <param name="sqlParameter">Provide arguments for sqlcommand</param>
        /// <returns>Returns true if the data is present and false otherwise</returns>
        public bool CheckDataIsExists(string sql, List<SqlParameter> sqlParameterList)
        {
            SqlCommand sqlCommand = new SqlCommand(sql, this._sqlCon);
            sqlCommand.Parameters.AddRange(sqlParameterList.ToArray());

            try
            {
                //Returns the results of the query
                return int.Parse(sqlCommand.ExecuteScalar().ToString()) > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlCommand.Parameters.Clear();
            }
        }
    }
}
