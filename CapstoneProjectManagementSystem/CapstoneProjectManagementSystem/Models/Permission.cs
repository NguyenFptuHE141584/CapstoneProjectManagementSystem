using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class Permission:CommonProperty
    {
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public IList<RolePermission> RolePermissions { get; set; }

    }
}
