using CapstoneProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface IFinalGroupService
    {
        //NguyenLH
        List<FinalGroup> getAllFinalGroups(int semester_Id);
        FinalGroup getFinalGroupById(int id);
        List<FinalGroup> GetLackOfMemberFinalGroupSearchList(int semester_Id, int profession_Id, int specialty_Id, string searchText, int offsetNumber, int fetchNumber);
        List<FinalGroup> GetFullMemberFinalGroupSearchList(int semester_Id, int profession_Id, int specialty_Id, string searchText, int offsetNumber, int fetchNumber);
        int UpdateNumberOfMemberWhenAdd(int groupId);
        int UpdateNumberOfMemberWhenRemove(int groupId);
        int UpdateGroupName(int groupId, string groupName);
        int DeleteFinalGroup(int finalGroupId);
        int CreateFinalGroup(int semesterId, int professionId, int specilatyId,string groupName, string englishName, string abbreviation, string vietnameseName, int maxMember, int numberOfMember);

        //NguyenNH
        int AddRegisteredGroupToFinalGroup(GroupIdea groupIdea, string groupName);
        FinalGroup GetFinalGroupByStudentIsLeader(string studentId, int semesterId);
        FinalGroup GetOldTopicByGroupName(int finalGroupId);
        List<Student> GetListCurrentMemberHaveFinalGroupByGroupName(string groupName, int semesterId);
        int GetMaxMemberOfFinalGroupByGroupName(string groupName, int semesterId);
        int UpdateNewTopicForFinalGroup(ChangeTopicRequest changeTopicRequest);
        string GetLatestGroupName(string codeOfGroupName);
    }
}

