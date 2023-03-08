using CapstoneProjectManagementSystem.Models.Dao.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models.Dao
{
    public class FinalGroupDao
    {
        //NguyenLH
        public static List<FinalGroup> getAllFinalGroups(int semester_Id)
        {
            string sql = "select * " +
                        "from [FinalGroups] " +
                        "where Semester_ID = @Semester_ID " +
                        "and [Deleted_At] is null " +
                        "order by [Specialty_ID]";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Semester_ID", SqlDbType.Int);
            parameters[0].Value = semester_Id;

            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<FinalGroup> finalGroupList = new List<FinalGroup>();
            foreach (DataRow dr in result.Rows)
            {
                finalGroupList.Add(new FinalGroup()
                {
                    FinalGroupID = Convert.ToInt32(dr["FinalGroup_ID"]),
                    GroupName = dr["GroupName"].ToString(),
                    ProjectEnglishName = dr["ProjectEnglishName"].ToString(),
                    ProjectVietNameseName = dr["ProjectVietNameseName"].ToString(),
                    Abbreviation = dr["Abbreviation"].ToString(),
                    Profession = new Profession()
                    {
                        ProfessionID = Convert.ToInt32(dr["Profession_ID"])
                    },
                    Specialty = new Specialty()
                    {
                        SpecialtyID = Convert.ToInt32(dr["Specialty_ID"])
                    },
                    Description = dr["Description"].ToString(),
                    NumberOfMember = Convert.ToInt32(dr["NumberOfMember"]),
                    MaxMember = Convert.ToInt32(dr["MaxMember"]),
                    CreatedAt = Convert.ToDateTime(dr["Created_At"]),
                    Semester = new Semester()
                    {
                        SemesterID = Convert.ToInt32(dr["Semester_ID"])
                    }
                });

            }
            return finalGroupList;
        }
        public static FinalGroup getFinalGroupById(int id)
        {
            string sql = @"select * from [FinalGroups]
                           where [FinalGroup_ID] = @id and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int);
            parameters[0].Value = id;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new FinalGroup()
            {
                FinalGroupID = Convert.ToInt32(dr["FinalGroup_ID"]),
                Profession = new Profession
                {
                    ProfessionID = Convert.ToInt32(dr["Profession_ID"])
                },
                Specialty = new Specialty
                {
                    SpecialtyID = Convert.ToInt32(dr["Specialty_ID"])
                },
                ProjectEnglishName = dr["ProjectEnglishName"].ToString(),
                ProjectVietNameseName = dr["ProjectVietNameseName"].ToString(),
                Abbreviation = dr["Abbreviation"].ToString(),
                Description = dr["Description"].ToString(),
                NumberOfMember = Convert.ToInt32(dr["NumberOfMember"]),
                MaxMember = Convert.ToInt32(dr["MaxMember"]),
                //Supervisor = new Supervisor
                //{
                //    SupervisorID = dr["Supervisor_ID"].ToString()
                //},
                Semester = new Semester
                {
                    SemesterID = Convert.ToInt32(dr["Semester_ID"])
                },
                CreatedAt = Convert.ToDateTime(dr["Created_At"]),
                GroupName = dr["GroupName"].ToString(),
                Students = new List<Student>()
            };
        }
        public static List<FinalGroup> GetLackOfMemberFinalGroupSearchList(string semester_Id, string profession_Id, string specialty_Id, string searchText, int offsetNumber, int fetchNumber)
        {
            string sql = "select * " +
                        "from [FinalGroups] " +
                        "where Semester_ID = @Semester_ID " +
                        "and [NumberOfMember] < [MaxMember] " +
                        "and (@Profession_Id = '' or Profession_ID = @Profession_Id) " +
                        "and (@Specialty_Id = '' or Specialty_ID = @Specialty_Id) " +
                        "and (@SearchText = '' " +
                        "or REPLACE(UPPER(GroupName), ' ', '') like @SearchText " +
                        "or REPLACE(UPPER(ProjectEnglishName), ' ', '') like @SearchText) " +
                        "and [Deleted_At] is null " +
                        "order by [Created_At] desc " +
                        "OFFSET @OffsetNumber rows fetch next @FetchNumber rows only";

            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@Semester_ID", SqlDbType.VarChar);
            parameters[0].Value = semester_Id;
            parameters[1] = new SqlParameter("@Profession_Id", SqlDbType.VarChar);
            parameters[1].Value = profession_Id;
            parameters[2] = new SqlParameter("@Specialty_Id", SqlDbType.VarChar);
            parameters[2].Value = specialty_Id;
            parameters[3] = new SqlParameter("@SearchText", SqlDbType.NVarChar);
            parameters[3].Value = searchText;
            parameters[4] = new SqlParameter("@OffsetNumber", SqlDbType.Int);
            parameters[4].Value = offsetNumber;
            parameters[5] = new SqlParameter("@FetchNumber", SqlDbType.Int);
            parameters[5].Value = fetchNumber;

            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<FinalGroup> finalGroupList = new List<FinalGroup>();
            foreach (DataRow dr in result.Rows)
            {
                finalGroupList.Add(new FinalGroup()
                {
                    FinalGroupID = Convert.ToInt32(dr["FinalGroup_ID"]),
                    GroupName = dr["GroupName"].ToString(),
                    ProjectEnglishName = dr["ProjectEnglishName"].ToString(),
                    Profession = new Profession()
                    {
                        ProfessionID = Convert.ToInt32(dr["Profession_ID"])
                    },
                    Specialty = new Specialty()
                    {
                        SpecialtyID = Convert.ToInt32(dr["Specialty_ID"])
                    },
                    Description = dr["Description"].ToString(),
                    NumberOfMember = Convert.ToInt32(dr["NumberOfMember"]),
                    MaxMember = Convert.ToInt32(dr["MaxMember"]),
                    CreatedAt = Convert.ToDateTime(dr["Created_At"]),
                    Semester = new Semester()
                    {
                        SemesterID = Convert.ToInt32(dr["Semester_ID"])
                    }
                });

            }
            return finalGroupList;
        }
        public static List<FinalGroup> GetFullMemberFinalGroupSearchList(string semester_Id, string profession_Id, string specialty_Id, string searchText, int offsetNumber, int fetchNumber)
        {
            string sql = "select * " +
                        "from [FinalGroups] " +
                        "where Semester_ID = @Semester_ID " +
                        "and ([NumberOfMember] = [MaxMember] or [NumberOfMember] > [MaxMember])" +
                        "and (@Profession_Id = '' or Profession_ID = @Profession_Id) " +
                        "and (@Specialty_Id = '' or Specialty_ID = @Specialty_Id) " +
                        "and (@SearchText = '' " +
                        "or REPLACE(UPPER(GroupName), ' ', '') like @SearchText " +
                        "or REPLACE(UPPER(ProjectEnglishName), ' ', '') like @SearchText) " +
                        "and [Deleted_At] is null " +
                        "order by [Created_At] desc " +
                        "OFFSET @OffsetNumber rows fetch next @FetchNumber rows only";

            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@Semester_ID", SqlDbType.VarChar);
            parameters[0].Value = semester_Id;
            parameters[1] = new SqlParameter("@Profession_Id", SqlDbType.VarChar);
            parameters[1].Value = profession_Id;
            parameters[2] = new SqlParameter("@Specialty_Id", SqlDbType.VarChar);
            parameters[2].Value = specialty_Id;
            parameters[3] = new SqlParameter("@SearchText", SqlDbType.NVarChar);
            parameters[3].Value = searchText;
            parameters[4] = new SqlParameter("@OffsetNumber", SqlDbType.Int);
            parameters[4].Value = offsetNumber;
            parameters[5] = new SqlParameter("@FetchNumber", SqlDbType.Int);
            parameters[5].Value = fetchNumber;

            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<FinalGroup> finalGroupList = new List<FinalGroup>();
            foreach (DataRow dr in result.Rows)
            {
                finalGroupList.Add(new FinalGroup()
                {
                    FinalGroupID = Convert.ToInt32(dr["FinalGroup_ID"]),
                    GroupName = dr["GroupName"].ToString(),
                    ProjectEnglishName = dr["ProjectEnglishName"].ToString(),
                    Profession = new Profession()
                    {
                        ProfessionID = Convert.ToInt32(dr["Profession_ID"])
                    },
                    Specialty = new Specialty()
                    {
                        SpecialtyID = Convert.ToInt32(dr["Specialty_ID"])
                    },
                    Description = dr["Description"].ToString(),
                    NumberOfMember = Convert.ToInt32(dr["NumberOfMember"]),
                    MaxMember = Convert.ToInt32(dr["MaxMember"]),
                    CreatedAt = Convert.ToDateTime(dr["Created_At"]),
                    Semester = new Semester()
                    {
                        SemesterID = Convert.ToInt32(dr["Semester_ID"])
                    }
                });

            }
            return finalGroupList;
        }
        public static int UpdateNumberOfMemberWhenAdd(int groupId)
        {
            string sql = @"update FinalGroups
                        set NumberOfMember = (NumberOfMember + 1)
                        where FinalGroup_ID = @finalGroupId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@finalGroupId", SqlDbType.Int);
            parameters[0].Value = groupId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int UpdateNumberOfMemberWhenRemove(int groupId)
        {
            string sql = @"update FinalGroups
                        set NumberOfMember = (NumberOfMember - 1)
                        where FinalGroup_ID = @finalGroupId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@finalGroupId", SqlDbType.Int);
            parameters[0].Value = groupId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int UpdateGroupName(int groupId, string groupName)
        {
            string sql = @"update FinalGroups
                        set GroupName = @groupName
                        where FinalGroup_ID = @finalGroupId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@groupName", SqlDbType.VarChar);
            parameters[0].Value = groupName;
            parameters[1] = new SqlParameter("@finalGroupId", SqlDbType.Int);
            parameters[1].Value = groupId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int DeleteFinalGroup(int finalGroupId)
        {
            string sql = @"update FinalGroups
                        set Deleted_At = @deletedAt
                        where FinalGroup_ID = @finalGroupId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@deletedAt", SqlDbType.DateTime);
            parameters[0].Value = DateTime.Now;
            parameters[1] = new SqlParameter("@finalGroupId", SqlDbType.Int);
            parameters[1].Value = finalGroupId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int CreateFinalGroup(int semesterId, int professionId, int specilatyId,string groupName, string englishName, string abbreviation, string vietnameseName, int maxMember, int numberOfMember)
        {
            string sql = @"insert into FinalGroups(Profession_ID,Specialty_ID,GroupName,ProjectEnglishName,ProjectVietNameseName
                                                ,Abbreviation,Semester_ID,MaxMember,NumberOfMember)
                            values(@professionId,@specialtyId,@groupName,@projectEnglishName,@projectVietNameseName,@abbreviation,@semesterId,@maxMember,@numberOfMember)
                            SELECT SCOPE_IDENTITY() as [count]";
            SqlParameter[] paramaters = new SqlParameter[9];
            paramaters[0] = new SqlParameter("@professionId", SqlDbType.Int);
            paramaters[0].Value = professionId;
            paramaters[1] = new SqlParameter("@specialtyId", SqlDbType.Int);
            paramaters[1].Value = specilatyId;
            paramaters[2] = new SqlParameter("@groupName", SqlDbType.VarChar);
            paramaters[2].Value = groupName;
            paramaters[3] = new SqlParameter("@projectEnglishName", SqlDbType.VarChar);
            paramaters[3].Value = englishName;
            paramaters[4] = new SqlParameter("@projectVietNameseName", SqlDbType.NVarChar);
            paramaters[4].Value = vietnameseName;
            paramaters[5] = new SqlParameter("@abbreviation", SqlDbType.VarChar);
            paramaters[5].Value = abbreviation;
            paramaters[6] = new SqlParameter("@semesterId", SqlDbType.Int);
            paramaters[6].Value = semesterId;
            paramaters[7] = new SqlParameter("@maxMember", SqlDbType.Int);
            paramaters[7].Value = maxMember;
            paramaters[8] = new SqlParameter("@numberOfMember", SqlDbType.Int);
            paramaters[8].Value = numberOfMember;
            DataTable result = DbContext.GetDataBySQL(sql, paramaters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            return Convert.ToInt32(dr["count"].ToString());
        }
        //NguyenNH
        public static int AddRegisteredGroupToFinalGroup(GroupIdea groupIdea,string groupName)
        {
            string sql = @"insert into FinalGroups
                        (Profession_ID,Specialty_ID,ProjectEnglishName,ProjectVietNameseName
                        ,Abbreviation,Semester_ID,NumberOfMember,MaxMember,[Description]
                        ,GroupName)
                        values(@professionId,@specialtyId,@projectEnglishName,@projectVietNameseName
                        ,@abbreviation,@semesterId,@numberOfMember,@maxMember,@description
                        ,@groupName)
                        SELECT SCOPE_IDENTITY() as [count]";
            SqlParameter[] paramaters = new SqlParameter[10];
            paramaters[0] = new SqlParameter("@professionId", SqlDbType.Int);
            paramaters[0].Value = groupIdea.Profession.ProfessionID;
            paramaters[1] = new SqlParameter("@specialtyId", SqlDbType.Int);
            paramaters[1].Value = groupIdea.Specialty.SpecialtyID;
            paramaters[2] = new SqlParameter("@projectEnglishName", SqlDbType.VarChar);
            paramaters[2].Value = groupIdea.ProjectEnglishName;
            paramaters[3] = new SqlParameter("@projectVietNameseName", SqlDbType.NVarChar);
            paramaters[3].Value = groupIdea.ProjectVietNameseName;
            paramaters[4] = new SqlParameter("@abbreviation", SqlDbType.VarChar);
            paramaters[4].Value = groupIdea.Abrrevation;
            paramaters[5] = new SqlParameter("@semesterId", SqlDbType.Int);
            paramaters[5].Value = groupIdea.Semester.SemesterID;
            paramaters[6] = new SqlParameter("@numberOfMember", SqlDbType.Int);
            paramaters[6].Value = groupIdea.NumberOfMember;
            paramaters[7] = new SqlParameter("@maxMember", SqlDbType.Int);
            paramaters[7].Value = groupIdea.MaxMember;
            paramaters[8] = new SqlParameter("@description", SqlDbType.NVarChar);
            paramaters[8].Value = groupIdea.Description;
            paramaters[9] = new SqlParameter("@groupName", SqlDbType.VarChar);
            paramaters[9].Value = groupName;
            DataTable result = DbContext.GetDataBySQL(sql, paramaters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            return Convert.ToInt32(dr["count"].ToString());
        }

        public static FinalGroup GetFinalGroupByStudentIsLeader(string studentId, int semesterId)
        {
            string sql = @"select f.GroupName,f.FinalGroup_ID from Students st
                            join FinalGroups f on f.FinalGroup_ID = st.FinalGroup_ID
                            where st.IsLeader = 1 and st.Student_ID = @studentId
                            and f.Semester_ID = @semesterId and f.Deleted_At is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            parameters[1] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[1].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new FinalGroup()
            {
                FinalGroupID = Convert.ToInt32(dr["FinalGroup_ID"].ToString()),
                GroupName = dr["GroupName"].ToString(),
            };
        }

        public static FinalGroup GetOldTopicByFinalGroupId(int finalGroupId)
        {
            string sql = @"select ProjectEnglishName,ProjectVietNameseName,Abbreviation from FinalGroups
                            where FinalGroup_ID = @finalGroupId and Deleted_At is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@finalGroupId", SqlDbType.Int);
            parameters[0].Value = finalGroupId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new FinalGroup()
            {
                ProjectEnglishName = dr["ProjectEnglishName"].ToString(),
                ProjectVietNameseName = dr["ProjectVietNameseName"].ToString(),
                Abbreviation = dr["Abbreviation"].ToString(),
            };
        }

        public static int GetMaxMemberOfFinalGroupByGroupName(string groupName, int semesterId)
        {
            string sql = @"select MaxMember from FinalGroups where GroupName = @groupName
                            and Semester_ID = @semesterId and Deleted_At is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@groupName", SqlDbType.VarChar);
            parameters[0].Value = groupName;
            parameters[1] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[1].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            return Convert.ToInt32(dr["MaxMember"].ToString());
        }

        public static List<Student> GetListCurrentMemberHaveFinalGroupByGroupName(string groupName, int semesterId)
        {
            string sql = @"select s.Student_ID,u.FptEmail,u.Avatar,s.IsLeader from Students s
                            inner join FinalGroups f on f.FinalGroup_ID = s.FinalGroup_ID
                            inner join Users u on u.[User_ID] = s.Student_ID
                            where f.GroupName = @groupName and f.Semester_ID = @semesterId 
                            and (s.Deleted_At is null and u.Deleted_At is null and f.Deleted_At is null)";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] =  new SqlParameter("@groupName",SqlDbType.VarChar);
            parameters[0].Value = groupName;
            parameters[1] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[1].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            List<Student> students = new List<Student>();
            if (result.Rows.Count == 0) return null;
            foreach (DataRow dr in result.Rows)
            {
                students.Add(new Student()
                {
                    StudentID = dr["Student_ID"].ToString(),
                    IsLeader = Convert.ToBoolean(dr["IsLeader"].ToString()),
                    User = new User()
                    {
                        FptEmail = dr["FptEmail"].ToString(),
                        Avatar = dr["Avatar"].ToString(),
                    }
                }) ;
            }
            return students;
        }

        public static int UpdateNewTopicForFinalGroup(ChangeTopicRequest changeTopicRequest)
        {
            string sql = @"update FinalGroups set ProjectEnglishName =@projectEnglishName
                            , ProjectVietNameseName = @projectVietNameseName ,Updated_At = CURRENT_TIMESTAMP
                            ,Abbreviation = @abbreviation where FinalGroup_ID = @finalGroupID";
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@projectEnglishName", SqlDbType.VarChar);
            parameters[0].Value = changeTopicRequest.NewTopicNameEnglish;
            parameters[1] = new SqlParameter("@projectVietNameseName", SqlDbType.NVarChar);
            parameters[1].Value = changeTopicRequest.NewTopicNameVietNamese;
            parameters[2] = new SqlParameter("@abbreviation", SqlDbType.VarChar);
            parameters[2].Value = changeTopicRequest.NewAbbreviation;
            parameters[3] = new SqlParameter("@finalGroupID", SqlDbType.Int);
            parameters[3].Value = changeTopicRequest.FinalGroup.FinalGroupID;
            return DbContext.ExecuteSQL(sql, parameters);
        }

        public static string GetLatestGroupName (string codeOfGroupName)
        {
            string sql = @"select top 1 GroupName from FinalGroups where GroupName like @codeOfGroupName and Semester_ID = (select Semester_ID from Semesters where StatusCloseBit = 1)
                            and Deleted_At is null order by Created_At desc";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@codeOfGroupName", SqlDbType.VarChar);
            parameters[0].Value = codeOfGroupName;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return dr["GroupName"].ToString();
        }
    }
}
