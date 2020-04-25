using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Extensions;
using Obuka_Vojnih_Pasa.Models.Repositories;
using Obuka_Vojnih_Pasa.Services.Interafaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Services.Implementation
{
    public class PasService:IPasService
    {
        private readonly IUnitOfWork unitOfWork;

        public PasService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Delete(int pasIzBazeId)
        {
            Pas pas = unitOfWork.PasRepository.FindById(pasIzBazeId);
            if (pas == null) throw new Exception();
            unitOfWork.PasRepository.Delete(pasIzBazeId);
            unitOfWork.Save();
        }

        public Pas FindById(int? id)
        {
                
                return unitOfWork.PasRepository.FindById(id);
            
          
        }

        public IEnumerable<Pas> GetAll()
        {
            return unitOfWork.PasRepository.GetAll();
        }

        public void Insert(Pas t)
        {
            if(isValid(t)==false || unitOfWork.PasRepository.GetAll().SingleOrDefault(p => p.BrojZdravstveneKnjizice == t.BrojZdravstveneKnjizice) != null) throw new ArgumentOutOfRangeException("Nevalidan unos!");
            unitOfWork.PasRepository.Insert(t);
            unitOfWork.Save();
        }

        private bool isValid(Pas t)
        {
            bool valid = true;
            if (t == null) return false;
            if (string.IsNullOrEmpty(t.BrojZdravstveneKnjizice) ||
                string.IsNullOrEmpty(t.Pol) || string.IsNullOrEmpty(t.Ime)
                || string.IsNullOrEmpty(t.Rasa) || t.Obuka == null) valid = false;
            if (t.Pol == "Muški" || t.Pol == "Ženski") valid = true;
            else valid = false;
            if(unitOfWork.ObukaRepository.FindById(t.ObukaId) == null) valid = false;
            if (!RaseStore.rase.Contains(t.Rasa)) valid = false;
            if (t.DatumRodjenja == null) valid = false;
          if(t.DatumRodjenja !=null && t.DatumRodjenja > DateTime.Now) valid = false;
            return valid;
        }

        public void PostaviAngazovanja(Pas pas)
        {
            unitOfWork.PasRepository.PostaviAngazovanja(pas);
        }

        public List<Pas> TriNajuspesnijaPsa()
        {
            return unitOfWork.PasRepository.TriNajuspesnijaPsa();
        }

        public void Update(Pas pas)
        {
            if (isValid(pas) == false) throw new ArgumentOutOfRangeException("Nevalidan unos!");

            unitOfWork.PasRepository.Update(pas);
            unitOfWork.Save();
        }

        public double UspesnostPsaNaObavljenimZadacima(Pas pasIzBaze)
        {

            return unitOfWork.PasRepository.UspesnostPsaNaObavljenimZadacima(pasIzBaze);

        }

        public List<Angazovanje> vratiAngazovanjaPoKriterijumu(string akcija, int? pasId)
        {
            return unitOfWork.PasRepository.vratiAngazovanjaPoKriterijumu(akcija, pasId);
        }

        public int VratiBrojOcena(Pas pasIzBaze, int v)
        {
            return unitOfWork.PasRepository.VratiBrojOcena(pasIzBaze, v);
        }
    }
}
