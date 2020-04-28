using Services.Appsetings;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Services.Services
{
    public class MedicServices : BasicService
    {
        public List<Medic> GetAllWithoutCons()
        {
            var listaMedici = new List<Medic>();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "  Select m.Nume,m.Prenume from medici as m join specializari as s on s.Id=m.Id_specializare where m.Id Not in (select c.Id_medic from consultatii as c join medici as med on med.Id=c.Id_medic where c.Ora_Data <GetDate())";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            var newMedic = new Medic
                            {
                            
                                Nume = dr["Nume"].ToString(),
                                Prenume = dr["Prenume"].ToString(),
                              

                            };

                            listaMedici.Add(newMedic);
                        }

                    }
                }
                connection.Close();
                return listaMedici;


            }
        }
        public Medic GetMedic(int Id)
        {


            var newMedic = new Medic();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select * from medici where Id=@Id";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            newMedic.Id = Int32.Parse(dr["Id"].ToString());
                            newMedic.Nume = dr["Nume"].ToString();
                            newMedic.Prenume = dr["Prenume"].ToString();
                            newMedic.DataNasterii = DateTime.Parse(dr["Data_Nasterii"].ToString());
                            newMedic.Id_specializare = Int32.Parse(dr["Id_specializare"].ToString());
                        }
                    }
                }


                connection.Close();
                return newMedic;
            }
        }
        public void Insert(Medic medic)
        {
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Insert into medici(Nume,Prenume,Data_nasterii,Id_specializare,Id_cabinet) values(@Nume,@Prenume,@Data_nasterii,@Id_specializare,@Id_cabinet)";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Nume", medic.Nume);
                    command.Parameters.AddWithValue("@Prenume", medic.Prenume);
                    command.Parameters.AddWithValue("@Data_Nasterii", medic.DataNasterii);
                    command.Parameters.AddWithValue("@ID_specializare", medic.Id_specializare);
                    command.Parameters.AddWithValue("Id_cabinet", medic.Id_cabinet);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Medic medic)
        {
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Update medici set Nume=@nume, Prenume=@prenume, Data_Nasterii=@data_nasterii, Id_specializare=@Id_specializare, Id_cabinet=@Id_cabinet where Id=@Id";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@nume", medic.Nume);
                    command.Parameters.AddWithValue("@Id", medic.Id);
                    command.Parameters.AddWithValue("@prenume", medic.Prenume);
                    command.Parameters.AddWithValue("@data_Nasterii", medic.DataNasterii);
                    command.Parameters.AddWithValue("@Id_specializare", medic.Id_specializare);
                    command.Parameters.AddWithValue("@Id_cabinet", medic.Id_cabinet);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Delete from medici where Id=@ID";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@ID", Id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public List<Medic> GetAll()
        {
            var listaMedici = new List<Medic>();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select m.Id,m.Nume as NumeMedic,Prenume,Data_Nasterii,c.Etaj ,s.Nume as NumeSpecializare from medici as m " +
                                                       "join specializari as s on m.Id_specializare=s.Id " +
                                                       "join cabinet as c on c.Id=m.Id_cabinet";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            var newMedic = new Medic
                            {
                                Id = Int32.Parse(dr["Id"].ToString()),
                                Nume = dr["NumeMedic"].ToString(),
                                Prenume = dr["Prenume"].ToString(),
                                NumeSpecializare = dr["NumeSpecializare"].ToString(),
                                DataNasterii = DateTime.Parse(dr["Data_Nasterii"].ToString()),
                                EtajCabinet = Int32.Parse(dr["Etaj"].ToString())

                            };

                            listaMedici.Add(newMedic);
                        }

                    }
                }
                connection.Close();
                return listaMedici;


            }
        }
        public List<Medic> TakeAllMedicNameswithCab()
        {
            var listaMedici = new List<Medic>();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select c.etaj as NumeCabinet, m.Nume as NumeMedic,Prenume from medici as m join cabinet as c on  c.Id=m.Id_cabinet";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            var newMedic = new Medic
                            {

                                Nume = dr["NumeMedic"].ToString(),
                                Prenume = dr["Prenume"].ToString(),
                                EtajCabinet = Int32.Parse(dr["NumeCabinet"].ToString()),

                            };

                            listaMedici.Add(newMedic);
                        }

                    }
                }
                connection.Close();
                return listaMedici;


            }
        }


        public List<Medic> TakeAllTheMedicThatCanExecuteaAService(int ServiciuId)
        {
            var listaMedici = new List<Medic>();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select m.Id,Nume, Prenume, Etaj from medici as m join cabinet as c on c.Id=m.Id_cabinet join specializari_servicii as ss on ss.Id_specializare=m.Id_specializare where m.Id_specializare IN (select Id_specializare from specializari_servicii  where Id_serviciu=@Id)";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Id", ServiciuId);
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            var newMedic = new Medic
                            {

                                Nume = dr["Nume"].ToString(),
                                Prenume = dr["Prenume"].ToString(),
                                EtajCabinet = Int32.Parse(dr["Etaj"].ToString()),

                            };

                            listaMedici.Add(newMedic);
                        }

                    }
                }
                connection.Close();
                return listaMedici;


            }
        }

        public List<Cabinet> GetAllCab()
        {
            var listaCabinete = new List<Cabinet>();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "Select Id, etaj from cabinet";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            var newCabinet = new Cabinet
                            {
                                Id = Int32.Parse(dr["Id"].ToString()),
                                Etaj=Int32.Parse(dr["etaj"].ToString())

                            };

                            listaCabinete.Add(newCabinet);
                        }

                    }
                }
                connection.Close();
                return listaCabinete;


            }

        }

        public List<Medic> GetTheYoungestMedics()
        {
            var listaMedici = new List<Medic>();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "select m.Nume, m.Prenume, s.Nume as NumeSpecializare from medici as m join specializari as s on s.Id = m.Id_specializare where Data_nasterii IN(select top(1) Data_nasterii from medici as me where me.Id_specializare = s.Id order by Data_nasterii DESC)";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            var newMedic = new Medic
                            {
                                Nume = dr["Nume"].ToString(),
                                Prenume = dr["Prenume"].ToString(),
                                NumeSpecializare = dr["NumeSpecializare"].ToString()
                            };

                            listaMedici.Add(newMedic);
                        }

                    }
                }
                connection.Close();
                return listaMedici;


            }
        }

        public List<Medic> GetAllByName(string nume)
        {
            var listaMedici = new List<Medic>();
            using (SqlConnection connection = new SqlConnection(AppSettings.connectionString))
            {
                var queryString = "  select m.Nume,s.Id,m.Id,m.Prenume from medici as m" + 
                                    " join specializari as s on s.Id = m.Id_specializare" +
                                    " where s.Nume = @Nume and(select count(*) from consultatii as c where c.Id_medic = m.Id) >=" +
                                    " (select avg(counts) from" +
                                    " (select COUNT(*) as counts, med.Id  from consultatii as cs join medici as med on med.Id = cs.Id_medic where med.Id_specializare=s.Id group by med.Id) as newtable ) ";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Nume",nume);
                    connection.Open();
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dr.Read())
                        {
                            var newMedic = new Medic
                            {
                                Id = Int32.Parse(dr["Id"].ToString()),
                                Nume = dr["Nume"].ToString(),
                                Prenume = dr["Prenume"].ToString(),
                              

                            };

                            listaMedici.Add(newMedic);
                        }

                    }
                }
                connection.Close();
                return listaMedici;


            }
        
    }

    }
}
