using CapstoneProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface IUserService
    {
        int AddUser(User user, int roleId);

        User GetUserByID(string userId);

        bool CheckRoleOfUser(string userId,string role);

        int CheckProfileUserHaveAttributeIsNullByUserId(string userId);

        int UpdateAvatar(string avatar, string userId);

        string GetListFptEmailByGroupIdeaId(int groupIdeaId);

        string GetNameStudentByUserId(string userId);

        User GetUserByFptEmail(string fptEmail);
    }
}
