using System.Data.SqlClient;
using System.Data;
using System;

namespace ReadAndSaveFile
{
    public class ReadAndSaveFileDbHelper : DbBase.DbHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadAndSaveFileDbHelper"/> class.
        /// </summary>
        /// <param name="connStr">A string to connect to the database</param>
        public ReadAndSaveFileDbHelper(string connStr) : base(connStr)
        {

        }

        /// <summary>
        /// Save and update the data
        /// </summary>
        /// <param name="updateSql">Update sql statements</param>
        /// <param name="dublicateDataTable">Duplicate data</param>
        /// <param name="notDublicateDataTable">Not dublicate data</param>
        /// <param name="targetTable">The target table</param>
        /// <returns><returns>Returns true if saving data was successful or changing data was successful or deleting data was successful/returns>
        public bool SaveAndUpdateBulkData(string updateSql, DataTable dublicateDataTable, DataTable notDublicateDataTable, string targetTable)
        {
            bool result = false;
            OpenDbConnection();
            SqlTransaction sqlTransaction = SqlCon.BeginTransaction();
            SqlCommand sqlCmd = new SqlCommand(updateSql, SqlCon);
            sqlCmd.Transaction = sqlTransaction;
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(SqlCon, SqlBulkCopyOptions.Default, sqlTransaction);
            //Target data table
            sqlBulkCopy.DestinationTableName = targetTable.Trim();

            //Map the corresponding fields in the data table
            foreach (var column in notDublicateDataTable.Columns)
            {
                sqlBulkCopy.ColumnMappings.Add(column.ToString(), column.ToString());
            }

            try
            {
                //Writes data to the target table
                sqlBulkCopy.WriteToServer(notDublicateDataTable);

                for (int i = 0; i < dublicateDataTable.Rows.Count; i++)
                {
                    sqlCmd.Parameters.Add(new SqlParameter("@Name", dublicateDataTable.Rows[i]["Name"]));
                    sqlCmd.Parameters.Add(new SqlParameter("@Age", dublicateDataTable.Rows[i]["Age"]));
                    sqlCmd.Parameters.Add(new SqlParameter("@Snumber", dublicateDataTable.Rows[i]["Snumber"]));
                    sqlCmd.Parameters.Add(new SqlParameter("@Sex", DBNull.Value));
                    sqlCmd.Parameters.Add(new SqlParameter("@Email", DBNull.Value));
                    sqlCmd.Parameters.Add(new SqlParameter("@AdmissionDate", DBNull.Value));

                    for (int j = 0; j < dublicateDataTable.Columns.Count; j++)
                    {
                        if (dublicateDataTable.Columns[j].ColumnName.Equals("Sex"))
                        {
                            sqlCmd.Parameters.RemoveAt("@Sex");
                            sqlCmd.Parameters.Add(new SqlParameter("@Sex", dublicateDataTable.Rows[i]["Sex"]));
                        }

                        if (dublicateDataTable.Columns[j].ColumnName.Equals("Email"))
                        {
                            sqlCmd.Parameters.RemoveAt("@Email");
                            sqlCmd.Parameters.Add(new SqlParameter("@Email", dublicateDataTable.Rows[i]["Email"]));
                        }

                        if (dublicateDataTable.Columns[j].ColumnName.Equals("AdmissionDate"))
                        {
                            sqlCmd.Parameters.RemoveAt("@AdmissionDate");
                            sqlCmd.Parameters.Add(new SqlParameter("@AdmissionDate", dublicateDataTable.Rows[i]["AdmissionDate"]));
                        }

                    }

                    //A loop updates duplicate data
                    result = sqlCmd.ExecuteNonQuery() > 0;
                    //Remove parameters
                    sqlCmd.Parameters.Clear();

                    if (result == false)
                    {
                        return false;
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
            finally
            { 
        
                if (sqlBulkCopy != null)
                {
                    sqlBulkCopy.Close();
                }
                //Forcibly close the Database connection
                CloseDbConnection();
            }

            return true;
        }

    }
}
