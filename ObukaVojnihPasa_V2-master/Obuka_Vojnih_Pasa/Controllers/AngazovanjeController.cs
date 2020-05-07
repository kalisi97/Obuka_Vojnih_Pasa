
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Extensions;
using Obuka_Vojnih_Pasa.Services.Interafaces;
using Obuka_Vojnih_Pasa.ViewModels;

namespace Obuka_Vojnih_Pasa.Controllers
{

    public class AngazovanjeController : Controller
    {

        private readonly IPasService servicePsi;
        private readonly IZadatakService serviceZadaci;
        private readonly IAngazovanjeService service;
        private readonly UserManager<ApplicationUser> userManager;
        public AngazovanjeController(UserManager<ApplicationUser> userManager,IPasService servicePsi, IZadatakService serviceZadaci, IAngazovanjeService service)
        {
            this.servicePsi = servicePsi;
            this.serviceZadaci = serviceZadaci;
            this.service = service;
            this.userManager = userManager;
        }
  

        public IActionResult Index(string message)

        {
            try
            {
                ViewBag.ListaAngazovanja = service.GetAll().ToList();
                List<Zadatak> zadaci = serviceZadaci.GetAll().Distinct(new ZadatakComparer()).ToList();
                ViewBag.ListaZadataka = zadaci;
                if (message == null) message = string.Empty;
                ViewBag.Message = message;
                return View();
            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home", new { message = ex.Message });

            }
        }



        [HttpPost]
    
        public IActionResult Angazovanja(AngazovanjaFilter filters)
        {
            var ang = GetAngazovanja(filters);

            return PartialView(ang.ToList());
        }

        private List<Angazovanje> GetAngazovanja(AngazovanjaFilter filters)
        {
            List<Angazovanje> sviPsi = service.GetAll().ToList();
            List<Angazovanje> psi = new List<Angazovanje>();
            if (!string.IsNullOrEmpty(filters.Kriterijum) && filters.ZadatakId != 0)
            {
                psi = sviPsi.Where(s => s.Pas.Ime.ToLower().Contains(filters.Kriterijum.ToLower())
                    && s.ZadatakId == filters.ZadatakId).ToList();
                if (psi.Count() == 0) return psi;
            }
            else
            {

                if (!string.IsNullOrEmpty(filters.Kriterijum))
                {
                    psi = sviPsi.Where(s => s.Pas.Ime.ToLower().Contains(filters.Kriterijum.ToLower())).ToList();

                    if (psi.Count() == 0) return psi;
                }

                if (filters.ZadatakId != 0)
                {
                    psi.AddRange(sviPsi.Where(s => s.ZadatakId == filters.ZadatakId && !psi.Contains(s)));
                    return psi;
                }
            }

            if (psi.Count() == 0) psi = sviPsi;
            return psi;
        }




        //pri izmeni izlistati sta je do sada ocenjeno, razmisliti kako to moze da se uradi
        public async Task<IActionResult> Update(int id)
        {
     

            // koji psi su angazovani na angazovanju cije ocene unosimo


           
            ViewBag.Zadaci = serviceZadaci.GetAll().ToList();
            ViewBag.Zadatak = serviceZadaci.FindById(id).NazivZadatka;
            ViewBag.Id = serviceZadaci.FindById(id).Id;

            List<Angazovanje> angazovanjaZaOcenu = service.GetAll().Where(a => a.ZadatakId == id).ToList();
            List<Pas> angazovaniPsi = new List<Pas>();
            List<Angazovanje> ocenjenaAngazovanja = new List<Angazovanje>();

            Instruktor user = null;
            if (!User.IsInRole("Admin"))
            {
                user = await userManager.FindByIdAsync(userManager.GetUserId(User)) as Instruktor;
                foreach (Angazovanje angazovanje in angazovanjaZaOcenu)
                {
                    if (angazovanje.Pas.ObukaId == user.ObukaId)
                    {
                        if (angazovanje.Ocena == null)
                            angazovaniPsi.Add(angazovanje.Pas);
                        else ocenjenaAngazovanja.Add(angazovanje);
                    }
                }
            }
            else
            {
                if (User.IsInRole("Admin"))
                {
                    foreach (Angazovanje angazovanje in angazovanjaZaOcenu)
                    {
                        if (angazovanje.Ocena == null)
                            angazovaniPsi.Add(angazovanje.Pas);
                        else ocenjenaAngazovanja.Add(angazovanje);
                    }

                }
            }
            if (!User.IsInRole("Admin"))
                if (angazovaniPsi.Count()==0) return RedirectToAction("PageNotFound", "Home", new { message = "Nijedan pas sa vaše obuke nije angažovan na ovom zadatku!" });

            if (ocenjenaAngazovanja!=null)  ViewBag.Ocenjena = ocenjenaAngazovanja;
          
            
            ViewBag.Psi = angazovaniPsi;
            ViewBag.BrojAngazovanja = angazovaniPsi.Count();
     
            ZadatakDropDownList();
            Angazovanje a = new Angazovanje()
            {
                ZadatakId = serviceZadaci.FindById(id).Id,
                Zadatak = serviceZadaci.FindById(id),
                DatumUnosaOcene = DateTime.Now,

            };

            return View(a);

        }
     
        [HttpPost]
       
