using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Services;

namespace Obuka_Vojnih_Pasa.Controllers
{
   [AllowAnonymous]
    public class ObukaController : Controller
    {
        private readonly IObukaService service;

        public ObukaController(IObukaService service)
        {

            this.service = service;
        }

        
        public IActionResult Index()

        {
            ViewBag.Obuke = service.GetAll().ToList();
            return View(service.GetAll().ToList());

        

        }


     

        public IActionResult Edit(int? id)
        {
            if (id == null) return View("PageNotFound");
            var obukaIzBaze = service.FindById(id);
            if (obukaIzBaze == null) return View("PageNotFound");
            return View(obukaIzBaze);
        }

        [HttpPost]
        public IActionResult Edit(Obuka obuka)
        {

            if(ModelState.IsValid)
            {
                var obukaIzBaze = service.FindById(obuka.Id);
                if (obukaIzBaze == null) return View("PageNotFound");
                obukaIzBaze.Naziv = obuka.Naziv;
                obukaIzBaze.Opis = obuka.Opis;
                obukaIzBaze.Trajanje = obuka.Trajanje;
                service.Update(obukaIzBaze);
                return RedirectToAction("Index");
            }

          
            return View(obuka);
        }
  
    }
}