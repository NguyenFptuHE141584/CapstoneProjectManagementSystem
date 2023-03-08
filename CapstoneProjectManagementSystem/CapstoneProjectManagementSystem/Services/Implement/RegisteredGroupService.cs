using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class RegisteredGroupService : IRegisteredGroupService
    {
        public int AddRegisteredGroup(RegisteredGroup registeredGroup)
        {
            if (registeredGroup.RegisteredSupervisorName1 == null) registeredGroup.RegisteredSupervisorName1 = "";
            if (registeredGroup.RegisteredSupervisorPhone1 == null) registeredGroup.RegisteredSupervisorPhone1 = "";
            if (registeredGroup.RegisteredSupervisorEmail1 == null) registeredGroup.RegisteredSupervisorEmail1 = "";
            if (registeredGroup.RegisteredSupervisorName2 == null) registeredGroup.RegisteredSupervisorName2 = "";
            if (registeredGroup.RegisteredSupervisorPhone2 == null) registeredGroup.RegisteredSupervisorPhone2 = "";
            if (registeredGroup.RegisteredSupervisorEmail2 == null) registeredGroup.RegisteredSupervisorEmail2 = "";
            if (registeredGroup.StudentComment == null) registeredGroup.StudentComment = "";
            return RegisteredGroupDao.AddRegisteredGroup(registeredGroup);
        }

        public RegisteredGroup GetDetailRegistrationOfStudentByGroupIdeaId(int registeredGroupId)
        {
            return RegisteredGroupDao.GetDetailRegistrationOfStudentByGroupIdeaId(registeredGroupId);
        }

        public RegisteredGroup GetRegisteredGroupByGroupIdeaId(int groupIdeaId)
        {
            return RegisteredGroupDao.GetRegisteredGroupByGroupIdeaId(groupIdeaId);
        }

        public List<RegisteredGroup> GetRegisteredGroupsBySearch(int semesterId, int status, string searchText, int offsetNumber, int fetchNumber)
        {
            if (searchText == null)
            {
                searchText = "";
            }
            else
            {
                searchText = String.Concat("%", searchText.Trim().Replace(" ", "").ToUpper(), "%");
            }
            return RegisteredGroupDao.GetRegisteredGroupSearchList(semesterId,status,searchText,offsetNumber,fetchNumber);
        }

       public int CountRecordRegisteredGroupSearchList(int semesterId, int status, string searchText)
        {
            if (searchText == null)
            {
                searchText = "";
            }
            else
            {
                searchText = String.Concat("%", searchText.Trim().Replace(" ", "").ToUpper(), "%");
            }
            return RegisteredGroupDao.CountRecordRegisteredGroupSearchList(semesterId, status, searchText);
        }

        public int UpdateStaffCommentByRegisteredGroupID(string staffComment, int registeredGroupId)
        {
            return RegisteredGroupDao.UpdateStaffCommentByRegisteredGroupID(staffComment, registeredGroupId);
        }

        public int UpdateStatusByRegisteredGroupID(int registeredGroupID)
        {
            return RegisteredGroupDao.UpdateStatusByRegisteredGroupID(registeredGroupID);
        }

        public RegisteredGroup GetGroupIDByRegisteredGroupId(int registeredGroupId)
        {
            return RegisteredGroupDao.GetGroupIDByRegisteredGroupId(registeredGroupId);
        }
        public int DeleteRecord(int id)
        {
            return RegisteredGroupDao.DeleteRecord(id);
        }

        public int RejectWhenRegisteredGroupAccepted(int registeredGroupID, string commentReject, int groupIdeaId, int finalGroupId)
        {
            return RegisteredGroupDao.RejectWhenRegisteredGroupAccepted(registeredGroupID, commentReject, groupIdeaId,finalGroupId);
        }
    }
}
