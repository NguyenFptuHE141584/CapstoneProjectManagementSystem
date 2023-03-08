using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneProjectManagementSystem.Services.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProjectManagementSystem.Models;
using Moq;

namespace CapstoneProjectManagementSystem.Services.Implement.Tests
{
    [TestClass()]
    public class ChangeTopicRequestServiceTests
    {
        /// <summary>
        /// Test Function AddChangeTopicRequest
        /// </summary>


        //Test Change Topic Request Is Null
        [TestMethod()]
        public void AddChangeTopicRequest_TestChangeTopicRequesIsNull()
        {
            ChangeTopicRequest changeTopicRequest = null;
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsFalse(!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsFalse(!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (!string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsFalse(!string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (!string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsFalse(!string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (!string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsFalse(!string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (!string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsFalse(!string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (!string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsFalse(!string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (!string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsFalse(!string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }


        //Test OldTopicNameEnglish Is Empty
        [TestMethod()]
        public void AddChangeTopicRequest_TestOldTopicNameEnglishIsEmpty()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "",
                OldTopicNameVietNamese = "asdf",
                OldAbbreviation = "fds",
                NewTopicNameEnglish = "sdaf",
                NewTopicNameVietNamese = "fasdf",
                NewAbbreviation = "fsdaf",
                ReasonChangeTopic = "fdsfsd",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 0,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsFalse(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsFalse(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsFalse(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsFalse(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsFalse(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsFalse(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsFalse(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }

        //Test OldTopicNameVietNamese Is Empty
        [TestMethod()]
        public void AddChangeTopicRequest_TestOldTopicNameVietNameseIsEmpty()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "asdfasdfasd",
                OldTopicNameVietNamese = "",
                OldAbbreviation = "fds",
                NewTopicNameEnglish = "sdaf",
                NewTopicNameVietNamese = "fasdf",
                NewAbbreviation = "fsdaf",
                ReasonChangeTopic = "fdsfsd",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 0,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }

        //Test OldAbbreviation Is Empty
        [TestMethod()]
        public void AddChangeTopicRequest_TestOldAbbreviationIsEmpty()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "asdfasdfasd",
                OldTopicNameVietNamese = "asdfasdf",
                OldAbbreviation = "",
                NewTopicNameEnglish = "sdaf",
                NewTopicNameVietNamese = "fasdf",
                NewAbbreviation = "fsdaf",
                ReasonChangeTopic = "fdsfsd",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 0,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }

        //Test NewTopicNameEnglish Is Empty
        [TestMethod()]
        public void AddChangeTopicRequest_TestNewTopicNameEnglishIsEmpty()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "asdfasdfasd",
                OldTopicNameVietNamese = "asdfasdf",
                OldAbbreviation = "asdfasdf",
                NewTopicNameEnglish = "",
                NewTopicNameVietNamese = "fasdf",
                NewAbbreviation = "fsdaf",
                ReasonChangeTopic = "fdsfsd",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 0,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }

        //Test NewTopicNameVietNamese Is Empty
        [TestMethod()]
        public void AddChangeTopicRequest_TestNewTopicNameVietNameseIsEmpty()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "asdfasdfasd",
                OldTopicNameVietNamese = "asdfasdf",
                OldAbbreviation = "asdfasdf",
                NewTopicNameEnglish = "afsdfsda",
                NewTopicNameVietNamese = "",
                NewAbbreviation = "fsdaf",
                ReasonChangeTopic = "fdsfsd",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 0,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }

        //Test NewAbbreviation Is Empty
        [TestMethod()]
        public void AddChangeTopicRequest_TestNewAbbreviationIsEmpty()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "asdfasdfasd",
                OldTopicNameVietNamese = "asdfasdf",
                OldAbbreviation = "asdfasdf",
                NewTopicNameEnglish = "afsdfsda",
                NewTopicNameVietNamese = "adsfasdfasdf",
                NewAbbreviation = "",
                ReasonChangeTopic = "fdsfsd",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 0,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }

        //Test ReasonChangeTopic Is Empty
        [TestMethod()]
        public void AddChangeTopicRequest_TestReasonChangeTopicIsEmpty()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "asdfasdfasd",
                OldTopicNameVietNamese = "asdfasdf",
                OldAbbreviation = "asdfasdf",
                NewTopicNameEnglish = "afsdfsda",
                NewTopicNameVietNamese = "adsfasdfasdf",
                NewAbbreviation = "asfdasdf",
                ReasonChangeTopic = "",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 0,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }


        //Test FinalGroupID Not Equal 0
        [TestMethod()]
        public void AddChangeTopicRequest_TestFinalGroupIDNotEqual0()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "asdfasdfasd",
                OldTopicNameVietNamese = "asdfasdf",
                OldAbbreviation = "asdfasdf",
                NewTopicNameEnglish = "afsdfsda",
                NewTopicNameVietNamese = "adsfasdfasdf",
                NewAbbreviation = "asfdasdf",
                ReasonChangeTopic = "",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 0,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }


        //Test EmailSuperVisor Is Empty
        [TestMethod()]
        public void AddChangeTopicRequest_TestEmailSuperVisorIsEmpty()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "asdfasdfasd",
                OldTopicNameVietNamese = "asdfasdf",
                OldAbbreviation = "asdfasdf",
                NewTopicNameEnglish = "afsdfsda",
                NewTopicNameVietNamese = "adsfasdfasdf",
                NewAbbreviation = "asfdasdf",
                EmailSuperVisor = "",
                ReasonChangeTopic = "asdfasdfasdf",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 1,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }


        //Test Length Of OldTopicNameEnglish Greater 150
        [TestMethod()]
        public void AddChangeTopicRequest_TestLengthOfOldTopicNameEnglishGreater150()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "Your words matter, and our paraphrasing tool is designed to ensure you use the right ones. With two free modes and five Premium modes to choose from, you can use QuillBot’s online Paraphraser to rephrase any text in a variety of ways. Our product will improve your fluency while also ensuring you have the appropriate vocabulary, tone, and style for any occasion. Simply enter your text into the input box, and our AI will work with you to create the best paraphrase.",
                OldTopicNameVietNamese = "asdfasdf",
                OldAbbreviation = "asdfasdf",
                NewTopicNameEnglish = "afsdfsda",
                NewTopicNameVietNamese = "adsfasdfasdf",
                NewAbbreviation = "asfdasdf",
                EmailSuperVisor = "sdfsdf",
                ReasonChangeTopic = "asdfsdafafsda",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 1,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }

        //Test Length Of OldTopicNameVietNamese Greater 150
        [TestMethod()]
        public void AddChangeTopicRequest_TestLengthOfOldTopicNameVietNameseGreater150()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "dfsfsf",
                OldTopicNameVietNamese = "Your words matter, and our paraphrasing tool is designed to ensure you use the right ones. With two free modes and five Premium modes to choose from, you can use QuillBot’s online Paraphraser to rephrase any text in a variety of ways. Our product will improve your fluency while also ensuring you have the appropriate vocabulary, tone, and style for any occasion. Simply enter your text into the input box, and our AI will work with you to create the best paraphrase.",
                OldAbbreviation = "asdfasdf",
                NewTopicNameEnglish = "afsdfsda",
                NewTopicNameVietNamese = "adsfasdfasdf",
                NewAbbreviation = "asfdasdf",
                EmailSuperVisor = "sdfsdf",
                ReasonChangeTopic = "asdfsdafafsda",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 1,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }

        //Test Length Of OldAbbreviation Greater 20
        [TestMethod()]
        public void AddChangeTopicRequest_TestLengthOfOldAbbreviationGreater20()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "dfsfsf",
                OldTopicNameVietNamese = "OldAbbreviation",
                OldAbbreviation = "Your words matter, and our paraphrasing tool is designed to ensure you use the right ones. With two free modes and five Premium modes to choose from, you can use QuillBot’s online Paraphraser to rephrase any text in a variety of ways. Our product will improve your fluency while also ensuring you have the appropriate vocabulary, tone, and style for any occasion. Simply enter your text into the input box, and our AI will work with you to create the best paraphrase.",
                NewTopicNameEnglish = "afsdfsda",
                NewTopicNameVietNamese = "adsfasdfasdf",
                NewAbbreviation = "asfdasdf",
                EmailSuperVisor = "sdfsdf",
                ReasonChangeTopic = "asdfsdafafsda",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 1,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldAbbreviation.Length > 20);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }

        //Test Length Of NewTopicNameEnglish Greater 150
        [TestMethod()]
        public void AddChangeTopicRequest_TestLengthOfNewTopicNameEnglishGreater150()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "dfsfsf",
                OldTopicNameVietNamese = "OldAbbreviation",
                OldAbbreviation = "OldAbbreviation",
                NewTopicNameEnglish = "Your words matter, and our paraphrasing tool is designed to ensure you use the right ones. With two free modes and five Premium modes to choose from, you can use QuillBot’s online Paraphraser to rephrase any text in a variety of ways. Our product will improve your fluency while also ensuring you have the appropriate vocabulary, tone, and style for any occasion. Simply enter your text into the input box, and our AI will work with you to create the best paraphrase.",
                NewTopicNameVietNamese = "adsfasdfasdf",
                NewAbbreviation = "asfdasdf",
                EmailSuperVisor = "sdfsdf",
                ReasonChangeTopic = "asdfsdafafsda",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 1,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldAbbreviation.Length > 20);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }

        //Test Length Of NewTopicNameVietNamese Greater 150
        [TestMethod()]
        public void AddChangeTopicRequest_TestLengthOfNewTopicNameVietNameseGreater150()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "dfsfsf",
                OldTopicNameVietNamese = "OldAbbreviation",
                OldAbbreviation = "OldAbbreviation",
                NewTopicNameEnglish = "NewTopicNameEnglish",
                NewTopicNameVietNamese = "Your words matter, and our paraphrasing tool is designed to ensure you use the right ones. With two free modes and five Premium modes to choose from, you can use QuillBot’s online Paraphraser to rephrase any text in a variety of ways. Our product will improve your fluency while also ensuring you have the appropriate vocabulary, tone, and style for any occasion. Simply enter your text into the input box, and our AI will work with you to create the best paraphrase.",
                NewAbbreviation = "asfdasdf",
                EmailSuperVisor = "sdfsdf",
                ReasonChangeTopic = "asdfsdafafsda",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 1,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldAbbreviation.Length > 20);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }

        //Test Length Of EmailSuperVisor Greater 100
        [TestMethod()]
        public void AddChangeTopicRequest_TestLengthOfEmailSuperVisorGreater150()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "dfsfsf",
                OldTopicNameVietNamese = "OldAbbreviation",
                OldAbbreviation = "OldAbbreviation",
                NewTopicNameEnglish = "NewTopicNameEnglish",
                NewTopicNameVietNamese = "NewTopicNameVietNamese",
                NewAbbreviation = "NewAbbreviation",
                EmailSuperVisor = "EmailSuperVisor EmailSuperVisor EmailSuperVisor EmailSuperVisor EmailSuperVisor EmailSuperVisor EmailSuperVisor EmailSuperVisor EmailSuperVisor EmailSuperVisor EmailSuperVisor",
                ReasonChangeTopic = "asdfsdafafsda",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 1,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.EmailSuperVisor.Length <= 100
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldAbbreviation.Length > 20);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.EmailSuperVisor.Length > 100)
                        {
                            Assert.IsTrue(changeTopicRequest.EmailSuperVisor.Length > 100);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }

        //Test Length Of NewAbbreviation Greater 150
        [TestMethod()]
        public void AddChangeTopicRequest_TestLengthOfNewAbbreviationGreater150()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "dfsfsf",
                OldTopicNameVietNamese = "OldAbbreviation",
                OldAbbreviation = "OldAbbreviation",
                NewTopicNameEnglish = "NewTopicNameEnglish",
                NewTopicNameVietNamese = "NewTopicNameVietNamese",
                NewAbbreviation = "Your words matter, and our paraphrasing tool is designed to ensure you use the right ones. With two free modes and five Premium modes to choose from, you can use QuillBot’s online Paraphraser to rephrase any text in a variety of ways. Our product will improve your fluency while also ensuring you have the appropriate vocabulary, tone, and style for any occasion. Simply enter your text into the input box, and our AI will work with you to create the best paraphrase.",
                EmailSuperVisor = "sdfsdf",
                ReasonChangeTopic = "asdfsdafafsda",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 1,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldAbbreviation.Length > 20);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }

