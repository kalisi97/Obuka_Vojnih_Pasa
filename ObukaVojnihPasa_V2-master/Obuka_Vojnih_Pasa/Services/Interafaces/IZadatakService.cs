using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Services.Interafaces
{
   public interface IZadatakService:IService<Zadatak>
    {
        public void Update(Zadatak zadatak);
        public void Delete(int zadatakIzBazeId);
        public Zadatak FindById(int? id);
        public List<string> VratiListuNazivaZadataka();
        public List<int> VratiBrojAngazovanjaPoZadatku();
       public List<int> VratiBrojPozitivihOcenaPoZadatku();

    }
}
