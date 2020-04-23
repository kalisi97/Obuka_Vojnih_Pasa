using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories.Interfaces
{
    public interface IInstruktorRepository : IRepository<Instruktor>
    {
       public Instruktor FindById(int? id);
       public void Update(Instruktor instruktorIzBaze);
    }
}
