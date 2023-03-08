using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
using System.Collections.Generic;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class ChangeTopicRequestService : IChangeTopicRequestService
    {
        public int AddChangeTopicRequest(ChangeTopicRequest changeTopicRequest)
        {
           return ChangeTopicRequestDao.AddChangeTopicRequest(changeTopicRequest);
        }
        public List<ChangeTopicRequest> GetChangeTopicRequestsByStudentId(string studentId, int semesterId)
        {
            return ChangeTopicRequestDao.GetChangeTopicRequestsByStudentId(studentId,semesterId);
        }
        public List<ChangeTopicRequest> GetChangeTopicRequestsBySearchText(string searchText, int status, int semesterId, int offsetNumber, int fetchNumber)
        {
            if (searchText == null)
            {
                searchText = "";
            }
            else
            {
                searchText = string.Concat("%", searchText.Trim().Replace(" ", "").ToUpper(), "%");
            }
            return ChangeTopicRequestDao.GetChangeTopicRequestsBySearchText(searchText, status, semesterId, offsetNumber, fetchNumber);
        }

        public int CountRecordChangeTopicRequestsBySearchText(string searchText, int status, int semesterId)
        {
            if (searchText == null)
            {
                searchText = "";
            }
            else
            {
                searchText = string.Concat("%", searchText.Trim().Replace(" ", "").ToUpper(), "%");
            }
            return ChangeTopicRequestDao.CountRecordChangeTopicRequestsBySearchText(searchText, status, semesterId);
        }

        public int UpdateStatusOfChangeTopicRequest(int changeTopicRequestId, int status,string staffComment)
        {
            return ChangeTopicRequestDao.UpdateStatusOfChangeTopicRequest(changeTopicRequestId, status,staffComment);
        }

        public ChangeTopicRequest GetNewTopicByChangeTopicRequestId(int changeTopicRequestId)
        {
            return ChangeTopicRequestDao.GetNewTopicByChangeTopicRequestId(changeTopicRequestId);
        }

        public int DeleteChangeTopicRequestsByFinalGroup(int finalGropId)
        {
            return ChangeTopicRequestDao.DeleteChangeTopicRequestsByFinalGroup(finalGropId);
        }

        public ChangeTopicRequest GetDetailChangeTopicRequestsByRequestId(int requestId)
        {
            return ChangeTopicRequestDao.GetDetailChangeTopicRequestsByRequestId(requestId);
        }
    }
}
