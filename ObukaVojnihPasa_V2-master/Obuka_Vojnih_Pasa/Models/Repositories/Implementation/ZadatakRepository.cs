using Microsoft.EntityFrameworkCore;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories.Implementation
{
    public class ZadatakRepository:BaseRepository<Zadatak>,IZadatakRepository
    {
        private readonly ObukaPasaContext context;

        public ZadatakRepository(ObukaPasaContext context) : base(context)
        {
            this.context = context;
        }


        public void Delete(int ZadatakId)
        {

            try
            {
               
                Zadatak ZaBrisanje = context.Zadaci.ToList().SingleOrDefault(p => p.Id == ZadatakId);

                context.Entry(ZaBrisanje).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom brisanja zadatka! Greška: {ex.Message}");
            }

        }

        public Zadatak FindById(int? id)
        {
            try
            {
               
                Zadatak z = context.Zadaci.AsQueryable().AsNoTracking()
                    .Include(z => z.Angazovanja).ToList().Where((p => p.Id == id)).FirstOrDefault();
                
                return z;
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom vraćanja zadatka! Greška: {ex.Message}");
            }
        }

        public override IEnumerable<Zadatak> GetAll()
        {
            try
            {
                return context.Zadaci.AsNoTracking().Include(z => z.Angazovanja).ThenInclude(a=>a.Pas);
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom vraćanja zadataka! Greška: {ex.Message}");
            }
        }

        public override void Insert(Zadatak t)
        {
            try
            {

                context.Add(t);
                foreach(Angazovanje a in t.Angazovanja)
                {
                    context.Angazovanja.Add(a);
                }
          
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom unosa zadatka! Greška: {ex.Message}");
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

                throw new Exception($"Greška prilikom čuvanja zadatka! Greška: {ex.Message}");
            }
        }

        public void Update(Zadatak zadatak)
        {
            try
            {
                foreach (Angazovanje a in zadatak.Angazovanja)
                {
                    context.Entry(a).State = EntityState.Unchanged;
                }
                context.Entry(zadatak).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom izmena zadatka! Greška: {ex.Message}");
            }
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
                int broj = 0;
                foreach (Zadatak zadatak1 in zadaciSaOvimTipom)
                {
                    if (zadatak1.Angazovanja != null)
                    {
                        broj += zadatak1.Angazovanja.Count();

                    }
                }
                brojAngazovanjaPoZadatku.Add(broj);
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

                int pozitivneOcene = 0;
                foreach (Zadatak zadatak1 in zadaciSaOvimTipom)
                {
                    if (zadatak1.Angazovanja != null)
                    {

                        foreach (Angazovanje a in zadatak1.Angazovanja)
                        {
                            if (a.Ocena > 5) pozitivneOcene++;
                        }

                    }
                }
                brojPozitivhihOcenaPoZadatku.Add(pozitivneOcene);
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
