using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.ViewModels.ApplicationUserViewModels
{
    public class EditInstruktorViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public string ImePrezime { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public string Email { get; set; }
        public string Cin { get; set; }

        public int ObukaId { get; set; }

        public Obuka Obuka { get; set; }
    }
}
