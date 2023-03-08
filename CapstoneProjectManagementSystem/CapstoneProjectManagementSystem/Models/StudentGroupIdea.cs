using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class StudentGroupIdea:CommonProperty
    {
        public string StudentID { get; set; }
        public int GroupIdeaID { get; set; }

        //Status:   1 -> Leader
        //          2 -> Member
        //          3 -> Request
        //          4 -> Request accepted / Invited
        //          5 -> Request denied
        //          6 -> leaved group
        public int Status { get; set; } 
        public Student Student { get; set; }
        public GroupIdea GroupIdea { get; set; }
        public string Message { get; set; }
    }
}
