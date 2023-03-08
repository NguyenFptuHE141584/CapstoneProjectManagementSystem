using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneProjectManagementSystem.Models.Dao.DBContext;
using System.Data;
using System.Data.SqlClient;

namespace CapstoneProjectManagementSystem.Models.Dao
{
    public class AffiliateAccountDao
    {
        public static int AddOTP(string affiliateAccountId, string otp)
        {
            string sql = @"insert into AffiliateAccounts(AffiliateAccount_ID,PersonalEmail,One_Time_Password,OTP_Request_Time)
                           values(@userId,@personalEmail,@otp,@time)";
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
            parameters[0].Value = affiliateAccountId;
            parameters[1] = new SqlParameter("@otp", SqlDbType.VarChar);
            parameters[1].Value = otp;
            parameters[2] = new SqlParameter("@time", SqlDbType.DateTime);
            parameters[2].Value = DateTime.Now;
            parameters[3] = new SqlParameter("@personalEmail", SqlDbType.VarChar);
            parameters[3].Value = "";

            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int UpdateOTP(String BackupAccount_Id, String OTP)
        {
            string sql = @"update AffiliateAccounts
                        set One_Time_Password = @OTP , OTP_Request_Time = @OTP_Request_Time
                        where AffiliateAccount_ID = @Id and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@OTP", SqlDbType.VarChar);
            parameters[0].Value = OTP;
            parameters[1] = new SqlParameter("@OTP_Request_Time", SqlDbType.DateTime);
            parameters[1].Value = DateTime.Now;
            parameters[2] = new SqlParameter("@Id", SqlDbType.VarChar);
            parameters[2].Value = BackupAccount_Id;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int UpdatePasswordHash(String BackupAccount_Id, String password)
        {
            string sql = @"update AffiliateAccounts
                        set PasswordHash = @password 
                        where AffiliateAccount_ID = @Id and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@password", SqlDbType.VarChar);
            parameters[0].Value = password;
            parameters[1] = new SqlParameter("@Id", SqlDbType.VarChar);
            parameters[1].Value = BackupAccount_Id;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static AffiliateAccount GetAffiliateAccountById(string BackupAccount_Id)
        {
            string sql = "select * from [AffiliateAccounts] where [AffiliateAccount_ID] = @Id and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id", SqlDbType.VarChar);
            parameters[0].Value = BackupAccount_Id;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new AffiliateAccount()
            {
                AffiliateAccount_ID = dr["AffiliateAccount_ID"].ToString(),
                PersonalEmail = dr["PersonalEmail"].ToString(),
                IsVerifyEmail = Convert.ToBoolean(dr["IsVerifyEmail"]),
                PasswordHash = dr["PasswordHash"].ToString(),
                OneTimePassword = dr["One_Time_Password"].ToString(),
                OtpRequestTime = Convert.ToDateTime(dr["OTP_Request_Time"]),
                //IsPersonEmailConfirmed = Convert.ToInt32(dr["IsPersonalEmailConfirmed"]),
            };
        }
        public static AffiliateAccount GetAffiliateAccountByEmail(string email)
        {
            string sql = "select * from [AffiliateAccounts] where [PersonalEmail] = @Email and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Email", SqlDbType.VarChar);
            parameters[0].Value = email;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new AffiliateAccount()
            {
                AffiliateAccount_ID = dr["AffiliateAccount_ID"].ToString(),
                PersonalEmail = dr["PersonalEmail"].ToString(),
                PasswordHash = dr["PasswordHash"].ToString(),
                //OneTimePassword = dr["One_Time_Password"].ToString(),
                //OtpRequestTime = Convert.ToDateTime(dr["OTP_Request_Time"]),
                //IsPersonEmailConfirmed = Convert.ToInt32(dr["IsPersonalEmailConfirmed"])
            };
        }

        public static int UpdateIsVerifyEmail(string affiliateAccountId, string personalEmail)
        {
            string sql = @"UPDATE AffiliateAccounts
                            SET IsVerifyEmail = 1 ,PersonalEmail = @personalEmail
                            WHERE AffiliateAccount_ID= @affiliateAccountId;";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@affiliateAccountId", SqlDbType.VarChar);
            parameters[0].Value = affiliateAccountId;
            parameters[1] = new SqlParameter("@personalEmail", SqlDbType.VarChar);
            parameters[1].Value = personalEmail;
            return DbContext.ExecuteSQL(sql, parameters);
        }

        public static string GetPasswordHashByAffiliateAccountId(string affiliateAccountId)
        {
            string sql = @"select PasswordHash from AffiliateAccounts where AffiliateAccount_ID = @affiliateAccount_ID";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@affiliateAccount_ID", SqlDbType.VarChar);
            parameters[0].Value = affiliateAccountId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return dr["PasswordHash"].ToString();
        }

        public static bool CheckPersonalEmailExist(string personalEmail)
        {
            string sql = @"select count(*) as CheckExistPersonalEmail from AffiliateAccounts where PersonalEmail = @personalEmail";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@personalEmail", SqlDbType.VarChar);
            parameters[0].Value = personalEmail;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            DataRow dr = result.Rows[0];
            return (Convert.ToInt32(dr["CheckExistPersonalEmail"]) == 0) ? true : false;
        }



    }
}
