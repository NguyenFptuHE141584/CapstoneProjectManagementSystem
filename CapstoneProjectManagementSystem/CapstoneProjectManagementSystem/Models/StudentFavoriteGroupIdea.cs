using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class StudentFavoriteGroupIdea
    {
        public string StudentID { get; set; }
        public int GroupIdeaID { get; set; }
        public Student Student { get; set; }
        public GroupIdea GroupIdea { get; set; }
    }
}
