using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class Notification:CommonProperty
    {
        public int NotificationID { get; set; }
        public bool Readed{ get; set; }
        public string NotificationContent { get; set; }
        public string AttachedLink { get; set; }
        public User User { get; set; }
    }
}
