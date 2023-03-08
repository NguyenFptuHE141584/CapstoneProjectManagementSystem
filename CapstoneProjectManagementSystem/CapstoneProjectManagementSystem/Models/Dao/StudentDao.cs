using CapstoneProjectManagementSystem.Models.Dao.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models.Dao
{
    public class StudentDao
    {
        public static int UpdateStudentByGroupId(int finalGroupId, string groupName, int isLeader, string studentId)
        {
            string sql = @"UPDATE Students SET FinalGroup_ID = @finalGroupId,GroupName = @groupName, IsLeader = @status
                            WHERE Student_ID = @studentId and [Deleted_At] is null";
            SqlParameter[] paramaters = new SqlParameter[4];
            paramaters[0] = new SqlParameter("@finalGroupId", SqlDbType.Int);
            paramaters[0].Value = finalGroupId;
            paramaters[1] = new SqlParameter("@groupName", SqlDbType.VarChar);
            paramaters[1].Value = groupName;
            paramaters[2] = new SqlParameter("@status", SqlDbType.Int);
            paramaters[2].Value = isLeader;
            paramaters[3] = new SqlParameter("@studentId", SqlDbType.VarChar);
            paramaters[3].Value = studentId;
            return DbContext.ExecuteSQL(sql, paramaters);
        }
        public static int DeleteFinalGroupIdOfStudent(string studentId)
        {
            string sql = @"UPDATE Students SET FinalGroup_ID = null, GroupName = null, IsLeader = 0
                            WHERE Student_ID = @studentId;";
            SqlParameter[] paramaters = new SqlParameter[1];
            paramaters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            paramaters[0].Value = studentId;
            return DbContext.ExecuteSQL(sql, paramaters);
        }
        public static List<Student> GetStudentSearchList(string semester_Id, string profession_Id, string specialty_Id, int offsetNumber, int fetchNumber)
        {
            string sql = "select s.Student_ID , u.FptEmail , u.Avatar , u.FullName,s.Profession_ID, s.Specialty_ID , s.Created_At, s.Semester_ID from Students s " +
                        "join Users u on u.[User_ID] = s.Student_ID " +
                        "where s.Semester_ID = @Semester_ID " +
                        "and s.FinalGroup_ID is null " +
                        "and (@Profession_Id = '' or s.Profession_ID = @Profession_Id) " +
                        "and (@Specialty_Id = '' or s.Specialty_ID = @Specialty_Id) " +
                        "and s.[Deleted_At] is null " +
                        "and s.[Profession_ID] is not null " +
                        "and s.[Specialty_ID] is not null " +
                        "order by s.[Student_ID] " +
                        "OFFSET @OffsetNumber rows fetch next @FetchNumber rows only";

            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@Semester_ID", SqlDbType.VarChar);
            parameters[0].Value = semester_Id;
            parameters[1] = new SqlParameter("@Profession_Id", SqlDbType.VarChar);
            parameters[1].Value = profession_Id;
            parameters[2] = new SqlParameter("@Specialty_Id", SqlDbType.VarChar);
            parameters[2].Value = specialty_Id;
            parameters[3] = new SqlParameter("@OffsetNumber", SqlDbType.Int);
            parameters[3].Value = offsetNumber;
            parameters[4] = new SqlParameter("@FetchNumber", SqlDbType.Int);
            parameters[4].Value = fetchNumber;

            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<Student> studentList = new List<Student>();
            foreach (DataRow dr in result.Rows)
            {
                studentList.Add(new Student()
                {
                    StudentID = dr["Student_ID"].ToString(),
                    User = new User()
                    {
                        FptEmail = (dr["FptEmail"].ToString() == null) ? "" : dr["FptEmail"].ToString(),
                        Avatar = (dr["Avatar"].ToString() == null) ? "" : dr["Avatar"].ToString(),
                        FullName = (dr["FullName"].ToString() == null) ? "" : dr["FullName"].ToString()
                    },
                    Profession = new Profession()
                    {
                        ProfessionID = Convert.ToInt32(dr["Profession_ID"])
                    },
                    Specialty = new Specialty()
                    {
                        SpecialtyID = Convert.ToInt32(dr["Specialty_ID"]),
                        SpecialtyFullName = ""
                    },
                    CreatedAt = Convert.ToDateTime(dr["Created_At"]),
                    Semester = new Semester()
                    {
                        SemesterID = Convert.ToInt32(dr["Semester_ID"])
                    }
                });

            }
            return studentList;
        }

        public static Student GetStudentByStudentId(string studentId)
        {
            string sql = @"select s.Student_ID , u.FptEmail , u.Avatar , u.FullName, s.FinalGroup_ID, s.IsLeader, s.GroupName,s.Semester_ID
                            , s.Profession_ID , s.Specialty_ID, s.PhoneNumber from Students s
                            join Users u on u.[User_ID] = s.Student_ID
                            where s.Student_ID = @studentId and u.[Deleted_At] is null and s.[Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Student()
            {
                StudentID = dr["Student_ID"].ToString(),
                User = new User()
                {
                    FptEmail = (dr["FptEmail"].ToString() == null) ? "" : dr["FptEmail"].ToString(),
                    Avatar = (dr["Avatar"].ToString() == null) ? "" : dr["Avatar"].ToString(),
                    FullName = (dr["FullName"].ToString() == null) ? "" : dr["FullName"].ToString()
                },
                FinalGroup = new FinalGroup
                {
                    FinalGroupID = (dr["FinalGroup_ID"] is DBNull) ? 0 : Convert.ToInt32(dr["FinalGroup_ID"])
                },
                GroupName = (dr["GroupName"].ToString() == null) ? "" : dr["GroupName"].ToString(),
                IsLeader = Convert.ToBoolean(dr["IsLeader"]),
                Profession = new Profession()
                {
                    ProfessionID = (dr["Profession_ID"] is DBNull) ? 0 : Convert.ToInt32(dr["Profession_ID"]),
                    ProfessionFullName = "",
                },
                Specialty = new Specialty()
                {
                    SpecialtyID = (dr["Specialty_ID"] is DBNull) ? 0 : Convert.ToInt32(dr["Specialty_ID"]),
                    SpecialtyFullName = "",
                },
                PhoneNumber = (dr["PhoneNumber"].ToString() == null) ? "" : dr["PhoneNumber"].ToString(),
                Semester = new Semester()
                {
                    SemesterID = Convert.ToInt32(dr["Semester_ID"])
                }
            };
        }
        public static Student GetStudentByFptEmail(string fptEmail, int semesterId)
        {
            string sql = @"select  s.Student_ID ,s.RollNumber, u.FptEmail , u.Avatar, u.FullName, s.FinalGroup_ID from Students s
                            join Users u on u.[User_ID] = s.Student_ID 
                            where u.FptEmail = @fptEmail and s.Semester_ID = @semesterId
                            and u.[Deleted_At] is null and s.[Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@fptEmail", SqlDbType.VarChar);
            parameters[0].Value = fptEmail;
            parameters[1] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[1].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Student()
            {
                StudentID = dr["Student_ID"].ToString(),
                RollNumber = dr["RollNumber"].ToString(),
                User = new User()
                {
                    FptEmail = (dr["FptEmail"].ToString() == null) ? "" : dr["FptEmail"].ToString(),
                    Avatar = (dr["Avatar"].ToString() == null) ? "" : dr["Avatar"].ToString(),
                    FullName = (dr["FullName"].ToString() == null) ? "" : dr["FullName"].ToString()
                },
                FinalGroup = new FinalGroup
                {
                    FinalGroupID = (dr["FinalGroup_ID"] is DBNull) ? 0 : Convert.ToInt32(dr["FinalGroup_ID"])
                }
            };
        }

        public static Student GetStudentNotHaveGroupFinalByFptEmail(string fptEmail, int semesterId)
        {
            string sql = @"select  s.Student_ID , u.FptEmail , u.Avatar from Students s
                            join Users u on u.[User_ID] = s.Student_ID 
                            where u.FptEmail = @fptEmail and s.Semester_ID  = @semesterId
							and s.FinalGroup_ID is null
                            and u.[Deleted_At] is null and s.[Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@fptEmail", SqlDbType.VarChar);
            parameters[0].Value = fptEmail;
            parameters[1] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[1].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Student()
            {
                StudentID = dr["Student_ID"].ToString(),
                User = new User()
                {
                    FptEmail = dr["FptEmail"].ToString(),
                    Avatar = dr["Avatar"].ToString()
                },
            };
        }
        public static Student getLeaderByFinalGroupId(int finalGroupId)
        {
            string sql = @"select * from Students s
                        join Users u on u.[User_ID] = s.Student_ID
                        where s.FinalGroup_ID = @finalGroupId  and s.IsLeader = 1 and s.[Deleted_At] is null and u.[Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@finalGroupId", SqlDbType.Int);
            parameters[0].Value = finalGroupId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Student
            {
                StudentID = dr["Student_ID"].ToString(),
                Curriculum = (dr["Curriculum"].ToString() == null) ? "" : dr["Curriculum"].ToString(),
                EmailAddress = (dr["EmailAddress"].ToString() == null) ? "" : dr["EmailAddress"].ToString(),
                ExpectedRoleInGroup = (dr["ExpectedRoleInGroup"].ToString() == null) ? "" : dr["ExpectedRoleInGroup"].ToString(),
                SelfDescription = (dr["SelfDiscription"].ToString() == null) ? "" : dr["SelfDiscription"].ToString(),
                PhoneNumber = (dr["PhoneNumber"].ToString() == null) ? "" : dr["PhoneNumber"].ToString(),
                LinkFacebook = (dr["LinkFacebook"].ToString() == null) ? "" : dr["LinkFacebook"].ToString(),
                FinalGroup = new FinalGroup
                {
                    FinalGroupID = (dr["FinalGroup_ID"] is DBNull) ? 0 : Convert.ToInt32(dr["FinalGroup_ID"])
                },
                GroupName = (dr["GroupName"].ToString() == null) ? "" : dr["GroupName"].ToString(),
                RollNumber = dr["RollNumber"].ToString(),
                IsLeader = Convert.ToBoolean(dr["IsLeader"]),
                CreatedAt = Convert.ToDateTime(dr["Created_At"]),
                User = new User()
                {
                    FptEmail = (dr["FptEmail"].ToString() == null) ? "" : dr["FptEmail"].ToString(),
                    Avatar = (dr["Avatar"].ToString() == null) ? "" : dr["Avatar"].ToString(),
                    FullName = (dr["FullName"].ToString() == null) ? "" : dr["FullName"].ToString()
                },
                Profession = new Profession()
                {
                    ProfessionID = Convert.ToInt32(dr["Profession_ID"])
                },
                Specialty = new Specialty()
                {
                    SpecialtyID = Convert.ToInt32(dr["Specialty_ID"])
                }
            };
        }
        public static List<Student> getListMemberByFinalGroupId(int finalGroupId)
        {
            string sql = @"select * from Students s
                        join Users u on u.[User_ID] = s.Student_ID
                        where s.FinalGroup_ID = @finalGroupId  and s.IsLeader = 0 and s.[Deleted_At] is null and u.[Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@finalGroupId", SqlDbType.Int);
            parameters[0].Value = finalGroupId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<Student> memberList = new List<Student>();
            foreach (DataRow dr in result.Rows)
            {
                memberList.Add(new Student
                {
                    StudentID = dr["Student_ID"].ToString(),
                    Curriculum = (dr["Curriculum"].ToString() == null) ? "" : dr["Curriculum"].ToString(),
                    EmailAddress = (dr["EmailAddress"].ToString() == null) ? "" : dr["EmailAddress"].ToString(),
                    ExpectedRoleInGroup = (dr["ExpectedRoleInGroup"].ToString() == null) ? "" : dr["ExpectedRoleInGroup"].ToString(),
                    SelfDescription = (dr["SelfDiscription"].ToString() == null) ? "" : dr["SelfDiscription"].ToString(),
                    PhoneNumber = (dr["PhoneNumber"].ToString() == null) ? "" : dr["PhoneNumber"].ToString(),
                    LinkFacebook = (dr["LinkFacebook"].ToString() == null) ? "" : dr["LinkFacebook"].ToString(),
                    FinalGroup = new FinalGroup
                    {
                        FinalGroupID = (dr["FinalGroup_ID"] is DBNull) ? 0 : Convert.ToInt32(dr["FinalGroup_ID"])
                    },
                    GroupName = (dr["GroupName"].ToString() == null) ? "" : dr["GroupName"].ToString(),
                    RollNumber = dr["RollNumber"].ToString(),
                    IsLeader = Convert.ToBoolean(dr["IsLeader"]),
                    CreatedAt = Convert.ToDateTime(dr["Created_At"]),
                    User = new User()
                    {
                        FptEmail = (dr["FptEmail"].ToString() == null) ? "" : dr["FptEmail"].ToString(),
                        Avatar = (dr["Avatar"].ToString() == null) ? "" : dr["Avatar"].ToString(),
                        FullName = (dr["FullName"].ToString() == null) ? "" : dr["FullName"].ToString()
                    },
                    Profession = new Profession()
                    {
                        ProfessionID = Convert.ToInt32(dr["Profession_ID"])
                    },
                    Specialty = new Specialty()
                    {
                        SpecialtyID = Convert.ToInt32(dr["Specialty_ID"])
                    }
                });
            }
            return memberList;
        }
        public static List<Student> getListStudentNotHaveGroupBySpecialtyId(int semester_Id, int specialtyId)
        {
            string sql = @"select * from Students s
                        join Users u on u.[User_ID] = s.Student_ID
                        where s.Semester_ID = @Semester_ID and s.FinalGroup_ID is null and s.Specialty_ID = @specialtyId and s.[Deleted_At] is null and u.[Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Semester_ID", SqlDbType.Int);
            parameters[0].Value = semester_Id;
            parameters[1] = new SqlParameter("@specialtyId", SqlDbType.Int);
            parameters[1].Value = specialtyId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<Student> memberList = new List<Student>();
            foreach (DataRow dr in result.Rows)
            {
                memberList.Add(new Student
                {
                    StudentID = dr["Student_ID"].ToString(),
                    Curriculum = (dr["Curriculum"].ToString() == null) ? "" : dr["Curriculum"].ToString(),
                    EmailAddress = (dr["EmailAddress"].ToString() == null) ? "" : dr["EmailAddress"].ToString(),
                    ExpectedRoleInGroup = (dr["ExpectedRoleInGroup"].ToString() == null) ? "" : dr["ExpectedRoleInGroup"].ToString(),
                    SelfDescription = (dr["SelfDiscription"].ToString() == null) ? "" : dr["SelfDiscription"].ToString(),
                    PhoneNumber = (dr["PhoneNumber"].ToString() == null) ? "" : dr["PhoneNumber"].ToString(),
                    LinkFacebook = (dr["LinkFacebook"].ToString() == null) ? "" : dr["LinkFacebook"].ToString(),
                    IsLeader = Convert.ToBoolean(dr["IsLeader"]),
                    CreatedAt = Convert.ToDateTime(dr["Created_At"]),
                    User = new User()
                    {
                        FptEmail = (dr["FptEmail"].ToString() == null) ? "" : dr["FptEmail"].ToString(),
                        Avatar = (dr["Avatar"].ToString() == null) ? "" : dr["Avatar"].ToString(),
                        FullName = (dr["FullName"].ToString() == null) ? "" : dr["FullName"].ToString()
                    }
                });
            }
            return memberList;
        }

        public static Student GetProfileOfStudentByUserId(string userId)
        {
            string sql = @"select s.Student_ID,u.Avatar, u.FullName, s.RollNumber,s.Curriculum,u.Gender,u.FptEmail,s.ExpectedRoleInGroup
                            ,s.SelfDiscription,s.PhoneNumber,s.EmailAddress,s.LinkFacebook
							,se.Semester_ID,se.Semester_Name
                            ,Profession_ID,Specialty_ID
                            from Users u join Students s on s.Student_ID = u.[User_ID]
										 join Semesters se on se.Semester_ID = s.Semester_ID
                            where u.[User_ID] =  @userId and s.Deleted_At is null";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
            parameters[0].Value = userId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Student()
            {
                StudentID = dr["Student_ID"].ToString(),
                User = new User()
                {
                    FptEmail = (dr["FptEmail"].ToString() == null) ? "" : dr["FptEmail"].ToString(),
                    Avatar = (dr["Avatar"].ToString() == null) ? "" : dr["Avatar"].ToString(),
                    FullName = (dr["FullName"].ToString() == null) ? "" : dr["FullName"].ToString(),
                    Gender = Convert.ToInt32(dr["Gender"].ToString())
                },
                Profession = new Profession()
                {
                    ProfessionID = (dr["Profession_ID"] is DBNull) ? 0 : Convert.ToInt32(dr["Profession_ID"]),
                },
                Specialty = new Specialty()
                {
                    SpecialtyID = (dr["Specialty_ID"] is DBNull) ? 0 : Convert.ToInt32(dr["Specialty_ID"]) ,
                },
                Semester = new Semester()
                {
                    SemesterID = Convert.ToInt32(dr["Semester_ID"].ToString()),
                    SemesterName = dr["Semester_Name"].ToString()
                },
                RollNumber = dr["RollNumber"].ToString(),
                Curriculum = (dr["Curriculum"].ToString() == null) ? "" : dr["Curriculum"].ToString(),
                EmailAddress = (dr["EmailAddress"].ToString() == null) ? "" : dr["EmailAddress"].ToString(),
                ExpectedRoleInGroup = (dr["ExpectedRoleInGroup"].ToString() == null) ? "" : dr["ExpectedRoleInGroup"].ToString(),
                SelfDescription = (dr["SelfDiscription"].ToString() == null) ? "" : dr["SelfDiscription"].ToString(),
                PhoneNumber = (dr["PhoneNumber"].ToString() == null) ? "" : dr["PhoneNumber"].ToString(),
                LinkFacebook = (dr["LinkFacebook"].ToString() == null) ? "" : dr["LinkFacebook"].ToString()
            };
        }
        public static int UpdateInforProfileOfStudent(Student student)
        {
            string sql = @"BEGIN TRANSACTION
                                UPDATE Users SET Gender = @gender ,FullName =@fullName
                                WHERE [User_ID] = @userId

                                UPDATE Students SET ExpectedRoleInGroup = @expectedRoleInGroup
                                ,SelfDiscription = @selfDiscription
                                ,PhoneNumber = @phoneNumber , EmailAddress = @emailAddress 
                                ,LinkFacebook = @linkFacebook,Profession_ID = @professionId
                                ,Specialty_ID = @specialtyId
                                WHERE Student_ID =  @studentId
                            COMMIT;";
            SqlParameter[] paramaters = new SqlParameter[11];
            paramaters[0] = new SqlParameter("@gender", SqlDbType.Int);
            paramaters[0].Value = student.User.Gender;
            paramaters[1] = new SqlParameter("@userId", SqlDbType.VarChar);
            paramaters[1].Value = student.User.UserID;
            paramaters[2] = new SqlParameter("@expectedRoleInGroup", SqlDbType.VarChar);
            paramaters[2].Value = student.ExpectedRoleInGroup;
            paramaters[3] = new SqlParameter("@selfDiscription", SqlDbType.NVarChar);
            paramaters[3].Value = student.SelfDescription;
            paramaters[4] = new SqlParameter("@phoneNumber", SqlDbType.VarChar);
            paramaters[4].Value = student.PhoneNumber;
            paramaters[5] = new SqlParameter("@emailAddress", SqlDbType.VarChar);
            paramaters[5].Value = student.EmailAddress;
            paramaters[6] = new SqlParameter("@linkFacebook", SqlDbType.VarChar);
            paramaters[6].Value = student.LinkFacebook;
            paramaters[7] = new SqlParameter("@studentId", SqlDbType.VarChar);
            paramaters[7].Value = student.StudentID;
            paramaters[8] = new SqlParameter("@fullName", SqlDbType.NVarChar);
            paramaters[8].Value = student.User.FullName;
            paramaters[9] = new SqlParameter("@professionId", SqlDbType.Int);
            paramaters[9].Value = student.Profession.ProfessionID;
            paramaters[10] = new SqlParameter("@specialtyId", SqlDbType.Int);
            paramaters[10].Value = student.Specialty.SpecialtyID;
            return DbContext.ExecuteSQL(sql, paramaters);
        }
        public static int UpdateMajorOfStudentByUserId(string userId, int professionId, int specialtyId)
        {
            string sql = @"update Students
                            set Profession_ID  = @Profession_ID , Specialty_ID = @Specialty_ID
                            where Student_ID = @userId";
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@Profession_ID", SqlDbType.Int);
            parameters[0].Value = professionId;
            parameters[1] = new SqlParameter("@Specialty_ID", SqlDbType.Int);
            parameters[1].Value = specialtyId;
            parameters[2] = new SqlParameter("@userId", SqlDbType.VarChar);
            parameters[2].Value = userId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int UpdateSemesterOfStudentByUserId(string userId)
        {
            string sql = @"update Students
                            set Semester_ID  = (select Semester_ID from Semesters where StatusCloseBit = 1)
                            where Student_ID = @userId";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
            parameters[0].Value = userId;
            return DbContext.ExecuteSQL(sql, parameters);
        }

        public static int GetProfessionIdOfStudentByUserId(string userId)
        {
            string sql = @"select Profession_ID from Students 
                                where Student_ID = @userId
                                        and Deleted_At is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
            parameters[0].Value = userId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            if (dr["Profession_ID"] is DBNull)
                return 0;
            else
                return Convert.ToInt32(dr["Profession_ID"]);
        }
        public static int GetSpecialtyIdOfStudentByUserId(string userId)
        {
            string sql = @"select Specialty_ID from Students 
                                where Student_ID = @userId
                                        and Deleted_At is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
            parameters[0].Value = userId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            if (dr["Specialty_ID"] is DBNull)
                return 0;
            else
                return Convert.ToInt32(dr["Specialty_ID"]);
        }

        public static Student GetProfessionAndSpecialtyByStudentId(string studentId)
        {
            string sql = @"select s.Profession_ID , p.Profession_FullName,s.Specialty_ID,sp.Specialty_FullName from Students s 
                                join Professions p on p.Profession_ID = s.Profession_ID
                                join Specialties sp on sp.Specialty_ID = s.Specialty_ID
                                where Student_ID = @studentId
                                and s.Deleted_At is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Student()
            {
                Profession = new Profession()
                {
                    ProfessionID = Convert.ToInt32(dr["Profession_ID"]),
                    ProfessionFullName = (dr["Profession_FullName"].ToString() == null) ? "" : dr["Profession_FullName"].ToString(),
                },
                Specialty = new Specialty()
                {
                    SpecialtyID = Convert.ToInt32(dr["Specialty_ID"]),
                    SpecialtyFullName = (dr["Specialty_FullName"].ToString() == null) ? "" : dr["Specialty_FullName"].ToString(),
                }
            };
        }

        public static int UpdateFinalGroupIdForStudent(List<string> listStudentIdOfOldMember, List<string> listStudentIdOfNewMember
                                                        , int finalGroupId, int changeMemberRequestId)
        {
            string sql = "";
            string sqlForUpdateFinalIsNullOfOldMember = "";
            foreach (var item in listStudentIdOfOldMember)
            {
                sqlForUpdateFinalIsNullOfOldMember += @$"update Students set FinalGroup_ID = NULL
                                                    where Student_ID = {item} and FinalGroup_ID = {finalGroupId}";
            }
            string sqlForUpdateFinalOfNewMember = "";
            foreach (var item in listStudentIdOfNewMember)
            {
                sqlForUpdateFinalIsNullOfOldMember += @$"update Students set FinalGroup_ID = {finalGroupId}
                                                    where Student_ID = {item}  and FinalGroup_ID is null ";
            }
            sql = sqlForUpdateFinalIsNullOfOldMember + sqlForUpdateFinalOfNewMember
                          + @$"update ChangeMemberRequests set [Status] = 1
                            where ChangeMemberRequestID = {changeMemberRequestId}";
            return DbContext.ExecuteSQL(sql);
        }

        public static int SetFinalGroupForStudent(int finalGroupId, int isLeader, string studentId, string groupName)
        {
            string sql = @"UPDATE Students SET FinalGroup_ID = @finalGroupId, IsLeader = @status ,GroupName = @groupName
                            WHERE Student_ID = @studentId and [Deleted_At] is null";
            SqlParameter[] paramaters = new SqlParameter[4];
            paramaters[0] = new SqlParameter("@finalGroupId", SqlDbType.Int);
            paramaters[0].Value = finalGroupId;
            paramaters[1] = new SqlParameter("@status", SqlDbType.Int);
            paramaters[1].Value = isLeader;
            paramaters[2] = new SqlParameter("@studentId", SqlDbType.VarChar);
            paramaters[2].Value = studentId;
            paramaters[3] = new SqlParameter("@groupName", SqlDbType.VarChar);
            paramaters[3].Value = groupName;
            return DbContext.ExecuteSQL(sql, paramaters);
        }

        public static int UpdateGroupName(string studentId, string groupName)
        {
            string sql = @"update Students
                        set GroupName = @groupName
                        where Student_ID = @studentId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@groupName", SqlDbType.VarChar);
            parameters[0].Value = groupName;
            parameters[1] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[1].Value = studentId;
            return DbContext.ExecuteSQL(sql, parameters);
        }

        public static int GetFinalGroupIdOfStudentIsLeader(int groupIdeaId)
        {
            string sql = @"select FinalGroup_ID from Students 
                            where Student_ID = (select FinalGroup_ID from Students where Student_ID = 
                            (select Student_ID from Student_GroupIdea where GroupIdea_ID = @groupIdeaId and Deleted_At is null and [Status] = 1) 
                            and Deleted_At is null)";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[0].Value = groupIdeaId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            return Convert.ToInt32(dr["FinalGroup_ID"]);
        }

        public static List<Student> GetListStudentIdByFinalGroupId(int finalGroupId)
        {
            string sql = @"select Student_ID from Students where FinalGroup_ID = @finalGroupId";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@finalGroupId", SqlDbType.Int);
            parameters[0].Value = finalGroupId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<Student> student = new List<Student>();
            foreach (DataRow dr in result.Rows)
            {
                student.Add(new Student
                {
                    StudentID = dr["Student_ID"].ToString(),
                });
            }
            return student;
        }
        public static Student GetInforStudentHaveFinalGroup(string studentId, int semesterId)
        {
            string sql = @"select s.Student_ID,u.FptEmail,f.GroupName from Students s
                                join FinalGroups f on f.FinalGroup_ID = s.FinalGroup_ID
                                join Users u on u.[User_ID]  = s.Student_ID
                                where s.Student_ID = @studentId and f.Deleted_At is null
                                and f.Semester_ID = @semesterId";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            parameters[1] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[1].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Student()
            {
                StudentID = dr["Student_ID"].ToString(),
                User = new User()
                {
                    FptEmail = (dr["FptEmail"].ToString() == null) ? "" : dr["FptEmail"].ToString(),
                },
                FinalGroup = new FinalGroup()
                {
                    GroupName = (dr["GroupName"].ToString()==null) ? "" : dr["GroupName"].ToString(),
                }
            };
        }
        public static string GetStudentIDByFptEmailAndGroupName(string fptEmail, string groupName, int semesterId)
        {
            string sql = @"select s.Student_ID from Students s 
                                join FinalGroups f on f.FinalGroup_ID = s.FinalGroup_ID
                                join Users u on u.[User_ID] = s.Student_ID
                            where u.FptEmail = @fptEmail and f.GroupName = @groupName and f.Semester_ID = @semesterId
                            and f.Deleted_At is null";
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@fptEmail", SqlDbType.VarChar);
            parameters[0].Value = fptEmail;
            parameters[1] = new SqlParameter("@groupName", SqlDbType.VarChar);
            parameters[1].Value = groupName;
            parameters[2] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[2].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return dr["Student_ID"].ToString();
        }

        public static Student GetInforStudentHaveRegisteredGroup(string fptEmail,int groupIdeaId)
        {
            string sql = @"select s.Student_ID,u.Avatar,u.FptEmail,sp.Specialty_FullName,p.Profession_FullName, sg.[Status] from Students s
                                join Users u on u.[User_ID] = s.Student_ID
                                join Specialties sp on sp.Specialty_ID = s.Specialty_ID
                                join Professions p on p.Profession_ID = s.Profession_ID
                                join Student_GroupIdea sg on sg.Student_ID  = s.Student_ID
                                where u.FptEmail = @fptEmail and sg.GroupIdea_ID = @groupIdeaId ";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@fptEmail", SqlDbType.VarChar);
            parameters[0].Value = fptEmail; 
            parameters[1] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[1].Value = groupIdeaId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Student()
            {
                StudentID = dr["Student_ID"].ToString(),
                User = new User()
                {
                    FptEmail = (dr["FptEmail"].ToString() == null) ? "" : dr["FptEmail"].ToString(),
                    Avatar = (dr["Avatar"].ToString() == null) ? "" : dr["Avatar"].ToString()
                },
                Specialty = new Specialty()
                {
                    SpecialtyFullName = dr["Specialty_FullName"].ToString()
                },
                Profession = new Profession()
                {
                    ProfessionFullName = dr["Profession_FullName"].ToString(),
                },
                IsLeader = (Convert.ToInt32(dr["Status"]) != 1) ? false : true,
            };
        }

    }
}
