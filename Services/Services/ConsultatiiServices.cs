using Services.Appsetings;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Services.Services
{
    public class ConsultatiiServices : BasicService
    {

        public List<Consultatii> GetAllByNameP(string nume)
        {
            var listaConsultatii = new List<Consultatii>();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select Ora_Data,pret,m.Nume as NumeMedic,m.Prenume as PrenumeMedic,p.Nume as NumePacient,p.Prenume as PrenumePacient,c.Id,isConfirmed from consultatii as c " +
                                        "join medici as m on Id_medic=m.Id " +
                                        "join pacienti as p on Id_pacient=p.Id" +
                                        " where p.Nume=@Nume";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Nume", nume);
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            var newConsultatie = new Consultatii
                            {
                                Id = Int32.Parse(dr["Id"].ToString()),
                                Ora_data = DateTime.Parse(dr["Ora_Data"].ToString()),
                                pret = Int32.Parse(dr["pret"].ToString()),
                                isConfirmed = Boolean.Parse(dr["isConfirmed"].ToString()),
                                NumeMedic = dr["NumeMedic"].ToString() + " " + dr["PrenumeMedic"].ToString(),
                                NumePacient = dr["NumePacient"].ToString() + " " + dr["PrenumePacient"].ToString(),

                            };
                            newConsultatie.Confirmare = newConsultatie.isConfirmed == true ? "Confirmat" : "Neconfirmat";

                            listaConsultatii.Add(newConsultatie);
                        }

                    }
                }
                connection.Close();
                return listaConsultatii;

            }
        }
        public Consultatii TakeAllDetails(int Id)
        {
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = " Select c.Id as IdC,Ora_Data,(Sum(s.pret) + c.pret) as PretDePlataTotal,m.Nume as NumeMedic,m.Prenume as PrenumeMedic,p.Nume as NumePacient,p.Prenume as PrenumePacient,isConfirmed from consultatii as c" +
                                       " join medici as m on Id_medic = m.Id" +
                                       " join pacienti as p on Id_pacient = p.Id" +
                                       " join consultatii_servicii as cs on Id_consultatie = c.Id" +
                                       " join specializari_servicii as ss on ss.Id = cs.Id_serviciu" +
                                       " join servicii as s on ss.Id_serviciu=s.Id" +
                                        " group by Ora_Data,c.Id,m.Nume,m.Prenume,p.Nume,p.Prenume,isConfirmed,c.pret" +
                                       " having c.Id = @Id;";
                var newConsultatie = new Consultatii();

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {

                            newConsultatie.Id = Int32.Parse(dr["IdC"].ToString());
                            newConsultatie.Ora_data = DateTime.Parse(dr["Ora_Data"].ToString());
                            newConsultatie.pret = Int32.Parse(dr["PretDePlataTotal"].ToString());
                            newConsultatie.isConfirmed = Boolean.Parse(dr["isConfirmed"].ToString());
                            newConsultatie.NumeMedic = dr["NumeMedic"].ToString() + " " + dr["PrenumeMedic"].ToString();
                            newConsultatie.NumePacient = dr["NumePacient"].ToString() + " " + dr["PrenumePacient"].ToString();

                        }
                        newConsultatie.Confirmare = newConsultatie.isConfirmed == true ? "Confirmat" : "Neconfirmat";

                    }
                }
                connection.Close();
                return newConsultatie;

            }
        }


        public List<Consultatii> GetAll()
        {
            var listaConsultatii = new List<Consultatii>();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select Ora_Data,pret,m.Nume as NumeMedic,m.Prenume as PrenumeMedic,p.Nume as NumePacient,p.Prenume as PrenumePacient,c.Id,isConfirmed from consultatii as c " +
                                        "join medici as m on Id_medic=m.Id " +
                                        "join pacienti as p on Id_pacient=p.Id";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            var newConsultatie = new Consultatii
                            {
                                Id = Int32.Parse(dr["Id"].ToString()),
                                Ora_data = DateTime.Parse(dr["Ora_Data"].ToString()),
                                pret = Int32.Parse(dr["pret"].ToString()),
                                isConfirmed = Boolean.Parse(dr["isConfirmed"].ToString()),
                                NumeMedic = dr["NumeMedic"].ToString() + " " + dr["PrenumeMedic"].ToString(),
                                NumePacient = dr["NumePacient"].ToString() + " " + dr["PrenumePacient"].ToString(),

                            };
                            newConsultatie.Confirmare = newConsultatie.isConfirmed == true ? "Confirmat" : "Neconfirmat";

                            listaConsultatii.Add(newConsultatie);
                        }

                    }
                }
                connection.Close();
                return listaConsultatii;

            }
        }

        public List<Servicii> TakeServicesByConsultationId(int Id)
        {
            var listaServicii = new List<Servicii>();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select Nume,pret from servicii as s join specializari_servicii as ss on s.Id = ss.Id_serviciu join consultatii_servicii as cs on cs.Id_serviciu = ss.Id_serviciu where cs.Id_consultatie = @Id";

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
                                Nume = dr["Nume"].ToString()

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

