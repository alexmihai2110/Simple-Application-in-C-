using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Entities
{
    public class Pacient
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string CNP { get; set; }
        public DateTime Data_Nasterii { get; set; }

    }
}
