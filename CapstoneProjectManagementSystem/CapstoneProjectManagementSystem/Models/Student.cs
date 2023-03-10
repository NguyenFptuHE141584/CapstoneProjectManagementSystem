using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class Student :CommonProperty
    {
        public string StudentID { get; set; }
        public string RollNumber { get; set; }
        public string Curriculum { get; set; }
        public string EmailAddress{ get; set; }
        public string SelfDescription { get; set; }
        public string ExpectedRoleInGroup { get; set; }
        public string PhoneNumber { get; set; }
        public string LinkFacebook { get; set; }
        public string GroupName { get; set; }
        public bool IsLeader { get; set; }
        public User User{ get; set; }
        public Profession Profession{ get; set; }
        public Specialty Specialty { get; set; }
        public Semester Semester{ get; set; }
        public IList<Support> Supports { get; set; }
        public IList<StudentGroupIdea> StudentGroupIdeas { get; set; }
        public IList<GroupIdea> GroupIdea { get; set; }
        public FinalGroup FinalGroup { get; set; }
        public IList<ChangeFinalGroupRequest> ChangeFinalGroupRequests { get; set; }
    }
}
