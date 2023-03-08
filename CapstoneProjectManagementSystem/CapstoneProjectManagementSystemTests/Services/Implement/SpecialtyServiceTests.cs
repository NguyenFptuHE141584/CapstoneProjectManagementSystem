using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneProjectManagementSystem.Services.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Moq;
using CapstoneProjectManagementSystem.Models;

namespace CapstoneProjectManagementSystem.Services.Implement.Tests
{
    [TestClass()]
    public class SpecialtyServiceTests
    {
        /// <summary>
        /// Test function getAllSpecialtyTest()
        /// </summary>
        // correct semester id 
        [TestMethod()]
        public void getAllSpecialtyTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            var specialtyService = container.Resolve<SpecialtyService>();

            var semesterId = 1;
            var actual = specialtyService.getAllSpecialty(semesterId);

            var specialty = new Specialty()
            {
                SpecialtyID = 1,
                Profession = new Profession()
                {
                    ProfessionID = 1
                },
                SpecialtyAbbreviation = "BC",
                SpecialtyFullName = "Blockchain",
                MaxMember = 5,
                CodeOfGroupName = "IAP491",
                Semester = new Semester()
                {
                    SemesterID = 1
                }
            };
            var specialtyList = new List<Specialty>();
            specialtyList.Add(specialty);
            Assert.AreEqual(specialtyList[0].SpecialtyID, actual[0].SpecialtyID);
            Assert.AreEqual(specialtyList[0].Profession.ProfessionID, actual[0].Profession.ProfessionID);
            Assert.AreEqual(specialtyList[0].SpecialtyAbbreviation, actual[0].SpecialtyAbbreviation);
            Assert.AreEqual(specialtyList[0].SpecialtyFullName, actual[0].SpecialtyFullName);
            Assert.AreEqual(specialtyList[0].MaxMember, actual[0].MaxMember);
            Assert.AreEqual(specialtyList[0].CodeOfGroupName, actual[0].CodeOfGroupName);
            Assert.AreEqual(specialtyList[0].Semester.SemesterID, actual[0].Semester.SemesterID);
        }
        // wrong semester id 
        [TestMethod()]
        public void getAllSpecialtyTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            var specialtyService = container.Resolve<SpecialtyService>();

            var semesterId = 0;
            var actual = specialtyService.getAllSpecialty(semesterId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function getSpecialtyByIdTest()
        /// </summary>
        //exist specialty
        [TestMethod()]
        public void getSpecialtyByIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            var specialtyService = container.Resolve<SpecialtyService>();

            var specialtyId = 1;
            var actual = specialtyService.getSpecialtyById(specialtyId);

            var specialty = new Specialty()
            {
                SpecialtyID = 1,
                Profession = new Profession()
                {
                    ProfessionID = 1
                },
                SpecialtyAbbreviation = "BC",
                SpecialtyFullName = "Blockchain",
                MaxMember = 5,
                CodeOfGroupName = "IAP491",
                Semester = new Semester()
                {
                    SemesterID = 1
                }
            };
            Assert.AreEqual(specialty.SpecialtyID, actual.SpecialtyID);
            Assert.AreEqual(specialty.Profession.ProfessionID, actual.Profession.ProfessionID);
            Assert.AreEqual(specialty.SpecialtyAbbreviation, actual.SpecialtyAbbreviation);
            Assert.AreEqual(specialty.SpecialtyFullName, actual.SpecialtyFullName);
            Assert.AreEqual(specialty.MaxMember, actual.MaxMember);
            Assert.AreEqual(specialty.CodeOfGroupName, actual.CodeOfGroupName);
            Assert.AreEqual(specialty.Semester.SemesterID, actual.Semester.SemesterID);
        }
        //not exist specialty
        [TestMethod()]
        public void getSpecialtyByIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            var specialtyService = container.Resolve<SpecialtyService>();

            var specialtyId = 100;
            var actual = specialtyService.getSpecialtyById(specialtyId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetSpecialtyByNameTest()
        /// </summary>
        // correct specialty name and semester id
        [TestMethod()]
        public void GetSpecialtyByNameTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            var specialtyService = container.Resolve<SpecialtyService>();

            var specialtyName = "Blockchain";
            var semesterId = 1;
            var actual = specialtyService.GetSpecialtyByName(specialtyName, semesterId);

            var specialty = new Specialty()
            {
                SpecialtyID = 1,
                Profession = new Profession()
                {
                    ProfessionID = 1
                },
                SpecialtyAbbreviation = "BC",
                SpecialtyFullName = "Blockchain",
                MaxMember = 5,
                CodeOfGroupName = "IAP491",
                Semester = new Semester()
                {
                    SemesterID = 1
                }
            };
            Assert.AreEqual(specialty.SpecialtyID, actual.SpecialtyID);
        }
        // wrong specialty name , correct semester id
        [TestMethod()]
        public void GetSpecialtyByNameTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            var specialtyService = container.Resolve<SpecialtyService>();

