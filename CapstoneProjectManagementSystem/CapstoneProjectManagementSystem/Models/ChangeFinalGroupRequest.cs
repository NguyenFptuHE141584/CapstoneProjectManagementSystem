using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class ChangeFinalGroupRequest:CommonProperty
    {
        public int ChangeFinalGroupRequestId { get; set; }
        public Student FromStudent { get; set; }
        public Student ToStudent { get; set; }
        public int StatusOfToStudent{ get; set; }
        public int StatusOfStaff{ get; set; }
        public string StaffComment { get; set; }
    }
}
