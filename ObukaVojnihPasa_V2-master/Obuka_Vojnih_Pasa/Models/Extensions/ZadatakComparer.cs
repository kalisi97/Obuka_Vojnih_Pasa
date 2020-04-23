using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Extensions
{
    public class ZadatakComparer: IEqualityComparer<Zadatak>
    {
        public bool Equals(Zadatak x, Zadatak y)
        {


            if (Object.ReferenceEquals(x, y)) return true;


            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;


            return x.NazivZadatka == y.NazivZadatka;
        }



        public int GetHashCode(Zadatak Zadatak)
        {

            if (Object.ReferenceEquals(Zadatak, null)) return 0;


            int hashZadatakName = Zadatak.NazivZadatka == null ? 0 : Zadatak.NazivZadatka.GetHashCode();




            return hashZadatakName;
        }
    }
}

