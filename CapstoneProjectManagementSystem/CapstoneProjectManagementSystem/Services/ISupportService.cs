using CapstoneProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface ISupportService 
    {
        int AddSupportRequestThenReturnId(Support support);
        List<Support> GetAllPendingRequest();
        List<Support> GetAllProcessedRequest();
        int UpdateStatusToProcessed(int requestID);
    }
}
