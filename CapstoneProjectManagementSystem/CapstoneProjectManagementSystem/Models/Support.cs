using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class Support :CommonProperty
    {
        public int SupportID { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactEmail{ get; set; }
        public string SupportMessge{ get; set; }
        public string Attachment{ get; set; }
        public string Title{ get; set; }
        public int Status{ get; set; }  //0: pending
                                        //1: processed
        public Student Student{ get; set; }
    }
}
