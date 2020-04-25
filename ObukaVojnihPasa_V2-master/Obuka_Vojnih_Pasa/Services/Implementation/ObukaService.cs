using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Services.Implementation
{
    public class ObukaService : IObukaService
    {
        private readonly IUnitOfWork unitOfWork;

        public ObukaService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Delete(int obukaIzBazeId)
        {
            Obuka obuka = unitOfWork.ObukaRepository.FindById(obukaIzBazeId);
            if (obuka == null) throw new Exception();
            unitOfWork.ObukaRepository.Delete(obuka.Id);
            unitOfWork.Save();
        }

        public Obuka FindById(int? id)
        {
          return unitOfWork.ObukaRepository.FindById(id);
        }

        public IEnumerable<Obuka> GetAll()
        {
            return unitOfWork.ObukaRepository.GetAll();
        }

        public void Insert(Obuka t)
        {
            if (isValid(t) == false) throw new ArgumentOutOfRangeException();
            unitOfWork.ObukaRepository.Insert(t);
            unitOfWork.Save();
        }

        private bool isValid(Obuka t)
        {
            bool valid = true;
           if (t == null) return false;
            if (string.IsNullOrEmpty(t.Naziv) || string.IsNullOrEmpty(t.Opis)
                || t.Trajanje < 1) valid = false;
            return valid;
        }

        public void Update(Obuka obuka)
        {
            if (isValid(obuka) == false) throw new ArgumentOutOfRangeException();

            unitOfWork.ObukaRepository.Update(obuka);
            unitOfWork.Save();
        }
    }
}
