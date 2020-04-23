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
    [Route("api/pas")]
    [ApiController]
    public class PasController : ControllerBase
    {
       private readonly IPasService _service;
        private readonly IMapper _mapper;
        public PasController(IPasService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Pas>> Get()
        {
            var psi = _service.GetAll().ToList();
            var psiDTO = _mapper.Map<IEnumerable<PasDTO>>(psi);
            return Ok(psiDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<Pas> GetById(int? id)
        {
            if (id == null) return NotFound();
            var pas = _service.FindById(id);
            if (pas == null) return NotFound();
            var pasDTO = _mapper.Map<PasDTO>(pas);
            return Ok(pasDTO);
        }

        [HttpPut("{ime}")]
        public ActionResult<Pas> Put(string ime, [FromBody] PasDTO model)
        {
            var data = _service.GetAll().FirstOrDefault(d => d.Ime.Equals(ime));
            if (data == null) return NotFound();

            data.Ime = model.Ime;
            data.Pol = model.Pol;
            data.Rasa = model.Rasa;
            data.BrojZdravstveneKnjizice = model.BrojZdravstveneKnjizice;
            data.DatumRodjenja = model.DatumRodjenja;

            _service.Update(data);
            return NoContent();
        }

        [HttpPost]
        public ActionResult<Pas> Post([FromBody]PasDTO model)
        {
            var pas = _mapper.Map<Pas>(model);
            _service.Insert(pas);
           
         
            return Created("api/pas", null);

        }

       
        [HttpDelete("{name}")]
        public ActionResult<Pas> Delete(string name)
        {
            var data = _service.GetAll().FirstOrDefault(d => d.Ime.Equals(name));
            if (data == null) return NotFound();

            _service.Delete(data.Id);

            return NoContent();
        }

    }
}