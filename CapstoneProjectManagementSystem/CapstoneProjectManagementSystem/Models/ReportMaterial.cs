using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class ReportMaterial:CommonProperty
    {
        public int ReportID{ get; set; }
        public string ReportTitle{ get; set; }
        public string ReportContent{ get; set; }
        public int Status{ get; set; }
        public DateTime DueDate{ get; set; }
        public string SubmissionComment{ get; set; }
        public string SubmissionAttachment{ get; set; }
        public FinalGroup FinalGroup { get; set; }
        public Supervisor Supervisor{ get; set; }
    }
}
