using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
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
    public class RegistrationGroupController : Controller
    {
        private readonly IRegisteredGroupService _registeredGroupService;
        private readonly ISemesterService _semesterService;
        private readonly IGroupIdeaService _groupIdeaService;
        private readonly IFinalGroupService _finalGroupService;
        private readonly IStudent_GroupIdeaService _student_GroupIdeaService;
        private readonly IStudentService _studentService;
        private readonly IProfessionService _professionService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IChangeTopicRequestService _changeTopicRequestService;
        private readonly INotificationService _notificationService;
        private readonly IStudent_FavoriteGroupIdeaService _student_FavoriteGroupIdeaService;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;

        public RegistrationGroupController(IRegisteredGroupService registeredGroupService
                                            , ISemesterService semesterService
                                            , IGroupIdeaService groupIdeaService
                                            , IFinalGroupService finalGroupService
                                            , IStudent_GroupIdeaService student_GroupIdeaService
                                            , IStudentService studentService
                                            , IProfessionService professionService
                                            , ISpecialtyService specialtyService
                                            , IChangeTopicRequestService changeTopicRequestService
                                            , INotificationService notificationService
                                            , IStudent_FavoriteGroupIdeaService student_FavoriteGroupIdeaService
                                            , IMailService mailService
                                            , IUserService userService)
        {
            _registeredGroupService = registeredGroupService;
            _semesterService = semesterService;
            _groupIdeaService = groupIdeaService;
            _finalGroupService = finalGroupService;
            _student_GroupIdeaService = student_GroupIdeaService;
            _studentService = studentService;
            _professionService = professionService;
            _specialtyService = specialtyService;
            _changeTopicRequestService = changeTopicRequestService;
            _notificationService = notificationService;
            _student_FavoriteGroupIdeaService = student_FavoriteGroupIdeaService;
            _mailService = mailService;
            _userService = userService;
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
                    return View("/Views/Staff_View/RegistrationGroup/Index.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Index", "SemesterManage");
            }
        }

        [HttpPost]
        public JsonResult AcceptRegisteredGroup(int registeredGroupID)
        {
            try
            {
                if (_registeredGroupService.UpdateStatusByRegisteredGroupID(registeredGroupID) == 1)// update status accepted for registration group
                {
                    //get groupIdeaId by registeredGroup
                    var groupId = _registeredGroupService.GetGroupIDByRegisteredGroupId(registeredGroupID).GroupIdea.GroupIdeaID;
                    //get groupIdea by groupIdeaId
                    var groupIdea = _groupIdeaService.GetGroupIdeaById(groupId);

                    //get codeOfGroupName of specialty by groupIdeaId 
                    var codeOfGroupName = _specialtyService.GetCodeOfGroupNameByGroupIdeaId(groupId);

                    // generate final group name
                    var groupName = "";
                    var groupNumber = 0;
                    var groupNameLastest = _finalGroupService.GetLatestGroupName(codeOfGroupName);
                    if (groupNameLastest == null)
                    {
                        groupName = $"{codeOfGroupName}_G1";
                        groupNumber = 1;
                    }
                    else
                    {
                        var groupNameStrs = groupNameLastest.Split("_");
                        groupNumber = Convert.ToInt32(groupNameStrs[1].Substring(1, groupNameStrs[1].Length - 1)) + 1;
                        groupName = $"{codeOfGroupName}_G{groupNumber}";
                    }
                    //create final group
                    var finalGroupId = _finalGroupService.AddRegisteredGroupToFinalGroup(groupIdea, groupName);

                    // get detail registration request
                    var detailRegistration = _registeredGroupService.GetDetailRegistrationOfStudentByGroupIdeaId(registeredGroupID);

                    //get infor student in group idea 
                    var listInforStudentInGroupIdea = _student_GroupIdeaService.GetInforStudentInGroupIdea(detailRegistration.GroupIdea.GroupIdeaID);

                    //sent notification for student and set final group for student
                    var notificationContent = "Your group's registration request has been approved";
                    var attachedLink = "/MyGroup/Index";
                    var listEmailMemberInGroup = "";
                    for (int i = 0; i < listInforStudentInGroupIdea.Count; i++)
                    {
                        var groupNameOfStudent = $"{listInforStudentInGroupIdea[i].Student.Specialty.CodeOfGroupName}_G{groupNumber}";
                        if (listInforStudentInGroupIdea[i].Status == 1)
                        {
                            if (_studentService.SetFinalGroupForStudent(finalGroupId, 1, listInforStudentInGroupIdea[i].StudentID, groupNameOfStudent) == 1)
                            {
                                _notificationService.InsertDataNotification(listInforStudentInGroupIdea[i].StudentID, notificationContent, attachedLink);
                            }
                        }
                        else
                        {
                            if (_studentService.SetFinalGroupForStudent(finalGroupId, 0, listInforStudentInGroupIdea[i].StudentID, groupNameOfStudent) == 1)
                            {
                                _notificationService.InsertDataNotification(listInforStudentInGroupIdea[i].StudentID, notificationContent, attachedLink);
                            }
                        }
                        listEmailMemberInGroup += _userService.GetUserByID(listInforStudentInGroupIdea[i].StudentID).FptEmail + ",";
                    }
                    //send email notification for student submit registration
                    string subject = $"Accepted registration request of group {detailRegistration.GroupIdea.ProjectEnglishName}";
                    string body = "";
                    body += "<h2 style='color:black'>Capstone Project Registration System</h2>";
                    body += "<p style='color:black'>Hello!</p>";
                    body += $"<p style='color:black'>You are receiving this email because Your group's registration request has been approved.</p>";
                    body += "<p style='color:black'>Regards,</p>";
                    body += "<p style='color:black'>Capstone Project Registration System</p>";
                    _mailService.SendMailNotification(listEmailMemberInGroup, null, subject, body);
                    _student_GroupIdeaService.DeleteGroupIdeaAndStudentInGroupIdea(groupIdea.GroupIdeaID);
                    _student_FavoriteGroupIdeaService.DeleteAllRecordOfAGroupIdea(groupIdea.GroupIdeaID);
                    return Json(true);
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
        public JsonResult GetDetailRegistrationOfGroupStudent(int registeredGroupId)
        {
            var detailRegistration = _registeredGroupService.GetDetailRegistrationOfStudentByGroupIdeaId(registeredGroupId);
            var members = new List<Student>();
            var listFptEmailRegistraion = detailRegistration.Students_Registration.Split(",");
            for (int i = 0; i < listFptEmailRegistraion.Length - 1; i++)
            {
                members.Add(_studentService.GetInforStudentHaveRegisteredGroup
                    (listFptEmailRegistraion[i], detailRegistration.GroupIdea.GroupIdeaID));
            }
            return Json(new
            {
                detailRegistration = detailRegistration,
                listInforStudentInGroupIdea = members,
            });
        }

        [HttpPost]
        public JsonResult RejectRegisteredGrop(string staffComment, int registeredGroupId)
        {
            try
            {
                if (registeredGroupId != 0 || staffComment != null || staffComment != "")
                {
                    if (_registeredGroupService.UpdateStaffCommentByRegisteredGroupID(staffComment, registeredGroupId) == 1)
                    {
                        // get detail registration group
                        var detailRegistration = _registeredGroupService.GetDetailRegistrationOfStudentByGroupIdeaId(registeredGroupId);

                        //get groupIdeaId by registeredGroup
                        var groupId = _registeredGroupService.GetGroupIDByRegisteredGroupId(registeredGroupId).GroupIdea.GroupIdeaID;

                        //get groupIdea by groupIdeaId
                        var groupIdea = _groupIdeaService.GetGroupIdeaById(groupId);

                        // send notification for student
                        var notificationContent = "Your group's registration request has been rejected";
                        var attachedLink = "/MyGroup/Index";
                        var members = _student_GroupIdeaService.GetInforStudentInGroupIdea(detailRegistration.GroupIdea.GroupIdeaID);
                        var listEmailMemberInGroup = "";
                        for (int i = 0; i < members.Count; i++)
                        {
                            listEmailMemberInGroup += _userService.GetUserByID(members[i].StudentID).FptEmail + ",";
                            //send notification reject to student
                            _notificationService.InsertDataNotification(members[i].StudentID, notificationContent, attachedLink);
                        }
                        //send email notification for student submit registration
                        string subject = $"Rejected registration request of group {groupIdea.ProjectEnglishName}";
                        string body = "";
                        body += "<h2 style='color:black'>Capstone Project Registration System</h2>";
                        body += "<p style='color:black'>Hello!</p>";
                        body += $"<p style='color:black'>You are receiving this email because Your group's registration request has been rejected.</p>";
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

        [HttpPost]
        public JsonResult GetListRegisteredGroup(int status, string searchText, string pagingType, int recordNumber)
        {
            var semester = _semesterService.GetCurrentSemester();
            int numberOfRecordsPerPage = 5;
            int startNum = (recordNumber != 0) ? Convert.ToInt32(recordNumber) : 1;

            //click next page
            if (pagingType.Equals("previous")) startNum = startNum - numberOfRecordsPerPage;
            //click next page
            else if (pagingType.Equals("next")) startNum = startNum + numberOfRecordsPerPage;

            int countResult = _registeredGroupService.CountRecordRegisteredGroupSearchList(semester.SemesterID, status, searchText);

            var registeredGroups = _registeredGroupService.GetRegisteredGroupsBySearch(semester.SemesterID, status, searchText, startNum - 1, numberOfRecordsPerPage);
            return Json(
                new
                {
                    numberOfRecordsPerPage = numberOfRecordsPerPage,
                    startNum = startNum,
                    countResult = countResult,
                    registeredGroups = registeredGroups,
                });
        }

        [HttpPost]
        public JsonResult RejectWhenAccepted(int registeredGroupID, string commentReject)
        {
            try
            {
                if (registeredGroupID != 0 || commentReject != null || commentReject != "")
                {
                    //get current semester
                    var currentSemester = _semesterService.GetCurrentSemester();
                    //get groupideaid by registeredgroupid
                    var groupId = _registeredGroupService.GetGroupIDByRegisteredGroupId(registeredGroupID).GroupIdea.GroupIdeaID;
                    // get detail registration group by group idea 
                    var detailRegistration = _registeredGroupService.GetDetailRegistrationOfStudentByGroupIdeaId(registeredGroupID);
                    //get fptEmail of leader registered 
                    var fptEmailOfStudent = detailRegistration.Students_Registration.Split(",");
                    var emailLeader = detailRegistration.Students_Registration.Split(",").FirstOrDefault();

                    //get finalGroupId by student
                    var finalGroupIdOfStudent = _studentService.GetStudentByFptEmail(emailLeader, currentSemester.SemesterID).FinalGroup.FinalGroupID;

                    if (_registeredGroupService.RejectWhenRegisteredGroupAccepted
                        (registeredGroupID, commentReject, groupId, finalGroupIdOfStudent) >= 1)
                    {
                        // delete change topic request by final group 
                        _changeTopicRequestService.DeleteChangeTopicRequestsByFinalGroup(finalGroupIdOfStudent);

                        // recovery student in group idea 
                        for (int i = 0; i < fptEmailOfStudent.Length - 1; i++)
                        {
                            _student_GroupIdeaService.RecoveryStudentInGroupIdeaAfterRejected
                                (_studentService.GetStudentByFptEmail(fptEmailOfStudent[i].ToString(),currentSemester.SemesterID).StudentID
                                , detailRegistration.GroupIdea.GroupIdeaID);
                        }

                        //notification reject to student
                        var notificationContent = "Your group's registration request has been rejected";
                        var attachedLink = "/MyGroup/Index";
                        var members = _student_GroupIdeaService.GetInforStudentInGroupIdea(detailRegistration.GroupIdea.GroupIdeaID);
                        string listEmailMemberInGroup = "";
                        for (int i = 0; i < members.Count; i++)
                        {
                            listEmailMemberInGroup += _userService.GetUserByID(members[i].StudentID).FptEmail + ",";
                            //send notification reject to student
                            _notificationService.InsertDataNotification(members[i].StudentID, notificationContent, attachedLink);
                        }


                        //send email notification for student submit registration
                        string subject = $"Rejected registration request of group {detailRegistration.GroupIdea.ProjectEnglishName}";
                        string body = "";
                        body += "<h2 style='color:black'>Capstone Project Registration System</h2>";
                        body += "<p style='color:black'>Hello!</p>";
                        body += $"<p style='color:black'>You are receiving this email because Your group's registration request has been rejected.</p>";
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
