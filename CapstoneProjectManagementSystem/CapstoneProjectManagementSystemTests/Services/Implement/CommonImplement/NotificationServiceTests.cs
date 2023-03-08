using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneProjectManagementSystem.Services.Implement.CommonImplement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using CapstoneProjectManagementSystem.Services.CommonServices;
using Moq;

namespace CapstoneProjectManagementSystem.Services.Implement.CommonImplement.Tests
{
    [TestClass()]
    public class NotificationServiceTests
    {
        private NotificationService notificationService;
        public NotificationServiceTests()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(INotificationService), typeof(NotificationService));
            notificationService = container.Resolve<NotificationService>();
        }


        /// <summary>
        /// Test Function CountNotificationNotRead
        /// </summary>
        //Test Exception
        [TestMethod()]
        public void CountNotificationNotRead_TestException()
        {
            string userId = null;
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.CountNotificationNotRead(userId);
                        if (actual != 0)
                        {
                            Assert.AreNotEqual(0, actual);
                            return;
                        }
                        else
                        {
                            Assert.AreEqual(0, actual);
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
        public void CountNotificationNotRead_TestUserIdIsEmpty()
        {
            string userId = "";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.CountNotificationNotRead(userId);
                        if (actual != 0)
                        {
                            Assert.AreNotEqual(0, actual);
                            return;
                        }
                        else
                        {
                            Assert.AreEqual(0, actual);
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

        //Test function count notification not read don't have any notificaiton
        [TestMethod()]
        public void CountNotificationNotRead_TestUserIdDontHaveAnyNotification()
        {
            string userId = "nguyennhhe144324@fpt.edu.vn";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.CountNotificationNotRead(userId);
                        if (actual != 0)
                        {
                            Assert.AreNotEqual(0, actual);
                            return;
                        }
                        else
                        {
                            Assert.AreEqual(0, actual);
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

        ////Test function count notification not read have  notificaiton
        [TestMethod()]
        public void CountNotificationNotRead_TestUserIdHaveNotification()
        {
            string userId = "nguyennhhe141584@fpt.edu.vn";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.CountNotificationNotRead(userId);
                        if (actual != 0)
                        {
                            Assert.AreNotEqual(0, actual);
                            return;
                        }
                        else
                        {
                            Assert.AreEqual(0, actual);
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
        /// Test Function CountAllNotification
        /// </summary>

        [TestMethod()]
        public void CountAllNotification_TestException()
        {
            string userId = null;
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.CountAllNotification(userId);
                        if (actual != 0)
                        {
                            Assert.AreNotEqual(0, actual);
                            return;
                        }
                        else
                        {
                            Assert.AreEqual(0, actual);
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
        public void CountAllNotification_TestUserIdIsEmpty()
        {
            string userId = "";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.CountAllNotification(userId);
                        if (actual != 0)
                        {
                            Assert.AreNotEqual(0, actual);
                            return;
                        }
                        else
                        {
                            Assert.AreEqual(0, actual);
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

        //Test function count notification not read don't have any notificaiton
        [TestMethod()]
        public void CountAllNotification_TestUserIdDontHaveAnyNotification()
        {
            string userId = "nguyennhhe144324@fpt.edu.vn";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.CountAllNotification(userId);
                        if (actual != 0)
                        {
                            Assert.AreNotEqual(0, actual);
                            return;
                        }
                        else
                        {
                            Assert.AreEqual(0, actual);
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

        ////Test function count notification not read have  notificaiton
        [TestMethod()]
        public void CountAllNotification_TestUserIdHaveNotification()
        {
            string userId = "nguyennhhe141584@fpt.edu.vn";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.CountAllNotification(userId);
                        if (actual != 0)
                        {
                            Assert.AreNotEqual(0, actual);
                            return;
                        }
                        else
                        {
                            Assert.AreEqual(0, actual);
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
        /// Test Function GetListNotificationNotReadByReceiverID
        /// </summary>
        /// 
        // test exception
        [TestMethod()]
        public void GetListNotificationNotReadByReceiverID_TestException()
        {
            string userId = null;
            int numberOfRecord = 0;
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && numberOfRecord != 0)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.GetListNotificationNotReadByReceiverID(numberOfRecord, userId);
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
        public void GetListNotificationNotReadByReceiverID_TestUserIdIsEmpty()
        {
            string userId = "";
            int numberOfRecord = 8;
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && numberOfRecord != 0)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.GetListNotificationNotReadByReceiverID(numberOfRecord, userId);
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

        //Test function have result return null
        [TestMethod()]
        public void GetListNotificationNotReadByReceiverID_TestFunctionHaveResultReturnNull()
        {
            string userId = "nguyennhhe14185342@fpt.edu,vn";
            int numberOfRecord = 8;
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && numberOfRecord != 0)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.GetListNotificationNotReadByReceiverID(numberOfRecord, userId);
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

        //Test function have result return not null
        [TestMethod()]
        public void GetListNotificationNotReadByReceiverID_TestFunctionHaveResultReturnNotNull()
        {
            string userId = "nguyennhhe141584@fpt.edu.vn";
            int numberOfRecord = 8;
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && numberOfRecord != 0)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.GetListNotificationNotReadByReceiverID(numberOfRecord, userId);
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
        /// Test Function GetListAllNotificationByUserId
        /// </summary>

        //test exception
        [TestMethod()]
        public void GetListAllNotificationByUserId_TestException()
        {
            string userId = null;
            int numberOfRecord = 0;
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && numberOfRecord != 0)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.GetListAllNotificationByUserId(numberOfRecord, userId);
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
        public void GetListAllNotificationByUserId_TestUserIdIsEmpty()
        {
            string userId = "";
            int numberOfRecord = 8;
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && numberOfRecord != 0)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.GetListAllNotificationByUserId(numberOfRecord, userId);
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


        //Test function have result return null
        [TestMethod()]
        public void GetListAllNotificationByUserId_TestFunctionHaveResultReturnNull()
        {
            string userId = "nguyennhhe14185342@fpt.edu,vn";
            int numberOfRecord = 8;
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && numberOfRecord != 0)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.GetListAllNotificationByUserId(numberOfRecord, userId);
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


        //Test function have result return not null
        [TestMethod()]
        public void GetListAllNotificationByUserId_TestFunctionHaveResultReturnNotNull()
        {
            string userId = "nguyennhhe141584@fpt.edu.vn";
            int numberOfRecord = 8;
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && numberOfRecord != 0)
                {
                    if (userId != "")
                    {
                        var actual = notificationService.GetListAllNotificationByUserId(numberOfRecord, userId);
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
        /// Test Function InsertDataNotification
        /// </summary>

        //Test exception
        [TestMethod()]
        public void InsertDataNotification_TestException()
        {
            string userId = null;
            string notificationContent = null;
            string attachedLink = null;
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && notificationContent != null && attachedLink != null)
                {
                    if (userId != "" && notificationContent != "" && attachedLink != "")
                    {
                        var repoMock = new Mock<INotificationService>();
                        repoMock.Setup(repoMock => repoMock.InsertDataNotification(userId, notificationContent, attachedLink))
                            .Returns(0);
                        var notificationService = repoMock.Object;
                        var actual = notificationService.InsertDataNotification(userId, notificationContent, attachedLink);
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
                        else if (notificationContent == "")
                        {
                            Assert.AreEqual("", notificationContent);
                            return;
                        }
                        else if (attachedLink == "")
                        {
                            Assert.AreEqual("", attachedLink);
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

        //Test UserId Is Empty
        [TestMethod()]
        public void InsertDataNotification_TestUserIdIsEmpty()
        {
            string userId = "";
            string notificationContent = "";
            string attachedLink = "";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && notificationContent != null && attachedLink != null)
                {
                    if (userId != "" && notificationContent != "" && attachedLink != "")
                    {
                        var repoMock = new Mock<INotificationService>();
                        repoMock.Setup(repoMock => repoMock.InsertDataNotification(userId, notificationContent, attachedLink))
                            .Returns(0);
                        var notificationService = repoMock.Object;
                        var actual = notificationService.InsertDataNotification(userId, notificationContent, attachedLink);
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
                        else if (notificationContent == "")
                        {
                            Assert.AreEqual("", notificationContent);
                            return;
                        }
                        else if (attachedLink == "")
                        {
                            Assert.AreEqual("", attachedLink);
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

        //Test Notification Content Is Empty
        [TestMethod()]
        public void InsertDataNotification_TestNotificationContentIsEmpty()
        {
            string userId = "nguyennhhe141584@fpt.edu.vn";
            string notificationContent = "";
            string attachedLink = "";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && notificationContent != null && attachedLink != null)
                {
                    if (userId != "" && notificationContent != "" && attachedLink != "")
                    {
                        var repoMock = new Mock<INotificationService>();
                        repoMock.Setup(repoMock => repoMock.InsertDataNotification(userId, notificationContent, attachedLink))
                            .Returns(0);
                        var notificationService = repoMock.Object;
                        var actual = notificationService.InsertDataNotification(userId, notificationContent, attachedLink);
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
                        else if (notificationContent == "")
                        {
                            Assert.AreEqual("", notificationContent);
                            return;
                        }
                        else if (attachedLink == "")
                        {
                            Assert.AreEqual("", attachedLink);
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

        //Test attachedLink Is Empty
        [TestMethod()]
        public void InsertDataNotification_TestAttachedLinkIsEmpty()
        {
            string userId = "nguyennhhe141584@fpt.edu.vn";
            string notificationContent = "Phương Yến Ðan(HE140021) joined your group.";
            string attachedLink = "";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && notificationContent != null && attachedLink != null)
                {
                    if (userId != "" && notificationContent != "" && attachedLink != "")
                    {
                        var repoMock = new Mock<INotificationService>();
                        repoMock.Setup(repoMock => repoMock.InsertDataNotification(userId, notificationContent, attachedLink))
                            .Returns(0);
                        var notificationService = repoMock.Object;
                        var actual = notificationService.InsertDataNotification(userId, notificationContent, attachedLink);
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
                        else if (notificationContent == "")
                        {
                            Assert.AreEqual("", notificationContent);
                            return;
                        }
                        else if (attachedLink == "")
                        {
                            Assert.AreEqual("", attachedLink);
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

        //test insert fail
        [TestMethod()]
        public void InsertDataNotification_TestInsertFail()
        {
            string userId = "nguyennhhe141584@fpt.edu.vn";
            string notificationContent = "Phương Yến Ðan(HE140021) joined your group.";
            string attachedLink = "/MyGroup/Index";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && notificationContent != null && attachedLink != null)
                {
                    if (userId != "" && notificationContent != "" && attachedLink != "")
                    {
                        var repoMock = new Mock<INotificationService>();
                        repoMock.Setup(repoMock => repoMock.InsertDataNotification(userId, notificationContent, attachedLink))
                            .Returns(0);
                        var notificationService = repoMock.Object;
                        var actual = notificationService.InsertDataNotification(userId, notificationContent, attachedLink);
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                            return;
                        }
                        else
                        {
                            Assert.AreEqual(0, actual);
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
                        else if (notificationContent == "")
                        {
                            Assert.AreEqual("", notificationContent);
                            return;
                        }
                        else if (attachedLink == "")
                        {
                            Assert.AreEqual("", attachedLink);
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

        //test insert success
        [TestMethod()]
        public void InsertDataNotification_TestInsertSuccess()
        {
            string userId = "nguyennhhe141584@fpt.edu.vn";
            string notificationContent = "Phương Yến Ðan(HE140021) joined your group.";
            string attachedLink = "/MyGroup/Index";
            var exception = "Error Message: Test Exception";
            try
            {
                if (userId != null && notificationContent != null && attachedLink != null)
                {
                    if (userId != "" && notificationContent != "" && attachedLink != "")
                    {
                        var repoMock = new Mock<INotificationService>();
                        repoMock.Setup(repoMock => repoMock.InsertDataNotification(userId, notificationContent, attachedLink))
                            .Returns(1);
                        var notificationService = repoMock.Object;
                        var actual = notificationService.InsertDataNotification(userId, notificationContent, attachedLink);
                        if (actual == 1)
                        {
                            Assert.AreEqual(1, actual);
                            return;
                        }
                        else
                        {
                            Assert.AreEqual(0, actual);
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
                        else if (notificationContent == "")
                        {
                            Assert.AreEqual("", notificationContent);
                            return;
                        }
                        else if (attachedLink == "")
                        {
                            Assert.AreEqual("", attachedLink);
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
        /// Test Function GetAttachedLinkByNotificationId
        /// </summary>
        
        /// test exception 
        [TestMethod()]
        public void GetAttachedLinkByNotificationId_TestException()
        {
            int notificationId = -543;
            var exception = "Error Message: Test Exception";
            try
            {
                if (notificationId > 0)
                {
                    var actual = notificationService.GetAttachedLinkByNotificationId(notificationId);
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

        /// Test GetAttached Link Is Null
        [TestMethod()]
        public void GetAttachedLinkByNotificationId_TestGetAttachedLinkIsNull()
        {
            int notificationId = 1000;
            var exception = "Error Message: Test Exception";
            try
            {
                if (notificationId > 0)
                {
                    var actual = notificationService.GetAttachedLinkByNotificationId(notificationId);
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
        
        /// Test GetAttached Link Is Not Null
        [TestMethod()]
        public void GetAttachedLinkByNotificationId_TestGetAttachedLinkIsNotNull()
        {
            int notificationId = 1;
            var exception = "Error Message: Test Exception";
            try
            {
                if (notificationId > 0)
                {
                    var actual = notificationService.GetAttachedLinkByNotificationId(notificationId);
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
        /// Test Function UpdateReadedNotificationByNotificationId
        /// </summary>
        /// 

        // test exception
        [TestMethod()]
        public void UpdateReadedNotificationByNotificationId_TestException()
        {
            var notificationId = "asdf";
            var exception = "";
            try
            {
                if (Convert.ToInt32(notificationId) > 0)
                {
                    var repoMock = new Mock<INotificationService>();
                    repoMock.Setup(repoMock => repoMock.UpdateReadedNotificationByNotificationId
                        (Convert.ToInt32(notificationId))).Returns(0);
                    var notificationService = repoMock.Object;

                    var actual = notificationService.UpdateReadedNotificationByNotificationId
                        (Convert.ToInt32(notificationId));
                    if (actual == 1)
                    {
                        Assert.AreEqual(0,actual);
                        return;
                    }
                    if(actual == 0)
                    {
                        Assert.AreNotEqual(0,actual);
                        return;
                    }
                }
                else
                {
                    exception += "Error Message: Test Exception";
                    throw new Exception(exception);
                }
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
                return;
            }
        }

        // test notificationId not positive number
        [TestMethod()]
        public void UpdateReadedNotificationByNotificationId_TestNotificationIdNotPositiveNumber()
        {
            var notificationId = -53445;
            try
            {
                if (Convert.ToInt32(notificationId) > 0)
                {
                    var repoMock = new Mock<INotificationService>();
                    repoMock.Setup(repoMock => repoMock.UpdateReadedNotificationByNotificationId
                        (Convert.ToInt32(notificationId))).Returns(0);
                    var notificationService = repoMock.Object;

                    var actual = notificationService.UpdateReadedNotificationByNotificationId
                        (Convert.ToInt32(notificationId));
                    if (actual == 1)
                    {
                        Assert.AreEqual(0, actual);
                        return;
                    }
                    if (actual == 0)
                    {
                        Assert.AreNotEqual(0, actual);
                        return;
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


        // test notificationId equal 0
        [TestMethod()]
        public void UpdateReadedNotificationByNotificationId_TestNotificationIdEqual0()
        {
            var notificationId = 0;
            try
            {
                if (Convert.ToInt32(notificationId) > 0)
                {
                    var repoMock = new Mock<INotificationService>();
                    repoMock.Setup(repoMock => repoMock.UpdateReadedNotificationByNotificationId
                        (Convert.ToInt32(notificationId))).Returns(0);
                    var notificationService = repoMock.Object;

                    var actual = notificationService.UpdateReadedNotificationByNotificationId
                        (Convert.ToInt32(notificationId));
                    if (actual == 1)
                    {
                        Assert.AreEqual(0, actual);
                        return;
                    }
                    if (actual == 0)
                    {
                        Assert.AreNotEqual(0, actual);
                        return;
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



        // test function update fail;
        [TestMethod()]
        public void UpdateReadedNotificationByNotificationId_TestFunctionUpdateFail()
        {
            var notificationId = 3;
            try
            {
                if (Convert.ToInt32(notificationId) > 0)
                {
                    var repoMock = new Mock<INotificationService>();
                    repoMock.Setup(repoMock => repoMock.UpdateReadedNotificationByNotificationId
                        (Convert.ToInt32(notificationId))).Returns(0);
                    var notificationService = repoMock.Object;

                    var actual = notificationService.UpdateReadedNotificationByNotificationId
                        (Convert.ToInt32(notificationId));
                    if (actual == 1)
                    {
                        Assert.AreEqual(1, actual);
                        return;
                    }
                    if (actual == 0)
                    {
                        Assert.AreNotEqual(1, actual);
                        return;
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

        // test function update success;
        [TestMethod()]
        public void UpdateReadedNotificationByNotificationId_TestFunctionUpdateSuccess()
        {
            var notificationId = 23543345;
            try
            {
                if (Convert.ToInt32(notificationId) > 0)
                {
                    var repoMock = new Mock<INotificationService>();
                    repoMock.Setup(repoMock => repoMock.UpdateReadedNotificationByNotificationId
                        (Convert.ToInt32(notificationId))).Returns(1);
                    var notificationService = repoMock.Object;

                    var actual = notificationService.UpdateReadedNotificationByNotificationId
                        (Convert.ToInt32(notificationId));
                    if (actual == 1)
                    {
                        Assert.AreEqual(1, actual);
                        return;
                    }
                    if (actual == 0)
                    {
                        Assert.AreNotEqual(1, actual);
                        return;
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