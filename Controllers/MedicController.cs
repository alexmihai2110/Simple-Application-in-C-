using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosultations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using Services.Entities;
using Services.Services;

namespace Cosultations.Controllers
{
    public class MedicController : Controller
    {
        private readonly MedicServices serviceMedici;
        private readonly SpecializareServices specializareServices;
        private readonly CurrentUser currentUser;

        public MedicController(MedicServices serviceMedici, SpecializareServices specializareServices, CurrentUser currentUser)
        {
            this.currentUser = currentUser;
            this.specializareServices = specializareServices;
            this.serviceMedici = serviceMedici;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (currentUser.IsAuthenticated)
            {
                var medici = serviceMedici.GetAll();
                return View(medici);
            }
            else return NotFound();
        }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            if (currentUser.IsAuthenticated)
            {
                serviceMedici.Delete(Id);
                return RedirectToAction("Index", "Medic");
            }
            else return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            if (currentUser.IsAuthenticated)
            {
                var model = serviceMedici.GetMedic(Id);
                var editedModel = new MedicVM
                {
                    Id = model.Id,
                    Nume = model.Nume,
                    Prenume = model.Prenume,
                    Data_Nasterii = model.DataNasterii,
                    Id_specializare = model.Id_specializare
                };
                editedModel.specializari = GetSpecializari();
                editedModel.cabinet = GetCabinet();
                return View(editedModel);
            }
            else return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(MedicVM medic)
        {
            var editedMedic = new Medic { Id = medic.Id, Nume = medic.Nume, Prenume = medic.Prenume, DataNasterii = medic.Data_Nasterii, Id_specializare = medic.Id_specializare, Id_cabinet = medic.Id_cabinet };
            serviceMedici.Update(editedMedic);
            return RedirectToAction("Index", "Medic");
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (currentUser.IsAuthenticated)
            {
                var model = new MedicVM
                {
                    specializari = GetSpecializari(),
                    cabinet = GetCabinet()
                };

                return View(model);
            }
            else return NotFound();
        }

        [HttpPost]

        public IActionResult Create(MedicVM medic)
        {
            var newMedic = new Medic
            {
                DataNasterii = medic.Data_Nasterii,
                Nume = medic.Nume,
                Prenume = medic.Prenume,
                Id_specializare = medic.Id_specializare,
                Id_cabinet = medic.Id_cabinet
            };
            serviceMedici.Insert(newMedic);
            return RedirectToAction("Index", "Medic");
        }
        private List<SelectListItem> GetSpecializari()
        {
            var services = specializareServices.GetAll();

            return services.Select(c => new SelectListItem
            {
                Text = c.Nume,
                Value = c.Id.ToString()
            })
            .ToList();
        }
        private List<SelectListItem> GetCabinet()
        {
            var cabinet = serviceMedici.GetAllCab();

            return cabinet.Select(c => new SelectListItem
            {
                Text = c.Etaj.ToString(),
                Value = c.Id.ToString()
            })
            .ToList();
        }

        public IActionResult ViewList()
        {
            var model = serviceMedici.TakeAllMedicNameswithCab();
            return View(model);
        }
        public IActionResult SeeAllMedicsByServiciuId(int Id)
        {
            var model = serviceMedici.TakeAllTheMedicThatCanExecuteaAService(Id);
            return View(model);
        }

        public IActionResult SeeTheYoungestStaff()
        {
            var model = serviceMedici.GetTheYoungestMedics();
            return View(model);
        }
        public IActionResult IndexFilter(string Nume)
        {
            if (Nume != null) {
                var medici = serviceMedici.GetAllByName(Nume);
                var model = new IndexMedicNameVm();
                model.medici = medici;
                return View(model);
            }
            else
            {
                var medici = serviceMedici.GetAll();
                var model = new IndexMedicNameVm();
                model.medici = medici;
                return View(model);
            }

        }
        public IActionResult TakeAllWithoutCons()
        {
            var model = serviceMedici.GetAllWithoutCons();
            return View(model);
        }
    }
}