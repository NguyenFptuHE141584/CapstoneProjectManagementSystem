using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Services;
using CapstoneProjectManagementSystem.Services.Implement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace CapstoneProjectManagementSystem.Services.Implement.CommonImplement.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        private UserService userService;
        public UserServiceTests()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IUserService), typeof(UserService));
            userService = container.Resolve<UserService>();
        }

        /// <summary>
        /// Test Function AddUser
        /// </summary>
        /// 

        //Test exception
        [TestMethod()]
        public void AddUser_TestException()
        {
            User user = null;
            int roleId = 0;
            try
            {
                if (user != null)
                {
                    if (roleId != 1 || roleId != 2)
                    {
                        if (user.UserID != "" && user.UserName != "" && user.FptEmail != ""
                            && user.Avatar != "" && user.FullName != "")
                        {
                            var repoMock = new Mock<IUserService>();
                            repoMock.Setup(repoMock => repoMock.AddUser(user, roleId)).Returns(0);
                            var userService = repoMock.Object;
                            var actual = userService.AddUser(user, roleId);
                            if (actual == 2)
                            {
                                Assert.AreEqual(2, actual);
                            }
                            else
                            {
                                Assert.AreEqual(0, actual);
                            }
                        }
                        else
                        {
                            if (user.UserID == "")
                            {
                                Assert.AreEqual("", user.UserID);
                                return;
                            }
                            else if (user.UserName == "")
                            {
                                Assert.AreEqual("", user.UserName);
                                return;
                            }
                            else if (user.FptEmail == "")
                            {
                                Assert.AreEqual("", user.FptEmail);
                                return;
                            }
                            else if (user.Avatar == "")
                            {
                                Assert.AreEqual("", user.Avatar);
                                return;
                            }
                            else if (user.FullName == "")
                            {
                                Assert.AreEqual("", user.UserID);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Assert.AreNotEqual(roleId, 1);
                        Assert.AreNotEqual(roleId, 2);
                        return;
                    }
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                Assert.IsNull(user);
                return;
            }
        }

        //Test RoleID Not Be long To 1 or 3
        [TestMethod()]
        public void AddUser_TestRoleIDNotBelongTo1or3()
        {
            User user = new User()
            {
                UserID = "nguyennhhe141584@fpt.edu.vn",
                UserName = "nguyennhhe141584",
                FptEmail = "nguyennhhe141584@fpt.edu.vn",
                Avatar = @"https://lh3.googleusercontent.com/a/AEdFTp4ECSBbtM8UL4rXjXEn01_p-6Lqq3uq8dtvg2Cf=s96-c",
                FullName = "Nguyễn Hoàng Nguyên",
            };
            int roleId = 0;
            try
            {
                if (user != null)
                {
                    if (roleId == 1 || roleId == 3)
                    {
                        if (user.UserID != "" && user.UserName != "" && user.FptEmail != ""
                            && user.Avatar != "" && user.FullName != "")
                        {
                            var repoMock = new Mock<IUserService>();
                            repoMock.Setup(repoMock => repoMock.AddUser(user, roleId)).Returns(0);
                            var userService = repoMock.Object;
                            var actual = userService.AddUser(user, roleId);
                            if (actual == 2)
                            {
                                Assert.AreEqual(2, actual);
                            }
                            else
                            {
                                Assert.AreEqual(0, actual);
                            }
                        }
                        else
                        {
                            if (user.UserID == "")
                            {
                                Assert.AreEqual("", user.UserID);
                                return;
                            }
                            else if (user.UserName == "")
                            {
                                Assert.AreEqual("", user.UserName);
                                return;
                            }
                            else if (user.FptEmail == "")
                            {
                                Assert.AreEqual("", user.FptEmail);
                                return;
                            }
                            else if (user.Avatar == "")
                            {
                                Assert.AreEqual("", user.Avatar);
                                return;
                            }
                            else if (user.FullName == "")
                            {
                                Assert.AreEqual("", user.UserID);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Assert.AreNotEqual(roleId, 1);
                        Assert.AreNotEqual(roleId, 2);
                        return;
                    }
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                Assert.IsNull(user);
                return;
            }
        }

        //Test UserID Is Empty
        [TestMethod()]
        public void AddUser_TestUserIDIsEmpty()
        {
            User user = new User()
            {
                UserID = "",
                UserName = "nguyennhhe141584",
                FptEmail = "nguyennhhe141584@fpt.edu.vn",
                Avatar = @"https://lh3.googleusercontent.com/a/AEdFTp4ECSBbtM8UL4rXjXEn01_p-6Lqq3uq8dtvg2Cf=s96-c",
                FullName = "Nguyễn Hoàng Nguyên",
            };
            int roleId = 1;
            //int roleId = 3;
            try
            {
                if (user != null)
                {
                    if (roleId == 1 || roleId == 3)
                    {
                        if (user.UserID != "" && user.UserName != "" && user.FptEmail != ""
                            && user.Avatar != "" && user.FullName != "")
                        {
                            var repoMock = new Mock<IUserService>();
                            repoMock.Setup(repoMock => repoMock.AddUser(user, roleId)).Returns(0);
                            var userService = repoMock.Object;
                            var actual = userService.AddUser(user, roleId);
                            if (actual == 2)
                            {
                                Assert.AreEqual(2, actual);
                            }
                            else
                            {
                                Assert.AreEqual(0, actual);
                            }
                        }
                        else
                        {
                            if (user.UserID == "")
                            {
                                Assert.AreEqual("", user.UserID);
                                return;
                            }
                            else if (user.UserName == "")
                            {
                                Assert.AreEqual("", user.UserName);
                                return;
                            }
                            else if (user.FptEmail == "")
                            {
                                Assert.AreEqual("", user.FptEmail);
                                return;
                            }
                            else if (user.Avatar == "")
                            {
                                Assert.AreEqual("", user.Avatar);
                                return;
                            }
                            else if (user.FullName == "")
                            {
                                Assert.AreEqual("", user.UserID);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Assert.AreNotEqual(roleId, 1);
                        Assert.AreNotEqual(roleId, 2);
                        return;
                    }
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                Assert.IsNull(user);
                return;
            }
        }

        //Test UserName Is Empty
        [TestMethod()]
        public void AddUser_TestUserNameIsEmpty()
        {
            User user = new User()
            {
                UserID = "nguyennhhe141584@fpt.edu.vn",
                UserName = "",
                FptEmail = "nguyennhhe141584@fpt.edu.vn",
                Avatar = @"https://lh3.googleusercontent.com/a/AEdFTp4ECSBbtM8UL4rXjXEn01_p-6Lqq3uq8dtvg2Cf=s96-c",
                FullName = "Nguyễn Hoàng Nguyên",
            };
            int roleId = 1;
            //int roleId = 3;
            try
            {
                if (user != null)
                {
                    if (roleId == 1 || roleId == 3)
                    {
                        if (user.UserID != "" && user.UserName != "" && user.FptEmail != ""
                            && user.Avatar != "" && user.FullName != "")
                        {
                            var repoMock = new Mock<IUserService>();
                            repoMock.Setup(repoMock => repoMock.AddUser(user, roleId)).Returns(0);
                            var userService = repoMock.Object;
                            var actual = userService.AddUser(user, roleId);
                            if (actual == 2)
                            {
                                Assert.AreEqual(2, actual);
                            }
                            else
                            {
                                Assert.AreEqual(0, actual);
                            }
                        }
                        else
                        {
                            if (user.UserID == "")
                            {
                                Assert.AreEqual("", user.UserID);
                                return;
                            }
                            else if (user.UserName == "")
                            {
                                Assert.AreEqual("", user.UserName);
                                return;
                            }
                            else if (user.FptEmail == "")
                            {
                                Assert.AreEqual("", user.FptEmail);
                                return;
                            }
                            else if (user.Avatar == "")
                            {
                                Assert.AreEqual("", user.Avatar);
                                return;
                            }
                            else if (user.FullName == "")
                            {
                                Assert.AreEqual("", user.UserID);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Assert.AreNotEqual(roleId, 1);
                        Assert.AreNotEqual(roleId, 2);
                        return;
                    }
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                Assert.IsNull(user);
                return;
            }
        }

        //Test FptEmail Is Empty
        [TestMethod()]
        public void AddUser_TestFptEmailIsEmpty()
        {
            User user = new User()
            {
                UserID = "nguyennhhe141584@fpt.edu.vn",
                UserName = "nguyennhhe141584",
                FptEmail = "",
                Avatar = @"https://lh3.googleusercontent.com/a/AEdFTp4ECSBbtM8UL4rXjXEn01_p-6Lqq3uq8dtvg2Cf=s96-c",
                FullName = "Nguyễn Hoàng Nguyên",
            };
            int roleId = 1;
            //int roleId = 3;
            try
            {
                if (user != null)
                {
                    if (roleId == 1 || roleId == 3)
                    {
                        if (user.UserID != "" && user.UserName != "" && user.FptEmail != ""
                            && user.Avatar != "" && user.FullName != "")
                        {
                            var repoMock = new Mock<IUserService>();
                            repoMock.Setup(repoMock => repoMock.AddUser(user, roleId)).Returns(0);
                            var userService = repoMock.Object;
                            var actual = userService.AddUser(user, roleId);
                            if (actual == 2)
                            {
                                Assert.AreEqual(2, actual);
                            }
                            else
                            {
                                Assert.AreEqual(0, actual);
                            }
                        }
                        else
                        {
                            if (user.UserID == "")
                            {
                                Assert.AreEqual("", user.UserID);
                                return;
                            }
                            else if (user.UserName == "")
                            {
                                Assert.AreEqual("", user.UserName);
                                return;
                            }
                            else if (user.FptEmail == "")
                            {
                                Assert.AreEqual("", user.FptEmail);
                                return;
                            }
                            else if (user.Avatar == "")
                            {
                                Assert.AreEqual("", user.Avatar);
                                return;
                            }
                            else if (user.FullName == "")
                            {
                                Assert.AreEqual("", user.UserID);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Assert.AreNotEqual(roleId, 1);
                        Assert.AreNotEqual(roleId, 2);
                        return;
                    }
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                Assert.IsNull(user);
                return;
            }
        }

        //Test Avatar Is Empty
        [TestMethod()]
        public void AddUser_TestAvatarIsEmpty()
        {
            User user = new User()
            {
                UserID = "nguyennhhe141584@fpt.edu.vn",
                UserName = "nguyennhhe141584",
                FptEmail = "nguyennhhe141584@fpt.edu.vn",
                Avatar = @"",
                FullName = "Nguyễn Hoàng Nguyên",
            };
            int roleId = 1;
            //int roleId = 3;
            try
            {
                if (user != null)
                {
                    if (roleId == 1 || roleId == 3)
                    {
                        if (user.UserID != "" && user.UserName != "" && user.FptEmail != ""
                            && user.Avatar != "" && user.FullName != "")
                        {
                            var repoMock = new Mock<IUserService>();
                            repoMock.Setup(repoMock => repoMock.AddUser(user, roleId)).Returns(0);
                            var userService = repoMock.Object;
                            var actual = userService.AddUser(user, roleId);
                            if (actual == 2)
                            {
                                Assert.AreEqual(2, actual);
                            }
                            else
                            {
                                Assert.AreEqual(0, actual);
                            }
                        }
                        else
                        {
                            if (user.UserID == "")
                            {
                                Assert.AreEqual("", user.UserID);
                                return;
                            }
                            else if (user.UserName == "")
                            {
                                Assert.AreEqual("", user.UserName);
                                return;
                            }
                            else if (user.FptEmail == "")
                            {
                                Assert.AreEqual("", user.FptEmail);
                                return;
                            }
                            else if (user.Avatar == "")
                            {
                                Assert.AreEqual("", user.Avatar);
                                return;
                            }
                            else if (user.FullName == "")
                            {
                                Assert.AreEqual("", user.UserID);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Assert.AreNotEqual(roleId, 1);
                        Assert.AreNotEqual(roleId, 2);
                        return;
                    }
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                Assert.IsNull(user);
                return;
            }
        }

        //Test FullName Is Empty
        [TestMethod()]
        public void AddUser_TestFullNameIsEmpty()
        {
            User user = new User()
            {
                UserID = "nguyennhhe141584@fpt.edu.vn",
                UserName = "nguyennhhe141584",
                FptEmail = "nguyennhhe141584@fpt.edu.vn",
                Avatar = @"https://lh3.googleusercontent.com/a/AEdFTp4ECSBbtM8UL4rXjXEn01_p-6Lqq3uq8dtvg2Cf=s96-c",
                FullName = "",
            };
            int roleId = 1;
            //int roleId = 3;
            try
            {
                if (user != null)
                {
                    if (roleId == 1 || roleId == 3)
                    {
                        if (user.UserID != "" && user.UserName != "" && user.FptEmail != ""
                            && user.Avatar != "" && user.FullName != "")
                        {
                            var repoMock = new Mock<IUserService>();
                            repoMock.Setup(repoMock => repoMock.AddUser(user, roleId)).Returns(0);
                            var userService = repoMock.Object;
                            var actual = userService.AddUser(user, roleId);
                            if (actual == 2)
                            {
                                Assert.AreEqual(2, actual);
                            }
                            else
                            {
                                Assert.AreEqual(0, actual);
                            }
                        }
                        else
                        {
                            if (user.UserID == "")
                            {
                                Assert.AreEqual("", user.UserID);
                                return;
                            }
                            else if (user.UserName == "")
                            {
                                Assert.AreEqual("", user.UserName);
                                return;
                            }
                            else if (user.FptEmail == "")
                            {
                                Assert.AreEqual("", user.FptEmail);
                                return;
                            }
                            else if (user.Avatar == "")
                            {
                                Assert.AreEqual("", user.Avatar);
                                return;
                            }
                            else if (user.FullName == "")
                            {
                                Assert.AreEqual("", user.FullName);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Assert.AreNotEqual(roleId, 1);
                        Assert.AreNotEqual(roleId, 2);
                        return;
                    }
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                Assert.IsNull(user);
                return;
            }
        }

        //Test Add User UnSuccess
        [TestMethod()]
        public void AddUser_TestAddUserUnSuccess()
        {
            User user = new User()
            {
                UserID = "nguyennhhe141584@fpt.edu.vn",
                UserName = "nguyennhhe141584",
                FptEmail = "nguyennhhe141584@fpt.edu.vn",
                Avatar = @"https://lh3.googleusercontent.com/a/AEdFTp4ECSBbtM8UL4rXjXEn01_p-6Lqq3uq8dtvg2Cf=s96-c",
                FullName = "Nguyễn Hoàng Nguyên",
            };
            int roleId = 1;
            //int roleId = 3;
            try
            {
                if (user != null)
                {
                    if (roleId == 1 || roleId == 3)
                    {
                        if (user.UserID != "" && user.UserName != "" && user.FptEmail != ""
                            && user.Avatar != "" && user.FullName != "")
                        {
                            var repoMock = new Mock<IUserService>();
                            repoMock.Setup(repoMock => repoMock.AddUser(user, roleId)).Returns(0);
                            var userService = repoMock.Object;
                            var actual = userService.AddUser(user, roleId);
                            if (actual == 2)
                            {
                                Assert.AreEqual(2, actual);
                            }
                            else
                            {
                                Assert.AreEqual(0, actual);
                            }
                        }
                        else
                        {
                            if (user.UserID == "")
                            {
                                Assert.AreEqual("", user.UserID);
                                return;
                            }
                            else if (user.UserName == "")
                            {
                                Assert.AreEqual("", user.UserName);
                                return;
                            }
                            else if (user.FptEmail == "")
                            {
                                Assert.AreEqual("", user.FptEmail);
                                return;
                            }
                            else if (user.Avatar == "")
                            {
                                Assert.AreEqual("", user.Avatar);
                                return;
                            }
                            else if (user.FullName == "")
                            {
                                Assert.AreEqual("", user.FullName);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Assert.AreNotEqual(roleId, 1);
                        Assert.AreNotEqual(roleId, 2);
                        return;
                    }
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                Assert.IsNull(user);
                return;
            }
        }

        //Test Add User Success
        [TestMethod()]
        public void AddUser_TestAddUserSuccess()
        {

            User user = new User()
            {
                UserID = "nguyennhhe141584@fpt.edu.vn",
                UserName = "nguyennhhe141584",
                FptEmail = "nguyennhhe141584@fpt.edu.vn",
                Avatar = @"https://lh3.googleusercontent.com/a/AEdFTp4ECSBbtM8UL4rXjXEn01_p-6Lqq3uq8dtvg2Cf=s96-c",
                FullName = "Nguyễn Hoàng Nguyên",
            };
            int roleId = 1;
            //int roleId = 3;
            try
            {
                if (user != null)
                {
                    if (roleId == 1 || roleId == 3)
                    {
                        if (user.UserID != "" && user.UserName != "" && user.FptEmail != ""
                            && user.Avatar != "" && user.FullName != "")
                        {
                            var repoMock = new Mock<IUserService>();
                            repoMock.Setup(repoMock => repoMock.AddUser(user, roleId)).Returns(2);
                            var userService = repoMock.Object;
                            var actual = userService.AddUser(user, roleId);
                            if (actual == 2)
                            {
                                Assert.AreEqual(2, actual);
                            }
                            else
                            {
                                Assert.AreEqual(0, actual);
                            }
                        }
                        else
                        {
                            if (user.UserID == "")
                            {
                                Assert.AreEqual("", user.UserID);
                                return;
                            }
                            else if (user.UserName == "")
                            {
                                Assert.AreEqual("", user.UserName);
                                return;
                            }
                            else if (user.FptEmail == "")
                            {
                                Assert.AreEqual("", user.FptEmail);
                                return;
                            }
                            else if (user.Avatar == "")
                            {
                                Assert.AreEqual("", user.Avatar);
                                return;
                            }
                            else if (user.FullName == "")
                            {
                                Assert.AreEqual("", user.FullName);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Assert.AreNotEqual(roleId, 1);
                        Assert.AreNotEqual(roleId, 2);
                        return;
                    }
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                Assert.IsNull(user);
                return;
            }
        }

        /// <summary>
        /// Test Function CheckProfileUserHaveAttributeIsNullByUserId
        /// </summary>
        /// 

        //test exception
        [TestMethod()]
        public void CheckProfileUserHaveAttributeIsNullByUserId_TestException()
        {
            string userId = null;
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = userService.CheckProfileUserHaveAttributeIsNullByUserId(userId);
                        if (actual == 0)
                        {
                            Assert.AreEqual(0, actual);
                            return;
                        }
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.IsNull(string.IsNullOrEmpty(userId));
                        return;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Assert.IsNull(userId);
                return;
            }
        }

        //Test UserId Is Empty
        [TestMethod()]
        public void CheckProfileUserHaveAttributeIsNullByUserId_TestUserIdIsEmpty()
        {
            string userId = "";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = userService.CheckProfileUserHaveAttributeIsNullByUserId(userId);
                        if (actual == 0)
                        {
                            Assert.AreEqual(0, actual);
                            return;
                        }
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.AreEqual("", userId);
                        return;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Assert.IsNull(userId);
                return;
            }
        }

        //Test UserId Is Invalid
        [TestMethod()]
        public void CheckProfileUserHaveAttributeIsNullByUserId_TestUserIdInValid()
        {
            string userId = "asdfasdf@fpt.edu.vn";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = userService.CheckProfileUserHaveAttributeIsNullByUserId(userId);
                        if (actual == 0)
                        {
                            Assert.AreEqual(0, actual);
                            return;
                        }
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.AreEqual("", userId);
                        return;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Assert.IsNull(userId);
                return;
            }
        }

        //Test UserId Is Valid
        [TestMethod()]
        public void CheckProfileUserHaveAttributeIsNullByUserId_TestUserIdIsValid()
        {
            string userId = "nguyennhhe141584@fpt.edu.vn";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = userService.CheckProfileUserHaveAttributeIsNullByUserId(userId);
                        if (actual == 0)
                        {
                            Assert.AreEqual(0, actual);
                            return;
                        }
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.AreEqual("", userId);
                        return;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Assert.IsNull(userId);
                return;
            }
        }


        /// <summary>
        /// Test Function CheckRoleOfUser
        /// </summary>
        /// 

        //Test Exception
        [TestMethod()]
        public void CheckRoleOfUser_TestException()
        {
            string userId = null;
            string role = null;
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && role != null)
                {
                    if (userId != "" && role != "")
                    {
                        var actual = userService.CheckRoleOfUser(userId, role);
                        if (actual == true)
                        {
                            Assert.IsTrue(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsFalse(actual);
                            return;
                        }
                    }
                    else
                    {
                        if (userId == "")
                        {
                            Assert.AreEqual("", userId);
                            return;
                        }
                        if (role == "")
                        {
                            Assert.AreEqual("", role);
                            return;
                        }

                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }
        //Test UserId Is empty
        [TestMethod()]
        public void CheckRoleOfUser_TestUserIdIsEmpty()
        {
            string userId = "";
            string role = "";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && role != null)
                {
                    if (userId != "" && role != "")
                    {
                        var actual = userService.CheckRoleOfUser(userId, role);
                        if (actual == true)
                        {
                            Assert.IsTrue(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsFalse(actual);
                            return;
                        }
                    }
                    else
                    {
                        if (userId == "")
                        {
                            Assert.AreEqual("", userId);
                            return;
                        }
                        if (role == "")
                        {
                            Assert.AreEqual("", role);
                            return;
                        }

                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        //Test role Is empty
        [TestMethod()]
        public void CheckRoleOfUser_TestRoleIsEmpty()
        {
            string userId = "nguyennhhe141584@fpt.edu.vn";
            string role = "";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && role != null)
                {
                    if (userId != "" && role != "")
                    {
                        var actual = userService.CheckRoleOfUser(userId, role);
                        if (actual == true)
                        {
                            Assert.IsTrue(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsFalse(actual);
                            return;
                        }
                    }
                    else
                    {
                        if (userId == "")
                        {
                            Assert.AreEqual("", userId);
                            return;
                        }
                        if (role == "")
                        {
                            Assert.AreEqual("", role);
                            return;
                        }

                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        //Test function Is invalid
        [TestMethod()]
        public void CheckRoleOfUser_TestFunctionIsInvalid()
        {
            string userId = "nguyennhhe145434@fpt.edu.vn";
            string role = "Student";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && role != null)
                {
                    if (userId != "" && role != "")
                    {
                        var actual = userService.CheckRoleOfUser(userId, role);
                        if (actual == true)
                        {
                            Assert.IsTrue(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsFalse(actual);
                            return;
                        }
                    }
                    else
                    {
                        if (userId == "")
                        {
                            Assert.AreEqual("", userId);
                            return;
                        }
                        if (role == "")
                        {
                            Assert.AreEqual("", role);
                            return;
                        }

                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        //Test function Is valid
        [TestMethod()]
        public void CheckRoleOfUser_TestFunctionIsValid()
        {
            string userId = "nguyennhhe141584@fpt.edu.vn";
            string role = "Student";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && role != null)
                {
                    if (userId != "" && role != "")
                    {
                        var actual = userService.CheckRoleOfUser(userId, role);
                        if (actual == true)
                        {
                            Assert.IsTrue(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsFalse(actual);
                            return;
                        }
                    }
                    else
                    {
                        if (userId == "")
                        {
                            Assert.AreEqual("", userId);
                            return;
                        }
                        if (role == "")
                        {
                            Assert.AreEqual("", role);
                            return;
                        }

                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        /// <summary>
        /// Test Function GetUserByID
        /// </summary>
        /// 

        //Test Exception
        [TestMethod()]
        public void GetUserByID_TestException()
        {
            string userId = null;
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = userService.GetUserByID(userId);
                        if (actual != null)
                        {
                            Assert.IsNotNull(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsNull(actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.AreEqual("", userId);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        //Test UserId Is Empty
        [TestMethod()]
        public void GetUserByID_TestUserIdIsEmpty()
        {
            string userId = "";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = userService.GetUserByID(userId);
                        if (actual != null)
                        {
                            Assert.IsNotNull(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsNull(actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.AreEqual("", userId);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        //Test function invalid
        [TestMethod()]
        public void GetUserByID_TestFunctionInvalid()
        {
            string userId = "nguyennhhe144324@fpt.edu.vn";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = userService.GetUserByID(userId);
                        if (actual != null)
                        {
                            Assert.IsNotNull(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsNull(actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.AreEqual("", userId);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        //Test function valid
        [TestMethod()]
        public void GetUserByID_TestFunctionValid()
        {
            string userId = "nguyennhhe141584@fpt.edu.vn";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = userService.GetUserByID(userId);
                        if (actual != null)
                        {
                            Assert.IsNotNull(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsNull(actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.AreEqual("", userId);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }


        /// <summary>
        /// Test Function UpdateAvatar
        /// </summary>

        //Test exception
        [TestMethod()]
        public void UpdateAvatar_TestException()
        {
            string avatar = null;
            string userId = null;
            try
            {
                if (userId != null && avatar != null)
                {
                    if (userId != "" || avatar != "")
                    {
                        var repoMock = new Mock<IUserService>();
                        repoMock.Setup(repoMock => repoMock.UpdateAvatar(avatar, userId)).Returns(0);
                        var userService = repoMock.Object;
                        var actual = userService.UpdateAvatar(avatar, userId);
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                        }
                        else
                        {
                            Assert.AreEqual(0, actual);
                        }
                    }
                    else
                    {
                        if (userId == "")
                        {
                            Assert.AreEqual("", userId);
                            return;
                        }
                        if (avatar == "")
                        {
                            Assert.AreEqual("", avatar);
                            return;
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                if (userId == null)
                {
                    Assert.IsNull(userId);
                    return;
                }
                if (avatar == null)
                {
                    Assert.IsNull(avatar);
                    return;
                }
            }
        }

        //Test userid or avatar is empty
        [TestMethod()]
        public void UpdateAvatar_TestUserIdOrAvatarIsEmpty()
        {
            string avatar = "";
            string userId = "";
            try
            {
                if (userId != null && avatar != null)
                {
                    if (userId != "" || avatar != "")
                    {
                        var repoMock = new Mock<IUserService>();
                        repoMock.Setup(repoMock => repoMock.UpdateAvatar(avatar, userId)).Returns(0);
                        var userService = repoMock.Object;
                        var actual = userService.UpdateAvatar(avatar, userId);
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                        }
                        else
                        {
                            Assert.AreEqual(0, actual);
                        }
                    }
                    else
                    {
                        if (userId == "")
                        {
                            Assert.AreEqual("", userId);
                            return;
                        }
                        if (avatar == "")
                        {
                            Assert.AreEqual("", avatar);
                            return;
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                if (userId == null)
                {
                    Assert.IsNull(userId);
                    return;
                }
                if (avatar == null)
                {
                    Assert.IsNull(avatar);
                    return;
                }
            }
        }


        //Test function update avatar InValid
        [TestMethod()]
        public void UpdateAvatar_TestFunctionUpdateAvatarInValid()
        {
            string avatar = @"https://lh3.googleusercontent.com/a/AEdFTp4ECSBbtM8UL4rXjXEn01_p-6Lqq3uq8dtvg2Cf=s96-c";
            string userId = "nguyennhhe141324@fpt.edu.vn";
            try
            {
                if (userId != null && avatar != null)
                {
                    if (userId != "" || avatar != "")
                    {
                        var repoMock = new Mock<IUserService>();
                        repoMock.Setup(repoMock => repoMock.UpdateAvatar(avatar, userId)).Returns(0);
                        var userService = repoMock.Object;
                        var actual = userService.UpdateAvatar(avatar, userId);
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                        }
                        else
                        {
                            Assert.AreEqual(0, actual);
                        }
                    }
                    else
                    {
                        if (userId == "")
                        {
                            Assert.AreEqual("", userId);
                            return;
                        }
                        if (avatar == "")
                        {
                            Assert.AreEqual("", avatar);
                            return;
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                if (userId == null)
                {
                    Assert.IsNull(userId);
                    return;
                }
                if (avatar == null)
                {
                    Assert.IsNull(avatar);
                    return;
                }
            }
        }

        //Test function update avatar valid
        [TestMethod()]
        public void UpdateAvatar_TestFunctionUpdateAvatarValid()
        {
            string avatar = @"https://lh3.googleusercontent.com/a/AEdFTp4ECSBbtM8UL4rXjXEn01_p-6Lqq3uq8dtvg2Cf=s96-c";
            string userId = "nguyennhhe141584@fpt.edu.vn";
            try
            {
                if (userId != null && avatar != null)
                {
                    if (userId != "" || avatar != "")
                    {
                        var repoMock = new Mock<IUserService>();
                        repoMock.Setup(repoMock => repoMock.UpdateAvatar(avatar, userId)).Returns(1);
                        var userService = repoMock.Object;
                        var actual = userService.UpdateAvatar(avatar, userId);
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                        }
                        else
                        {
                            Assert.AreEqual(0, actual);
                        }
                    }
                    else
                    {
                        if (userId == "")
                        {
                            Assert.AreEqual("", userId);
                            return;
                        }
                        if (avatar == "")
                        {
                            Assert.AreEqual("", avatar);
                            return;
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                if (userId == null)
                {
                    Assert.IsNull(userId);
                    return;
                }
                if (avatar == null)
                {
                    Assert.IsNull(avatar);
                    return;
                }
            }
        }


        /// <summary>
        /// Test Function GetListFptEmailByGroupIdeaId
        /// </summary>


        // test exception
        [TestMethod()]
        public void GetListFptEmailByGroupIdeaId_TestException()
        {
            int groupIdeaId = 0;
            var exception = "Error Message: Test Exception";
            try
            {
                if (groupIdeaId != 0)
                {
                    var actual = userService.GetListFptEmailByGroupIdeaId(groupIdeaId);
                    if (actual != null)
                    {
                        Assert.IsNotNull(actual);
                        return;
                    }
                    else
                    {
                        Assert.IsNull(actual);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        // Test GroupIdeaId Is Negative Number
        [TestMethod()]
        public void GetListFptEmailByGroupIdeaId_TestGroupIdeaIdIsNegativeNumber()
        {
            int groupIdeaId = -456;
            var exception = "Error Message: Test Exception";
            try
            {
                if (groupIdeaId >= 0)
                {
                    var actual = userService.GetListFptEmailByGroupIdeaId(groupIdeaId);
                    if (actual != null)
                    {
                        Assert.IsNotNull(actual);
                        return;
                    }
                    else
                    {
                        Assert.IsNull(actual);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        // Test Function Excuted Null
        [TestMethod()]
        public void GetListFptEmailByGroupIdeaId_TestFunctionExcuteNull()
        {
            int groupIdeaId = 43534;
            var exception = "Error Message: Test Exception";
            try
            {
                if (groupIdeaId >= 0)
                {
                    var actual = userService.GetListFptEmailByGroupIdeaId(groupIdeaId);
                    if (actual != null)
                    {
                        Assert.IsNotNull(actual);
                        return;
                    }
                    else
                    {
                        Assert.IsNull(actual);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        // Test Function Excuted Null
        [TestMethod()]
        public void GetListFptEmailByGroupIdeaId_TestFunctionExcuteNotNull()
        {
            int groupIdeaId = 4;
            var exception = "Error Message: Test Exception";
            try
            {
                if (groupIdeaId >= 0)
                {
                    var actual = userService.GetListFptEmailByGroupIdeaId(groupIdeaId);
                    if (actual != null)
                    {
                        Assert.IsNotNull(actual);
                        return;
                    }
                    else
                    {
                        Assert.IsNull(actual);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }


        /// <summary>
        /// Test Function GetNameStudentByUserId
        /// </summary>

        //Test Exception
        [TestMethod()]
        public void GetNameStudentByUserId_TestException()
        {
            string userId = null;
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = userService.GetNameStudentByUserId(userId);
                        if (actual != null)
                        {
                            Assert.IsNotNull(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsNull(actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.AreEqual("", userId);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        //Test UserId Is Empty
        [TestMethod()]
        public void GetNameStudentByUserId_TestUserIdIsEmpty()
        {
            string userId = "";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = userService.GetNameStudentByUserId(userId);
                        if (actual != null)
                        {
                            Assert.IsNotNull(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsNull(actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.AreEqual("", userId);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        //Test function invalid
        [TestMethod()]
        public void GetNameStudentByUserId_TestFunctionInvalid()
        {
            string userId = "nguyennhhe144324@fpt.edu.vn";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = userService.GetNameStudentByUserId(userId);
                        if (actual != null)
                        {
                            Assert.IsNotNull(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsNull(actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.AreEqual("", userId);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        //Test function valid
        [TestMethod()]
        public void GetNameStudentByUserId_TestFunctionValid()
        {
            string userId = "nguyennhhe141584@fpt.edu.vn";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = userService.GetNameStudentByUserId(userId);
                        if (actual != null)
                        {
                            Assert.IsNotNull(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsNull(actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.AreEqual("", userId);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }


        /// <summary>
        /// Test Function GetUserByFptEmail
        /// </summary>

        //Test Exception
        [TestMethod()]
        public void GetUserByFptEmail_TestException()
        {
            string fptEmail = null;
            var exception = "Error Message: Test Exception";
            try
            {
                if (fptEmail != null)
                {
                    if (fptEmail != "")
                    {
                        var actual = userService.GetUserByFptEmail(fptEmail);
                        if (actual != null)
                        {
                            Assert.IsNotNull(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsNull(actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.AreEqual("", fptEmail);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        //Test UserId Is Empty
        [TestMethod()]
        public void GetUserByFptEmail_TestUserIdIsEmpty()
        {
            string fptEmail = "";
            var exception = "Error Message: Test Exception";
            try
            {
                if (fptEmail != null)
                {
                    if (fptEmail != "")
                    {
                        var actual = userService.GetUserByFptEmail(fptEmail);
                        if (actual != null)
                        {
                            Assert.IsNotNull(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsNull(actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.AreEqual("", fptEmail);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        //Test function invalid
        [TestMethod()]
        public void GetUserByFptEmail_TestFunctionInvalid()
        {
            string fptEmail = "nguyennhhe144324@fpt.edu.vn";
            var exception = "Error Message: Test Exception";
            try
            {
                if (fptEmail != null)
                {
                    if (fptEmail != "")
                    {
                        var actual = userService.GetUserByFptEmail(fptEmail);
                        if (actual != null)
                        {
                            Assert.IsNotNull(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsNull(actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.AreEqual("", fptEmail);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }

        //Test function valid
        [TestMethod()]
        public void GetUserByFptEmail_TestFunctionValid()
        {
            string fptEmail = "nguyennhhe141584@fpt.edu.vn";
            var exception = "Error Message: Test Exception";
            try
            {
                if (fptEmail != null)
                {
                    if (fptEmail != "")
                    {
                        var actual = userService.GetUserByFptEmail(fptEmail);
                        if (actual != null)
                        {
                            Assert.IsNotNull(actual);
                            return;
                        }
                        else
                        {
                            Assert.IsNull(actual);
                            return;
                        }
                    }
                    else
                    {
                        Assert.AreEqual("", fptEmail);
                        return;
                    }
                }
                else
                {
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception, ex.Message);
                return;
            }
        }
    }
}
