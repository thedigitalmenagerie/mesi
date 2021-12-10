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
    [Route("api/cards/householddetaildash")]
    [ApiController]
    public class CardsWithDetailController : ControllerBase
    {
        private CardsRepository _cardsRepository;

        public CardsWithDetailController(CardsRepository cardsRepo)
        {
            _cardsRepository = cardsRepo;
        }

        [HttpGet("{householdId}")]
        public IActionResult GetCardsByHouseholdId(Guid householdId, Guid userId)
        {
            var result = _cardsRepository.GetCardsWithDetails(householdId, userId);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"{householdId} does not have any cards for {userId}");
        }
    }
}
