using CapstoneProjectManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.CustomHandler
{
    public class SemesterFilter : IActionFilter
    {
        private readonly ISemesterService _semesterService;
        private readonly IProfessionService _professionService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IStudentService _studentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISessionExtensionService _sessionExtensionService;
        public SemesterFilter(ISemesterService semesterService
                , IProfessionService professionService
                , ISpecialtyService specialtyService
                ,IStudentService studentService
                ,IHttpContextAccessor httpContextAccessor
                ,ISessionExtensionService sessionExtensionService)
        {
            _semesterService = semesterService;
            _professionService = professionService;
            _specialtyService = specialtyService;
            _studentService = studentService;
            _httpContextAccessor = httpContextAccessor;
            _sessionExtensionService = sessionExtensionService;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var currentSemester = _semesterService.GetCurrentSemester();
            var user = _sessionExtensionService.GetObjectFromJson<User>(_httpContextAccessor.HttpContext.Session, "sessionAccount");
            if (currentSemester == null)
            {
                context.Result = new RedirectToActionResult("CloseSemester", "Semester", null);
            }
            else
            {
                if (_professionService.getAllProfession(currentSemester.SemesterID) == null)
                {
                    context.Result = new RedirectToActionResult("CloseSemester", "Semester", null);
                }
                else
                {
                    if (currentSemester.SemesterID != _studentService.GetStudentByStudentId(user.UserID).Semester.SemesterID)
                    {
                        _httpContextAccessor.HttpContext.Session.Remove("sessionAccount");
                        context.Result = new RedirectToActionResult("SignIn", "User", null);
                    }
                }
            }
        }
    }
}
