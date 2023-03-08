using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement.CommonImplement
{
    public class FinalGroupDisplayFormService : IFinalGroupDisplayFormService
    {
        private readonly ISpecialtyService _specialtyService;
        private readonly IFinalGroupService _finalGroupService;
        public FinalGroupDisplayFormService(ISpecialtyService specialtyService,
                                            IFinalGroupService finalGroupService)
        {
            _specialtyService = specialtyService;
            _finalGroupService = finalGroupService;
        }
        public List<FinalGroupDisplayForm> ConvertFromFinalList(List<FinalGroup> finalGroupList)
        {
            List<FinalGroupDisplayForm> finalGroupDisplayFormList = new List<FinalGroupDisplayForm>();
            if (finalGroupList == null) return finalGroupDisplayFormList;
            else
            {
                foreach (FinalGroup item in finalGroupList)
                {

                    FinalGroupDisplayForm finalGroupDisplayForm = new FinalGroupDisplayForm()
                    {
                        FinalGroupID = item.FinalGroupID,
                        GroupName = item.GroupName,
                        ProjectEnglishName = item.ProjectEnglishName,
                        SpecialtyFullName = _specialtyService.getSpecialtyById(item.Specialty.SpecialtyID).SpecialtyFullName,
                        MaxMember = item.MaxMember,
                        NumberOfMember = item.NumberOfMember,
                        CreatedAt = item.CreatedAt
                    };
                    finalGroupDisplayFormList.Add(finalGroupDisplayForm);
                }
                return finalGroupDisplayFormList;
            }
        }
    }
}
