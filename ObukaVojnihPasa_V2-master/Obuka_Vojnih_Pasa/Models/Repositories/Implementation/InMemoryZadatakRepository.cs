using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories.Implementation
{
    public class InMemoryZadatakRepository:IZadatakRepository
    {
        private List<Zadatak> zadaci = new List<Zadatak>();


        public void Delete(int Id)
        {

            zadaci.Remove(zadaci.Find(o => o.Id == Id));
        }

        public Zadatak FindById(int? id)
        {
            return zadaci.Find(p => p.Id == id);
        }

        public IEnumerable<Zadatak> GetAll()
        {
            return zadaci;
        }

        public void Insert(Zadatak t)
        {
            zadaci.Add(t);
        }

        public void Save()
        {

        }

        public void Update(Zadatak zadatak)
        { 
            zadaci.Find(z => z.Id == zadatak.Id).NazivZadatka = zadatak.NazivZadatka;
            zadaci.Find(z => z.Id == zadatak.Id).Teren = zadatak.Teren;
            zadaci.Find(z => z.Id == zadatak.Id).Datum = zadatak.Datum;
          

        }

        public List<int> VratiBrojAngazovanjaPoZadatku()
        {
            List<int> brojAngazovanjaPoZadatku = new List<int>();
            List<string> naziviZadataka = VratiListuNazivaZadataka();
            for (int i = 0; i < naziviZadataka.Count(); i++)
            {
                List<Zadatak> zadaciSaOvimTipom = new List<Zadatak>();
                foreach (Zadatak zadatak in GetAll())
                {
                    if (zadatak.NazivZadatka.Equals(naziviZadataka.ElementAt(i)))
                    {
                        zadaciSaOvimTipom.Add(zadatak);
                    }
                }

                foreach (Zadatak zadatak1 in zadaciSaOvimTipom)
                {
                    if (zadatak1.Angazovanja != null)
                    {
                        brojAngazovanjaPoZadatku.Add(zadatak1.Angazovanja.Count());
                    }
                }
            }
            return brojAngazovanjaPoZadatku;
        }

        public List<int> VratiBrojPozitivihOcenaPoZadatku()
        {
            List<int> brojPozitivhihOcenaPoZadatku = new List<int>();
            List<string> naziviZadataka = VratiListuNazivaZadataka();
            for (int i = 0; i < naziviZadataka.Count(); i++)
            {
                List<Zadatak> zadaciSaOvimTipom = new List<Zadatak>();
                foreach (Zadatak zadatak in GetAll())
                {
                    if (zadatak.NazivZadatka.Equals(naziviZadataka.ElementAt(i)))
                    {
                        zadaciSaOvimTipom.Add(zadatak);
                    }
                }


                foreach (Zadatak zadatak1 in zadaciSaOvimTipom)
                {
                    if (zadatak1.Angazovanja != null)
                    {
                        int pozitivneOcene = 0;
                        foreach (Angazovanje a in zadatak1.Angazovanja)
                        {
                            if (a.Ocena > 5) pozitivneOcene++;
                        }
                        brojPozitivhihOcenaPoZadatku.Add(pozitivneOcene);
                    }
                }
            }
            return brojPozitivhihOcenaPoZadatku;
        }

        public List<string> VratiListuNazivaZadataka()
        {
            List<string> naziviZadataka = new List<string>();
            foreach (Zadatak zadatak in GetAll())
            {
                if (!naziviZadataka.Contains(zadatak.NazivZadatka)) naziviZadataka.Add(zadatak.NazivZadatka);
            }
            return naziviZadataka;
        }
    }
}
