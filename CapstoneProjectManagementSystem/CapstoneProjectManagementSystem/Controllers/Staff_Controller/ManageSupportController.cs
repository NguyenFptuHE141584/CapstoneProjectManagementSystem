using CapstoneProjectManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Controllers.Staff_Controller
{
    public class ManageSupportController : Controller
    {
        private readonly ISupportService _supportService;
        private readonly IStudentService _studentService;
        private readonly ISessionExtensionService _sessionExtensionService;

        public ManageSupportController(ISupportService supportService,
                                        IStudentService studentService,
                                        ISessionExtensionService sessionExtensionService)
        {
            _supportService = supportService;
            _studentService = studentService;
            _sessionExtensionService = sessionExtensionService;
        }
        public IActionResult Index()
        {
            ViewBag.pendingList = _supportService.GetAllPendingRequest();
            ViewBag.processedList = _supportService.GetAllProcessedRequest();
            return View(("/Views/Staff_View/ManageSupportRequest/Index.cshtml"));
        }
        public JsonResult ChangeStatusToProcessed([FromBody] int id)
        {
            return Json(_supportService.UpdateStatusToProcessed(id));
        }
    }
}
