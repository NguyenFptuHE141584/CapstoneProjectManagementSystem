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
    public class SupportServiceTests
    {
        /// <summary>
        /// Test function GetAllPendingRequestTest()
        /// </summary>
        [TestMethod()]
        public void GetAllPendingRequestTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISupportService), typeof(SupportService));
            var semesterService = container.Resolve<SupportService>();

            var actual = semesterService.GetAllPendingRequest();

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetAllProcessedRequestTest()
        /// </summary>
        [TestMethod()]
        public void GetAllProcessedRequestTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISupportService), typeof(SupportService));
            var semesterService = container.Resolve<SupportService>();

            var actual = semesterService.GetAllProcessedRequest();

            Assert.IsNull(actual);
        }
    }
}