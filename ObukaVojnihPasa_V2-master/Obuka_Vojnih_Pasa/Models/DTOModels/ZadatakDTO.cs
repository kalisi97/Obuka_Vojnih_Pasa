using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.DTOModels
{
    public class ZadatakDTO
    {
        public string NazivZadatka { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}")]
        public DateTime Datum { get; set; }
        public string Teren { get; set; }
        public int UkupanBrojAngazovanja { get; set; }
    }
}
