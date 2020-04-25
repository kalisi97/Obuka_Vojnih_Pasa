using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Domain
{
   
    public class Angazovanje
    {
        public int PasId { get; set; }
        public Pas Pas { get; set; }
        public int ZadatakId { get; set; }
        public Zadatak Zadatak { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}", NullDisplayText = "Datum još uvek nije unet")]
        public DateTime? DatumUnosaOcene { get; set; }
        [Range(5,10)]
        [DisplayFormat(NullDisplayText = "Ocena još uvek nije uneta!")]
        public int? Ocena { get; set; }
    }
}
