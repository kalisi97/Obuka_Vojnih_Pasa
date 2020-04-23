using Microsoft.EntityFrameworkCore;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories.Implementation
{
    public class InstruktorRepository : BaseRepository<Instruktor>, IInstruktorRepository
    {
        private readonly ObukaPasaContext context;

        public InstruktorRepository(ObukaPasaContext context) : base(context)
        {
            this.context = context;
        }

        public Instruktor FindById(int? id)
        {
            try
            {
             
                Instruktor instruktor = context.Instruktori.Include(p => p.Obuka).ToList().Find(i=>i.Id==id.ToString());

                return instruktor;
            }
            catch (Exception ex)
            {
                throw new Exception($"Greška prilikom vraćanja instruktora! Greška: {ex.Message}");

            }
        }

        public override IEnumerable<Instruktor> GetAll()
        {
            try
            {
                return context.Instruktori.Include(i => i.Obuka);
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom vraćanja instruktora! Greška: {ex.Message}");
            }
        }

        public override void Insert(Instruktor t)
        {
            try
            {
                context.Add(t);
            }
            catch (Exception ex)
            {
                throw new Exception($"Greška prilikom unosa instruktora! Greška: {ex.Message}");
            }
        }

        public void Update(Instruktor instruktorIzBaze)
        {
            try
            {
                context.Entry(instruktorIzBaze).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom čuvanja izmena instruktora! Greška: {ex.Message}");
            }
        }
    }
}
