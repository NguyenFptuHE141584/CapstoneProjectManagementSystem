using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Services;
using CapstoneProjectManagementSystem.Services.CommonServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Controllers.Staff_Controller
{
    [Authorize(Roles ="Staff")]
    public class ManageChangeFinalGroupController : Controller
    {
        private readonly ISemesterService _semesterService;
        private readonly IProfessionService _professionService;
        private readonly IChangeFinalGroupRequestService _changeFinalGroupRequestService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;
        public ManageChangeFinalGroupController(ISemesterService semesterService
                                            ,IProfessionService professionService
                                            ,IChangeFinalGroupRequestService changeFinalGroupRequestService
                                            ,INotificationService notificationService
                                            ,IUserService userService
                                            ,IMailService mailService)
        {
            _semesterService = semesterService;
            _professionService = professionService;
            _changeFinalGroupRequestService = changeFinalGroupRequestService;
            _notificationService = notificationService;
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
                    return View("/Views/Staff_View/ManageChangeFinalGroup/Index.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Index", "SemesterManage");
            }
        }

        public JsonResult GetListChangeFinalGroupRequestBySearchText(int status, string searchText, string pagingType, int recordNumber)
        {
            var semester = _semesterService.GetCurrentSemester();
            int numberOfRecordsPerPage = 5;
            int startNum = (recordNumber != 0) ? Convert.ToInt32(recordNumber) : 1;

            //click next page
            if (pagingType.Equals("previous")) startNum = startNum - numberOfRecordsPerPage;
            //click next page
            else if (pagingType.Equals("next")) startNum = startNum + numberOfRecordsPerPage;

            int countResult = _changeFinalGroupRequestService.CountRecordChangeFinalGroupBySearchText
                (searchText, status, semester.SemesterID);
            var listChangeFinalGroupRequest = _changeFinalGroupRequestService.GetListChangeFinalGroupRequestBySearchText
                (searchText, status, semester.SemesterID, startNum - 1, numberOfRecordsPerPage);

            return Json(new 
            {
                numberOfRecordsPerPage = numberOfRecordsPerPage,
                startNum = startNum,
                countResult = countResult,
                listChangeFinalGroupRequest = listChangeFinalGroupRequest,
            });
        }

        public JsonResult AcceptChangeFinalGroupRequest(int changeFinalGroupRequestID)
        {
            try
            {
                var changeFinalGroupRequest = _changeFinalGroupRequestService.GetInforOfStudentExchangeFinalGroup(changeFinalGroupRequestID);
                if (_changeFinalGroupRequestService.UpdateGroupForStudentByChangeFinalGroupRequest(changeFinalGroupRequest) == 3)
                {
                    string notificationContent = "Your request to change groups has been approved";
                    string attachedLink = "/ChangeFinalGroup/Index";
                    _notificationService.InsertDataNotification(changeFinalGroupRequest.FromStudent.StudentID, notificationContent, attachedLink);
                    _notificationService.InsertDataNotification(changeFinalGroupRequest.ToStudent.StudentID, notificationContent, attachedLink);

                    //send email notification for student change final group request
                    string subject = $"Accepted change final group request of Staff";
                    string body = "";
                    body += "<h2 style='color:black'>Capstone Project Registration System</h2>";
                    body += "<p style='color:black'>Hello!</p>";
                    body += $"<p style='color:black'>You are receiving this email because Staff was accepted your exchange group request.</p>";
                    body += "<p style='color:black'>Regards,</p>";
                    body += "<p style='color:black'>Capstone Project Registration System</p>";
                    string receiverEmail = _userService.GetUserByID(changeFinalGroupRequest.FromStudent.StudentID).FptEmail + "," + _userService.GetUserByID(changeFinalGroupRequest.ToStudent.StudentID).FptEmail + ",";
                    _mailService.SendMailNotification(receiverEmail, null, subject, body);
                    return Json(1);
                }
                else
                {
                    return Json(0);
                }
            }
            catch (Exception)
            {

                return Json(0);
            }
        }
        public JsonResult RejectChangeFinalGroupRequest(int changeFinalGroupRequestID,string staffComment)
        {
            try
            {
                if (changeFinalGroupRequestID != 0 && !string.IsNullOrEmpty(staffComment))
                {
                    var changeFinalGroupRequest = _changeFinalGroupRequestService.GetInforOfStudentExchangeFinalGroup(changeFinalGroupRequestID);
                    if (_changeFinalGroupRequestService.UpdateStatusOfStaffByChangeFinalGroupRequestId
                        (changeFinalGroupRequestID, staffComment) == 1)
                    {
                        string notificationContent = "Your request to change groups has been rejected";
                        string attachedLink = "/ChangeFinalGroup/Index";
                        _notificationService.InsertDataNotification(changeFinalGroupRequest.FromStudent.StudentID, notificationContent, attachedLink);
                        _notificationService.InsertDataNotification(changeFinalGroupRequest.ToStudent.StudentID, notificationContent, attachedLink);

                        //send email notification for student change final group request
                        string subject = $"Rejected change final group request of Staff";
                        string body = "";
                        body += "<h2 style='color:black'>Capstone Project Registration System</h2>";
                        body += "<p style='color:black'>Hello!</p>";
                        body += $"<p style='color:black'>You are receiving this email because Staff was rejected your exchange group request.</p>";
                        body += "<p style='color:black'>Regards,</p>";
                        body += "<p style='color:black'>Capstone Project Registration System</p>";
                        string receiverEmail = _userService.GetUserByID(changeFinalGroupRequest.FromStudent.StudentID).FptEmail + "," + _userService.GetUserByID(changeFinalGroupRequest.ToStudent.StudentID).FptEmail + ",";
                        _mailService.SendMailNotification(receiverEmail, null, subject, body);
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
            catch (Exception)
            {

                return Json(0);
            }
        }
    }
}
