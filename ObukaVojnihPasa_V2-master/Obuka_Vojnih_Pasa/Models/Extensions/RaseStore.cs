using Obuka_Vojnih_Pasa.ViewModels.PasViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.Extensions
{
    public class RaseStore
    {

        public static List<string> rase = new List<string>() { "Šarplaninac", "Nemački ovčar" ,
        "Belgijski ovčar","Labrador retriver"};

        public static List<RasaViewModel> SveRase = new List<RasaViewModel>()
        {
           new RasaViewModel()
            {
                NazivRase = "Šarplaninac",
                Opis = "Najpogodniji za obuku:  Zaštitna služba",
                Url = "https://sr.wikipedia.org/sr-ec/%D0%A8%D0%B0%D1%80%D0%BF%D0%BB%D0%B0%D0%BD%D0%B8%D0%BD%D0%B0%D1%86",
                Slika = new string("https://petface.net/wp-content/uploads/2017/12/%C5%A1ampionsko-petface-2.png")

            },
            new RasaViewModel()
            {
                NazivRase = "Nemački ovčar (Vučjak)",
                Opis = "Najpogodniji za obuku:  Pogodni za sve obuke",
                Url = "https://sr.wikipedia.org/sr-ec/%D0%9D%D0%B5%D0%BC%D0%B0%D1%87%D0%BA%D0%B8_%D0%BE%D0%B2%D1%87%D0%B0%D1%80",
                Slika =

                     new string("https://2.bp.blogspot.com/-si14E_IUb9M/UpNq0GyAFbI/AAAAAAAAHKU/Ykuo38iRorA/s1600/vojnik.jpg")
                    // new string("https://ljubiteljipasa.com/wp-content/uploads/2016/01/Nemacki-ovcar-4.jpg")

               


            },
                new RasaViewModel()
            {
                NazivRase = "Belgijski ovčar (Malinoa)",
                Opis = "Najpogodniji za obuku:  Pogodni za sve obuke",
                Url = "https://sr.wikipedia.org/sr/Malinoa",
                Slika = new string("https://frendizoo.com/images-oglasi/2017-09/oglas59bcfbaa70b3b.jpg")






            },
      new RasaViewModel()
            {
                NazivRase = "Labrador retriver",
                Opis = "Najpogodniji za obuku:  Tragačka služba",
                Url = "https://sr.wikipedia.org/sr-ec/%D0%9B%D0%B0%D0%B1%D1%80%D0%B0%D0%B4%D0%BE%D1%80_%D1%80%D0%B5%D1%82%D1%80%D0%B8%D0%B2%D0%B5%D1%80",
                Slika =
                   new string("https://www.zivotinjice.com/wp-content/uploads/2015/10/Labrador-retriver-umiljat-i-vrlo-popularan-pas-1024x682.jpg")
                 //  new string("https://www.superljubimac.rs/pictures/s5-labrador-stenci.jpg"),
                  // new string("https://www.juznevesti.com/uploads/assets/2015/03/05/46621/1280x0_vozni-penzionisani-psi.jpg")



               


            }

    };
    }
}
