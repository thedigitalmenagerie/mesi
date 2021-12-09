using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mesi.Models
{
    public class UserValues : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
