using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
using CapstoneProjectManagementSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using CapstoneProjectManagementSystem.Services.CustomHandler;

namespace CapstoneProjectManagementSystem.Controllers.Common_Controller
{
    public class UserController : Controller
    {
        private readonly IAffiliateAccountService _affiliateAccountService;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;
        private readonly ISessionExtensionService _sessionExtensionService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStudentService _studentService;

        [TempData]
        public string ErrorMessage { get; set; }    //ErrorMessage is used to report the error and push to the client by tempdata
        public string SuccessMessaage { get; set; } //SuccessMessaage is used to report the success and push to the client by tempdata
        public UserController(IAffiliateAccountService affiliateAccountService, IPasswordHasherService passwordHasherService
            , IMailService mailService, IUserService userService, ISessionExtensionService sessionExtensionService
            , IHttpContextAccessor httpContextAccessor
            ,IStudentService studentService)
            
        {
            _affiliateAccountService = affiliateAccountService;
            _passwordHasherService = passwordHasherService;
            _mailService = mailService;
            _userService = userService;
            _sessionExtensionService = sessionExtensionService;
            _httpContextAccessor = httpContextAccessor;
            _studentService = studentService;
        }

        public IActionResult SignIn() // view page signin 
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            if (user == null)
            {
                return View("/Views/Common_View/SignIn.cshtml");
            }
            else
            {
                if (user.Role.Role_ID == 1)
                {
                    return RedirectToAction("Index", "StudentHome");
                }
                else
                {
                    return RedirectToAction("Index", "SemesterManage");
                }
            }
        }

        [HttpGet]
        public IActionResult SignInByAffiliateAccount() // view sign in with account registered in profile
        {

            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            if (user == null)
            {
                return View("/Views/Common_View/SignInByAffiliateAccount.cshtml");
            }
            else
            {
                if (user.Role.Role_ID == 1)
                {
                    return RedirectToAction("Index", "StudentHome");
                }
                else
                {
                    return RedirectToAction("Index", "SemesterManage");
                }
            }
        }


        [HttpPost]
        public async Task<IActionResult> SignInByAffiliateAccount(string personalEmail, string passwordHash)
        {

            //get infor affiliate account by function GetBackupAccountByEmail with parameter is personalEmail
            var affiliateAccount = _affiliateAccountService.GetAffiliateAccountByEmail(personalEmail);
            // check login with affiliate accoutn and password hash 
            var checkUserLogin = _affiliateAccountService.CheckAffiliateAccountAndPasswordHash(personalEmail, passwordHash);
            //if it return true -> login sucssess and set to session redirect homepage of student 
            if (checkUserLogin)
            {
                var userLogin = _userService.GetUserByID(affiliateAccount.AffiliateAccount_ID);
                _sessionExtensionService.SetObjectAsJson<User>(HttpContext.Session, "sessionAccount", userLogin);
                _studentService.UpdateSemesterOfStudentByUserId(userLogin.UserID);
                var claims = new List<Claim>() {
                        new Claim(ClaimTypes.NameIdentifier, userLogin.UserID),
                        new Claim(ClaimTypes.Email, userLogin.FptEmail),
                        new Claim("UserName",userLogin.UserName),
                    };

                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);

                await HttpContext.SignInAsync(claimPrincipal);
                return RedirectToAction("Index", "StudentHome");
            }
            // if it return false -> login error will redirect page SignInByAffiliateAccount with notify error message
            else
            {
                ErrorMessage = "Email or Password invalid";
                TempData["oldPersonalEmail"] = personalEmail;
                return RedirectToAction("SignInByAffiliateAccount", "User", new { message = ErrorMessage });
            }
        }

        public IActionResult Forbidden()
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            if (user == null)
            {
                return View("/Views/Common_View/Forbidden.cshtml");
            }
            else
            {
                if (user.Role.Role_ID == 1)
                {
                    return RedirectToAction("Index", "StudentHome");
                }
                else
                {
                    return RedirectToAction("Index", "SemesterManage");
                }
            }
        }


        public IActionResult ForgotPassword()
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            if (user == null)
            {
                return View("/Views/Common_View/ForgotPassword.cshtml");
            }
            else
            {
                if (user.Role.Role_ID == 1)
                {
                    return RedirectToAction("Index", "StudentHome");
                }
                else
                {
                    return RedirectToAction("Index", "SemesterManage");
                }
            }
        }

        [HttpPost]
        public IActionResult VerifyCode(string email)
        {
            //check if the email is linked or not
            AffiliateAccount AffiliateAccount;
            AffiliateAccount = _affiliateAccountService.GetAffiliateAccountByEmail(email);
            if (AffiliateAccount != null)
            {
                //Send OTP
                string OTP = _mailService.GenerateOTP();
                try
                {
                    _mailService.SendEmailAsync(email, "Verify your Email", OTP);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" Send OTP Mail Exception Type: {0}", ex.GetType());
                    Console.WriteLine(" Message: {0}", ex.Message);
                }
                //Save OTP to DB
                _affiliateAccountService.UpdateOTP(AffiliateAccount.AffiliateAccount_ID, OTP);
                TempData["backupAccount_Id"] = AffiliateAccount.AffiliateAccount_ID;
                return View("/Views/Common_View/VerifyOTP.cshtml");
            }
            else
            {
                TempData["ErrorMessage"] = "Email does not exist. ";
                return View("/Views/Common_View/ForgotPassword.cshtml");
            }
        }


        public IActionResult ResetPassword(string OTP, string backupAccount_Id)
        {
            TempData["backupAccount_Id"] = backupAccount_Id;
            AffiliateAccount affiliateAccount = _affiliateAccountService.GetAffiliateAccountById(backupAccount_Id);
            if (affiliateAccount.OneTimePassword.Equals(OTP)
                && (affiliateAccount.OtpRequestTime.AddMinutes(5) > DateTime.Now))
            {
                return View("/Views/Common_View/ResetPassword.cshtml");
            }
            // incorrect OTP
            else if (!affiliateAccount.OneTimePassword.Equals(OTP))
            {
                TempData["ErrorMessage"] = "OTP incorrect. ";
                return View("/Views/Common_View/VerifyOTP.cshtml");
            }
            // OTP has expired
            else if (affiliateAccount.OneTimePassword.Equals(OTP)
                && !(affiliateAccount.OtpRequestTime.AddMinutes(5) > DateTime.Now))
            {
                TempData["ErrorMessage"] = "OTP has expired. ";
                return View("/Views/Common_View/VerifyOTP.cshtml");
            }
            else
            {
                TempData["ErrorMessage"] = "Something wrong. Please try again! ";
                return View("/Views/Common_View/VerifyOTP.cshtml");
            }
        }
        public IActionResult ReSignin(string backupAccount_Id, string newPassword, string confirmNewPassword)
        {
            if (newPassword.Equals(confirmNewPassword))
            {
                TempData["SuccessMessage"] = "Reset Password successfully. ";
                _affiliateAccountService.UpdatePasswordHash(backupAccount_Id, newPassword);
                return View("/Views/Common_View/SigninByAffiliateAccount.cshtml");
            }
            else
            {
                TempData["backupAccount_Id"] = backupAccount_Id;
                TempData["ErrorMessage"] = "Passwords don’t match.";
                return View("/Views/Common_View/ResetPassword.cshtml");
            }
        }

        public async Task<IActionResult> SignOut()
        {
            HttpContext.Session.Remove("sessionAccount");
            await HttpContext.SignOutAsync();
            return Redirect("~/");
        }
    }
}
