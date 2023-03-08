using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class Profession:CommonProperty
    {
        public int ProfessionID { get; set; }
        public string ProfessionFullName { get; set; }
        public string ProfessionAbbreviation { get; set; }
        public Semester Semester { get; set; }
        public IList<Specialty> Specialties { get; set; }
        public IList<GroupIdea> GroupIdeas { get; set; }
        public IList<FinalGroup> FinalGroups { get; set; }
        public IList<Student> Students { get; set; }
    }
}
