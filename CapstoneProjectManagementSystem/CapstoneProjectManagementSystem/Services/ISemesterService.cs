using CapstoneProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface ISemesterService
    {
        Semester GetCurrentSemester();
        Semester GetSemesterById(int semesterId);
        List<Semester> GetAllSemester();
        Semester GetLastSemester();
        int UpdateCurrentSemester(Semester semester);
        int AddNewSemester(Semester semester);
        int CloseCurrentSemester(int semesterId);
        int ChangeShowGroupNameStatus(int semesterId, int status);
    }
}
