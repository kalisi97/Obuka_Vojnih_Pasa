using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Obuka_Vojnih_Pasa.Models.Domain
{
 
    public class Instruktor:ApplicationUser
    {
     
        public string ImePrezime { get; set; }

     
        public string Cin { get; set; }
      
        public int ObukaId { get; set; }

        public Obuka Obuka { get; set; }
     
    }

  
}
