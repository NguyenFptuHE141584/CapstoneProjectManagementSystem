using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface IGroupIdeaDisplayFormService
    {
        List<GroupIdeaDisplayForm> ConvertFromGroupIdeaList(List<GroupIdea> groupIdeaList);
    }
}
