using Microsoft.AspNetCore.Mvc;

namespace CapstoneProjectManagementSystem.Controllers.Staff_Controller
{
    public class StaffUserGuideController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Staff_View/StaffUserGuide/Index.cshtml");
        }
    }
}
