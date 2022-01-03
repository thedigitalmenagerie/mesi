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
    [Route("api/householdMembers")]
    [ApiController]
    public class HouseholdMembersController : ControllerBase
    {
        private HouseholdMembersRepository _householdMembersRepository;

        public HouseholdMembersController(HouseholdMembersRepository householdMembersRepo)
        {
            _householdMembersRepository = householdMembersRepo;
        }

        [HttpGet]
        public IActionResult GetHouseholdMembers()
        {
            var result = _householdMembersRepository.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"Not found");
        }

        [HttpGet("byHousehold/{householdId}")]
        public IActionResult GetCardByHHId(Guid householdId)
        {
            var result = _householdMembersRepository.GetByHouseholdId(householdId);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"No card with id {householdId}");
        }

        [HttpGet("byHouseholdWithUserInfo/{householdId}")]
        public IActionResult GetCardByHHIdWithUserInfo(Guid householdId)
        {
            var result = _householdMembersRepository.GetByHouseholdIdWithUserInfo(householdId);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"No card with id {householdId}");
        }

        [HttpGet("{id}")]
        public IActionResult GetMemberById(Guid id)
        {
            var result = _householdMembersRepository.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"No card with id {id}");
        }


        [HttpGet("byUser/{userId}")]
        public IActionResult GetMemberByUserId(Guid userId)
        {
            var result = _householdMembersRepository.GetByUserId(userId);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"No card with id {userId}");
        }

        [HttpPost]
        public IActionResult CreateHouseholdMember(HouseholdMembers householdMembers)
        {
            _householdMembersRepository.AddNewHouseholdMembers(householdMembers);
            return Created($"/api/householdMembers/{householdMembers.Id}", householdMembers);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHouseholdMember(Guid id, HouseholdMembers householdMembers)
        {
            var cardToUpdate = _householdMembersRepository.GetById(id);

            if (cardToUpdate == null)
            {
                return NotFound($"Could not find a category with the id {id} to update");
            }

            var updatedCategory = _householdMembersRepository.EditHouseholdMember(id, householdMembers);

            return Ok(updatedCategory);

        }
    }
}
