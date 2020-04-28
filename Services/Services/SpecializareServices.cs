using Services.Appsetings;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Services.Services
{
    public class SpecializareServices : BasicService

    {
        public Specializare GetSpecializare(int Id)
        {


            var newSpecializare = new Specializare();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select * from specializari where Id=@Id";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            newSpecializare.Id = Int32.Parse(dr["Id"].ToString());
                            newSpecializare.Nume = dr["Nume"].ToString();
                        }
                    }
                }


                connection.Close();
                return newSpecializare;
            }
        }
        public List<Specializare> GetAll()
        {
            var listaSpecializari = new List<Specializare>();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select * from specializari";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            var newSpecializare = new Specializare
                            {
                                Id = Int32.Parse(dr["Id"].ToString()),
                                Nume = dr["Nume"].ToString(),
                            };

                            listaSpecializari.Add(newSpecializare);
                        }

                    }
                }
                connection.Close();
                return listaSpecializari;


            }

        }

        public void Insert(Specializare specializare)
        {
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Insert into specializari values(@Nume)";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Nume", specializare.Nume);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Specializare specializare)
        {
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Update specializari set Nume=@Nume where Id=@Id";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Nume", specializare.Nume);
                    command.Parameters.AddWithValue("@Id", specializare.Id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Delete from specializari where Id=@ID";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@ID", Id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Servicii> TakeServicesBySpecializareId(int Id)
        {
            var listaServicii = new List<Servicii>();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select Nume,pret from servicii as s join specializari_servicii as ss on s.Id=ss.Id_serviciu where ss.Id_specializare=@Id ";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            var newServiciu = new Servicii
                            {
                                pret = Int32.Parse(dr["pret"].ToString()),
                                Nume = dr["Nume"].ToString(),
                            };

                            listaServicii.Add(newServiciu);
                        }

                    }
                }
                connection.Close();
                return listaServicii;
            }


        }
    }
}
