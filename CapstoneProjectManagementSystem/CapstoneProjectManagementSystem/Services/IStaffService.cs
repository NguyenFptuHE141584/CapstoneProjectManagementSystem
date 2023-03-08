using CapstoneProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.StaffServices
{
    public interface IStaffService
    {
        Staff GetUserIsStaffByRoleId(int roleId);
    }
}
