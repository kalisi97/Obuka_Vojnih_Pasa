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
    public class LoginViewModel
    {

        //   public ApplicationUser User { get; set; }




        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
