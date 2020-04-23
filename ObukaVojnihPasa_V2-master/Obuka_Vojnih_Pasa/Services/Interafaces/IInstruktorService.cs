using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Services
{
    public interface IInstruktorService : IService<Instruktor>
    {
        public void Update(Instruktor instruktorIzBaze);

        public Instruktor FindById(int? id);
    }
}
