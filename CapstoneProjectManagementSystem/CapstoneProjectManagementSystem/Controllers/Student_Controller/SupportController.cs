using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Services;
using CapstoneProjectManagementSystem.Services.CommonServices;
using CapstoneProjectManagementSystem.Services.CustomHandler;
using CapstoneProjectManagementSystem.Services.StaffServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Controllers.Student_Controller
{
    [Authorize(Roles = "Student")]
    [ServiceFilter(typeof(SemesterFilter))]
    [ServiceFilter(typeof(StudentProfileFilter))]
    public class SupportController : Controller
    {
        private readonly ISupportService _supportService;
        private readonly IStudentService _studentService;
        private readonly ISessionExtensionService _sessionExtensionService;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly IStaffService _staffService;
        public SupportController(ISupportService supportService,
                                IStudentService studentService,
                                ISessionExtensionService sessionExtensionService,
                                IUserService userService,
                                INotificationService notificationService,
                                IStaffService staffService)
        {
            _supportService = supportService;
            _studentService = studentService;
            _sessionExtensionService = sessionExtensionService;
            _userService = userService;
            _notificationService = notificationService;
            _staffService = staffService;
        }
        public IActionResult Index()
        {
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            ViewBag.student = _studentService.GetStudentByStudentId(user.UserID);
            return View("/Views/Student_View/Support/Index.cshtml");
        }
        public JsonResult AddRequest([FromBody] Support data)
        {
            try
            {
                if (!string.IsNullOrEmpty(data.Title) && !string.IsNullOrEmpty(data.SupportMessge))
                {
                    if (_supportService.AddSupportRequestThenReturnId(data) != 0)
                    {
                        var staff = _staffService.GetUserIsStaffByRoleId(3);
                        var contentNotification = $"{_userService.GetNameStudentByUserId(data.Student.StudentID)} has sent support request.";
                        _notificationService.InsertDataNotification(staff.StaffID, contentNotification, "/ManageSupport/Index");
                        return Json(true);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception)
            {
                return Json(false);
            }
           
        }
    }
}
