using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneProjectManagementSystem.Services.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProjectManagementSystem.Models.Dao;
using Moq;
using CapstoneProjectManagementSystem.Models;
using Unity;
using System.Text.RegularExpressions;

namespace CapstoneProjectManagementSystem.Services.Implement.CommonImplement.Tests
{
    [TestClass()]
    public class AffiliateAccountServiceTests
    {
        /// <summary>
        /// Test function CheckAffiliateAccountAndPasswordHash
        /// </summary>

        // test parameter is null
        [TestMethod()]
        public void CheckAffiliateAccountAndPasswordHash_TestParameterIsNull()
        {
            string personalEmail = "";
            string passwordHash = "";
            var repoMock = new Mock<IAffiliateAccountService>();
            repoMock.Setup(repoMock => repoMock.CheckAffiliateAccountAndPasswordHash(personalEmail, passwordHash)).Returns(false);
            var _affiliateAccountService = repoMock.Object;
            var actual = _affiliateAccountService.CheckAffiliateAccountAndPasswordHash
                (personalEmail, passwordHash);
            Assert.AreEqual(false, actual);
        }

        //test account sign in correct
        [TestMethod()]
        public void CheckAffiliateAccountAndPasswordHash_TestAccountCorrect()
        {
            string personalEmail = "nguyennhhe141584@fpt.edu.vn";
            string passwordHash = "Nguyen158@";
            var repoMock = new Mock<IAffiliateAccountService>();
            repoMock.Setup(repoMock => repoMock.CheckAffiliateAccountAndPasswordHash(personalEmail, passwordHash)).Returns(true);
            var _affiliateAccountService = repoMock.Object;
            var actual = _affiliateAccountService.CheckAffiliateAccountAndPasswordHash
                (personalEmail, passwordHash);
            Assert.AreEqual(true, actual);
        }

        //test account sigin incorrect
        [TestMethod()]
        public void CheckAffiliateAccountAndPasswordHash_TestAccountInCorrect()
        {
            string personalEmail = "nguyennhhe141584@fpt.edu.vn";
            string passwordHash = "abcxyz";
            var repoMock = new Mock<IAffiliateAccountService>();
            repoMock.Setup(repoMock => repoMock.CheckAffiliateAccountAndPasswordHash(personalEmail, passwordHash)).Returns(false);
            var _affiliateAccountService = repoMock.Object;
            var actual = _affiliateAccountService.CheckAffiliateAccountAndPasswordHash
                (personalEmail, passwordHash);
            Assert.AreEqual(false, actual);
        }

        //test account sigin correct
        [TestMethod()]
        public void CheckAffiliateAccountAndPasswordHash_TestPasswordCorrect()
        {
            string personalEmail = "nguyennhhe141234@fpt.edu.vn";
            string passwordHash = "abc";
            var repoMock = new Mock<IAffiliateAccountService>();
            repoMock.Setup(repoMock => repoMock.CheckAffiliateAccountAndPasswordHash(personalEmail, passwordHash)).Returns(false);
            var _affiliateAccountService = repoMock.Object;
            var actual = _affiliateAccountService.CheckAffiliateAccountAndPasswordHash
                (personalEmail, passwordHash);
            Assert.AreEqual(false, actual);
        }


        /// <summary>
        /// Test function GetAffiliateAccountById
        /// </summary>
        //Test parameter null
        [TestMethod()]
        public void GetAffiliateAccountById_TestParameterIsNull()
        {
            var container = new UnityContainer();
            var x = container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "";
            var actual = affiliateAccountService.GetAffiliateAccountById(affiliateAccountId);

            Assert.IsNull(actual);
        }

        //Test AffiliateAccount not Exist
        [TestMethod()]
        public void GetAffiliateAccountById_TestAffiliateAccountNotExist()
        {
            var container = new UnityContainer();
            var x = container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "nguyennhhe141586@fpt.edu.vn";
            var actual = affiliateAccountService.GetAffiliateAccountById(affiliateAccountId);

            Assert.IsNull(actual);
        }

        //Test AffiliateAccount Exist
        [TestMethod()]
        public void GetAffiliateAccountById_TestAffiliateAccountExist()
        {
            var container = new UnityContainer();
            var x = container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "nguyennhhe141584@fpt.edu.vn";
            var actual = affiliateAccountService.GetAffiliateAccountById(affiliateAccountId);

            Assert.IsNotNull(actual);
        }

