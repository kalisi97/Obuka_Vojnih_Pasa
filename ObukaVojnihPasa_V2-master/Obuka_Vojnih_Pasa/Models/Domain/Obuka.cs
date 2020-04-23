using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Domain
{
    public class Obuka
    {
        public int Id { get; set; }

        [DisplayName("Naziv obuke")]
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int Trajanje { get; set; }
    
      //  public string imageUrl { get; set; }
       public ICollection<Instruktor> Instruktori { get; set; }
      
        public ICollection<Pas> Psi { get; set; }
    }
}
