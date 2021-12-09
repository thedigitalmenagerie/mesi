using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mesi.Models
{
    public class Cards : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
