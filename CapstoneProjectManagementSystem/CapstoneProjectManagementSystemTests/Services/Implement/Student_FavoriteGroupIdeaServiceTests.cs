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
    public class Student_FavoriteGroupIdeaServiceTests
    {
        /// <summary>
        /// Test function GetFavoriteIdeaListByStudentIdTest()
        /// </summary>
        // list favorite not null
        [TestMethod()]
        public void GetFavoriteIdeaListByStudentIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_FavoriteGroupIdeaService), typeof(Student_FavoriteGroupIdeaService));
            var student_favoriteGroupIdeaService = container.Resolve<Student_FavoriteGroupIdeaService>();

            var studentId = "anbthe140005@fpt.edu.vn";
            var groupId = 4;
            student_favoriteGroupIdeaService.AddRecord(studentId, groupId);
            var actual = student_favoriteGroupIdeaService.GetFavoriteIdeaListByStudentId(studentId);
            student_favoriteGroupIdeaService.DeleteRecord(studentId, groupId);

            var studentFavoriteGroupIdea = new StudentFavoriteGroupIdea()
            {
                StudentID = "anbthe140005@fpt.edu.vn",
                GroupIdeaID = 4
            };
            var studentFavoriteGroupIdeaList = new List<StudentFavoriteGroupIdea>();
            studentFavoriteGroupIdeaList.Add(studentFavoriteGroupIdea);
            Assert.AreEqual(studentFavoriteGroupIdeaList[0].StudentID, actual[0].StudentID);
            Assert.AreEqual(studentFavoriteGroupIdeaList[0].GroupIdeaID, actual[0].GroupIdeaID);
        }
        // list favorite is null
        [TestMethod()]
        public void GetFavoriteIdeaListByStudentIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_FavoriteGroupIdeaService), typeof(Student_FavoriteGroupIdeaService));
            var student_favoriteGroupIdeaService = container.Resolve<Student_FavoriteGroupIdeaService>();

            var studentId = "anbthe140005@fpt.edu.vn";
            var actual = student_favoriteGroupIdeaService.GetFavoriteIdeaListByStudentId(studentId);

            Assert.IsNull(actual);
        }
        // not exist student id
        [TestMethod()]
        public void GetFavoriteIdeaListByStudentIdTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_FavoriteGroupIdeaService), typeof(Student_FavoriteGroupIdeaService));
            var student_favoriteGroupIdeaService = container.Resolve<Student_FavoriteGroupIdeaService>();

            var studentId = "aaa140005@fpt.edu.vn";
            var actual = student_favoriteGroupIdeaService.GetFavoriteIdeaListByStudentId(studentId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetRecordTest()
        /// </summary>
        // record esixt
        [TestMethod()]
        public void GetRecordTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_FavoriteGroupIdeaService), typeof(Student_FavoriteGroupIdeaService));
            var student_favoriteGroupIdeaService = container.Resolve<Student_FavoriteGroupIdeaService>();

            var studentId = "anbthe140005@fpt.edu.vn";
            var groupId = 4;
            student_favoriteGroupIdeaService.AddRecord(studentId, groupId);
            var actual = student_favoriteGroupIdeaService.GetRecord(studentId, groupId);
            student_favoriteGroupIdeaService.DeleteRecord(studentId, groupId);

            var studentFavoriteGroupIdea = new StudentFavoriteGroupIdea()
            {
                StudentID = "anbthe140005@fpt.edu.vn",
                GroupIdeaID = 4
            };
            Assert.AreEqual(studentFavoriteGroupIdea.StudentID, actual.StudentID);
            Assert.AreEqual(studentFavoriteGroupIdea.GroupIdeaID, actual.GroupIdeaID);
        }
        // record not esixt
        [TestMethod()]
        public void GetRecordTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_FavoriteGroupIdeaService), typeof(Student_FavoriteGroupIdeaService));
            var student_favoriteGroupIdeaService = container.Resolve<Student_FavoriteGroupIdeaService>();

            var studentId = "anbthe140005@fpt.edu.vn";
            var groupId = 4;
            var actual = student_favoriteGroupIdeaService.GetRecord(studentId, groupId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function AddRecordTest()
        /// </summary>
        // exist studentId and groupid
        [TestMethod()]
        public void AddRecordTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_FavoriteGroupIdeaService), typeof(Student_FavoriteGroupIdeaService));
            var student_favoriteGroupIdeaService = container.Resolve<Student_FavoriteGroupIdeaService>();

            var studentId = "anbthe140005@fpt.edu.vn";
            var groupId = 4;
            var actual = student_favoriteGroupIdeaService.AddRecord(studentId, groupId);
            student_favoriteGroupIdeaService.DeleteRecord(studentId, groupId);

            Assert.AreEqual(1, actual);
        }
        // studentId not exist
        [TestMethod()]
        public void AddRecordTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_FavoriteGroupIdeaService), typeof(Student_FavoriteGroupIdeaService));
            var student_favoriteGroupIdeaService = container.Resolve<Student_FavoriteGroupIdeaService>();

            var studentId = "aaa140005@fpt.edu.vn";
            var groupId = 4;
            var actual = student_favoriteGroupIdeaService.AddRecord(studentId, groupId);
            student_favoriteGroupIdeaService.DeleteRecord(studentId, groupId);

            Assert.AreEqual(0, actual);
        }
        // groupId not exist
        [TestMethod()]
        public void AddRecordTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_FavoriteGroupIdeaService), typeof(Student_FavoriteGroupIdeaService));
            var student_favoriteGroupIdeaService = container.Resolve<Student_FavoriteGroupIdeaService>();

            var studentId = "anbthe140005@fpt.edu.vn";
            var groupId = 0;
            var actual = student_favoriteGroupIdeaService.AddRecord(studentId, groupId);
            student_favoriteGroupIdeaService.DeleteRecord(studentId, groupId);

            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test function DeleteRecordTest()
        /// </summary>
        // record esixt
        [TestMethod()]
        public void DeleteRecordTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_FavoriteGroupIdeaService), typeof(Student_FavoriteGroupIdeaService));
            var student_favoriteGroupIdeaService = container.Resolve<Student_FavoriteGroupIdeaService>();

            var studentId = "anbthe140005@fpt.edu.vn";
            var groupId = 4;
            student_favoriteGroupIdeaService.AddRecord(studentId, groupId);
            var actual = student_favoriteGroupIdeaService.DeleteRecord(studentId, groupId);

            Assert.AreEqual(1, actual);
        }
        // record not esixt
        [TestMethod()]
        public void DeleteRecordTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_FavoriteGroupIdeaService), typeof(Student_FavoriteGroupIdeaService));
            var student_favoriteGroupIdeaService = container.Resolve<Student_FavoriteGroupIdeaService>();

            var studentId = "anbthe140005@fpt.edu.vn";
            var groupId = 4;
            var actual = student_favoriteGroupIdeaService.DeleteRecord(studentId, groupId);

            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test function DeleteAllRecordOfAGroupIdeaTest()
        /// </summary>
        // group idea have records
        [TestMethod()]
        public void DeleteAllRecordOfAGroupIdeaTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_FavoriteGroupIdeaService), typeof(Student_FavoriteGroupIdeaService));
            var student_favoriteGroupIdeaService = container.Resolve<Student_FavoriteGroupIdeaService>();

            var studentId_1 = "anbthe140005@fpt.edu.vn";
            var studentId_2 = "anhbdhe140010@fpt.edu.vn";
            var studentId_3 = "anhnths140013@fpt.edu.vn";
            var studentId_4 = "anhvphs140009@fpt.edu.vn";
            var groupId = 4;
            student_favoriteGroupIdeaService.AddRecord(studentId_1, groupId);
            student_favoriteGroupIdeaService.AddRecord(studentId_2, groupId);
            student_favoriteGroupIdeaService.AddRecord(studentId_3, groupId);
            student_favoriteGroupIdeaService.AddRecord(studentId_4, groupId);
            var actual = student_favoriteGroupIdeaService.DeleteAllRecordOfAGroupIdea(groupId);

            Assert.AreEqual(4, actual);
        }
        // group idea not have records
        [TestMethod()]
        public void DeleteAllRecordOfAGroupIdeaTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_FavoriteGroupIdeaService), typeof(Student_FavoriteGroupIdeaService));
            var student_favoriteGroupIdeaService = container.Resolve<Student_FavoriteGroupIdeaService>();

            var groupId = 4;
            var actual = student_favoriteGroupIdeaService.DeleteAllRecordOfAGroupIdea(groupId);

            Assert.AreEqual(0, actual);
        }
        // group idea not exist
        [TestMethod()]
        public void DeleteAllRecordOfAGroupIdeaTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_FavoriteGroupIdeaService), typeof(Student_FavoriteGroupIdeaService));
            var student_favoriteGroupIdeaService = container.Resolve<Student_FavoriteGroupIdeaService>();

            var groupId = 0;
            var actual = student_favoriteGroupIdeaService.DeleteAllRecordOfAGroupIdea(groupId);

            Assert.AreEqual(0, actual);
        }
    }
}