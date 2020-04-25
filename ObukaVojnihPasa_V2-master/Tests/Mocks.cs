using Moq;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
   public class Mocks
    {
        public static Mock<IObukaRepository> GetMockObukaRepository()
        {
           var obuke = new List<Obuka>()
        {
            new Obuka
            {
                Id = 1,
                Naziv = "Tragačka služba",
                Opis = "Upotreba pasa za otkrivanje i praćenje tragova mirisa predstavlja najstariju vrstu upotrebe ovih životinja. Prema saznanjima moderne odorologije i kinologije, čulo mirisa psa je višestruko istančanije nego kod ljudi, a samo osećanje mirisa zavisi od njegove granične koncentracije koja se određuje brojem molekula mirisa u jedinici zapremine.",
                Trajanje = 5

            },
            new Obuka
            {
                Id = 2,
                Naziv = "Zaštitna služba",
                Opis = "Ovu službu čine psi koji se dresiraju za potrebe izvođenja hapšenja agresivnih lica kao i zaštite ovlašćenih službenih lica prilikom obavljanja patrolne delatnosti ili drugih intervencija. Drugim rečima, zaštitni pas može se koristiti i za potrebe napada i za potrebe odbrane.",
                Trajanje = 6
            },
              new Obuka
            {
                Id = 3,
                Naziv = "Čuvarska služba",
                Opis="Jedna od prvih zadataka pasa u vojsci bila je da danju i posebno noću čuvaju vojne kapmove. Psi bi lajali i zavijali ukoliko bi se stranci kretali u blizini kampa i tako upozorili na potencijalnu opasnost. Ovu obuku pohađa veliki broj pasa i može smatrati jednom od najbitnijih obuka.",
                Trajanje = 4

            },
             new Obuka
            {
                Id = 4,
                Naziv = "Pronalaženje opojnih sredstava",
                Opis="U ovoj ulozi isprepleću se zadaci vojnih i policijskih pasa. Kako imaju vrlo dobar i istreniran njuh, ovakvi psi vrlo lako pronađu skrivene zabranjene supstance na graničnim prelazima, kontrlolama ili u zračnim lukama. Takođe, vrlo lako otkrivaju i opojna sredstva.",
                Trajanje = 9

            },
             new Obuka()
             {
                 Id=5,
                 Naziv="Pronalaženje eksploziva",
                 Opis="Veliki broj pasa koristi se u razotkrivanju mina i ekspoziva. Psi minotragači obučavaju se tako da se koriste ogoljene električne žice ispod tla gde električna energija delomično udari psa te on na taj način uči da se opasnost krije ispod površine. Međutim, pronalaženje mina za pse je izrazito stresna aktivnost, tako da je mogu obavljati tek u intervalu oko pola sata nakon čega se moraju odmoriti.",
                 Trajanje = 7
             }
        };

            var mockObukaRepository = new Mock<IObukaRepository>();
            mockObukaRepository.Setup(repo => repo.GetAll()).Returns(obuke);
            mockObukaRepository.Setup(repo => repo.FindById(It.IsAny<int>())).Returns((int i)=>obuke.SingleOrDefault(x=>x.Id==i));
            mockObukaRepository.Setup(i => i.Insert(It.IsAny<Obuka>())).Callback((Obuka item) =>
            {
               
                obuke.Add(item);
            });
            mockObukaRepository.Setup(m => m.Update(It.IsAny<Obuka>())).Callback((Obuka target) =>
            {
                var original = obuke.FirstOrDefault(
                    q => q.Id == target.Id);

                if (original != null)
                {
                    original.Naziv = target.Naziv;
                    original.Trajanje = target.Trajanje;
                    original.Opis = target.Opis;

                }

            }).Verifiable();
            mockObukaRepository.Setup(m => m.Delete(It.IsAny<int>())).Callback((int? i) =>
            {

                var original = obuke.FirstOrDefault(
                    q => q.Id == i);

                if (original != null)
                {
                    obuke.Remove(original);
     
                }



            }).Verifiable();
            return mockObukaRepository;
        }
        public static Mock<IPasRepository> GetMockPasRepository()
        {
            var psi = new List<Pas>()
        {
            new Pas
            {
                Id = 1,
                Ime = "Boni",
                BrojZdravstveneKnjizice = "110100",
                Pol = "Muški",
                Rasa="Šarplaninac",
                Obuka = GetMockObukaRepository().Object.FindById(1),
                ObukaId = 1,
                DatumRodjenja = new DateTime(2020,10,10)
               

            },
            new Pas
            {
                 Id = 2,
                Ime = "Leo",
                BrojZdravstveneKnjizice = "110200",
                Pol = "Muški",
                Rasa="Labrador-retriver",
                Obuka = GetMockObukaRepository().Object.FindById(2),
                ObukaId = 2,
                DatumRodjenja = new DateTime(2019,10,10)
            },
              new Pas
            {
                Id = 3,
                Ime = "Lara",
                BrojZdravstveneKnjizice = "110190",
                Pol = "Ženski",
                Rasa="Belgijski ovčar",
                Obuka = GetMockObukaRepository().Object.FindById(3),
                ObukaId = 3,
                DatumRodjenja = new DateTime(2020,11,10)

            },
             new Pas
            {
                  Id = 4,
                Ime = "Lana",
                BrojZdravstveneKnjizice = "113190",
                Pol = "Ženski",
                Rasa="Nemački ovčar",
                Obuka = GetMockObukaRepository().Object.FindById(4),
                ObukaId = 4,
                DatumRodjenja = new DateTime(2020,11,09)

            },
               new Pas
            {
                  Id = 5,
                Ime = "Maks",
                BrojZdravstveneKnjizice = "112290",
                Pol = "Muški",
                Rasa="Belgijski ovčar",
                Obuka = GetMockObukaRepository().Object.FindById(3),
                ObukaId = 3,
                DatumRodjenja = new DateTime(2020,12,09)

            },
                  new Pas
            {
                  Id = 6,
                Ime = "Mara",
                BrojZdravstveneKnjizice = "192290",
                Pol = "Ženski",
                Rasa="Labrador-retriver",
                Obuka = GetMockObukaRepository().Object.FindById(2),
                ObukaId = 2,
                DatumRodjenja = new DateTime(2018,12,09)

            },
                       new Pas
            {
                  Id = 7,
                Ime = "Mara",
                BrojZdravstveneKnjizice = "792290",
                Pol = "Ženski",
                Rasa="Labrador-retriver",
                Obuka = GetMockObukaRepository().Object.FindById(5),
                ObukaId = 5,
                DatumRodjenja = new DateTime(2018,12,01)

            }
        };

            var mockPasRepository = new Mock<IPasRepository>();
            mockPasRepository.Setup(repo => repo.GetAll()).Returns(psi);
            mockPasRepository.Setup(repo => repo.FindById(It.IsAny<int>())).Returns((int? i) => {
                if (i != null)
                    return psi.SingleOrDefault(x => x.Id == i);
                else return null;
            });
            mockPasRepository.Setup(i => i.Insert(It.IsAny<Pas>())).Callback((Pas item) => {
           
                psi.Add(item);

            }).Verifiable();
            mockPasRepository.Setup(m => m.Update(It.IsAny<Pas>())).Callback((Pas target) =>
            {
                var original = psi.FirstOrDefault(
                    q => q.Id == target.Id);

                
                    original.Ime = target.Ime;
                    original.Pol = target.Pol;
                    original.Rasa = target.Rasa;
                    original.DatumRodjenja = target.DatumRodjenja;
                    original.Angazovanja = target.Angazovanja;
                    original.BrojZdravstveneKnjizice = target.BrojZdravstveneKnjizice;
                    original.Obuka = target.Obuka;
                    original.ObukaId = target.ObukaId;
                

            }).Verifiable();
            mockPasRepository.Setup(m => m.Delete(It.IsAny<int>())).Callback((int? i) =>
            {

                var original = psi.FirstOrDefault(
                    q => q.Id == i);

                if (original != null)
                {
                    psi.Remove(original);
                    foreach (Angazovanje a in GetMockAngazovanjeRepository().Object.GetAll())
                    {
                        if (a.PasId == original.Id)
                        {
                            GetMockAngazovanjeRepository().Object.Delete(a);
                        }
                    }
                }
               

         
            }).Verifiable();
  
            return mockPasRepository;
        }
        public static Mock<IZadatakRepository> GetMockZadatakRepository()
        {
            #region Initialize 
            var zadaci = new List<Zadatak>();
            Zadatak z1 = new Zadatak()
            {
                Id = 1,
                Datum = new DateTime(2020, 04, 22),
                Status = "Završen",
                Teren = "Beograd",
                NazivZadatka = "Kontrola policijskog časa"
            };
            z1.Angazovanja = new List<Angazovanje>()
            { new Angazovanje()
            {
                    DatumUnosaOcene = null,
                            Ocena = null,
                            ZadatakId = 1,
                            Pas = GetMockPasRepository().Object.FindById(1),
                            PasId = 1,
                            Zadatak = z1
            }, new Angazovanje()
            {
                    DatumUnosaOcene = null,
                            Ocena = null,
                            ZadatakId = 1,
                            Pas = GetMockPasRepository().Object.FindById(2),
                            PasId = 2,
                            Zadatak = z1
            },
             new Angazovanje()
            {
                    DatumUnosaOcene = null,
                            Ocena = null,
                            ZadatakId = 1,
                            Pas = GetMockPasRepository().Object.FindById(3),
                            PasId = 3,
                            Zadatak = z1
            }
            };
            zadaci.Add(z1);
            Zadatak z2 = new Zadatak()
            {
                Id = 2,
                Datum = new DateTime(2020, 05, 22),
                Status = "Kreiran",
                Teren = "Beograd",
                NazivZadatka = "Kontrola policijskog časa"
            };
            z1.Angazovanja = new List<Angazovanje>()
            { new Angazovanje()
            {
                    DatumUnosaOcene = null,
                            Ocena = null,
                            ZadatakId = 2,
                            Pas = GetMockPasRepository().Object.FindById(3),
                            PasId = 3,
                            Zadatak = z2
            }, new Angazovanje()
            {
                    DatumUnosaOcene = null,
                            Ocena = null,
                            ZadatakId = 2,
                            Pas = GetMockPasRepository().Object.FindById(2),
                            PasId = 2,
                            Zadatak = z2
            },
             new Angazovanje()
            {
                    DatumUnosaOcene = null,
                            Ocena = null,
                            ZadatakId = 2,
                            Pas = GetMockPasRepository().Object.FindById(5),
                            PasId = 5,
                            Zadatak = z2
            }
            };
            zadaci.Add(z2);
            #endregion

            var mockZadatakRepository = new Mock<IZadatakRepository>();
            mockZadatakRepository.Setup(repo => repo.GetAll()).Returns(zadaci);
            mockZadatakRepository.Setup(repo => repo.FindById(It.IsAny<int>())).Returns((int? i) => {
                if (i != null)
                    return zadaci.SingleOrDefault(x => x.Id == i);
                else return null;
            });
            mockZadatakRepository.Setup(i => i.Insert(It.IsAny<Zadatak>())).Callback((Zadatak item) =>
            {

                zadaci.Add(item);
                foreach (Angazovanje a in item.Angazovanja)
                {
                    GetMockAngazovanjeRepository().Object.Insert(a);
                }

            });
            mockZadatakRepository.Setup(m => m.Update(It.IsAny<Zadatak>())).Callback((Zadatak target) =>
            {
                var original = zadaci.FirstOrDefault(
                    q => q.Id == target.Id);


                original.NazivZadatka = target.NazivZadatka;
                original.Datum = target.Datum;
                original.Status = target.Status;
                original.Teren = target.Teren;
                original.Angazovanja = target.Angazovanja;


            }).Verifiable();
            mockZadatakRepository.Setup(m => m.Delete(It.IsAny<int>())).Callback((int? i) =>
            {

                var original = zadaci.FirstOrDefault(
                    q => q.Id == i);

                if (original != null)
                {
                   zadaci.Remove(original);
                    foreach(Angazovanje a in GetMockAngazovanjeRepository().Object.GetAll())
                    {
                        if(a.ZadatakId == original.Id)
                        {
                            GetMockAngazovanjeRepository().Object.Delete(a);
                        }
                    }
                }



            }).Verifiable();
            return mockZadatakRepository;

        }
        public static Mock<IAngazovanjeRepository> GetMockAngazovanjeRepository()
        {
            var angazovanja = new List<Angazovanje>()
            {
            new Angazovanje()
            {
                    DatumUnosaOcene = null,
                            Ocena = null,
                            ZadatakId = 1,
                            Pas = GetMockPasRepository().Object.FindById(1),
                            PasId = 1,
                            Zadatak = GetMockZadatakRepository().Object.FindById(1)
            }, new Angazovanje()
            {
                    DatumUnosaOcene = null,
                            Ocena = null,
                            ZadatakId = 1,
                            Pas = GetMockPasRepository().Object.FindById(2),
                            PasId = 2,
                            Zadatak = GetMockZadatakRepository().Object.FindById(1)
            },
             new Angazovanje()
            {
                    DatumUnosaOcene = null,
                            Ocena = null,
                            ZadatakId = 1,
                            Pas = GetMockPasRepository().Object.FindById(3),
                            PasId = 3,
                            Zadatak = GetMockZadatakRepository().Object.FindById(1)
            },
             new Angazovanje()
            {
                    DatumUnosaOcene = null,
                            Ocena = null,
                            ZadatakId = 2,
                            Pas = GetMockPasRepository().Object.FindById(3),
                            PasId = 3,
                            Zadatak =GetMockZadatakRepository().Object.FindById(2)
            }, new Angazovanje()
            {
                    DatumUnosaOcene = null,
                            Ocena = null,
                            ZadatakId = 2,
                            Pas = GetMockPasRepository().Object.FindById(2),
                            PasId = 2,
                            Zadatak = GetMockZadatakRepository().Object.FindById(2)
            },
             new Angazovanje()
            {
                    DatumUnosaOcene = null,
                            Ocena = null,
                            ZadatakId = 2,
                            Pas = GetMockPasRepository().Object.FindById(5),
                            PasId = 5,
                            Zadatak =GetMockZadatakRepository().Object.FindById(2)
            }
            };
            var mockAngazovanjeRepository = new Mock<IAngazovanjeRepository>();
            mockAngazovanjeRepository.Setup(repo => repo.GetAll()).Returns(angazovanja);
            mockAngazovanjeRepository.Setup(repo => repo.GetById(It.IsAny<int>(),It.IsAny<int>())).Returns((int? p, int? z) => {
                if (p != null && z != null)
                    return angazovanja.SingleOrDefault(x => x.PasId == p && x.ZadatakId == z);
                else return null;
            });
            
            mockAngazovanjeRepository.Setup(i => i.Insert(It.IsAny<Angazovanje>())).Callback((Angazovanje item) => {
                angazovanja.Add(item);

            }).Verifiable();
            
            mockAngazovanjeRepository.Setup(m => m.Update(It.IsAny<Angazovanje>())).Callback((Angazovanje target) =>
            {
                var original = angazovanja.FirstOrDefault(
                    q => q.PasId == target.PasId && q.ZadatakId == target.ZadatakId);


                original.Zadatak = target.Zadatak;
                original.Pas = target.Pas;
                original.ZadatakId = target.ZadatakId;
                original.PasId = target.PasId;
                original.Ocena = target.Ocena;
                original.DatumUnosaOcene = target.DatumUnosaOcene;


            }).Verifiable();
            mockAngazovanjeRepository.Setup(m => m.Delete(It.IsAny<Angazovanje>())).Callback((Angazovanje item) =>
            {

                var original = angazovanja.FirstOrDefault(
                    q => q.PasId == item.PasId && q.ZadatakId == item.ZadatakId);

                if (original != null)
                {
                    angazovanja.Remove(original);
                }



            }).Verifiable();
            return mockAngazovanjeRepository;
        }
        public static Mock<IUnitOfWork> GetMockUnitOfWork()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.ObukaRepository).Returns(GetMockObukaRepository().Object);
            unitOfWork.Setup(x => x.PasRepository).Returns(GetMockPasRepository().Object);
            unitOfWork.Setup(x => x.ZadatakRepository).Returns(GetMockZadatakRepository().Object);
            unitOfWork.Setup(x => x.AngazovanjeRepository).Returns(GetMockAngazovanjeRepository().Object);
            unitOfWork.Setup(x => x.Save()).Verifiable();
            return unitOfWork;
        }
    }
}
