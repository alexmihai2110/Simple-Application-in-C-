using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Entities;
using Services.Services;

namespace Cosultations.Controllers
{
    public class PacientController : Controller
    {
        private readonly PacientServices pacientServices;
        public PacientController(PacientServices pacientServices)
        {
            this.pacientServices = pacientServices;   
        }
        public IActionResult Index()
        {
            var model = pacientServices.GetAll();
            return View(model);
        }
        public IActionResult GetConsultatiiForPacient(int Id)
        {
            var model = pacientServices.GetConsultatiiForPacient(Id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pacient pacient)
        {
            pacientServices.Insert(pacient);
            return RedirectToAction("Index","Pacient");
        }

        public IActionResult Delete(int Id)
        {
            pacientServices.Delete(Id);
            return RedirectToAction("Index", "Pacient");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var entity = pacientServices.GetPacient(Id);
            return View(entity);
        }
        [HttpPost]
        public IActionResult Edit(Pacient pacient)
        {
            pacientServices.Update(pacient);
            return RedirectToAction("Index","Pacient");
        }
    }
}