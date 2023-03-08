using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Common;
using CapstoneProjectManagementSystem.Models.Dao;
using CapstoneProjectManagementSystem.Services;
using CapstoneProjectManagementSystem.Services.CustomHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CapstoneProjectManagementSystem.Controllers.Student_Controller
{
    [Authorize(Roles = "Student")]
    [ServiceFilter(typeof(SemesterFilter))]
    [ServiceFilter(typeof(StudentProfileFilter))]
    public class StudentHomeController : Controller
    {
        private readonly IProfessionService _professionService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IGroupIdeaService _groupIdeaService;
        private readonly ISemesterService _semesterService;
        private readonly IGroupIdeaDisplayFormService _groupIdeaDisplayFormService;
        private readonly IStudent_GroupIdeaService _studentGroupIdeaService;
        private readonly ISessionExtensionService _sessionExtensionService;
        private readonly IUserService _userService;
        private readonly IStudent_FavoriteGroupIdeaService _student_FavoriteGroupIdeaService;
        public StudentHomeController(IProfessionService professionService,
                                    ISpecialtyService specialtyService,
                                    IGroupIdeaService groupIdeaService,
                                    ISemesterService semesterService,
                                    IGroupIdeaDisplayFormService groupIdeaDisplayFormService,
                                    IStudent_GroupIdeaService studentGroupIdeaService,
                                    ISessionExtensionService sessionExtensionService,
                                    IUserService userService,
                                    IStudent_FavoriteGroupIdeaService student_FavoriteGroupIdeaService)
        {
            _professionService = professionService;
            _specialtyService = specialtyService;
            _groupIdeaService = groupIdeaService;
            _semesterService = semesterService;
            _groupIdeaDisplayFormService = groupIdeaDisplayFormService;
            _studentGroupIdeaService = studentGroupIdeaService;
            _sessionExtensionService = sessionExtensionService;
            _userService = userService;
            _student_FavoriteGroupIdeaService = student_FavoriteGroupIdeaService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (_semesterService.GetCurrentSemester() != null)
            {
                User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
                TempData["CheckProfile"] = _userService.CheckProfileUserHaveAttributeIsNullByUserId(user.UserID);
                ViewBag.ProfessionList = _professionService.getAllProfession(_semesterService.GetCurrentSemester().SemesterID);
            }
            return View("/Views/Student_View/Home/Index.cshtml");
        }
        [HttpPost]
        public IActionResult Index(string profession_id, string specialty_id, string searchText, string pagingType, string recordNumber)
        {
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            TempData["CheckProfile"] = _userService.CheckProfileUserHaveAttributeIsNullByUserId(user.UserID);
            //get number of record per page
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            int numberOfRecordsPerPage = Convert.ToInt32(config.GetSection("paging")["numberOfRecordPerPage"]);

            int startNum = (recordNumber != null) ? Convert.ToInt32(recordNumber) : 1;
            //click previous page
            if (pagingType.Equals("previous")) startNum = startNum - numberOfRecordsPerPage;
            //click next page
            else if (pagingType.Equals("next")) startNum = startNum + numberOfRecordsPerPage;

            int countResult = 0;
            List<GroupIdea> groupIdeaList = new List<GroupIdea>();
            if (_studentGroupIdeaService.FilterStudentHaveIdea(user.UserID, _semesterService.GetCurrentSemester().SemesterID))
            {
                //number of results
                countResult = _groupIdeaService.getNumberOfResultWhenSearch_2(_semesterService.GetCurrentSemester().SemesterID,
                                                                               Convert.ToInt32(profession_id),
                                                                               Convert.ToInt32(specialty_id),
                                                                               searchText,
                                                                               user.UserID);

                TempData["startNum"] = startNum;
                TempData["numberOfRecordsPerPage"] = numberOfRecordsPerPage;
                //list result
                groupIdeaList = _groupIdeaService.GetGroupIdeaSearchList_2(_semesterService.GetCurrentSemester().SemesterID,
                                                                               Convert.ToInt32(profession_id),
                                                                               Convert.ToInt32(specialty_id),
                                                                               searchText,
                                                                               user.UserID,
                                                                               startNum - 1,
                                                                               numberOfRecordsPerPage);
            }
            else
            {
                //number of results
                countResult = _groupIdeaService.getNumberOfResultWhenSearch(_semesterService.GetCurrentSemester().SemesterID,
                                                                               Convert.ToInt32(profession_id),
                                                                               Convert.ToInt32(specialty_id),
                                                                               searchText);

                TempData["startNum"] = startNum;
                TempData["numberOfRecordsPerPage"] = numberOfRecordsPerPage;
                //list result
                groupIdeaList = _groupIdeaService.GetGroupIdeaSearchList(_semesterService.GetCurrentSemester().SemesterID,
                                                                               Convert.ToInt32(profession_id),
                                                                               Convert.ToInt32(specialty_id),
                                                                               searchText,
                                                                               startNum - 1,
                                                                               numberOfRecordsPerPage);
            }
            TempData["countResult"] = countResult;
            if (groupIdeaList != null)
            {
                List<GroupIdeaDisplayForm> groupIdeaDisplayFormList = _groupIdeaDisplayFormService.ConvertFromGroupIdeaList(groupIdeaList);
                ViewBag.GroupIdeaDisplayFormList = groupIdeaDisplayFormList;
            }
            int semesterId = _semesterService.GetCurrentSemester().SemesterID;
            TempData["old_profession_id"] = profession_id;
            ViewBag.ProfessionList = _professionService.getAllProfession(semesterId);
            TempData["old_specialty_id"] = specialty_id;
            if (!profession_id.Equals("0"))
            {
                ViewBag.SpecialtyList = _specialtyService.getSpecialtiesByProfessionId(Convert.ToInt32(profession_id), semesterId);
            }
            TempData["old_searchText"] = searchText;
            return View("/Views/Student_View/Home/Index.cshtml");
        }
        [HttpPost]
        public JsonResult AddToFavorites([FromBody] int groupId)
        {
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            if (_student_FavoriteGroupIdeaService.GetRecord(user.UserID, groupId) != null)
            {
                return Json(false);
            }
            else
            {
                _student_FavoriteGroupIdeaService.AddRecord(user.UserID, groupId);
                return Json(true);
            }
        }
        [HttpPost]
        public JsonResult RemoveFromFavorites([FromBody] int groupId)
        {
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            _student_FavoriteGroupIdeaService.DeleteRecord(user.UserID, groupId);
            return Json(true);
        }
        [HttpPost]
        public JsonResult GetCorrespondingSpecialty([FromBody] string data)
        {
            List<Specialty> specialtyList = _specialtyService.getSpecialtiesByProfessionId(Convert.ToInt32(data), _semesterService.GetCurrentSemester().SemesterID);
            return Json(specialtyList);
        }
        [HttpPost]
        public JsonResult GetFavoritesList([FromBody] string data)
        {
            User user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            List<StudentFavoriteGroupIdea> favoritesIdList = _student_FavoriteGroupIdeaService.GetFavoriteIdeaListByStudentId(user.UserID);
            List<GroupIdeaDisplayForm> favoritesGroupIdeaDisplayFormList = new List<GroupIdeaDisplayForm>();
            if (favoritesIdList != null)
            {
                foreach (StudentFavoriteGroupIdea item in favoritesIdList.ToList())
                {
                    string groupId = _studentGroupIdeaService.GetGroupIdByStudentId(user.UserID);
                    if (groupId != null)
                    {
                        if (item.GroupIdeaID == Convert.ToInt32(groupId))
                        {
                            if (favoritesIdList.Count == 1) favoritesIdList = null;
                            else favoritesIdList.Remove(item);
                            break;
                        }
                    }
                }
            }
            if (favoritesIdList != null)
            {
                List<GroupIdea> favoritesGroupIdeaList = new List<GroupIdea>();
                foreach (StudentFavoriteGroupIdea item in favoritesIdList)
                {
                    favoritesGroupIdeaList.Add(_groupIdeaService.GetGroupIdeaById(item.GroupIdeaID));
                }
                favoritesGroupIdeaDisplayFormList = _groupIdeaDisplayFormService.ConvertFromGroupIdeaList(favoritesGroupIdeaList);
                ViewBag.FavoritesGroupIdeaDisplayFormList = favoritesGroupIdeaDisplayFormList;
            }
            return Json(favoritesGroupIdeaDisplayFormList);
        }
    }
}
