using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao.StaffDao;
using CapstoneProjectManagementSystem.Services.StaffServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement.StaffImplement
{
    public class StaffService : IStaffService
    {
        public Staff GetUserIsStaffByRoleId(int roleId)
        {
            return StaffDao.GetUserIsStaffByRoleId(roleId);
        }
    }
}
