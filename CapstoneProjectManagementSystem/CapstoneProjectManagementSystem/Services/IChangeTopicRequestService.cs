using CapstoneProjectManagementSystem.Models;
using System.Collections.Generic;

namespace CapstoneProjectManagementSystem.Services
{
    public interface IChangeTopicRequestService
    {
        int AddChangeTopicRequest(ChangeTopicRequest changeTopicRequest);
        List<ChangeTopicRequest> GetChangeTopicRequestsByStudentId(string studentId, int semesterId);
        ChangeTopicRequest GetDetailChangeTopicRequestsByRequestId(int requestId);
        List<ChangeTopicRequest> GetChangeTopicRequestsBySearchText
            (string searchText, int status, int semesterId, int offsetNumber, int fetchNumber);
        int UpdateStatusOfChangeTopicRequest(int changeTopicRequestId, int status, string staffComment);
        ChangeTopicRequest GetNewTopicByChangeTopicRequestId(int changeTopicRequestId);
        int DeleteChangeTopicRequestsByFinalGroup(int finalGropId);
        int CountRecordChangeTopicRequestsBySearchText(string searchText, int status, int semesterId);
    }
}
