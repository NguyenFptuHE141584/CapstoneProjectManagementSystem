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
    public class GroupIdeaDisplayFormServiceTests
    {
        /// <summary>
        /// Test function ConvertFromGroupIdeaListTest()
        /// </summary>
        // list have elements
        [TestMethod()]
        public void ConvertFromGroupIdeaListTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IGroupIdeaDisplayFormService), typeof(GroupIdeaDisplayFormService));
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            container.RegisterType(typeof(IProfessionService), typeof(ProfessionService));
            container.RegisterType(typeof(IUserService), typeof(UserService));
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            container.RegisterType(typeof(IGroupIdeaService), typeof(GroupIdeaService));
            var groupIdeaDisplayFormService = container.Resolve<GroupIdeaDisplayFormService>();

            var groupIdea = new GroupIdea()
            {
                GroupIdeaID = 4,
                ProjectEnglishName = "Eng 1",
                ProjectTags = "java",
                CreatedAt = DateTime.Now,
                Profession = new Profession() { 
                    ProfessionID = 1
                },
                Specialty = new Specialty()
                {
                    SpecialtyID = 1
                },
                Semester = new Semester()
                {
                    SemesterID = 1
                },
                Description = "Des 1",
                MaxMember = 5,
                NumberOfMember = 5
            };
            var groupIdeaList = new List<GroupIdea>();
            groupIdeaList.Add(groupIdea);
            var actual = groupIdeaDisplayFormService.ConvertFromGroupIdeaList(groupIdeaList);

            Assert.AreEqual("Software Engineering", actual[0].ProfessionFullName);
            Assert.AreEqual("Blockchain", actual[0].SpecialtyFullName);
        }
        // list is null
        [TestMethod()]
        public void ConvertFromGroupIdeaListTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IGroupIdeaDisplayFormService), typeof(GroupIdeaDisplayFormService));
            container.RegisterType(typeof(ISpecialtyService), typeof(SpecialtyService));
            container.RegisterType(typeof(IProfessionService), typeof(ProfessionService));
            container.RegisterType(typeof(IUserService), typeof(UserService));
            container.RegisterType(typeof(IStudent_GroupIdeaService), typeof(Student_GroupIdeaService));
            container.RegisterType(typeof(IGroupIdeaService), typeof(GroupIdeaService));
            var groupIdeaDisplayFormService = container.Resolve<GroupIdeaDisplayFormService>();

            var groupIdeaList = new List<GroupIdea>();
            var actual = groupIdeaDisplayFormService.ConvertFromGroupIdeaList(groupIdeaList);

            Assert.AreEqual(0,actual.Count);
        }
    }
}