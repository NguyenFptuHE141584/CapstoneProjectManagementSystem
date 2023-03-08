using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapstoneProjectManagementSystem.Models.Dao.DBContext;

namespace CapstoneProjectManagementSystem.Models.Dao
{
    public class GroupIdeaDao
    {
        public static List<GroupIdea> GetGroupIdeaSearchList(string semester_Id,string profession_Id, string specialty_Id, string searchText, int offsetNumber, int fetchNumber )
        {
            string sql = "select * " +
                        "from [GroupIdeas] " +
                        "where Semester_ID = @Semester_ID " +
                        "and [NumberOfMember] < [MaxMember] " +
                        "and (@Profession_Id = '' or Profession_ID = @Profession_Id) " +
                        "and (@Specialty_Id = '' or Specialty_ID = @Specialty_Id) " +
                        "and (@SearchText = '' " +
                        "or REPLACE(UPPER(ProjectTags), ' ', '') like @SearchText " +
                        "or REPLACE(UPPER(ProjectEnglishName), ' ', '') like @SearchText " +
                        "or REPLACE(UPPER(Abbreviation), ' ', '') like @SearchText) " +
                        "and [Deleted_At] is null " +
                        "order by GroupIdea_ID " +
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
            List<GroupIdea> groupIdeaList = new List<GroupIdea>();
            foreach(DataRow dr in result.Rows)
            {
                groupIdeaList.Add(new GroupIdea()
                {
                    GroupIdeaID = Convert.ToInt32(dr["GroupIdea_ID"]),
                    ProjectEnglishName = dr["ProjectEnglishName"].ToString(),
                    ProjectTags = dr["ProjectTags"].ToString(),
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
                    Semester  = new Semester()
                    {
                        SemesterID = Convert.ToInt32(dr["Semester_ID"])
                    }
                });
               
            }
            return groupIdeaList;
        }
        public static List<GroupIdea> GetGroupIdeaSearchList_2(string semester_Id, string profession_Id, string specialty_Id, string searchText,string studentId,int offsetNumber,int fetchNumber)
        {
            string sql = "select * " +
                        "from [GroupIdeas] " +
                        "where Semester_ID = @Semester_ID " +
                        "and [NumberOfMember] < [MaxMember] " +
                        "and (@Profession_Id = '' or Profession_ID = @Profession_Id) " +
                        "and (@Specialty_Id = '' or Specialty_ID = @Specialty_Id) " +
                        "and (@SearchText = '' " +
                        "or REPLACE(UPPER(ProjectTags), ' ', '') like @SearchText " +
                        "or REPLACE(UPPER(ProjectEnglishName), ' ', '') like @SearchText " +
                        "or REPLACE(UPPER(Abbreviation), ' ', '') like @SearchText) " +
                        "and GroupIdea_ID <> (select GroupIdea_ID from Student_GroupIdea where Student_ID = @studentId and ([Status] = 1 or [Status] = 2) and [Deleted_At] is null)" +
                        "and [Deleted_At] is null " +
                        "order by GroupIdea_ID " +
                        "OFFSET @OffsetNumber rows fetch next @FetchNumber rows only";

            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@Semester_ID", SqlDbType.VarChar);
            parameters[0].Value = semester_Id;
            parameters[1] = new SqlParameter("@Profession_Id", SqlDbType.VarChar);
            parameters[1].Value = profession_Id;
            parameters[2] = new SqlParameter("@Specialty_Id", SqlDbType.VarChar);
            parameters[2].Value = specialty_Id;
            parameters[3] = new SqlParameter("@SearchText", SqlDbType.NVarChar);
            parameters[3].Value = searchText;
            parameters[4] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[4].Value = studentId;
            parameters[5] = new SqlParameter("@OffsetNumber", SqlDbType.Int);
            parameters[5].Value = offsetNumber;
            parameters[6] = new SqlParameter("@FetchNumber", SqlDbType.Int);
            parameters[6].Value = fetchNumber;

            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<GroupIdea> groupIdeaList = new List<GroupIdea>();
            foreach (DataRow dr in result.Rows)
            {
                groupIdeaList.Add(new GroupIdea()
                {
                    GroupIdeaID = Convert.ToInt32(dr["GroupIdea_ID"]),
                    ProjectEnglishName = dr["ProjectEnglishName"].ToString(),
                    ProjectTags = dr["ProjectTags"].ToString(),
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
            return groupIdeaList;
        }
        public static int getNumberOfResultWhenSearch(string semester_Id, string profession_Id, string specialty_Id, string searchText)
        {
            string sql = "select count(GroupIdea_ID) as [count] " +
                        "from [GroupIdeas] " +
                        "where Semester_ID = @Semester_ID " +
                        "and [NumberOfMember] < [MaxMember] " +
                        "and (@Profession_Id = '' or Profession_ID = @Profession_Id) " +
                        "and (@Specialty_Id = '' or Specialty_ID = @Specialty_Id) " +
                        "and (@SearchText = '' " +
                        "or REPLACE(UPPER(ProjectTags), ' ', '') like @SearchText " +
                        "or REPLACE(UPPER(ProjectEnglishName), ' ', '') like @SearchText " +
                        "or REPLACE(UPPER(Abbreviation), ' ', '') like @SearchText) " +
                        "and [Deleted_At] is null ";

            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@Semester_ID", SqlDbType.VarChar);
            parameters[0].Value = semester_Id;
            parameters[1] = new SqlParameter("@Profession_Id", SqlDbType.VarChar);
            parameters[1].Value = profession_Id;
            parameters[2] = new SqlParameter("@Specialty_Id", SqlDbType.VarChar);
            parameters[2].Value = specialty_Id;
            parameters[3] = new SqlParameter("@SearchText", SqlDbType.NVarChar);
            parameters[3].Value = searchText;

            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            return Convert.ToInt32(dr["count"]);
        }
        public static int getNumberOfResultWhenSearch_2(string semester_Id, string profession_Id, string specialty_Id, string searchText, string studentId)
        {
            string sql = "select count(GroupIdea_ID) as [count] " +
                        "from [GroupIdeas] " +
                        "where Semester_ID = @Semester_ID " +
                        "and [NumberOfMember] < [MaxMember] " +
                        "and (@Profession_Id = '' or Profession_ID = @Profession_Id) " +
                        "and (@Specialty_Id = '' or Specialty_ID = @Specialty_Id) " +
                        "and (@SearchText = '' " +
                        "or REPLACE(UPPER(ProjectTags), ' ', '') like @SearchText " +
                        "or REPLACE(UPPER(ProjectEnglishName), ' ', '') like @SearchText " +
                        "or REPLACE(UPPER(Abbreviation), ' ', '') like @SearchText) " +
                        "and GroupIdea_ID <> (select GroupIdea_ID from Student_GroupIdea where Student_ID = @studentId and ([Status] = 1 or [Status] = 2) and [Deleted_At] is null)" +
                        "and [Deleted_At] is null ";

            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@Semester_ID", SqlDbType.VarChar);
            parameters[0].Value = semester_Id;
            parameters[1] = new SqlParameter("@Profession_Id", SqlDbType.VarChar);
            parameters[1].Value = profession_Id;
            parameters[2] = new SqlParameter("@Specialty_Id", SqlDbType.VarChar);
            parameters[2].Value = specialty_Id;
            parameters[3] = new SqlParameter("@SearchText", SqlDbType.NVarChar);
            parameters[3].Value = searchText;
            parameters[4] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[4].Value = studentId;

            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            return Convert.ToInt32(dr["count"]);
        }
        public static GroupIdea GetGroupIdeaById(int Id)
        {
            string sql = "select * from [GroupIdeas] where [GroupIdea_ID] = @id and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int);
            parameters[0].Value = Id;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new GroupIdea()
            {
                GroupIdeaID = Convert.ToInt32(dr["GroupIdea_ID"]),
                Profession = new Profession()
                {
                    ProfessionID = Convert.ToInt32(dr["Profession_ID"])
                },
                Specialty = new Specialty()
                {
                    SpecialtyID = Convert.ToInt32(dr["Specialty_ID"])
                },
                ProjectEnglishName = dr["ProjectEnglishName"].ToString(),
                ProjectVietNameseName = dr["ProjectVietNameseName"].ToString(),
                Abrrevation = dr["Abbreviation"].ToString(),
                Description = dr["Description"].ToString(),
                ProjectTags = dr["ProjectTags"].ToString(),
                Semester = new Semester()
                {
                    SemesterID = Convert.ToInt32(dr["Semester_ID"])
                },
                NumberOfMember = Convert.ToInt32(dr["NumberOfMember"]),
                MaxMember = Convert.ToInt32(dr["MaxMember"]),
                CreatedAt = Convert.ToDateTime(dr["Created_At"])
            };
        }
        public static int UpdateNumberOfMemberWhenAdd(int groupIdeaId)
        {
            string sql = @"update GroupIdeas
                        set NumberOfMember = (NumberOfMember + 1)
                        where GroupIdea_ID = @groupIdeaId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[0].Value = groupIdeaId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int UpdateNumberOfMemberWhenRemove(int groupIdeaId)
        {
            string sql = @"update GroupIdeas
                        set NumberOfMember = (NumberOfMember - 1)
                        where GroupIdea_ID = @groupIdeaId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[0].Value = groupIdeaId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int UpdateIdea(GroupIdea groupIdea, int semesterId)
        {
            string sql = @"update GroupIdeas
                        set Profession_ID = @professionId,
                            Specialty_ID = @specialtyId,
                            ProjectEnglishName = @projectEnglishName,
                            ProjectVietNameseName = @projectVietNameseName,
                            Abbreviation = @abbreviation,
                            [Description] = @description,
                            ProjectTags = @projectTags,
                            Semester_ID = @semesterId
                        where GroupIdea_ID = @groupIdeaId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[9];
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
            parameters[8] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[8].Value = groupIdea.GroupIdeaID;
            return DbContext.ExecuteSQL(sql,parameters);
        }
        public static int DeleteGroupIdea(int groupIdeaId)
        {
            string sql = @"update GroupIdeas
                        set Deleted_At = @deletedAt
                        where GroupIdea_ID = @groupIdeaId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@deletedAt", SqlDbType.DateTime);
            parameters[0].Value = DateTime.Now;
            parameters[1] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[1].Value = groupIdeaId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
    }
}
