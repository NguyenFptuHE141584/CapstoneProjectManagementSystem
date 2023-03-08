using Microsoft.AspNetCore.Mvc;

namespace CapstoneProjectManagementSystem.Controllers.Common_Controller
{
    public class Error404Controller : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Common_View/404NotFound.cshtml");
        }
    }
}
