using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class GroupIdeaDisplayFormService : IGroupIdeaDisplayFormService
    {
        private readonly IUserService _userService;
        private readonly IStudent_GroupIdeaService _student_GroupIdeaService;
        private readonly IProfessionService _professionService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IGroupIdeaService _groupIdeaService;

        public GroupIdeaDisplayFormService(IUserService userService, 
                                            IStudent_GroupIdeaService student_GroupIdeaService, 
                                            IProfessionService professionService,
                                            ISpecialtyService specialtyService,
                                            IGroupIdeaService groupIdeaService)
        {
            _userService = userService;
            _student_GroupIdeaService = student_GroupIdeaService;
            _professionService = professionService;
            _specialtyService = specialtyService;
            _groupIdeaService = groupIdeaService;
        }
        public List<GroupIdeaDisplayForm> ConvertFromGroupIdeaList(List<GroupIdea> groupIdeaList)
        {
            List<GroupIdeaDisplayForm> groupIdeaDisplayFormList = new List<GroupIdeaDisplayForm>();
            if (groupIdeaList == null) return groupIdeaDisplayFormList;
            else {
            foreach(GroupIdea item in groupIdeaList){
                List<string> projectTagList = _groupIdeaService.ConvertProjectTags(item.ProjectTags);
                GroupIdeaDisplayForm groupIdeaDisplayForm = new GroupIdeaDisplayForm()
                {
                    GroupIdeaID = item.GroupIdeaID,
                    ProjectEnglishName = item.ProjectEnglishName,
                    LeaderFullName = _userService.GetUserByID(_student_GroupIdeaService.GetLeaderIdByGroupIdeaId(item.GroupIdeaID)).UserName,
                    Avatar = _userService.GetUserByID(_student_GroupIdeaService.GetLeaderIdByGroupIdeaId(item.GroupIdeaID)).Avatar,
                    CreatedAt = item.CreatedAt.ToString(),
                    ProjectTags = projectTagList,
                    ProfessionFullName = _professionService.getProfessionById(item.Profession.ProfessionID).ProfessionFullName,
                    SpecialtyFullName = _specialtyService.getSpecialtyById(item.Specialty.SpecialtyID).SpecialtyFullName,
                    Description = item.Description,
                    AvailableSlot = (item.MaxMember - item.NumberOfMember),
                    Semester_Id = item.Semester.SemesterID
                };
                groupIdeaDisplayFormList.Add(groupIdeaDisplayForm);
            }
            return groupIdeaDisplayFormList;
            }
        }
    }
}
