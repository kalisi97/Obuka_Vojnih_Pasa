using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly ObukaPasaContext context;

        public BaseRepository(ObukaPasaContext context)
        {
            this.context = context;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }

       
    }
}
