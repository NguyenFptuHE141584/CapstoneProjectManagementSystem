using CapstoneProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class Role:CommonProperty
    {
        public int Role_ID { get; set; }
        public string RoleName { get; set; }
        public IList<User> Users { get; set; }
        public IList<RolePermission>  RolePermissions{ get; set; }
    }
}
