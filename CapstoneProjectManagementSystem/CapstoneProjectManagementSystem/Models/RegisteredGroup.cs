using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class RegisteredGroup:CommonProperty
    {
        public int RegisteredGroupID { get; set; }
        public string RegisteredSupervisorName1 { get; set; }
        public string RegisteredSupervisorName2 { get; set; }
        public string RegisteredSupervisorPhone1 { get; set; }
        public string RegisteredSupervisorPhone2 { get; set; }
        public string RegisteredSupervisorEmail1 { get; set; }
        public string RegisteredSupervisorEmail2 { get; set; }
        public string StudentComment { get; set; }
        public int  Status { get; set; }
        public string StaffComment { get; set; }
        public string Students_Registration { get; set; }
        public GroupIdea GroupIdea { get; set; }
    }
}
