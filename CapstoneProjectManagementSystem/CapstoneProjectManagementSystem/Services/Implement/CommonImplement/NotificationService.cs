using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
using CapstoneProjectManagementSystem.Services.CommonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement.CommonImplement
{
    public class NotificationService : INotificationService
    {
        public int CountNotificationNotRead(string userId)
        {
            return NotificationDao.CountNotificationNotRead(userId);
        }

        public int CountAllNotification(string userId)
        {
            return NotificationDao.CountAllNotification(userId);
        }

        public List<Notification> GetListNotificationNotReadByReceiverID(int numberOfRecord,string userId)
        {
            return NotificationDao.GetListNotificationNotReadByReceiverID(numberOfRecord,userId);
        }

        public List<Notification> GetListAllNotificationByUserId(int numberOfRecord, string userId)
        {
            return NotificationDao.GetListAllNotificationByUserId(numberOfRecord, userId);
        }

        public int InsertDataNotification(string userId, string notificationContent, string attachedLink)
        {
            return NotificationDao.InsertDataNotification(userId, notificationContent, attachedLink);
        }

        public string GetAttachedLinkByNotificationId(int notificationId)
        {
            return NotificationDao.GetAttachedLinkByNotificationId(notificationId);
        }

        public int UpdateReadedNotificationByNotificationId(int notificationId)
        {
            return NotificationDao.UpdateReadedNotificationByNotificationId(notificationId);
        }
    }
}
