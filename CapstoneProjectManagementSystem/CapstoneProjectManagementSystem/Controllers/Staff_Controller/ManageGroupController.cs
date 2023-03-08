using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Common;
using CapstoneProjectManagementSystem.Services;
using CapstoneProjectManagementSystem.Services.CommonServices;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Controllers.Staff_Controller
{
    [Authorize(Roles = "Staff")]
    public class ManageGroupController : Controller
    {
        private readonly IProfessionService _professionService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IFinalGroupService _finalGroupService;
        private readonly IFinalGroupDisplayFormService _finalGroupDisplayFormService;
        private readonly ISemesterService _semesterService;
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;
        private readonly IChangeFinalGroupRequestService _changeFinalGroupRequestService;
        private readonly IChangeTopicRequestService _changeTopicRequestService;
        private readonly INotificationService _notificationService;
        private readonly IMailService _mailService;

        public ManageGroupController(IProfessionService professionService,
                                        ISpecialtyService specialtyService,
                                        IFinalGroupService finalGroupService,
                                        IFinalGroupDisplayFormService finalGroupDisplayFormService,
                                        ISemesterService semesterService,
                                        IStudentService studentService,
                                        IUserService userService,
                                        IChangeFinalGroupRequestService changeFinalGroupRequestService,
                                        IChangeTopicRequestService changeTopicRequestService,
                                        INotificationService notificationService,
                                        IMailService mailService)
        {
            _professionService = professionService;
            _specialtyService = specialtyService;
            _finalGroupService = finalGroupService;
            _finalGroupDisplayFormService = finalGroupDisplayFormService;
            _semesterService = semesterService;
            _studentService = studentService;
            _userService = userService;
            _changeFinalGroupRequestService = changeFinalGroupRequestService;
            _changeTopicRequestService = changeTopicRequestService;
            _notificationService = notificationService;
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
                    TempData["showGroupName"] = _semesterService.GetCurrentSemester().ShowGroupName ? "true" : "false";
                    ViewBag.ProfessionList = professionList;
                    ViewBag.SemesterList = _semesterService.GetAllSemester();
                    return View("/Views/Staff_View/ManageGroup/ManageGroup.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Index", "SemesterManage");
            }
        }
        [HttpPost]
        public IActionResult Index(string profession_id, string specialty_id, string searchText)
        {
            //List Final Group has full member
            List<FinalGroup> fullMemberFinalGroupList = _finalGroupService.GetFullMemberFinalGroupSearchList(_semesterService.GetCurrentSemester().SemesterID,
                                                                           Convert.ToInt32(profession_id),
                                                                           Convert.ToInt32(specialty_id),
                                                                           searchText,
                                                                           0,
                                                                           int.MaxValue);
            List<FinalGroupDisplayForm> fullMemberFinalGroupDisplayFormList = _finalGroupDisplayFormService.ConvertFromFinalList(fullMemberFinalGroupList);
            if (fullMemberFinalGroupDisplayFormList != null)
            {
                ViewBag.FullMemberFinalGroupList = fullMemberFinalGroupDisplayFormList;
            }
            //List Final Group lack of member
            List<FinalGroup> lackOfMemberFinalGroupList = _finalGroupService.GetLackOfMemberFinalGroupSearchList(_semesterService.GetCurrentSemester().SemesterID,
                                                                           Convert.ToInt32(profession_id),
                                                                           Convert.ToInt32(specialty_id),
                                                                           searchText,
                                                                           0,
                                                                           int.MaxValue);
            List<FinalGroupDisplayForm> lackOfMemberFinalGroupDisplayFormList = _finalGroupDisplayFormService.ConvertFromFinalList(lackOfMemberFinalGroupList);
            if (lackOfMemberFinalGroupDisplayFormList != null)
            {
                ViewBag.LackOfMemberFinalGroupList = lackOfMemberFinalGroupDisplayFormList;
            }
            //List Student that not have group
            List<Student> studentList = _studentService.GetStudentSearchList(_semesterService.GetCurrentSemester().SemesterID,
                                                                           Convert.ToInt32(profession_id),
                                                                           Convert.ToInt32(specialty_id),
                                                                           0,
                                                                           int.MaxValue);
            if (studentList != null)
            {
                foreach (Student stu in studentList)
                {
                    string specialtyFullName = _specialtyService.getSpecialtyById(stu.Specialty.SpecialtyID).SpecialtyFullName;
                    stu.Specialty.SpecialtyFullName = specialtyFullName;
                }
                ViewBag.StudentList = studentList;
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
            TempData["showGroupName"] = _semesterService.GetCurrentSemester().ShowGroupName ? "true" : "false";
            ViewBag.SemesterList = _semesterService.GetAllSemester();
            return View("/Views/Staff_View/ManageGroup/ManageGroup.cshtml");
        }

        [HttpPost]
        public JsonResult GetCorrespondingSpecialty([FromBody] string data)
        {
            List<Specialty> specialtyList = _specialtyService.getSpecialtiesByProfessionId(Convert.ToInt32(data), _semesterService.GetCurrentSemester().SemesterID);
            return Json(specialtyList);
        }
        [HttpPost]
        public JsonResult GetGroupInfo([FromBody] string data)
        {
            FinalGroup finalGroup = _finalGroupService.getFinalGroupById(Convert.ToInt32(data));
            finalGroup.Profession.ProfessionFullName = _professionService.getProfessionById(finalGroup.Profession.ProfessionID).ProfessionFullName;
            finalGroup.Specialty.SpecialtyFullName = _specialtyService.getSpecialtyById(finalGroup.Specialty.SpecialtyID).SpecialtyFullName;
            Student leader = _studentService.getLeaderByFinalGroupId(Convert.ToInt32(data));
            finalGroup.Students.Add(leader);
            List<Student> memberList = _studentService.getListMemberByFinalGroupId(Convert.ToInt32(data));
            if (memberList != null)
            {
                foreach (Student member in memberList)
                {
                    finalGroup.Students.Add(member);
                }
            }
            return Json(finalGroup);
        }
        [HttpPost]
        public JsonResult AddMemberToGroup([FromBody] Student_Group data)
        {
            var attachedLink = "/MyGroup/Index";
            Student student = _studentService.GetStudentByFptEmail(data.fptEmail, _semesterService.GetCurrentSemester().SemesterID);
            FinalGroup finalGroup = _finalGroupService.getFinalGroupById(Convert.ToInt32(data.groupId));
            if (student != null && student.FinalGroup.FinalGroupID == 0)
            {
                //check if group is ful member (Note: change to allow add member to group even when group is full)
                //if (finalGroup.MaxMember == finalGroup.NumberOfMember)
                //{
                //    return Json(false);
                //}
                //else
                //{

                //send notify
                _notificationService.InsertDataNotification(student.StudentID, "You have been added to group " + finalGroup.GroupName + " by Staff", attachedLink);
                //send mail
                string receiver = student.StudentID + ",";
                var subject = $"You have been added to group {finalGroup.GroupName}";
                var body = "<p>Hello, </p>";
                body += $"<p>You have been added to group {finalGroup.GroupName} by Staff in Capstone Project Registration System. </p>"
                        + "<p>This group will be your capstone project/ thesis group for this semester. </p>"
                        + "<p>Please access Capstone Project Registration System to see more details.</p>";
                _mailService.SendMailNotification(receiver, null, subject, body);

                // for each member in group
                foreach (Student item in _studentService.GetListStudentIdByFinalGroupId(finalGroup.FinalGroupID))
                {
                    //send mail
                    _notificationService.InsertDataNotification(item.StudentID, _userService.GetNameStudentByUserId(student.StudentID) + " have been added to your group by Staff", attachedLink);
                    //send mail
                    receiver = item.StudentID + ",";
                    subject = $"{_userService.GetNameStudentByUserId(student.StudentID)} have been added to your group";
                    body = "<p>Hello, </p>";
                    body += $"<p>{student.StudentID} have been added to your group by Staff in Capstone Project Registration System. </p>"
                            + "<p>Please access Capstone Project Registration System to see more details.</p>";
                    _mailService.SendMailNotification(receiver, null, subject, body);
                }
                _studentService.UpdateStudentByGroupId(Convert.ToInt32(data.groupId), finalGroup.GroupName, 0, student.StudentID);
                _finalGroupService.UpdateNumberOfMemberWhenAdd(Convert.ToInt32(data.groupId));
                student.GroupName = finalGroup.GroupName;
                return Json(student);
                //}
            }
            else
            {
                return Json(null);
            }
        }
        [HttpPost]
        public JsonResult DeleteMember([FromBody] string userId)
        {
            var attachedLink = "/MyGroup/Index";
            Student student = _studentService.GetStudentByStudentId(userId);
            int finalGroupId = student.FinalGroup.FinalGroupID;
            //set leader to another member when delete leader
            if (student.IsLeader == true)
            {
                List<Student> memberList = _studentService.getListMemberByFinalGroupId(finalGroupId);
                if (memberList != null)
                {
                    foreach (Student item in memberList)
                    {
                        _studentService.SetFinalGroupForStudent(item.FinalGroup.FinalGroupID, 1, item.StudentID, item.GroupName);
                        _notificationService.InsertDataNotification(item.StudentID, "You have been promoted to team leader of group " + item.GroupName + " by Staff", attachedLink);
                        break;
                    }
                }
            }
            _notificationService.InsertDataNotification(student.StudentID, "You have been removed from your group by Staff", attachedLink);
            //send mail
            string receiver = student.StudentID + ",";
            var subject = "You have been removed from your group";
            var body = "<p>Hello, </p>";
            body += "<p>You have been removed from your group by Staff in Capstone Project Registration System. </p>"
                    + "<p>Please access Capstone Project Registration System to see more details.</p>";
            _mailService.SendMailNotification(receiver, null, subject, body);

            int count = _studentService.DeleteFinalGroupIdOfStudent(student.StudentID);
            List<Student> memberList2 = _studentService.GetListStudentIdByFinalGroupId(finalGroupId);
            if (memberList2 != null)
            {
                foreach (Student item in memberList2)
                {
                    _notificationService.InsertDataNotification(item.StudentID, _userService.GetNameStudentByUserId(student.StudentID) + " have been removed from your group by Staff", attachedLink);
                    //send mail
                    receiver = item.StudentID + ",";
                    subject = $"{_userService.GetNameStudentByUserId(student.StudentID)} have been removed from your group";
                    body = "<p>Hello, </p>";
                    body += $"<p>{student.StudentID} have been removed from your group by Staff in Capstone Project Registration System. </p>"
                            + "<p>Please access Capstone Project Registration System to see more details.</p>";
                    _mailService.SendMailNotification(receiver, null, subject, body);
                }
            }
            if (count == 1)
            {
                _finalGroupService.UpdateNumberOfMemberWhenRemove(finalGroupId);
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        [HttpPost]
        public JsonResult DeleteGroup([FromBody] string groupId)
        {
            var notificationContent = "Your capstone project/ thesis group have been deleted by Staff";
            var attachedLink = "/MyGroup/Index";
            Student leader = _studentService.getLeaderByFinalGroupId(Convert.ToInt32(groupId));
            _studentService.DeleteFinalGroupIdOfStudent(leader.StudentID);
            //send notify
            _notificationService.InsertDataNotification(leader.StudentID, notificationContent, attachedLink);
            List<Student> memberList = _studentService.getListMemberByFinalGroupId(Convert.ToInt32(groupId));
            if (memberList != null)
            {
                foreach (Student mem in memberList)
                {
                    _studentService.DeleteFinalGroupIdOfStudent(mem.StudentID);
                    //send notify
                    _notificationService.InsertDataNotification(mem.StudentID, notificationContent, attachedLink);
                    //send mail
                    string receiver = mem.StudentID + ",";
                    var subject = "Your group have been deleted";
                    var body = "<p>Hello, </p>";
                    body += "<p>Your group have been deleted by Staff in Capstone Project Registration System. </p>"
                            + "<p>Please access Capstone Project Registration System to see more details.</p>";
                    _mailService.SendMailNotification(receiver, null, subject, body);
                }
            }
            _finalGroupService.DeleteFinalGroup(Convert.ToInt32(groupId));
            //_changeFinalGroupRequestService.DeleteChangeMemberRequestsByFinalGroup(Convert.ToInt32(groupId));
            _changeTopicRequestService.DeleteChangeTopicRequestsByFinalGroup(Convert.ToInt32(groupId));
            return Json(null);
        }

        [HttpPost]
        public JsonResult UpdateGroupName([FromBody] FinalGroup group)
        {
            _finalGroupService.UpdateGroupName(group.FinalGroupID, group.GroupName);
            return Json(null);
        }
        [HttpPost]
        public JsonResult UpdateGroupNameOfStudent([FromBody] Student student)
        {
            _studentService.UpdateGroupName(student.StudentID, student.GroupName);
            return Json(null);
        }
        [HttpPost]
        public JsonResult ChangeShowGroupNameStatus()
        {
            Semester semester = _semesterService.GetCurrentSemester();
            if (semester.ShowGroupName) _semesterService.ChangeShowGroupNameStatus(semester.SemesterID, 0);
            else _semesterService.ChangeShowGroupNameStatus(semester.SemesterID, 1);
            return Json("success");
        }
        [HttpPost]
        public JsonResult Grouping([FromBody] int specialtyId)
        {
            Specialty specialty = _specialtyService.getSpecialtyById(specialtyId);
            int maxMember = specialty.MaxMember;
            int semesterId = _semesterService.GetCurrentSemester().SemesterID;
            int professionId = specialty.Profession.ProfessionID;
            int countMem = 0;
            int countGroup = 0;
            int finalGroupId = 0;
            string groupName = "";
            var notificationContent = "";
            var attachedLink = "/MyGroup/Index";
            List<Student> studentList = _studentService.getListStudentNotHaveGroupBySpecialtyId(_semesterService.GetCurrentSemester().SemesterID, specialtyId);
            if (studentList != null)
            {
                foreach (Student student in studentList)
                {
                    if ((countMem % maxMember) == 0)
                    {
                        countGroup++;
                        countMem++;
                        //generate group name
                        var codeOfGroupName = specialty.CodeOfGroupName;
                        var groupNumber = 1;
                        var groupNameLastest = _finalGroupService.GetLatestGroupName(codeOfGroupName);
                        if (groupNameLastest == null)
                        {
                            groupName = $"{codeOfGroupName}_G1";
                        }
                        else
                        {
                            var groupNameStrs = groupNameLastest.Split("_");
                            groupNumber = Convert.ToInt32(groupNameStrs[1].Substring(1, groupNameStrs[1].Length - 1)) + 1;
                            groupName = $"{codeOfGroupName}_G{groupNumber}";
                        }
                        //generate final group
                        finalGroupId = _finalGroupService.CreateFinalGroup(semesterId,
                                                            professionId,
                                                            specialtyId,
                                                            groupName,
                                                            "Project English Name " + groupNumber,
                                                            "Abbreviation " + groupNumber,
                                                            "Project Vietnamese Name " + groupNumber,
                                                            maxMember,
                                                            1);
                        _studentService.UpdateStudentByGroupId(finalGroupId, groupName, 1, student.StudentID);
                        //send notify
                        notificationContent = "You have been automatically added to group " + groupName + ". This will be your capstone project/ thesis group for this semester";
                        _notificationService.InsertDataNotification(student.StudentID, notificationContent, attachedLink);
                        //send mail
                        string receiver = student.StudentID + ",";
                        var subject = $"You have been added to group {groupName}";
                        var body = "<p>Hello, </p>";
                        body += $"<p>You have been automatically added to group {groupName} in Capstone Project Registration System. </p>"
                                + "<p>This group will be your capstone project/ thesis group for this semester. </p>"
                                + "<p>Please access Capstone Project Registration System to see more details.</p>";

                        _mailService.SendMailNotification(receiver, null, subject, body);
                    }
                    else
                    {
                        countMem++;
                        _studentService.UpdateStudentByGroupId(finalGroupId, groupName, 0, student.StudentID);
                        _finalGroupService.UpdateNumberOfMemberWhenAdd(finalGroupId);
                        //send notify
                        _notificationService.InsertDataNotification(student.StudentID, notificationContent, attachedLink);
                        //send mail
                        string receiver = student.StudentID + ",";
                        var subject = $"You have been added to group {groupName}";
                        var body = "<p>Hello, </p>";
                        body += $"<p>You have been automatically added to group {groupName} in Capstone Project Registration System. </p>"
                                + "<p>This group will be your capstone project/ thesis group for this semester. </p>"
                                + "<p>Please access Capstone Project Registration System to see more details.</p>";

                        _mailService.SendMailNotification(receiver, null, subject, body);
                    }
                }
            }
            return Json(countGroup);
        }
        public IActionResult ExportAllGroupToExcel(string semesterId)
        {
            //prepare data
            List<FinalGroup> finalGroupList = new List<FinalGroup>();
            //currernt semester
            if (semesterId.Equals(_semesterService.GetCurrentSemester().SemesterID.ToString()))
            {
                finalGroupList = _finalGroupService.getAllFinalGroups(Convert.ToInt32(semesterId));

                if (finalGroupList != null)
                {
                    foreach (FinalGroup fg in finalGroupList)
                    {
                        List<Student> studentList = new List<Student>();
                        Student leader = _studentService.getLeaderByFinalGroupId(fg.FinalGroupID);
                        leader.Profession.ProfessionFullName = _professionService.getProfessionById(leader.Profession.ProfessionID).ProfessionFullName;
                        leader.Specialty.SpecialtyFullName = _specialtyService.getSpecialtyById(leader.Specialty.SpecialtyID).SpecialtyFullName;
                        studentList.Add(leader);
                        var listMember = _studentService.getListMemberByFinalGroupId(fg.FinalGroupID);
                        if (listMember != null)
                        {
                            foreach (Student st in listMember)
                            {
                                st.Profession.ProfessionFullName = _professionService.getProfessionById(st.Profession.ProfessionID).ProfessionFullName;
                                st.Specialty.SpecialtyFullName = _specialtyService.getSpecialtyById(st.Specialty.SpecialtyID).SpecialtyFullName;
                                studentList.Add(st);
                            }
                        }
                        fg.Students = studentList;
                    }
                }
            }
            else //old semester
            {

            }
            //generate excel file
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("GroupList");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Group Name (Personnal)";
                worksheet.Cell(currentRow, 2).Value = "Roll Number";
                worksheet.Cell(currentRow, 3).Value = "Full Name";
                worksheet.Cell(currentRow, 4).Value = "Email FPT";
                worksheet.Cell(currentRow, 5).Value = "Phone Number";
                worksheet.Cell(currentRow, 6).Value = "Profession";
                worksheet.Cell(currentRow, 7).Value = "Specialty";
                worksheet.Cell(currentRow, 8).Value = "English Title Of Project";
                worksheet.Cell(currentRow, 9).Value = "Vietnamese Title Of Project";
                worksheet.Cell(currentRow, 10).Value = "Abbreviation Of Project";
                if (finalGroupList != null)
                {
                    foreach (FinalGroup fg in finalGroupList)
                    {
                        foreach (Student st in fg.Students)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = st.GroupName;
                            worksheet.Cell(currentRow, 2).Value = st.RollNumber;
                            worksheet.Cell(currentRow, 3).Value = st.User.FullName;
                            worksheet.Cell(currentRow, 4).Value = st.User.FptEmail;
                            worksheet.Cell(currentRow, 5).Value = (st.PhoneNumber.ToString().Length <= 1) ? "" : "(+84) " + st.PhoneNumber.ToString().Substring(1);
                            worksheet.Cell(currentRow, 6).Value = st.Profession.ProfessionFullName;
                            worksheet.Cell(currentRow, 7).Value = st.Specialty.SpecialtyFullName;
                            worksheet.Cell(currentRow, 8).Value = fg.ProjectEnglishName;
                            worksheet.Cell(currentRow, 9).Value = fg.ProjectVietNameseName;
                            worksheet.Cell(currentRow, 10).Value = fg.Abbreviation;
                        }
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "" + _semesterService.GetSemesterById(Convert.ToInt32(semesterId)).SemesterCode + "_CapstoneProjectGroupList.xlsx");
                }
            }
        }
        public IActionResult ExportAllStudentToExcel(string semesterId)
        {
            //prepare data
            List<FinalGroup> finalGroupList = new List<FinalGroup>();
            //current semester
            if (semesterId.Equals(_semesterService.GetCurrentSemester().SemesterID.ToString()))
            {
                finalGroupList = _finalGroupService.getAllFinalGroups(Convert.ToInt32(semesterId));
                if (finalGroupList != null)
                {
                    foreach (FinalGroup fg in finalGroupList)
                    {
                        List<Student> studentList = new List<Student>();
                        Student leader = _studentService.getLeaderByFinalGroupId(fg.FinalGroupID);
                        studentList.Add(leader);
                        var listMember = _studentService.getListMemberByFinalGroupId(fg.FinalGroupID);
                        if(listMember != null)
                        {
                            foreach (Student st in listMember)
                            {
                                studentList.Add(st);
                            }
                        }
                        fg.Students = studentList;
                    }
                }
            }
            else //old semester
            {

            }
            //generate excel file
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("StudentList");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "RollNumber";
                worksheet.Cell(currentRow, 2).Value = "Ten_De_Tai";

                if(finalGroupList != null)
                {
                    foreach (FinalGroup fg in finalGroupList)
                    {
                        foreach (Student st in fg.Students)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = st.RollNumber;
                            worksheet.Cell(currentRow, 2).Value = fg.ProjectEnglishName + " (" + fg.Abbreviation + ") (" + fg.ProjectVietNameseName + ")";
                        }
                    }
                }
              

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "" + _semesterService.GetSemesterById(Convert.ToInt32(semesterId)).SemesterCode + "_StudentList.xlsx");
                }
            }
        }
        
        public IActionResult ExportTemplate1()
        {
            //generate excel file
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("StudentList");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "FPT Email";
                worksheet.Cell(currentRow, 2).Value = "Profession";
                worksheet.Cell(currentRow, 3).Value = "Specialty";

                for (int i = 0; i < 10; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = "abcdeHE" + i + i + i + i + i + i + "@fpt.edu.vn";
                    worksheet.Cell(currentRow, 2).Value = "Information Technology";
                    worksheet.Cell(currentRow, 3).Value = "Information System";
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Template_StudentList.xlsx");
                }
            }
        }
        public IActionResult ExportTemplate2()
        {
            //generate excel file
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("CheckConditionList");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "FPT Email";
                worksheet.Cell(currentRow, 2).Value = "Status";

                for (int i = 0; i < 10; i++)
                {
                    if (i % 3 == 1)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = "abcdeHE" + i + i + i + i + i + i + "@fpt.edu.vn";
                        worksheet.Cell(currentRow, 2).Value = "not qualified";
                    }
                    else
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = "abcdeHE" + i + i + i + i + i + i + "@fpt.edu.vn";
                        worksheet.Cell(currentRow, 2).Value = "qualified";
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Template_CheckCondition.xlsx");
                }
            }
        }
        [HttpPost]
        public IActionResult ImportStudentList(IFormFile file)
        {
            int insertCount = 0;
            List<Student> studentAddedList = new List<Student>();
            try
            {
                var fileextension = Path.GetExtension(file.FileName);
                var filename = Guid.NewGuid().ToString() + fileextension;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", filename);
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    file.CopyTo(fs);
                }
                int rowNo = 1;
                XLWorkbook workbook = XLWorkbook.OpenFromTemplate(filepath);
                var sheets = workbook.Worksheets.First();
                var rows = sheets.Rows().ToList();
                foreach (var row in rows)
                {
                    //skip row 1
                    if (rowNo != 1)
                    {
                        //check if next row is empty
                        var test = row.Cell(1).Value.ToString();
                        if (string.IsNullOrWhiteSpace(test) || string.IsNullOrEmpty(test))
                        {
                            break;
                        }
                        string FptEmail = row.Cell(1).Value.ToString().Trim();
                        string professionName = row.Cell(2).Value.ToString();
                        string specialtyName = row.Cell(3).Value.ToString();
                        if (_studentService.GetStudentByStudentId(FptEmail) == null)
                        {
                            var newUser = new User()
                            {
                                UserID = FptEmail,
                                FptEmail = FptEmail,
                                FullName = "",
                                UserName = FptEmail.Substring(0, FptEmail.Length - 11),
                                Avatar = "",
                            };
                            int professionId = _professionService.GetProfessionByName(professionName, _semesterService.GetCurrentSemester().SemesterID).ProfessionID;
                            int specialtyId = _specialtyService.GetSpecialtyByName(specialtyName, _semesterService.GetCurrentSemester().SemesterID).SpecialtyID;
                            _userService.AddUser(newUser, 1);
                            _studentService.UpdateMajorOfStudentByUserId(FptEmail, professionId, specialtyId);
                            insertCount++;
                            studentAddedList.Add(new Student()
                            {
                                StudentID = FptEmail,
                                Profession = new Profession() { ProfessionFullName = professionName },
                                Specialty = new Specialty() { SpecialtyFullName = specialtyName }
                            });
                        }
                    }
                    else
                    {
                        rowNo = 2;
                    }
                }
                TempData["count"] = insertCount;
                ViewBag.studentAddedList = studentAddedList;
                return View("/Views/Staff_View/ManageGroup/ImportStudentList.cshtml"); ;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost]
        public IActionResult ImportCheckCondition(IFormFile file)
        {
            int deleteCount = 0;
            var attachedLink = "/MyGroup/Index";
            List<Student> studentRemovedList = new List<Student>();
            try
            {
                var fileextension = Path.GetExtension(file.FileName);
                var filename = Guid.NewGuid().ToString() + fileextension;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", filename);
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    file.CopyTo(fs);
                }
                int rowNo = 1;
                XLWorkbook workbook = XLWorkbook.OpenFromTemplate(filepath);
                var sheets = workbook.Worksheets.First();
                var rows = sheets.Rows().ToList();
                foreach (var row in rows)
                {
                    //skip row 1
                    if (rowNo != 1)
                    {
                        //check if next row is empty
                        var test = row.Cell(1).Value.ToString();
                        if (string.IsNullOrWhiteSpace(test) || string.IsNullOrEmpty(test))
                        {
                            break;
                        }
                        //handle student that not qualified
                        string qualified = row.Cell(2).Value.ToString().Trim();
                        if (qualified.Equals("not qualified"))
                        {
                            //delete student out of group
                            string userId = row.Cell(1).Value.ToString().Trim();
                            if (_studentService.GetStudentByStudentId(userId) != null)
                            {
                                Student student = _studentService.GetStudentByStudentId(userId);
                                int finalGroupId = student.FinalGroup.FinalGroupID;
                                //set leader to another member when delete leader
                                if (student.IsLeader == true)
                                {
                                    List<Student> memberList = _studentService.getListMemberByFinalGroupId(finalGroupId);
                                    if (memberList != null)
                                    {
                                        foreach (Student item in memberList)
                                        {
                                            _studentService.SetFinalGroupForStudent(item.FinalGroup.FinalGroupID, 1, item.StudentID, item.GroupName);
                                            _notificationService.InsertDataNotification(item.StudentID, "You have been promoted to team leader of group " + item.GroupName + " by Staff", attachedLink);
                                            break;
                                        }
                                    }
                                }
                                _notificationService.InsertDataNotification(student.StudentID, "You have been removed from your group because you are not eligible to do Capstone Project/ Thesis in this semester ", attachedLink);
                                //send mail
                                string receiver = student.StudentID + ",";
                                var subject = "You have been removed from your group";
                                var body = "<p>Hello, </p>";
                                body += "<p>You have been removed from your group by Staff in Capstone Project Registration System. </p>"
                                        + "<p>Please access Capstone Project Registration System to see more details.</p>";
                                _mailService.SendMailNotification(receiver, null, subject, body);
                                int count = _studentService.DeleteFinalGroupIdOfStudent(userId);

                                List<Student> memberList2 = _studentService.getListMemberByFinalGroupId(finalGroupId);
                                if (memberList2 != null)
                                {
                                    foreach (Student item in memberList2)
                                    {
                                        _notificationService.InsertDataNotification(item.StudentID, _userService.GetNameStudentByUserId(student.StudentID) + " have been removed from your group because they are not eligible to do Capstone Project/ Thesis in this semester", attachedLink);
                                        //send mail
                                        receiver = item.StudentID + ",";
                                        subject = $"{_userService.GetNameStudentByUserId(student.StudentID)} have been removed from your group";
                                        body = "<p>Hello, </p>";
                                        body += $"<p>{student.StudentID} have been removed from your group by Staff in Capstone Project Registration System. </p>"
                                                + "<p>Please access Capstone Project Registration System to see more details.</p>";
                                        _mailService.SendMailNotification(receiver, null, subject, body);
                                    }
                                }
                                if (count == 1)
                                {
                                    _finalGroupService.UpdateNumberOfMemberWhenRemove(finalGroupId);
                                    deleteCount++;
                                }
                                studentRemovedList.Add(student);
                            }
                        }
                    }
                    else
                    {
                        rowNo = 2;
                    }
                }
                TempData["count"] = deleteCount;
                ViewBag.studentRemovedList = studentRemovedList;
                return View("/Views/Staff_View/ManageGroup/ImportCheckCondition.cshtml");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
