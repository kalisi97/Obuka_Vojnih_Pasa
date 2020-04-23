using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories
{
  public  interface IUnitOfWork:IDisposable
    {
        IInstruktorRepository InstruktorRepository { get; set; }

        IObukaRepository ObukaRepository { get; set; }
        IZadatakRepository ZadatakRepository { get; set; }
        IPasRepository PasRepository { get; set; }
        IAngazovanjeRepository AngazovanjeRepository  { get; set; }
        void Save();
    
    }
}
