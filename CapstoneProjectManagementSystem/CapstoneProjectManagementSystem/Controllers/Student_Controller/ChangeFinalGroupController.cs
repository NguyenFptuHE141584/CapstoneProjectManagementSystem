using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Services;
using CapstoneProjectManagementSystem.Services.CommonServices;
using CapstoneProjectManagementSystem.Services.CustomHandler;
using CapstoneProjectManagementSystem.Services.StaffServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProjectManagementSystem.Controllers.Student_Controller
{

    [Authorize(Roles = "Student")]
    [ServiceFilter(typeof(SemesterFilter))]
    [ServiceFilter(typeof(StudentProfileFilter))]
    public class ChangeFinalGroupController : Controller
    {
        private readonly ISemesterService _semesterService;
        private readonly IStudentService _studentService;
        private readonly ISessionExtensionService _sessionExtensionService;
        private readonly IFinalGroupService _finalGroupService;
        private readonly IChangeFinalGroupRequestService _changeFinalGroupService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IStaffService _staffService;
        private readonly IMailService _mailService;
        public ChangeFinalGroupController(ISemesterService semesterService
                                        , IStudentService studentService
                                        , ISessionExtensionService sessionExtensionService
                                        , IFinalGroupService finalGroupService
                                        , IChangeFinalGroupRequestService changeFinalGroupService
                                        , INotificationService notificationService
                                        , IUserService userService
                                        , IStaffService staffService
                                        , IMailService mailService)
        {
            _semesterService = semesterService;
            _studentService = studentService;
            _sessionExtensionService = sessionExtensionService;
            _finalGroupService = finalGroupService;
            _changeFinalGroupService = changeFinalGroupService;
            _notificationService = notificationService;
            _userService = userService;
            _staffService = staffService;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            var currentSemester = _semesterService.GetCurrentSemester();
            if (currentSemester == null)
            {
                return View("/Views/Common_View/Forbidden.cshtml");
            }
            else
            {
                var studentHaveFinalGroup = _studentService.GetInforStudentHaveFinalGroup(user.UserID, currentSemester.SemesterID);
                if (studentHaveFinalGroup == null)
                {
                    return RedirectToAction("Index", "Error404");
                }
                else
                {
                    ViewBag.studentHaveFinalGroup = studentHaveFinalGroup;
                    return View("/Views/Student_View/ChangeFinalGroup/Index.cshtml");
                }
            }
        }

        [HttpPost]
        public JsonResult SubmitFormGroupExchange(string toGroupName, string toEmail)
        {
            try
            {
                var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
                var currentSemester = _semesterService.GetCurrentSemester();
                var to_studentID = _studentService.GetStudentIDByFptEmailAndGroupName(toEmail, toGroupName, currentSemester.SemesterID);
                if (to_studentID != null && to_studentID != user.UserID)
                {
                    if (_changeFinalGroupService.CreateChangeFinalGroupRequestDao(user.UserID, to_studentID) == 1)
                    {
                        string notifationContent = $"{_userService.GetNameStudentByUserId(user.UserID)} sent you a request to exchange group";
                        string attachedLink = "/ChangeFinalGroup/Index";
                        _notificationService.InsertDataNotification(to_studentID, notifationContent, attachedLink);

                        //send email notification for student change final group request
                        string subject = $"Change final group request of {_userService.GetNameStudentByUserId(to_studentID)}";
                        string body = "";
                        body += "<h2 style='color:black'>Capstone Project Registration System</h2>";
                        body += "<p style='color:black'>Hello!</p>";
                        body += $"<p style='color:black'>You are receiving this email because {_userService.GetNameStudentByUserId(user.UserID)} want changing final group with you.</p>";
                        body += "<p style='color:black'>Regards,</p>";
                        body += "<p style='color:black'>Capstone Project Registration System</p>";
                        var receiver = _userService.GetUserByID(to_studentID).FptEmail + ",";
                        _mailService.SendMailNotification(receiver, null, subject, body);
                        return Json(1);
                    }
                    else
                    {
                        return Json(0);
                    }
                }
                else
                {
                    return Json(0);
                }
            }
            catch (System.Exception)
            {
                return Json(0);
            }
           
        }

        [HttpPost]
        public JsonResult GetListChangeFinalGroupRequestFromOfStudent()
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            var currentSemester = _semesterService.GetCurrentSemester();
            var listChangeFinalGroupRequestFromOfStudent = _changeFinalGroupService.GetListChangeFinalGroupRequestFromOfStudent(user.UserID, currentSemester.SemesterID);
            return Json(listChangeFinalGroupRequestFromOfStudent);

        }

        [HttpPost]
        public JsonResult GetListChangeFinalGroupRequestToOfStudent()
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            var currentSemester = _semesterService.GetCurrentSemester();
            var listChangeFinalGroupRequestToOfStudent = _changeFinalGroupService.GetListChangeFinalGroupRequestToOfStudent(user.UserID, currentSemester.SemesterID);
            return Json(listChangeFinalGroupRequestToOfStudent);
        }

        [HttpPost]
        public JsonResult GetListChangeFinalGroupRequest()
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            var currentSemester = _semesterService.GetCurrentSemester();
            var listChangeFinalGroupRequest = _changeFinalGroupService.GetListChangeFinalGroupRequest(user.UserID, currentSemester.SemesterID);
            return Json(listChangeFinalGroupRequest);
        }

        [HttpPost]
        public JsonResult AcceptChangeFinalGroupRequest(int changeFinalGroupRequestId)
        {
            try
            {
                if (_changeFinalGroupService.UpdateStatusAcceptOfToStudentByChangeFinalGroupRequestId(changeFinalGroupRequestId) == 1)
                {
                    var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
                    var fromStudentId = _changeFinalGroupService.GetFromStudentIdByChangeFinalGroupRequestIdAndToStudentId(changeFinalGroupRequestId, user.UserID);
                    string notifationContent = $"{_userService.GetNameStudentByUserId(user.UserID)} accepted your exchange group request";
                    string attachedLink = "/ChangeFinalGroup/Index";
                    _notificationService.InsertDataNotification(fromStudentId, notifationContent, attachedLink);

                    //send email notification for student change final group request
                    string subject = $"Accepted change final group request of {_userService.GetNameStudentByUserId(user.UserID)}";
                    string body = "";
                    body += "<h2 style='color:black'>Capstone Project Registration System</h2>";
                    body += "<p style='color:black'>Hello!</p>";
                    body += $"<p style='color:black'>You are receiving this email because {_userService.GetNameStudentByUserId(user.UserID)} accepted your exchange group request.</p>";
                    body += "<p style='color:black'>Regards,</p>";
                    body += "<p style='color:black'>Capstone Project Registration System</p>";
                    var receiver = _userService.GetUserByID(fromStudentId).FptEmail + ",";
                    _mailService.SendMailNotification(receiver, null, subject, body);


                    var staffId = _staffService.GetUserIsStaffByRoleId(3).StaffID; // 3 is role id of staff
                    string notifationContentForStaff = $"{_userService.GetNameStudentByUserId(user.UserID)} sent you a request to exchange group";
                    string attachedLinkForStaff = "/ManageChangeFinalGroup/Index";
                    _notificationService.InsertDataNotification(staffId, notifationContentForStaff, attachedLinkForStaff);
                    return Json(1);
                }
                else
                {
                    return Json(0);
                }
            }
            catch (System.Exception)
            {
                return Json(0);
            }
        }

        [HttpPost]
        public JsonResult RejectChangeFinalGroupRequest(int changeFinalGroupRequestId)
        {
            try
            {
                if (_changeFinalGroupService.UpdateStatusRejectOfToStudentByChangeFinalGroupRequestId(changeFinalGroupRequestId) == 1)
                {
                    var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
                    var fromStudentId = _changeFinalGroupService.GetFromStudentIdByChangeFinalGroupRequestIdAndToStudentId(changeFinalGroupRequestId, user.UserID);
                    string notifationContent = $"{_userService.GetNameStudentByUserId(user.UserID)} rejected your exchange group request";
                    string attachedLink = "/ChangeFinalGroup/Index";
                    _notificationService.InsertDataNotification(fromStudentId, notifationContent, attachedLink);

                    //send email notification for student change final group request
                    string subject = $"Rejected change final group request of {_userService.GetNameStudentByUserId(user.UserID)}";
                    string body = "";
                    body += "<h2 style='color:black'>Capstone Project Registration System</h2>";
                    body += "<p style='color:black'>Hello!</p>";
                    body += $"<p style='color:black'>You are receiving this email because {_userService.GetNameStudentByUserId(user.UserID)} rejected your exchange group request.</p>";
                    body += "<p style='color:black'>Regards,</p>";
                    body += "<p style='color:black'>Capstone Project Registration System</p>";
                    var receiver = _userService.GetUserByID(fromStudentId).FptEmail + ",";
                    _mailService.SendMailNotification(receiver, null, subject, body);

                    return Json(1);
                }
                else
                {
                    return Json(0);
                }
            }
            catch (System.Exception)
            {
                return Json(0);
            }
        }
    }
}
