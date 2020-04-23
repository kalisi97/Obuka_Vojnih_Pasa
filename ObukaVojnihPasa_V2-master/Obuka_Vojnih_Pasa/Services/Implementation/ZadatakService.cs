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
    public class ZadatakService:IZadatakService
    {
        private readonly IUnitOfWork unitOfWork;

        public ZadatakService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

       
        public void Delete(int zadatakIzBazeId)
        {
            unitOfWork.ZadatakRepository.Delete(zadatakIzBazeId);
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
            if (isValid(t) == false) throw new ArgumentOutOfRangeException("Nevalidan unos!");
           
            unitOfWork.ZadatakRepository.Insert(t);
            unitOfWork.Save();
        }

        private bool isValid(Zadatak t)
        {
            bool valid = true;
            if (t.Angazovanja == null || t.Angazovanja.Count() == 0) valid = false;
            if (t.Datum > DateTime.Now && t.Status != Enumerations.Status.Kreiran.ToString()) valid = false;
            if (t.Datum <= DateTime.Now && t.Status == Enumerations.Status.Kreiran.ToString()) valid = false;
            return valid;
        }

        public void Update(Zadatak zadatak)
        {
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
