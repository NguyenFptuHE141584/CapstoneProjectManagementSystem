using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Services;
using CapstoneProjectManagementSystem.Services.CommonServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Controllers.Common_Controller
{
    public class NotificationController : Controller
    {
        private readonly ISessionExtensionService _sessionExtensionService;
        private readonly INotificationService _notificationService;

        public NotificationController(ISessionExtensionService sessionExtensionService
                                      ,INotificationService notificationService)
        {
            _sessionExtensionService = sessionExtensionService;
            _notificationService = notificationService;
        }

        public JsonResult CountNotificationNotReadOfUser()
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            return Json(_notificationService.CountNotificationNotRead(user.UserID));
        }

        public JsonResult CountAllNotification()
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            return Json(_notificationService.CountAllNotification(user.UserID));
        }

        public JsonResult GetListNotificationNotReadByReceiverID(int numberOfRecord)
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            var x=  _notificationService.GetListNotificationNotReadByReceiverID(numberOfRecord, user.UserID);
            return Json(x);
        }
        public JsonResult GetListAllNotificationByUserId (int numberOfRecord)
        {
            var user = _sessionExtensionService.GetObjectFromJson<User>(HttpContext.Session, "sessionAccount");
            var x=  _notificationService.GetListAllNotificationByUserId(numberOfRecord, user.UserID);
            return Json(x);
        }

        public JsonResult ReadedNotification(int notificationId)
        {
            _notificationService.UpdateReadedNotificationByNotificationId(notificationId);
            var attachedLink = _notificationService.GetAttachedLinkByNotificationId(notificationId);
            return Json(attachedLink) ;
        }


    }
}
