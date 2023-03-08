using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class User : CommonProperty
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string FptEmail { get; set; }
        public string Avatar { get; set; }
        public int Gender { get; set; }
        public AffiliateAccount AffiliateAccount { get; set; }
        public Staff Staff { get; set; }
        public Student Student { get; set; }
        public Supervisor Supervisor { get; set; }
        public Role Role { get; set; }
        public IList<Notification> Notifications { get; set; }
    }
}
