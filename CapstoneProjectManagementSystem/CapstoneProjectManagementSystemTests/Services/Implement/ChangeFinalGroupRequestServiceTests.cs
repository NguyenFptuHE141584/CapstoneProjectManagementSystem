using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneProjectManagementSystem.Services.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace CapstoneProjectManagementSystem.Services.Implement.Tests
{
    [TestClass()]
    public class ChangeFinalGroupRequestServiceTests
    {

        /// <summary>
        /// Test Function CreateChangeFinalGroupRequestDao
        /// </summary>
        /// 

        //Test From StudentId Is Null
        [TestMethod()]
        public void CreateChangeFinalGroupRequestDao_TestFromStudentIdIsNull()
        {
            string fromStudentId = null;
            string toStudentId = null;
            try
            {
                if (fromStudentId != null && toStudentId != null)
                {
                    if (fromStudentId.Length <= 200 && toStudentId.Length <= 200)
                    {
                        var repoMock = new Mock<IChangeFinalGroupRequestService>();
                        repoMock.Setup(repoMock => repoMock.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId)).Returns(0);
                        var changeFinalGroupRequestService = repoMock.Object;
                        var actual = changeFinalGroupRequestService.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId);
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                            return;
                        }
                        if (actual == 0)
                        {
                            Assert.AreEqual(0, actual);
                            return;
                        }
                    }
                    else
                    {
                        if (fromStudentId.Length > 200)
                        {
                            Assert.IsTrue(fromStudentId.Length > 200);
                            return;
                        }
                        if (toStudentId.Length > 200)
                        {
                            Assert.IsTrue(toStudentId.Length > 200);
                            return;
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
                return;
            }
        }

        //Test To StudentId Is Null
        [TestMethod()]
        public void CreateChangeFinalGroupRequestDao_TestToStudentIdIsNull()
        {
            string fromStudentId = "nguyennhhe141584@fpt.edu.vn";
            string toStudentId = null;
            try
            {
                if (fromStudentId != null && toStudentId != null)
                {
                    if (fromStudentId.Length <= 200 && toStudentId.Length <= 200)
                    {
                        var repoMock = new Mock<IChangeFinalGroupRequestService>();
                        repoMock.Setup(repoMock => repoMock.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId)).Returns(0);
                        var changeFinalGroupRequestService = repoMock.Object;
                        var actual = changeFinalGroupRequestService.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId);
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                            return;
                        }
                        if (actual == 0)
                        {
                            Assert.AreEqual(0, actual);
                            return;
                        }
                    }
                    else
                    {
                        if (fromStudentId.Length > 200)
                        {
                            Assert.IsTrue(fromStudentId.Length > 200);
                            return;
                        }
                        if (toStudentId.Length > 200)
                        {
                            Assert.IsTrue(toStudentId.Length > 200);
                            return;
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
                return;
            }
        }

        //Test From StudentId Is Empty
        [TestMethod()]
        public void CreateChangeFinalGroupRequestDao_TestFromStudentIdIsEmpty()
        {
            string fromStudentId = "";
            string toStudentId = "";
            try
            {
                if (!string.IsNullOrWhiteSpace(fromStudentId) && !string.IsNullOrWhiteSpace(toStudentId))
                {
                    if (fromStudentId.Length <= 200 && toStudentId.Length <= 200)
                    {
                        var repoMock = new Mock<IChangeFinalGroupRequestService>();
                        repoMock.Setup(repoMock => repoMock.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId)).Returns(0);
                        var changeFinalGroupRequestService = repoMock.Object;
                        var actual = changeFinalGroupRequestService.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId);
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                            return;
                        }
                        if (actual == 0)
                        {
                            Assert.AreEqual(0, actual);
                            return;
                        }
                    }
                    else
                    {
                        if (fromStudentId.Length > 200)
                        {
                            Assert.IsTrue(fromStudentId.Length > 200);
                            return;
                        }
                        if (toStudentId.Length > 200)
                        {
                            Assert.IsTrue(toStudentId.Length > 200);
                            return;
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
                return;
            }
        }

        //Test To StudentId Is Empty
        [TestMethod()]
        public void CreateChangeFinalGroupRequestDao_TestToStudentIdIsEmpty()
        {
            string fromStudentId = "nguyennhhe141584@fpt.edu.vn";
            string toStudentId = "";
            try
            {
                if (!string.IsNullOrWhiteSpace(fromStudentId) && !string.IsNullOrWhiteSpace(toStudentId))
                {
                    if (fromStudentId.Length <= 200 && toStudentId.Length <= 200)
                    {
                        var repoMock = new Mock<IChangeFinalGroupRequestService>();
                        repoMock.Setup(repoMock => repoMock.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId)).Returns(0);
                        var changeFinalGroupRequestService = repoMock.Object;
                        var actual = changeFinalGroupRequestService.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId);
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                            return;
                        }
                        if (actual == 0)
                        {
                            Assert.AreEqual(0, actual);
                            return;
                        }
                    }
                    else
                    {
                        if (fromStudentId.Length > 200)
                        {
                            Assert.IsTrue(fromStudentId.Length > 200);
                            return;
                        }
                        if (toStudentId.Length > 200)
                        {
                            Assert.IsTrue(toStudentId.Length > 200);
                            return;
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
                return;
            }
        }

        //Test From StudentId Have Length Greater 200
        [TestMethod()]
        public void CreateChangeFinalGroupRequestDao_TestFromStudentIdHaveLengthGreater200()
        {
            string fromStudentId = " asdfasdf fdasfsadf sdfasdfasadsfasdf  sadfasdf asdfasdfnguyennhhe141584@fpt.edu.vnasdfasd dddddddd ddddddddd ddddddddd dddddddddddddd ddddddddd dasdfasdfasdf     asdfasdfasdf asdfsdfsd    asdfasdf  sadfasdf  asdfasdfasdf";
            string toStudentId = "anbthe140005@fpt.edu.vn";
            try
            {
                if (!string.IsNullOrWhiteSpace(fromStudentId) && !string.IsNullOrWhiteSpace(toStudentId))
                {
                    if (fromStudentId.Length <= 200 && toStudentId.Length <= 200)
                    {
                        var repoMock = new Mock<IChangeFinalGroupRequestService>();
                        repoMock.Setup(repoMock => repoMock.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId)).Returns(0);
                        var changeFinalGroupRequestService = repoMock.Object;
                        var actual = changeFinalGroupRequestService.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId);
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                            return;
                        }
                        if (actual == 0)
                        {
                            Assert.AreEqual(0, actual);
                            return;
                        }
                    }
                    else
                    {
                        if (fromStudentId.Length > 200)
                        {
                            Assert.IsTrue(fromStudentId.Length > 200);
                            return;
                        }
                        if (toStudentId.Length > 200)
                        {
                            Assert.IsTrue(toStudentId.Length > 200);
                            return;
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
                return;
            }
        }

        //Test To StudentId Have Length Greater 200
        [TestMethod()]
        public void CreateChangeFinalGroupRequestDao_TestToStudentIdHaveLengthGreater200()
        {
            string toStudentId = " asdfasdf fdasfsadf sdfasdfasadsfasdf  sadfasdf asdfasdfnguyennhhe141584@fpt.edu.vnasdfasd dddddddd ddddddddd ddddddddd dddddddddddddd ddddddddd dasdfasdfasdf     asdfasdfasdf asdfsdfsd    asdfasdf  sadfasdf  asdfasdfasdf";
            string fromStudentId = "anbthe140005@fpt.edu.vn";
            try
            {
                if (!string.IsNullOrWhiteSpace(fromStudentId) && !string.IsNullOrWhiteSpace(toStudentId))
                {
                    if (fromStudentId.Length <= 200 && toStudentId.Length <= 200)
                    {
                        var repoMock = new Mock<IChangeFinalGroupRequestService>();
                        repoMock.Setup(repoMock => repoMock.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId)).Returns(0);
                        var changeFinalGroupRequestService = repoMock.Object;
                        var actual = changeFinalGroupRequestService.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId);
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                            return;
                        }
                        if (actual == 0)
                        {
                            Assert.AreEqual(0, actual);
                            return;
                        }
                    }
                    else
                    {
                        if (fromStudentId.Length > 200)
                        {
                            Assert.IsTrue(fromStudentId.Length > 200);
                            return;
                        }
                        if (toStudentId.Length > 200)
                        {
                            Assert.IsTrue(toStudentId.Length > 200);
                            return;
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
                return;
            }
        }

        //Test Insert Fail
        [TestMethod()]
        public void CreateChangeFinalGroupRequestDao_TestInsertFail()
        {
            string fromStudentId = "nguyennhhe141584@fpt.edu.vn";
            string toStudentId = "anbthe140005@fpt.edu.vn";
            try
            {
                if (!string.IsNullOrWhiteSpace(fromStudentId) && !string.IsNullOrWhiteSpace(toStudentId))
                {
                    if (fromStudentId.Length <= 200 && toStudentId.Length <= 200)
                    {
                        var repoMock = new Mock<IChangeFinalGroupRequestService>();
                        repoMock.Setup(repoMock => repoMock.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId)).Returns(0);
                        var changeFinalGroupRequestService = repoMock.Object;
                        var actual = changeFinalGroupRequestService.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId);
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                            return;
                        }
                        if (actual == 0)
                        {
                            Assert.AreEqual(0, actual);
                            return;
                        }
                    }
                    else
                    {
                        if (fromStudentId.Length > 200)
                        {
                            Assert.IsTrue(fromStudentId.Length > 200);
                            return;
                        }
                        if (toStudentId.Length > 200)
                        {
                            Assert.IsTrue(toStudentId.Length > 200);
                            return;
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
                return;
            }
        }

        //Test Insert Success
        [TestMethod()]
        public void CreateChangeFinalGroupRequestDao_TestInsertSuccess()
        {
            string fromStudentId = "nguyennhhe141584@fpt.edu.vn";
            string toStudentId = "anbthe140005@fpt.edu.vn";
            try
            {
                if (!string.IsNullOrWhiteSpace(fromStudentId) && !string.IsNullOrWhiteSpace(toStudentId))
                {
                    if (fromStudentId.Length <= 200 && toStudentId.Length <= 200)
                    {
                        var repoMock = new Mock<IChangeFinalGroupRequestService>();
                        repoMock.Setup(repoMock => repoMock.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId)).Returns(1);
                        var changeFinalGroupRequestService = repoMock.Object;
                        var actual = changeFinalGroupRequestService.CreateChangeFinalGroupRequestDao(fromStudentId, toStudentId);
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                            return;
                        }
                        if (actual == 0)
                        {
                            Assert.AreEqual(0, actual);
                            return;
                        }
                    }
                    else
                    {
                        if (fromStudentId.Length > 200)
                        {
                            Assert.IsTrue(fromStudentId.Length > 200);
                            return;
                        }
                        if (toStudentId.Length > 200)
                        {
                            Assert.IsTrue(toStudentId.Length > 200);
                            return;
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
                return;
            }
        }
    }
}