        public JsonResult UpdateAngazovanja([FromBody] IEnumerable<Object> angazovanja)
        {
          

    

            try
            {
                Zadatak z = null;
                foreach (Object a in angazovanja)
                {
                    string json = a.ToString();
                    Angazovanje ang = Newtonsoft.Json.JsonConvert.DeserializeObject<Angazovanje>(json);
                 
                    var angazovanjeIzBaze = service.GetById(ang.PasId, ang.ZadatakId) ?? throw new Exception("Angažovanje čije podatke želite da izmenite ne postoji!");
                    angazovanjeIzBaze.DatumUnosaOcene = ang.DatumUnosaOcene;
                    angazovanjeIzBaze.Ocena = ang.Ocena;
                    z = angazovanjeIzBaze.Zadatak;
                    service.Update(angazovanjeIzBaze);
                    ZadatakDropDownList(ang.ZadatakId);
                   
                }

                return Json("Angažovanja uspešno ocenjena!");
            }
            catch (Exception ex)
            {
                return Json("Došlo je do greške prilikom unosa!"+ex);            }

        }

        
        public async Task<IActionResult> Edit(string PasId, string ZadatakId)
        {


            try
            {
                if (PasId == null || ZadatakId == null) throw new Exception("Angažovanje čije podatke želite da izmenite ne postoji!");
                Angazovanje angazovanjeIzBaze = service.GetById(int.Parse(PasId), int.Parse(ZadatakId)) ?? throw new Exception("Angažovanje čije podatke želite da izmenite ne postoji!");
                if (angazovanjeIzBaze == null) throw new Exception("Angažovanje čije podatke želite da izmenite ne postoji!");

                await PasDropDownListAsync(angazovanjeIzBaze.PasId);
                ZadatakDropDownList(angazovanjeIzBaze.ZadatakId);
                angazovanjeIzBaze.DatumUnosaOcene = DateTime.Now;
                return View(angazovanjeIzBaze);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home", new { message = ex.Message });

            }

        }

     
        [HttpPost, ActionName("Edit")]
     
        public IActionResult EditAngazovanje(Angazovanje angazovanje)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var angazovanjeIzBaze = service.GetById(angazovanje.PasId, angazovanje.ZadatakId) ?? throw new Exception("Angažovanje čije podatke želite da izmenite ne postoji!");
                    if (angazovanjeIzBaze == null) throw new Exception("Angažovanje čije podatke želite da izmenite ne postoji!");
                    angazovanjeIzBaze.DatumUnosaOcene = angazovanje.DatumUnosaOcene;
                    angazovanjeIzBaze.Ocena = angazovanje.Ocena;


                    service.Update(angazovanjeIzBaze);
                    return RedirectToAction("Index", new { message = $"Izmene o angažovanju psa: {angazovanje.Pas.Ime} za zadatak:  {angazovanje.Zadatak.NazivZadatka}  uspešno sačuvane" });
                }
              


                return View(angazovanje);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home", new { message = ex.Message });

            }

        }
        private async Task PasDropDownListAsync(object izabranPas = null)
        {
            Instruktor user = null;
            if(!User.IsInRole("Admin"))
            {
                user = await userManager.FindByIdAsync(userManager.GetUserId(User)) as Instruktor;
                var pasQueryObuka = from p in servicePsi.GetAll().AsQueryable().Where(p => p.ObukaId == user.ObukaId) select p;
                ViewBag.PasId = new SelectList(pasQueryObuka.AsNoTracking(), "Id", "Ime", izabranPas);
            }
            else {
                if (User.IsInRole("Admin"))
                {

                    var pasQuery = from p in servicePsi.GetAll().AsQueryable() select p;
                    ViewBag.PasId = new SelectList(pasQuery.AsNoTracking(), "Id", "Ime", izabranPas);
                }
            }
           
        }

        private void ZadatakDropDownList(object izabranZadatak = null)
        {
            var zadatakQuery = from z in serviceZadaci.GetAll().AsQueryable()
                               orderby z.NazivZadatka
                               select z;
            ViewBag.ZadatakId = new SelectList(zadatakQuery.AsNoTracking(), "Id", "NazivZadatka", izabranZadatak);
        }



        [Authorize(Policy = "AngazovanjePolicy")]
        public IActionResult Delete(string PasId, string ZadatakId)
        {
            try
            {
                if (PasId == null || ZadatakId == null) throw new Exception("Angažovanje čije podatke zahtevate ne postoji!");
                Angazovanje angazovanjeIzBaze = service.GetById(int.Parse(PasId), int.Parse(ZadatakId));

                if (angazovanjeIzBaze == null) throw new Exception("Angažovanje čije podatke želite da izmenite ne postoji!");


                return View(angazovanjeIzBaze);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }


        }
        [Authorize(Policy = "AngazovanjePolicy")]
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteAngazovanje(Angazovanje angazovanje)
        {
            try
            {

                var angazovanjeIzBaze = service.GetById(angazovanje.PasId, angazovanje.ZadatakId) ?? throw new Exception("Angažovanje čije podatke želite da izmenite ne postoji!");

                service.Delete(angazovanjeIzBaze);
                return RedirectToAction("Index", new { message = "Uspešno obrisani podaci o izabranom angažovanju" });


            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }




        }














    }
}