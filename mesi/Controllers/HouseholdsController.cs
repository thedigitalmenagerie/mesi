using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mesi.Models;
using mesi.DataAccess;

namespace mesi.Controllers
{
    [Route("api/dash")]
    [ApiController]
    public class HouseholdsWithDetailController : ControllerBase
    {
        private HouseholdsRepository _householdsRepository;

        public HouseholdsWithDetailController(HouseholdsRepository householdsRepo)
        {
            _householdsRepository = householdsRepo;
        }

        [HttpGet("byUser/{userId}")]
        public IActionResult GetHouseholds(Guid userId)
        {
            var result = _householdsRepository.GetHouseholdWithDetails(userId);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"{userId} does not belong to any households");
        }

        [HttpGet("{id}")]
        public IActionResult GetHousehold(Guid id)
        {
            var result = _householdsRepository.GetHouseholdWithDetails(id);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"{id} does not belong to any households");
        }

        [HttpPost]
        public IActionResult CreateHousehold(Household household)
        {
            _householdsRepository.AddHousehold(household);
            return Created($"/api/dash/{household.Id}", household);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHousehold(Guid id, Household household)
        {
            var cardToUpdate = _householdsRepository.GetHousehold(id);

            if (cardToUpdate == null)
            {
                return NotFound($"Could not find a category with the id {id} to update");
            }

            var updatedHousehold = _householdsRepository.EditHousehold(id, household);

            return Ok(updatedHousehold);

        }
    }
}
