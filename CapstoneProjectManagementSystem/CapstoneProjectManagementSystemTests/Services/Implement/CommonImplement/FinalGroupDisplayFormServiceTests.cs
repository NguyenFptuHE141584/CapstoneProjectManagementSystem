using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneProjectManagementSystem.Services.Implement.CommonImplement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using CapstoneProjectManagementSystem.Models;

namespace CapstoneProjectManagementSystem.Services.Implement.CommonImplement.Tests
{
    [TestClass()]
    public class FinalGroupDisplayFormServiceTests
    {
        /// <summary>
        /// Test function ConvertFromFinalListTest()
        /// </summary>
        // list have elements
        [TestMethod()]
        public void ConvertFromFinalListTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupDisplayFormService), typeof(FinalGroupDisplayFormService));
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalGroupDisplayFormService = container.Resolve<FinalGroupDisplayFormService>();

            var finalGroup = new FinalGroup() { 
                FinalGroupID = 1,
                GroupName = "Group 1",
                ProjectEnglishName = "Eng 1",
                Specialty = new Specialty()
                {
                    SpecialtyID = 1
                },
                MaxMember = 5,
                NumberOfMember = 5,
                CreatedAt = DateTime.Now
            };
            var finalGroupList = new List<FinalGroup>();
            finalGroupList.Add(finalGroup);
            var actual = finalGroupDisplayFormService.ConvertFromFinalList(finalGroupList);

            var specialtyFullname = "Blockchain";
            Assert.AreEqual(specialtyFullname, actual[0].SpecialtyFullName);
        }
        // list is null 
        [TestMethod()]
        public void ConvertFromFinalListTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupDisplayFormService), typeof(FinalGroupDisplayFormService));
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalGroupDisplayFormService = container.Resolve<FinalGroupDisplayFormService>();

            var finalGroupList = new List<FinalGroup>();
            var actual = finalGroupDisplayFormService.ConvertFromFinalList(finalGroupList);

            Assert.AreEqual(0,actual.Count);
        }
    }
}