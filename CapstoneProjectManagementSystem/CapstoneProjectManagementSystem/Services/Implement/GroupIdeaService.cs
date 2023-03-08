using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class GroupIdeaService : IGroupIdeaService
    {
        public List<GroupIdea> GetGroupIdeaSearchList(int semester_Id, int profession_Id, int specialty_Id, string searchText, int offsetNumber, int fetchNumber)
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
                searchText = String.Concat("%",searchText.Trim().Replace(" ","").ToUpper(),"%");
            }
            return GroupIdeaDao.GetGroupIdeaSearchList(semester_Id.ToString(), profession_Id_ToString, specialty_Id_ToString, searchText, offsetNumber, fetchNumber);
        }
        public List<GroupIdea> GetGroupIdeaSearchList_2(int semester_Id, int profession_Id, int specialty_Id, string searchText,string studentId, int offsetNumber, int fetchNumber)
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
            return GroupIdeaDao.GetGroupIdeaSearchList_2(semester_Id.ToString(), profession_Id_ToString, specialty_Id_ToString, searchText,studentId, offsetNumber, fetchNumber);
        }
        public int getNumberOfResultWhenSearch(int semester_Id, int profession_Id, int specialty_Id, string searchText)
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
            return GroupIdeaDao.getNumberOfResultWhenSearch(semester_Id.ToString(), profession_Id_ToString, specialty_Id_ToString, searchText);
        }
        public int getNumberOfResultWhenSearch_2(int semester_Id, int profession_Id, int specialty_Id, string searchText,string studentId)
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
            return GroupIdeaDao.getNumberOfResultWhenSearch_2(semester_Id.ToString(), profession_Id_ToString, specialty_Id_ToString, searchText,studentId);
        }
        public GroupIdea GetGroupIdeaById(int id)
        {
            return GroupIdeaDao.GetGroupIdeaById(id);
        }
        public List<string> ConvertProjectTags(string projectTags)
        {
            string[] projectTagsArray = projectTags.Trim().Split(',');
            List<string> projectTagList = new List<string>();
            foreach (var tag in projectTagsArray)
            {
                projectTagList.Add(tag);
            }
            return projectTagList;
        }
        public int UpdateNumberOfMemberWhenAdd(int groupIdeaId)
        {
            return GroupIdeaDao.UpdateNumberOfMemberWhenAdd(groupIdeaId);
        }
        public int UpdateNumberOfMemberWhenRemove(int groupIdeaId)
        {
            return GroupIdeaDao.UpdateNumberOfMemberWhenRemove(groupIdeaId);
        }
        public int DeleteGroupIdea(int groupIdeaId)
        {
            return GroupIdeaDao.DeleteGroupIdea(groupIdeaId);
        }
        public int UpdateIdea(GroupIdea groupIdea, int semesterId)
        {
            return GroupIdeaDao.UpdateIdea(groupIdea, semesterId);
        }
    }
}
