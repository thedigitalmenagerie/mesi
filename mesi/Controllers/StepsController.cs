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
    [Route("api/steps")]
    [ApiController]
    public class StepsController : ControllerBase

    {
        private StepsRepository _stepsRepository;

        public StepsController(StepsRepository stepsRepo)
        {
            _stepsRepository = stepsRepo;
        }

        [HttpGet("{id}")]
        public IActionResult GetStepById(Guid id)
        {
            var step = _stepsRepository.GetStepById(id);
            if (step == null)
            {
                return NotFound("No step found.");
            }
            return Ok(step);
        }

        [HttpGet]
        public IActionResult GetAllSteps()
        {
            var result = _stepsRepository.GetAll();
            if (result.Count() >= 0)
            {
                return Ok(result);
            }
            else return NotFound("No steps");
        }

    }
}