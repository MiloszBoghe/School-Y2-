using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Phone()
        {
            return Content("666-666-666-666-666-666-666-666-666-666-666-666");
        }
        public string Address()
        {
            return "Hell.";
        }
    }
}
