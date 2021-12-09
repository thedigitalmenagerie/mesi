using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mesi.DataAccess;
using mesi.Models;

namespace mesi.Controllers
{
    [Route("api/needTypes")]
    [ApiController]
    public class NeedTypesController : ControllerBase

    {
        private NeedTypesRepository _needTypesRepository;

        public NeedTypesController(NeedTypesRepository needTypesRepo)
        {
            _needTypesRepository = needTypesRepo;
        }

        [HttpGet("{id}")]
        public IActionResult GetNeedTypeById(Guid id)
        {
            var needType = _needTypesRepository.GetNeedTypeById(id);
            if (needType == null)
            {
                return NotFound("No need type found.");
            }
            return Ok(needType);
        }

        [HttpGet]
        public IActionResult GetAllNeedTypes()
        {
            var result = _needTypesRepository.GetAll();
            if (result.Count() >= 0)
            {
                return Ok(result);
            }
            else return NotFound("No need Types");
        }

    }
}
