using CapstoneProjectManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Controllers.Student_Controller
{
    [Authorize(Roles = "Student")]
    public class SemesterController : Controller
    {
        private readonly ISemesterService _semesterService;
        public SemesterController(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }
        
        public IActionResult CloseSemester()
        {
            return View("/Views/Common_View/SemesterClose.cshtml");
        }

        public JsonResult GetCurrentSemester()
        {
            return Json(_semesterService.GetCurrentSemester());
        }
    }
}
