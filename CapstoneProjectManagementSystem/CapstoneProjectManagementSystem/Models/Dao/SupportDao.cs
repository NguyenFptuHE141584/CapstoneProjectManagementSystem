using CapstoneProjectManagementSystem.Models.Dao.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models.Dao
{
    public class SupportDao
    {
        public static int AddSupportRequestThenReturnId(string title, string message, string studentId , string phoneNumber)
        {
            string sql = @"insert into [Supports](Title,SupportMessage,Student_ID,PhoneNumber)
                            values(@title, @message,@studentId,@phoneNumber)
                            SELECT SCOPE_IDENTITY() as [id]";
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@title", SqlDbType.NVarChar);
            parameters[0].Value = title;
            parameters[1] = new SqlParameter("@message", SqlDbType.NVarChar);
            parameters[1].Value = message;
            parameters[2] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[2].Value = studentId;
            parameters[3] = new SqlParameter("@phoneNumber", SqlDbType.VarChar);
            parameters[3].Value = phoneNumber;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            return Convert.ToInt32(dr["id"].ToString());
        }
        public static List<Support> GetAllPendingRequest()
        {
            string sql = "select * from [Supports] where Status = 0 and [Deleted_At] is null";
            DataTable result = DbContext.GetDataBySQL(sql,null);
            if (result.Rows.Count == 0) return null;
            List<Support> requestList = new List<Support>();
            foreach (DataRow dr in result.Rows)
            {
                requestList.Add(new Support() { 
                    SupportID = Convert.ToInt32(dr["Support_ID"]),
                    Title = (dr["Title"].ToString() == null) ? "" : dr["Title"].ToString(),
                    SupportMessge = (dr["SupportMessage"].ToString() == null) ? "" : dr["SupportMessage"].ToString(),
                    Student =  new Student()
                    {
                        StudentID = dr["Student_ID"].ToString()
                    },
                    PhoneNumber = (dr["PhoneNumber"].ToString() == null) ? "" : dr["PhoneNumber"].ToString()
                });
            }
            return requestList;
        }
        public static List<Support> GetAllProcessedRequest()
        {
            string sql = "select * from [Supports] where Status = 1 and [Deleted_At] is null";
            DataTable result = DbContext.GetDataBySQL(sql, null);
            if (result.Rows.Count == 0) return null;
            List<Support> requestList = new List<Support>();
            foreach (DataRow dr in result.Rows)
            {
                requestList.Add(new Support()
                {
                    SupportID = Convert.ToInt32(dr["Support_ID"]),
                    Title = (dr["Title"].ToString() == null) ? "" : dr["Title"].ToString(),
                    SupportMessge = (dr["SupportMessage"].ToString() == null) ? "" : dr["SupportMessage"].ToString(),
                    Student = new Student()
                    {
                        StudentID = dr["Student_ID"].ToString()
                    },
                    PhoneNumber = (dr["PhoneNumber"].ToString() == null) ? "" : dr["PhoneNumber"].ToString()
                });
            }
            return requestList;
        }
        public static int UpdateStatusToProcessed(int requestID)
        {
            string sql = @"update [Supports]
                            set Status = 1
                            where Support_ID = @id and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int);
            parameters[0].Value = requestID;
            return DbContext.ExecuteSQL(sql, parameters);
        }
    }
}
