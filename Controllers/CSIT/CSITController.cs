using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCSIT.Controllers.CSIT

{
    public class CSITController : Controller
    {
        public IActionResult CSIT()
        {
            return View();
        }
    }
}
