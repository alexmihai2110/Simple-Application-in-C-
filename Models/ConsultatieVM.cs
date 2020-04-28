using Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cosultations.Models
{
    public class ConsultatieVM
    {
        public int Id { get; set; }
        public int pret { get; set; }
        public bool isConfirmed { get; set; }
        public DateTime Data { get; set; }
        public int Id_cabinet { get; set; }
        public int Id_medic{get;set;}
        public Medic medic { get; set; }
        public int Id_pacient { get; set; }
        public Pacient pacient { get; set; }

    }
}