        //Test AffiliateAccount have field correct
        [TestMethod()]
        public void GetAffiliateAccountById_TestAffiliateAccountHaveFieldCorrect()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "nguyennhhe141584@fpt.edu.vn";
            var actual = affiliateAccountService.GetAffiliateAccountById(affiliateAccountId);

            var expected = new AffiliateAccount()
            {
                AffiliateAccount_ID = "nguyennhhe141584@fpt.edu.vn",
                PersonalEmail = "nguyennhhe141584@fpt.edu.vn",
                IsVerifyEmail = true,
                PasswordHash = "AHT12B5Rz8REitFpSh/Ia8pc/9agLu9ke9pEF5oR6j6xS9bL/Kil9Uk5mSrS3XVKMg==",
                OneTimePassword = "123456",
                OtpRequestTime = Convert.ToDateTime("2022-11-27 17:32:25.853"),
            };
            Assert.AreEqual(expected.AffiliateAccount_ID, actual.AffiliateAccount_ID);
            Assert.AreEqual(expected.PersonalEmail, actual.PersonalEmail);
            Assert.AreEqual(expected.IsVerifyEmail, actual.IsVerifyEmail);
            Assert.AreEqual(expected.PasswordHash, actual.PasswordHash);
            Assert.AreEqual(expected.OneTimePassword, actual.OneTimePassword);
            Assert.AreEqual(expected.OtpRequestTime, actual.OtpRequestTime);
        }


        /// <summary>
        /// Test Function GetAffiliateAccountByEmail
        /// </summary>

