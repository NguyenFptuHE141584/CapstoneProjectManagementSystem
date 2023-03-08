using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
using CapstoneProjectManagementSystem.Services;
using CapstoneProjectManagementSystem.Services.CustomHandler;
using CapstoneProjectManagementSystem.Services.StaffServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nancy.Json;

namespace CapstoneProjectManagementSystem.Controllers.Common_Controller
{


    public class ExternalSignInController : Controller
    {
        private readonly IUserService _userService;

        private readonly ISessionExtensionService _sessionExtensionService;
        private readonly IStudentService _studentService;
        private readonly IStudent_GroupIdeaService _student_GroupIdeaService;
        private readonly IStaffService _staffService;
        private readonly ISemesterService _semesterService;
        private readonly IFinalGroupService _finalGroupService;
        public ExternalSignInController(IUserService userService, ISessionExtensionService sessionExtensionService
                                        , IStudentService studentService
                                        , IStudent_GroupIdeaService student_GroupIdeaService
                                        , IStaffService staffService
                                        , ISemesterService semesterService
                                        , IFinalGroupService finalGroupService)
        {
            _userService = userService;
            _sessionExtensionService = sessionExtensionService;
            _studentService = studentService;
            _student_GroupIdeaService = student_GroupIdeaService;
            _staffService = staffService;
            _semesterService = semesterService;
            _finalGroupService = finalGroupService;
        }

        [TempData]
        public string ErrorMessage { get; set; }
        public IActionResult OnPost(string returnUrl)
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("SignInCallBack") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SignInCallBack(string returnUrl)
        {
            try
            {
                var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                if (authenticateResult.Principal == null)
                {
                    await HttpContext.SignOutAsync();
                    ErrorMessage = "Error loading external login information";
                    return RedirectToAction("SignIn", "User", new { message = ErrorMessage });
                }
                if (authenticateResult.Principal.Identity.IsAuthenticated)
                {
                    var loginInfo = new User()
                    {
                        UserID = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email),
                        FptEmail = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email),
                        FullName = authenticateResult.Principal.FindFirstValue(ClaimTypes.Name),
                        UserName = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email).Substring(0, authenticateResult.Principal.FindFirstValue(ClaimTypes.Email).Length - 11),
                        Avatar = authenticateResult.Principal.FindFirstValue("image"),
                    }
;
                    if (_userService.GetUserByID(loginInfo.UserID) != null)
                    {
                        if (_userService.GetUserByID(loginInfo.UserID).Role.Role_ID == 3) //staff
                        {
                            var role = new Role()
                            {
                                Role_ID = 3
                            };
                            loginInfo.Role = role; // role staff
                            _sessionExtensionService.SetObjectAsJson<User>(HttpContext.Session, "sessionAccount", loginInfo);
                            return RedirectToAction("Index", "SemesterManage");
                        }
                    }
                    if (Regex.IsMatch(loginInfo.FptEmail, "[A-Za-z][0-9]{5,}@fpt.edu.vn"))
                    {
                        var role = new Role()
                        {
                            Role_ID = 1
                        };
                        loginInfo.Role = role; // role student
                        var user = _userService.GetUserByID(loginInfo.UserID);
                        _sessionExtensionService.SetObjectAsJson<User>(HttpContext.Session, "sessionAccount", loginInfo);
                        if (user == null)
                        {
                            try
                            {
                                _userService.AddUser(loginInfo, 1);
                                return RedirectToAction("Index", "StudentHome");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                await HttpContext.SignOutAsync();
                                return RedirectToAction("SignIn", "User");
                            }
                        }
                        else
                        {
                            _userService.UpdateAvatar(loginInfo.Avatar, loginInfo.UserID);
                            _studentService.UpdateSemesterOfStudentByUserId(loginInfo.UserID);
                            if (_semesterService.GetCurrentSemester() == null)
                            {
                                return RedirectToAction("CloseSemester", "Semester");
                            }
                            if (_student_GroupIdeaService.FilterStudentHaveIdea(loginInfo.UserID, _semesterService.GetCurrentSemester().SemesterID)
                                || !_studentService.GetStudentByStudentId(loginInfo.UserID).FinalGroup.FinalGroupID.ToString().Equals("0"))
                            {
                                var groupId = _student_GroupIdeaService.GetGroupIdByStudentId(loginInfo.UserID);
                                return RedirectToAction("Index", "MyGroup");
                            }
                            else
                            {
                                return RedirectToAction("Index", "StudentHome");
                            }
                        }
                    }
                    else
                    {
                        ErrorMessage = "Sorry, you do not have permission to access the system";
                        return RedirectToAction("SignIn", "User", new { message = ErrorMessage });
                    }

                }
                else
                {
                    HttpContext.Session.Remove("sessionAccount");
                    await HttpContext.SignOutAsync();
                    ErrorMessage = "This account is not authenticated";
                    return RedirectToAction("SignIn", "User", new { message = ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.Remove("sessionAccount");
                await HttpContext.SignOutAsync();
                ErrorMessage = ex.Message;
                return RedirectToAction("SignIn", "User", new
                {
                    message = ErrorMessage
                });
            }
        }
    }
}
