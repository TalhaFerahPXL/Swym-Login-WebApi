using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data.Framework
{
     abstract class SqlServer
    {
        public string TableName { get; set; }

        public string TesttableName = "user_login";

        SqlConnection connection;
        SqlDataAdapter adapter;

        protected BaseResult BaseResult { get; set; }
        public SqlServer(string tableName)
        {
            TableName = tableName;
            connection = new SqlConnection(Settings.GetConnectionString());
        }



        public SelectResult SelectOnlyOne(SqlCommand selectCommand)
        {
            DataTable dataTable = new DataTable();

            var result = new SelectResult();
            try
            {
                using (var conn = connection)
                {
                    conn.Open();
                    selectCommand.Connection = conn;

                    using (adapter = new SqlDataAdapter(selectCommand))
                    {
                        adapter.Fill(dataTable);
                        result.DataTable = dataTable;
                        result.Rows = dataTable.Rows.Count;
                        if (dataTable.Rows.Count >= 1)
                        {
                            result.Succeeded = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }


        public InsertResult Insert(SqlCommand insertCommand)
        {
            DataTable dataTable = new DataTable();
            InsertResult result = new InsertResult();
            try
            {
                using (var con = connection)
                {
                    con.Open();
                    insertCommand.Connection = con;


                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected != 0)
                    {
                        result.Succeeded = true;
                    }
                    else
                    {
                        result.AddError("werkt niet");
                    }

                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }





    }
}
