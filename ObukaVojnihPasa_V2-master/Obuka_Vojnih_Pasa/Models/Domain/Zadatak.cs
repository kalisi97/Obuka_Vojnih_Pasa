using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Domain
{
    public class Zadatak
    {
        public int Id { get; set; }
        [DisplayName("Naziv zadatka")]
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public string NazivZadatka { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}")]
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public DateTime Datum { get; set; }
        public ICollection<Angazovanje> Angazovanja { get; set; }

        [DisplayName("Naziv terena")]
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public string Teren { get; set; }

        public string Status { get; set; }
      
    }
}
