using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Controllers.Common_Controller
{
    [Authorize(Roles = "Student")]
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Common_View/AboutUs.cshtml");
        }
    }
}
