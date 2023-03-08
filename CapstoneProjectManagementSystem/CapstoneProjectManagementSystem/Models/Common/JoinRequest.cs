using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models.Common
{
    public class JoinRequest : CommonProperty
    {
        public string UserId { get; set; }
        public string FptEmail { get; set; }
        public string Avatar { get; set; }
        public string Message { get; set; }

    }
}
