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
    [Route("api/households/dash")]
    [ApiController]
    public class HouseholdsWithDetailController : ControllerBase
    {
        private HouseholdsRepository _householdsRepository;

        public HouseholdsWithDetailController(HouseholdsRepository householdsRepo)
        {
            _householdsRepository = householdsRepo;
        }

        [HttpGet("{userId}")]
        public IActionResult GetHouseholds(Guid userId)
        {
            var result = _householdsRepository.GetHouseholdWithDetails(userId);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"{userId} does not belong to any households");
        }
    }
}
