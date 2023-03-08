using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneProjectManagementSystem.Services.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using CapstoneProjectManagementSystem.Models;
using Moq;

namespace CapstoneProjectManagementSystem.Services.Implement.Tests
{
    [TestClass()]
    public class SemesterServiceTests
    {
        /// <summary>
        /// Test function GetCurrentSemester()
        /// </summary>
        [TestMethod()]
        public void GetCurrentSemesterTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISemesterService), typeof(SemesterService));
            var semesterService = container.Resolve<SemesterService>();

            var actual = semesterService.GetCurrentSemester();

            var semester = new Semester()
            {
                SemesterID = 1,
                SemesterCode = "SP_23",
                SemesterName = "Spring 2023"
            };
            Assert.AreEqual(semester.SemesterID, actual.SemesterID);
            Assert.AreEqual(semester.SemesterCode, actual.SemesterCode);
            Assert.AreEqual(semester.SemesterName, actual.SemesterName);
        }

        /// <summary>
        /// Test function GetSemesterByIdTest()
        /// </summary>
        // Test with exist semesterId
        [TestMethod()]
        public void GetSemesterByIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISemesterService), typeof(SemesterService));
            var semesterService = container.Resolve<SemesterService>();

            var semesterId = 1;
            var actual = semesterService.GetSemesterById(semesterId);

            var semester = new Semester()
            {
                SemesterID = 1,
                SemesterCode = "SP_23",
                SemesterName = "Spring 2023"
            };
            Assert.AreEqual(semester.SemesterID, actual.SemesterID);
            Assert.AreEqual(semester.SemesterCode, actual.SemesterCode);
            Assert.AreEqual(semester.SemesterName, actual.SemesterName);
        }
        // Test with not exist semesterId
        [TestMethod()]
        public void GetSemesterByIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISemesterService), typeof(SemesterService));
            var semesterService = container.Resolve<SemesterService>();

            var semesterId = 2;
            var actual = semesterService.GetSemesterById(semesterId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetAllSemesterTest()
        /// </summary>
        [TestMethod()]
        public void GetAllSemesterTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISemesterService), typeof(SemesterService));
            var semesterService = container.Resolve<SemesterService>();

            var actual = semesterService.GetAllSemester();

            var semester = new Semester()
            {
                SemesterID = 1,
                SemesterCode = "SP_23",
                SemesterName = "Spring 2023"
            };
            var semesterList = new List<Semester>();
            semesterList.Add(semester);
            Assert.AreEqual(semesterList[0].SemesterID, actual[0].SemesterID);
            Assert.AreEqual(semesterList[0].SemesterCode, actual[0].SemesterCode);
            Assert.AreEqual(semesterList[0].SemesterName, actual[0].SemesterName);
        }

        /// <summary>
        /// Test function GetLastSemesterTest()
        /// </summary>
        [TestMethod()]
        public void GetLastSemesterTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISemesterService), typeof(SemesterService));
            var semesterService = container.Resolve<SemesterService>();

            var actual = semesterService.GetLastSemester();

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function UpdateCurrentSemesterTest()
        /// </summary>
        // wrong semesterId
        [TestMethod()]
        public void UpdateCurrentSemesterTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISemesterService), typeof(SemesterService));
            var semesterService = container.Resolve<SemesterService>();

            var newSemester = new Semester()
            {
                SemesterID = 2,
                SemesterCode = "SP_23_new",
                SemesterName = "Spring 2023 new",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,

            };
            var actual = semesterService.UpdateCurrentSemester(newSemester);

            Assert.AreEqual(0, actual);
        }
        // correct semesterId
        [TestMethod()]
        public void UpdateCurrentSemesterTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISemesterService), typeof(SemesterService));
            var semesterService = container.Resolve<SemesterService>();

            var newSemester = new Semester()
            {
                SemesterID = 1,
                SemesterCode = "SP_23_new",
                SemesterName = "Spring 2023 new",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,

            };
            var actual = semesterService.UpdateCurrentSemester(newSemester);
            var oldSemester = new Semester()
            {
                SemesterID = 1,
                SemesterCode = "SP_23",
                SemesterName = "Spring 2023",
                StartTime = new DateTime(2022, 12, 08),
                EndTime = new DateTime(2023, 03, 30),

            };
            semesterService.UpdateCurrentSemester(oldSemester);

            Assert.AreEqual(1, actual);           
        }

        /// <summary>
        /// Test function ChangeShowGroupNameStatusTest()
        /// </summary>
        // change status to false, wrong semesterId
        [TestMethod()]
        public void ChangeShowGroupNameStatusTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISemesterService), typeof(SemesterService));
            var semesterService = container.Resolve<SemesterService>();

            var semesterId = 2;
            var status = 0;
            var actual = semesterService.ChangeShowGroupNameStatus(semesterId, status);

            Assert.AreEqual(0, actual);
        }
        // change status to false, correct semesterId
        [TestMethod()]
        public void ChangeShowGroupNameStatusTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISemesterService), typeof(SemesterService));
            var semesterService = container.Resolve<SemesterService>();

            var semesterId = 1;
            var status = 0;
            var actual = semesterService.ChangeShowGroupNameStatus(semesterId,status);

            Assert.AreEqual(1, actual);
        }
        // change status to true, wrong semesterId
        [TestMethod()]
        public void ChangeShowGroupNameStatusTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISemesterService), typeof(SemesterService));
            var semesterService = container.Resolve<SemesterService>();

            var semesterId = 2;
            var status = 1;
            var actual = semesterService.ChangeShowGroupNameStatus(semesterId, status);

            Assert.AreEqual(0, actual);
        }
        // change status to true, correct semesterId
        [TestMethod()]
        public void ChangeShowGroupNameStatusTest_4()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISemesterService), typeof(SemesterService));
            var semesterService = container.Resolve<SemesterService>();

            var semesterId = 1;
            var status = 1;
            var actual = semesterService.ChangeShowGroupNameStatus(semesterId, status);

            Assert.AreEqual(1, actual);
            semesterService.ChangeShowGroupNameStatus(semesterId,0);
        }



       // //demo
       //[TestMethod()]
       // public void AddUser_TestException()
       // {
       //     User user = null;
       //     int roleId = 0;
       //     try
       //     {
       //         if (user != null)
       //         {
       //             if (roleId != 1 || roleId != 2)
       //             {
       //                 if (user.UserID != "" && user.UserName != "" && user.FptEmail != ""
       //                     && user.Avatar != "" && user.FullName != "")
       //                 {
       //                     var repoMock = new Mock<IUserService>();
       //                     repoMock.Setup(repoMock => repoMock.AddUser(user, roleId)).Returns(0);
       //                     var userService = repoMock.Object;
       //                     var actual = userService.AddUser(user, roleId);
       //                     if (actual == 2)
       //                     {
       //                         Assert.AreEqual(2, actual);
       //                     }
       //                     else
       //                     {
       //                         Assert.AreEqual(0, actual);
       //                     }
       //                 }
       //                 else
       //                 {
       //                     if (user.UserID == "")
       //                     {
       //                         Assert.AreEqual("", user.UserID);
       //                         return;
       //                     }
       //                     else if (user.UserName == "")
       //                     {
       //                         Assert.AreEqual("", user.UserName);
       //                         return;
       //                     }
       //                     else if (user.FptEmail == "")
       //                     {
       //                         Assert.AreEqual("", user.FptEmail);
       //                         return;
       //                     }
       //                     else if (user.Avatar == "")
       //                     {
       //                         Assert.AreEqual("", user.Avatar);
       //                         return;
       //                     }
       //                     else if (user.FullName == "")
       //                     {
       //                         Assert.AreEqual("", user.UserID);
       //                         return;
       //                     }
       //                 }
       //             }
       //             else
       //             {
       //                 Assert.AreNotEqual(roleId, 1);
       //                 Assert.AreNotEqual(roleId, 2);
       //                 return;
       //             }
       //         }
       //         else
       //         {
       //             throw new Exception();
       //         }

       //     }
       //     catch (Exception)
       //     {
       //         Assert.IsNull(user);
       //         return;
       //     }
       // }
    }
}