using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Extensions;
using Obuka_Vojnih_Pasa.Services.Interafaces;
using Obuka_Vojnih_Pasa.ViewModels.ZadatakViewModel;

namespace Obuka_Vojnih_Pasa.Controllers
{
    [AllowAnonymous]
    public class ZadatakController : Controller
    {
        private readonly IZadatakService service;
        private readonly IPasService servicePas;
        private readonly IAngazovanjeService serviceAngazovanje;
        private readonly UserManager<ApplicationUser> userManager;
        public ZadatakController(UserManager<ApplicationUser> userManager, IZadatakService service, IPasService servicePas, IAngazovanjeService serviceAngazovanje)
        {
            this.service = service;
            this.servicePas = servicePas;
            this.serviceAngazovanje = serviceAngazovanje;
            this.userManager = userManager;

        }

        public IActionResult Index(string message)
        {
            try
            {
                if (message == null) message = string.Empty;

                List<Zadatak> zadaci = service.GetAll().ToList();

                foreach (Zadatak z in zadaci)
                {
                    if (z.Datum <= DateTime.Now && z.Status == "Kreiran")
                    {

                        Zadatak zadatakIzBaze = service.FindById(z.Id);
                        if (zadatakIzBaze == null) return RedirectToAction("PageNotFound", "Home", new { message = "Zadatak čije podatke zahtevate ne postoji!" });
                        zadatakIzBaze.Angazovanja = z.Angazovanja;
                        zadatakIzBaze.Datum = z.Datum;
                        zadatakIzBaze.NazivZadatka = z.NazivZadatka;
                        zadatakIzBaze.Teren = z.Teren;
                        zadatakIzBaze.Status = Enumerations.Status.Završen.ToString();
                        service.Update(zadatakIzBaze);

                    }
                    int i = 0;
                    foreach (Angazovanje a in z.Angazovanja)
                    {
                        if (a.Ocena != null) i++;
                    }
                    if (i == z.Angazovanja.Count())
                    {
                        Zadatak zadatakIzBaze = service.FindById(z.Id);
                        if (zadatakIzBaze == null) return RedirectToAction("PageNotFound", "Home", new { message = "Zadatak čije podatke zahtevate ne postoji!" });
                        zadatakIzBaze.Angazovanja = z.Angazovanja;
                        zadatakIzBaze.Datum = z.Datum;
                        zadatakIzBaze.NazivZadatka = z.NazivZadatka;
                        zadatakIzBaze.Teren = z.Teren;
                        zadatakIzBaze.Status = Enumerations.Status.Ocenjen.ToString();
                        service.Update(zadatakIzBaze);
                    }

                }

                ViewBag.ListaZadataka = service.GetAll().ToList();
                List<string> naziviZadataka = service.VratiListuNazivaZadataka();
                List<int> ang = service.VratiBrojAngazovanjaPoZadatku();
                List<int> poz = service.VratiBrojPozitivihOcenaPoZadatku();

                ViewBag.Message = message;

                List<DataPoint> dataPoints1 = new List<DataPoint>();
                List<DataPoint> dataPoints2 = new List<DataPoint>();
                for (int i = 0; i < naziviZadataka.Count(); i++)
                {

                    if (zadaci.ElementAt(i).Status == "Ocenjen")
                        dataPoints1.Add(new DataPoint(zadaci.ElementAt(i).NazivZadatka, poz.ElementAt(i)));


                }
                for (int i = 0; i < naziviZadataka.Count(); i++)
                {
                    if (zadaci.ElementAt(i).Status == "Ocenjen")
                        dataPoints2.Add(new DataPoint(zadaci.ElementAt(i).NazivZadatka, ang.ElementAt(i)));
                }


                ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
                ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);

                return View();
            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }



        }
        [HttpPost]
        public IActionResult Zadaci(ZadatakFilter filters)
        {

            var zadaci = GetZadaci(filters);

            return PartialView(zadaci.ToList());

        }
        [AllowAnonymous]
        private List<Zadatak> GetZadaci(ZadatakFilter filters)
        {


            List<Zadatak> sviZadaci = service.GetAll().ToList();
            List<Zadatak> zadaci = new List<Zadatak>();

            if (!string.IsNullOrEmpty(filters.Zadatak))
            {
                zadaci = sviZadaci.Where(s => s.NazivZadatka.ToLower().Contains(filters.Zadatak.ToLower())
                || s.Teren.ToLower().Contains(filters.Zadatak.ToLower())).ToList();

                if (zadaci.Count() == 0) return zadaci;
            }


            if (zadaci.Count() == 0) zadaci = sviZadaci;
            return zadaci;


        }
        public IActionResult Details(int? id)
        {
            try
            {
                IEnumerable<Angazovanje> angazovanjaNaZadatku = serviceAngazovanje.GetAll().Where(a => a.ZadatakId == id).ToList();

                if (angazovanjaNaZadatku == null) throw new Exception("Zadatak čije podaatke zahtevate ne postoji!");





                ViewBag.ZadatakId = id;
                return View(angazovanjaNaZadatku);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }

        }
        
      
        [Authorize(Policy = "ZadatakPolicy")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Psi = servicePas.GetAll().ToList();
            PasDropDownList();
            return View();
        }
        
