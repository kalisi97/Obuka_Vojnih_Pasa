using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Extensions
{
    public class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
              {

            //ima smisla da bude claim unos zadatka

              
               new Claim("Unos zadatka","Unos zadatka"),
                   new Claim("Brisanje angažovanja","Brisanje angažovanja")
              };
    }
}
