using CapstoneProjectManagementSystem.Models.Dao.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models.Dao
{
    public class SemesterDao
    {
        public static Semester GetCurrentSemester()
        {
            string sql = @"select Semester_ID,Semester_Name,Semester_Code,showGroupName
                            ,Start_Time,End_Time from Semesters
                            where StatusCloseBit = 1 and Deleted_At is null";
            DataTable result = DbContext.GetDataBySQL(sql);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Semester()
            {
                SemesterID = Convert.ToInt32(dr["Semester_ID"]),
                SemesterName = dr["Semester_Name"].ToString(),
                SemesterCode = dr["Semester_Code"].ToString(),
                StartTime = Convert.ToDateTime(dr["Start_Time"]),
                EndTime  = Convert.ToDateTime(dr["End_Time"]),
                ShowGroupName = Convert.ToBoolean(dr["showGroupName"])
            };
        }
        public static Semester GetSemesterById(int semesterId)
        {
            string sql = @"select Semester_ID,Semester_Name,Semester_Code
                            ,Start_Time,End_Time from Semesters
                            where Semester_ID = @semesterId ";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[0].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Semester()
            {
                SemesterID = Convert.ToInt32(dr["Semester_ID"]),
                SemesterName = dr["Semester_Name"].ToString(),
                SemesterCode = dr["Semester_Code"].ToString(),
                StartTime = Convert.ToDateTime(dr["Start_Time"]),
                EndTime = Convert.ToDateTime(dr["End_Time"])
            };
        }
        public static List<Semester> GetAllSemester()
        {
            string sql = @"select Semester_ID,Semester_Name,Semester_Code
                            ,Start_Time,End_Time from Semesters
                            order by Semester_ID desc";
            DataTable result = DbContext.GetDataBySQL(sql);
            if (result.Rows.Count == 0) return null;
            List<Semester> semesterList = new List<Semester>();
            foreach(DataRow dr in result.Rows)
            {
                semesterList.Add( new Semester()
                {
                    SemesterID = Convert.ToInt32(dr["Semester_ID"]),
                    SemesterName = dr["Semester_Name"].ToString(),
                    SemesterCode = dr["Semester_Code"].ToString(),
                    StartTime = Convert.ToDateTime(dr["Start_Time"]),
                    EndTime = Convert.ToDateTime(dr["End_Time"])
                });
            }
            return semesterList;
        }
        public static Semester GetLastSemester()
        {
            string sql = @"select top 1 Semester_ID,Semester_Name,Semester_Code,Start_Time,End_Time 
                            from Semesters
                            where StatusCloseBit = 0 and Deleted_At is null
                            order by Semester_ID desc";
            DataTable result = DbContext.GetDataBySQL(sql);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Semester()
            {
                SemesterID = Convert.ToInt32(dr["Semester_ID"]),
                SemesterName = dr["Semester_Name"].ToString(),
                SemesterCode = dr["Semester_Code"].ToString(),
                StartTime = Convert.ToDateTime(dr["Start_Time"]),
                EndTime = Convert.ToDateTime(dr["End_Time"])
            };
        }

        public static int UpdateCurrentSemester(Semester semester)
        {
            string sql = @"update Semesters
                        set Semester_Name =  @semesterName,Semester_Code = @semesterCode
                        , Start_Time = @startTime , End_Time = @endTime
                        where  Semester_ID = @semesterId and StatusCloseBit =  1";
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@semesterName", SqlDbType.VarChar);
            parameters[0].Value = semester.SemesterName;
            parameters[1] = new SqlParameter("@semesterCode", SqlDbType.VarChar);
            parameters[1].Value = semester.SemesterCode;
            parameters[2] = new SqlParameter("@startTime", SqlDbType.Date);
            parameters[2].Value = semester.StartTime;
            parameters[3] = new SqlParameter("@endTime", SqlDbType.Date);
            parameters[3].Value = semester.EndTime;
            parameters[4] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[4].Value = semester.SemesterID;
            return DbContext.ExecuteSQL(sql,parameters);
        }

        public static int AddNewSemester (Semester semester)
        {
            string sql = @"insert into Semesters(Semester_Name,Semester_Code,Start_Time,End_Time)
                          values(@semesterName,@semesterCode,@startTime,@endTime)";
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@semesterName", SqlDbType.VarChar);
            parameters[0].Value = semester.SemesterName;
            parameters[1] = new SqlParameter("@semesterCode", SqlDbType.VarChar);
            parameters[1].Value = semester.SemesterCode;
            parameters[2] = new SqlParameter("@startTime", SqlDbType.Date);
            parameters[2].Value = semester.StartTime;
            parameters[3] = new SqlParameter("@endTime", SqlDbType.Date);
            parameters[3].Value = semester.EndTime;
            return DbContext.ExecuteSQL(sql, parameters);
        }

        public static int CloseSemesterCurrent(int semesterId)
        {
            string sql = @"update Semesters set StatusCloseBit = 0 
                            where Semester_ID = @semesterId
                                update GroupIdeas 
                                set Deleted_At = CURRENT_TIMESTAMP

                                update Student_GroupIdea
							    set Deleted_At = CURRENT_TIMESTAMP

                                update FinalGroups  
                                set Deleted_At = CURRENT_TIMESTAMP

                                update Students 
                                set FinalGroup_ID = NULL ,GroupName = NULL  

                                update RegisteredGroups 
                                set Deleted_At = CURRENT_TIMESTAMP 

                                update Change_FinalGroup_Requests 
                                set Deleted_At =  CURRENT_TIMESTAMP

                                update ChangeTopicRequests 
                                set Deleted_At =  CURRENT_TIMESTAMP

                                update Notifications 
                                set Deleted_At = CURRENT_TIMESTAMP
                                
                                update Supports
                                set Deleted_At = CURRENT_TIMESTAMP
                                
                                update Student_FavoriteGroupIdea
                                set Deleted_At = CURRENT_TIMESTAMP";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[0].Value = semesterId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int ChangeShowGroupNameStatus(int semesterId,int status)
        {
            string sql = @"update Semesters set showGroupName = @status 
                            where Semester_ID = @semesterId";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@status", SqlDbType.Int);
            parameters[0].Value = status;
            parameters[1] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[1].Value = semesterId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
    }
}
