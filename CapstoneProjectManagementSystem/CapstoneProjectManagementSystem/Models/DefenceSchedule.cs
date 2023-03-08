using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class DefenceSchedule:CommonProperty
    {
        public int DefenceScheduleID { get; set; }
        public int Type { get; set; }
        public DateTime DateDefence{ get; set; }
        public DateTime TimeDefence{ get; set; }
        public string RoomDefence{ get; set; }
        public string CouncilInfor{ get; set; }
        public FinalGroup FinalGroup { get; set; }
    }
}
