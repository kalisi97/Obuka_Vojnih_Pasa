using Moq;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Repositories;
using Obuka_Vojnih_Pasa.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests
{
    public class AngazovanjeTests
    {
        Mock<IUnitOfWork> unitOfWork = Mocks.GetMockUnitOfWork();

        [Fact]
        public void TestAngazovanjeServiceFindById()
        {

            var service = new AngazovanjeService(unitOfWork.Object);
            var result = service.GetById(1,1);
            var resultAngazovanje = Assert.IsType<Angazovanje>(result);
            var expected = unitOfWork.Object.AngazovanjeRepository.GetById(1,1);
            Assert.Equal(expected.PasId, resultAngazovanje.PasId);
            Assert.Equal(expected.ZadatakId, resultAngazovanje.ZadatakId);
        }
        [Fact]
        public void TestAngazovanjeServiceFindByIdInvalid()
        {

            var service = new AngazovanjeService(unitOfWork.Object);
            var result = service.GetById(-9, 1);
           
            Assert.Null(result);
        }

        [Fact]
        public void TestAngazovanjeServiceOceniAngazovanje()
        {
            var service = new AngazovanjeService(unitOfWork.Object);
         
            var angazovanje = new Angazovanje
            {

             
                ZadatakId = 1,
                Pas = unitOfWork.Object.PasRepository.FindById(1),
                PasId = 1,
                Zadatak = unitOfWork.Object.ZadatakRepository.FindById(1),
                DatumUnosaOcene = DateTime.Now,
                Ocena = 10
            };
            service.Update(angazovanje);

            var angazovanjeOcenjeno = unitOfWork.Object.AngazovanjeRepository.GetById(1,1);

            Assert.NotNull(angazovanjeOcenjeno.Ocena);
            Assert.NotNull(angazovanjeOcenjeno.DatumUnosaOcene);
            unitOfWork.Verify(s => s.AngazovanjeRepository.Update(It.IsAny<Angazovanje>()), Times.Once);
            unitOfWork.Verify(s => s.Save(), Times.Once);
        }

        [Fact]
        public void TestAngazovanjeServiceOceniAngazovanjeInvalid()
        {
            var service = new AngazovanjeService(unitOfWork.Object);


            //ocena je nevalidna -> -2
            var angazovanje = new Angazovanje
            {


                ZadatakId = 1,
                Pas = unitOfWork.Object.PasRepository.FindById(1),
                PasId = 1,
                Zadatak = unitOfWork.Object.ZadatakRepository.FindById(1),
                DatumUnosaOcene = DateTime.Now,
               Ocena = -2
            };
       
            Assert.Throws<ArgumentOutOfRangeException>(() => service.Update(angazovanje));
            unitOfWork.Verify(s => s.AngazovanjeRepository.Update(It.IsAny<Angazovanje>()), Times.Never);
            unitOfWork.Verify(s => s.Save(), Times.Never);
        }

        [Fact]
        public void TestAngazovanjeServiceDelete()
        {
            var service = new AngazovanjeService(unitOfWork.Object);
            var toDelete = unitOfWork.Object.AngazovanjeRepository.GetById(1, 1);
            service.Delete(toDelete);
            var result = unitOfWork.Object.AngazovanjeRepository.GetById(1, 1);
            var lista = unitOfWork.Object.AngazovanjeRepository.GetAll();
            Assert.Null(result);
            Assert.DoesNotContain(lista, x => x.PasId == toDelete.PasId && x.ZadatakId == toDelete.ZadatakId);
            unitOfWork.Verify(s => s.AngazovanjeRepository.Delete(toDelete), Times.Once);
            unitOfWork.Verify(s => s.Save(), Times.Once);

        }

        [Fact]
        public void TestAngazovanjeServiceDeleteInvalid()
        {
            var service = new AngazovanjeService(unitOfWork.Object);
            // pasId is invalid id
            var toDelete = unitOfWork.Object.AngazovanjeRepository.GetById(100, 1);
            Assert.Throws<Exception>(() => service.Delete(toDelete));
            unitOfWork.Verify(s => s.AngazovanjeRepository.Delete(toDelete), Times.Never);
            unitOfWork.Verify(s => s.Save(), Times.Never);

        }
    }
}
