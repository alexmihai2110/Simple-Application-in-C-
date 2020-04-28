using Services.Appsetings;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Services.Services
{
    public class ServiciiServices : BasicService
    {
        public Servicii GetServiciu(int Id)
        {


            var newServiciu = new Servicii();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select * from servicii where Id=@Id";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            newServiciu.Id = Int32.Parse(dr["Id"].ToString());
                            newServiciu.Nume = dr["Nume"].ToString();
                            newServiciu.pret = Int32.Parse(dr["pret"].ToString());
                        }
                    }
                }


                connection.Close();
                return newServiciu;
            }
        }
        public List<Servicii> GetAll()
        {
            var listaServicii = new List<Servicii>();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select * from servicii";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            var newServiciu = new Servicii
                            {
                                Id = Int32.Parse(dr["Id"].ToString()),
                                Nume = dr["Nume"].ToString(),
                                pret= Int32.Parse(dr["pret"].ToString())
                            };

                            listaServicii.Add(newServiciu);
                        }

                    }
                }
                connection.Close();
                return listaServicii;


            }

        }

        public void Insert(Servicii serviciu)
        {
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Insert into servicii values(@Nume,@pret)";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Nume", serviciu.Nume);
                    command.Parameters.AddWithValue("@pret", serviciu.pret);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Servicii serviciu)
        {
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Update servicii set Nume=@Nume, pret=@pret where Id=@Id";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Nume", serviciu.Nume);
                    command.Parameters.AddWithValue("@Id", serviciu.Id);
                    command.Parameters.AddWithValue("@pret", serviciu.pret);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Delete from servicii where Id=@ID";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@ID", Id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
