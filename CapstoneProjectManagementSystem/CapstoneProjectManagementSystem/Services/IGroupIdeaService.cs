using CapstoneProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface IGroupIdeaService
    {
        List<GroupIdea> GetGroupIdeaSearchList(int semester_Id, int profession_Id, int specialty_Id, string searchText, int offsetNumber, int fetchNumber);
        List<GroupIdea> GetGroupIdeaSearchList_2(int semester_Id, int profession_Id, int specialty_Id, string searchText,string studentId, int offsetNumber, int fetchNumber);
        int getNumberOfResultWhenSearch(int semester_Id, int profession_Id, int specialty_Id, string searchText);
        int getNumberOfResultWhenSearch_2(int semester_Id, int profession_Id, int specialty_Id, string searchText, string studentId);
        GroupIdea GetGroupIdeaById(int id);
        List<string> ConvertProjectTags(string projectTags);
        int UpdateNumberOfMemberWhenAdd(int groupIdeaId);
        int UpdateNumberOfMemberWhenRemove(int groupIdeaId);
        int DeleteGroupIdea(int groupIdeaId);
        int UpdateIdea(GroupIdea groupIdea, int semesterId);
    }
}
