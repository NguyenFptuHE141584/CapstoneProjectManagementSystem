using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class SemesterService : ISemesterService
    {
        public Semester GetCurrentSemester()
        {
            return SemesterDao.GetCurrentSemester();
        }
        public Semester GetSemesterById(int semesterId)
        {
            return SemesterDao.GetSemesterById(semesterId);
        }
        public List<Semester> GetAllSemester()
        {
            return SemesterDao.GetAllSemester();
        }
        public Semester GetLastSemester()
        {
            return SemesterDao.GetLastSemester();
        }
        public int UpdateCurrentSemester(Semester semester)
        {
            return SemesterDao.UpdateCurrentSemester(semester);
        }

        public  int AddNewSemester(Semester semester)
        {
            return SemesterDao.AddNewSemester(semester);
        }

        public int CloseCurrentSemester(int semesterId)
        {
            return SemesterDao.CloseSemesterCurrent(semesterId);
        }
        public int ChangeShowGroupNameStatus(int semesterId, int status)
        {
            return SemesterDao.ChangeShowGroupNameStatus(semesterId, status);
        }
    }
}