            var specialtyName = "aaaaaaaaa";
            var semesterId = 1;
            var actual = specialtyService.GetSpecialtyByName(specialtyName, semesterId);

            Assert.IsNull(actual);
        }
        // correct specialty name , wrong semester id
        [TestMethod()]
        public void GetSpecialtyByNameTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            var specialtyService = container.Resolve<SpecialtyService>();

            var specialtyName = "Blockchain";
            var semesterId = 0;
            var actual = specialtyService.GetSpecialtyByName(specialtyName, semesterId);

            Assert.IsNull(actual);
        }
        // wrong specialty name and semester id
        [TestMethod()]
        public void GetSpecialtyByNameTest_4()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            var specialtyService = container.Resolve<SpecialtyService>();

            var specialtyName = "aaaaaaaaa";
            var semesterId = 0;
            var actual = specialtyService.GetSpecialtyByName(specialtyName, semesterId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function getSpecialtiesByProfessionIdTest()
        /// </summary>
        // correct professionId and semester id
        [TestMethod()]
        public void getSpecialtiesByProfessionIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            var specialtyService = container.Resolve<SpecialtyService>();

            var professionId = 1;
            var semesterId = 1;
            var actual = specialtyService.getSpecialtiesByProfessionId(professionId, semesterId);

            var specialty = new Specialty()
            {
                SpecialtyID = 1,
                Profession = new Profession()
                {
                    ProfessionID = 1
                },
                SpecialtyAbbreviation = "BC",
                SpecialtyFullName = "Blockchain",
                MaxMember = 5,
                CodeOfGroupName = "IAP491",
                Semester = new Semester()
                {
                    SemesterID = 1
                }
            };
            var specialtyList = new List<Specialty>();
            specialtyList.Add(specialty);
            Assert.AreEqual(specialtyList[0].SpecialtyID, actual[0].SpecialtyID);
            Assert.AreEqual(specialtyList[0].Profession.ProfessionID, actual[0].Profession.ProfessionID);
            Assert.AreEqual(specialtyList[0].SpecialtyAbbreviation, actual[0].SpecialtyAbbreviation);
            Assert.AreEqual(specialtyList[0].SpecialtyFullName, actual[0].SpecialtyFullName);
            Assert.AreEqual(specialtyList[0].MaxMember, actual[0].MaxMember);
            Assert.AreEqual(specialtyList[0].CodeOfGroupName, actual[0].CodeOfGroupName);
            Assert.AreEqual(specialtyList[0].Semester.SemesterID, actual[0].Semester.SemesterID);
        }
        // wrong professionId, corrext semester id
        [TestMethod()]
        public void getSpecialtiesByProfessionIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            var specialtyService = container.Resolve<SpecialtyService>();

            var professionId = 0;
            var semesterId = 1;
            var actual = specialtyService.getSpecialtiesByProfessionId(professionId, semesterId);

            Assert.IsNull(actual);
        }
        // correct profession id , wrong semesterId
        [TestMethod()]
        public void getSpecialtiesByProfessionIdTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            var specialtyService = container.Resolve<SpecialtyService>();

            var professionId = 1;
            var semesterId = 0;
            var actual = specialtyService.getSpecialtiesByProfessionId(professionId, semesterId);

            Assert.IsNull(actual);
        }
        // wrong profession id and semesterId
        [TestMethod()]
        public void getSpecialtiesByProfessionIdTest_4()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            var specialtyService = container.Resolve<SpecialtyService>();

            var professionId = 0;
            var semesterId = 0;
            var actual = specialtyService.getSpecialtiesByProfessionId(professionId, semesterId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetCodeOfGroupNameByGroupIdeaIdTest()
        /// </summary>
        // exist group idea id 
        [TestMethod()]
        public void GetCodeOfGroupNameByGroupIdeaIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            var specialtyService = container.Resolve<SpecialtyService>();

            var groupIdeaId = 4;
            var actual = specialtyService.GetCodeOfGroupNameByGroupIdeaId(groupIdeaId);

            var codeOfGroupName = "SWP493";
            Assert.AreEqual(codeOfGroupName,actual);
        }
        // not exist group idea id 
        [TestMethod()]
        public void GetCodeOfGroupNameByGroupIdeaIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            var specialtyService = container.Resolve<SpecialtyService>();

            var groupIdeaId = 0;
            var actual = specialtyService.GetCodeOfGroupNameByGroupIdeaId(groupIdeaId);

            Assert.IsNull(actual);
        }
    }
}