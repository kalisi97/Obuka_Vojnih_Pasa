using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Obuka_Vojnih_Pasa.Models;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Services;
using Obuka_Vojnih_Pasa.ViewModels.ApplicationUserViewModels;

namespace Obuka_Vojnih_Pasa.Controllers
{
    public class InstruktorController : Controller
    {
        private readonly IObukaService service;
        private readonly ObukaPasaContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public InstruktorController(SignInManager<ApplicationUser> signInManager, IObukaService service, ObukaPasaContext context, UserManager<ApplicationUser> userManager)
        {
            this.service = service;
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {



            if (id == userManager.GetUserId(User))
            {

                ApplicationUser user = await userManager.FindByIdAsync(id);
                Instruktor instruktor = user as Instruktor;

                if (user == null)
                {
                    return View("PageNotFound");
                }

                ObukeDropDownList(instruktor.ObukaId);
                CinoviDropDownList(instruktor.Cin);

                var model = new EditInstruktorViewModel()
                {
                    Id = instruktor.Id,
                    UserName = instruktor.UserName,
                    Cin = instruktor.Cin,
                    ImePrezime = instruktor.ImePrezime,
                    Obuka = instruktor.Obuka,
                    ObukaId = instruktor.ObukaId,
                    Email = instruktor.Email
                };

                return View(model);
            }
            else
            {
                return View("AccessDenied");
            }


        }



        [HttpPost]
        public async Task<IActionResult> Edit(EditInstruktorViewModel instruktor)
        {

            if (ModelState.IsValid)
            {
                string id = instruktor.Id;
                Instruktor instruktorIzBaze = await userManager.FindByIdAsync(id) as Instruktor;


                if (instruktorIzBaze == null) return NotFound();
                instruktorIzBaze.Cin = instruktor.Cin;
                instruktorIzBaze.ImePrezime = instruktor.ImePrezime;
                instruktorIzBaze.Obuka = instruktor.Obuka;
                instruktorIzBaze.ObukaId = instruktor.ObukaId;

                var result = await userManager.UpdateAsync(instruktorIzBaze);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { message = $"Izmene profila uspešno sačuvane" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }





            }

            return View(instruktor);

        }
        private void CinoviDropDownList(string cin = null)
        {
            List<string> cinovi = new List<string> { "Razvodnik", "Desetar", "Mlađi vodnik", "Vodnik", "Vodnik I klase", "Stariji vodnik", "Stariji vodnik I klase", "Zastavik" };
            ViewBag.Cinovi = new SelectList(cinovi.AsQueryable().AsNoTracking(), cin);
        }
        private void ObukeDropDownList(object izabranaObuka = null)
        {
            var obukeQuery = from o in service.GetAll().AsQueryable()
                             orderby o.Naziv
                             select o;
            ViewBag.ObukaId = new SelectList(obukeQuery.AsNoTracking(), "Id", "Naziv", izabranaObuka);
        }




    }

}
