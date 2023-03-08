using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class UserService : IUserService
    {
        public int AddUser(User user, int roleId)
        {
           return  UserDao.AddUser(user, roleId);
        }

        public int CheckProfileUserHaveAttributeIsNullByUserId(string userId)
        {
            return UserDao.CheckProfileUserHaveAttributeIsNullByUserId(userId);
        }

        public bool CheckRoleOfUser(string userId ,string role)
        {
            return(UserDao.CheckUserByUserIdAndRoleExist(userId,role) == 0) ? false : true;
        }

        public User GetUserByID(string userId)
        {
            return UserDao.GetUserById(userId);
        }

        public int UpdateAvatar(string avatar, string userId)
        {
            return UserDao.UpdateAvatar(avatar, userId);
        }

        public string GetListFptEmailByGroupIdeaId(int groupIdeaId)
        {
            return UserDao.GetListFptEmailByGroupIdeaId(groupIdeaId);
        }

        public string GetNameStudentByUserId(string userId)
        {
            return UserDao.GetNameStudentByUserId(userId);
        }

        public User GetUserByFptEmail(string fptEmail)
        {
            return UserDao.GetUserByFptEmail(fptEmail);
        }
    }
}
