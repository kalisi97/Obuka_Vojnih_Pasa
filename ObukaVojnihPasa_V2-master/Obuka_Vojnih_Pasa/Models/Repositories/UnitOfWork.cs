using Obuka_Vojnih_Pasa.Models.Repositories.Implementation;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ObukaPasaContext context;

        public UnitOfWork(ObukaPasaContext context)
        {
            this.context = context;
            InstruktorRepository = new InstruktorRepository(context);
            ObukaRepository = new ObukaRepository(context);
            PasRepository = new PasRepository(context);
            ZadatakRepository = new ZadatakRepository(context);
            AngazovanjeRepository = new AngazovanjeRepository(context);
        }

        public IInstruktorRepository InstruktorRepository { get; set; }
        public IObukaRepository ObukaRepository { get; set; }
        public IPasRepository PasRepository { get; set; }
        public IZadatakRepository ZadatakRepository { get; set; }
        public IAngazovanjeRepository AngazovanjeRepository { get; set; }
        public void Save()
        {
            context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
