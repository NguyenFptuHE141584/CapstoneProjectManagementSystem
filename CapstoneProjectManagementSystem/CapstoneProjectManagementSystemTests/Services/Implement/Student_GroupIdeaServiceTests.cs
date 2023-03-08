using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneProjectManagementSystem.Services.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace CapstoneProjectManagementSystem.Services.Implement.Tests
{
    [TestClass()]
    public class Student_GroupIdeaServiceTests
    {
        /// <summary>
        /// Test function GetLeaderIdByGroupIdeaIdTest()
        /// </summary>
        // exist group id
        [TestMethod()]
        public void GetLeaderIdByGroupIdeaIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var groupIdeaId = 4;
            var actual = finalgroupService.GetLeaderIdByGroupIdeaId(groupIdeaId);

            var leaderId = "nguyennhhe141584@fpt.edu.vn";
            Assert.AreEqual(leaderId, actual);
        }
        // not exist group id
        [TestMethod()]
        public void GetLeaderIdByGroupIdeaIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var groupIdeaId = 0;
            var actual = finalgroupService.GetLeaderIdByGroupIdeaId(groupIdeaId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetMemberIdByGroupIdeaIdTest()
        /// </summary>
        // exist group id
        [TestMethod()]
        public void GetMemberIdByGroupIdeaIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var groupIdeaId = 4;
            var actual = finalgroupService.GetMemberIdByGroupIdeaId(groupIdeaId);

            var leaderId = "baobthe140001@fpt.edu.vn";
            Assert.AreEqual(leaderId, actual[0]);
        }
        // not exist group id
        [TestMethod()]
        public void GetMemberIdByGroupIdeaIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var groupIdeaId = 0;
            var actual = finalgroupService.GetMemberIdByGroupIdeaId(groupIdeaId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetAllJoinRequestByGroupIdeaIdTest()
        /// </summary>
        // exist group id
        [TestMethod()]
        public void GetAllJoinRequestByGroupIdeaIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var groupIdeaId = 4;
            var studentId = "anhnths140013@fpt.edu.vn";
            var status = 3;
            var message = "";
            finalgroupService.AddRecord(studentId,groupIdeaId,status,message);
            var actual = finalgroupService.GetAllJoinRequestByGroupIdeaId(groupIdeaId);
            finalgroupService.DeleteRecord(studentId, groupIdeaId);

            Assert.AreEqual(studentId, actual[0].FptEmail);
        }
        // not exist group id
        [TestMethod()]
        public void GetAllJoinRequestByGroupIdeaIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var groupIdeaId = 0;
            var actual = finalgroupService.GetAllJoinRequestByGroupIdeaId(groupIdeaId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetGroupIdByStudentIdTest()
        /// </summary>
        // exist group id
        [TestMethod()]
        public void GetGroupIdByStudentIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var groupIdeaId = "4";
            var studentId = "nguyennhhe141584@fpt.edu.vn";
            var actual = finalgroupService.GetGroupIdByStudentId(studentId);

            Assert.AreEqual(groupIdeaId, actual);
        }
        // not exist group id
        [TestMethod()]
        public void GetGroupIdByStudentIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "aaa";
            var actual = finalgroupService.GetGroupIdByStudentId(studentId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetListRequestByStudentIdTest()
        /// </summary>
        // exist student id
        [TestMethod()]
        public void GetListRequestByStudentIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var groupIdeaId = 4;
            var studentId = "anhnths140013@fpt.edu.vn";
            var status = 3;
            var message = "";
            finalgroupService.AddRecord(studentId, groupIdeaId, status, message);
            var actual = finalgroupService.GetListRequestByStudentId(studentId);
            finalgroupService.DeleteRecord(studentId, groupIdeaId);

            Assert.AreEqual(groupIdeaId, actual[0].GroupIdeaID);
        }
        // not exist student id
        [TestMethod()]
        public void GetListRequestByStudentIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "aaa";
            var actual = finalgroupService.GetListRequestByStudentId(studentId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function UpdateStatusToAcceptTest()
        /// </summary>
        // exist student id, group idea id
        [TestMethod()]
        public void UpdateStatusToAcceptTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var groupIdeaId = 4;
            var studentId = "anhnths140013@fpt.edu.vn";
            var status = 3;
            var message = "";
            finalgroupService.AddRecord(studentId, groupIdeaId, status, message);
            var actual = finalgroupService.UpdateStatusToAccept(studentId,groupIdeaId);
            finalgroupService.DeleteRecord(studentId, groupIdeaId);

            Assert.AreEqual(1, actual);
        }
        // not exist student id
        [TestMethod()]
        public void UpdateStatusToAcceptTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "aaa";
            var groupIdeaId = 4;
            var actual = finalgroupService.UpdateStatusToAccept(studentId,groupIdeaId);

            Assert.AreEqual(0, actual);
        }
        // not exist group idea id
        [TestMethod()]
        public void UpdateStatusToAcceptTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "anhnths140013@fpt.edu.vn";
            var groupIdeaId = 0;
            var actual = finalgroupService.UpdateStatusToAccept(studentId, groupIdeaId);

            Assert.AreEqual(0, actual);
        }
        // exist student id and group idea id but not have records
        [TestMethod()]
        public void UpdateStatusToAcceptTest_4()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "anhnths140013@fpt.edu.vn";
            var groupIdeaId = 4;
            var actual = finalgroupService.UpdateStatusToAccept(studentId, groupIdeaId);

            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test function UpdateStatusToRejectTest()
        /// </summary>
        // exist student id, group idea id
        [TestMethod()]
        public void UpdateStatusToRejectTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var groupIdeaId = 4;
            var studentId = "anhnths140013@fpt.edu.vn";
            var status = 3;
            var message = "";
            finalgroupService.AddRecord(studentId, groupIdeaId, status, message);
            var actual = finalgroupService.UpdateStatusToReject(studentId, groupIdeaId);
            finalgroupService.DeleteRecord(studentId, groupIdeaId);

            Assert.AreEqual(1, actual);
        }
        // not exist student id
        [TestMethod()]
        public void UpdateStatusToRejectTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "aaa";
            var groupIdeaId = 4;
            var actual = finalgroupService.UpdateStatusToReject(studentId, groupIdeaId);

            Assert.AreEqual(0, actual);
        }
        // not exist group idea id
        [TestMethod()]
        public void UpdateStatusToRejectTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "anhnths140013@fpt.edu.vn";
            var groupIdeaId = 0;
            var actual = finalgroupService.UpdateStatusToReject(studentId, groupIdeaId);

            Assert.AreEqual(0, actual);
        }
        // exist student id and group idea id but not have records
        [TestMethod()]
        public void UpdateStatusToRejectTest_4()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "anhnths140013@fpt.edu.vn";
            var groupIdeaId = 4;
            var actual = finalgroupService.UpdateStatusToReject(studentId, groupIdeaId);

            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test function UpdateStatusToMemberTest()
        /// </summary>
        // exist student id, group idea id
        [TestMethod()]
        public void UpdateStatusToMemberTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var groupIdeaId = 4;
            var studentId = "anhnths140013@fpt.edu.vn";
            var status = 3;
            var message = "";
            finalgroupService.AddRecord(studentId, groupIdeaId, status, message);
            var actual = finalgroupService.UpdateStatusToMember(studentId, groupIdeaId);
            finalgroupService.DeleteRecord(studentId, groupIdeaId);

            Assert.AreEqual(1, actual);
        }
        // not exist student id
        [TestMethod()]
        public void UpdateStatusToMemberTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "aaa";
            var groupIdeaId = 4;
            var actual = finalgroupService.UpdateStatusToMember(studentId, groupIdeaId);

            Assert.AreEqual(0, actual);
        }
        // not exist group idea id
        [TestMethod()]
        public void UpdateStatusToMemberTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "anhnths140013@fpt.edu.vn";
            var groupIdeaId = 0;
            var actual = finalgroupService.UpdateStatusToMember(studentId, groupIdeaId);

            Assert.AreEqual(0, actual);
        }
        // exist student id and group idea id but not have records
        [TestMethod()]
        public void UpdateStatusToMemberTest_4()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "anhnths140013@fpt.edu.vn";
            var groupIdeaId = 4;
            var actual = finalgroupService.UpdateStatusToMember(studentId, groupIdeaId);

            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test function UpdateStatusToLeaderTest()
        /// </summary>
        // exist student id, group idea id
        [TestMethod()]
        public void UpdateStatusToLeaderTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var groupIdeaId = 4;
            var studentId = "anhnths140013@fpt.edu.vn";
            var status = 3;
            var message = "";
            finalgroupService.AddRecord(studentId, groupIdeaId, status, message);
            var actual = finalgroupService.UpdateStatusToLeader(studentId, groupIdeaId);
            finalgroupService.DeleteRecord(studentId, groupIdeaId);

            Assert.AreEqual(1, actual);
        }
        // not exist student id
        [TestMethod()]
        public void UpdateStatusToLeaderTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "aaa";
            var groupIdeaId = 4;
            var actual = finalgroupService.UpdateStatusToLeader(studentId, groupIdeaId);

            Assert.AreEqual(0, actual);
        }
        // not exist group idea id
        [TestMethod()]
        public void UpdateStatusToLeaderTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "anhnths140013@fpt.edu.vn";
            var groupIdeaId = 0;
            var actual = finalgroupService.UpdateStatusToLeader(studentId, groupIdeaId);

            Assert.AreEqual(0, actual);
        }
        // exist student id and group idea id but not have records
        [TestMethod()]
        public void UpdateStatusToLeaderTest_4()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "anhnths140013@fpt.edu.vn";
            var groupIdeaId = 4;
            var actual = finalgroupService.UpdateStatusToLeader(studentId, groupIdeaId);

            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test function DeleteRecordTest()
        /// </summary>
        // exist student id, group idea id
        [TestMethod()]
        public void DeleteRecordTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var groupIdeaId = 4;
            var studentId = "anhnths140013@fpt.edu.vn";
            var status = 3;
            var message = "";
            finalgroupService.AddRecord(studentId, groupIdeaId, status, message);
            var actual = finalgroupService.DeleteRecord(studentId, groupIdeaId);

            Assert.AreEqual(1, actual);
        }
        // not exist student id
        [TestMethod()]
        public void DeleteRecordTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "aaa";
            var groupIdeaId = 4;
            var actual = finalgroupService.DeleteRecord(studentId, groupIdeaId);

            Assert.AreEqual(0, actual);
        }
        // not exist group idea id
        [TestMethod()]
        public void DeleteRecordTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "anhnths140013@fpt.edu.vn";
            var groupIdeaId = 0;
            var actual = finalgroupService.DeleteRecord(studentId, groupIdeaId);

            Assert.AreEqual(0, actual);
        }
        // exist student id and group idea id but not have records
        [TestMethod()]
        public void DeleteRecordTest_4()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "anhnths140013@fpt.edu.vn";
            var groupIdeaId = 4;
            var actual = finalgroupService.DeleteRecord(studentId, groupIdeaId);

            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test function DeleteAllRequestTest()
        /// </summary>
        // exist student id
        [TestMethod()]
        public void DeleteAllRequestTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var groupIdeaId = 4;
            var studentId = "anhnths140013@fpt.edu.vn";
            var status = 3;
            var message = "";
            finalgroupService.AddRecord(studentId, groupIdeaId, status, message);
            var actual = finalgroupService.DeleteAllRequest(studentId);

            Assert.AreEqual(1, actual);
        }
        // not exist student id
        [TestMethod()]
        public void DeleteAllRequestTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "aaa";
            var actual = finalgroupService.DeleteAllRequest(studentId);

            Assert.AreEqual(0, actual);
        }
        // exist student id but not have records
        [TestMethod()]
        public void DeleteAllRequestTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var studentId = "anhnths140013@fpt.edu.vn";
            var actual = finalgroupService.DeleteAllRequest(studentId);

            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test function DeleteAllRecordOfGroupIdeaTest()
        /// </summary>
        // not exist group idea id
        [TestMethod()]
        public void DeleteAllRecordOfGroupIdeaTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            var finalgroupService = container.Resolve<Student_GroupIdeaService>();

            var groupIdeaId = 0;
            var actual = finalgroupService.DeleteAllRecordOfGroupIdea(groupIdeaId);

            Assert.AreEqual(0, actual);
        }
    }
}