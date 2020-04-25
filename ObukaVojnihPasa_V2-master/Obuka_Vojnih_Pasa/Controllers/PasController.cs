using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Obuka_Vojnih_Pasa.Models;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Services;
using Obuka_Vojnih_Pasa.Services.Interafaces;
using Obuka_Vojnih_Pasa.Models.Extensions;
using Microsoft.AspNetCore.Identity;
using Obuka_Vojnih_Pasa.ViewModels;
using Obuka_Vojnih_Pasa.ViewModels.PasViewModel;

namespace Obuka_Vojnih_Pasa.Controllers
{
  
    public class PasController : Controller
    {
        private readonly IPasService service;
        private readonly IObukaService serviceObuka;
        private readonly IAngazovanjeService serviceAngazovanje;

        private ObukaPasaContext obukaPasaContext;
        public PasController(IAngazovanjeService serviceAngazovanje, IPasService service, IObukaService serviceObuka, ObukaPasaContext obukaPasaContext)
        {
            this.service = service;
            this.serviceObuka = serviceObuka;
            this.obukaPasaContext = obukaPasaContext;
            this.serviceAngazovanje = serviceAngazovanje;

        }




        [Authorize(Roles = "Korisnik,Admin")]
        [AllowAnonymous]
        public IActionResult Statistika(int? id)
        {


            try
            {

                if (id == null)
                    throw new Exception("Pas čije podatke zahtevate ne postoji!");

                Pas pasIzBaze = service.FindById(id);

                if (pasIzBaze == null) throw new Exception("Pas čije podatke zahtevate ne postoji!");


                if (pasIzBaze.Angazovanja.Count() != 0)
                {
                    ViewBag.Desetke = service.VratiBrojOcena(pasIzBaze, 10);
                    ViewBag.Devetke = service.VratiBrojOcena(pasIzBaze, 9);
                    ViewBag.Osmice = service.VratiBrojOcena(pasIzBaze, 8);
                    ViewBag.Sedmice = service.VratiBrojOcena(pasIzBaze, 7);
                    ViewBag.Sestice = service.VratiBrojOcena(pasIzBaze, 6);
                    ViewBag.Petice = service.VratiBrojOcena(pasIzBaze, 5);
                    ViewBag.Uspeh = service.UspesnostPsaNaObavljenimZadacima(pasIzBaze);
                    ViewBag.UkupnoAng = pasIzBaze.Angazovanja.Count();
                }
                else
                {
                    ViewBag.UkupnoAng = 0;
                    ViewBag.Uspeh = 0;
                }

                ViewBag.PasId = pasIzBaze.Id;
                return View(pasIzBaze);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });

            }
        }
        public IActionResult Search(string message, int? id)
        {
            try
            {
                if (id != null) ViewBag.Obuka = id;
                else ViewBag.Obuka = null;
                ViewBag.ListaPasa = service.GetAll().ToList();
                ViewBag.ListaObuka = serviceObuka.GetAll().ToList();
                if (message == null) message = string.Empty;
                ViewBag.Message = message;
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });

            }
        }
        [HttpGet]
 
      
        public IActionResult Create()
        {
            ObukeDropDownList();
            RaseDropDownList();
            PolDropDownList();

            return View();
        }

        [HttpPost]
     
        public IActionResult Create([Bind("Id", "BrojZdravstveneKnjizice", "Ime", "DatumRodjenja", "Rasa", "Pol", "ObukaId")] InsertPasViewModel pas)
        {
            try
            {
              

                if (ModelState.IsValid)
                {
                    Pas pasToInsert = new Pas()
                    {
                        BrojZdravstveneKnjizice = pas.BrojZdravstveneKnjizice,
                        DatumRodjenja = pas.DatumRodjenja,
                        Ime = pas.Ime,
                        Id = pas.Id,
                        Obuka = pas.Obuka,
                        Rasa = pas.Rasa,
                        ObukaId = pas.ObukaId,
                        Angazovanja = pas.Angazovanja,
                        Pol = pas.Pol
                        
                    };
                    service.Insert(pasToInsert);
                    return RedirectToAction("Search", new { message = $"Uspešno sačuvani podaci o psu: {pas.Ime}" });
                }

                ObukeDropDownList(pas.ObukaId);
                RaseDropDownList(pas.Rasa);
                PolDropDownList(pas.Pol);
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }

        }
      
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null) throw new Exception("Pas čije podatke zahtevate ne postoji!");
                var pas = service.FindById(id);
                if (pas == null) throw new Exception("Pas čije podatke zahtevate ne postoji!");
                ObukeDropDownList(pas.ObukaId);

                return View(pas);

            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }


        }


 
        [HttpPost]
        public IActionResult Edit(Pas pas)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var pasIzBaze = service.FindById(pas.Id);
                    if (pasIzBaze == null) throw new Exception("Pas čije podatke zahtevate ne postoji!");


                    pasIzBaze.BrojZdravstveneKnjizice = pas.BrojZdravstveneKnjizice;
                    pasIzBaze.DatumRodjenja = pas.DatumRodjenja;
                    pasIzBaze.Ime = pas.Ime;
                    pasIzBaze.Pol = pas.Pol;
                    pasIzBaze.Obuka = pas.Obuka;
                    pasIzBaze.ObukaId = pas.ObukaId;
                    pasIzBaze.Rasa = pas.Rasa;
                    service.Update(pasIzBaze);


                    return RedirectToAction("Search", new { message = $"Izmene o psu: {pasIzBaze.Ime} uspešno sačuvane" });
                }
                return View(pas);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }

        }

        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null) throw new Exception("Pas čije podatke zahtevate ne postoji!");
                var pasIzBaze = service.FindById(id);
                if (pasIzBaze == null) throw new Exception("Pas čije podatke zahtevate ne postoji!");


                return View(pasIzBaze);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }


        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePas(int? id)
        {
            try
            {
                if (id == null) throw new Exception("Pas čije podatke zahtevate ne postoji!");
                var pasZaBrisanje = service.FindById(id);
                if (pasZaBrisanje == null) throw new Exception("Pas čije podatke zahtevate ne postoji!");



                service.Delete(pasZaBrisanje.Id);
                return RedirectToAction("Search", new { message = $"Uspešno obrisani podaci o psu: {pasZaBrisanje.Ime}" });


            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }




        }











        public IActionResult UkStatistika()
        {

            try
            {

                List<Pas> tri = service.TriNajuspesnijaPsa();
                if (tri.Count() != 0)
                {
                    List<double> uspesnost = new List<double>() {
                  service.UspesnostPsaNaObavljenimZadacima(tri.ElementAt(0)),
                  service.UspesnostPsaNaObavljenimZadacima(tri.ElementAt(1)),
                    service.UspesnostPsaNaObavljenimZadacima(tri.ElementAt(2))
            };
                    ViewBag.Uspesnost = uspesnost;
                }
                else tri = new List<Pas>();




                return View(tri);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }

        }

        [HttpPost]
        public IActionResult Psi(PsiFilter filters)
        {




            var psi = GetPsi(filters);

            return PartialView(psi.ToList());
        }
        private List<Pas> GetPsi(PsiFilter filters)
        {
            List<Pas> sviPsi = service.GetAll().ToList();
            List<Pas> psi = new List<Pas>();
            if (!string.IsNullOrEmpty(filters.Pas) && filters.ObukaId != 0)
            {
                psi = sviPsi.Where(s => s.Ime.ToLower().Contains(filters.Pas.ToLower())
                    && s.ObukaId == filters.ObukaId).ToList();
                if (psi.Count() == 0) return psi;
            }
            else
            {

                if (!string.IsNullOrEmpty(filters.Pas))
                {
                    psi = sviPsi.Where(s => s.Ime.ToLower().Contains(filters.Pas.ToLower())
                        || s.Rasa.ToLower().Contains(filters.Pas.ToLower())).ToList();
                    if (psi.Count() == 0) return psi;
                }

                if (filters.ObukaId != 0)
                {
                    psi.AddRange(sviPsi.Where(s => s.ObukaId == filters.ObukaId && !psi.Contains(s)));
                    return psi;
                }
            }

            if (psi.Count() == 0) psi = sviPsi;
            return psi;
        }
        private void ObukeDropDownList(object izabranaObuka = null)
        {
            var obukeQuery = from o in serviceObuka.GetAll().AsQueryable()
                             orderby o.Naziv
                             select o;
            ViewBag.ObukaId = new SelectList(obukeQuery.AsNoTracking(), "Id", "Naziv", izabranaObuka);
        }

        private void PolDropDownList(string rasa = null)
        {

            ViewBag.Rase = new SelectList(RaseStore.rase.AsQueryable().AsNoTracking(), rasa);


        }
        private void RaseDropDownList(string pol = null)
        {
            List<string> polovi = new List<string> { "Muški", "Ženski" };
            ViewBag.Pol = new SelectList(polovi.AsQueryable().AsNoTracking(), pol);

        }
        [HttpPost]
        public IActionResult AngazovanjaPsa(PsiFilter filters)
        {
            List<Angazovanje> angazovanja = new List<Angazovanje>();

            if (filters.Akcija == null)
            {
                ViewBag.Init = "";
                return PartialView(angazovanja.ToList());

            }

            if (filters.Akcija != null)
            {

                angazovanja = service.vratiAngazovanjaPoKriterijumu(filters.Akcija, filters.PasId);

            }

            if (angazovanja == null)
            {
                angazovanja = new List<Angazovanje>();
            }


            return PartialView(angazovanja.ToList());

        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public IActionResult NevalidanBrojKnjizice([FromQuery]string BrojZdravstveneKnjizice)
        {
            var pas =  service.GetAll().ToList().SingleOrDefault(p=>p.BrojZdravstveneKnjizice == BrojZdravstveneKnjizice);
       
            if (pas == null)
            {
                return Json(true);
            }
            else
            {
                return Json($" {pas.BrojZdravstveneKnjizice} broj knjižice je zauzet!");
            }
        }


    }


}