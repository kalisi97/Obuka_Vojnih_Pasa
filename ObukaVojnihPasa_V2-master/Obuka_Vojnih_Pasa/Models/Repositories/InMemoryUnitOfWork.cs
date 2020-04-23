using Obuka_Vojnih_Pasa.Models.Repositories.Implementation;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public InMemoryUnitOfWork()
        {

        }
        public IInstruktorRepository InstruktorRepository { get; set; } = new InMemoryInstruktorRepository();
        public IObukaRepository ObukaRepository { get; set; } = new InMemoryObukaRepository();
        public IPasRepository PasRepository { get; set; } = new InMemoryPasRepository();
        public IZadatakRepository ZadatakRepository { get; set; } = new InMemoryZadatakRepository();
        public IAngazovanjeRepository AngazovanjeRepository { get; set; } = new InMemoryAngazovanjeRepository();

        public void Dispose()
        {
          
        }

        public void Save()
        {

        }
    }
}
