using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.DTOModels
{
    public class PasDTO
    {
        public string BrojZdravstveneKnjizice { get; set; }
        public string Ime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}")]

        public DateTime DatumRodjenja { get; set; }
        public string Rasa { get; set; }
        public string Pol { get; set; }

        public int UkupanBrojAngazovanja { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int Trajanje { get; set; }

       // public ICollection<Angazovanje> Angazovanja { get; set; }
    }
}
