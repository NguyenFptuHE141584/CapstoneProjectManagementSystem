using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProjectManagementSystem.Controllers.Staff_Controller
{
    [Authorize(Roles ="Staff")]
    public class LoginAsController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISessionExtensionService _sessionExtensionService;
        public LoginAsController(IUserService userService
                                ,ISessionExtensionService sessionExtensionService)
        {
            _userService = userService;
            _sessionExtensionService = sessionExtensionService;
        }

        public IActionResult Index()
        {
            return View("/Views/Staff_View/LoginAs/Index.cshtml");
        }

        [HttpPost]
        public JsonResult LoginAs([FromBody]string fptEmail)
        {
            try
            {
                if (fptEmail == null || fptEmail == "")
                {
                    return Json(0);
                }
                else
                {
                    var user =   _userService.GetUserByFptEmail(fptEmail);
                    if(user != null)
                    {
                        HttpContext.Session.Remove("sessionAccount");
                        _sessionExtensionService.SetObjectAsJson<User>(HttpContext.Session, "sessionAccount",user);
                        return Json(1);
                    }
                    else
                    {
                        return Json(2);
                    }
                }
            }
            catch (System.Exception)
            {
                return Json(0);
            }
        }
    }
}
