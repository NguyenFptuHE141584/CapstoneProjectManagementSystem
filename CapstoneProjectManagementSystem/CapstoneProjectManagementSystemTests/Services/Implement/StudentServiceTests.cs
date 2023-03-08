using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneProjectManagementSystem.Services.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using CapstoneProjectManagementSystem.Models;

namespace CapstoneProjectManagementSystem.Services.Implement.Tests
{
    [TestClass()]
    public class StudentServiceTests
    {
        /// <summary>
        /// Test function GetStudentByFptEmailTest()
        /// </summary>
        // exist student and semesterId
        [TestMethod()]
        public void GetStudentByFptEmailTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var fptEmail = "anbthe140005@fpt.edu.vn";
            var semesterId = 1;
            var actual = studentService.GetStudentByFptEmail(fptEmail, semesterId);

            var student = new Student()
            {
                StudentID = "anbthe140005@fpt.edu.vn",
            };
            Assert.AreEqual(student.StudentID, actual.StudentID);
        }
        // not exist student
        [TestMethod()]
        public void GetStudentByFptEmailTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var fptEmail = "aaa140005@fpt.edu.vn";
            var semesterId = 1;
            var actual = studentService.GetStudentByFptEmail(fptEmail, semesterId);

            Assert.IsNull(actual);
        }
        // not exist semester id
        [TestMethod()]
        public void GetStudentByFptEmailTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var fptEmail = "anbthe140005@fpt.edu.vn";
            var semesterId = 0;
            var actual = studentService.GetStudentByFptEmail(fptEmail, semesterId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetStudentByStudentIdTest()
        /// </summary>
        // exist student
        [TestMethod()]
        public void GetStudentByStudentIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var studentId = "anbthe140005@fpt.edu.vn";
            var actual = studentService.GetStudentByStudentId(studentId);

            var student = new Student()
            {
                StudentID = "anbthe140005@fpt.edu.vn",
            };
            Assert.AreEqual(student.StudentID, actual.StudentID);
        }
        // not exist student
        [TestMethod()]
        public void GetStudentByStudentIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var studentId = "aaa140005@fpt.edu.vn";
            var actual = studentService.GetStudentByStudentId(studentId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetStudentSearchListTest()
        /// </summary>
        // search for all profession and specialty , no limit
        [TestMethod()]
        public void GetStudentSearchListTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var semesterId = 1;
            var professionId = 0;
            var specialtyId = 0;
            var offset = 0;
            var fetch = int.MaxValue;
            var actual = studentService.GetStudentSearchList(semesterId, professionId, specialtyId, offset, fetch);
            Assert.AreEqual(126, actual.Count);
        }
        // search first 10 records for all profession and specialty 
        [TestMethod()]
        public void GetStudentSearchListTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var semesterId = 1;
            var professionId = 0;
            var specialtyId = 0;
            var offset = 0;
            var fetch = 10;
            var actual = studentService.GetStudentSearchList(semesterId, professionId, specialtyId, offset, fetch);

            Assert.AreEqual(10, actual.Count);
        }
        // search for profession = 1 and all specialty 
        [TestMethod()]
        public void GetStudentSearchListTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 0;
            var offset = 0;
            var fetch = int.MaxValue;
            var actual = studentService.GetStudentSearchList(semesterId, professionId, specialtyId, offset, fetch);

            Assert.AreEqual(51, actual.Count);
        }
        // search first 10 records for profession = 1 and all specialty 
        [TestMethod()]
        public void GetStudentSearchListTest_4()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 0;
            var offset = 0;
            var fetch = 10;
            var actual = studentService.GetStudentSearchList(semesterId, professionId, specialtyId, offset, fetch);

            Assert.AreEqual(10, actual.Count);
        }
        // search for profession = 1 and specialty = 1  
        [TestMethod()]
        public void GetStudentSearchListTest_5()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var offset = 0;
            var fetch = int.MaxValue;
            var actual = studentService.GetStudentSearchList(semesterId, professionId, specialtyId, offset, fetch);

            Assert.AreEqual(8, actual.Count);
        }
        // search first 10 records for profession = 1 and specialty = 1  
        [TestMethod()]
        public void GetStudentSearchListTest_6()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var offset = 0;
            var fetch = 10;
            var actual = studentService.GetStudentSearchList(semesterId, professionId, specialtyId, offset, fetch);

            Assert.AreEqual(8, actual.Count);
        }
        // search with wrong semester id
        [TestMethod()]
        public void GetStudentSearchListTest_7()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var semesterId = 0;
            var professionId = 0;
            var specialtyId = 0;
            var offset = 0;
            var fetch = int.MaxValue;
            var actual = studentService.GetStudentSearchList(semesterId, professionId, specialtyId, offset, fetch);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function getLeaderByFinalGroupIdTest()
        /// </summary>
        [TestMethod()]
        public void getLeaderByFinalGroupIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var finalgroupId = 1;
            var actual = studentService.getLeaderByFinalGroupId(finalgroupId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function getListMemberByFinalGroupIdTest()
        /// </summary>
        [TestMethod()]
        public void getListMemberByFinalGroupIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var finalgroupId = 1;
            var actual = studentService.getListMemberByFinalGroupId(finalgroupId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function getListStudentNotHaveGroupBySpecialtyIdTest()
        /// </summary>
        // semester id and specialty id exist
        [TestMethod()]
        public void getListStudentNotHaveGroupBySpecialtyIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var semesterId = 1;
            var specialtyId = 1;
            var actual = studentService.getListStudentNotHaveGroupBySpecialtyId(semesterId, specialtyId);

            Assert.AreEqual(8, actual.Count);
        }
        // specialty id not exist 
        [TestMethod()]
        public void getListStudentNotHaveGroupBySpecialtyIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var semesterId = 1;
            var specialtyId = 0;
            var actual = studentService.getListStudentNotHaveGroupBySpecialtyId(semesterId, specialtyId);

            Assert.IsNull(actual);
        }
        // semester id not exist 
        [TestMethod()]
        public void getListStudentNotHaveGroupBySpecialtyIdTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var semesterId = 0;
            var specialtyId = 1;
            var actual = studentService.getListStudentNotHaveGroupBySpecialtyId(semesterId, specialtyId);

            Assert.IsNull(actual);
        }
        // semester id and specialty id not exist 
        [TestMethod()]
        public void getListStudentNotHaveGroupBySpecialtyIdTest_4()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var semesterId = 0;
            var specialtyId = 0;
            var actual = studentService.getListStudentNotHaveGroupBySpecialtyId(semesterId, specialtyId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetProfileOfStudentByUserIdTest()
        /// </summary>
        // user exist
        [TestMethod()]
        public void GetProfileOfStudentByUserIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var userId = "anbthe140005@fpt.edu.vn";
            var actual = studentService.GetProfileOfStudentByUserId(userId);

            var student = new Student()
            {
                StudentID = "anbthe140005@fpt.edu.vn",
            };
            Assert.AreEqual(student.StudentID, actual.StudentID);
        }
        // user not exist
        [TestMethod()]
        public void GetProfileOfStudentByUserIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var userId = "aaa140005@fpt.edu.vn";
            var actual = studentService.GetProfileOfStudentByUserId(userId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function UpdateMajorOfStudentByUserIdTest()
        /// </summary>
        // student and profession id and specialty id exist
        [TestMethod()]
        public void UpdateMajorOfStudentByUserIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var userId = "anbthe140005@fpt.edu.vn";
            var professionId = 2;
            var specialtyId = 13;
            var actual = studentService.UpdateMajorOfStudentByUserId(userId, professionId, specialtyId);
            studentService.UpdateMajorOfStudentByUserId(userId, 1, 3);
            Assert.AreEqual(1, actual);
        }
        // student not exist
        [TestMethod()]
        public void UpdateMajorOfStudentByUserIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var userId = "aaa@fpt.edu.vn";
            var professionId = 1;
            var specialtyId = 1;
            var actual = studentService.UpdateMajorOfStudentByUserId(userId, professionId, specialtyId);

            Assert.AreEqual(0, actual);
        }
        // profession id not exist
        [TestMethod()]
        public void UpdateMajorOfStudentByUserIdTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var userId = "anbthe140005@fpt.edu.vn";
            var professionId = 0;
            var specialtyId = 1;
            var actual = studentService.UpdateMajorOfStudentByUserId(userId, professionId, specialtyId);

            Assert.AreEqual(0, actual);
        }
        // specialty id not exist
        [TestMethod()]
        public void UpdateMajorOfStudentByUserIdTest_4()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var userId = "anbthe140005@fpt.edu.vn";
            var professionId = 1;
            var specialtyId = 0;
            var actual = studentService.UpdateMajorOfStudentByUserId(userId, professionId, specialtyId);

            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test function UpdateSemesterOfStudentByUserIdTest()
        /// </summary>
        // user exist
        [TestMethod()]
        public void UpdateSemesterOfStudentByUserIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var userId = "anbthe140005@fpt.edu.vn";
            var actual = studentService.UpdateSemesterOfStudentByUserId(userId);

            Assert.AreEqual(1, actual);
        }
        // user not exist
        [TestMethod()]
        public void UpdateSemesterOfStudentByUserIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var userId = "aaa140005@fpt.edu.vn";
            var actual = studentService.UpdateSemesterOfStudentByUserId(userId);

            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test function GetProfessionIdOfStudentByUserIdTest()
        /// </summary>
        // user exist
        [TestMethod()]
        public void GetProfessionIdOfStudentByUserIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var userId = "anbthe140005@fpt.edu.vn";
            var actual = studentService.GetProfessionIdOfStudentByUserId(userId);

            var professionId = 1;
            Assert.AreEqual(professionId, actual);
        }
        // user not exist
        [TestMethod()]
        public void GetProfessionIdOfStudentByUserIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var userId = "aaa140005@fpt.edu.vn";
            var actual = studentService.GetProfessionIdOfStudentByUserId(userId);

            var professionId = 0;
            Assert.AreEqual(professionId, actual);
        }

        /// <summary>
        /// Test function GetSpecialtyIdOfStudentByUserIdTest()
        /// </summary>
        // user exist
        [TestMethod()]
        public void GetSpecialtyIdOfStudentByUserIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var userId = "anbthe140005@fpt.edu.vn";
            var actual = studentService.GetSpecialtyIdOfStudentByUserId(userId);

            var specialtyId = 3;
            Assert.AreEqual(specialtyId, actual);
        }
        // user not exist
        [TestMethod()]
        public void GetSpecialtyIdOfStudentByUserIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var userId = "aaa140005@fpt.edu.vn";
            var actual = studentService.GetSpecialtyIdOfStudentByUserId(userId);

            var specialtyId = 0;
            Assert.AreEqual(specialtyId, actual);
        }

        /// <summary>
        /// Test function GetProfessionAndSpecialtyByStudentIdTest()
        /// </summary>
        // user exist
        [TestMethod()]
        public void GetProfessionAndSpecialtyByStudentIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var userId = "anbthe140005@fpt.edu.vn";
            var actual = studentService.GetProfessionAndSpecialtyByStudentId(userId);

            var student = new Student()
            {
                Profession = new Profession()
                {
                    ProfessionID = 1
                },
                Specialty = new Specialty()
                {
                    SpecialtyID = 3
                }
            };
            Assert.AreEqual(student.Profession.ProfessionID, actual.Profession.ProfessionID);
            Assert.AreEqual(student.Specialty.SpecialtyID, actual.Specialty.SpecialtyID);
        }
        // user not exist
        [TestMethod()]
        public void GetProfessionAndSpecialtyByStudentIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var userId = "aaa140005@fpt.edu.vn";
            var actual = studentService.GetProfessionAndSpecialtyByStudentId(userId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetStudentNotHaveGroupFinalByFptEmailTest()
        /// </summary>
        // user and semester id exist
        [TestMethod()]
        public void GetStudentNotHaveGroupFinalByFptEmailTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var fptEmail = "anbthe140005@fpt.edu.vn";
            var semesterId = 1;
            var actual = studentService.GetStudentNotHaveGroupFinalByFptEmail(fptEmail, semesterId);

            var student = new Student()
            {
                StudentID = "anbthe140005@fpt.edu.vn"
            };
            Assert.AreEqual(student.StudentID, actual.StudentID);
        }
        // user not exist
        [TestMethod()]
        public void GetStudentNotHaveGroupFinalByFptEmailTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var fptEmail = "aaa@fpt.edu.vn";
            var semesterId = 1;
            var actual = studentService.GetStudentNotHaveGroupFinalByFptEmail(fptEmail, semesterId);

            Assert.IsNull(actual);
        }
        // semester id not exist
        [TestMethod()]
        public void GetStudentNotHaveGroupFinalByFptEmailTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var fptEmail = "anbthe140005@fpt.edu.vn";
            var semesterId = 0;
            var actual = studentService.GetStudentNotHaveGroupFinalByFptEmail(fptEmail, semesterId);
            Assert.IsNull(actual);
        }
        // user and semester id not exist
        [TestMethod()]
        public void GetStudentNotHaveGroupFinalByFptEmailTest_4()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var fptEmail = "aaa140005@fpt.edu.vn";
            var semesterId = 0;
            var actual = studentService.GetStudentNotHaveGroupFinalByFptEmail(fptEmail, semesterId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetListStudentIdByFinalGroupIdTest()
        /// </summary>
        [TestMethod()]
        public void GetListStudentIdByFinalGroupIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var finalGroupId = 1;
            var actual = studentService.GetListStudentIdByFinalGroupId(finalGroupId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetStudentIDByFptEmailAndGroupNameTest()
        /// </summary>
        [TestMethod()]
        public void GetStudentIDByFptEmailAndGroupNameTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var fptEmail = "anbthe140005@fpt.edu.vn";
            var groupName = "";
            var semesterId = 1;
            var actual = studentService.GetStudentIDByFptEmailAndGroupName(fptEmail, groupName, semesterId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetInforStudentHaveFinalGroupTest()
        /// </summary>
        [TestMethod()]
        public void GetInforStudentHaveFinalGroupTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var studentId = "anbthe140005@fpt.edu.vn";
            var semesterId = 1;
            var actual = studentService.GetInforStudentHaveFinalGroup(studentId, semesterId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetInforStudentHaveRegisteredGroupTest()
        /// </summary>
        [TestMethod()]
        public void GetInforStudentHaveRegisteredGroupTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudentService), typeof(StudentService));
            var studentService = container.Resolve<StudentService>();

            var studentId = "anbthe140005@fpt.edu.vn";
            var groupIdeaId = 1;
            var actual = studentService.GetInforStudentHaveRegisteredGroup(studentId, groupIdeaId);

            Assert.IsNull(actual);
        }
    }
}