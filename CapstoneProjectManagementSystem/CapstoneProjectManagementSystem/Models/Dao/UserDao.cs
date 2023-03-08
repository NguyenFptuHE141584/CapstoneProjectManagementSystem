using CapstoneProjectManagementSystem.Models.Dao.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models.Dao
{
    public class UserDao
    {
        public static int AddUser(User user, int roleId)
        {
            if (roleId == 1)
            {
                string sql = @"BEGIN TRANSACTION;
                                insert into [Users] ([User_ID],UserName,FptEmail,Avatar,FullName,Role_ID)
                                values (@userId,@userName,@fptEmail,@avatar,@fullName,@roleId)

                                insert into Students (Student_ID,RollNumber,Semester_ID) 
                                values(@studentId,@rollNumber,(select Semester_ID from Semesters where StatusCloseBit = 1))
                              COMMIT;";
                SqlParameter[] paramaters = new SqlParameter[8];
                paramaters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
                paramaters[0].Value = user.UserID;
                paramaters[1] = new SqlParameter("@userName", SqlDbType.VarChar);
                paramaters[1].Value = user.UserName;
                paramaters[2] = new SqlParameter("@fptEmail", SqlDbType.VarChar);
                paramaters[2].Value = user.FptEmail;
                paramaters[3] = new SqlParameter("@avatar", SqlDbType.VarChar);
                paramaters[3].Value = user.Avatar;
                paramaters[4] = new SqlParameter("@fullName", SqlDbType.NVarChar);
                paramaters[4].Value = user.FullName;
                paramaters[5] = new SqlParameter("@roleId", SqlDbType.Int);
                paramaters[5].Value = roleId;
                paramaters[6] = new SqlParameter("@studentId", SqlDbType.VarChar);
                paramaters[6].Value = user.UserID;
                var countAcount = 0;
                foreach (char i in user.UserID)
                {
                    if (i == '0' || i == '1' || i == '2' || i == '3' || i == '4' || i == '5' || i == '6' || i == '7' || i == '8' || i == '9')
                    {
                        countAcount++;
                    }
                }
                if(countAcount == 5)
                {
                    paramaters[7] = new SqlParameter("@rollNumber", SqlDbType.VarChar);
                    paramaters[7].Value = user.UserName.Substring(user.UserName.Length - 7).ToUpper();
                }
                if (countAcount == 6)
                {
                    paramaters[7] = new SqlParameter("@rollNumber", SqlDbType.VarChar);
                    paramaters[7].Value = user.UserName.Substring(user.UserName.Length - 8).ToUpper();
                }
                return DbContext.ExecuteSQL(sql, paramaters);
            }
            else
            {
                string sql = @"
                            BEGIN TRANSACTION;
                                insert into [Users] ([User_ID],UserName,FptEmail,Avatar,FullName,Role_ID)
                                values (@userId,@userName,@fptEmail,@avatar,@fullName,@roleId)

                                insert into Staffs(Staff_ID) values(@staffId)
                            COMMIT;";
                SqlParameter[] paramaters = new SqlParameter[7];
                paramaters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
                paramaters[0].Value = user.UserID;
                paramaters[1] = new SqlParameter("@userName", SqlDbType.VarChar);
                paramaters[1].Value = user.UserName;
                paramaters[2] = new SqlParameter("@fptEmail", SqlDbType.VarChar);
                paramaters[2].Value = user.FptEmail;
                paramaters[3] = new SqlParameter("@avatar", SqlDbType.VarChar);
                paramaters[3].Value = user.Avatar;
                paramaters[4] = new SqlParameter("@fullName", SqlDbType.NVarChar);
                paramaters[4].Value = user.FullName;
                paramaters[5] = new SqlParameter("@roleId", SqlDbType.Int);
                paramaters[5].Value = roleId;
                paramaters[6] = new SqlParameter("@staffId", SqlDbType.VarChar);
                paramaters[6].Value = user.UserID;
                return DbContext.ExecuteSQL(sql, paramaters);
            }

        }
        public static int UpdateAvatar(string avatar, string userId)
        {
            string sql = @"update Users set Avatar = @avatar
                            where [User_ID] = @userId";
            SqlParameter[] paramaters = new SqlParameter[2];
            paramaters[0] = new SqlParameter("@avatar", SqlDbType.VarChar);
            paramaters[0].Value = avatar;
            paramaters[1] = new SqlParameter("@userId", SqlDbType.VarChar);
            paramaters[1].Value = userId;
            return DbContext.ExecuteSQL(sql, paramaters);
        }
        public static User GetUserById(string userId)
        {
            string sql = "select * from [Users] where [User_ID] = @userId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
            parameters[0].Value = userId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new User()
            {
                UserID = dr["User_ID"].ToString(),
                UserName = (dr["UserName"].ToString() == null) ? "" : dr["UserName"].ToString(),
                FptEmail = (dr["FptEmail"].ToString() == null) ? "" : dr["FptEmail"].ToString(),
                Avatar = (dr["Avatar"].ToString() == null) ? "" : dr["Avatar"].ToString(),
                FullName = (dr["FullName"].ToString() == null) ? "" : dr["FullName"].ToString(),
                Role = new Role()
                {
                    Role_ID = Convert.ToInt32(dr["Role_ID"].ToString())
                }
            };
        }

        public static int CheckUserByUserIdAndRoleExist(string userId, string role)
        {
            string sql = @"select count(*) as count from Users u
                           join Roles r on r.Role_ID = u.Role_ID
                           where u.[User_ID] = @userId and r.RoleName = @role and u.[Deleted_At] is null and r.[Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
            parameters[0].Value = userId;
            parameters[1] = new SqlParameter("@role", SqlDbType.VarChar);
            parameters[1].Value = role;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            if (Convert.ToInt32(dr["count"].ToString()) == 0)
                return 0;
            else
                return 1;

        }

        public static int CheckProfileUserHaveAttributeIsNullByUserId(string userId)
        {
            string sql = @"select count(*) as [count] from Students s 
                               join Users u on u.[User_ID] = s.Student_ID
                               join AffiliateAccounts a on a.AffiliateAccount_ID = u.[User_ID]
                                where u.[User_ID] =  @userId and a.AffiliateAccount_ID is not null
											                                 and s.SelfDiscription is not null 
										                                     and s.ExpectedRoleInGroup is not null
											                                 and s.PhoneNumber is not null 
											                                 and s.LinkFacebook is not null
                                                                             and u.[Deleted_At] is null
                                                                             and s.[Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
            parameters[0].Value = userId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            if (Convert.ToInt32(dr["count"].ToString()) == 0)
                return 0;
            else
                return 1;
        }

        public static string GetListFptEmailByGroupIdeaId(int groupIdeaId)
        {
            string sql = @"select u.FptEmail from Student_GroupIdea sg 
                            join  Students s on s.Student_ID = sg.Student_ID
                            join Users u on u.[User_ID] = s.Student_ID
                            where GroupIdea_ID = @groupIdeaId 
                    and (sg.[Status] = 1 or sg.[Status] = 2 and sg.Deleted_At is null)";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[0].Value = groupIdeaId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            var listFptEmail = "";
            foreach (DataRow dr in result.Rows)
            {
                listFptEmail += dr["FptEmail"].ToString() + ",";
            }
            return listFptEmail;
        }

        public static string GetNameStudentByUserId(string userId)
        {
            string sql = @"select CONCAT(u.FullName,'(',s.RollNumber,')') as NameStudent from Users u
                                join Students s on s.Student_ID = u.User_ID
                                where [User_ID] = @userId and (s.Deleted_At is null and u.Deleted_At is null)";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
            parameters[0].Value = userId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return dr["NameStudent"].ToString();
        }

        public static User GetUserByFptEmail(string fptEmail)
        {
            string sql = "select * from [Users] where [User_ID] = @fptEmail and Role_ID = 1 and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@fptEmail", SqlDbType.VarChar);
            parameters[0].Value = fptEmail;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new User()
            {
                UserID = dr["User_ID"].ToString(),
                UserName = (dr["UserName"].ToString() == null) ? "" : dr["UserName"].ToString(),
                FptEmail = (dr["FptEmail"].ToString() == null) ? "" : dr["FptEmail"].ToString(),
                Avatar = (dr["Avatar"].ToString() == null) ? "" : dr["Avatar"].ToString(),
                FullName = (dr["FullName"].ToString() == null) ? "" : dr["FullName"].ToString(),
                Role = new Role()
                {
                    Role_ID = Convert.ToInt32(dr["Role_ID"].ToString())
                }
            };
        }
    }
}
