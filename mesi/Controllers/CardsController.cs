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
    public class CardsWithDetailController : ControllerBase
    {
        private CardsRepository _cardsRepository;

        public CardsWithDetailController(CardsRepository cardsRepo)
        {
            _cardsRepository = cardsRepo;
        }

        [HttpGet("households/{householdId}")]
        public IActionResult GetCardsByHouseholdId(Guid householdId)
        {
            var result = _cardsRepository.GetCardsWithDetails(householdId);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"{householdId} does not have any cards");
        }

        [HttpGet("households/{householdId}/{assignedUserId}")]
        public IActionResult GetCardsByHouseholdId(Guid householdId, Guid assignedUserId)
        {
            var result = _cardsRepository.GetCardsWithDetailsByAssignedUser(householdId, assignedUserId);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"{householdId} does not have any cards");
        }

        [HttpGet("cardswithdetail/{cardId}")]
        public IActionResult GetSingleCardsByCardId(Guid cardId)
        {
            var result = _cardsRepository.GetSingleCardWithDetails(cardId);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"No card with id {cardId}");
        }

        [HttpPost("cards")]
        public IActionResult CreateIndividualCard(Cards cards)
        {
            _cardsRepository.AddIndividualCard(cards);
            return Created($"/api/dash/{cards.Id}", cards);
        }

        [HttpGet("cards/{cardId}")]
        public IActionResult GetSingleCardByCardId(Guid cardId)
        {
            var result = _cardsRepository.GetSingleCard(cardId);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"No card with id {cardId}");
        }

        [HttpGet("cardsByHH/{householdId}")]
        public IActionResult GetCardByHHId(Guid householdId)
        {
            var result = _cardsRepository.GetSingleCardByHHId(householdId);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"No card with id {householdId}");
        }

        [HttpGet("dgCards/{householdId}/{categoryTypeId}")]
        public IActionResult GetDgValueChart(Guid householdId, Guid categoryTypeId)
        {
            var result = _cardsRepository.GetDGCardsforValueChart(householdId, categoryTypeId);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"No card with id {householdId}");
        }

        [HttpGet("nondgCards/{householdId}/{categoryTypeId}")]
        public IActionResult GetNonDGValueChart(Guid householdId, Guid categoryTypeId)
        {
            var result = _cardsRepository.GetNONDGCardsforValueChart(householdId, categoryTypeId);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"No card with id {householdId}");
        }

        [HttpPut("cards/{id}")]
        public IActionResult UpdateIndividualCard(Guid id, Cards cards)
        {
            var cardToUpdate = _cardsRepository.GetSingleCard(id);

            if (cardToUpdate == null)
            {
                return NotFound($"Could not find a category with the id {id} to update");
            }

            var updatedCategory = _cardsRepository.EditIndividualCard(id, cards);

            return Ok(updatedCategory);

        }

        [HttpDelete("cards/{id}")]
        public IActionResult DeleteCard(Guid id)
        {
            var result = _cardsRepository.DeleteCard(id);
            if (result)
            {
                return Ok($"{id} deleted");
            }
            else return NotFound($"{id} not found");
        }

        [HttpGet("cards/declarations/{userId}/{houseHoldId}")]
        public List<CardsWithDetail> GetCardDecl(Guid userId, Guid houseHoldId)
        {
           return _cardsRepository.getAllButDeclarations(userId, houseHoldId);
        }
    }
}
