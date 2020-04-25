using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories;
using Obuka_Vojnih_Pasa.Services.Interafaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Services.Implementation
{
    public class AngazovanjeService : IAngazovanjeService
    {
        private readonly IUnitOfWork unitOfWork;

        public AngazovanjeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }





        public IEnumerable<Angazovanje> GetAll()
        {
            return unitOfWork.AngazovanjeRepository.GetAll();
        }


     

    

        public void Update(Angazovanje angazovanje)
        {
            if (isValid(angazovanje) == false) throw new ArgumentOutOfRangeException();
            unitOfWork.AngazovanjeRepository.Update(angazovanje);
            unitOfWork.Save();
        }

        private bool isValid(Angazovanje angazovanje)
        {
            bool valid = true;
            if (angazovanje == null) return false;
            if (angazovanje.Ocena == null || angazovanje.DatumUnosaOcene == null ||
                angazovanje.Zadatak == null || angazovanje.Pas == null) return false;
            if (angazovanje.DatumUnosaOcene < angazovanje.Zadatak.Datum) valid = false;
            if (angazovanje.Ocena < 5 || angazovanje.Ocena > 10) valid = false;
            return valid;
        }

        public Angazovanje GetById(int PasId, int ZadatakId)
        {
           Angazovanje a = unitOfWork.AngazovanjeRepository.GetById(PasId, ZadatakId);
   
            return a;
        }

        public void Delete(Angazovanje angazovanje)
        {
            if (angazovanje == null) throw new Exception();
            Angazovanje a = unitOfWork.AngazovanjeRepository.GetById(angazovanje.PasId, angazovanje.ZadatakId);
            if (a == null) throw new Exception();
            unitOfWork.AngazovanjeRepository.Delete(angazovanje);
            unitOfWork.Save();
        }

        public void Insert(Angazovanje a)
        {
            if (isValidUnos(a) == false) throw new ArgumentOutOfRangeException();
            unitOfWork.AngazovanjeRepository.Insert(a);
            unitOfWork.Save();
        }

        private bool isValidUnos(Angazovanje angazovanje)
        {
            bool valid = true;
            if (angazovanje == null) return false;
            if (angazovanje.Ocena != null || angazovanje.DatumUnosaOcene != null ||
                angazovanje.Zadatak == null || angazovanje.Pas == null) valid = false;
            return valid;
        }
    }
}
