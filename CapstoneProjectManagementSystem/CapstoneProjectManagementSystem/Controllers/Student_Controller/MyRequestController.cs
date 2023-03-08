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

namespace CapstoneProjectManagementSystem.Controllers.Student_Controller
{
    [Authorize(Roles = "Student")]
    [ServiceFilter(typeof(SemesterFilter))]
    [ServiceFilter(typeof(StudentProfileFilter))]
    public class MyRequestController : Controller
    {
        private readonly ISessionExtensionService _sessionExtensionService;
        private readonly IStudent_GroupIdeaService _student_GroupIdeaService;
        private readonly IGroupIdeaService _groupIdeaService;
        private readonly ISemesterService _semesterService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;
        public MyRequestController(ISessionExtensionService sessionExtensionService,
                                    IStudent_GroupIdeaService student_GroupIdeaService,
                                    IGroupIdeaService groupIdeaService,
                                    ISemesterService semesterService,
                                    INotificationService notificationService,
                                    IUserService userService,
                                    IStudentService studentService)
        {
            _sessionExtensionService = sessionExtensionService;
            _student_GroupIdeaService = student_GroupIdeaService;
            _groupIdeaService = groupIdeaService;
            _semesterService = semesterService;
            _notificationService = notificationService;
            _userService = userService;
            _studentService = studentService;
        }
        public IActionResult Index()
        {
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            List<StudentGroupIdea> requestList = _student_GroupIdeaService.GetListRequestByStudentId(user.UserID);
            if (requestList != null)
            {
                List<StudentGroupIdea> acceptedList = new List<StudentGroupIdea>();
                List<StudentGroupIdea> pendingList = new List<StudentGroupIdea>();
                List<StudentGroupIdea> rejectedList = new List<StudentGroupIdea>();
                List<StudentGroupIdea> invitedList = new List<StudentGroupIdea>();
                foreach (StudentGroupIdea sg in requestList)
                {
                    if (_groupIdeaService.GetGroupIdeaById(sg.GroupIdeaID).Semester.SemesterID == _semesterService.GetCurrentSemester().SemesterID)
                    {
                        sg.GroupIdea.ProjectEnglishName = _groupIdeaService.GetGroupIdeaById(sg.GroupIdeaID).ProjectEnglishName;
                        sg.GroupIdea.Description = _groupIdeaService.GetGroupIdeaById(sg.GroupIdeaID).Description;
                        if (sg.Status == 3)
                        {
                            pendingList.Add(sg);
                        }
                        else if (sg.Status == 5)
                        {
                            rejectedList.Add(sg);
                        }
                        else if (sg.Status == 4 && !(sg.Message.Equals("")))
                        {
                            acceptedList.Add(sg);
                        }
                        else if (sg.Status == 4 && sg.Message.Equals(""))
                        {
                            invitedList.Add(sg);
                        }
                    }
                }
                ViewBag.acceptedList = acceptedList;
                ViewBag.rejectedList = rejectedList;
                ViewBag.pendingList = pendingList;
                ViewBag.invitedList = invitedList;
            }
            return View("/Views/Student_View/MyRequest/Myrequest.cshtml");
        }
        [HttpPost]
        public IActionResult UnsendRequest(string id)
        {
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            int count = _student_GroupIdeaService.DeleteRecord(user.UserID, Convert.ToInt32(id));
            return RedirectToAction("Index", "MyRequest");
        }
        [HttpPost]
        public IActionResult Join(string id)
        {
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            GroupIdea groupIdea = _groupIdeaService.GetGroupIdeaById(Convert.ToInt32(id));
            if (groupIdea.MaxMember > groupIdea.NumberOfMember) {
                //insert notification for group idea have who joined;
                string contentNotification = $"{_userService.GetNameStudentByUserId(user.UserID)} joined your group.";
                string attachedLink = $"/MyGroup/Index";
                var listStudentInGroup = _student_GroupIdeaService.GetListStudentInGroupByGroupIdeaId(Convert.ToInt32(id));
                foreach (var item in listStudentInGroup)
                {
                    _notificationService.InsertDataNotification(item.StudentID, contentNotification, attachedLink);
                }
                int count = _student_GroupIdeaService.UpdateStatusToMember(user.UserID, Convert.ToInt32(id));
                int count_2 = _student_GroupIdeaService.DeleteAllRequest(user.UserID);
                int count_3 = _groupIdeaService.UpdateNumberOfMemberWhenAdd(Convert.ToInt32(id));
                return RedirectToAction("Index", "MyGroup");
            }
            else {
                return RedirectToAction("Index", "MyRequest");
            }
        }
        [HttpPost]
        public JsonResult CheckIfGroupIsFull([FromBody] string data)
        {
            GroupIdea groupIdea = _groupIdeaService.GetGroupIdeaById(Convert.ToInt32(data));
            if (groupIdea.MaxMember == groupIdea.NumberOfMember)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        [HttpPost]
        public JsonResult CheckIfUserAlreadyHaveGroup()
        {
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            //already in a group idea
            if (_student_GroupIdeaService.FilterStudentHaveIdea(user.UserID, _semesterService.GetCurrentSemester().SemesterID))
            {
                return Json(true);
            }
            //alreadyin a final group
            else if(_studentService.GetStudentByStudentId(user.UserID).FinalGroup.FinalGroupID != 0)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
    }
}
