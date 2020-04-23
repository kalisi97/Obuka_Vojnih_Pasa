using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.DTOModels
{
    public class ObukaDTO
    {
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int Trajanje { get; set; }
        public int BrojAngazovanihInstruktora { get; set; }
        public int BrojPasaNaObuci { get; set; }
    }
}
