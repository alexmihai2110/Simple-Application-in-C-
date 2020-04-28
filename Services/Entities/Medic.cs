using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Entities
{
    public class Medic
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string CNP { get; set; }
        public DateTime DataNasterii { get; set; }
        public int Id_specializare { get; set; }
        public int Id_cabinet { get; set; }
        public string NumeSpecializare { get; set; }
        public int EtajCabinet { get; set; }
    }
}
