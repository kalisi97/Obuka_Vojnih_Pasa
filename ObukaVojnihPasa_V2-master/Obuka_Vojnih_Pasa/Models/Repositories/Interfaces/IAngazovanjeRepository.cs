using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories.Interfaces
{
    public interface IAngazovanjeRepository:IRepository<Angazovanje>
    {
        public void Save();
        public void Update(Angazovanje a);

        public Angazovanje GetById(int PasId, int ZadatakId);
        void Delete(Angazovanje angazovanje);
        public void InsertRange(List<Angazovanje> angazovanja);
    }
}
