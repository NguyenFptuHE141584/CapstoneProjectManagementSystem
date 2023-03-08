using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Common;
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

namespace CapstoneProjectManagementSystem.Controllers.Student_Controller
{
    [Authorize(Roles = "Student")]
    [ServiceFilter(typeof(SemesterFilter))]
    [ServiceFilter(typeof(StudentProfileFilter))]
    public class CreateIdeaController : Controller
    {
        private readonly IProfessionService _professionService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IStudent_GroupIdeaService _studentGroupIdeaService;
        private readonly ISessionExtensionService _sessionExtensionService;
        private readonly IStudentService _studentService;
        private readonly ISemesterService _semesterService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IFinalGroupService _finalGroupService;

        public string ErrorMessage { get; set; }

        public CreateIdeaController(IProfessionService professionService,
                                    ISpecialtyService specialtyService,
                                    IStudent_GroupIdeaService studentGroupIdeaService,
                                    ISessionExtensionService sessionExtensionService,
                                    IStudentService studentService,
                                    ISemesterService semesterService,
                                    INotificationService notificationService,
                                    IUserService userService,
                                    IFinalGroupService finalGroupService)
        {
            _professionService = professionService;
            _specialtyService = specialtyService;
            _studentGroupIdeaService = studentGroupIdeaService;
            _sessionExtensionService = sessionExtensionService;
            _studentService = studentService;
            _semesterService = semesterService;
            _notificationService = notificationService;
            _userService = userService;
            _finalGroupService = finalGroupService;
        }
        public IActionResult Index()
        {
            
            var studentId = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount").UserID;
            //check semester
            if (_semesterService.GetCurrentSemester() != null)
            {
                var checkStatusStudentHaveIdea = _studentGroupIdeaService.FilterStudentHaveIdea(studentId, _semesterService.GetCurrentSemester().SemesterID);
                var student = _studentService.GetStudentByStudentId(studentId);
                if (checkStatusStudentHaveIdea || student.FinalGroup.FinalGroupID != 0)
                {
                    return View("/Views/Common_View/AccessDeniedForCreateGroup.cshtml");
                }
                else
                {
                    ViewBag.Student = _studentService.GetStudentByStudentId(studentId);
                    ViewBag.ProfessionList = _professionService.getAllProfession(_semesterService.GetCurrentSemester().SemesterID);
                    return View("/Views/Student_View/CreateIdea/Index.cshtml");
                }
            }
            else
            {
                ViewBag.Student = _studentService.GetStudentByStudentId(studentId);
                ViewBag.ProfessionList = _professionService.getAllProfession(_semesterService.GetCurrentSemester().SemesterID);
                return View("/Views/Student_View/CreateIdea/Index.cshtml");
            }
        }

        [HttpPost]
        public JsonResult GetMaxMemberOfSpecialty([FromBody] int specialtyId)
        {
            return Json(_specialtyService.getSpecialtyById(specialtyId));
        }

        [HttpPost]
        public JsonResult CheckStudentExistWhenBeforeAdded([FromBody] string fptEmail)
        {
            var currentSemester = _semesterService.GetCurrentSemester();
            if (!_studentGroupIdeaService.CheckAddedStudentIsValid(fptEmail))
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
        public JsonResult CreateIdea([FromBody] GroupIdea groupIdea)
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
                    var studentId = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount").UserID;
                    var semester = _semesterService.GetCurrentSemester();
                    if(_studentGroupIdeaService.FilterStudentHaveIdea(studentId,semester.SemesterID) == true)
                    {
                        return Json(false);
                    }
                    else
                    {
                        _studentGroupIdeaService.DeleteAllRequest(studentId);
                        var groupId = _studentGroupIdeaService.CreateGroupIdea(groupIdea, studentId, _semesterService.GetCurrentSemester().SemesterID, groupIdea.MaxMember);
                        //insert notification invite member when create idea
                        string attachedLink = $"/MyRequest/Index";
                        foreach (var item in groupIdea.Students)
                        {
                            if (item.StudentID == studentId)
                                _studentGroupIdeaService.AddRecord(studentId, groupId, 1, "");
                            else
                            {
                                _studentGroupIdeaService.AddRecord(item.StudentID, groupId, 4, "");
                                string contentNotification = "";
                                contentNotification += $"{_userService.GetNameStudentByUserId(studentId)} invited you to join their group <b>{groupIdea.ProjectEnglishName}.</b> Click here to see.";
                                _notificationService.InsertDataNotification(item.StudentID, contentNotification, attachedLink);
                            }
                        }
                        return Json(true);
                    }
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Create idea invalid";
                return Json(false);
            }
        }

        [HttpPost]
        public JsonResult GetCorrespondingSpecialty([FromBody] string data)
        {

            List<Specialty> specialtyList = _specialtyService.getSpecialtiesByProfessionId(Convert.ToInt32(data), _semesterService.GetCurrentSemester().SemesterID);
            return Json(specialtyList);
        }
    }
}
