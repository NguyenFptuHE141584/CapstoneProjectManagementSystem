using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Common;
using CapstoneProjectManagementSystem.Services;
using CapstoneProjectManagementSystem.Services.CommonServices;
using CapstoneProjectManagementSystem.Services.CustomHandler;
using CapstoneProjectManagementSystem.Services.StaffServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Controllers.Student_Controller
{
    [Authorize(Roles = "Student")]
    [ServiceFilter(typeof(SemesterFilter))]
    [ServiceFilter(typeof(StudentProfileFilter))]
    public class MyGroupController : Controller
    {
        private readonly IGroupIdeaService _groupIdeaService;
        private readonly IUserService _userService;
        private readonly IStudent_GroupIdeaService _student_GroupIdeaService;
        private readonly IProfessionService _professionService;
        private readonly ISpecialtyService _specialtyService;
        private readonly ISessionExtensionService _sessionExtensionService;
        private readonly IRegisteredGroupService _registeredGroupService;
        private readonly IStudentService _studentService;
        private readonly IFinalGroupService _finalGroupService;
        private readonly ISemesterService _semesterService;
        private readonly INotificationService _notificationService;
        private readonly IStaffService _staffService;
        private readonly IMailService _mailService;
        public MyGroupController(IGroupIdeaService groupIdeaService,
                                        IUserService userService,
                                        IStudent_GroupIdeaService student_GroupIdeaService,
                                        IProfessionService professionService,
                                        ISpecialtyService specialtyService,
                                        ISessionExtensionService sessionExtensionService,
                                        IRegisteredGroupService registeredGroupService,
                                        IStudentService studentService,
                                        IFinalGroupService finalGroupService,
                                        ISemesterService semesterService,
                                        INotificationService notificationService,
                                        IStaffService staffService,
                                        IMailService mailService)
        {
            _groupIdeaService = groupIdeaService;
            _userService = userService;
            _student_GroupIdeaService = student_GroupIdeaService;
            _professionService = professionService;
            _specialtyService = specialtyService;
            _sessionExtensionService = sessionExtensionService;
            _registeredGroupService = registeredGroupService;
            _studentService = studentService;
            _finalGroupService = finalGroupService;
            _semesterService = semesterService;
            _notificationService = notificationService;
            _staffService = staffService;
            _mailService = mailService;
        }
        public IActionResult Index()
        {
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            Student student = _studentService.GetStudentByStudentId(user.UserID);
            // have final group
            if (student.FinalGroup.FinalGroupID != 0)
            {
                if (_finalGroupService.getFinalGroupById(student.FinalGroup.FinalGroupID).Semester.SemesterID == _semesterService.GetCurrentSemester().SemesterID)
                {
                    FinalGroup finalGroup = _finalGroupService.getFinalGroupById(student.FinalGroup.FinalGroupID);
                    ViewBag.finalGroup = finalGroup;
                    string profession = _professionService.getProfessionById(finalGroup.Profession.ProfessionID).ProfessionFullName;
                    TempData["profession"] = profession;
                    string specialty = _specialtyService.getSpecialtyById(finalGroup.Specialty.SpecialtyID).SpecialtyFullName;
                    TempData["specialty"] = specialty;
                    User leader = _userService.GetUserByID(_studentService.getLeaderByFinalGroupId(finalGroup.FinalGroupID).StudentID);
                    ViewBag.leader = leader;
                    List<User> userMemberList = new List<User>();
                    List<Student> memberList = _studentService.getListMemberByFinalGroupId(finalGroup.FinalGroupID);
                    if (memberList != null)
                    {
                        foreach (Student s in memberList)
                        {
                            userMemberList.Add(_userService.GetUserByID(s.StudentID));
                        }
                    }
                    ViewBag.memberList = userMemberList;
                    TempData["showGroupName"] = _semesterService.GetCurrentSemester().ShowGroupName ? "true" : "false";
                    TempData["groupNamePersonal"] = student.GroupName;
                    //is Leader
                    if (user.UserID.Equals(leader.UserID))
                    {
                        return View("/Views/Student_View/MyGroup/FinalGroupForLeader.cshtml");
                    }
                    //is Member
                    else
                    {
                        return View("/Views/Student_View/MyGroup/FinalGroupForMember.cshtml");
                    }
                }
                else
                {
                    return View("/Views/Common_View/AccessDeniedForMyGroup.cshtml");
                }
            }
            //not have final group yet
            else
            {
                string groupId = _student_GroupIdeaService.GetGroupIdByStudentId(user.UserID);
                if (groupId != null)
                {
                    GroupIdea groupIdea = _groupIdeaService.GetGroupIdeaById(Convert.ToInt32(groupId));
                    if (groupIdea.Semester.SemesterID == _semesterService.GetCurrentSemester().SemesterID)
                    {
                        //send group details
                        ViewBag.groupIdea = groupIdea;
                        string profession = _professionService.getProfessionById(groupIdea.Profession.ProfessionID).ProfessionFullName;
                        TempData["profession"] = profession;
                        string specialty = _specialtyService.getSpecialtyById(groupIdea.Specialty.SpecialtyID).SpecialtyFullName;
                        TempData["specialty"] = specialty;
                        List<string> projectTagList = _groupIdeaService.ConvertProjectTags(groupIdea.ProjectTags);
                        ViewBag.projectTagList = projectTagList;
                        TempData["availableSlot"] = (groupIdea.MaxMember - groupIdea.NumberOfMember).ToString();
                        User leader = _userService.GetUserByID(_student_GroupIdeaService.GetLeaderIdByGroupIdeaId(groupIdea.GroupIdeaID));
                        ViewBag.leader = leader;
                        List<User> memberList = new List<User>();
                        List<string> memberIdList = _student_GroupIdeaService.GetMemberIdByGroupIdeaId(groupIdea.GroupIdeaID);
                        if (memberIdList != null)
                        {
                            foreach (string mId in memberIdList)
                            {
                                memberList.Add(_userService.GetUserByID(mId));
                            }
                        }
                        ViewBag.memberList = memberList;
                        //send registration Status
                        RegisteredGroup registeredGroup = _registeredGroupService.GetRegisteredGroupByGroupIdeaId(groupIdea.GroupIdeaID);
                        if (registeredGroup != null)
                        {
                            TempData["registerStatus"] = registeredGroup.Status.ToString();
                            if (registeredGroup.Status == 2)
                            {
                                TempData["staffComment"] = registeredGroup.StaffComment;
                            }
                        }
                        //is Leader
                        if (user.UserID.Equals(leader.UserID))
                        {
                            int semesterId = _semesterService.GetCurrentSemester().SemesterID;
                            ViewBag.ProfessionList = _professionService.getAllProfession(semesterId);
                            ViewBag.SpecialtyList = _specialtyService.getSpecialtiesByProfessionId(_professionService.getAllProfession(semesterId).ElementAt<Profession>(0).ProfessionID, semesterId);
                            // send list request
                            List<JoinRequest> joinRequestList = _student_GroupIdeaService.GetAllJoinRequestByGroupIdeaId(groupIdea.GroupIdeaID);
                            ViewBag.joinRequestList = joinRequestList;
                            return View("/Views/Student_View/MyGroup/MyGroupForLeader.cshtml");
                        }
                        //is Member
                        else
                        {
                            return View("/Views/Student_View/MyGroup/MyGroupForMember.cshtml");
                        }
                    }
                    else
                    {
                        return View("/Views/Common_View/AccessDeniedForMyGroup.cshtml");
                    }
                }
                else
                {
                    return View("/Views/Common_View/AccessDeniedForMyGroup.cshtml");
                }
            }
        }
        [HttpPost]
        public IActionResult AcceptRequest(string userId, string projectId)
        {
            _student_GroupIdeaService.UpdateStatusToAccept(userId, Convert.ToInt32(projectId));

            //send notification for student request to group
            var leader = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            string notficationContent = $"{_userService.GetNameStudentByUserId(leader.UserID)} approved your request.";
            string attachedLink = $"/MyRequest/Index";
            _notificationService.InsertDataNotification(userId, notficationContent, attachedLink);

            return RedirectToAction("Index", "Mygroup");
        }
        [HttpPost]
        public IActionResult RejectRequest(string userId, string projectId)
        {
            _student_GroupIdeaService.UpdateStatusToReject(userId, Convert.ToInt32(projectId));

            //send notification for student request to group
            var leader = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            string notficationContent = $"{_userService.GetNameStudentByUserId(leader.UserID)} rejected your request.";
            string attachedLink = $"/MyRequest/Index";
            _notificationService.InsertDataNotification(userId, notficationContent, attachedLink);

            return RedirectToAction("Index", "MyGroup");
        }
        [HttpPost]
        public IActionResult ChangeToLeader(string leaderId, string memberId, string projectId)
        {
            _student_GroupIdeaService.UpdateStatusToLeader(memberId, Convert.ToInt32(projectId));
            _student_GroupIdeaService.UpdateStatusToMember(leaderId, Convert.ToInt32(projectId));

            //insert notification change to leader
            string attachedLink = $"/MyGroup/Index";
            var listStudentInGroup = _student_GroupIdeaService.GetListStudentInGroupByGroupIdeaId(Convert.ToInt32(projectId));
            foreach (var item in listStudentInGroup)
            {
                string contentNotification = "";
                if (item.StudentID == memberId)
                {
                    contentNotification += $"{_userService.GetNameStudentByUserId(leaderId)} promotes you to be the leader.";
                    _notificationService.InsertDataNotification(memberId, contentNotification, attachedLink);
                }
                else
                {
                    contentNotification += $"{_userService.GetNameStudentByUserId(leaderId)} changed the leadership to {_userService.GetNameStudentByUserId(memberId)}.";
                    _notificationService.InsertDataNotification(item.StudentID, contentNotification, attachedLink);
                }
            }
            return RedirectToAction("Index", "MyGroup");
        }
        public IActionResult RemoveMember(string userId, string projectId)
        {
            // insert notification remove member
            string attachedLink = $"/MyGroup/Index";
            var listStudentInGroup = _student_GroupIdeaService.GetListStudentInGroupByGroupIdeaId(Convert.ToInt32(projectId));
            foreach (var item in listStudentInGroup)
            {
                string contentNotification = "";
                if (item.StudentID == userId)
                {
                    contentNotification += $"You have been removed from group {listStudentInGroup.FirstOrDefault(x => x.Status == 1).GroupIdea.ProjectEnglishName}.";
                    _notificationService.InsertDataNotification(userId, contentNotification, attachedLink);
                }
                else
                {
                    contentNotification += $"{_userService.GetNameStudentByUserId(userId)} has been removed from your group.";
                    _notificationService.InsertDataNotification(item.StudentID, contentNotification, attachedLink);
                }
            }

            _student_GroupIdeaService.DeleteRecord(userId, Convert.ToInt32(projectId));
            _groupIdeaService.UpdateNumberOfMemberWhenRemove(Convert.ToInt32(projectId));

            return RedirectToAction("Index", "MyGroup");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGroup(string projectId)
        {
            // insert notification delete group
            string attachedLink = $"/CreateIdea/Index";
            var listStudentInGroup = _student_GroupIdeaService.GetListStudentInGroupByGroupIdeaId(Convert.ToInt32(projectId));
            string groupIdeaName = listStudentInGroup.FirstOrDefault(x => x.Status == 1).GroupIdea.ProjectEnglishName;
            string listEmailMemberInGroup = "";
            foreach (var item in listStudentInGroup)
            {
                string contentNotification = "";
                if (item.Status != 1)
                {
                    contentNotification += $"The group <b>{groupIdeaName}</b> you applied to has been removed. Please find another or create your own group";
                    _notificationService.InsertDataNotification(item.StudentID, contentNotification, attachedLink);
                    listEmailMemberInGroup += _userService.GetUserByID(item.StudentID).FptEmail + ",";
                }
            }

            // send email notification delete group 
            string subject = $"Delete group idea {groupIdeaName}";
            string link = $"{HttpContext.Request.Host.Value}/StudentHome/Index";
            string body = "";
            body += "<h2 style='color:black'>Capstone Project Registration System</h2>";
            body += "<p style='color:black'>Hello!</p>";
            body += $"<p style='color:black'>You are receiving this email because The group <b>{groupIdeaName}</b> you applied to has been removed.Please find another or create your own group.</p>";
            body += "<p style='color:black'>Regards,</p>";
            body += "<p style='color:black'>Capstone Project Registration System</p>";

            await _mailService.SendMailNotification(listEmailMemberInGroup, null, subject, body);


            _student_GroupIdeaService.DeleteAllRecordOfGroupIdea(Convert.ToInt32(projectId));
            _groupIdeaService.DeleteGroupIdea(Convert.ToInt32(projectId));
            return RedirectToAction("Index", "MyGroup");
        }

        [HttpPost]
        public IActionResult LeaveGroup(string projectId)
        {
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            string attachedLink = $"/MyGroup/Index";
            var listStudentInGroup = _student_GroupIdeaService.GetListStudentInGroupByGroupIdeaId(Convert.ToInt32(projectId));
            foreach (var item in listStudentInGroup)
            {
                string contentNotification = "";
                if (item.StudentID != user.UserID)
                {
                    contentNotification += $"{_userService.GetNameStudentByUserId(user.UserID)} has left your group.";
                    _notificationService.InsertDataNotification(item.StudentID, contentNotification, attachedLink);
                }
            }
            _student_GroupIdeaService.DeleteRecord(user.UserID, Convert.ToInt32(projectId));
            _groupIdeaService.UpdateNumberOfMemberWhenRemove(Convert.ToInt32(projectId));
            return RedirectToAction("Index", "MyGroup");
        }
        [HttpPost]
        public IActionResult CancelRegistrationRequest(string projectId)
        {
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            string attachedLink = $"/MyGroup/Index";
            var listStudentInGroup = _student_GroupIdeaService.GetListStudentInGroupByGroupIdeaId(Convert.ToInt32(projectId));
            foreach (var item in listStudentInGroup)
            {
                string contentNotification = "";
                if (item.StudentID != user.UserID)
                {
                    contentNotification += $"{_userService.GetNameStudentByUserId(user.UserID)} has cancel group registration request of your group.";
                    _notificationService.InsertDataNotification(item.StudentID, contentNotification, attachedLink);
                }
            }
            _registeredGroupService.DeleteRecord(Convert.ToInt32(projectId));
            return RedirectToAction("Index", "MyGroup");
        }
        [HttpPost]
        public async Task<IActionResult> RegisterGroup(string groupId, string name1, string phone1, string email1, string name2, string phone2, string email2, string otherComment)
        {
            var result = _registeredGroupService.AddRegisteredGroup(new RegisteredGroup
            {
                RegisteredGroupID = Convert.ToInt32(groupId),
                RegisteredSupervisorName1 = name1,
                RegisteredSupervisorName2 = name2,
                RegisteredSupervisorPhone1 = phone1,
                RegisteredSupervisorPhone2 = phone2,
                RegisteredSupervisorEmail1 = email1,
                RegisteredSupervisorEmail2 = email2,
                StudentComment = otherComment,
                Students_Registration = _userService.GetListFptEmailByGroupIdeaId(Convert.ToInt32(groupId))
            });

            if  (result == 1)
            {
                _student_GroupIdeaService.DeleteRecordHaveStatusEqual3or4or5OfGroupIdea(Convert.ToInt32(groupId));
                //insert notification Submit registration 
                User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
                string attachedLink = $"/MyGroup/Index";
                string contentNotification = "";
                var listStudentInGroup = _student_GroupIdeaService.GetListStudentInGroupByGroupIdeaId(Convert.ToInt32(groupId));

                string listEmailMemberInGroup = "";
                foreach (var item in listStudentInGroup)
                {
                    contentNotification = "";
                    if (item.StudentID != user.UserID)
                    {
                        contentNotification += "Your team leader has sent a group registration request.";
                        _notificationService.InsertDataNotification(item.StudentID, contentNotification, attachedLink);
                        listEmailMemberInGroup += _userService.GetUserByID(item.StudentID).FptEmail + ",";
                    }
                }

                // insert notification staff have student registration group capstone
                var staff = _staffService.GetUserIsStaffByRoleId(3);
                attachedLink = "/RegistrationGroup/Index";
                var leader = listStudentInGroup.FirstOrDefault(x => x.Status == 1);
                contentNotification = $"Leader {_userService.GetNameStudentByUserId(leader.StudentID)} " +
                $"of group {leader.GroupIdea.ProjectEnglishName} has already sent group's registration request.";
                _notificationService.InsertDataNotification(staff.StaffID, contentNotification, attachedLink);


                //send email notification for student submit registration
                if (!string.IsNullOrEmpty(listEmailMemberInGroup))
                {
                    string subject = $"Submit registration request from group {leader.GroupIdea.ProjectEnglishName}";
                    string link = $"{HttpContext.Request.Host.Value}/StudentHome/Index";
                    string body = "";
                    body += "<h2 style='color:black'>Capstone Project Registration System</h2>";
                    body += "<p style='color:black'>Hello!</p>";
                    body += "<p style='color:black'>You are receiving this email because Your team leader has sent a group registration request.</p>";
                    body += "<p style='color:black'>Please access the system to get more details.</p>";
                    body += "<p style='color:black'>Regards,</p>";
                    body += "<p style='color:black'>Capstone Project Registration System</p>";
                    await _mailService.SendMailNotification(listEmailMemberInGroup, null, subject, body);
                }
            }
            return RedirectToAction("Index", "MyGroup");
        }

        [HttpPost]
        public JsonResult CheckStudentExistWhenBeforeAdded([FromBody] string fptEmail)
        {
            var currentSemester = _semesterService.GetCurrentSemester();
            if (!_student_GroupIdeaService.CheckAddedStudentIsValid(fptEmail))
            {
                var student = _studentService.GetStudentByFptEmail(fptEmail, currentSemester.SemesterID);
                return Json(student);
            }
            else
            {
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult UpdateIdea([FromBody] GroupIdea groupIdea)
        {
            try
            {
                if (groupIdea.ProjectEnglishName.Trim().Replace(" ", "").Length <= 0 || groupIdea.ProjectEnglishName.Trim().Replace(" ", "").Length > 100
                 || groupIdea.Abrrevation.Trim().Replace(" ", "").Length <= 0 || groupIdea.Abrrevation.Trim().Replace(" ", "").Length > 20
                 || groupIdea.ProjectVietNameseName.Trim().Replace(" ", "").Length <= 0 || groupIdea.ProjectVietNameseName.Trim().Replace(" ", "").Length > 100
                 || groupIdea.Description.Trim().Replace(" ", "").Length <= 0 || groupIdea.Description.Trim().Replace(" ", "").Length > 2000)
                {
                    return Json(false);
                }
                else
                {
                    _groupIdeaService.UpdateIdea(groupIdea, _semesterService.GetCurrentSemester().SemesterID);

                    //insert notification leader update idea
                    User leader = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
                    string contentNotification = $"Leader {_userService.GetNameStudentByUserId(leader.UserID)} has updated your group information.";
                    string attachedLink = $"/MyGroup/Index";
                    var listStudentInGroup = _student_GroupIdeaService.GetListStudentInGroupByGroupIdeaId(Convert.ToInt32(groupIdea.GroupIdeaID));
                    foreach (var item in listStudentInGroup)
                    {
                        if (item.Status != 1)
                        {
                            _notificationService.InsertDataNotification(item.StudentID, contentNotification, attachedLink);
                        }
                    }


                    foreach (var item in groupIdea.Students)
                    {
                        _student_GroupIdeaService.AddRecord(item.StudentID, groupIdea.GroupIdeaID, 4, "");
                        contentNotification = "";
                        attachedLink = "";
                        attachedLink += $"/MyRequest/Index";
                        contentNotification += $"{_userService.GetNameStudentByUserId(leader.UserID)} invited you to join their group <b>{groupIdea.ProjectEnglishName}.</b> Click here to see.";
                        _notificationService.InsertDataNotification(item.StudentID, contentNotification, attachedLink);
                    }
                    return Json(true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(false);
            }
        }
        [HttpPost]
        public JsonResult GetCorrespondingSpecialty([FromBody] string data)
        {
            List<Specialty> specialtyList = _specialtyService.getSpecialtiesByProfessionId(Convert.ToInt32(data), _semesterService.GetCurrentSemester().SemesterID);
            return Json(specialtyList);
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
    }
}
