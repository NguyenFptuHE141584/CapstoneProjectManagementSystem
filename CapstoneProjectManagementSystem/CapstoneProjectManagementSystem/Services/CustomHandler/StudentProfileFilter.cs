using CapstoneProjectManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CapstoneProjectManagementSystem.Services.CustomHandler
{
    public class StudentProfileFilter : IActionFilter
    {
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;
        private readonly ISessionExtensionService _sessionExtensionService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISpecialtyService _specialtyService;
        private readonly IProfessionService _professionService;
        private readonly ISemesterService _semesterService;
        

        public StudentProfileFilter(IStudentService studentService
                                    , IUserService userService
                                    , ISessionExtensionService sessionExtensionService
                                    , IHttpContextAccessor httpContextAccessor
                                    , ISpecialtyService specialtyService
                                    , IProfessionService professionService
                                    , ISemesterService semesterService)
        {
            _studentService = studentService;
            _userService = userService;
            _sessionExtensionService = sessionExtensionService;
            _httpContextAccessor = httpContextAccessor;
            _specialtyService = specialtyService;
            _professionService = professionService; 
            _semesterService = semesterService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(_httpContextAccessor.HttpContext.Session, "sessionAccount");
            var currentSemester = _semesterService.GetCurrentSemester();
            var studentProfile = _studentService.GetProfileOfStudentByUserId(user.UserID);
            if (studentProfile.PhoneNumber == null || studentProfile.PhoneNumber == ""
                || studentProfile.Specialty.SpecialtyID == 0 || studentProfile.Profession.ProfessionID == 0
                || _specialtyService.getSpecialtyById(studentProfile.Specialty.SpecialtyID).Semester.SemesterID != currentSemester.SemesterID
                || _professionService.getProfessionById(studentProfile.Profession.ProfessionID).Semester.SemesterID != currentSemester.SemesterID)
            {
                context.Result = new RedirectToActionResult("Index", "StudentProfile", null);
            }
        }
    }
}
