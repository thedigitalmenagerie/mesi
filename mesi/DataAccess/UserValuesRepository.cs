using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mesi.DataAccess
{
    public class UserValuesRepository : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
