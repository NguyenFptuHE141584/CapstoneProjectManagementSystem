using CapstoneProjectManagementSystem.Models.Common;
using CapstoneProjectManagementSystem.Models.Dao.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models.Dao
{
    public class Student_GroupIdeaDao
    {
        //NguyenLH
        public static string GetLeaderIdByGroupIdeaId(int groupIdeaId)
        {
            string sql = "select Student_ID from Student_GroupIdea " +
                "where GroupIdea_ID = @groupIdeaID and [Status] = 1 and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@groupIdeaID", SqlDbType.Int);
            parameters[0].Value = groupIdeaId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return dr["Student_ID"].ToString();
        }
        public static List<string> GetMemberIdByGroupIdeaId(int groupIdeaId)
        {
            string sql = "select Student_ID from Student_GroupIdea " +
                "where GroupIdea_ID = @groupIdeaID and [Status] = 2 and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@groupIdeaID", SqlDbType.Int);
            parameters[0].Value = groupIdeaId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<string> memberIdList = new List<string>();
            foreach (DataRow dr in result.Rows)
            {
                memberIdList.Add(dr["Student_ID"].ToString());
            }
            return memberIdList;
        }
        public static List<JoinRequest> GetAllJoinRequestByGroupIdeaId(int groupIdeaId)
        {
            string sql = "select u.User_ID, u.FptEmail,u.Avatar, sg.[Message], sg.Created_At " +
                "from Users u join Student_GroupIdea sg on u.User_ID = sg.Student_ID " +
                "where sg.Status = 3 and sg.GroupIdea_ID = @groupIdeaID and sg.Message is not null and sg.[Deleted_At] is null and u.[Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@groupIdeaID", SqlDbType.Int);
            parameters[0].Value = groupIdeaId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<JoinRequest> joinRequestList = new List<JoinRequest>();
            foreach (DataRow dr in result.Rows)
            {
                joinRequestList.Add(new JoinRequest
                {
                    UserId = dr["User_ID"].ToString(),
                    FptEmail = dr["FptEmail"].ToString(),
                    Avatar = dr["Avatar"].ToString(),
                    Message = dr["Message"].ToString(),
                    CreatedAt = Convert.ToDateTime(dr["Created_At"])
                });
            }
            return joinRequestList;
        }
        public static string GetGroupIdByStudentId(string studentId)
        {
            string sql = "select GroupIdea_ID from Student_GroupIdea " +
                "where Student_ID = @studentID and (Status = 1 or Status = 2) and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@studentID", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return dr["GroupIdea_ID"].ToString();
        }
        public static List<StudentGroupIdea> GetListRequestByStudentId(string studentId)
        {
            string sql = "select * from Student_GroupIdea " +
                "where Student_ID = @studentID and (Status = 3 or Status = 4 or Status = 5 ) and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@studentID", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<StudentGroupIdea> studentGroupIdeaList = new List<StudentGroupIdea>();
            foreach (DataRow dr in result.Rows)
            {
                studentGroupIdeaList.Add(new StudentGroupIdea()
                {
                    StudentID = dr["Student_ID"].ToString(),
                    GroupIdeaID = Convert.ToInt32(dr["GroupIdea_ID"]),
                    Status = Convert.ToInt32(dr["Status"]),
                    Message = dr["Message"].ToString(),
                    GroupIdea = new GroupIdea()
                    {
                        GroupIdeaID = Convert.ToInt32(dr["GroupIdea_ID"])
                    },
                });
            }
            return studentGroupIdeaList;
        }
        public static int UpdateStatusToLeader(string studentId, int groupIdeaId)
        {
            string sql = "update [Student_GroupIdea] " +
                "set [Status] = 1 " +
                "where [Student_ID] = @studentId and [GroupIdea_ID] = @groupIdeaId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            parameters[1] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[1].Value = groupIdeaId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int UpdateStatusToMember(string studentId, int groupIdeaId)
        {
            string sql = "update top(1) [Student_GroupIdea] " +
                "set [Status] = 2" +
                "where [Student_ID] = @studentId and [GroupIdea_ID] = @groupIdeaId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            parameters[1] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[1].Value = groupIdeaId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int UpdateStatusToAccept(string studentId, int groupIdeaId)
        {
            string sql = "update [Student_GroupIdea] " +
                "set [Status] = 4 " +
                "where [Student_ID] = @studentId and [GroupIdea_ID] = @groupIdeaId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            parameters[1] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[1].Value = groupIdeaId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int UpdateStatusToReject(string studentId, int groupIdeaId)
        {
            string sql = "update [Student_GroupIdea] " +
                "set [Status] = 5 " +
                "where [Student_ID] = @studentId and [GroupIdea_ID] = @groupIdeaId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            parameters[1] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[1].Value = groupIdeaId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int AddRecord(string studentId, int groupId, int status, string message)
        {
            string sql = @"insert into Student_GroupIdea(Student_ID,GroupIdea_ID,[Status],[Message])
                            values (@studentId,@groupId,@status,@message)";
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            parameters[1] = new SqlParameter("@groupId", SqlDbType.Int);
            parameters[1].Value = groupId;
            parameters[2] = new SqlParameter("@status", SqlDbType.Int);
            parameters[2].Value = status;
            parameters[3] = new SqlParameter("@message", SqlDbType.NVarChar);
            parameters[3].Value = message;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int DeleteRecord(string studentId, int groupIdeaId)
        {
            string sql = "update Student_GroupIdea " +
                "set Deleted_At = @deleteDate , [Status] = 6 " +
                "where Student_ID = @studentId and GroupIdea_ID = @groupIdeaId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("deleteDate", SqlDbType.DateTime);
            parameters[0].Value = DateTime.Now;
            parameters[1] = new SqlParameter("studentId", SqlDbType.VarChar);
            parameters[1].Value = studentId;
            parameters[2] = new SqlParameter("groupIdeaId", SqlDbType.Int);
            parameters[2].Value = groupIdeaId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int DeleteAllRequest(string studentId)
        {
            string sql = @"update Student_GroupIdea
                            set Deleted_At= @deleteDate
                            where Student_ID= @studentId and (([Status]) = 3 or ([Status] = 4) or ([Status]=5)) and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("deleteDate", SqlDbType.DateTime);
            parameters[0].Value = DateTime.Now;
            parameters[1] = new SqlParameter("studentId", SqlDbType.VarChar);
            parameters[1].Value = studentId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int DeleteAllRecordOfGroupIdea(int groupIdeaId)
        {
            string sql = @"update Student_GroupIdea
                            set Deleted_At= @deleteDate
                            where GroupIdea_ID = @groupIdeaId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("deleteDate", SqlDbType.DateTime);
            parameters[0].Value = DateTime.Now;
            parameters[1] = new SqlParameter("groupIdeaId", SqlDbType.Int);
            parameters[1].Value = groupIdeaId;
            return DbContext.ExecuteSQL(sql, parameters);
        }

        public static int DeleteRecordHaveStatusEqual3or4or5OfGroupIdea(int groupIdeaId)
        {
            string sql = @"update Student_GroupIdea
                            set Deleted_At= @deleteDate
                            where GroupIdea_ID = @groupIdeaId and [Status] in (3,4,5) and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("deleteDate", SqlDbType.DateTime);
            parameters[0].Value = DateTime.Now;
            parameters[1] = new SqlParameter("groupIdeaId", SqlDbType.Int);
            parameters[1].Value = groupIdeaId;
            return DbContext.ExecuteSQL(sql, parameters);
        }

        //NguyenNH
        public static bool CheckStudentHaveIdea(string studentId, int semesterId)
        {
            string sql = @"select count(*) as Status from Students s 
                            join Student_GroupIdea sg on sg.Student_ID = s.Student_ID
                            join GroupIdeas gi on gi.GroupIdea_ID = sg.GroupIdea_ID
                            where s.Student_ID = @studentId and (sg.[Status] = 1  or  sg.[Status] = 2) 
                            and gi.Semester_ID = @semesterId                          
                            and sg.[Deleted_At] is null and s.[Deleted_At] is null and gi.[Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            parameters[1] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[1].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return false;  // false: student dont't have in table student_groupidea
            DataRow dr = result.Rows[0];
            if (Convert.ToInt32(dr["Status"].ToString()) == 1)
                return true; //student have idea or student is member of one group
            else
                return false; // student just request to group don't have idea
        }
        public static int CheckStatusOfStudentInGroupIdea(string studentId)
        {
            string sql = @"select sg.[Status] from Students s  join Student_GroupIdea sg on sg.Student_ID = s.Student_ID
                            where s.Student_ID = @studentId and sg.[Deleted_At] is null and s.[Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;  // 0: user does not belong to an idea yet 
            DataRow dr = result.Rows[0];
            if (Convert.ToInt32(dr["Status"].ToString()) == 1)
                return 1; //user belong to leader
            else if (Convert.ToInt32(dr["Status"].ToString()) == 2)
                return 2; // user belong to member
            else
                return 3; //  user is requesting a group or is waiting for a reply that the group has been accepted 
        }
        public static GroupIdea GetGroupIdeaOfStudent(string studentId, int status)
        {

            string sql = @"select gi.Profession_ID,gi.Specialty_ID,gi.ProjectEnglishName,gi.Abbreviation,gi.ProjectVietNameseName,gi.[Description]
                            ,gi.ProjectTags,gi.GroupIdea_ID from Student_GroupIdea  sg
                            join GroupIdeas gi on gi.GroupIdea_ID = sg.GroupIdea_ID
                            where sg.Student_ID = @studentId and sg.[Status] = @status and sg.[Deleted_At] is null and gi.[Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            parameters[1] = new SqlParameter("@status", SqlDbType.Int);
            parameters[1].Value = status;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new GroupIdea()
            {
                Profession = new Profession()
                {
                    ProfessionID = Convert.ToInt32(dr["Profession_ID"].ToString()),
                },
                Specialty = new Specialty()
                {
                    SpecialtyID = Convert.ToInt32(dr["Specialty_ID"].ToString()),
                },
                ProjectEnglishName = dr["ProjectEnglishName"].ToString(),
                Abrrevation = dr["Abbreviation"].ToString(),
                ProjectVietNameseName = dr["ProjectVietNameseName"].ToString(),
                Description = dr["Description"].ToString(),
                ProjectTags = dr["ProjectTags"].ToString(),
                GroupIdeaID = Convert.ToInt32(dr["GroupIdea_ID"].ToString())
            };
        }
        public static List<Student> GetStudentsHaveGroup(int groupId)
        {
            string sql = @"select s.Student_ID , u.FptEmail,u.Avatar,sg.[Status] from Students s 
                            join Student_GroupIdea sg on sg.Student_ID = s.Student_ID
                            join Users u on u.[User_ID] = s.[Student_ID]
                            where sg.GroupIdea_ID = @groupId and sg.[Status] between 1 and 2 and sg.[Deleted_At] is null and u.[Deleted_At] is null and s.[Deleted_At] is null order by sg.[Status] asc ";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@groupId", SqlDbType.Int);
            parameters[0].Value = groupId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            List<Student> students = new List<Student>();
            foreach (DataRow dr in result.Rows)
                students.Add(
                    new Student()
                    {
                        StudentID = dr["Student_ID"].ToString(),
                        User = new User()
                        {
                            FptEmail = dr["FptEmail"].ToString(),
                            Avatar = dr["Avatar"].ToString()
                        },
                    });
            return students;
        }
        public static List<int> GetListStatusOfStudentInEachGroupByFptEmail(string fptEmail)
        {
            var listStatusOfStudentInEachGroup = new List<int>();
            string sql = @"select sg.[Status] from Students s
                            join Users u on u.[User_ID] = s.Student_ID
                            join Student_GroupIdea sg on sg.Student_ID = s.Student_ID
                            where u.[FptEmail] = @fptEmail and sg.[Deleted_At] is null and u.[Deleted_At] is null and s.[Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@fptEmail", SqlDbType.VarChar);
            parameters[0].Value = fptEmail;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            foreach (DataRow dr in result.Rows)
            {
                listStatusOfStudentInEachGroup.Add(Convert.ToInt32(dr["Status"].ToString()));
            }
            return listStatusOfStudentInEachGroup;
        }
        public static int CreateIdea(GroupIdea groupIdea, string studentId, int semesterId, int maxMember)
        {
            string sql = @"insert into GroupIdeas (Profession_ID,Specialty_ID,ProjectEnglishName,ProjectVietNameseName
                        ,Abbreviation,[Description],ProjectTags,Semester_ID
                        ,NumberOfMember,MaxMember)
                        values(@professionId,@specialtyId,@projectEnglishName,@projectVietNameseName,
                        @abbreviation,@description,@projectTags,@semesterId,@numberOfMember,@maxMember)
                        SELECT SCOPE_IDENTITY() as [count]";
            SqlParameter[] parameters = new SqlParameter[10];
            parameters[0] = new SqlParameter("@professionId", SqlDbType.Int);
            parameters[0].Value = groupIdea.Profession.ProfessionID;
            parameters[1] = new SqlParameter("@specialtyId", SqlDbType.Int);
            parameters[1].Value = groupIdea.Specialty.SpecialtyID;
            parameters[2] = new SqlParameter("@projectEnglishName", SqlDbType.VarChar);
            parameters[2].Value = groupIdea.ProjectEnglishName;
            parameters[3] = new SqlParameter("@projectVietNameseName", SqlDbType.NVarChar);
            parameters[3].Value = groupIdea.ProjectVietNameseName;
            parameters[4] = new SqlParameter("@abbreviation", SqlDbType.VarChar);
            parameters[4].Value = groupIdea.Abrrevation;
            parameters[5] = new SqlParameter("@description", SqlDbType.NVarChar);
            parameters[5].Value = groupIdea.Description;
            parameters[6] = new SqlParameter("@projectTags", SqlDbType.NVarChar);
            parameters[6].Value = groupIdea.ProjectTags;
            parameters[7] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[7].Value = semesterId;
            parameters[8] = new SqlParameter("@numberOfMember", SqlDbType.Int);
            parameters[8].Value = 1;
            parameters[9] = new SqlParameter("@maxMember", SqlDbType.Int);
            parameters[9].Value = maxMember;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            return Convert.ToInt32(dr["count"].ToString());
        }

        public static int DeleteGroupIdeaAndStudentInGroupIdea(int groupIdeaId)
        {
            string sql = @"update GroupIdeas set Deleted_At = CURRENT_TIMESTAMP
                            where GroupIdea_ID = @groupIdeaId

                            update Student_GroupIdea 
                            set Deleted_At = CURRENT_TIMESTAMP
                            where GroupIdea_ID = @groupIdeaId";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[0].Value = groupIdeaId;
            return DbContext.ExecuteSQL(sql, parameters);
        }

        public static StudentGroupIdea GetStudentGroupIdeaByGroupIdeaIdAndFptEmail(int groupIdeaId, string fptEmail)
        {
            string sql = @"select sg.Student_ID,sg.[Status],sp.CodeOfGroupName from Student_GroupIdea sg
                            join Students s on s.Student_ID = sg.Student_ID
                            join [Users] u on u.[User_ID] = s.Student_ID
                            join Specialties sp on sp.Specialty_ID = s.Specialty_ID
                            where sg.GroupIdea_ID = @groupIdeaId and u.FptEmail = @fptEmail and sg.Deleted_At is null and s.Deleted_At is null and u.Deleted_At is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[0].Value = groupIdeaId;
            parameters[1] = new SqlParameter("@fptEmail", SqlDbType.VarChar);
            parameters[1].Value = fptEmail;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new StudentGroupIdea()
            {
                StudentID = dr["Student_ID"].ToString(),
                Status = Convert.ToInt32(dr["Status"].ToString()),
                Student = new Student()
                {
                    Specialty = new Specialty()
                    {
                        CodeOfGroupName = dr["CodeOfGroupName"].ToString()
                    }
                }
            };
        }

        public static List<StudentGroupIdea> GetListStudentInGroupByGroupIdeaId(int groupIdeaId)
        {
            string sql = @"select Student_ID,[Status], gi.ProjectEnglishName from Student_GroupIdea sg
                            join GroupIdeas gi on gi.GroupIdea_ID = sg.GroupIdea_ID
                            where sg.GroupIdea_ID = @groupIdeaId and ([Status] = 1 or [Status] = 2) 
                            and (sg.Deleted_At is null and gi.Deleted_At is null)";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[0].Value = groupIdeaId;
            var listStudentGroupIdea = new List<StudentGroupIdea>();
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            foreach (DataRow dr in result.Rows)
            {
                listStudentGroupIdea.Add(new StudentGroupIdea
                {
                    StudentID = dr["Student_ID"].ToString(),
                    Status = Convert.ToInt32(dr["Status"].ToString()),
                    GroupIdea = new GroupIdea()
                    {
                        ProjectEnglishName = dr["ProjectEnglishName"].ToString(),
                    }
                });
            }
            return listStudentGroupIdea;
        }

        public static List<StudentGroupIdea> GetInforStudentInGroupIdea(int groupIdeaId)
        {
            string sql = @"select gi.GroupIdea_ID, s.Student_ID,u.Avatar,u.FptEmail,p.Profession_FullName
                                ,sp.Specialty_FullName,sp.CodeOfGroupName,sg.[Status] from GroupIdeas gi 
                        join Student_GroupIdea  sg on sg.GroupIdea_ID = gi.GroupIdea_ID
                        join Students s on s.Student_ID = sg.Student_ID
                        join Users u on u.[User_ID] = s.Student_ID
                        join Professions p on p.Profession_ID = s.Profession_ID
                        join Specialties sp on sp.Specialty_ID = s.Specialty_ID and sg.Deleted_At is null
                        where gi.GroupIdea_ID =  @groupIdeaId and (sg.[Status] = 1 or sg.[Status] = 2) order by sg.[Status] asc";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[0].Value = groupIdeaId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) result = null;
            List<StudentGroupIdea> studentGroupIdeas = new List<StudentGroupIdea>();
            foreach (DataRow dr in result.Rows)
            {
                studentGroupIdeas.Add(new StudentGroupIdea()
                {
                    GroupIdeaID = Convert.ToInt32(dr["GroupIdea_ID"]),
                    StudentID  = dr["Student_ID"].ToString(),
                    Student = new Student()
                    {
                        User = new User()
                        {
                            FptEmail = dr["FptEmail"].ToString(),
                            Avatar = dr["Avatar"].ToString(),
                        },
                        Profession = new Profession()
                        {
                            ProfessionFullName = dr["Profession_FullName"].ToString(),
                        },
                        Specialty = new Specialty()
                        {
                            SpecialtyFullName = dr["Specialty_FullName"].ToString(),
                            CodeOfGroupName = dr["CodeOfGroupName"].ToString(),
                        }
                    },
                   Status = Convert.ToInt32(dr["Status"]),
                }) ;
            }
            return studentGroupIdeas;
        }

        public static int RecoveryStudentInGroupIdeaAfterRejected(string studentId, int groupIdeaId)
        {
            string sql = @"update Student_GroupIdea set Deleted_At = NULL where Student_ID =  @studentId and GroupIdea_ID = @groupIdeaId and [Status] in (1,2)";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            parameters[1] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[1].Value = groupIdeaId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
    }
}
