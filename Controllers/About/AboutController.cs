using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCSIT.Controllers.About
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
