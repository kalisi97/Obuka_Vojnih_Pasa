using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Services
{
    public interface IObukaService : IService<Obuka>
    {
        public void Update(Obuka obuka);
        public void Delete(int obukaIzBazeId);
       public Obuka FindById(int? id);
    }
}
