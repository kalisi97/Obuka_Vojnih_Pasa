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
    [Route("api/angazovanje")]
    [ApiController]
    public class AngazovanjeController : ControllerBase
    {
        private readonly IAngazovanjeService _service;
        private readonly IMapper _mapper;

        public AngazovanjeController(IAngazovanjeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpGet]

        public ActionResult<IEnumerable<Angazovanje>> Get()
        {
            var angazovanja = _service.GetAll().ToList();
            var angazovanjaDTO = _mapper.Map<IEnumerable<AngazovanjaDTO>>(angazovanja);
            return Ok(angazovanjaDTO);
        }

    }
}