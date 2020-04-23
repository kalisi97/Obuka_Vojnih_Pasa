using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.DTOModels
{
    public class AngazovanjaDTO
    {


        public DateTime? DatumUnosaOcene { get; set; }
        public int? Ocena { get; set; }
        public string BrojZdravstveneKnjizice { get; set; }
        public string Ime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}")]

        public DateTime DatumRodjenja { get; set; }
        public string Rasa { get; set; }
        public string Pol { get; set; }
        public string NazivZadatka { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}")]
        public DateTime Datum { get; set; }


        //   public ICollection<Angazovanje> Angazovanja { get; set; }


        public string Teren { get; set; }


    }
}
