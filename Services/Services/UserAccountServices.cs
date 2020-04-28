using Services.Appsetings;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Services.Services
{
	public class UserAccountServices
	{

		public User Login(string email,string password)
		{
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select * from users where Email=@Email and Password=@Password";
                var newUser = new User();

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            newUser.FirstName = dr["FirstName"].ToString();
                            newUser.LastName = dr["LastName"].ToString();
                            newUser.Email = dr["Email"].ToString();
                            newUser.Password = dr["Password"].ToString();
                        }
                       

                    }
                }
                connection.Close();
                return newUser;

            }
        }
        public User Get(string email)
        {
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select * from users where Email=@Email ";
                var newUser = new User();

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                   
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            newUser.Id = Int32.Parse(dr["Id"].ToString());
                            newUser.FirstName = dr["FirstName"].ToString();
                            newUser.LastName = dr["LastName"].ToString();
                            newUser.Email = dr["Email"].ToString();
                            newUser.Password = dr["Password"].ToString();
                        }
                    }
                }
                connection.Close();
                return newUser;

            }
        }
    }
}
