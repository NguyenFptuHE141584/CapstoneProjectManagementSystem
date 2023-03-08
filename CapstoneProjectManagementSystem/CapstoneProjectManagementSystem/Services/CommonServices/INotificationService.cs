using CapstoneProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.CommonServices
{
    public interface INotificationService
    {
        int CountNotificationNotRead(string userId);
        int CountAllNotification(string userId);
        List<Notification> GetListAllNotificationByUserId(int numberOfRecord, string userId);
        List<Notification> GetListNotificationNotReadByReceiverID(int numberOfRecord, string userId);
        int InsertDataNotification(string userId, string notificationContent, string attachedLink);

        string GetAttachedLinkByNotificationId(int notificationId);
        int UpdateReadedNotificationByNotificationId(int notificationId);
    }
}
