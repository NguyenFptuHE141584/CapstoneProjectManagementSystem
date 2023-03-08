using CapstoneProjectManagementSystem.Models.Dao.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models.Dao
{
    public class SpecialtyDao
    {
        public static List<Specialty> GetAllSpecialty(int semesterId)
        {
            string sql = "select * from [Specialties] where Semester_ID = @semesterId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[0].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<Specialty> specialtyList = new List<Specialty>();
            foreach (DataRow dr in result.Rows)
            {
                specialtyList.Add(new Specialty()
                {
                    SpecialtyID = Convert.ToInt32(dr["Specialty_ID"]),
                    SpecialtyAbbreviation = dr["Specialty_Abbreviation"].ToString(),
                    SpecialtyFullName = dr["Specialty_FullName"].ToString(),
                    MaxMember = Convert.ToInt32(dr["MaxMember"]),
                    CodeOfGroupName = dr["CodeOfGroupName"].ToString(),
                    Profession = new Profession()
                    {
                        ProfessionID = Convert.ToInt32(dr["Profession_ID"])
                    },
                    Semester = new Semester()
                    {
                        SemesterID = Convert.ToInt32(dr["Semester_ID"])
                    }
                });
            }
            return specialtyList;
        }
        public static Specialty GetSpecialtyById(int specialtyId)
        {
            string sql = "select * from [Specialties] where [Specialty_ID] = @specialtyId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@specialtyId", SqlDbType.Int);
            parameters[0].Value = specialtyId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Specialty()
            {
                SpecialtyID = Convert.ToInt32(dr["Specialty_ID"]),
                SpecialtyAbbreviation = dr["Specialty_Abbreviation"].ToString(),
                SpecialtyFullName = dr["Specialty_FullName"].ToString(),
                MaxMember = Convert.ToInt32(dr["MaxMember"]),
                CodeOfGroupName = dr["CodeOfGroupName"].ToString(),
                Profession = new Profession()
                {
                    ProfessionID = Convert.ToInt32(dr["Profession_ID"])
                },
                Semester = new Semester()
                {
                    SemesterID = (dr["Semester_ID"] is DBNull) ? 0: Convert.ToInt32(dr["Semester_ID"]),
                }
            };
        }
        public static Specialty GetSpecialtyByName(string specialtyFullname, int semesterId)
        {
            string sql = "select Specialty_ID from [Specialties] where REPLACE(UPPER([Specialty_FullName]), ' ', '') like @Specialty_FullName and Semester_ID = @semesterId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Specialty_FullName", SqlDbType.NVarChar);
            parameters[0].Value = specialtyFullname;
            parameters[1] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[1].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Specialty()
            {
                SpecialtyID = Convert.ToInt32(dr["Specialty_ID"])
            };
        }
        public static List<Specialty> GetSpecialtiesByProfessionId(int professionId, int semesterId)
        {
            string sql = "select * from [Specialties] where [Profession_ID] = @professionId and Semester_ID = @semesterId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@professionId", SqlDbType.Int);
            parameters[0].Value = professionId;
            parameters[1] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[1].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<Specialty> specialtyList = new List<Specialty>();
            foreach(DataRow dr in result.Rows)
            {
                specialtyList.Add(new Specialty()
                {
                    SpecialtyID = Convert.ToInt32(dr["Specialty_ID"]),
                    SpecialtyAbbreviation = dr["Specialty_Abbreviation"].ToString(),
                    SpecialtyFullName = dr["Specialty_FullName"].ToString(),
                    MaxMember = Convert.ToInt32(dr["MaxMember"]),
                    CodeOfGroupName = dr["CodeOfGroupName"].ToString(),
                    Profession = new Profession()
                    {
                        ProfessionID = Convert.ToInt32(dr["Profession_ID"])
                    },
                    Semester = new Semester()
                    {
                        SemesterID = Convert.ToInt32(dr["Semester_ID"])
                    }
                });
            }
            return specialtyList;
        }
        public static int AddSpecialtyThenReturnId(string abbreviation, string fullName ,int professionId, int maxMember, string code, int semesterId)
        {
            string sql = @"insert into Specialties(Specialty_Abbreviation,Specialty_FullName,Profession_ID,MaxMember,CodeOfGroupName,Semester_ID)
                            values(@Abbreviation,@FullName,@ProfessionId,@MaxMember,@Code,@semesterId)
                            SELECT SCOPE_IDENTITY() as [id]";
            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@Abbreviation", SqlDbType.VarChar);
            parameters[0].Value = abbreviation;
            parameters[1] = new SqlParameter("@FullName", SqlDbType.NVarChar);
            parameters[1].Value = fullName;
            parameters[2] = new SqlParameter("@ProfessionId", SqlDbType.Int);
            parameters[2].Value = professionId;
            parameters[3] = new SqlParameter("@MaxMember", SqlDbType.Int);
            parameters[3].Value = maxMember;
            parameters[4] = new SqlParameter("@Code", SqlDbType.VarChar);
            parameters[4].Value = code;
            parameters[5] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[5].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            return Convert.ToInt32(dr["id"].ToString());
        }

        public static int UpdateSpecialty(int specId, string Abbreviation, string FullName,string code, int proId, int MaxMember)
        {
            string sql = @"update Specialties
                            set Specialty_Abbreviation = @Abbreviation, 
                                Specialty_FullName = @FullName ,
                                Profession_ID = @proId,
                                MaxMember = @MaxMember,
                                CodeOfGroupName = @Code,
                                Updated_At = @updateDate
                            where Specialty_ID = @specId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@Abbreviation", SqlDbType.VarChar);
            parameters[0].Value = Abbreviation;
            parameters[1] = new SqlParameter("@FullName", SqlDbType.NVarChar);
            parameters[1].Value = FullName;
            parameters[2] = new SqlParameter("@proId", SqlDbType.Int);
            parameters[2].Value = proId;
            parameters[3] = new SqlParameter("@MaxMember", SqlDbType.Int);
            parameters[3].Value = MaxMember;
            parameters[4] = new SqlParameter("@Code", SqlDbType.VarChar);
            parameters[4].Value = code;
            parameters[5] = new SqlParameter("@updateDate", SqlDbType.DateTime);
            parameters[5].Value = DateTime.Now;
            parameters[6] = new SqlParameter("@specId", SqlDbType.Int);
            parameters[6].Value = specId;
            return DbContext.ExecuteSQL(sql, parameters);
        }

        public static string GetCodeOfGroupNameByGroupIdeaId(int groupIdeaId)
        {
            string sql = @" select s.CodeOfGroupName from GroupIdeas gi
                            join Specialties s on s.Specialty_ID = gi.Specialty_ID
                            where GroupIdea_ID = @groupIdeaId and (gi.Deleted_At is null and s.Deleted_At is null)";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[0].Value = groupIdeaId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return dr["CodeOfGroupName"].ToString();
        }
    }
}
