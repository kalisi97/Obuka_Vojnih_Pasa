using Moq;
using Obuka_Vojnih_Pasa.Models.Domain;
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
   public class PasTests
    {
        Mock<IPasRepository> pasRepo = Mocks.GetMockPasRepository();
        Mock<IObukaRepository> obukaRepo = Mocks.GetMockObukaRepository();
        Mock<IUnitOfWork> unitOfWork = Mocks.GetMockUnitOfWork();

        [Fact]
        public void TestPasServiceGetAll()
        {
            var service = new PasService(unitOfWork.Object);
            var result = service.GetAll();
            var listaPasa = Assert.IsAssignableFrom<IEnumerable<Pas>>(result);
            Assert.NotEmpty(listaPasa);
        }

        [Fact]
        public void TestPasServiceFindById()
        {
           
            var service = new PasService(unitOfWork.Object);
            var result = service.FindById(2);
            var pasResult = Assert.IsType<Pas>(result);
            var expected = unitOfWork.Object.PasRepository.FindById(2);
            Assert.Equal(expected.Ime, pasResult.Ime);
        }
        [Fact]
        public void TestPasServiceFindByIdInvalid()
        {

            var service = new PasService(unitOfWork.Object);
            var result = service.FindById(222);
         
            Assert.Null(result);
        }
        [Fact]
        public void TestPasServiceInsertPas()
        {
          


            var newPas =  new Pas{
                Id = 8,
                Ime = "Lena",
                BrojZdravstveneKnjizice = "000765",
                Pol = "Ženski",
                Rasa = "Nemački ovčar",
                Obuka = obukaRepo.Object.FindById(5),
                ObukaId = 5,
                DatumRodjenja = new DateTime(2018, 11, 09)
            };

            var service = new PasService(unitOfWork.Object);
            service.Insert(newPas);
            var result = service.GetAll();
            Pas pas = service.FindById(newPas.Id);

            Assert.Equal(newPas.Id, pas.Id);
            unitOfWork.Verify(x => x.PasRepository.Insert(It.Is<Pas>(p => p.Ime == "Lena")),Times.Once);
            unitOfWork.Verify(x => x.Save(),Times.Once);
        }


        [Fact]
        public void TestPasServiceInsertInvalidPas()
        {



            var newPas = new Pas
            {
                Id = 10,
                Ime = "Lena",
                BrojZdravstveneKnjizice = "114190",
                Pol = "Muški",
                Rasa = "Akita",
                Obuka = obukaRepo.Object.FindById(5),
                ObukaId = 5,
                DatumRodjenja = new DateTime(2018, 11, 09)
            };
            var listaPasa = unitOfWork.Object.PasRepository.GetAll();
            var service = new PasService(unitOfWork.Object);
        
           Assert.Throws<ArgumentOutOfRangeException>(() => service.Insert(newPas));
            unitOfWork.Verify(x => x.PasRepository.Insert(It.IsAny<Pas>()), Times.Never);
            unitOfWork.Verify(s => s.Save(), Times.Never);
            Assert.DoesNotContain(listaPasa, x => x.Id == newPas.Id);
        }

        [Fact]
        public void TestPasServiceUpdatePas()
        {
            var service = new PasService(unitOfWork.Object);
         
            var pas = new Pas
            {
                Id=1,
                Ime = "Izmenjeno ime",
                BrojZdravstveneKnjizice = "110100",
                Pol = "Muški",
                Rasa = "Šarplaninac",
                Obuka = obukaRepo.Object.FindById(2),
                ObukaId = 2,
                DatumRodjenja = new DateTime(2019, 10, 10)
            };
            service.Update(pas);

            var pasIzmenjen = unitOfWork.Object.PasRepository.FindById(1);

            Assert.Equal(pas.Ime, pasIzmenjen.Ime);
            Assert.Equal(pas.BrojZdravstveneKnjizice,pasIzmenjen.BrojZdravstveneKnjizice);
            // sta ovde?
            unitOfWork.Verify(x => x.PasRepository.Update(It.IsAny<Pas>()), Times.Once);

            unitOfWork.Verify(s => s.Save(), Times.Once);
        }



        [Fact]
        public void TestPasServiceUpdateInvalidPas()
        {
            var service = new PasService(unitOfWork.Object);

            var pas = new Pas
            {
                Id = 1,
                Ime = "Boni",
                BrojZdravstveneKnjizice = "110100",
                Pol = "Muški",
                Rasa = "Šarplaninac",
             
                DatumRodjenja = new DateTime(2019, 10, 10)
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => service.Update(pas));
            unitOfWork.Verify(s => s.PasRepository.Update(It.IsAny<Pas>()), Times.Never);
            unitOfWork.Verify(s => s.Save(), Times.Never);

        }


        [Fact]
        public void TestPasServiceDeletePas()
        {
            var service = new PasService(unitOfWork.Object);
            int idToDelete = 1;
            service.Delete(idToDelete);
            var result = unitOfWork.Object.PasRepository.FindById(idToDelete);
            var listaPasa = unitOfWork.Object.PasRepository.GetAll();
            Assert.Null(result);
            Assert.DoesNotContain(listaPasa, x => x.Id == idToDelete);
            unitOfWork.Verify(s => s.PasRepository.Delete(idToDelete), Times.Once);
            unitOfWork.Verify(s => s.Save(), Times.Once);

        }
        [Fact]

        public void TestPasServiceDeleteInvalidPas()
        {
            var service = new PasService(unitOfWork.Object);
            int idToDelete = 99;
            Assert.Throws<Exception>(() => service.Delete(idToDelete));
            unitOfWork.Verify(s => s.PasRepository.Delete(idToDelete), Times.Never);
            unitOfWork.Verify(s => s.Save(), Times.Never);

        }

    }
}
