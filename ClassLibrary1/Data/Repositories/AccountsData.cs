using ClassLibrary1.Business.Entities;
using ClassLibrary1.Data.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data.Repositories
{
    internal class AccountsData : SqlServer
    {

        private const string tableName = "user_login";
        public AccountsData() : base(tableName)
        {

        }

        public SelectResult SelectCheckEmail(string email)
        {

            string query = $"select * from user_login where email = @Mail";

            using (SqlCommand command = new SqlCommand(query))
            {
                command.Parameters.AddWithValue("@Mail", email);
                return base.SelectOnlyOne(command);
            }


        }

        public SelectResult GetName(string email)
        {
            string query = $"select name from user_login where email = @Mail";
            
            using (SqlCommand command = new SqlCommand(query))
            {
                command.Parameters.AddWithValue("@Mail", email);
                return base.SelectOnlyOne(command);   
            }

        }

        public SelectResult CheckLogin(string email, string password)
        {
            var result = new SelectResult();
            string query = $"select email, password from user_login where email = @Email;";

            using (SqlCommand selectCommand = new SqlCommand(query))
            {
                selectCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;

                result = base.SelectOnlyOne(selectCommand);

                if (result.Rows == 1)
                {
                    string hashedPasswordFromDatabase = result.DataTable.Rows[0]["password"].ToString();

                   
                    bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, hashedPasswordFromDatabase);

                    if (passwordMatch)
                    {
                        result.Succeeded = true;
                    }
                    else
                    {
                        result.AddError("Invalid password");
                    }
                }
                else
                {
                    result.AddError("User not found");
                }
            }

            return result;
        }


        public InsertResult insertRegistration(Account account)
        {
            StringBuilder insertQuery = new StringBuilder();

            insertQuery.Append($"Insert INTO {tableName} ");
            insertQuery.Append($"(email,name,password) Values");
            insertQuery.Append($"(@Email,@Name,@Password); ");

            using (SqlCommand insertCommand = new SqlCommand(insertQuery.ToString()))
            {
                insertCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = account.email;
                insertCommand.Parameters.Add("@Name", SqlDbType.VarChar).Value = account.name;

                
                insertCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = account.password;

                return base.Insert(insertCommand);
            }
        }





        public InsertResult updatePassword(string email, string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            string updateQuery = "UPDATE user_login SET password = @Password WHERE email = @Email";

            using (SqlCommand command = new SqlCommand(updateQuery))
            {
            command.Parameters.AddWithValue("@Password", hashedPassword);
            command.Parameters.AddWithValue("@Email", email);

            return base.Insert(command);
            }
        }





}
}