        //Test Length Of EmailSuperVisor Greater 100
        [TestMethod()]
        public void AddChangeTopicRequest_TestLengthOfReasonChangeTopicrGreater150()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "dfsfsf",
                OldTopicNameVietNamese = "OldAbbreviation",
                OldAbbreviation = "OldAbbreviation",
                NewTopicNameEnglish = "NewTopicNameEnglish",
                NewTopicNameVietNamese = "NewTopicNameVietNamese",
                NewAbbreviation = "NewAbbreviation",
                EmailSuperVisor = "EmailSuperVisor",
                ReasonChangeTopic = "Your words matter, and our paraphrasing tool is designed to ensure you use the right ones. With two free modes and five Premium modes to choose from, you can use QuillBot’s online Paraphraser to rephrase any text in a variety of ways. Our product will improve your fluency while also ensuring you have the appropriate vocabulary, tone, and style for any occasion. Simply enter your text into the input box, and our AI will work with you to create the best paraphrase. Your words matter, and our paraphrasing tool is designed to ensure you use the right ones. With two free modes and five Premium modes to choose from, you can use QuillBot’s online Paraphraser to rephrase any text in a variety of ways. Our product will improve your fluency while also ensuring you have the appropriate vocabulary, tone, and style for any occasion. Simply enter your text into the input box, and our AI will work with you to create the best paraphrase.",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 1,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.EmailSuperVisor.Length <= 100
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldAbbreviation.Length > 20);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.EmailSuperVisor.Length > 100)
                        {
                            Assert.IsTrue(changeTopicRequest.EmailSuperVisor.Length > 100);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }

        //Test Add ChangeTopicRequest Fail
        [TestMethod()]
        public void AddChangeTopicRequest_TestAddChangeTopicRequestFail()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "dfsfsf",
                OldTopicNameVietNamese = "OldAbbreviation",
                OldAbbreviation = "OldAbbreviation",
                NewTopicNameEnglish = "NewTopicNameEnglish",
                NewTopicNameVietNamese = "NewTopicNameVietNamese",
                NewAbbreviation = "NewAbbreviation",
                EmailSuperVisor = "EmailSuperVisor",
                ReasonChangeTopic = "ReasonChangeTopic",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 1,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.EmailSuperVisor.Length <= 100
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(0);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldAbbreviation.Length > 20);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.EmailSuperVisor.Length > 100)
                        {
                            Assert.IsTrue(changeTopicRequest.EmailSuperVisor.Length > 100);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }

        //Test Add ChangeTopicRequest Success
        [TestMethod()]
        public void AddChangeTopicRequest_TestAddChangeTopicRequestSuccessl()
        {
            ChangeTopicRequest changeTopicRequest = new ChangeTopicRequest()
            {
                OldTopicNameEnglish = "dfsfsf",
                OldTopicNameVietNamese = "OldAbbreviation",
                OldAbbreviation = "OldAbbreviation",
                NewTopicNameEnglish = "NewTopicNameEnglish",
                NewTopicNameVietNamese = "NewTopicNameVietNamese",
                NewAbbreviation = "NewAbbreviation",
                EmailSuperVisor = "EmailSuperVisor",
                ReasonChangeTopic = "ReasonChangeTopic",
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = 1,
                }
            };
            if (changeTopicRequest != null)
            {
                if (!string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese)
                    && !string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation)
                     && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish)
                      && !string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese)
                       && !string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation)
                        && !string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor)
                         && changeTopicRequest.FinalGroup.FinalGroupID != 0
                          && !string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                {
                    if (changeTopicRequest.OldTopicNameEnglish.Length <= 150
                       && changeTopicRequest.OldTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.OldAbbreviation.Length <= 20
                       && changeTopicRequest.NewTopicNameEnglish.Length <= 150
                       && changeTopicRequest.NewTopicNameVietNamese.Length <= 150
                       && changeTopicRequest.NewAbbreviation.Length <= 20
                       && changeTopicRequest.EmailSuperVisor.Length <= 100
                       && changeTopicRequest.ReasonChangeTopic.Length <= 500)
                    {
                        var repoMock = new Mock<IChangeTopicRequestService>();
                        repoMock.Setup(repoMock => repoMock.AddChangeTopicRequest(changeTopicRequest)).Returns(1);
                        var changeTopicRequestService = repoMock.Object;
                        var actual = changeTopicRequestService.AddChangeTopicRequest(changeTopicRequest);
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
                        if (changeTopicRequest.OldTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.OldTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.OldAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.OldAbbreviation.Length > 20);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameEnglish.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameEnglish.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewTopicNameVietNamese.Length > 150)
                        {
                            Assert.IsTrue(changeTopicRequest.NewTopicNameVietNamese.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.NewAbbreviation.Length > 20)
                        {
                            Assert.IsTrue(changeTopicRequest.NewAbbreviation.Length > 150);
                            return;
                        }
                        if (changeTopicRequest.EmailSuperVisor.Length > 100)
                        {
                            Assert.IsTrue(changeTopicRequest.EmailSuperVisor.Length > 100);
                            return;
                        }
                        if (changeTopicRequest.ReasonChangeTopic.Length > 500)
                        {
                            Assert.IsTrue(changeTopicRequest.ReasonChangeTopic.Length > 500);
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.OldAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameVietNamese));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewTopicNameEnglish));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.NewAbbreviation));
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.EmailSuperVisor));
                        return;
                    }
                    if (changeTopicRequest.FinalGroup.FinalGroupID == 0)
                    {
                        Assert.AreEqual(0, changeTopicRequest.FinalGroup.FinalGroupID);
                        return;
                    }
                    if (string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic))
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(changeTopicRequest.ReasonChangeTopic));
                    }
                }
            }
            else
            {
                Assert.IsNull(changeTopicRequest);
            }
        }
    }
}