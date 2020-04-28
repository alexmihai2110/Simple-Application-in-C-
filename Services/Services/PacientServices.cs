using Services.Appsetings;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Services.Services
{
    public class PacientServices:BasicService
    {
        public Pacient GetPacient(int Id)
        {


            var newPacient = new Pacient();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select * from pacienti where Id=@Id";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            newPacient.Id = Int32.Parse(dr["Id"].ToString());
                            newPacient.Nume = dr["Nume"].ToString();
                            newPacient.Prenume = dr["Prenume"].ToString();
                            newPacient.CNP = dr["CNP"].ToString();
                            newPacient.Data_Nasterii = DateTime.Parse(dr["Data_Nasterii"].ToString());
                        }
                    }
                }


                connection.Close();
                return newPacient;
            }
        }
        public List<Pacient> GetAll()
        {
            var listaPacienti = new List<Pacient>();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select * from pacienti";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            var newPacient = new Pacient
                            {
                                Id = Int32.Parse(dr["Id"].ToString()),
                                Nume = dr["Nume"].ToString(),
                                Prenume=dr["Prenume"].ToString(),
                                CNP=dr["CNP"].ToString(),
                                Data_Nasterii=DateTime.Parse(dr["Data_Nasterii"].ToString())
                            };

                            listaPacienti.Add(newPacient);
                        }

                    }
                }
                connection.Close();
                return listaPacienti;


            }

        }

        public void Insert(Pacient pacient)
        {
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Insert into pacienti values(@Nume,@Prenume,@Data_Nasterii,@CNP)";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Nume", pacient.Nume);
                    command.Parameters.AddWithValue("@Prenume", pacient.Prenume);
                    command.Parameters.AddWithValue("@Data_Nasterii", pacient.Data_Nasterii);
                    command.Parameters.AddWithValue("@CNP", pacient.CNP);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Pacient pacient)
        {
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Update pacienti set Nume=@Nume, Prenume=@Prenume,Data_Nasterii=@Data_nasterii, CNP=@CNP where Id=@Id";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Nume", pacient.Nume);
                    command.Parameters.AddWithValue("@Id", pacient.Id);
                    command.Parameters.AddWithValue("@Data_Nasterii", pacient.Data_Nasterii);
                    command.Parameters.AddWithValue("@Prenume", pacient.Prenume);
                    command.Parameters.AddWithValue("@CNP", pacient.CNP);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Delete from pacienti where Id=@ID";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@ID", Id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Consultatii> GetConsultatiiForPacient(int Id)
        {
            var listaConsultatii = new List<Consultatii>();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select c.Id as IdC ,m.Nume as NumeMedic,m.Prenume as PrenumeMedic,Ora_Data,pret,isConfirmed from consultatii as c join pacienti as p on p.Id=c.Id_pacient join medici as m on c.Id_medic=m.Id where p.Id=@Id";
                
               
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("Id", Id);
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            var newConsultatie = new Consultatii
                            {
                                Id = Int32.Parse(dr["IdC"].ToString()),
                               Ora_data=DateTime.Parse(dr["Ora_Data"].ToString()),
                                pret=Int32.Parse(dr["pret"].ToString()),
                                isConfirmed=Boolean.Parse(dr["isConfirmed"].ToString()),
                                NumeMedic=dr["NumeMedic"].ToString()+" "+dr["PrenumeMedic"].ToString(),
                            };

                            listaConsultatii.Add(newConsultatie);
                        }

                    }
                }
                connection.Close();
                return listaConsultatii;


            }
        }

    }
}
