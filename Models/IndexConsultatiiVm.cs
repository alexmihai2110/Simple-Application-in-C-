using Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cosultations.Models
{
	public class IndexConsultatiiVm
	{
		public IndexConsultatiiVm()
		{
			listaConsultatii = new List<Consultatii>();
		}
		public string Nume { get; set;}
		public List<Consultatii> listaConsultatii { get; set; }
	}
}
