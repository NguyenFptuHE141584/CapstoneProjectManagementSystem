using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class RolePermission:CommonProperty
    {
        public int RoleID { get; set; }
        public int PermissionID { get; set; }
        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
