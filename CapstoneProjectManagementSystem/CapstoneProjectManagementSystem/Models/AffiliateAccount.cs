using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class AffiliateAccount:CommonProperty
    {
        public string AffiliateAccount_ID { get; set; }
        public string PersonalEmail { get; set; }
        public string PasswordHash { get; set; }
        public bool IsVerifyEmail { get; set; }
        public string OneTimePassword { get; set; }
        public DateTime OtpRequestTime { get; set; }
        public User User { get; set; }
    }
}
