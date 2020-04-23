using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Repositories
{
   
        public interface IRepository<T> where T : class
        {
            IEnumerable<T> GetAll();
            void Insert(T t);
       }
    
}
