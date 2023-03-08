using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class Student_FavoriteGroupIdeaService : IStudent_FavoriteGroupIdeaService
    {
        public List<StudentFavoriteGroupIdea> GetFavoriteIdeaListByStudentId(string studentId)
        {
            return Student_FavoriteGroupIdeaDao.GetFavoriteIdeaListByStudentId(studentId);
        }
        public StudentFavoriteGroupIdea GetRecord(string studentId, int groupId)
        {
            return Student_FavoriteGroupIdeaDao.GetRecord(studentId, groupId);
        }
        public int AddRecord(string studentId, int groupId)
        {
            return Student_FavoriteGroupIdeaDao.AddRecord(studentId, groupId);
        }
        public int DeleteRecord(string studentId, int groupIdeaId)
        {
            return Student_FavoriteGroupIdeaDao.DeleteRecord(studentId, groupIdeaId);
        }
        public int DeleteAllRecordOfAGroupIdea(int groupIdeaId)
        {
            return Student_FavoriteGroupIdeaDao.DeleteAllRecordOfAGroupIdea(groupIdeaId);
        }
    }
}
