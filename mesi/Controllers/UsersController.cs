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
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase

    {
        private UsersRepository _usersRepository;

        public UsersController(UsersRepository usersRepo)
        {
            _usersRepository = usersRepo;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = _usersRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound("No user found.");
            }
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var result = _usersRepository.GetAll();
            if (result.Count() >= 0)
            {
                return Ok(result);
            }
            else return NotFound("No users");
        }

        [HttpPost]
        public IActionResult AddUser(Users newUser)
        {
            _usersRepository.AddNewUser(newUser);
            return Created($"/api/users/{newUser.Id}", newUser);
        }

    }
}
