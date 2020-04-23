using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.ViewModels.ZadatakViewModel
{
    public class UnosZadatkaViewModel
    {
        public int Id { get; set; }
        [DisplayName("Naziv zadatka")]
        public string NazivZadatka { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}")]
        public DateTime Datum { get; set; }

        [DisplayName("Naziv terena")]
        public string Teren { get; set; }
        public string Status { get; set; }
        public int PasId { get; set; }
        public string Ime { get; set; }

    }
}
