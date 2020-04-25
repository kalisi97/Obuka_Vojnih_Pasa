using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Services.Implementation
{
    public class InstruktorService : IInstruktorService
    {
        private readonly IUnitOfWork unitOfWork;

        public InstruktorService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Instruktor FindById(int? id)
        {
            return unitOfWork.InstruktorRepository.FindById(id);
        }

        public IEnumerable<Instruktor> GetAll()
        {
            return unitOfWork.InstruktorRepository.GetAll();
        }

       

        public void Update(Instruktor instruktorIzBaze)
        {
            unitOfWork.InstruktorRepository.Update(instruktorIzBaze);
            unitOfWork.Save();
        }
    }
}
