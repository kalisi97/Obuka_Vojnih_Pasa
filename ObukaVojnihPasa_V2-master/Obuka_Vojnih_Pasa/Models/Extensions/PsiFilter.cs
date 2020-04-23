using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Extensions
{
    public class PsiFilter
    {
        public string Pas { get; set; }
        public int ObukaId { get; set; }

        public int PasId { get; set; }

        public string Akcija { get; set; }
        public string Operacija { get; set; }
    }
}
