using Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cosultations.Models
{
	public class IndexMedicNameVm
	{
		public IndexMedicNameVm()
		{
			medici = new List<Medic>();
		}
		public List<Medic> medici { get; set; }
		public string Nume { get; set; }
	}
}
