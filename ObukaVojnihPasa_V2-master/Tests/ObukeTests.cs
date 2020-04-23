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

        Mock<IObukaRepository> obukaRepo = Mocks.GetMockObukaRepository();
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
            unitOfWork.Verify(s => s.Save(), Times.Once);

        }
    }
}
