using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mesi.Controllers
{
    public class HouseholdMembersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
