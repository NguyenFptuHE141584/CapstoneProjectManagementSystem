using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Common;
using CapstoneProjectManagementSystem.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class Student_GroupIdeaService : IStudent_GroupIdeaService
    {
        //NguyenLH
        public string GetLeaderIdByGroupIdeaId(int groupIdeaId)
        {
            return Student_GroupIdeaDao.GetLeaderIdByGroupIdeaId(groupIdeaId);
        }
        public List<string> GetMemberIdByGroupIdeaId(int groupIdeaId)
        {
            return Student_GroupIdeaDao.GetMemberIdByGroupIdeaId(groupIdeaId);
        }
        public List<JoinRequest> GetAllJoinRequestByGroupIdeaId(int groupIdeaId)
        {
            return Student_GroupIdeaDao.GetAllJoinRequestByGroupIdeaId(groupIdeaId);
        }
        public string GetGroupIdByStudentId(string studentId)
        {
            return Student_GroupIdeaDao.GetGroupIdByStudentId(studentId);
        }
        public List<StudentGroupIdea> GetListRequestByStudentId(string studentId)
        {
            return Student_GroupIdeaDao.GetListRequestByStudentId(studentId);
        }
        public int UpdateStatusToAccept(string studentId, int groupIdeaId)
        {
            return Student_GroupIdeaDao.UpdateStatusToAccept(studentId, groupIdeaId);
        }
        public int UpdateStatusToReject(string studentId, int groupIdeaId)
        {
            return Student_GroupIdeaDao.UpdateStatusToReject(studentId, groupIdeaId);
        }
        public int UpdateStatusToMember(string studentId, int groupIdeaId)
        {
            return Student_GroupIdeaDao.UpdateStatusToMember(studentId, groupIdeaId);
        }
        public int UpdateStatusToLeader(string studentId, int groupIdeaId)
        {
            return Student_GroupIdeaDao.UpdateStatusToLeader(studentId, groupIdeaId);
        }

        public int DeleteRecord(string studentId, int groupIdeaId)
        {
            return Student_GroupIdeaDao.DeleteRecord(studentId, groupIdeaId);
        }
        public int DeleteAllRequest(string studentId)
        {
            return Student_GroupIdeaDao.DeleteAllRequest(studentId);
        }
        public int DeleteAllRecordOfGroupIdea(int groupIdeaId)
        {
            return Student_GroupIdeaDao.DeleteAllRecordOfGroupIdea(groupIdeaId);
        }

        public int DeleteRecordHaveStatusEqual3or4or5OfGroupIdea(int groupIdeaId)
        {
            return Student_GroupIdeaDao.DeleteRecordHaveStatusEqual3or4or5OfGroupIdea(groupIdeaId);
        }
        //NguyenNH
        public int FilterPermissionOfStudent(string studentId)
        {
            return Student_GroupIdeaDao.CheckStatusOfStudentInGroupIdea(studentId);
        }

        public GroupIdea GetGroupIdeaOfStudent(string studentId, int status)
        {
            return Student_GroupIdeaDao.GetGroupIdeaOfStudent(studentId, status);
        }

        public List<Student> GetStudentsHadOneGroupIdea(int groupIdea)
        {
            return Student_GroupIdeaDao.GetStudentsHaveGroup(groupIdea);
        }
        public bool FilterStudentHaveIdea(string studentId, int semesterId)
        {
            return Student_GroupIdeaDao.CheckStudentHaveIdea(studentId, semesterId);
        }
        public int AddRecord(string studentId, int groupId, int status, string message)
        {
            return Student_GroupIdeaDao.AddRecord(studentId, groupId, status, message);
        }

        public bool CheckAddedStudentIsValid(string fptEmail)
        {
            var listStatusOfStudentInEachGroup = Student_GroupIdeaDao.GetListStatusOfStudentInEachGroupByFptEmail(fptEmail);
            if (listStatusOfStudentInEachGroup == null)
                return false;
            else if (listStatusOfStudentInEachGroup.Any(status => status == 1 || status == 2))
                return true;
            else
                return false;
        }

        public int CreateGroupIdea(GroupIdea groupIdea, string studentId, int semesterId, int maxMember)
        {
            return Student_GroupIdeaDao.CreateIdea(groupIdea, studentId, semesterId, maxMember);
        }

        public int DeleteGroupIdeaAndStudentInGroupIdea(int groupIdeaId)
        {
            return Student_GroupIdeaDao.DeleteGroupIdeaAndStudentInGroupIdea(groupIdeaId);
        }

        public StudentGroupIdea GetStudentGroupIdeaByGroupIdeaIdAndFptEmail(int groupIdeaId, string fptEmail)
        {
            return Student_GroupIdeaDao.GetStudentGroupIdeaByGroupIdeaIdAndFptEmail(groupIdeaId, fptEmail);
        }

        public List<StudentGroupIdea> GetListStudentInGroupByGroupIdeaId(int groupIdeaId)
        {
            return Student_GroupIdeaDao.GetListStudentInGroupByGroupIdeaId(groupIdeaId);
        }

        public List<StudentGroupIdea> GetInforStudentInGroupIdea(int groupIdeaId)
        {
            return Student_GroupIdeaDao.GetInforStudentInGroupIdea(groupIdeaId);
        }

        public int RecoveryStudentInGroupIdeaAfterRejected(string studentId, int groupIdeaId)
        {
            return Student_GroupIdeaDao.RecoveryStudentInGroupIdeaAfterRejected(studentId, groupIdeaId);
        }
    }
}
