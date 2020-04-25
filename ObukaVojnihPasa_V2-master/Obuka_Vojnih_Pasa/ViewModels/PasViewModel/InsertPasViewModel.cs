using Microsoft.AspNetCore.Mvc;
using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.ViewModels.PasViewModel
{
    public class InsertPasViewModel
    {
        public int Id { get; set; }

        [DisplayName("Zdravstvena knjizica")]
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
         [Remote(action: "NevalidanBrojKnjizice", controller: "Pas")]
        public string BrojZdravstveneKnjizice { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public string Ime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}")]

        [DisplayName("Datum rodjenja")]
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public DateTime DatumRodjenja { get; set; }
        public string Rasa { get; set; }
        public string Pol { get; set; }

        public int ObukaId { get; set; }

        public Obuka Obuka { get; set; }
        public ICollection<Angazovanje> Angazovanja { get; internal set; }
    }
}
