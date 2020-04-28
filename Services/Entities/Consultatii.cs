using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Entities
{
    public class Consultatii
    {
        public int Id { get; set; }
        public int pret { get; set; }
        public int Id_medic { get; set; }
        public int Id_pacient { get; set; }
        public int Id_cabinet { get; set; }
        public bool isConfirmed { get; set; }
        public string NumePacient { get; set; }
        public string NumeMedic { get; set; }
        public DateTime Ora_data { get; set; }
        public string Confirmare { get; set; }

    }
}
