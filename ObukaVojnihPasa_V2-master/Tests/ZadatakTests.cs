using Moq;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Extensions;
using Obuka_Vojnih_Pasa.Models.Repositories;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using Obuka_Vojnih_Pasa.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests
{
   public class ZadatakTests
    {
        //Initialize
  
       
        Mock<IUnitOfWork> unitOfWork = Mocks.GetMockUnitOfWork();

        [Fact]
        public void TestZadatakServiceFindById()
        {

            var service = new ZadatakService(unitOfWork.Object);
            var result = service.FindById(2);
            var zadatakResult = Assert.IsType<Zadatak>(result);
            var expected = unitOfWork.Object.ZadatakRepository.FindById(2);
            Assert.Equal(expected, zadatakResult);
        }
        [Fact]
        public void TestZadatakServiceFindByIdInvalid()
        {

            var service = new ZadatakService(unitOfWork.Object);
            var result = service.FindById(null);

            Assert.Null(result);
        }

        [Fact]
        public void TestZadatakServiceInsertZadatak()
        {

            //Arrange
            var newZadatak = new Zadatak()
            {
                Id = 3,
                Datum = new DateTime(2020, 04, 29),
                Status = Enumerations.Status.Kreiran.ToString(),
                Teren = "Niš",
                NazivZadatka = "Kontrola policijskog časa"
            };
            newZadatak.Angazovanja = new List<Angazovanje>()
            { new Angazovanje()
            {
                    DatumUnosaOcene = null,
                            Ocena = null,
                            ZadatakId = 1,
                            Pas = unitOfWork.Object.PasRepository.FindById(1),
                            PasId = 1,
                            Zadatak = newZadatak
            }, new Angazovanje()
            {
                    DatumUnosaOcene = null,
                            Ocena = null,
                            ZadatakId = 1,
                            Pas =  unitOfWork.Object.PasRepository.FindById(2),
                            PasId = 2,
                            Zadatak = newZadatak
            },
             new Angazovanje()
            {
                    DatumUnosaOcene = null,
                            Ocena = null,
                            ZadatakId = 1,
                            Pas =  unitOfWork.Object.PasRepository.FindById(3),
                            PasId = 3,
                            Zadatak = newZadatak
            }
            };
            var service = new ZadatakService(unitOfWork.Object);
            //Act
            service.Insert(newZadatak);
            var result = service.GetAll();
            var zadaci = unitOfWork.Object.ZadatakRepository.GetAll();

            Zadatak zadatak = service.FindById(newZadatak.Id);
            //Assert
            Assert.Equal(newZadatak.Id, zadatak.Id);
            Assert.Equal(zadaci.Count(), result.Count());
            unitOfWork.Verify(x => x.Save(), Times.Once);
            unitOfWork.Verify(x => x.ZadatakRepository.Insert(It.Is<Zadatak>(p=>p.Id==3)),Times.Once);
        }

        [Fact]
        public void TestZadatakServiceInsertInvalidZadatak()
        {
            var newZadatak = new Zadatak()
            {
                Id = 3,
                Datum = new DateTime(2020, 04, 22),
                Status = Enumerations.Status.Kreiran.ToString(),
                Teren = "Niš"
     
            };

            var zadaci = unitOfWork.Object.ZadatakRepository.GetAll();
            var service = new ZadatakService(unitOfWork.Object);
            Assert.Throws<ArgumentOutOfRangeException>(() => service.Insert(newZadatak));
            unitOfWork.Verify(x => x.ZadatakRepository.Insert(It.IsAny<Zadatak>()),Times.Never);
            unitOfWork.Verify(s => s.Save(), Times.Never);
            Assert.DoesNotContain(zadaci, x => x.Id == newZadatak.Id);
        }

        [Fact]

        public void TestZadatakServiceDeleteZadatak()
        {
            var service = new ZadatakService(unitOfWork.Object);
            int idToDelete = 1;
            service.Delete(idToDelete);
            var result = unitOfWork.Object.ZadatakRepository.FindById(idToDelete);
            var zadaci = unitOfWork.Object.ZadatakRepository.GetAll();
            Assert.Null(result);
            Assert.DoesNotContain(zadaci, x => x.Id == idToDelete);
            unitOfWork.Verify(s=>s.ZadatakRepository.Delete(idToDelete),Times.Once);
            unitOfWork.Verify(s => s.Save(), Times.Once);
        }

        [Fact]

        public void TestZadatakServiceDeleteInvalidIdZadatak()
        {
            var service = new ZadatakService(unitOfWork.Object);
            int idToDelete = 99;
            Assert.Throws<Exception>(() => service.Delete(idToDelete));
            unitOfWork.Verify(s => s.ZadatakRepository.Delete(idToDelete), Times.Never);
            unitOfWork.Verify(s => s.Save(), Times.Never);
           
        }
    }
}
