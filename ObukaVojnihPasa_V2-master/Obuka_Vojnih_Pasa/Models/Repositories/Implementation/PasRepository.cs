using Microsoft.EntityFrameworkCore;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories.Implementation
{
    public class PasRepository:BaseRepository<Pas>, IPasRepository
    {
        private readonly ObukaPasaContext context;

        public PasRepository(ObukaPasaContext context) : base(context)
        {
            this.context = context;
          
        }
        public void PostaviAngazovanja(Pas pas)
        {
            pas.Angazovanja = context.Angazovanja.Where(a => a.PasId == pas.Id).ToList();
        }
        public void Delete(int PasId)
        {
            try
            {

                Pas pasZaBrisanje = context.Psi.ToList().SingleOrDefault(p => p.Id == PasId);

                context.Entry(pasZaBrisanje).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw new Exception($"Greška prilikom brisanja psa! Greška: {ex.Message}");
            }

        }

        public Pas FindById(int? id)
        {
            try
            {
                if (id == null) return null;
                Pas pas = context.Psi.Include(p => p.Obuka).Include(p => p.Angazovanja).ToList().SingleOrDefault((p => p.Id == id));

                return pas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Greška prilikom vraćanja psa! Greška: {ex.Message}");

            }
        }

        public override IEnumerable<Pas> GetAll()
        {
            try
            {
                return context.Psi.Include(p => p.Obuka).Include(p => p.Angazovanja);
            }
            catch (Exception ex)
            {
                throw new Exception($"Greška prilikom vraćanja pasa! Greška: {ex.Message}");
            }
        }

        public  void Insert(Pas t)
        {
            try
            {
               
                context.Add(t);
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom unos psa! Greška: {ex.Message}");
            }
        }


        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom čuvanja psa! Greška: {ex.Message}");
            }
        }

        public void Update(Pas pas)
        {
            try
            {
                foreach (Angazovanje a in pas.Angazovanja)
                {
                    context.Entry(a).State = EntityState.Unchanged;
                }
                context.Entry(pas).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom čuvanja izmena psa! Greška: {ex.Message}");
            }
        }




        public double UspesnostPsaNaObavljenimZadacima(Pas pasIzBaze)
        {
            try
            {
                if (pasIzBaze == null) throw new Exception();
                int desetke = pasIzBaze.Angazovanja.Where(a => a.Ocena == 10).ToList().Count();
                int devetke = pasIzBaze.Angazovanja.Where(a => a.Ocena == 9).ToList().Count();
                int osmice = pasIzBaze.Angazovanja.Where(a => a.Ocena == 8).ToList().Count();
                int sedmice = pasIzBaze.Angazovanja.Where(a => a.Ocena == 7).ToList().Count();
                int sestice = pasIzBaze.Angazovanja.Where(a => a.Ocena == 6).ToList().Count();
                int petice = pasIzBaze.Angazovanja.Where(a => a.Ocena == 5).ToList().Count();
                int ukupno = pasIzBaze.Angazovanja.Count();

                double zbirOcena = desetke * 10 + devetke * 9 + osmice * 8 + sedmice * 7 + sestice * 6 - petice;
                double ukupnoA = (double)ukupno / 10;


                double skor = (double)zbirOcena * ukupnoA;

                return Math.Round(skor, 2);
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom računanja uspešnosti psa! Greška: {ex.Message}");
            }
        }

        public List<Pas> TriNajuspesnijaPsa()
        {
            try
            {
                List<Pas> sviPsi = GetAll().ToList();


                double max1 = 0;
                Pas pas1 = null;
                foreach (Pas p in sviPsi)
                {
                    PostaviAngazovanja(p);
                    if (p.Angazovanja.Count() != 0)
                    {
                        double uspesnost = UspesnostPsaNaObavljenimZadacima(p);
                        if (uspesnost > max1)
                        {
                            max1 = uspesnost;
                            pas1 = p;
                        }
                    }
                }
                sviPsi.Remove(pas1);
                double max2 = 0;
                Pas pas2 = null;
                foreach (Pas p in sviPsi)
                {
                    PostaviAngazovanja(p);
                    if (p.Angazovanja.Count() != 0)
                    {
                        double uspesnost = UspesnostPsaNaObavljenimZadacima(p);
                        if (uspesnost > max2)
                        {
                            max2 = uspesnost;
                            pas2 = p;
                        }

                    }
                }
                sviPsi.Remove(pas2);
                double max3 = 0;
                Pas pas3 = null;
                foreach (Pas p in sviPsi)
                {
                    PostaviAngazovanja(p);
                    if (p.Angazovanja.Count() != 0)
                    {
                        double uspesnost = UspesnostPsaNaObavljenimZadacima(p);
                        if (uspesnost > max3)
                        {
                            max3 = uspesnost;
                            pas3 = p;
                        }
                    }
                }

                List<Pas> tri = new List<Pas>();
                if (pas1 != null) tri.Add(pas1);
                if (pas2 != null) tri.Add(pas2);
                if (pas3 != null) tri.Add(pas3);
                return tri;
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom vraćanja tri najuspešnija psa! Greška: {ex.Message}");
            }
        }

        public int VratiBrojOcena(Pas pasIzBaze, int v)
        {
            try
            {

                int broj = pasIzBaze.Angazovanja.Where(a => a.Ocena == v).ToList().Count();


                return broj;
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom računanja broja ocena za psa! Greška: {ex.Message}");
            }
        }

        public List<Angazovanje> vratiAngazovanjaPoKriterijumu(string akcija, int? pasId)
        {
            try
            {
                if (pasId == null) throw new Exception("Pas čija angažovanja zahtevate ne postoji!"); ;
                Pas pas = FindById(pasId);
                if (pas == null) throw new Exception("Pas čija angažovanja zahtevate ne postoji!");
                if (string.IsNullOrEmpty(akcija)) throw new Exception();
                IEnumerable<Angazovanje> angazovanjaZaPsa = context.Angazovanja.Where(p => p.PasId == pasId).Include(a => a.Zadatak).ToList();

                if (akcija == "Uspesna")
                    return angazovanjaZaPsa.Where(a => a.Ocena > 5).ToList();
                if (akcija == "Neuspesna")
                    return angazovanjaZaPsa.Where(a => a.Ocena == 5).ToList();

                return angazovanjaZaPsa.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom vraćanja angažovanja za psa! Greška: {ex.Message}");

            }

        }
    }
}

