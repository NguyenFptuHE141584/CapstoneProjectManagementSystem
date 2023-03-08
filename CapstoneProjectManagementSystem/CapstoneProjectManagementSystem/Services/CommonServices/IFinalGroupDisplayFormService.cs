using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface IFinalGroupDisplayFormService
    {
        List<FinalGroupDisplayForm> ConvertFromFinalList(List<FinalGroup> finalGroupList);
    }
}
