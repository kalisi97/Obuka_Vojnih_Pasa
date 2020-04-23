using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.DTOModels
{
    public class InstruktorDTO
    {
        public string ImePrezime { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Cin { get; set; }

        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int Trajanje { get; set; }
        public int BrojAngazovanihInstruktora { get; set; }
        public int BrojPasaNaObuci { get; set; }

    }
}
