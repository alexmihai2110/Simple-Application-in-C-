using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Cosultations.Controllers
{
    public class ServiciuController : Controller
    {
        private readonly ServiciiServices serviciiServices;
        public ServiciuController(ServiciiServices serviciiServices)
        {
            this.serviciiServices = serviciiServices;
        }
        public IActionResult Index()
        {
            var model = serviciiServices.GetAll();
            return View(model);
        }
    }
}