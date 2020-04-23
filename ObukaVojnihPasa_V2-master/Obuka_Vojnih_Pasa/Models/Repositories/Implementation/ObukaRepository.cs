using Microsoft.EntityFrameworkCore;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories.Implementation
{
    public class ObukaRepository : BaseRepository<Obuka>, IObukaRepository
    {
        private readonly ObukaPasaContext context;

        public ObukaRepository(ObukaPasaContext context) : base(context)
        {
            this.context = context;
        }

        public void Delete(int ObukaId)
        {

            try
            {
                Obuka obukaZaBrisanje = context.Obuke.ToList().SingleOrDefault(o => o.Id == ObukaId);

                context.Entry(obukaZaBrisanje).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom brisanja obuke! Greška: {ex.Message}");
            }

        }

        public Obuka FindById(int? id)
        {
            try
            {
               
                Obuka obuka = context.Obuke.ToList().SingleOrDefault((o => o.Id == id));

                return obuka;
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom vraćanja obuke! Greška: {ex.Message}");
            }
        }

        public override IEnumerable<Obuka> GetAll()
        {

            try
            {
                return context.Obuke.Include(o => o.Psi).Include(o => o.Instruktori);
            }
            catch (Exception ex)
            {
                throw new Exception($"Greška prilikom vraćanja obuka! Greška: {ex.Message}");
            }
        }

        public override void Insert(Obuka t)
        {
            try
            {
                context.Add(t);
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom unosa obuke! Greška: {ex.Message}");
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Greška prilikom čuvanja unete obuke! Greška: {ex.Message}");

            }
        }

        public void Update(Obuka obuka)
        {

            try
            {
                context.Entry(obuka).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception($"Greška prilikom čuvanja izmena obuke! Greška: {ex.Message}");
            }
        }
    }
}
