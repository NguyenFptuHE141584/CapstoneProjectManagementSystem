using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class GroupIdea:CommonProperty
    {
        public int GroupIdeaID { get; set; }
        public string ProjectEnglishName { get; set; }
        public string ProjectVietNameseName { get; set; }
        public string Abrrevation { get; set; }
        public string Description { get; set; }
        public string ProjectTags { get; set; }
        public int  NumberOfMember{ get; set; }
        public int MaxMember { get; set; }
        public Profession Profession { get; set; }
        public Specialty Specialty{ get; set; }
        public Semester Semester { get; set; }
        public IList<RegisteredGroup> RegisteredGroups{ get; set; }
        public IList<StudentGroupIdea> StudentGroupIdeas { get; set; }
        public IList<Student> Students { get; set; }
    }
}
