using Moq;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using Obuka_Vojnih_Pasa.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Obuka_Vojnih_Pasa_Tests
{
    public class UnitTest1
    {

        Mock<IPasRepository> pasRepo = new Mock<IPasRepository>();
        Mock<IObukaRepository> obukaRepo = new Mock<IObukaRepository>();
        Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        List<Obuka> obuke = new List<Obuka>()
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



        [Fact]
        public void TestGetAll()
        {
            obukaRepo.Setup(x => x.GetAll()).Returns(obuke);
            unitOfWork.Setup(x => x.ObukaRepository).Returns(obukaRepo.Object);
            ObukaService service = new ObukaService(unitOfWork.Object);
            var result = service.GetAll();
            Assert.IsAssignableFrom<IEnumerable<Obuka>>(result);
            Assert.Equal(5, obuke.Count());
        }




        [Fact]
        public void TestInsertObuka()
        {
           
            obukaRepo.Setup(x => x.GetAll()).Returns(obuke);
            obukaRepo.Setup(repo => repo.FindById(It.IsAny<int>())).Returns((int i) => obuke.SingleOrDefault(x => x.Id == i));
            obukaRepo.Setup(i => i.Insert(It.IsAny<Obuka>())).Callback((Obuka item) =>
            {
               
                obuke.Add(item);
            }).Verifiable();
            unitOfWork.Setup(x => x.ObukaRepository).Returns(obukaRepo.Object);
            ObukaService service = new ObukaService(unitOfWork.Object);
            Obuka newObuka = new Obuka
            {
                Id = 6,
                Naziv = "Nova obuka",
                Opis = "U ovoj ulozi isprepleću se zadaci vojnih i policijskih pasa. Kako imaju vrlo dobar i istreniran njuh, ovakvi psi vrlo lako pronađu skrivene zabranjene supstance na graničnim prelazima, kontrlolama ili u zračnim lukama. Takođe, vrlo lako otkrivaju i opojna sredstva.",
                Trajanje = 9
            };
            service.Insert(newObuka);
            Obuka readObuka = service.FindById(6);

            Assert.Equal(6, obuke.Count());
            unitOfWork.Verify(s => s.Save(), Times.Once);
           
        }
    }
}
