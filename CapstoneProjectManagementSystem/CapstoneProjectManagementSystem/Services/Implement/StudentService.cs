using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class StudentService : IStudentService
    {
        public int UpdateStudentByGroupId(int finalGroupId, string groupName, int status, string studentId)
        {
            return StudentDao.UpdateStudentByGroupId(finalGroupId,groupName, status, studentId);
        }
        public int DeleteFinalGroupIdOfStudent(string studentId)
        {
            return StudentDao.DeleteFinalGroupIdOfStudent(studentId);
        }
        public Student GetStudentByFptEmail(string fptEmail,int semesterId)
        {
            return StudentDao.GetStudentByFptEmail(fptEmail,semesterId);
        }

        public Student GetStudentByStudentId(string studentId)
        {
            return StudentDao.GetStudentByStudentId(studentId);
        }
        public List<Student> GetStudentSearchList(int semester_Id, int profession_Id, int specialty_Id, int offsetNumber, int fetchNumber)
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
            return StudentDao.GetStudentSearchList(semester_Id.ToString(), profession_Id_ToString, specialty_Id_ToString, offsetNumber, fetchNumber);
        }
        public Student getLeaderByFinalGroupId(int finalGroupId)
        {
            return StudentDao.getLeaderByFinalGroupId(finalGroupId);
        }
        public List<Student> getListMemberByFinalGroupId(int finalGroupId)
        {
            return StudentDao.getListMemberByFinalGroupId(finalGroupId);
        }
        public List<Student> getListStudentNotHaveGroupBySpecialtyId(int semester_Id, int specialtyId)
        {
            return StudentDao.getListStudentNotHaveGroupBySpecialtyId(semester_Id, specialtyId);
        }
        public Student GetProfileOfStudentByUserId(string userId)
        {
            return StudentDao.GetProfileOfStudentByUserId(userId);
        }

        public int UpdateProfileOfStudent(Student student)
        {
            var x = StudentDao.UpdateInforProfileOfStudent(student);
            if ( x == 2)
                return 1;
            else
                return 0;
        }
        public int UpdateMajorOfStudentByUserId(string userId, int professionId, int specialtyId)
        {
            return StudentDao.UpdateMajorOfStudentByUserId(userId, professionId, specialtyId);
        }
        public int UpdateSemesterOfStudentByUserId(string userId)
        {
            return StudentDao.UpdateSemesterOfStudentByUserId(userId);
        }
        public int UpdateGroupName(string studentId, string groupName)
        {
            return StudentDao.UpdateGroupName(studentId, groupName);
        }
        public int GetProfessionIdOfStudentByUserId(string userId)
        {
            return StudentDao.GetProfessionIdOfStudentByUserId(userId);
        }
        public int GetSpecialtyIdOfStudentByUserId(string userId)
        {
            return StudentDao.GetSpecialtyIdOfStudentByUserId(userId);
        }

        public Student GetProfessionAndSpecialtyByStudentId(string studentId)
        {
            return StudentDao.GetProfessionAndSpecialtyByStudentId(studentId);
        }

        public Student GetStudentNotHaveGroupFinalByFptEmail(string fptEmail, int semesterId)
        {
            return StudentDao.GetStudentNotHaveGroupFinalByFptEmail(fptEmail, semesterId);
        }

        public int ChangeMemberForStudent(string[] listStudentIdOfOldMember, string[] listStudentIdOfNewMember, int finalGroupId ,int changeMemberRequestId)
        {
            List<string> listStudentIdOld = new List<string>();
            List<string> listStudentIdNew = new List<string>();
            var currentSemester = SemesterDao.GetCurrentSemester();
            foreach (var item in listStudentIdOfOldMember)
            {
                listStudentIdOld.Add(StudentDao.GetStudentByFptEmail(item, currentSemester.SemesterID).StudentID);
            }
            foreach (var item in listStudentIdOfNewMember)
            {
                listStudentIdNew.Add(StudentDao.GetStudentByFptEmail(item, currentSemester.SemesterID).StudentID);
            }
            return StudentDao.UpdateFinalGroupIdForStudent(listStudentIdOld, listStudentIdNew, finalGroupId , changeMemberRequestId);
        }

     
        public int SetFinalGroupForStudent(int finalGroupId, int isLeader, string studentId, string groupName)
        {
            return StudentDao.SetFinalGroupForStudent(finalGroupId, isLeader, studentId, groupName) ;
        }

        public int GetFinalGroupIdOfStudentIsLeader(int groupIdeaId)
        {
            return StudentDao.GetFinalGroupIdOfStudentIsLeader(groupIdeaId);
        }

        public List<Student> GetListStudentIdByFinalGroupId(int finalGroupId)
        {
            return StudentDao.GetListStudentIdByFinalGroupId(finalGroupId);
        }

        public string GetStudentIDByFptEmailAndGroupName(string fptEmail, string groupName, int semesterId)
        {
            return StudentDao.GetStudentIDByFptEmailAndGroupName(fptEmail, groupName, semesterId);
        }

        public Student GetInforStudentHaveFinalGroup(string studentId, int semesterId)
        {
            return StudentDao.GetInforStudentHaveFinalGroup(studentId, semesterId);
        }
        public Student GetInforStudentHaveRegisteredGroup(string fptEmail, int groupIdeaId)
        {
            return StudentDao.GetInforStudentHaveRegisteredGroup(fptEmail,groupIdeaId);
        }

    }
}
