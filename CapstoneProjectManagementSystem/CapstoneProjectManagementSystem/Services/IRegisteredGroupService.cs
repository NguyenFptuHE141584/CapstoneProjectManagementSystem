using CapstoneProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface IRegisteredGroupService
    {
        int AddRegisteredGroup(RegisteredGroup registeredGroup);
        RegisteredGroup GetRegisteredGroupByGroupIdeaId(int groupIdeaId);
        List<RegisteredGroup> GetRegisteredGroupsBySearch(int semesterId, int status, string searchText, int offsetNumber, int fetchNumber);
        int CountRecordRegisteredGroupSearchList(int semesterId, int status, string searchText);
        int UpdateStatusByRegisteredGroupID(int registeredGroupID);
        RegisteredGroup GetDetailRegistrationOfStudentByGroupIdeaId(int registeredGroupId);
        int UpdateStaffCommentByRegisteredGroupID(string staffComment, int registeredGroupId);
        RegisteredGroup GetGroupIDByRegisteredGroupId(int registeredGroupId);
        int DeleteRecord(int id);
        int RejectWhenRegisteredGroupAccepted(int registeredGroupID, string commentReject, int groupIdeaId,int finalGroupId);
    }
}
