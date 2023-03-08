using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class Specialty:CommonProperty
    {
        public int SpecialtyID { get; set; }
        public string SpecialtyFullName { get; set; }
        public string SpecialtyAbbreviation { get; set; }
        public int MaxMember { get; set; }
        public string CodeOfGroupName { get; set; }
        public Semester Semester { get; set; }
        public Profession Profession { get; set; }
        public IList<GroupIdea> GroupIdeas { get; set; }
        public IList<FinalGroup> FinalGroups { get; set; }
        public IList<Student> Students{ get; set; }
    }
}
