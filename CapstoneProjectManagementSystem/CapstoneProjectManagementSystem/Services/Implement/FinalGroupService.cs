using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class FinalGroupService : IFinalGroupService
    {
        //NguyenLH
        public List<FinalGroup> getAllFinalGroups(int semester_Id)
        {
            return FinalGroupDao.getAllFinalGroups(semester_Id);
        }
        public FinalGroup getFinalGroupById(int id)
        {
            return FinalGroupDao.getFinalGroupById(id);
        }
        public List<FinalGroup> GetLackOfMemberFinalGroupSearchList(int semester_Id, int profession_Id, int specialty_Id, string searchText, int offsetNumber, int fetchNumber)
        {
            string profession_Id_ToString = profession_Id.ToString();
            string specialty_Id_ToString = specialty_Id.ToString();
            if (profession_Id_ToString.Equals("0"))
            {
                profession_Id_ToString = "";
            }
            if (specialty_Id_ToString.Equals("0"))
            {
                specialty_Id_ToString = "";
            }
            if (searchText == null)
            {
                searchText = "";
            }
            else
            {
                searchText = String.Concat("%", searchText.Trim().Replace(" ", "").ToUpper(), "%");
            }
            return FinalGroupDao.GetLackOfMemberFinalGroupSearchList(semester_Id.ToString(), profession_Id_ToString, specialty_Id_ToString, searchText, offsetNumber, fetchNumber);
        }
        public List<FinalGroup> GetFullMemberFinalGroupSearchList(int semester_Id, int profession_Id, int specialty_Id, string searchText, int offsetNumber, int fetchNumber)
        {
            string profession_Id_ToString = profession_Id.ToString();
            string specialty_Id_ToString = specialty_Id.ToString();
            if (profession_Id_ToString.Equals("0"))
            {
                profession_Id_ToString = "";
            }
            if (specialty_Id_ToString.Equals("0"))
            {
                specialty_Id_ToString = "";
            }
            if (searchText == null)
            {
                searchText = "";
            }
            else
            {
                searchText = String.Concat("%", searchText.Trim().Replace(" ", "").ToUpper(), "%");
            }
            return FinalGroupDao.GetFullMemberFinalGroupSearchList(semester_Id.ToString(), profession_Id_ToString, specialty_Id_ToString, searchText, offsetNumber, fetchNumber);
        }
        public int UpdateNumberOfMemberWhenAdd(int groupId)
        {
            return FinalGroupDao.UpdateNumberOfMemberWhenAdd(groupId);
        }
        public int UpdateNumberOfMemberWhenRemove(int groupId)
        {
            return FinalGroupDao.UpdateNumberOfMemberWhenRemove(groupId);
        }
        public int UpdateGroupName(int groupId, string groupName)
        {
            return FinalGroupDao.UpdateGroupName(groupId, groupName);
        }
        public int DeleteFinalGroup(int finalGroupId)
        {
            return FinalGroupDao.DeleteFinalGroup(finalGroupId);
        }
        public int CreateFinalGroup(int semesterId, int professionId, int specilatyId,string groupName, string englishName, string abbreviation, string vietnameseName, int maxMember, int numberOfMember)
        {
            return FinalGroupDao.CreateFinalGroup(semesterId, professionId, specilatyId, groupName, englishName, abbreviation, vietnameseName, maxMember, numberOfMember);
        }
        //NguyenNH
        public int AddRegisteredGroupToFinalGroup(GroupIdea groupIdea, string groupName)
        {
            return FinalGroupDao.AddRegisteredGroupToFinalGroup(groupIdea,groupName);
        }

        public FinalGroup GetFinalGroupByStudentIsLeader(string studentId, int semesterId)
        {
            return FinalGroupDao.GetFinalGroupByStudentIsLeader(studentId,semesterId);
        }
        public FinalGroup GetOldTopicByGroupName(int finalGroupId)
        {
            return FinalGroupDao.GetOldTopicByFinalGroupId(finalGroupId);
        }

        public List<Student> GetListCurrentMemberHaveFinalGroupByGroupName(string groupName, int semesterId)
        {
            return FinalGroupDao.GetListCurrentMemberHaveFinalGroupByGroupName(groupName,semesterId);
        }
        public int GetMaxMemberOfFinalGroupByGroupName(string groupName, int semesterId)
        {
            return FinalGroupDao.GetMaxMemberOfFinalGroupByGroupName(groupName, semesterId);
        }
        public int UpdateNewTopicForFinalGroup(ChangeTopicRequest changeTopicRequest)
        {
            return FinalGroupDao.UpdateNewTopicForFinalGroup(changeTopicRequest);
        }

        public string GetLatestGroupName(string codeOfGroupName)
        {
            if (codeOfGroupName == null)
            {
                codeOfGroupName = "";
            }
            else
            {
                codeOfGroupName = string.Concat("%", codeOfGroupName.Trim().Replace(" ", "").ToUpper(), "%");
            }
            return FinalGroupDao.GetLatestGroupName(codeOfGroupName); 
        }
    }
}
