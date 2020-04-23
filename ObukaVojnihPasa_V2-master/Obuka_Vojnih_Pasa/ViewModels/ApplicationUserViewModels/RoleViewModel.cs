using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "Naziv uloge")]
        public string RoleName{ get; set; }
    }
}
