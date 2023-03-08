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
    public class FinalGroupServiceTests
    {

        private FinalGroupService finalGroupService;
        public FinalGroupServiceTests()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IUserService), typeof(FinalGroupService));
            finalGroupService = container.Resolve<FinalGroupService>();
        }

        /// <summary>
        /// Test Function GetOldTopicByGroupName
        /// </summary>
        /// 

        //Test FinalGroupId Greater 0
        [TestMethod()]
        public void GetOldTopicByGroupName_TestFinalGroupIdGreater0()
        {
            int finalGroupId = -3424;
            try
            {
                if (Convert.ToInt32(finalGroupId) > 0)
                {
                    var actual = finalGroupService.GetOldTopicByGroupName(finalGroupId);
                    if (actual == null)
                    {
                        Assert.IsNull(actual);
                        return;
                    }
                    else
                    {
                        Assert.IsNotNull(actual);
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

        //Test Function Return Null
        [TestMethod()]
        public void GetOldTopicByGroupName_TestFunctionReturnNull()
        {
            int finalGroupId = 453;
            try
            {
                if (Convert.ToInt32(finalGroupId) > 0)
                {
                    var actual = finalGroupService.GetOldTopicByGroupName(finalGroupId);
                    if (actual == null)
                    {
                        Assert.IsNull(actual);
                        return;
                    }
                    else
                    {
                        Assert.IsNotNull(actual);
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
        
        //Test Function Return Not Null
        [TestMethod()]
        public void GetOldTopicByGroupName_TestFunctionReturnNotNull()
        {
            int finalGroupId = 1;
            try
            {
                if (Convert.ToInt32(finalGroupId) > 0)
                {
                    var actual = finalGroupService.GetOldTopicByGroupName(finalGroupId);
                    if (actual == null)
                    {
                        Assert.IsNull(actual);
                        return;
                    }
                    else
                    {
                        Assert.IsNotNull(actual);
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

        /// <summary>
        /// Test function getAllFinalGroupsTest()
        /// </summary>
        // have records in finalGroup table
        [TestMethod()]
        public void getAllFinalGroupsTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 5;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var actual = finalgroupService.getAllFinalGroups(semesterId);
            finalgroupService.DeleteFinalGroup(actual[0].FinalGroupID);

            Assert.AreEqual(1, actual.Count);
        }

        // not have records in finalGroup table
        [TestMethod()]
        public void getAllFinalGroupsTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var actual = finalgroupService.getAllFinalGroups(semesterId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function getFinalGroupByIdTest()
        /// </summary>
        // exist final group id
        [TestMethod()]
        public void getFinalGroupByIdTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 5;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            var actual = finalgroupService.getFinalGroupById(finalGroupId);
            finalgroupService.DeleteFinalGroup(finalGroupId);

            Assert.AreEqual("Group 1", actual.GroupName);
            Assert.AreEqual("Eng 1", actual.ProjectEnglishName);
            Assert.AreEqual("Abbre 1", actual.Abbreviation);
            Assert.AreEqual("Viet 1", actual.ProjectVietNameseName);
        }
        // not exist final group id
        [TestMethod()]
        public void getFinalGroupByIdTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 5;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var finalGroupId = 0;
            var actual = finalgroupService.getFinalGroupById(finalGroupId);
            finalgroupService.DeleteFinalGroup(finalGroupId);

            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test function GetLackOfMemberFinalGroupSearchListTest()
        /// </summary>
        
        // search with profession and specialty have records, search text = ""
        [TestMethod()]
        public void GetLackOfMemberFinalGroupSearchListTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 4;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var professionIdSearch = 1;
            var specialtyIdSearch = 1;
            var searchText = "";
            var offset = 0;
            var fetch = int.MaxValue;
            var actual = finalgroupService.GetLackOfMemberFinalGroupSearchList(semesterId, professionIdSearch, specialtyIdSearch, searchText, offset, fetch);
            finalgroupService.DeleteFinalGroup(actual[0].FinalGroupID);

            Assert.AreEqual(1, actual.Count);
        }
        // search with profession and specialty have records, search text not match
        [TestMethod()]
        public void GetLackOfMemberFinalGroupSearchListTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 4;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var professionIdSearch = 1;
            var specialtyIdSearch = 1;
            var searchText = "aaaaaaaaaaaaaa";
            var offset = 0;
            var fetch = int.MaxValue;
            var actual = finalgroupService.GetLackOfMemberFinalGroupSearchList(semesterId, professionIdSearch, specialtyIdSearch, searchText, offset, fetch);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            finalgroupService.DeleteFinalGroup(finalGroupId);

            Assert.IsNull(actual);
        }
        // search with profession not have records
        [TestMethod()]
        public void GetLackOfMemberFinalGroupSearchListTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 4;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var professionIdSearch = 2;
            var specialtyIdSearch = 1;
            var searchText = "";
            var offset = 0;
            var fetch = int.MaxValue;
            var actual = finalgroupService.GetLackOfMemberFinalGroupSearchList(semesterId, professionIdSearch, specialtyIdSearch, searchText, offset, fetch);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            finalgroupService.DeleteFinalGroup(finalGroupId);
            Assert.IsNull(actual);
        }
        // search with profession have records but specialty not have records
        [TestMethod()]
        public void GetLackOfMemberFinalGroupSearchListTest_4()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 4;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var professionIdSearch = 1;
            var specialtyIdSearch = 2;
            var searchText = "";
            var offset = 0;
            var fetch = int.MaxValue;
            var actual = finalgroupService.GetLackOfMemberFinalGroupSearchList(semesterId, professionIdSearch, specialtyIdSearch, searchText, offset, fetch);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            finalgroupService.DeleteFinalGroup(finalGroupId);

            Assert.IsNull(actual);
        }
        // test offset and fetch (smaller than total records)
        [TestMethod()]
        public void GetLackOfMemberFinalGroupSearchListTest_5()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 4;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var groupName2 = "Group 2";
            var engName2 = "Eng 2";
            var abbre2 = "Abbre 2";
            var vietName2 = "Viet 2";
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName2, engName2, abbre2, vietName2, maxMember, numberOfMember);
            var professionIdSearch = 1;
            var specialtyIdSearch = 1;
            var searchText = "";
            var offset = 0;
            var fetch = 1;
            var actual = finalgroupService.GetLackOfMemberFinalGroupSearchList(semesterId, professionIdSearch, specialtyIdSearch, searchText, offset, fetch);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            var finalGroupId2 = finalgroupService.getAllFinalGroups(semesterId)[1].FinalGroupID;
            finalgroupService.DeleteFinalGroup(finalGroupId);
            finalgroupService.DeleteFinalGroup(finalGroupId2);

            Assert.AreEqual(1, actual.Count);
        }
        // test offset and fetch (larger than total records)
        [TestMethod()]
        public void GetLackOfMemberFinalGroupSearchListTest_6()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 4;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var groupName2 = "Group 2";
            var engName2 = "Eng 2";
            var abbre2 = "Abbre 2";
            var vietName2 = "Viet 2";
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName2, engName2, abbre2, vietName2, maxMember, numberOfMember);
            var professionIdSearch = 1;
            var specialtyIdSearch = 1;
            var searchText = "";
            var offset = 0;
            var fetch = 3;
            var actual = finalgroupService.GetLackOfMemberFinalGroupSearchList(semesterId, professionIdSearch, specialtyIdSearch, searchText, offset, fetch);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            var finalGroupId2 = finalgroupService.getAllFinalGroups(semesterId)[1].FinalGroupID;
            finalgroupService.DeleteFinalGroup(finalGroupId);
            finalgroupService.DeleteFinalGroup(finalGroupId2);

            Assert.AreEqual(2, actual.Count);
        }

        /// <summary>
        /// Test function GetFullMemberFinalGroupSearchListTest()
        /// </summary>
        // search with profession and specialty have records, search text = ""
        [TestMethod()]
        public void GetFullMemberFinalGroupSearchListTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 5;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var professionIdSearch = 1;
            var specialtyIdSearch = 1;
            var searchText = "";
            var offset = 0;
            var fetch = int.MaxValue;
            var actual = finalgroupService.GetFullMemberFinalGroupSearchList(semesterId, professionIdSearch, specialtyIdSearch, searchText, offset, fetch);
            finalgroupService.DeleteFinalGroup(actual[0].FinalGroupID);

            Assert.AreEqual(1, actual.Count);
        }
        // search with profession and specialty have records, search text not match
        [TestMethod()]
        public void GetFullMemberFinalGroupSearchListTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 5;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var professionIdSearch = 1;
            var specialtyIdSearch = 1;
            var searchText = "aaaaaaaaaaaaaa";
            var offset = 0;
            var fetch = int.MaxValue;
            var actual = finalgroupService.GetFullMemberFinalGroupSearchList(semesterId, professionIdSearch, specialtyIdSearch, searchText, offset, fetch);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            finalgroupService.DeleteFinalGroup(finalGroupId);

            Assert.IsNull(actual);
        }
        // search with profession not have records
        [TestMethod()]
        public void GetFullMemberFinalGroupSearchList_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 5;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var professionIdSearch = 2;
            var specialtyIdSearch = 1;
            var searchText = "";
            var offset = 0;
            var fetch = int.MaxValue;
            var actual = finalgroupService.GetFullMemberFinalGroupSearchList(semesterId, professionIdSearch, specialtyIdSearch, searchText, offset, fetch);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            finalgroupService.DeleteFinalGroup(finalGroupId);

            Assert.IsNull(actual);
        }
        // search with profession have records but specialty not have records
        [TestMethod()]
        public void GetFullMemberFinalGroupSearchListTest_4()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 5;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var professionIdSearch = 1;
            var specialtyIdSearch = 2;
            var searchText = "";
            var offset = 0;
            var fetch = int.MaxValue;
            var actual = finalgroupService.GetFullMemberFinalGroupSearchList(semesterId, professionIdSearch, specialtyIdSearch, searchText, offset, fetch);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            finalgroupService.DeleteFinalGroup(finalGroupId);

            Assert.IsNull(actual);
        }
        // test offset and fetch (smaller than total records)
        [TestMethod()]
        public void GetFullMemberFinalGroupSearchListTest_5()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 5;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var groupName2 = "Group 2";
            var engName2 = "Eng 2";
            var abbre2 = "Abbre 2";
            var vietName2 = "Viet 2";
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName2, engName2, abbre2, vietName2, maxMember, numberOfMember);
            var professionIdSearch = 1;
            var specialtyIdSearch = 1;
            var searchText = "";
            var offset = 0;
            var fetch = 1;
            var actual = finalgroupService.GetFullMemberFinalGroupSearchList(semesterId, professionIdSearch, specialtyIdSearch, searchText, offset, fetch);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            var finalGroupId2 = finalgroupService.getAllFinalGroups(semesterId)[1].FinalGroupID;
            finalgroupService.DeleteFinalGroup(finalGroupId);
            finalgroupService.DeleteFinalGroup(finalGroupId2);

            Assert.AreEqual(1, actual.Count);
        }
        // test offset and fetch (larger than total records)
        [TestMethod()]
        public void GetFullMemberFinalGroupSearchListTest_6()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 5;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var groupName2 = "Group 2";
            var engName2 = "Eng 2";
            var abbre2 = "Abbre 2";
            var vietName2 = "Viet 2";
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName2, engName2, abbre2, vietName2, maxMember, numberOfMember);
            var professionIdSearch = 1;
            var specialtyIdSearch = 1;
            var searchText = "";
            var offset = 0;
            var fetch = 3;
            var actual = finalgroupService.GetFullMemberFinalGroupSearchList(semesterId, professionIdSearch, specialtyIdSearch, searchText, offset, fetch);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            var finalGroupId2 = finalgroupService.getAllFinalGroups(semesterId)[1].FinalGroupID;
            finalgroupService.DeleteFinalGroup(finalGroupId);
            finalgroupService.DeleteFinalGroup(finalGroupId2);

            Assert.AreEqual(2, actual.Count);
        }

        /// <summary>
        /// Test function UpdateNumberOfMemberWhenAddTest()
        /// </summary>
        // exist final group id
        [TestMethod()]
        public void UpdateNumberOfMemberWhenAddTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 3;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            var actual = finalgroupService.UpdateNumberOfMemberWhenAdd(finalGroupId);
            finalgroupService.DeleteFinalGroup(finalGroupId);

            Assert.AreEqual(1, actual);
        }
        // not exist final group id
        [TestMethod()]
        public void UpdateNumberOfMemberWhenAddTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var finalGroupId = 0;
            var actual = finalgroupService.UpdateNumberOfMemberWhenAdd(finalGroupId);

            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test function UpdateNumberOfMemberWhenRemoveTest()
        /// </summary>
        // exist final group id
        [TestMethod()]
        public void UpdateNumberOfMemberWhenRemoveTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 3;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            var actual = finalgroupService.UpdateNumberOfMemberWhenRemove(finalGroupId);
            finalgroupService.DeleteFinalGroup(finalGroupId);

            Assert.AreEqual(1, actual);
        }
        // not exist final group id
        [TestMethod()]
        public void UpdateNumberOfMemberWhenRemoveTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var finalGroupId = 0;
            var actual = finalgroupService.UpdateNumberOfMemberWhenRemove(finalGroupId);

            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test function UpdateGroupNameTest()
        /// </summary>
        // exist final group id
        [TestMethod()]
        public void UpdateGroupNameUpdateGroupNameTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 3;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            var newGroupName = "Group new";
            var actual = finalgroupService.UpdateGroupName(finalGroupId,newGroupName);
            finalgroupService.DeleteFinalGroup(finalGroupId);

            Assert.AreEqual(1, actual);
        }
        // not exist final group id
        [TestMethod()]
        public void UpdateGroupNameTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var finalGroupId = 0;
            var newGroupName = "Group new";
            var actual = finalgroupService.UpdateGroupName(finalGroupId,newGroupName);

            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test function DeleteFinalGroupTest()
        /// </summary>
        // exist final group id
        [TestMethod()]
        public void DeleteFinalGroupTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 3;
            finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            var actual = finalgroupService.DeleteFinalGroup(finalGroupId);

            Assert.AreEqual(1, actual);
        }
        // not exist final group id
        [TestMethod()]
        public void DeleteFinalGroupTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var finalGroupId = 0;
            var actual = finalgroupService.DeleteFinalGroup(finalGroupId);

            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test function CreateFinalGroupTest()
        /// </summary>
        // exist semester id, profession id, specialty id
        [TestMethod()]
        public void CreateFinalGroupTest()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 3;
            var actual = finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);
            var finalGroupId = finalgroupService.getAllFinalGroups(semesterId)[0].FinalGroupID;
            finalgroupService.DeleteFinalGroup(finalGroupId);

            Assert.IsNotNull(actual);
        }
        // not exist semester id
        [TestMethod()]
        public void CreateFinalGroupTest_2()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 0;
            var professionId = 1;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 3;
            var actual = finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);

            Assert.AreEqual(0,actual);
        }
        // not exist profession id
        [TestMethod()]
        public void CreateFinalGroupTest_3()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 0;
            var specialtyId = 1;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 3;
            var actual = finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);

            Assert.AreEqual(0, actual);
        }
        // not exist specialty id
        [TestMethod()]
        public void CreateFinalGroupTest_4()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IFinalGroupService), typeof(FinalGroupService));
            var finalgroupService = container.Resolve<FinalGroupService>();

            var semesterId = 1;
            var professionId = 1;
            var specialtyId = 0;
            var groupName = "Group 1";
            var engName = "Eng 1";
            var abbre = "Abbre 1";
            var vietName = "Viet 1";
            var maxMember = 5;
            var numberOfMember = 3;
            var actual = finalgroupService.CreateFinalGroup(semesterId, professionId, specialtyId, groupName, engName, abbre, vietName, maxMember, numberOfMember);

            Assert.AreEqual(0, actual);
        }
    }
}