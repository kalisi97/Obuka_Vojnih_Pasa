using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories.Implementation
{
    public class InMemoryInstruktorRepository : IInstruktorRepository
    {
        private List<Instruktor> instruktori = new List<Instruktor>();

        public Instruktor FindById(int? id)
        {
           return instruktori.Find(o => o.Id == id.ToString());
        }

        public IEnumerable<Instruktor> GetAll()
        {
            return instruktori;
        }

        public void Insert(Instruktor instruktor)
        {
            instruktori.Find(p => p.Id == instruktor.Id).ImePrezime = instruktor.ImePrezime;
            instruktori.Find(p => p.Id == instruktor.Id).Cin = instruktor.Cin;
            instruktori.Find(p => p.Id == instruktor.Id).Obuka = instruktor.Obuka;
            instruktori.Find(p => p.Id == instruktor.Id).ObukaId = instruktor.Obuka.Id;

        }

        public void Update(Instruktor instruktorIzBaze)
        {
            throw new NotImplementedException();
        }
    }
}
