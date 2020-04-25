using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories.Interfaces
{
    public interface IObukaRepository : IRepository<Obuka>
    {
        public void Save();
        public void Update(Obuka obuka);

        public void Delete(int ObukaId);

        public Obuka FindById(int? id);
        void Insert(Obuka t);
    }
}
