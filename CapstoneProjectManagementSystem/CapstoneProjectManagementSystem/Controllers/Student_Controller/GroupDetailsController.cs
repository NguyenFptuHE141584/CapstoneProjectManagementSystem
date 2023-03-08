
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
    public class GroupDetailsController : Controller
    {
        private readonly IGroupIdeaService _groupIdeaService;
        private readonly IUserService _userService;
        private readonly IStudent_GroupIdeaService _student_GroupIdeaService;
        private readonly IStudent_FavoriteGroupIdeaService _student_FavoriteGroupIdeaService;
        private readonly IProfessionService _professionService;
        private readonly ISpecialtyService _specialtyService;
        private readonly ISessionExtensionService _sessionExtensionService;
        private readonly INotificationService _notificationService;
        private readonly IStudentService _studentService;
        private readonly ISemesterService _semesterService;
        public GroupDetailsController(IGroupIdeaService groupIdeaService,
                                        IUserService userService,
                                        IStudent_GroupIdeaService student_GroupIdeaService,
                                        IStudent_FavoriteGroupIdeaService student_FavoriteGroupIdeaService,
                                        IProfessionService professionService,
                                        ISpecialtyService specialtyService,
                                        ISessionExtensionService sessionExtensionService,
                                        INotificationService notificationService,
                                        IStudentService studentService,
                                        ISemesterService semesterService)
        {
            _groupIdeaService = groupIdeaService;
            _userService = userService;
            _student_GroupIdeaService = student_GroupIdeaService;
            _student_FavoriteGroupIdeaService = student_FavoriteGroupIdeaService;
            _professionService = professionService;
            _specialtyService = specialtyService;
            _sessionExtensionService = sessionExtensionService;
            _notificationService = notificationService;
            _studentService = studentService;
            _semesterService = semesterService;
        }
        public IActionResult Index(string id)
        {
            //send group details info
            GroupIdea groupIdea = _groupIdeaService.GetGroupIdeaById(Convert.ToInt32(id));
            ViewBag.groupIdea = groupIdea;
            string profession = _professionService.getProfessionById(groupIdea.Profession.ProfessionID).ProfessionFullName;
            TempData["profession"] = profession;
            string specialty = _specialtyService.getSpecialtyById(groupIdea.Specialty.SpecialtyID).SpecialtyFullName;
            TempData["specialty"] = specialty;
            List<string> projectTagList = _groupIdeaService.ConvertProjectTags(groupIdea.ProjectTags);
            ViewBag.projectTagList = projectTagList;
            User leader = _userService.GetUserByID(_student_GroupIdeaService.GetLeaderIdByGroupIdeaId(groupIdea.GroupIdeaID));
            ViewBag.leader = leader;
            List<User> memberList = new List<User>();
            List<string> memberIdList = _student_GroupIdeaService.GetMemberIdByGroupIdeaId(groupIdea.GroupIdeaID);
            TempData["availableSlot"] = (groupIdea.MaxMember - groupIdea.NumberOfMember).ToString();
            if (memberIdList != null) { 
            foreach(string mId in memberIdList)
            {
                memberList.Add(_userService.GetUserByID(mId));
            }
            }
            ViewBag.memberList = memberList;
            //check if the request has been sent
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            List<StudentGroupIdea> studentGroupIdeaList = _student_GroupIdeaService.GetListRequestByStudentId(user.UserID);
            if (studentGroupIdeaList != null)
            {
                foreach(StudentGroupIdea sg in studentGroupIdeaList)
                {
                    if (sg.GroupIdeaID == groupIdea.GroupIdeaID && (!sg.Message.Equals("")))
                    {
                        TempData["status"] = "requested";
                    }else if(sg.GroupIdeaID == groupIdea.GroupIdeaID && sg.Message.Equals(""))
                    {
                        TempData["status"] = "invited";
                    }
                }
            }
            //check favorite
            if (_student_FavoriteGroupIdeaService.GetRecord(user.UserID, groupIdea.GroupIdeaID) != null) {
                TempData["favorite"] = "true";
            }
            else
            {
                TempData["favorite"] = "false";
            }
            return View("/Views/Student_View/GroupDetails/GroupDetails.cshtml");
        }
        [HttpPost]
        public IActionResult UnsendRequest(string id)
        {
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            int count = _student_GroupIdeaService.DeleteRecord(user.UserID, Convert.ToInt32(id));
            return RedirectToAction("Index", "GroupDetails",new {@id = id});
        }
        [HttpPost]
        public IActionResult SendRequest(string id, string message)
        {
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            if (message == null ) message = "None";
            int count = _student_GroupIdeaService.AddRecord(user.UserID, Convert.ToInt32(id), 3, message);

            //get leader of group idea 
             var leaderIdOfGroupIdea =  _student_GroupIdeaService.GetLeaderIdByGroupIdeaId(Convert.ToInt32(id));
            //add notification got a request to join your group from
            var notificationContent = $"{_userService.GetNameStudentByUserId(user.UserID)} has requested to join your group.";
            var attachedLink = $"/MyGroup/Index";
            _notificationService.InsertDataNotification(leaderIdOfGroupIdea, notificationContent ,attachedLink);


            return RedirectToAction("Index", "GroupDetails", new { @id = id });
        }
        public JsonResult CheckIfStudentAlreadyHaveGroup()
        {
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            //already in a group idea
            if (_student_GroupIdeaService.FilterStudentHaveIdea(user.UserID, _semesterService.GetCurrentSemester().SemesterID))
            {
                return Json(true);
            }
            //alreadyin a final group
            else if (_studentService.GetStudentByStudentId(user.UserID).FinalGroup.FinalGroupID != 0)
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
