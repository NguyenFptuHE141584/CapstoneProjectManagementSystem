using CapstoneProjectManagementSystem.Models.Dao.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models.Dao
{
    public class ProfessionDao
    {
        public static List<Profession> GetAllProfession(int semesterId)
        {
            string sql = "select * from [Professions] where Semester_ID = @semesterId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[0].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<Profession> professionList = new List<Profession>();
            foreach(DataRow dr in result.Rows){
                professionList.Add(new Profession()
                {
                    ProfessionID = Convert.ToInt32(dr["Profession_ID"]),
                    ProfessionAbbreviation = dr["Profession_Abbreviation"].ToString(),
                    ProfessionFullName = dr["Profession_FullName"].ToString(),
                    Semester = new Semester()
                    {
                        SemesterID = Convert.ToInt32(dr["Semester_ID"])
                    }
                });
            }
            return professionList;
        }
        public static Profession GetProfessionById(int professionId)
        {
            string sql = "select * from [Professions] where [Profession_ID] = @professionId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@professionId", SqlDbType.Int);
            parameters[0].Value = professionId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Profession()
            {
                ProfessionID = Convert.ToInt32(dr["Profession_ID"]),
                ProfessionAbbreviation = dr["Profession_Abbreviation"].ToString(),
                ProfessionFullName = dr["Profession_FullName"].ToString(),
                Semester = new Semester()
                {
                    SemesterID = (dr["Semester_ID"] is DBNull) ? 0 : Convert.ToInt32(dr["Semester_ID"]),
                }
            };
        }
        public static Profession GetProfessionByName(string professionFullname, int semesterId)
        {
            string sql = "select Profession_ID from [Professions] where REPLACE(UPPER([Profession_FullName]), ' ', '') like @Profession_FullName and Semester_ID = @semesterId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Profession_FullName", SqlDbType.NVarChar);
            parameters[0].Value = professionFullname;
            parameters[1] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[1].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Profession()
            {
                ProfessionID = Convert.ToInt32(dr["Profession_ID"])               
            };
        }
        public static int AddProfessionThenReturnId(string Abbreviation,string FullName, int semesterId)
        {
            string sql = @"insert into Professions(Profession_Abbreviation,Profession_FullName,Semester_ID)
                            values(@Abbreviation, @FullName,@semesterId)
                            SELECT SCOPE_IDENTITY() as [id]";
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@Abbreviation", SqlDbType.VarChar);
            parameters[0].Value = Abbreviation;
            parameters[1] = new SqlParameter("@FullName", SqlDbType.NVarChar);
            parameters[1].Value = FullName;
            parameters[2] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[2].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            return Convert.ToInt32(dr["id"].ToString());
        }
        public static int UpdateProfession(int id, string Abbreviation, string FullName)
        {
            string sql = @"update Professions
                            set Profession_Abbreviation = @Abbreviation, Profession_FullName = @FullName , Updated_At = @updateDate
                            where Profession_ID = @id and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@Abbreviation", SqlDbType.VarChar);
            parameters[0].Value = Abbreviation;
            parameters[1] = new SqlParameter("@FullName", SqlDbType.NVarChar);
            parameters[1].Value = FullName;
            parameters[2] = new SqlParameter("@updateDate", SqlDbType.DateTime);
            parameters[2].Value = DateTime.Now;
            parameters[3] = new SqlParameter("@id", SqlDbType.Int);
            parameters[3].Value = id;
            return DbContext.ExecuteSQL(sql,parameters);
        }
        public static int DeleteProfession(int id)
        {
            string sql = @"update Professions
                            set Deleted_At = @deleteDate
                            where Profession_ID = @id and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@deleteDate", SqlDbType.DateTime);
            parameters[0].Value = DateTime.Now;
            parameters[1] = new SqlParameter("@id", SqlDbType.Int);
            parameters[1].Value = id;
            return DbContext.ExecuteSQL(sql, parameters);
        }
    }
}
