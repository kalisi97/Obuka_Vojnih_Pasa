using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Services
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();
      
    }
}
