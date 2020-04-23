using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.DTOModels;
using Obuka_Vojnih_Pasa.Services.Interafaces;

namespace Obuka_Vojnih_Pasa.Controllers.APIController
{
    [Route("api/zadatak")]
    [ApiController]
    public class ZadatakController : ControllerBase
    {
        private readonly IZadatakService _service;
        private readonly IMapper _mapper;

        public ZadatakController(IZadatakService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpGet]

        public ActionResult<IEnumerable<Zadatak>> Get()
        {
            var zadaci = _service.GetAll().ToList();
            var zadaciDTO = _mapper.Map<IEnumerable<ZadatakDTO>>(zadaci);
            return Ok(zadaciDTO);
        }


        [HttpPost]
        public ActionResult<Zadatak> Post([FromBody]ZadatakDTO model)
        {
            var zadatak = _mapper.Map<Zadatak>(model);
            _service.Insert(zadatak);


            return Created("api/zadatak", null);

        }


        [HttpDelete("{name}")]
        public ActionResult<Zadatak> Delete(string name)
        {
            var data = _service.GetAll().FirstOrDefault(d => d.NazivZadatka.Equals(name));
            if (data == null) return NotFound();

            _service.Delete(data.Id);

            return NoContent();
        }

        [HttpPut("{naziv}")]

        public ActionResult<Pas> Put(string ime, [FromBody] ZadatakDTO model)
        {
            var data = _service.GetAll().FirstOrDefault(d => d.NazivZadatka.Equals(ime));
            if (data == null) return NotFound();

            data.NazivZadatka = model.NazivZadatka;
            data.Teren = model.Teren;
            data.Datum = model.Datum;
         

            _service.Update(data);
            return NoContent();
        }


    }
}