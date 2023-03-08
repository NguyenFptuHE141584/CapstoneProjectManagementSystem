using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneProjectManagementSystem.Services.Implement.StaffImplement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using CapstoneProjectManagementSystem.Services.StaffServices;
using CapstoneProjectManagementSystem.Models;

namespace CapstoneProjectManagementSystem.Services.Implement.StaffImplement.Tests
{
    [TestClass()]
    public class StaffServiceTests
    {
        /// <summary>
        /// Test function GetUserIsStaffByRoleIdTest()
        /// </summary>
        [TestMethod()]
        public void GetUserIsStaffByRoleIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStaffService), typeof(StaffService));
            var staffService = container.Resolve<StaffService>();

            var roleId = 3;
            var actual = staffService.GetUserIsStaffByRoleId(roleId);

            var staff = new Staff()
            {
                StaffID = "HaDTT39@fpt.edu.vn",
                User = new User()
                {
                    FptEmail = "HaDTT39@fpt.edu.vn"
                }
            };
            Assert.AreEqual(staff.StaffID, actual.StaffID);
            Assert.AreEqual(staff.User.FptEmail, actual.User.FptEmail);
        }
    }
}