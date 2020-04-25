using Moq;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories;
using Obuka_Vojnih_Pasa.Models.Repositories.Interfaces;
using Obuka_Vojnih_Pasa.Services.Implementation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class ObukeTests
    {

       
        Mock<IUnitOfWork> unitOfWork = Mocks.GetMockUnitOfWork();

        [Fact]
        public void TestObukaServiceGetAll()
        {

            ObukaService service = new ObukaService(unitOfWork.Object);
            var result = service.GetAll();
            var lista = Assert.IsAssignableFrom<IEnumerable<Obuka>>(result);
            Assert.Equal<int>(5, lista.Count());
     
        }
        [Fact]
        public void TestObukaServiceFindById()
        {
          
            var service = new ObukaService(unitOfWork.Object);
            var result = service.FindById(2);
            var listaObukaResult = Assert.IsType<Obuka>(result);
            var expected = unitOfWork.Object.ObukaRepository.FindById(2);
            Assert.Equal(expected.Naziv, listaObukaResult.Naziv);
        }
        [Fact]
        public void TestObukaServiceFindByIdInvalid()
        {

            var service = new ObukaService(unitOfWork.Object);
            var result = service.FindById(null);
            Assert.Null(result);
        }
        [Fact]
        public void TestInsertObuka()
        {

    
         
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
            var obuka = service.FindById(6);
            Assert.Equal("Nova obuka",obuka.Naziv);
            unitOfWork.Verify(x => x.ObukaRepository.Insert(It.Is<Obuka>(p => p.Id == 6)), Times.Once);
            unitOfWork.Verify(s => s.Save(), Times.Once);

        }

        [Fact]
        public void TestInsertInvalidObuka()
        {



            ObukaService service = new ObukaService(unitOfWork.Object);
            Obuka newObuka = new Obuka
            {
                Id = 6,
                Naziv = "Nova obuka",
                Opis = "U ovoj ulozi isprepleću se zadaci vojnih i policijskih pasa. Kako imaju vrlo dobar i istreniran njuh, ovakvi psi vrlo lako pronađu skrivene zabranjene supstance na graničnim prelazima, kontrlolama ili u zračnim lukama. Takođe, vrlo lako otkrivaju i opojna sredstva.",
                Trajanje = -2
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => service.Insert(newObuka));
            unitOfWork.Verify(x => x.ObukaRepository.Insert(It.IsAny<Obuka>()), Times.Never);

            unitOfWork.Verify(s => s.Save(), Times.Never);

        }

        [Fact]
        public void TestObukaServiceUpdateObuka()
        {
            var service = new ObukaService(unitOfWork.Object);

            var obuka = new Obuka
            {
                Id = 1,
                Naziv = "Zaštitna služba",
                Opis = "U ovoj ulozi isprepleću se zadaci vojnih i policijskih pasa. Kako imaju vrlo dobar i istreniran njuh, ovakvi psi vrlo lako pronađu skrivene zabranjene supstance na graničnim prelazima, kontrlolama ili u zračnim lukama. Takođe, vrlo lako otkrivaju i opojna sredstva.",
                Trajanje = 7
            };
            service.Update(obuka);

            var obukaUpdated = unitOfWork.Object.ObukaRepository.FindById(1);

            Assert.Equal(obuka.Naziv, obukaUpdated.Naziv);
            Assert.Equal(obuka.Opis, obukaUpdated.Opis);
            Assert.Equal(obuka.Trajanje, obukaUpdated.Trajanje);
            unitOfWork.Verify(x => x.ObukaRepository.Update(It.IsAny<Obuka>()), Times.Once);

            unitOfWork.Verify(s => s.Save(), Times.Once);
        }

        [Fact]
        public void TestObukaServiceUpdateInvalidObuka()
        {
            var service = new ObukaService(unitOfWork.Object);

            var obuka = new Obuka
            {
                Id = 1,
                Naziv = "Zaštitna služba",
                Opis = "U ovoj ulozi isprepleću se zadaci vojnih i policijskih pasa. Kako imaju vrlo dobar i istreniran njuh, ovakvi psi vrlo lako pronađu skrivene zabranjene supstance na graničnim prelazima, kontrlolama ili u zračnim lukama. Takođe, vrlo lako otkrivaju i opojna sredstva.",
                Trajanje = -7
            };
            
Assert.Throws<ArgumentOutOfRangeException>(()=> service.Update(obuka));
            unitOfWork.Verify(x => x.ObukaRepository.Update(It.IsAny<Obuka>()), Times.Never);

            unitOfWork.Verify(s => s.Save(), Times.Never);
        }

        [Fact]
        public void TestObukaServiceDeleteObuka()
        {
            var service = new ObukaService(unitOfWork.Object);
            int idToDelete = 1;
            service.Delete(idToDelete);
            var result = unitOfWork.Object.ObukaRepository.FindById(idToDelete);
            var lista = unitOfWork.Object.ObukaRepository.GetAll();
            Assert.Null(result);
            Assert.DoesNotContain(lista, x => x.Id == idToDelete);
            unitOfWork.Verify(s => s.ObukaRepository.Delete(idToDelete), Times.Once);
            unitOfWork.Verify(s => s.Save(), Times.Once);

        }
        [Fact]
        public void TestObukaServiceDeleteInvalidObuka()
        {
            var service = new ObukaService(unitOfWork.Object);
            int idToDelete = -2;
            Assert.Throws<Exception>(() => service.Delete(idToDelete));
            unitOfWork.Verify(s => s.ObukaRepository.Delete(idToDelete), Times.Never);
            unitOfWork.Verify(s => s.Save(), Times.Never);

        }
    }
}
