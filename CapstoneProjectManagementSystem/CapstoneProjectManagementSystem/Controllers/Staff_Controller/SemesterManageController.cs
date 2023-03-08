using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Controllers.Staff_Controller
{
    [Authorize (Roles = "Staff")]
    public class SemesterManageController : Controller
    {
        private readonly ISemesterService _semesterService;
        private readonly IProfessionService _professionService;
        private readonly ISpecialtyService _specialtyService;
        public SemesterManageController(ISemesterService semesterService, 
                                        IProfessionService professionService,
                                        ISpecialtyService specialtyService)
        {
            _professionService = professionService;
            _specialtyService = specialtyService;
            _semesterService = semesterService;
        }
        public IActionResult Index()
        {
            var currentSemester = _semesterService.GetCurrentSemester();
            return View("/Views/Staff_View/SemesterManage/Index.cshtml",currentSemester);
        }
        public IActionResult SetupMajor()
        {
            Semester semester = _semesterService.GetLastSemester();
            if (semester!=null)
            {
                List<Profession> professionList = _professionService.getAllProfession(semester.SemesterID);
                if (professionList != null)
                {
                    foreach (Profession pro in professionList)
                    {
                        List<Specialty> specialtyList = _specialtyService.getSpecialtiesByProfessionId(pro.ProfessionID,semester.SemesterID);
                        pro.Specialties = specialtyList;
                    }
                    ViewBag.professionList = professionList;
                }
            }
            return View("/Views/Staff_View/SemesterManage/SetupMajor.cshtml");
        }

        [HttpPost]
        public JsonResult UpdateCurrentSemester([FromBody]Semester semester)
        {
            return Json(_semesterService.UpdateCurrentSemester(semester)) ;
        }

        [HttpPost]
        public JsonResult GetCurrentSemester()
        {
            return Json(_semesterService.GetCurrentSemester());
        }

        [HttpPost]
        public JsonResult AddNewSemester([FromBody] Semester semester)
        {
            return Json(_semesterService.AddNewSemester(semester));
        }
        
        [HttpPost]
        public JsonResult CloseSemesterCurrent()
        {
            var currentSemester = _semesterService.GetCurrentSemester();
            return Json(_semesterService.CloseCurrentSemester(currentSemester.SemesterID));
        }
    }
}
