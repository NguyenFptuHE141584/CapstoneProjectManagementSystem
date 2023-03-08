using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class UserGuide:CommonProperty
    {
        public int UserGuideID { get; set; }
        public string  UserGuideLink { get; set; }
        public Staff Staff{ get; set; }
    }
}
