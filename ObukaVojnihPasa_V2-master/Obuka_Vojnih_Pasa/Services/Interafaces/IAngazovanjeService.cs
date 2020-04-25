using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Services.Interafaces
{
  public  interface IAngazovanjeService:IService<Angazovanje>
    {
        public void Update(Angazovanje angazovanje);
        public Angazovanje GetById(int PasId, int ZadatakId);
        public void Delete(Angazovanje ang);
        public void Insert(Angazovanje a);
    }
}
