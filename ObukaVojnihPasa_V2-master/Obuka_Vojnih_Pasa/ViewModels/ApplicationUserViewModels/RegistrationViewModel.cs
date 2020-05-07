using Microsoft.AspNetCore.Mvc;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        [EmailAddress]
        [ValidEmailDomain(allowedDomain: "vs.com",
         ErrorMessage = "Email se mora završavati domenom: vs.com")]
        [Remote(action: "IsEmailValid", controller: "Account")]
       
        public string Email { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        [Display(Name = "Korisničko ime")]
        [Remote(action: "IsUsernameInUse", controller: "Account")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        [Display(Name = "Potvrdi lozinku")]
        [DataType(DataType.Password)]
        [Compare("Password",
            ErrorMessage = "Lozinke se ne poklapaju!")]
        public string ConfirmPassword { get; set; }



        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        [Display(Name = "Ime i prezime")]
        public string ImePrezime { get; set; }

        [Display(Name = "Čin u vojsci")]
        public string Cin { get; set; }

        [Browsable(false)]
        public int ObukaId { get; set; }

        public Obuka Obuka { get; set; }


    }
}
