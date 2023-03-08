using CapstoneProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface IAffiliateAccountService
    {
        bool CheckAffiliateAccountAndPasswordHash(string personalEmail, string passwordHash);
        int UpdateOTP(String BackupAccount_Id, String OTP);
        int UpdatePasswordHash(String BackupAccount_Id, String password);
        AffiliateAccount GetAffiliateAccountById(string BackupAccount_Id);
        AffiliateAccount GetAffiliateAccountByEmail(string email);
        int AddOTP(string affiliateAccountId, string otp);
        int UpdateIsVerifyEmail(string affiliateAccountId, string personalEmail);
        bool CheckPersonalEmailExist(string personalEmail);
    }
}
