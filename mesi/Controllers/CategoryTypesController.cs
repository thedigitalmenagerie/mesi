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
    [Route("api/categoryTypes")]
    [ApiController]
    public class CategoryTypesController : ControllerBase

    {
        private CategoryTypesRepository _categoryTypesRepository;

        public CategoryTypesController(CategoryTypesRepository categoryTypesRepo)
        {
            _categoryTypesRepository = categoryTypesRepo;
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryTypeById(Guid id)
        {
            var categoryType = _categoryTypesRepository.GetCategoryTypeById(id);
            if (categoryType == null)
            {
                return NotFound("No need type found.");
            }
            return Ok(categoryType);
        }

        [HttpGet]
        public IActionResult GetAllCategoryTypes()
        {
            var result = _categoryTypesRepository.GetAll();
            if (result.Count() >= 0)
            {
                return Ok(result);
            }
            else return NotFound("No need Types");
        }

    }
}
