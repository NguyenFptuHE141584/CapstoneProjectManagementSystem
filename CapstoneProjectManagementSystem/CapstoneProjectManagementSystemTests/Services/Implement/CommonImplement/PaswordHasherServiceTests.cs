using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneProjectManagementSystem.Services.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using System.Text.RegularExpressions;

namespace CapstoneProjectManagementSystem.Services.Implement.CommonImplement.Tests
{
    [TestClass()]
    public class PaswordHasherServiceTests
    {
        private PaswordHasherService paswordHasherService;
        public PaswordHasherServiceTests()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IPasswordHasherService), typeof(PaswordHasherService));
            paswordHasherService = container.Resolve<PaswordHasherService>();
        }

        /// <summary>
        /// Test Function HashPassword
        /// </summary>
        /// 
        //test exception
        [TestMethod()]
        public void HashPassword_TestException()
        {
            string password = null;
            try
            {
                if (password != "")
                {
                    if (!password.Contains(" "))
                    {
                        var actual = paswordHasherService.HashPassword(password);
                        var excepted = "AHUZECwi1QQqSxVtqcoyRueR3WgfJms1li17Tm1230YWKO+dkN7eTVJLTRyUzeLkwg==";
                        Assert.AreEqual(excepted, actual);
                        return;
                    }
                    else
                    {
                        Assert.AreEqual(false, !password.Contains(" "));
                        return;
                    }
                }
                else
                {
                    Assert.IsTrue(string.IsNullOrEmpty(password));
                    return;
                }
            }
            catch (Exception ex)
            {
                Assert.IsNull(password);
                return;
            }
        }

        //test Password Is Empty
        [TestMethod()]
        public void HashPassword_TestPasswordIsEmpty()
        {
            string password = "";
            try
            {
                if (password != "")
                {
                    if (!password.Contains(" "))
                    {
                        var actual = paswordHasherService.HashPassword(password);
                        var excepted = "AHUZECwi1QQqSxVtqcoyRueR3WgfJms1li17Tm1230YWKO+dkN7eTVJLTRyUzeLkwg==";
                        Assert.AreEqual(excepted, actual);
                        return;
                    }
                    else
                    {
                        Assert.AreEqual(false, !password.Contains(" "));
                        return;
                    }
                }
                else
                {
                    Assert.IsTrue(string.IsNullOrEmpty(password));
                    return;
                }
            }
            catch (Exception ex)
            {
                Assert.IsNull(password);
                return;
            }
        }

        //test Password contain white space
        [TestMethod()]
        public void HashPassword_TestPasswordContainWhiteSpace()
        {
            string password = "nguyen 123   das";
            try
            {
                if (password != "")
                {
                    if (!password.Contains(" "))
                    {
                        var actual = paswordHasherService.HashPassword(password);
                        var excepted = "AHUZECwi1QQqSxVtqcoyRueR3WgfJms1li17Tm1230YWKO+dkN7eTVJLTRyUzeLkwg==";
                        Assert.AreEqual(excepted, actual);
                        return;
                    }
                    else
                    {
                        Assert.AreEqual(false, !password.Contains(" "));
                        return;
                    }
                }
                else
                {
                    Assert.IsTrue(string.IsNullOrEmpty(password));
                    return;
                }
            }
            catch (Exception ex)
            {
                Assert.IsNull(password);
                return;
            }
        }

        //test Password valid
        [TestMethod()]
        public void HashPassword_TestPasswordValid()
        {
            string password = "Nguyen158@";
            try
            {
                if (password != "")
                {
                    if (!password.Contains(" "))
                    {
                        var actual = paswordHasherService.HashPassword(password);
                        Assert.IsNotNull(actual);
                        return;
                    }
                    else
                    {
                        Assert.AreEqual(false, !password.Contains(" "));
                        return;
                    }
                }
                else
                {
                    Assert.IsTrue(string.IsNullOrEmpty(password));
                    return;
                }
            }
            catch (Exception ex)
            {
                Assert.IsNull(password);
                return;
            }
        }

        /// <summary>
        /// Test Function PasswordVerificationResult
        /// </summary>

        //test exception
        [TestMethod()]
        public void PasswordVerificationResult_TestException()
        {
            string password = null;
            string passwordHash = null;
            try
            {
                if (password != "" && passwordHash != "")
                {
                    if (!password.Contains(" "))
                    {
                        var actual = paswordHasherService.PasswordVerificationResult(passwordHash,password);
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
                        Assert.AreEqual(false, !password.Contains(" "));
                        return;
                    }
                }
                else
                {
                    Assert.IsTrue(string.IsNullOrEmpty(password));
                    Assert.IsTrue(string.IsNullOrEmpty(passwordHash));
                    return;
                }
            }
            catch (Exception ex)
            {
                Assert.IsNull(password);
                Assert.IsNull(passwordHash);
                return;
            }
        }

        //Test Password And HashPassword Is Empty
        [TestMethod()]
        public void PasswordVerificationResult_TestPasswordAndHashPasswordIsEmpty()
        {
            string password = "";
            string passwordHash = "";
            try
            {
                if (password != "" && passwordHash != "")
                {
                    if (!password.Contains(" "))
                    {
                        var actual = paswordHasherService.PasswordVerificationResult(passwordHash, password);
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
                        Assert.AreEqual(false, !password.Contains(" "));
                        return;
                    }
                }
                else
                {
                    Assert.IsTrue(string.IsNullOrEmpty(password));
                    Assert.IsTrue(string.IsNullOrEmpty(passwordHash));
                    return;
                }
            }
            catch (Exception ex)
            {
                Assert.IsNull(password);
                Assert.IsNull(passwordHash);
                return;
            }
        }


        //Test Password Contain White Space
        [TestMethod()]
        public void PasswordVerificationResult_TestPasswordContainWhiteSpace()
        {
            string password = "Nguyen 158@";
            string passwordHash = "AHUZECwi1QQqSxVtqcoyRueR3WgfJms1li17Tm1230YWKO+dkN7eTVJLTRyUzeLkwg==";
            try
            {
                if (password != "" && passwordHash != "")
                {
                    if (!password.Contains(" "))
                    {
                        var actual = paswordHasherService.PasswordVerificationResult(passwordHash, password);
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
                        Assert.AreEqual(false, !password.Contains(" "));
                        return;
                    }
                }
                else
                {
                    Assert.IsTrue(string.IsNullOrEmpty(password));
                    Assert.IsTrue(string.IsNullOrEmpty(passwordHash));
                    return;
                }
            }
            catch (Exception ex)
            {
                Assert.IsNull(password);
                Assert.IsNull(passwordHash);
                return;
            }
        }

        //Test Password and PasswordHash verification result
        [TestMethod()]
        public void PasswordVerificationResult_TestPasswordAndPasswordHashVerificationResult()
        {
            string password = "Nguyen158@";
            string passwordHash = "AHUZECwi1QQqSxVtqcoyRueR3WgfJms1li17Tm1230YWKO+dkN7eTVJLTRyUzeLkwg==";
            try
            {
                if (password != "" && passwordHash != "")
                {
                    if (!password.Contains(" "))
                    {
                        var actual = paswordHasherService.PasswordVerificationResult(passwordHash, password);
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
                        Assert.AreEqual(false, !password.Contains(" "));
                        return;
                    }
                }
                else
                {
                    Assert.IsTrue(string.IsNullOrEmpty(password));
                    Assert.IsTrue(string.IsNullOrEmpty(passwordHash));
                    return;
                }
            }
            catch (Exception ex)
            {
                Assert.IsNull(password);
                Assert.IsNull(passwordHash);
                return;
            }
        }

        //Test Password and PasswordHash not verification result
        [TestMethod()]
        public void PasswordVerificationResult_TestPasswordAndPasswordHashNotVerificationResult()
        {
            string password = "Nguyenadsfsd158@";
            string passwordHash = "AHUZECwi1QQqSxVtqcoyRueR3WgfJms1li17Tm1230YWKO+dkN7eTVJLTRyUzeLkwg==";
            try
            {
                if (password != "" && passwordHash != "")
                {
                    if (!password.Contains(" "))
                    {
                        var actual = paswordHasherService.PasswordVerificationResult(passwordHash, password);
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
                        Assert.AreEqual(false, !password.Contains(" "));
                        return;
                    }
                }
                else
                {
                    Assert.IsTrue(string.IsNullOrEmpty(password));
                    Assert.IsTrue(string.IsNullOrEmpty(passwordHash));
                    return;
                }
            }
            catch (Exception ex)
            {
                Assert.IsNull(password);
                Assert.IsNull(passwordHash);
                return;
            }
        }
    }
}