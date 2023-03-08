using CapstoneProjectManagementSystem.Models;
using System.Collections.Generic;

namespace CapstoneProjectManagementSystem.Services
{
    public interface IChangeFinalGroupRequestService
    {
        int CreateChangeFinalGroupRequestDao(string fromStudentId, string toStudentId);
        List<ChangeFinalGroupRequest> GetListChangeFinalGroupRequestFromOfStudent(string fromStudentId, int semesterId);
        List<ChangeFinalGroupRequest> GetListChangeFinalGroupRequestToOfStudent(string toStudentId, int semesterId);
        List<ChangeFinalGroupRequest> GetListChangeFinalGroupRequest(string studentId, int semesterId);
        int UpdateStatusAcceptOfToStudentByChangeFinalGroupRequestId(int changeFinalGroupRequestId);
        int UpdateStatusRejectOfToStudentByChangeFinalGroupRequestId(int changeFinalGroupRequestId);
        string GetFromStudentIdByChangeFinalGroupRequestIdAndToStudentId(int changeFinalGroupRequestId, string toStudentId);
        List<ChangeFinalGroupRequest> GetListChangeFinalGroupRequestBySearchText
                (string searchText, int status, int semesterId, int offsetNumber, int fetchNumber);
        int CountRecordChangeFinalGroupBySearchText(string searchText, int status, int semesterId);
        ChangeFinalGroupRequest GetInforOfStudentExchangeFinalGroup(int changeFinalGroupRequestId);
        int UpdateGroupForStudentByChangeFinalGroupRequest(ChangeFinalGroupRequest changeFinalGroupRequest);
        int UpdateStatusOfStaffByChangeFinalGroupRequestId(int changeFinalGroupRequestId, string staffComment);
    }
}
