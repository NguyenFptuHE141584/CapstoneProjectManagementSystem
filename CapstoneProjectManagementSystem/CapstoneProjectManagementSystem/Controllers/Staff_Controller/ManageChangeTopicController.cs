using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Services;
using CapstoneProjectManagementSystem.Services.CommonServices;
using CapstoneProjectManagementSystem.Services.CustomHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Controllers.Staff_Controller
{
    [Authorize(Roles = "Staff")]
    public class ManageChangeTopicController : Controller
    {
        private readonly ISemesterService _semesterService;
        private readonly IChangeTopicRequestService _changeTopicRequestService;
        private readonly IFinalGroupService _finalGroupService;
        private readonly IProfessionService _professionService;
        private readonly IStudentService _studentService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;
        public ManageChangeTopicController(ISemesterService semesterService
                                , IChangeTopicRequestService changeTopicRequestService
                                , IFinalGroupService finalGroupService
                                , IProfessionService professionService
                                , IStudentService studentService
                                , INotificationService notificatonService
                                , IUserService userService
                                , IMailService mailService)
        {
            _semesterService = semesterService;
            _changeTopicRequestService = changeTopicRequestService;
            _finalGroupService = finalGroupService;
            _professionService = professionService;
            _studentService = studentService;
            _notificationService = notificatonService;
            _userService = userService;
            _mailService = mailService;

        }
        public IActionResult Index()
        {
            if (_semesterService.GetCurrentSemester() != null)
            {
                int semesterId = _semesterService.GetCurrentSemester().SemesterID;
                List<Profession> professionList = _professionService.getAllProfession(semesterId);
                if (professionList is null)
                {
                    return RedirectToAction("SetupMajor", "SemesterManage");
                }
                else
                {
                    return View("/Views/Staff_View/ManageChangeTopic/Index.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Index", "SemesterManage");
            }
        }

        [HttpPost]
        public JsonResult GetListRequestChangeTopic(int status, string searchText, string pagingType, int recordNumber)
        {
            var semester = _semesterService.GetCurrentSemester();
            int numberOfRecordsPerPage = 5;
            int startNum = (recordNumber != 0) ? Convert.ToInt32(recordNumber) : 1;

            //click next page
            if (pagingType.Equals("previous")) startNum = startNum - numberOfRecordsPerPage;
            //click next page
            else if (pagingType.Equals("next")) startNum = startNum + numberOfRecordsPerPage;

            int countResult = _changeTopicRequestService.CountRecordChangeTopicRequestsBySearchText(searchText, status, semester.SemesterID);
            var changeTopicRequests = _changeTopicRequestService.GetChangeTopicRequestsBySearchText(searchText, status, semester.SemesterID, startNum -1, numberOfRecordsPerPage);
            return Json(
               new
               {
                   numberOfRecordsPerPage = numberOfRecordsPerPage,
                   startNum = startNum,
                   countResult = countResult,
                   changeTopicRequests = changeTopicRequests,
               });
        }

        [HttpPost]
        public JsonResult GetDetailRequestChangeTopic(int requestId)
        {
            var x = _changeTopicRequestService.GetDetailChangeTopicRequestsByRequestId(requestId);
            return Json(x);
        }

        [HttpPost]
        public JsonResult AcceptChangeTopicRequest(int changeTopicRequestId)
        {
            try
            {
                if (changeTopicRequestId != 0)
                {
                    if (_changeTopicRequestService.UpdateStatusOfChangeTopicRequest(changeTopicRequestId, 1, "") != 0)
                    {
                        var newTopic = _changeTopicRequestService.GetNewTopicByChangeTopicRequestId(changeTopicRequestId);
                        if (_finalGroupService.UpdateNewTopicForFinalGroup(newTopic) == 1)
                        {
                            var students = _studentService.GetListStudentIdByFinalGroupId(newTopic.FinalGroup.FinalGroupID);
                            string attachedLinkForStudent = $"/MyGroup/Index";
                            string contentNotificationForStudent = $"Your group's changing topic request has been approved";
                            string listEmailMemberInGroup = "";
                            foreach (var student in students)
                            {
                                _notificationService.InsertDataNotification(student.StudentID, contentNotificationForStudent, attachedLinkForStudent);
                                listEmailMemberInGroup += _userService.GetUserByID(student.StudentID).FptEmail + ",";
                            }
                            //send email notification for student accepted change topic request
                            string subject = $"Accepted change topic request of final group ";
                            string body = "";
                            body += "<h2 style='color:black'>Capstone Project Registration System</h2>";
                            body += "<p style='color:black'>Hello!</p>";
                            body += $"<p style='color:black'>You are receiving this email because Your group's changing topic request has been approved.</p>";
                            body += "<p style='color:black'>Regards,</p>";
                            body += "<p style='color:black'>Capstone Project Registration System</p>";
                            _mailService.SendMailNotification(listEmailMemberInGroup, null, subject, body);
                            return Json(true);
                        }
                        else
                        {
                            _changeTopicRequestService.UpdateStatusOfChangeTopicRequest(changeTopicRequestId,0 , "");
                            return Json(false);
                        }
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

        [HttpPost]
        public JsonResult RejectChangeTopicRequest(int changeTopicRequestId, string commentReject)
        {
            try
            {
                if (changeTopicRequestId != 0 && !string.IsNullOrEmpty(commentReject))
                {
                    if (_changeTopicRequestService.UpdateStatusOfChangeTopicRequest(changeTopicRequestId, 2, commentReject) == 1)
                    {
                        var newTopic = _changeTopicRequestService.GetNewTopicByChangeTopicRequestId(changeTopicRequestId);
                        var students = _studentService.GetListStudentIdByFinalGroupId(newTopic.FinalGroup.FinalGroupID);
                        string attachedLinkForStudent = $"/ChangeTopic/Index";
                        string contentNotificationForStudent = $"Your group's changing topic request has been rejected";
                        string listEmailMemberInGroup = "";
                        foreach (var student in students)
                        {
                            _notificationService.InsertDataNotification(student.StudentID, contentNotificationForStudent, attachedLinkForStudent);
                            listEmailMemberInGroup += _userService.GetUserByID(student.StudentID).FptEmail + ",";
                        }
                        //send email notification for student accepted change topic request
                        string subject = $"Rejected change topic request of final group";
                        string body = "";
                        body += "<h2 style='color:black'>Capstone Project Registration System</h2>";
                        body += "<p style='color:black'>Hello!</p>";
                        body += $"<p style='color:black'>You are receiving this email because Your group's changing topic request has been rejected.</p>";
                        body += "<p style='color:black'>Regards,</p>";
                        body += "<p style='color:black'>Capstone Project Registration System</p>";
                        _mailService.SendMailNotification(listEmailMemberInGroup, null, subject, body);
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