       // ako se naziv i datum poklapaju ne bi trebalo da ubaci zadatak
        public JsonResult InsertZadatak([FromBody] Object zadatak)
        {
            try
            {
               string zadatakZaDeserijalizaciju = zadatak.ToString();
               Zadatak z = Newtonsoft.Json.JsonConvert.DeserializeObject<Zadatak>(zadatakZaDeserijalizaciju);

                Zadatak zadatakZaUnos = new Zadatak()
                {
                    NazivZadatka = z.NazivZadatka,
                    Teren = z.Teren,
                    Datum = z.Datum,
                    Status = Enumerations.Status.Kreiran.ToString()
                };
                
          
                List<Angazovanje> angazovanjaZaUnos = new List<Angazovanje>();
                foreach(Angazovanje a in z.Angazovanja)
                {
                  
                    Pas pasIzBaze = servicePas.FindById(a.PasId);
                    Angazovanje angazovanje = new Angazovanje()
                    {
                        PasId = a.PasId,
                        ZadatakId = zadatakZaUnos.Id,
                        Zadatak = zadatakZaUnos,
                        DatumUnosaOcene = null,
                        Ocena = null,
                        Pas = pasIzBaze
                      
                       
                    };
                 
                    angazovanjaZaUnos.Add(angazovanje);

     
                }
             
                zadatakZaUnos.Angazovanja = angazovanjaZaUnos;
                service.Insert(zadatakZaUnos);
                
                return Json("Uspešno uneto!");
            }
            catch (Exception ex)
            {

                return Json("Došlo je do greške prilikom unosa!"+ex);
            }
        }

        private void ZadatakDropDownList(object izabranZadatak = null)
        {
            var zadatakQuery = from z in service.GetAll().AsQueryable()
                               orderby z.NazivZadatka
                               select z;
            ViewBag.ZadatakId = new SelectList(zadatakQuery.AsNoTracking(), "Id", "NazivZadatka", izabranZadatak);
        }

        private void PasDropDownList(object izabranPas=null)
        {
            var pasQuery = from p in servicePas.GetAll().AsQueryable()
                           orderby p.Obuka.Naziv
                           select p;
    
            ViewBag.PasId = new SelectList(pasQuery.AsNoTracking(), "Id", "FullName", izabranPas);
        }

        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null) throw new Exception("Zadatak čije podaatke zahtevate ne postoji!");

                var zadatak = service.FindById(id);// ?? throw new NotFoundCustomException("Pas čije podatke želite da izmenite ne postoji!", $"Proverite parametre za id: {id}");

                if (zadatak == null) throw new Exception("Zadatak čije podaatke zahtevate ne postoji!");

                return View(zadatak);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });


            }


        }
        [HttpPost]
        public IActionResult Edit(Zadatak zadatak)
        {



            try
            {
                if (ModelState.IsValid)
                {
                    var zadatakIzBaze = service.FindById(zadatak.Id);
                    if (zadatakIzBaze == null) return RedirectToAction("PageNotFound", "Home", new { message = "Zadatak čije podatke zahtevate ne postoji!" });
                    zadatakIzBaze.Angazovanja = zadatak.Angazovanja;
                    zadatakIzBaze.Datum = zadatak.Datum;
                    zadatakIzBaze.NazivZadatka = zadatak.NazivZadatka;
                    zadatakIzBaze.Teren = zadatak.Teren;
                    zadatakIzBaze.Status = zadatak.Status;
                    service.Update(zadatakIzBaze);
                    return RedirectToAction("Search", new { message = $"Izmene o psu: {zadatakIzBaze.NazivZadatka} uspešno sačuvane" });
                }

                return View(zadatak);
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

                if (id == null) throw new Exception("Zadatak čije podaatke zahtevate ne postoji!");
                var zadatakIzBaze = service.FindById(id);// ?? throw new NotFoundCustomException("Nisu pronađeni podaci", $"Proverite parametre za id: {id}");
                if (zadatakIzBaze == null) throw new Exception("Zadatak čije podaatke zahtevate ne postoji!");

                return View(zadatakIzBaze);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home", new { message = ex.Message });

            }


        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteZadatak(int? id)
        {
            try
            {
                if (id == null) throw new Exception("Zadatak čije podaatke zahtevate ne postoji!");
                var zadatakZaBrisanje = service.FindById(id);// ?? throw new NotFoundCustomException("Nisu pronađeni podaci", $"Proverite parametre za id: {id}");
                if (zadatakZaBrisanje == null) throw new Exception("Zadatak čije podaatke zahtevate ne postoji!");


                service.Delete(zadatakZaBrisanje.Id);
                return RedirectToAction("Index", new { message = $"Uspešno obrisani podaci o zadatku: {zadatakZaBrisanje.NazivZadatka}" });


            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home", new { message = ex.Message });

            }


        }


    }
    }

