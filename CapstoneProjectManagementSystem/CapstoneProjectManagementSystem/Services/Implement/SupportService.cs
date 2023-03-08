using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class SupportService : ISupportService
    {
        public int AddSupportRequestThenReturnId(Support support)
        {
            return SupportDao.AddSupportRequestThenReturnId(support.Title.Trim(), support.SupportMessge.Trim(), support.Student.StudentID, support.PhoneNumber);
        }
        public List<Support> GetAllPendingRequest()
        {
            return SupportDao.GetAllPendingRequest();
        }
        public List<Support> GetAllProcessedRequest()
        {
            return SupportDao.GetAllProcessedRequest();
        }
        public int UpdateStatusToProcessed(int requestID)
        {
            return SupportDao.UpdateStatusToProcessed(requestID);
        }
    }
}
