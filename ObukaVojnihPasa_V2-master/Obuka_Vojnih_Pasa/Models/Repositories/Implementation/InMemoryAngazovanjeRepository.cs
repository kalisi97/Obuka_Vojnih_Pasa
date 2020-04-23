using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories.Implementation
{
    public class InMemoryAngazovanjeRepository : IAngazovanjeRepository
    {
        private List<Angazovanje> angazovanja = new List<Angazovanje>();
        public IEnumerable<Angazovanje> GetAll()
        {
            return angazovanja;
        }

        public void InsertRange(List<Angazovanje> t)
        {
            angazovanja.AddRange(t);
        }

        public void Save()
        {

        }

        public void Update(Angazovanje a)
        {
            angazovanja.Find(an => an.PasId == a.PasId && an.ZadatakId == a.ZadatakId).Ocena = a.Ocena;
            angazovanja.Find(an => an.PasId == a.PasId && an.ZadatakId == a.ZadatakId).DatumUnosaOcene = a.DatumUnosaOcene;
           
        }

        public Angazovanje GetById(int PasId,int ZadatakId)
        {
            return angazovanja.Find(a => a.PasId == PasId && a.ZadatakId == ZadatakId);
        }

        public void Delete(Angazovanje angazovanje)
        {
            angazovanja.Remove(angazovanje);
        }

        public void Insert(Angazovanje t)
        {
            
            angazovanja.Add(t);
        }
    }
    }
    