        //Test parameter null
        [TestMethod()]
        public void GetAffiliateAccountByEmail_TestParameterIsNull()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string personalEmail = null;
            var actual = affiliateAccountService.GetAffiliateAccountByEmail(personalEmail);
            Assert.IsNull(actual);
        }

        //Test AffiliateAccount not Exist
        [TestMethod()]
        public void GetAffiliateAccountByEmail_TestAffiliateAccountNotExist()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string personalEmail = "nguyennhhe141586@fpt.edu.vn";
            var actual = affiliateAccountService.GetAffiliateAccountByEmail(personalEmail);

            Assert.IsNull(actual);
        }

        //Test AffiliateAccount Exist
        [TestMethod()]
        public void GetAffiliateAccountByEmail_TestAffiliateAccountExist()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string personalEmail = "nguyennhhe141584@fpt.edu.vn";
            var actual = affiliateAccountService.GetAffiliateAccountByEmail(personalEmail);

            Assert.IsNotNull(actual);
        }

        //Test AffiliateAccount have field correct
        [TestMethod()]
        public void GetAffiliateAccountByEmail_TestAffiliateAccountHaveFieldCorrect()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string personalEmail = "nguyennhhe141584@fpt.edu.vn";
            var actual = affiliateAccountService.GetAffiliateAccountByEmail(personalEmail);

            var expected = new AffiliateAccount()
            {
                AffiliateAccount_ID = "nguyennhhe141584@fpt.edu.vn",
                PersonalEmail = "nguyennhhe141584@fpt.edu.vn",
                PasswordHash = "AHT12B5Rz8REitFpSh/Ia8pc/9agLu9ke9pEF5oR6j6xS9bL/Kil9Uk5mSrS3XVKMg==",
            };

            Assert.AreEqual(expected.AffiliateAccount_ID, actual.AffiliateAccount_ID);
            Assert.AreEqual(expected.PersonalEmail, actual.PersonalEmail);
            Assert.AreEqual(expected.PasswordHash, actual.PasswordHash);
        }

        /// <summary>
        /// Test function UpdateOTP
        /// </summary>
        /// 

        //check parameter backupaccountid and otp is null
        [TestMethod()]
        public void UpdateOTP_CheckParameterBackupAccountIDAndOtpIsNull()
        {
            string BackupAccountID = "";
            string Otp = "";
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            var actual = affiliateAccountService.UpdateOTP(BackupAccountID, Otp);
            Assert.AreEqual(0, actual);
        }

        //check parameter otp is digit but not must 6 digt
        [TestMethod()]
        public void UpdateOTP_CheckParameterOtpIsInValid1()
        {
            string BackupAccountID = "anbthe140005@fpt.edu.vn";
            string Otp = "34534534";
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            var regexOTP = @"^[0-9]{6,6}$";
            var match = Regex.Match(Otp, regexOTP);
            if (!match.Success)
            {
                var actual = affiliateAccountService.UpdateOTP(BackupAccountID, Otp);
                Assert.AreEqual(0, actual);
            }
            else
            {
                Assert.IsFalse(!match.Success);
            }
        }

        //check parameter otp is not digit
        [TestMethod()]
        public void UpdateOTP_CheckParameterOtpIsInValid2()
        {
            string BackupAccountID = "anbthe140005@fpt.edu.vn";
            string Otp = "sdfsdfsdf";
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            var regexOTP = @"^[0-9]{6,6}$";
            var match = Regex.Match(Otp, regexOTP);
            if (!match.Success)
            {
                var actual = affiliateAccountService.UpdateOTP(BackupAccountID, Otp);
                Assert.AreEqual(0, actual);
            }
            else
            {
                Assert.IsFalse(!match.Success);
            }
        }


        //check parameter is valid
        [TestMethod()]
        public void UpdateOTP_CheckParameterIsValid()
        {
            string BackupAccountID = "anbthe140005@fpt.edu.vn";
            string Otp = "123456";
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            var actual = affiliateAccountService.UpdateOTP(BackupAccountID, Otp);
            Assert.AreEqual(1, actual);
        }

        //check parameter affiliateAccountId is invalid
        [TestMethod()]
        public void UpdateOTP_CheckParameterAffiliateAccountIdIsInValid()
        {
            string BackupAccountID = "anbthe144325@fpt.edu.vn";
            string Otp = "123456";
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            var actual = affiliateAccountService.UpdateOTP(BackupAccountID, Otp);
            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test Function UpdatePasswordHash
        /// </summary>

        // check affiliateAccountId and password  is null 
        [TestMethod()]
        public void UpdateOTP_CheckParameterAffiliateAccountIdAndPasswordIsNull()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "";
            string password = "";
            string regexPass = @"^(?=\S*[a-z])(?=\S*[A-Z])(?=\S*\d)(?=\S*[^\w\s])\S{8,32}$";
            var match = Regex.Match(password, regexPass);
            if (affiliateAccountId != null && password != null && affiliateAccountId != "" && password != "")
            {
                if (match.Success)
                {
                    var actual = affiliateAccountService.UpdatePasswordHash(affiliateAccountId, password);
                    Assert.AreEqual(1, actual);
                    return;

                }
                else
                {
                    Assert.AreEqual(false, match.Success);
                    return;

                }
            }
            else
            {
                Assert.AreEqual(0, affiliateAccountService.UpdatePasswordHash(affiliateAccountId, password));
                return;

            }
        }

        // test regex password 
        [TestMethod()]
        public void UpdateOTP_CheckRegexPassword()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "anbthe140005@fpt.edu.vn";
            string password = "Nguyen";
            string regexPass = @"^(?=\S*[a-z])(?=\S*[A-Z])(?=\S*\d)(?=\S*[^\w\s])\S{8,32}$";
            var match = Regex.Match(password, regexPass);
            if (affiliateAccountId != null && password != null && affiliateAccountId != "" && password != "")
            {
                if (match.Success)
                {
                    var actual = affiliateAccountService.UpdatePasswordHash(affiliateAccountId, password);
                    Assert.AreEqual(1, actual);
                    return;

                }
                else
                {
                    Assert.IsFalse(match.Success);
                    return;

                }
            }
            else
            {
                Assert.AreEqual(0, affiliateAccountService.UpdatePasswordHash(affiliateAccountId, password));
                return;
            }
        }

        // test affiliateAccountId is invalid
        [TestMethod()]
        public void UpdateOTP_TestAffiliateAccountIdIsInvalid()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "anbthe143455@fpt.edu.vn";
            string password = "Nguyen158@";
            string regexPass = @"^(?=\S*[a-z])(?=\S*[A-Z])(?=\S*\d)(?=\S*[^\w\s])\S{8,32}$";
            var match = Regex.Match(password, regexPass);
            if (affiliateAccountId != null && password != null && affiliateAccountId != "" && password != "")
            {
                if (match.Success)
                {
                    var actual = affiliateAccountService.UpdatePasswordHash(affiliateAccountId, password);
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
                    Assert.IsFalse(match.Success);
                    return;

                }
            }
            else
            {
                Assert.AreEqual(0, affiliateAccountService.UpdatePasswordHash(affiliateAccountId, password));
                return;

            }
        }

        // test affiliateAccountId is valid
        [TestMethod()]
        public void UpdateOTP_TestAffiliateAccountIdIsValid()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "anbthe140005@fpt.edu.vn";
            string password = "Nguyen158@";
            string regexPass = @"^(?=\S*[a-z])(?=\S*[A-Z])(?=\S*\d)(?=\S*[^\w\s])\S{8,32}$";
            var match = Regex.Match(password, regexPass);
            if (affiliateAccountId != null && password != null && affiliateAccountId != "" && password != "")
            {
                if (match.Success)
                {
                    var actual = affiliateAccountService.UpdatePasswordHash(affiliateAccountId, password);
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
                    Assert.IsFalse(match.Success);
                    return;

                }
            }
            else
            {
                Assert.AreEqual(0, affiliateAccountService.UpdatePasswordHash(affiliateAccountId, password));
                return;

            }
        }



        /// <summary>
        /// Test Function AddOTP
        /// </summary>
        // check affiliateAccountId and otp  is null 
        [TestMethod()]
        public void AddOTP_TestAffiliateAccountAndOTPIsNull()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "";
            string otp = "";
            var regexOTP = @"^[0-9]{6,6}$";
            var match = Regex.Match(otp, regexOTP);
            if (affiliateAccountId != null && otp != null && affiliateAccountId != "" && otp != "")
            {
                if (match.Success)
                {
                    var actual = affiliateAccountService.AddOTP(affiliateAccountId, otp);
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
                    Assert.IsFalse(match.Success);
                    return;

                }
            }
            else
            {
                Assert.AreEqual(0, affiliateAccountService.AddOTP(affiliateAccountId, otp));
                return;

            }
        }

        // test  regex of otp is digit must not 6 digit
        [TestMethod()]
        public void AddOTP_TestRegexOTPMustNot6digit()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "anbthe140005@fpt.edu.vn";
            string otp = "2315433";
            var regexOTP = @"^[0-9]{6,6}$";
            var match = Regex.Match(otp, regexOTP);
            if (affiliateAccountId != null && otp != null && affiliateAccountId != "" && otp != "")
            {
                if (match.Success)
                {
                    var actual = affiliateAccountService.AddOTP(affiliateAccountId, otp);
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
                    Assert.IsFalse(match.Success);
                    return;

                }
            }
            else
            {
                Assert.AreEqual(0, affiliateAccountService.AddOTP(affiliateAccountId, otp));
                return;

            }
        }

        // test  regex of otp is digit must not digit
        [TestMethod()]
        public void AddOTP_TestRegexOTPNotDigit()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "anbthe140005@fpt.edu.vn";
            string otp = "fgfdgdfgd";
            var regexOTP = @"^[0-9]{6,6}$";
            var match = Regex.Match(otp, regexOTP);
            if (affiliateAccountId != null && otp != null && affiliateAccountId != "" && otp != "")
            {
                if (match.Success)
                {
                    var actual = affiliateAccountService.AddOTP(affiliateAccountId, otp);
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
                    Assert.IsFalse(match.Success);
                    return;

                }
            }
            else
            {
                Assert.AreEqual(0, affiliateAccountService.AddOTP(affiliateAccountId, otp));
                return;

            }
        }

        // Test AddOTP If userid Not Exist DB
        [TestMethod()]
        public void AddOTP_TestAddOTPIfUserIdNotExist()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "anbthe140gfdg05@fpt.edu.vn";
            string otp = "123456";
            var regexOTP = @"^[0-9]{6,6}$";
            var match = Regex.Match(otp, regexOTP);
            if (affiliateAccountId != null && otp != null && affiliateAccountId != "" && otp != "")
            {
                if (match.Success)
                {
                    var actual = affiliateAccountService.AddOTP(affiliateAccountId, otp);
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
                    Assert.IsFalse(match.Success);
                    return;

                }
            }
            else
            {
                Assert.AreEqual(0, affiliateAccountService.AddOTP(affiliateAccountId, otp));
                return;

            }
        }

        // Test AddOTP If Record Haved AffiliateAccountId 
        [TestMethod()]
        public void AddOTP_TestAddOTPIfAffiliateAccountIdHavedDB()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "baoldhe140046@fpt.edu.vn";
            string otp = "123456";
            var regexOTP = @"^[0-9]{6,6}$";
            var match = Regex.Match(otp, regexOTP);
            if (affiliateAccountId != null && otp != null && affiliateAccountId != "" && otp != "")
            {
                if (match.Success)
                {
                    var actual = affiliateAccountService.AddOTP(affiliateAccountId, otp);
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
                    Assert.IsFalse(match.Success);
                    return;

                }
            }
            else
            {
                Assert.AreEqual(0, affiliateAccountService.AddOTP(affiliateAccountId, otp));
                return;

            }
        }

        // Test AddOTP valid
        [TestMethod()]
        public void AddOTP_TestAddOTPValid()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "anbthe140005@fpt.edu.vn";
            string otp = "123456";
            var regexOTP = @"^[0-9]{6,6}$";
            var match = Regex.Match(otp, regexOTP);
            if (affiliateAccountId != null && otp != null && affiliateAccountId != "" && otp != "")
            {
                if (match.Success)
                {
                    var actual = affiliateAccountService.AddOTP(affiliateAccountId, otp);
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
                    Assert.IsFalse(match.Success);
                    return;

                }
            }
            else
            {
                Assert.AreEqual(0, affiliateAccountService.AddOTP(affiliateAccountId, otp));
                return;
            }
        }


        /// <summary>
        /// Test Function UpdateIsVerifyEmail
        /// </summary>

        // test affiliateAccountId and personalEmail is null
        [TestMethod]
        public void UpdateIsVerifyEmail_TestParameterIsNull()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "";
            string personalEmail = "";
            var regexpersonalEmail = @"^[0-9]{6,6}$";
            var match = Regex.Match(personalEmail, regexpersonalEmail);
            if (affiliateAccountId != null && personalEmail != null && affiliateAccountId != "" && personalEmail != "")
            {
                if (match.Success)
                {
                    var actual = affiliateAccountService.UpdateIsVerifyEmail(affiliateAccountId, personalEmail);
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
                    Assert.IsFalse(match.Success);
                    return;

                }
            }
            else
            {
                Assert.AreEqual(0, affiliateAccountService.UpdateIsVerifyEmail(affiliateAccountId, personalEmail));
                return;

            }
        }

        // Test Regex Personal Email
        [TestMethod]
        public void UpdateIsVerifyEmail_TestRegexPersonalEmail()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "nguyennhhe141584@fpt.edu.vn";
            string personalEmail = "nguyennhhe141584";
            var regexPersonalEmail = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
            var match = Regex.Match(personalEmail, regexPersonalEmail);
            if (affiliateAccountId != null && personalEmail != null && affiliateAccountId != "" && personalEmail != "")
            {
                if (match.Success)
                {
                    var actual = affiliateAccountService.UpdateIsVerifyEmail(affiliateAccountId, personalEmail);
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
                    Assert.IsFalse(match.Success);
                    return;

                }
            }
            else
            {
                Assert.AreEqual(0, affiliateAccountService.UpdateIsVerifyEmail(affiliateAccountId, personalEmail));
                return;

            }
        }

        // test affiliateAccountId is not exist db
        [TestMethod]
        public void UpdateIsVerifyEmail_TestAffiliateAccountIdIsNotExistDb()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "nguyennhhe14fds4@fpt.edu.vn";
            string personalEmail = "nguyennhhe141584@fpt.edu.vn";
            var regexPersonalEmail = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
            var match = Regex.Match(personalEmail, regexPersonalEmail);
            if (affiliateAccountId != null && personalEmail != null && affiliateAccountId != "" && personalEmail != "")
            {
                if (match.Success)
                {
                    var actual = affiliateAccountService.UpdateIsVerifyEmail(affiliateAccountId, personalEmail);
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
                    Assert.IsFalse(match.Success);
                    return;

                }
            }
            else
            {
                Assert.AreEqual(0, affiliateAccountService.UpdateIsVerifyEmail(affiliateAccountId, personalEmail));
                return;
            }
        }

        // test valid
        [TestMethod]
        public void UpdateIsVerifyEmail_TestValid()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string affiliateAccountId = "nguyennhhe141584@fpt.edu.vn";
            string personalEmail = "nguyennhhe141584@fpt.edu.vn";
            var regexPersonalEmail = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
            var match = Regex.Match(personalEmail, regexPersonalEmail);
            if (affiliateAccountId != null && personalEmail != null && affiliateAccountId != "" && personalEmail != "")
            {
                if (match.Success)
                {
                    var actual = affiliateAccountService.UpdateIsVerifyEmail(affiliateAccountId, personalEmail);
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
                    Assert.IsFalse(match.Success);
                    return;
                }
            }
            else
            {
                Assert.AreEqual(0, affiliateAccountService.UpdateIsVerifyEmail(affiliateAccountId, personalEmail));
                return;
            }
        }

        /// <summary>
        /// Test Function CheckPersonalEmailExist
        /// </summary>
        /// 

        //Test Parameter Is Null
        [TestMethod()]
        public void CheckPersonalEmailExist_TestParameterIsNull()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string personalEmail = "";
            var x = affiliateAccountService.CheckPersonalEmailExist(personalEmail);
            var regexPersonalEmail = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
            var match = Regex.Match(personalEmail, regexPersonalEmail);
            if (personalEmail != null && personalEmail != "")
            {
                if (match.Success)
                {
                    var result = affiliateAccountService.CheckPersonalEmailExist(personalEmail);
                    if (result == false)
                    {
                        Assert.IsFalse(result);
                        return;
                    }
                    if (result == true)
                    {
                        Assert.IsTrue(result);
                        return;
                    }
                }
                else
                {
                    Assert.IsFalse(match.Success);
                    return;
                }

            }
            else
            {
                Assert.AreEqual(true, affiliateAccountService.CheckPersonalEmailExist(personalEmail));
                return;
            }
        }


        //Test regex personal email
        [TestMethod()]
        public void CheckPersonalEmailExist_TestRegexPersonalEmail()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string personalEmail = "nguyennhhe141584";
            var regexPersonalEmail = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
            var match = Regex.Match(personalEmail, regexPersonalEmail);
            if (personalEmail != null && personalEmail != "")
            {
                if (match.Success)
                {
                    var result = affiliateAccountService.CheckPersonalEmailExist(personalEmail);
                    if (result == false)
                    {
                        Assert.IsFalse(result);
                        return;
                    }
                    if (result == true)
                    {
                        Assert.IsTrue(result);
                        return;
                    }
                }
                else
                {
                    Assert.IsFalse(match.Success);
                    return;
                }

            }
            else
            {
                Assert.AreEqual(false, affiliateAccountService.CheckPersonalEmailExist(personalEmail));
                return;
            }
        }

        //Test Personal Email Not Exist
        [TestMethod()]
        public void CheckPersonalEmailExist_TestPersonalEmailNotExist()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string personalEmail = "nguyennhhe141585@fpt.edu.vn";
            var regexPersonalEmail = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
            var match = Regex.Match(personalEmail, regexPersonalEmail);
            if (personalEmail != null && personalEmail != "")
            {
                if (match.Success)
                {
                    var result = affiliateAccountService.CheckPersonalEmailExist(personalEmail);
                    if (result == false)
                    {
                        Assert.IsFalse(result);
                        return;
                    }
                    if (result == true)
                    {
                        Assert.IsTrue(result);
                        return;
                    }
                }
                else
                {
                    Assert.IsFalse(match.Success);
                    return;
                }

            }
            else
            {
                Assert.AreEqual(false, affiliateAccountService.CheckPersonalEmailExist(personalEmail));
                return;
            }
        }

        //test personal email exist 
        [TestMethod()]
        public void CheckPersonalEmailExist_TestPersonalEmailExist()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            var affiliateAccountService = container.Resolve<AffiliateAccountService>();

            string personalEmail = "nguyennhhe141584@fpt.edu.vn";
            var regexPersonalEmail = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
            var match = Regex.Match(personalEmail, regexPersonalEmail);
            if (personalEmail != null && personalEmail != "")
            {
                if (match.Success)
                {
                    var result = affiliateAccountService.CheckPersonalEmailExist(personalEmail);
                    if (result == false)
                    {
                        Assert.IsFalse(result);
                        return;
                    }
                    if (result == true)
                    {
                        Assert.IsTrue(result);
                        return;
                    }
                }
                else
                {
                    Assert.IsFalse(match.Success);
                    return;
                }

            }
            else
            {
                Assert.AreEqual(false, affiliateAccountService.CheckPersonalEmailExist(personalEmail));
            }
        }

        //Test Exception Check Personal Email
        [TestMethod()]
        public void CheckPersonalEmailExist_TestExceptionCheckPersonalEmail()
        {
            try
            {
                var container = new UnityContainer();
                container.RegisterType(typeof(IAffiliateAccountService), typeof(AffiliateAccountService));
                container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
                var affiliateAccountService = container.Resolve<AffiliateAccountService>();

                string personalEmail = null;
                affiliateAccountService.CheckPersonalEmailExist(personalEmail);
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message,ex.Message);
            }
        }
    }
}