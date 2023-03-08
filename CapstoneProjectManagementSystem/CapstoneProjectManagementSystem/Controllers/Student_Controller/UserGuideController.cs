using CapstoneProjectManagementSystem.Services.CustomHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Controllers.Student_Controller
{
  
    public class UserGuideController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Student_View/UserGuide/Index.cshtml");
        }
    }
}
