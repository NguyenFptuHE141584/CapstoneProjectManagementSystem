using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class AffiliateAccountService : IAffiliateAccountService
    {
        private readonly IPasswordHasherService _passwordHasherService;
        public AffiliateAccountService(IPasswordHasherService passwordHasherService)
        {
            _passwordHasherService = passwordHasherService;
        }

        public bool CheckAffiliateAccountAndPasswordHash(string personalEmail, string passwordHash)
        {
            var affiliateAccount = AffiliateAccountDao.GetAffiliateAccountByEmail(personalEmail);
            if (affiliateAccount != null)
            {
                var check = _passwordHasherService.PasswordVerificationResult(affiliateAccount.PasswordHash, passwordHash);
                if (check == true)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public AffiliateAccount GetAffiliateAccountById(string BackupAccount_Id)
        {
            return AffiliateAccountDao.GetAffiliateAccountById(BackupAccount_Id);
        }
        public AffiliateAccount GetAffiliateAccountByEmail(string email)
        {
            return AffiliateAccountDao.GetAffiliateAccountByEmail(email);
        }

        public int UpdateOTP(string BackupAccount_Id, string OTP)
        {
            return AffiliateAccountDao.UpdateOTP(BackupAccount_Id, OTP);
        }
        public int UpdatePasswordHash(string BackupAccount_Id, string password)
        {
            password = _passwordHasherService.HashPassword(password);
            return AffiliateAccountDao.UpdatePasswordHash(BackupAccount_Id, password);
        }

        public int AddOTP(string affiliateAccountId, string otp)
        {
            return AffiliateAccountDao.AddOTP(affiliateAccountId,otp);
        }

        public int UpdateIsVerifyEmail(string affiliateAccountId,string personalEmail)
        {
            return AffiliateAccountDao.UpdateIsVerifyEmail( affiliateAccountId,  personalEmail);
        }

        public bool CheckPersonalEmailExist(string personalEmail)
        {
            return AffiliateAccountDao.CheckPersonalEmailExist(personalEmail);
        }
    }
}
