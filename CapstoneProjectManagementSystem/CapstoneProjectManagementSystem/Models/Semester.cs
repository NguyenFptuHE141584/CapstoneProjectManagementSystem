using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class Semester:CommonProperty
    {
        public int SemesterID { get; set; }
        public string SemesterName { get; set; }
        public string SemesterCode { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool StatusClose { get; set; }
        public bool ShowGroupName { get; set; }
        public IList<GroupIdea> GroupIdeas { get; set; }
        public IList<FinalGroup> FinalGroups { get; set; }
    }
}
