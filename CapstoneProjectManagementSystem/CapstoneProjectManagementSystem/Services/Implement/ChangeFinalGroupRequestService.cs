using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
using System.Collections.Generic;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class ChangeFinalGroupRequestService : IChangeFinalGroupRequestService
    {
        public int CreateChangeFinalGroupRequestDao(string fromStudentId, string toStudentId)
        {
            return ChangeFinalGroupRequestDao.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId);
        }

        public List<ChangeFinalGroupRequest> GetListChangeFinalGroupRequest(string studentId, int semesterId)
        {
            return ChangeFinalGroupRequestDao.GetListChangeFinalGroupRequest(studentId, semesterId);
        }

        public List<ChangeFinalGroupRequest> GetListChangeFinalGroupRequestFromOfStudent(string fromStudentId, int semesterId)
        {
            return ChangeFinalGroupRequestDao.GetListChangeFinalGroupRequestFromOfStudent(fromStudentId, semesterId);
        }
        public List<ChangeFinalGroupRequest> GetListChangeFinalGroupRequestToOfStudent(string toStudentId, int semesterId)
        {
            return ChangeFinalGroupRequestDao.GetListChangeFinalGroupRequestToOfStudent(toStudentId, semesterId);   
        }

        public int UpdateStatusAcceptOfToStudentByChangeFinalGroupRequestId(int changeFinalGroupRequestId)
        {
            return ChangeFinalGroupRequestDao.UpdateStatusAcceptOfToStudentByChangeFinalGroupRequestId(changeFinalGroupRequestId);
        }

        public int UpdateStatusRejectOfToStudentByChangeFinalGroupRequestId(int changeFinalGroupRequestId)
        {
            return ChangeFinalGroupRequestDao.UpdateStatusRejectOfToStudentByChangeFinalGroupRequestId(changeFinalGroupRequestId);
        }
        public string GetFromStudentIdByChangeFinalGroupRequestIdAndToStudentId(int changeFinalGroupRequestId, string toStudentId)
        {
            return ChangeFinalGroupRequestDao.GetFromStudentIdByChangeFinalGroupRequestIdAndToStudentId(changeFinalGroupRequestId, toStudentId);
        }

        public List<ChangeFinalGroupRequest> GetListChangeFinalGroupRequestBySearchText
                (string searchText, int status, int semesterId, int offsetNumber, int fetchNumber)
        {

            if (searchText == null)
            {
                searchText = "";
            }
            else
            {
                searchText = string.Concat("%", searchText.Trim().Replace(" ", "").ToUpper(), "%");
            }
            return ChangeFinalGroupRequestDao.GetListChangeFinalGroupRequestBySearchText
                (searchText, status, semesterId,offsetNumber, fetchNumber);
        }

        public int CountRecordChangeFinalGroupBySearchText(string searchText, int status, int semesterId)
        {
            if (searchText == null)
            {
                searchText = "";
            }
            else
            {
                searchText = string.Concat("%", searchText.Trim().Replace(" ", "").ToUpper(), "%");
            }
            return ChangeFinalGroupRequestDao.CountRecordChangeFinalGroupBySearchText(searchText,status,semesterId);
        }

        public ChangeFinalGroupRequest GetInforOfStudentExchangeFinalGroup(int changeFinalGroupRequestId)
        {
            return ChangeFinalGroupRequestDao.GetInforOfStudentExchangeFinalGroup(changeFinalGroupRequestId);
        }

        public int UpdateGroupForStudentByChangeFinalGroupRequest(ChangeFinalGroupRequest changeFinalGroupRequest)
        {
            return ChangeFinalGroupRequestDao.UpdateGroupForStudentByChangeFinalGroupRequest(changeFinalGroupRequest);
        }
        public int UpdateStatusOfStaffByChangeFinalGroupRequestId(int changeFinalGroupRequestId, string staffComment)
        {
            return ChangeFinalGroupRequestDao.UpdateStatusOfStaffByChangeFinalGroupRequestId(changeFinalGroupRequestId,staffComment);
        }
    }
}
