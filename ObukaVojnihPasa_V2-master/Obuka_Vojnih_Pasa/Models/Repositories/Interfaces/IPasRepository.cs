using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories.Interfaces
{
    public interface IPasRepository:IRepository<Pas>
    {
        public void Save();
        public void Update(Pas pas);

        public void Delete(int PasId);

        public Pas FindById(int? id);
        public void PostaviAngazovanja(Pas pas);
        public double UspesnostPsaNaObavljenimZadacima(Pas pasIzBaze);
        public List<Pas> TriNajuspesnijaPsa();
      public  int VratiBrojOcena(Pas pasIzBaze, int v);
        List<Angazovanje> vratiAngazovanjaPoKriterijumu(string akcija, int? pasId);
    }
}
