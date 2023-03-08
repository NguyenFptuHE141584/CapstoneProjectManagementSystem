using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class FinalGroup:CommonProperty
    {
        public int FinalGroupID { get; set; }
        public string GroupName{ get; set; }
        public string ProjectEnglishName { get; set; }
        public string ProjectVietNameseName { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public int MaxMember { get; set; }
        public int NumberOfMember { get; set; }
        public Semester Semester { get; set; }
        public Profession Profession { get; set; }
        public Specialty Specialty { get; set; }
        public Supervisor Supervisor { get; set; }
        public IList<Student> Students { get; set; }
        public IList<DefenceSchedule> DefenceSchedules { get; set; }
        public IList<ChangeTopicRequest> ChangeTopicRequests { get; set; }
        public IList<ReportMaterial> ReportMaterials { get; set; }

    }
}
