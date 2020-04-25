using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories.Interfaces
{
    public interface IZadatakRepository:IRepository<Zadatak>
    {
        public void Save();
        public void Update(Zadatak zadatak);

        public void Delete(int ZadatakId);

        public Zadatak FindById(int? id);
       public List<int> VratiBrojAngazovanjaPoZadatku();
      public  List<int> VratiBrojPozitivihOcenaPoZadatku();
      public  List<string> VratiListuNazivaZadataka();
      public  void Insert(Zadatak t);
    }
}
