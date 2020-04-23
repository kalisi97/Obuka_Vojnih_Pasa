using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories.Implementation
{
    public class InMemoryPasRepository:IPasRepository
    {
        private List<Pas> psi = new List<Pas>();

      

        public void Delete(int PasId)
        {

            psi.Remove(psi.Find(o => o.Id == PasId));
        }

        public Pas FindById(int? id)
        {
            return psi.Find(p => p.Id == id);
        }

        public IEnumerable<Pas> GetAll()
        {
            return psi;
        }

        public void Insert(Pas t)
        {
           psi.Add(t);
        }

        public void PostaviAngazovanja(Pas pas)
        {
        

        }

        public void Save()
        {

        }

        public List<Pas> TriNajuspesnijaPsa()
        {
            List<Pas> sviPsi = psi;


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

        public void Update(Pas pas)
        {
            psi.Find(p => p.Id == pas.Id).Ime = pas.Ime;
            psi.Find(p => p.Id == pas.Id).Pol = pas.Pol;
            psi.Find(p => p.Id == pas.Id).Rasa = pas.Rasa;
            psi.Find(p => p.Id == pas.Id).Obuka = pas.Obuka;
            psi.Find(p => p.Id == pas.Id).ObukaId = pas.Obuka.Id;
            psi.Find(p => p.Id == pas.Id).DatumRodjenja = pas.DatumRodjenja;
            psi.Find(p => p.Id == pas.Id).Angazovanja = pas.Angazovanja;
            psi.Find(p => p.Id == pas.Id).BrojZdravstveneKnjizice = pas.BrojZdravstveneKnjizice;


        }

        public double UspesnostPsaNaObavljenimZadacima(Pas pasIzBaze)
        {

            int desetke = pasIzBaze.Angazovanja.Where(a => a.Ocena == 10).ToList().Count();
            int devetke = pasIzBaze.Angazovanja.Where(a => a.Ocena == 9).ToList().Count();
            int osmice = pasIzBaze.Angazovanja.Where(a => a.Ocena == 8).ToList().Count();
            int sedmice = pasIzBaze.Angazovanja.Where(a => a.Ocena == 7).ToList().Count();
            int sestice = pasIzBaze.Angazovanja.Where(a => a.Ocena == 6).ToList().Count();
            int petice = pasIzBaze.Angazovanja.Where(a => a.Ocena == 5).ToList().Count();
            int ukupno = pasIzBaze.Angazovanja.Count();

            return ((desetke * 10 + devetke * 9 + osmice * 8 + sedmice * 7 + sestice * 6 - petice) * (ukupno / 10));

        }

        public List<Angazovanje> vratiAngazovanjaPoKriterijumu(string akcija, int? pasId)
        {
            Pas pas = FindById(pasId);

            IEnumerable<Angazovanje> angazovanjaZaPsa = pas.Angazovanja;

            if (akcija == "Uspesna")
                return angazovanjaZaPsa.Where(a => a.Ocena > 5).ToList();
            if (akcija == "Neuspesna")
                return angazovanjaZaPsa.Where(a => a.Ocena == 5).ToList();

            return angazovanjaZaPsa.ToList();
        }

        public int VratiBrojOcena(Pas pasIzBaze, int v)
        {
            int broj = pasIzBaze.Angazovanja.Where(a => a.Ocena == v).ToList().Count();
            return broj;
        }
    }
}
