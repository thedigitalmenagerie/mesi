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
    [Route("api/userDeclaration")]
    [ApiController]
    public class UserDeclarationController : ControllerBase
    {
        private UserDeclarationRepository _userDeclarationRepository;

        public UserDeclarationController(UserDeclarationRepository userDeclarationRepo)
        {
            _userDeclarationRepository = userDeclarationRepo;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserDeclarationByid(Guid id)
        {
            var result = _userDeclarationRepository.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound($"No user value with id {id}");
        }

        [HttpGet("{userId}/{cardId}")]
        public IActionResult GetUserDeclarationByCardIdAndUserId(Guid userId, Guid cardId)
        {
            var result = _userDeclarationRepository.GetByUserandCard(userId, cardId);
            return Ok(result);
        }

        [HttpGet("byCard/{cardId}")]
        public IActionResult GetUserDeclarationByCardId(Guid cardId)
        {
            var result = _userDeclarationRepository.GetByCard(cardId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateUserDeclaration(UserDeclaration userDeclaration)
        {
            _userDeclarationRepository.AddUserDeclaration(userDeclaration);
            return Created($"/api/dash/{userDeclaration.Id}", userDeclaration);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateIndividualCard(Guid id, UserDeclaration userDeclaration)
        {
            var userValueToUpdate = _userDeclarationRepository.GetById(id);

            if (userValueToUpdate == null)
            {
                return NotFound($"Could not find a category with the id {id} to update");
            }

            var updatedCategory = _userDeclarationRepository.EditUserDeclaration(id, userDeclaration);

            return Ok(updatedCategory);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserDeclaration(Guid id)
        {
            var result = _userDeclarationRepository.DeleteUserDeclaration(id);
            if (result)
            {
                return Ok($"{id} deleted");
            }
            else return NotFound($"{id} not found");
        }
    }
}
