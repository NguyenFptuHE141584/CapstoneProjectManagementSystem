using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Services;
using CapstoneProjectManagementSystem.Services.CommonServices;
using CapstoneProjectManagementSystem.Services.CustomHandler;
using CapstoneProjectManagementSystem.Services.StaffServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Controllers.Student_Controller
{
    [Authorize(Roles = "Student")]
    [ServiceFilter(typeof(SemesterFilter))]
    [ServiceFilter(typeof(StudentProfileFilter))]
    public class ChangeTopicController : Controller
    {
        private readonly ISemesterService _semesterService;
        private readonly IFinalGroupService _finalGroupService;
        private readonly ISessionExtensionService _sessionExtensionService;
        private readonly IMailService _mailService;
        private readonly IChangeTopicRequestService _changeTopicRequestService;
        private readonly IStudentService _studentService;
        private readonly INotificationService _notificationService;
        private readonly IStaffService _staffService;
        private readonly IUserService _userService;

        public ChangeTopicController(ISemesterService semesterService
                                    , IFinalGroupService finalGroupService
                                    , ISessionExtensionService sessionExtensionService
                                    , IMailService mailService
                                    , IChangeTopicRequestService changeTopicRequestService
                                    , IStudentService studentService
                                    , INotificationService notificationService
                                    , IStaffService staffService
                                    , IUserService userService)

        {
            _semesterService = semesterService;
            _finalGroupService = finalGroupService;
            _sessionExtensionService = sessionExtensionService;
            _mailService = mailService;
            _changeTopicRequestService = changeTopicRequestService;
            _studentService = studentService;
            _notificationService = notificationService;
            _staffService = staffService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            var currentSemester = _semesterService.GetCurrentSemester();
            if (currentSemester != null)
            {
                var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
                var finalGroup = _finalGroupService.GetFinalGroupByStudentIsLeader(user.UserID, currentSemester.SemesterID);
                if (finalGroup == null)
                {
                    return RedirectToAction("Index","Error404");
                }
                else
                {

                    ViewBag.finalGroup = finalGroup;
                    return View("/Views/Student_View/ChangeTopic/Index.cshtml", ViewBag.finalGroup);
                }
            }
            else
            {
                return View("/Views/Student_View/ChangeTopic/Index.cshtml", TempData["groupName"]);
            }
        }

        public JsonResult GetListRequestChangeTopic()
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            var semsester = _semesterService.GetCurrentSemester();
            return Json(_changeTopicRequestService.GetChangeTopicRequestsByStudentId(user.UserID, semsester.SemesterID));
        }

        public JsonResult GetDetailRequestChangeTopic(int requestId)
        {
            return Json(_changeTopicRequestService.GetDetailChangeTopicRequestsByRequestId(requestId));
        }

        [HttpPost]
        public JsonResult SubmitChangeTopic([FromBody] ChangeTopicRequest changeTopicRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish) || string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                    || string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation) ||string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                    || string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    return Json(false);
                }
                else
                {
                    var regexEmail = @"[a-z0-9]+@[a-z]+\.[a-z]{2,3}";
                    var matchEmail = Regex.Match(changeTopicRequest.EmailSuperVisor, regexEmail);
                    if (matchEmail.Success)
                    {
                        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                        var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
                        var finalGroupOfStudent = _finalGroupService.GetOldTopicByGroupName(changeTopicRequest.FinalGroup.FinalGroupID);
                        changeTopicRequest.OldTopicNameEnglish = finalGroupOfStudent.ProjectEnglishName;
                        changeTopicRequest.OldTopicNameVietNamese = finalGroupOfStudent.ProjectVietNameseName;
                        changeTopicRequest.OldAbbreviation = finalGroupOfStudent.Abbreviation;
                        var requestChangeTopic = _changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
                        if (requestChangeTopic == 1)
                        {
                            var receiver = config.GetSection("MailSettings")["Mail"]; ; //  mail school
                            var cc = user.FptEmail + "," + changeTopicRequest.EmailSuperVisor; // leader , supervisor
                            var subject = $"Change Topic Of {changeTopicRequest.FinalGroup.GroupName}";
                            var body = $"<h4>Old Topic</h4>"
                                        + $"<p>Project English Name: {changeTopicRequest.OldTopicNameEnglish} </p>"
                                        + $"<p>Project VietNamese Name: {changeTopicRequest.OldTopicNameEnglish}</p>"
                                        + $"<p>Abbreviation: {changeTopicRequest.OldAbbreviation}</p>"
                                        + $"<h4>New Topic</h4>"
                                        + $"<p>Project English Name: {changeTopicRequest.NewTopicNameEnglish} </p>"
                                        + $"<p>Project VietNamese Name: {changeTopicRequest.NewTopicNameVietNamese} </p>"
                                        + $"<p>Abbreviation: {changeTopicRequest.NewAbbreviation}</p>"
                                        + $"<p>Reason Change: {changeTopicRequest.ReasonChangeTopic}</p>"
                                        + $"<p>The system has received the {changeTopicRequest.FinalGroup.GroupName} group's topic change, we need your supervisor to confirm this changed</p>";
                            _mailService.SendMailChangeRequest(receiver, cc, subject, body);

                            // insert notification send request change topic
                            string attachedLinkForStudent = $"/MyGroup/Index";
                            var listMemberInFinalGroup = _studentService.getListMemberByFinalGroupId(changeTopicRequest.FinalGroup.FinalGroupID);
                            if (listMemberInFinalGroup != null)
                            {
                                foreach (var member in listMemberInFinalGroup)
                                {
                                    string contentNotificationForStudent = $"your group has submitted changing topic request.";
                                    _notificationService.InsertDataNotification(member.StudentID, contentNotificationForStudent, attachedLinkForStudent);
                                }
                            }
                            var staffId = _staffService.GetUserIsStaffByRoleId(3).StaffID;
                            var leaderId = _studentService.getLeaderByFinalGroupId(changeTopicRequest.FinalGroup.FinalGroupID).StudentID;
                            string attachedLinkForStaff = $"/ManageChangeTopic/Index";
                            string contentNotificationForStaff = $"Leader {_userService.GetNameStudentByUserId(leaderId)} of final group {changeTopicRequest.FinalGroup.GroupName} has sent changing topic request.";
                            _notificationService.InsertDataNotification(staffId, contentNotificationForStaff, attachedLinkForStaff);
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
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
}
