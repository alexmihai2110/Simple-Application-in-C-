using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosultations.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Entities;
using Services.Services;

namespace Cosultations.Controllers
{
    public class SpecializareController : Controller
    {
        private readonly SpecializareServices specializareServices;
        public SpecializareController(SpecializareServices specializareServices)
        {
            this.specializareServices = specializareServices;
        }
        public IActionResult Index()
        {
          var specializari = specializareServices.GetAll();
          return View(specializari);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(CreateSpecializareVm model)
        {
            var newModel = new Specializare { Nume = model.Nume };
            specializareServices.Insert(newModel);
            return RedirectToAction("Index", "Specializare");
        }

        public IActionResult Delete(int Id)
        {
            specializareServices.Delete(Id);
            return RedirectToAction("Index", "Specializare");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var model=specializareServices.GetSpecializare(Id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Specializare specializare)
        {
            specializareServices.Update(specializare);
            return RedirectToAction("Index", "Specializare");
        }

        [HttpGet]

        public IActionResult SeeServicesBySpecializareId(int Id)
        {
            var servicii = specializareServices.TakeServicesBySpecializareId(Id);
            return View(servicii);
        }
    }
}