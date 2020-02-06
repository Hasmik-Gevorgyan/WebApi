using WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WebApi.DataAccess
{
    public class UserDAL
    {
        private int rowsAffected { get; set; }
        public string ResultText { get; set; }

        public void InsertUser(User user)
        {
            rowsAffected = 0;

            // Create SQL statement to submit
            string sql = "INSERT INTO [User]( ID, Name, LastName, Email, Address)";
            sql += $" VALUES( {user.ID} , '{user.Name}', '{user.LastName}', '{user.Email}', '{user.Address}')";

            try
            {
                // Create SQL connection object in using block for automatic closing and disposing
                using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
                {
                    // Create command object in using block for automatic disposal
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        // Set CommandType
                        cmd.CommandType = CommandType.Text;
                        // Open the connection
                        cnn.Open();
                        // Execute the INSERT statement
                        rowsAffected = cmd.ExecuteNonQuery();

                        ResultText = "Rows Affected: " + rowsAffected.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ResultText = ex.ToString();
            }
        }

        public int GetUsersCountScalar()
        {
            rowsAffected = 0;
            // Create SQL statement to submit
            string sql = "SELECT COUNT(*) FROM [User]";

            // Create a connection
            using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
            {
                // Create command object
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    // Open the connection
                    cnn.Open();
                    // Execute command
                    rowsAffected = (int)cmd.ExecuteScalar();
                }
            }
            ResultText = "Rows Affected: " + rowsAffected.ToString();

            return rowsAffected;
        }
         public void DeleteUser(int id)
        {
            string sql="DELETE FROM [User]] WHERE Id = '{0}'";
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection connection=new SqlConnection(AppSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(string.Format(sql,id),connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch(SqlException ex)
                    {
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                             errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                        }
                        Console.WriteLine(errorMessages.ToString());
                    }
                }
            }
            
        }

        public List<User> GetUsersAsGenericList()
        {
            List<User> Users = new List<User>();

            // Create SQL statement to submit
            string sql = "SELECT ID, Name, LastName,Email,Address FROM [User]";

            // Create a connection
            using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
            {
                // Create command object
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    // Open the connection
                    cnn.Open();

                    // Execute command to get data reader
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            Users.Add(new User
                            {
                                ID = dr.GetInt32(dr.GetOrdinal("ID")),
                                Name = dr.GetString(dr.GetOrdinal("Name")),
                                LastName = dr.GetString(dr.GetOrdinal("LastName")),
                                Email = dr.GetString(dr.GetOrdinal("Email")),
                                Address = dr.GetString(dr.GetOrdinal("Address"))
                            });
                        }
                    }
                }
            }

            return Users;
        }
        public void updateUser(User sth)
        {   
            string sql="UPDATE [User] " +
                       "SET Name = @Name, LastName = @LastName, Email = @Email,Address = @Address"+
                       "Where Id=@Id";
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(AppSettings.ConnectionString))
            {
                using (SqlCommand command= new SqlCommand(sql, connection))
                {
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();    
                        command.Parameters.Add("@ID",SqlDbType.Int).Value=sth.ID;
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value=sth.Name;
                        command.Parameters.Add("@LastName", SqlDbType.VarChar).Value=sth.LastName;
                        command.Parameters.Add("@Email", SqlDbType.VarChar).Value=sth.Email;
                        command.Parameters.Add("@Address", SqlDbType.VarChar).Value=sth.Address;
                        command.ExecuteNonQuery();
                    }
                    catch(SqlException ex)
                    {
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                             errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                        }
                        Console.WriteLine(errorMessages.ToString());
                    }
                    
                }
            }
        }
    }
}
