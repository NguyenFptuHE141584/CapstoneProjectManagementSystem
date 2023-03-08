using CapstoneProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface IStudent_FavoriteGroupIdeaService
    {
        List<StudentFavoriteGroupIdea> GetFavoriteIdeaListByStudentId(string studentId);
        StudentFavoriteGroupIdea GetRecord(string studentId, int groupId);
        int AddRecord(string studentId, int groupId);
        int DeleteRecord(string studentId, int groupIdeaId);
        int DeleteAllRecordOfAGroupIdea(int groupIdeaId);
    }
}
