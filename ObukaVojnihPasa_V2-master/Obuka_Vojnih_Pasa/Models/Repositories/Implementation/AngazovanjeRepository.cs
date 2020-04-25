using Microsoft.EntityFrameworkCore;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories.Implementation
{
    public class AngazovanjeRepository : IAngazovanjeRepository
    {
        private readonly ObukaPasaContext context;

        public AngazovanjeRepository(ObukaPasaContext context)
        {
            this.context = context;
        }

        public void Delete(Angazovanje angazovanje)
        {
            try
            {

                context.Entry(angazovanje).State = EntityState.Deleted;
             
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom brisanja angažovanja! Greška: {ex.Message}");
            }
        }

        public IEnumerable<Angazovanje> GetAll()
        {
            try
            {
                return context.Angazovanja.AsNoTracking().Include(a => a.Zadatak).Include(a => a.Pas).ThenInclude(p => p.Obuka);
            } catch (Exception ex)
            {
                throw new Exception($"Nisu vraćena sva angažovanja! Greška: {ex.Message}");
            }
        }

        public Angazovanje GetById(int PasId, int ZadatakId)
        {
            try
            {
                return context.Angazovanja.Include(a => a.Zadatak).Include(a => a.Pas).ThenInclude(p => p.Obuka).ToList().Find(a => a.PasId == PasId && a.ZadatakId == ZadatakId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Greška prilikom učitavanja angažovanja! Greška: {ex.Message}");
            }
        }

        public void Insert(Angazovanje t)
        {
            try
            {
                
                context.Add(t);
            }
            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom unosa angažovanja! Greška: {ex.Message}");
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

                throw new Exception($"Greška prilikom čuvanja angažovanja! Greška: {ex.Message}");
            }
        }

        public void Update(Angazovanje a)
        {
            try
            {
            


        context.Entry(a).State = EntityState.Modified;
        }




            catch (Exception ex)
            {

                throw new Exception($"Greška prilikom čuvanja promene podataka o  angažovanju! Greška: {ex.Message}");
            }

        }
    }
    }

