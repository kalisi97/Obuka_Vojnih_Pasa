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


     

        public void InsertRange(List<Angazovanje> t)
        {
            unitOfWork.AngazovanjeRepository.InsertRange(t);
            unitOfWork.Save();
        }

        public void Update(Angazovanje angazovanje)
        {
            unitOfWork.AngazovanjeRepository.Update(angazovanje);
            unitOfWork.Save();
        }

        public Angazovanje GetById(int PasId, int ZadatakId)
        {
           Angazovanje a = unitOfWork.AngazovanjeRepository.GetById(PasId, ZadatakId);
            return a;
        }

        public void Delete(Angazovanje angazovanje)
        {
            unitOfWork.AngazovanjeRepository.Delete(angazovanje);
            unitOfWork.Save();
        }

        public void Insert(Angazovanje t)
        {
            unitOfWork.AngazovanjeRepository.Insert(t);
            unitOfWork.Save();
        }
    }
}
