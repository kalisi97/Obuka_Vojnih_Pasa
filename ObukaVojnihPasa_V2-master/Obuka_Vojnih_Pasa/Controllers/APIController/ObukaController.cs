using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.DTOModels;
using Obuka_Vojnih_Pasa.Services;

namespace Obuka_Vojnih_Pasa.Controllers.APIController
{
    [Route("api/obuke")]
    [ApiController]
    public class ObukaController : ControllerBase
    {
        private readonly IObukaService _service;
        private readonly IMapper _mapper;

        public ObukaController(IObukaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpGet]

        public ActionResult<IEnumerable<Obuka>> Get()
        {
            var obuke = _service.GetAll().ToList();
            var obukeDTO = _mapper.Map<IEnumerable<ObukaDTO>>(obuke);
            return Ok(obukeDTO);
        }

        [HttpPut("{nazivObuke}")]

        public ActionResult<Obuka> Put(string nazivObuke, [FromBody] ObukaDTO model)
        {
            var obuka = _service.GetAll().FirstOrDefault(o => o.Naziv.Equals(nazivObuke));
            if (obuka == null) return NotFound();

            obuka.Naziv = model.Naziv;
            obuka.Trajanje = model.Trajanje;
            obuka.Opis = model.Opis;

            _service.Update(obuka);
            return NoContent();
        }

        [HttpDelete("{id}")]

        public ActionResult<Obuka> Delete(int id)
        {
            var obuka = _service.FindById(id);
            if (obuka == null) return NoContent();
            _service.Delete(obuka.Id);
            return NoContent();
        }
      
    }
}