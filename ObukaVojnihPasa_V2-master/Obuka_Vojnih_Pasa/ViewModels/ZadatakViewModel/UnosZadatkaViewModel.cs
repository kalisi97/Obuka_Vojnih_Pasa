using Microsoft.AspNetCore.Mvc;
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
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        [DisplayName("Naziv zadatka")]
        public string NazivZadatka { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}")]
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        [Remote(action: "NevalidanUnosDatuma", controller: "Zadatak")]
        public DateTime Datum { get; set; }

        [DisplayName("Naziv terena")]
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public string Teren { get; set; }
        public string Status { get; set; }
        public int PasId { get; set; }
        public string Ime { get; set; }

    }
}
