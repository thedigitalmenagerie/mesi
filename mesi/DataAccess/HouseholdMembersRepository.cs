using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mesi.DataAccess
{
    public class HouseholdMembersRepository : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
