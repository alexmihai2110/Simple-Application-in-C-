using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosultations.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Entities;
using Services.Services;

namespace Cosultations.Controllers
{
    public class ConsultationController : Controller
    {
        private readonly ConsultatiiServices consultatiiServices;
        private readonly CurrentUser currentUser;
        public ConsultationController(ConsultatiiServices consultatiiServices, CurrentUser currentUser)
        {
            this.currentUser = currentUser;
            this.consultatiiServices = consultatiiServices;

        }
        public IActionResult Index(string Nume)
        {
            var model = new IndexConsultatiiVm();
            if (currentUser.IsAuthenticated)
            {
                if (Nume == null)
                {
                    var consultatii = consultatiiServices.GetAll();
                    model.listaConsultatii = consultatii;
                    return View(model);
                }
                else
                {
                    var consultatii = consultatiiServices.GetAllByNameP(Nume);
                    model.listaConsultatii = consultatii;
                    return View(model);
                }
            }
            else return NotFound();
        }
       
        public IActionResult SeeServices(int Id)
        {
            if (currentUser.IsAuthenticated)
            {
                var services = consultatiiServices.TakeServicesByConsultationId(Id);
                return View(services);
            }
            else return NotFound();
        }

        public IActionResult DetailsAboutConsultation(int Id)
        {
            if (currentUser.IsAuthenticated)
            {
                var model = consultatiiServices.TakeAllDetails(Id);
                return View(model);
            }
            else return NotFound();
        }

        
        

      
    }
}