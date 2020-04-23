using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories.Implementation
{
    public class InMemoryObukaRepository : IObukaRepository
    {

        private List<Obuka> obuke = new List<Obuka>();

        public void Delete(int ObukaId)
        {

            obuke.Remove(obuke.Find(o => o.Id == ObukaId));
        }

        public Obuka FindById(int? id)
        {
            return obuke.Find(o => o.Id == id);
        }

        public IEnumerable<Obuka> GetAll()
        {
            return obuke;
        }

        public void Insert(Obuka t)
        {
            obuke.Add(t);
        }

        public void Save()
        {

        }

        public void Update(Obuka obuka)
        {
            obuke.Find(o => o.Id == obuka.Id).Naziv = obuka.Naziv;
            obuke.Find(o => o.Id == obuka.Id).Opis = obuka.Opis;

        }
    }
}
