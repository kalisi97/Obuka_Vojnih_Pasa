using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Extensions;
using Obuka_Vojnih_Pasa.Models.Repositories;
using Obuka_Vojnih_Pasa.Services.Interafaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Services.Implementation
{
    public class ZadatakService : IZadatakService
    {
        private readonly IUnitOfWork unitOfWork;

        public ZadatakService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public void Delete(int zadatakIzBazeId)
        {
            Zadatak zadatak = unitOfWork.ZadatakRepository.FindById(zadatakIzBazeId);
            if (zadatak == null) throw new Exception();
            unitOfWork.ZadatakRepository.Delete(zadatak.Id);
            unitOfWork.Save();
        }

        public Zadatak FindById(int? id)
        {
            return unitOfWork.ZadatakRepository.FindById(id);
        }

        public IEnumerable<Zadatak> GetAll()
        {
            return unitOfWork.ZadatakRepository.GetAll();
        }

        public void Insert(Zadatak t)
        {
            if (isValid(t) == false ||
            t.Status != Enumerations.Status.Kreiran.ToString()
            || postojiUBazi(t)==true
            ) throw new ArgumentOutOfRangeException("Nevalidan unos!");
            
            unitOfWork.ZadatakRepository.Insert(t);
            unitOfWork.Save();
        }

        private bool postojiUBazi(Zadatak t)
        {
            bool postoji = false;
            if (t != null)
            {
               foreach(Zadatak z in GetAll().ToList())
                {
                    if (z.Datum.Equals(t.Datum) && z.NazivZadatka == t.NazivZadatka
                        && z.Teren == t.Teren) postoji = true;
                }
            }

            return postoji;
        }

        private bool isValid(Zadatak t)
        {
            bool valid = true;
            if (t == null) return false;
            if (string.IsNullOrEmpty(t.NazivZadatka) || string.IsNullOrEmpty(t.Teren)
            || string.IsNullOrEmpty(t.Status) || t.Datum == null || t.Angazovanja == null) valid = false;
            if (t.Status == Enumerations.Status.Kreiran.ToString() && t.Datum <= DateTime.Now) valid = false;   
            if (t.Angazovanja != null && t.Angazovanja.Count() == 0) valid = false;
        

            return valid;
        }
        public void Update(Zadatak zadatak)
        {
            if(isValid(zadatak) == false) throw new ArgumentOutOfRangeException("Nevalidan unos!");
            unitOfWork.ZadatakRepository.Update(zadatak);
            unitOfWork.Save();
        }

        public List<int> VratiBrojAngazovanjaPoZadatku()
        {
            return unitOfWork.ZadatakRepository.VratiBrojAngazovanjaPoZadatku();
        }

        public List<int> VratiBrojPozitivihOcenaPoZadatku()
        {
            return unitOfWork.ZadatakRepository.VratiBrojPozitivihOcenaPoZadatku();
        }

        public List<string> VratiListuNazivaZadataka()
        {
            return unitOfWork.ZadatakRepository.VratiListuNazivaZadataka();
        }
    }
}
