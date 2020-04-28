using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cosultations.Models
{
    public class MedicVM
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateTime Data_Nasterii { get; set; }
        public List<SelectListItem> specializari { get; set; }
        public List<SelectListItem> cabinet { get; set; }
        public int Id_specializare { get; set; }
        public int Id_cabinet { get; set; }
    }
}
