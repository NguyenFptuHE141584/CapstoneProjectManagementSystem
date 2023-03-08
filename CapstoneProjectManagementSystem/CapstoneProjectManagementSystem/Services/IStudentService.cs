using CapstoneProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface IStudentService
    {
        int UpdateStudentByGroupId(int finalGroupId, string groupName, int status, string studentId);
        int DeleteFinalGroupIdOfStudent(string studentId);
        Student GetStudentByStudentId(string studentId);
        List<Student> GetStudentSearchList(int semester_Id, int profession_Id, int specialty_Id, int offsetNumber, int fetchNumber);
        Student GetStudentByFptEmail(string fptEmail, int semesterId);
        Student getLeaderByFinalGroupId(int finalGroupId);
        List<Student> getListMemberByFinalGroupId(int finalGroupId);
        List<Student> GetListStudentIdByFinalGroupId(int finalGroupId);
        List<Student> getListStudentNotHaveGroupBySpecialtyId(int semester_Id, int specialtyId);
        Student GetProfileOfStudentByUserId(string userId);
        int UpdateProfileOfStudent(Student student);
        int UpdateMajorOfStudentByUserId(string userId, int professionId, int specialtyId);
        int UpdateSemesterOfStudentByUserId(string userId);
        int UpdateGroupName(string studentId, string groupName);
        int GetProfessionIdOfStudentByUserId(string userId);
        int GetSpecialtyIdOfStudentByUserId(string userId);
        Student GetProfessionAndSpecialtyByStudentId(string studentId);
        Student GetStudentNotHaveGroupFinalByFptEmail(string fptEmail, int semesterId);
        int ChangeMemberForStudent(string[] listStudentIdOfOldMember, string[] listStudentIdOfNewMember, int finalGroupId,int changeMemberRequestId);
        int SetFinalGroupForStudent(int finalGroupId, int isLeader, string studentId, string groupName);
        int GetFinalGroupIdOfStudentIsLeader(int groupIdeaId);
        string GetStudentIDByFptEmailAndGroupName(string fptEmail, string groupName, int semesterId);
        Student GetInforStudentHaveFinalGroup(string studentId, int semesterId);
        Student GetInforStudentHaveRegisteredGroup(string fptEmail, int groupIdeaId);
    }
}
