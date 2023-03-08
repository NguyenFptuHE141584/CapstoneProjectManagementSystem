using CapstoneProjectManagementSystem.Models;
using CapstoneProjectManagementSystem.Models.Dao.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models.Dao
{
    public class RegisteredGroupDao
    {
        public static int AddRegisteredGroup(RegisteredGroup registeredGroup)
        {
            string sql = @"insert into [RegisteredGroups] (GroupIdea_ID,
                                                            Registered_Supervisor_Name_1,
                                                            Registered_Supervisor_Phone_1,
                                                            Registered_Supervisor_Email_1,
                                                            Registered_Supervisor_Name_2,
                                                            Registered_Supervisor_Phone_2,
                                                            Registered_Supervisor_Email_2,
                                                            Student_Comment,
                                                            Students_Registraiton)
                                                            values (@GroupId,
                                                                    @Name1,
                                                                    @Phone1,
                                                                    @Email1,
                                                                    @Name2,
                                                                    @Phone2,
                                                                    @Email2,
                                                                    @Comment,
                                                                    @Students_Registraiton)";
            SqlParameter[] paramaters = new SqlParameter[9];
            paramaters[0] = new SqlParameter("@GroupId", SqlDbType.Int);
            paramaters[0].Value = registeredGroup.RegisteredGroupID;
            paramaters[1] = new SqlParameter("@Name1", SqlDbType.NVarChar);
            paramaters[1].Value = registeredGroup.RegisteredSupervisorName1;
            paramaters[2] = new SqlParameter("@Phone1", SqlDbType.VarChar);
            paramaters[2].Value = registeredGroup.RegisteredSupervisorPhone1;
            paramaters[3] = new SqlParameter("@Email1", SqlDbType.VarChar);
            paramaters[3].Value = registeredGroup.RegisteredSupervisorEmail1;
            paramaters[4] = new SqlParameter("@Name2", SqlDbType.NVarChar);
            paramaters[4].Value = registeredGroup.RegisteredSupervisorName2;
            paramaters[5] = new SqlParameter("@Phone2", SqlDbType.VarChar);
            paramaters[5].Value = registeredGroup.RegisteredSupervisorPhone2;
            paramaters[6] = new SqlParameter("@Email2", SqlDbType.VarChar);
            paramaters[6].Value = registeredGroup.RegisteredSupervisorEmail2;
            paramaters[7] = new SqlParameter("@Comment", SqlDbType.NVarChar);
            paramaters[7].Value = registeredGroup.StudentComment;
            paramaters[8] = new SqlParameter("@Students_Registraiton", SqlDbType.VarChar);
            paramaters[8].Value = registeredGroup.Students_Registration;
            return DbContext.ExecuteSQL(sql, paramaters);
        }
        public static RegisteredGroup GetRegisteredGroupByGroupIdeaId(int groupIdeaId)
        {
            string sql = "select * from [RegisteredGroups] where GroupIdea_ID = @groupIdeaId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[0].Value = groupIdeaId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new RegisteredGroup()
            {
                RegisteredGroupID = Convert.ToInt32(dr["RegisteredGroup_ID"]),
                GroupIdea = new GroupIdea()
                {
                    GroupIdeaID = Convert.ToInt32(dr["GroupIdea_ID"])
                },
                RegisteredSupervisorName1 = dr["Registered_Supervisor_Name_1"].ToString(),
                RegisteredSupervisorPhone1 = dr["Registered_Supervisor_Phone_1"].ToString(),
                RegisteredSupervisorEmail1 = dr["Registered_Supervisor_Email_1"].ToString(),
                RegisteredSupervisorName2 = dr["Registered_Supervisor_Name_2"].ToString(),
                RegisteredSupervisorPhone2 = dr["Registered_Supervisor_Phone_2"].ToString(),
                RegisteredSupervisorEmail2 = dr["Registered_Supervisor_Email_2"].ToString(),
                StudentComment = dr["Student_Comment"].ToString(),
                Status = Convert.ToInt32(dr["Status"]),
                StaffComment = dr["Staff_Comment"].ToString()
            };
        }

        public static RegisteredGroup GetGroupIDByRegisteredGroupId(int registeredGroupId)
        {
            string sql = "select GroupIdea_ID from [RegisteredGroups] where RegisteredGroup_ID = @registeredGroupId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@registeredGroupId", SqlDbType.Int);
            parameters[0].Value = registeredGroupId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new RegisteredGroup()
            {
                GroupIdea = new GroupIdea()
                {
                    GroupIdeaID = Convert.ToInt32(dr["GroupIdea_ID"].ToString())
                }
            };
        }
        public static int CountRecordRegisteredGroupSearchList(int semesterId, int status, string searchText)
        {
            string sql = "";
            if (status == 2)
            {
                sql += @"select count(rg.RegisteredGroup_ID) as [CountRegisteredGroup_ID]          
                                            from RegisteredGroups rg
                                            join GroupIdeas gi on gi.GroupIdea_ID =  rg.GroupIdea_ID
                                            join Specialties s on s.Specialty_ID = gi.Specialty_ID
                                            where gi.Semester_ID = @semesterId and rg.[Status] = @status 
                                and(@searchText =  '' or REPLACE(UPPER(gi.ProjectEnglishName), ' ', '') like @searchText 
            			 or REPLACE(UPPER(s.Specialty_FullName), ' ', '') like @searchText 
            		     or REPLACE(UPPER(rg.Registered_Supervisor_Name_1), ' ', '') like @searchText
            			 or REPLACE(UPPER(rg.Registered_Supervisor_Name_2), ' ', '') like @searchText) 
                        and s.[Deleted_At] is null";
            }
            else if (status == 0)
            {
                sql += @"select count(rg.RegisteredGroup_ID) as [CountRegisteredGroup_ID]     
                                            from RegisteredGroups rg
                                            join GroupIdeas gi on gi.GroupIdea_ID =  rg.GroupIdea_ID
                                            join Specialties s on s.Specialty_ID = gi.Specialty_ID
                                            where gi.Semester_ID = @semesterId and rg.[Status] = @status 
                                and(@searchText =  '' or REPLACE(UPPER(gi.ProjectEnglishName), ' ', '') like @searchText 
            			 or REPLACE(UPPER(s.Specialty_FullName), ' ', '') like @searchText 
            		     or REPLACE(UPPER(rg.Registered_Supervisor_Name_1), ' ', '') like @searchText
            			 or REPLACE(UPPER(rg.Registered_Supervisor_Name_2), ' ', '') like @searchText) 
                        and s.[Deleted_At] is null and rg.[Deleted_At] is null";
            }
            else
            {
                sql = @"select count(rg.RegisteredGroup_ID) as [CountRegisteredGroup_ID]             
                                            from RegisteredGroups rg
                                            join GroupIdeas gi on gi.GroupIdea_ID =  rg.GroupIdea_ID
                                            join Specialties s on s.Specialty_ID = gi.Specialty_ID
                                            where gi.Semester_ID = @semesterId and rg.[Status] = @status 
                                and(@searchText =  '' or REPLACE(UPPER(gi.ProjectEnglishName), ' ', '') like @searchText 
            			 or REPLACE(UPPER(s.Specialty_FullName), ' ', '') like @searchText 
            		     or REPLACE(UPPER(rg.Registered_Supervisor_Name_1), ' ', '') like @searchText
            			 or REPLACE(UPPER(rg.Registered_Supervisor_Name_2), ' ', '') like @searchText) 
                         and rg.[Deleted_At] is null";
            }
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[0].Value = semesterId;
            parameters[1] = new SqlParameter("@status", SqlDbType.Int);
            parameters[1].Value = status;
            parameters[2] = new SqlParameter("@searchText", SqlDbType.NVarChar);
            parameters[2].Value = searchText;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            return Convert.ToInt32(dr["CountRegisteredGroup_ID"]);
        }
        public static List<RegisteredGroup> GetRegisteredGroupSearchList(int semesterId, int status, string searchText, int offsetNumber, int fetchNumber)
        {
            string sql = "";
            if (status == 2)
            {
                sql += @"select rg.RegisteredGroup_ID,rg.GroupIdea_ID,gi.ProjectEnglishName,s.Specialty_FullName
                                ,rg.Registered_Supervisor_Name_1,rg.Registered_Supervisor_Name_2 
                                ,rg.Staff_Comment             
                                            from RegisteredGroups rg
                                            join GroupIdeas gi on gi.GroupIdea_ID =  rg.GroupIdea_ID
                                            join Specialties s on s.Specialty_ID = gi.Specialty_ID
                                            where gi.Semester_ID = @semesterId and rg.[Status] = @status 
                                and(@searchText =  '' or REPLACE(UPPER(gi.ProjectEnglishName), ' ', '') like @searchText 
            			 or REPLACE(UPPER(s.Specialty_FullName), ' ', '') like @searchText 
            		     or REPLACE(UPPER(rg.Registered_Supervisor_Name_1), ' ', '') like @searchText
            			 or REPLACE(UPPER(rg.Registered_Supervisor_Name_2), ' ', '') like @searchText) 
                        and s.[Deleted_At] is null order by rg.Created_At desc
            			OFFSET @offsetNumber rows fetch next @fetchNumber rows only";
            }
            else if (status == 0)
            {
                sql += @"select rg.RegisteredGroup_ID,rg.GroupIdea_ID,gi.ProjectEnglishName,s.Specialty_FullName
                                ,rg.Registered_Supervisor_Name_1,rg.Registered_Supervisor_Name_2 
                                ,rg.Staff_Comment             
                                            from RegisteredGroups rg
                                            join GroupIdeas gi on gi.GroupIdea_ID =  rg.GroupIdea_ID
                                            join Specialties s on s.Specialty_ID = gi.Specialty_ID
                                            where gi.Semester_ID = @semesterId and rg.[Status] = @status 
                                and(@searchText =  '' or REPLACE(UPPER(gi.ProjectEnglishName), ' ', '') like @searchText 
            			 or REPLACE(UPPER(s.Specialty_FullName), ' ', '') like @searchText 
            		     or REPLACE(UPPER(rg.Registered_Supervisor_Name_1), ' ', '') like @searchText
            			 or REPLACE(UPPER(rg.Registered_Supervisor_Name_2), ' ', '') like @searchText) 
                        and s.[Deleted_At] is null and rg.[Deleted_At] is null
            			  order by rg.Created_At asc
            			OFFSET @offsetNumber rows fetch next @fetchNumber rows only";
            }
            else
            {
                sql = @"select rg.RegisteredGroup_ID,rg.GroupIdea_ID,gi.ProjectEnglishName,s.Specialty_FullName
                                ,rg.Registered_Supervisor_Name_1,rg.Registered_Supervisor_Name_2 
                                ,rg.Staff_Comment             
                                            from RegisteredGroups rg
                                            join GroupIdeas gi on gi.GroupIdea_ID =  rg.GroupIdea_ID
                                            join Specialties s on s.Specialty_ID = gi.Specialty_ID
                                            where gi.Semester_ID = @semesterId and rg.[Status] = @status 
                                and(@searchText =  '' or REPLACE(UPPER(gi.ProjectEnglishName), ' ', '') like @searchText 
            			 or REPLACE(UPPER(s.Specialty_FullName), ' ', '') like @searchText 
            		     or REPLACE(UPPER(rg.Registered_Supervisor_Name_1), ' ', '') like @searchText
            			 or REPLACE(UPPER(rg.Registered_Supervisor_Name_2), ' ', '') like @searchText) 
                         and rg.[Deleted_At] is null
            			  order by rg.Created_At asc
            			OFFSET @offsetNumber rows fetch next @fetchNumber rows only";
            }
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[0].Value = semesterId;
            parameters[1] = new SqlParameter("@status", SqlDbType.Int);
            parameters[1].Value = status;
            parameters[2] = new SqlParameter("@searchText", SqlDbType.NVarChar);
            parameters[2].Value = searchText;
            parameters[3] = new SqlParameter("@offsetNumber", SqlDbType.Int);
            parameters[3].Value = offsetNumber;
            parameters[4] = new SqlParameter("@fetchNumber", SqlDbType.Int);
            parameters[4].Value = fetchNumber;

            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<RegisteredGroup> registeredGroups = new List<RegisteredGroup>();
            foreach (DataRow dr in result.Rows)
            {
                registeredGroups.Add(new RegisteredGroup()
                {
                    RegisteredGroupID = Convert.ToInt32(dr["RegisteredGroup_ID"].ToString()),
                    RegisteredSupervisorName1 = dr["Registered_Supervisor_Name_1"].ToString(),
                    RegisteredSupervisorName2 = dr["Registered_Supervisor_Name_2"].ToString(),
                    StaffComment = dr["Staff_Comment"].ToString(),
                    GroupIdea = new GroupIdea()
                    {
                        GroupIdeaID = Convert.ToInt32(dr["GroupIdea_ID"].ToString()),
                        ProjectEnglishName = dr["ProjectEnglishName"].ToString(),
                        Specialty = new Specialty()
                        {
                            SpecialtyFullName = dr["Specialty_FullName"].ToString()
                        }
                    }
                });
            }
            return registeredGroups;
        }


        public static int UpdateStatusByRegisteredGroupID(int registeredGroupID)
        {
            string sql = @"UPDATE RegisteredGroups SET [Status] = 1
                                WHERE RegisteredGroup_ID = @registeredGroupID";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@registeredGroupID", SqlDbType.Int);
            parameters[0].Value = registeredGroupID;
            return DbContext.ExecuteSQL(sql, parameters);
        }

        public static RegisteredGroup GetDetailRegistrationOfStudentByGroupIdeaId(int registeredGroupId)
        {
            string sql = @"select gi.ProjectEnglishName,gi.[Description],p.Profession_FullName,s.Specialty_FullName
                                ,gi.Abbreviation,gi.ProjectVietNameseName,gi.NumberOfMember,rg.Registered_Supervisor_Name_1
                                ,rg.Registered_Supervisor_Name_2,rg.Created_At,rg.Registered_Supervisor_Email_1,rg.Registered_Supervisor_Email_2
                                ,rg.Registered_Supervisor_Phone_1,rg.Registered_Supervisor_Phone_2,rg.Student_Comment
                                ,rg.Students_Registraiton,gi.GroupIdea_ID
                                from GroupIdeas gi
                                join Professions p on p.Profession_ID = gi.Profession_ID
                                join Specialties s on s.Specialty_ID = gi.Specialty_ID
                                join RegisteredGroups rg on rg.GroupIdea_ID = gi.GroupIdea_ID
                                where rg.RegisteredGroup_ID = @registeredGroupId and p.Deleted_At is null and s.Deleted_At is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@registeredGroupId", SqlDbType.Int);
            parameters[0].Value = registeredGroupId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new RegisteredGroup()
            {
                RegisteredSupervisorName1 = dr["Registered_Supervisor_Name_1"].ToString(),
                RegisteredSupervisorName2 = dr["Registered_Supervisor_Name_2"].ToString(),
                RegisteredSupervisorPhone1 = dr["Registered_Supervisor_Phone_1"].ToString(),
                RegisteredSupervisorPhone2 = dr["Registered_Supervisor_Phone_2"].ToString(),
                RegisteredSupervisorEmail1 = dr["Registered_Supervisor_Email_1"].ToString(),
                RegisteredSupervisorEmail2 = dr["Registered_Supervisor_Email_2"].ToString(),
                Students_Registration = dr["Students_Registraiton"].ToString(),
                CreatedAt = DateTime.ParseExact(Convert.ToDateTime(dr["Created_At"].ToString()).ToString("dd/MM/yyyy"), "dd/MM/yyyy", null),
                StudentComment = dr["Student_Comment"].ToString(),
                GroupIdea = new GroupIdea()
                {
                    GroupIdeaID = Convert.ToInt32(dr["GroupIdea_ID"]),
                    ProjectEnglishName = dr["ProjectEnglishName"].ToString(),
                    Description = dr["Description"].ToString(),
                    Profession = new Profession()
                    {
                        ProfessionFullName = dr["Profession_FullName"].ToString()
                    },
                    Specialty = new Specialty()
                    {
                        SpecialtyFullName = dr["Specialty_FullName"].ToString()
                    },
                    NumberOfMember = Convert.ToInt32(dr["NumberOfMember"].ToString()),
                    Abrrevation = dr["Abbreviation"].ToString(),
                    ProjectVietNameseName = dr["ProjectVietNameseName"].ToString()
                }
            };
        }

        public static int UpdateStaffCommentByRegisteredGroupID(string staffComment, int registeredGroupId)
        {
            string sql = @"UPDATE RegisteredGroups SET Staff_Comment = @staffComment, [Status] = 2
                            WHERE RegisteredGroup_ID = @registeredGroupId";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@staffComment", SqlDbType.NVarChar);
            parameters[0].Value = staffComment;
            parameters[1] = new SqlParameter("@registeredGroupId", SqlDbType.Int);
            parameters[1].Value = registeredGroupId;
            return DbContext.ExecuteSQL(sql, parameters);
        }

        public static int DeleteRecord(int id)
        {
            string sql = @"update RegisteredGroups
                        set Deleted_At = @deletedAt
                        where GroupIdea_ID = @Id and (Status = 0 or Status = 2) and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@deletedAt", SqlDbType.DateTime);
            parameters[0].Value = DateTime.Now;
            parameters[1] = new SqlParameter("@Id", SqlDbType.Int);
            parameters[1].Value = id;
            return DbContext.ExecuteSQL(sql, parameters);
        }

        public static int RejectWhenRegisteredGroupAccepted(int registeredGroupID, string commentReject, int groupIdeaId, int finalGroupId)
        {
            string sql = @"
                update RegisteredGroups set [Status] = 2 ,Staff_Comment = @commentReject where RegisteredGroup_ID = @registeredGroupId

                update GroupIdeas set Deleted_At = NULL where GroupIdea_ID = @groupIdeaId

                update FinalGroups set Deleted_At = CURRENT_TIMESTAMP where FinalGroup_ID = @finalGroupId
                
                update Students set FinalGroup_ID = NULL,GroupName = NULL,IsLeader = 0 where FinalGroup_ID = @finalGroupId";
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@registeredGroupID", SqlDbType.Int);
            parameters[0].Value = registeredGroupID;
            parameters[1] = new SqlParameter("@commentReject", SqlDbType.NVarChar);
            parameters[1].Value = commentReject;
            parameters[2] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[2].Value = groupIdeaId;
            parameters[3] = new SqlParameter("@finalGroupId", SqlDbType.Int);
            parameters[3].Value = finalGroupId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
    }
}
