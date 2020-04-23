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
            unitOfWork.ObukaRepository.Delete(obukaIzBazeId);
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
            unitOfWork.ObukaRepository.Insert(t);
            unitOfWork.Save();
        }

        public void Update(Obuka obuka)
        {
            unitOfWork.ObukaRepository.Update(obuka);
            unitOfWork.Save();
        }
    }
}
