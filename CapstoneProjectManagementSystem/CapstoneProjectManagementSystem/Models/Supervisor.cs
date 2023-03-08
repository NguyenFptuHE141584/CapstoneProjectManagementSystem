using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class Supervisor:CommonProperty
    {
        public string SupervisorID { get; set; }
        public bool IsDevHead{ get; set; }
        public User User { get; set; }
        public IList<FinalGroup> FinalGroups { get; set; }
        public IList<ChangeTopicRequest> ChangeTopicRequests { get; set; }
        public IList<ReportMaterial> ReportMaterials { get; set; }

    }
}
