using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Services;
using CapstoneProjectManagementSystem.Services.CustomHandler;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Controllers.Student_Controller
{
    [Authorize(Roles = "Student")]
    [ServiceFilter(typeof(SemesterFilter))]
    public class StudentProfileController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ISessionExtensionService _sessionExtensionService;
        private readonly IAffiliateAccountService _affiliateAccountService;
        private readonly IMailService _mailService;
        private readonly IProfessionService _professionService;
        private readonly ISpecialtyService _specialtyService;
        private readonly ISemesterService _semesterService;
        public StudentProfileController(IStudentService studentService
                                    , ISessionExtensionService sessionExtensionService
                                    , IAffiliateAccountService affiliateAccountService
                                    , IMailService mailService
                                    , IProfessionService professionService
                                    , ISpecialtyService specialtyService
                                    , ISemesterService semesterService)
        {
            _studentService = studentService;
            _sessionExtensionService = sessionExtensionService;
            _affiliateAccountService = affiliateAccountService;
            _mailService = mailService;
            _professionService = professionService;
            _specialtyService = specialtyService;
            _semesterService = semesterService;
        }

        public async Task<IActionResult> Index(string studentId)
        {
            var myUser = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            var currentSemester = _semesterService.GetCurrentSemester();
            // my profile
            if (myUser.UserID == studentId || studentId == null)
            {
                try
                {
                    if (_studentService.GetProfileOfStudentByUserId(myUser.UserID) == null)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        ViewBag.studentProfile = _studentService.GetProfileOfStudentByUserId(myUser.UserID);
                        ViewBag.affiliateAccount = _affiliateAccountService.GetAffiliateAccountById(myUser.UserID);
                        List<Profession> professions = new List<Profession>();
                        professions.Add(new Profession()
                        {
                            ProfessionID = 0,
                            ProfessionFullName = "Professional",
                        });
                        foreach (var item in _professionService.getAllProfession(currentSemester.SemesterID))
                        {
                            professions.Add(item);
                        }
                        ViewBag.profession = professions;
                        TempData["ProfessionIdOfStudent"] = _studentService.GetProfessionIdOfStudentByUserId(myUser.UserID);
                        return View("/Views/Student_View/StudentProfile/YourProfile.cshtml");
                    }
                }
                catch (Exception)
                {
                    HttpContext.Session.Remove("sessionAccount");
                    await HttpContext.SignOutAsync();
                    return RedirectToAction("SignIn", "User");
                }
            }
            // profile of another student 
            else
            {
                try
                {
                    if (_studentService.GetProfileOfStudentByUserId(studentId) == null)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        ViewBag.studentProfile = _studentService.GetProfileOfStudentByUserId(studentId);
                        ViewBag.professionAndSpecialty = _studentService.GetProfessionAndSpecialtyByStudentId(studentId);
                        TempData["ProfessionIdOfStudent"] = _studentService.GetProfessionIdOfStudentByUserId(studentId);
                        return View("/Views/Student_View/StudentProfile/ProfileOfStudent.cshtml");
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Error404", "Index");
                }
            }
        }

        [HttpPost]
        // get list specialty when choose profession
        public JsonResult GetSpecialtyByProfessionId(int professionId)
        {
            return Json(_specialtyService.getSpecialtiesByProfessionId(professionId, _semesterService.GetCurrentSemester().SemesterID));
        }


        //get specialty of student
        public JsonResult GetSpecialtyIdOfStudent()
        {
            var userId = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount").UserID;
            var result = _studentService.GetSpecialtyIdOfStudentByUserId(userId);
            return Json(result);
        }



        [HttpPost]
        // update profile when student edit
        public JsonResult EditMyProfile([FromBody] Student student)
        {
            try
            {
                var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
                student.User.UserID = user.UserID;
                student.StudentID = user.UserID;
                var regexPhoneNumber = "^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$";
                var match = Regex.Match(student.PhoneNumber, regexPhoneNumber);
                if (!match.Success || student.PhoneNumber == "" || student.PhoneNumber == null
                    || student.Profession.ProfessionID == 0 || student.Specialty.SpecialtyID == 0)
                {
                    return Json(0);
                }
                else
                {
                    _studentService.UpdateProfileOfStudent(student);
                    return Json(1);
                }
            }
            catch (Exception)
            {
                return Json(0);
            }
        }


        // get profile of student , after ajax call this function to check field phoneNumber , professionId, specialtyId
        public JsonResult CheckProfile()
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            var currentSemester = _semesterService.GetCurrentSemester();
            var studentProfile = _studentService.GetProfileOfStudentByUserId(user.UserID);
            if (studentProfile.PhoneNumber == null || studentProfile.PhoneNumber == ""
                || studentProfile.Specialty.SpecialtyID == 0 || studentProfile.Profession.ProfessionID == 0
                || _specialtyService.getSpecialtyById(studentProfile.Specialty.SpecialtyID).Semester.SemesterID != currentSemester.SemesterID
                || _professionService.getProfessionById(studentProfile.Profession.ProfessionID).Semester.SemesterID != currentSemester.SemesterID)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }


        [HttpPost]
        // add personal email of student
        public JsonResult AddOtp([FromBody] string email)
        {
            try
            {
                if (!_affiliateAccountService.CheckPersonalEmailExist(email))
                {
                    return Json(2);
                }
                else
                {
                    var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
                    var otp = _mailService.GenerateOTP();
                    if (_affiliateAccountService.GetAffiliateAccountById(user.UserID) == null)
                    {
                        if (_affiliateAccountService.AddOTP(user.UserID, otp) >= 1)
                        {
                            _mailService.SendEmailAsync(email, "Verify your Email", otp); // send email otp to verify
                            return Json(1); // success
                        }
                        else
                        {
                            return Json(0);// somthing with wrong
                        }
                    }
                    else
                    {
                        if(_affiliateAccountService.UpdateOTP(user.UserID, otp) >= 1)
                        {
                            _mailService.SendEmailAsync(email, "Verify your Email", otp); // send email otp to verify
                            return Json(1); // success
                        }
                        else
                        {
                            return Json(0);// somthing with wrong
                        }
                    }

                }
            }
            catch (Exception)
            {
                return Json(0); // somthing with wrong
            }
        }

        [HttpPost]
        public JsonResult SendOtpVerify([FromBody] string email)
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            var otp = _mailService.GenerateOTP();
            var result = _affiliateAccountService.UpdateOTP(user.UserID, otp);
            var afffiliateAccount = _affiliateAccountService.GetAffiliateAccountById(user.UserID);
            if (result >= 1)
            {
                if (email != null)
                {
                    _mailService.SendEmailAsync(email, "Verify your Email", afffiliateAccount.OneTimePassword);
                    return Json(true);
                }
                else
                {
                    _mailService.SendEmailAsync(afffiliateAccount.PersonalEmail, "Verify your Email", afffiliateAccount.OneTimePassword);
                    return Json(true);
                }
            }
            else
            {
                return Json(false);
            }
        }

        [HttpPost]
        // verify email with otp 
        public JsonResult VerifyEmailByOTP(string otp, string personalEmail)
        {
            try
            {
                if (otp == null || otp == "")
                {
                    return Json(3);
                }
                else
                {
                    var regexOTP = @"^[0-9]{6,6}$";
                    var match = Regex.Match(otp, regexOTP);
                    if (match.Success)
                    {
                        var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
                        var affiliateAccount = _affiliateAccountService.GetAffiliateAccountById(user.UserID);
                        if (affiliateAccount.OneTimePassword.Equals(otp)
                           && (affiliateAccount.OtpRequestTime.AddMinutes(5) > DateTime.Now))
                        {
                            _affiliateAccountService.UpdateIsVerifyEmail(affiliateAccount.AffiliateAccount_ID, personalEmail);
                            return Json(1); // correct otp 
                        }
                        else if (!affiliateAccount.OneTimePassword.Equals(otp))
                        {
                            return Json(0);// incorrect OTP
                        }
                        else if (affiliateAccount.OneTimePassword.Equals(otp)
                            && !(affiliateAccount.OtpRequestTime.AddMinutes(5) > DateTime.Now))
                        {
                            return Json(2); // expired otp
                        }
                        else
                        {
                            return Json(3); //something wrong
                        }
                    }
                    else
                    {
                        return Json(4); //  otp must have 6 digits
                    }
                }
            }
            catch (Exception)
            {
                return Json(3);
            }
        }

        [HttpPost]
        //set password for email
        public JsonResult SetPasswordFofAccount(string password, string confirmPassword)
        {
            try
            {
                if (password == null || password == "" || confirmPassword == null || confirmPassword == "")
                {
                    return Json(2); // new password or confirm new password not empty
                }
                else
                {
                    var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
                    if (password.Equals(confirmPassword))
                    {
                        if (_affiliateAccountService.UpdatePasswordHash(user.UserID, password) == 1)
                        {
                            return Json(1); // set password success
                        }
                        else
                        {
                            return Json(0);// somthing with wrong
                        }
                    }
                    else
                    {
                        return Json(3);// new password and confirm new password is not match
                    }
                }
            }
            catch (Exception)
            {
                return Json(0); // somthing with wrong
            }
        }

        [HttpPost]
        public JsonResult CheckInputOldPassword(string personalEmail, string oldPassword)
        {
            try
            {
                if (oldPassword == null || oldPassword == "")
                {
                    return Json(0);
                }
                else
                {
                    return Json(_affiliateAccountService.CheckAffiliateAccountAndPasswordHash(personalEmail, oldPassword));
                }
            }
            catch (Exception)
            {
                return Json(0);
            }
        }

        [HttpPost]
        public JsonResult ChangePassword(string newPassword, string confirmNewPassword)
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            if (newPassword == confirmNewPassword)
            {
                _affiliateAccountService.UpdatePasswordHash(user.UserID, newPassword);
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
    }
}
