using CapstoneProjectManagementSystem.Models.Dao.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapstoneProjectManagementSystem.Models.Dao
{
    public class ChangeTopicRequestDao
    {
        public static int AddChangeTopicRequest(ChangeTopicRequest changeTopicRequest)
        {
            string sql = @"insert into ChangeTopicRequests 
                            (OldTopicNameEnglish,OldTopicNameVietNamese,OldAbbreviation
                            ,NewTopicNameEnglish,NewTopicNameVietNamese,NewAbbreviation
                            ,EmailSuperVisor,Reason_Change_Topic,FinalGroup_ID)
                          values (@oldEnglish,@oldVietnamese,@oldAbbreviation
                            ,@newEnglish,@newVietnamese,@newAbbreviation
                            ,@emailSperVisor,@reasonChangeTopic,@finalGroupId)";
            SqlParameter[] parameters = new SqlParameter[9];
            parameters[0] = new SqlParameter("@oldEnglish", SqlDbType.VarChar);
            parameters[0].Value = changeTopicRequest.OldTopicNameEnglish;
            parameters[1] = new SqlParameter("@oldVietnamese", SqlDbType.NVarChar);
            parameters[1].Value = changeTopicRequest.OldTopicNameVietNamese;
            parameters[2] = new SqlParameter("@oldAbbreviation", SqlDbType.VarChar);
            parameters[2].Value = changeTopicRequest.OldAbbreviation;
            parameters[3] = new SqlParameter("@newEnglish", SqlDbType.VarChar);
            parameters[3].Value = changeTopicRequest.NewTopicNameEnglish;
            parameters[4] = new SqlParameter("@newVietnamese", SqlDbType.NVarChar);
            parameters[4].Value = changeTopicRequest.NewTopicNameVietNamese;
            parameters[5] = new SqlParameter("@newAbbreviation", SqlDbType.VarChar);
            parameters[5].Value = changeTopicRequest.NewAbbreviation;
            parameters[6] = new SqlParameter("emailSperVisor", SqlDbType.VarChar);
            parameters[6].Value = changeTopicRequest.EmailSuperVisor;
            parameters[7] = new SqlParameter("@finalGroupId", SqlDbType.Int);
            parameters[7].Value = changeTopicRequest.FinalGroup.FinalGroupID;
            parameters[8] = new SqlParameter("@reasonChangeTopic", SqlDbType.NVarChar);
            parameters[8].Value = changeTopicRequest.ReasonChangeTopic;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int UpdateStatusOfChangeTopicRequest(int changeTopicRequestId, int status, string staffComment)
        {
            string sql = "";
            if (status == 1)
            {
                sql = @"update ChangeTopicRequests set [Status] = @status
                            where Request_ID = @changeTopicRequestId";
            }
            if (status == 2)
                sql = @"update ChangeTopicRequests set [Status] = @status , Staff_Comment = @staffComment
                            where Request_ID = @changeTopicRequestId";

            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@status", SqlDbType.Int);
            parameters[0].Value = status;
            parameters[1] = new SqlParameter("@changeTopicRequestId", SqlDbType.Int);
            parameters[1].Value = changeTopicRequestId;
            parameters[2] = new SqlParameter("@staffComment", SqlDbType.NVarChar);
            parameters[2].Value = staffComment;
            return DbContext.ExecuteSQL(sql, parameters);
        }

        public static ChangeTopicRequest GetNewTopicByChangeTopicRequestId(int changeTopicRequestId)
        {
            string sql = @"select FinalGroup_ID,NewTopicNameEnglish,NewTopicNameVietNamese,NewAbbreviation
                            from ChangeTopicRequests
                            where Request_ID = @changeTopicRequestId and Deleted_At is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@changeTopicRequestId", SqlDbType.Int);
            parameters[0].Value = changeTopicRequestId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new ChangeTopicRequest()
            {
                FinalGroup = new FinalGroup()
                {
                    FinalGroupID = Convert.ToInt32(dr["FinalGroup_ID"].ToString()),
                },
                NewTopicNameEnglish = dr["NewTopicNameEnglish"].ToString(),
                NewTopicNameVietNamese = dr["NewTopicNameVietNamese"].ToString(),
                NewAbbreviation = dr["NewAbbreviation"].ToString()
            };
        }

        public static List<ChangeTopicRequest> GetChangeTopicRequestsByStudentId(string studentId, int semesterId)
        {
            string sql = @"select c.Request_ID,f.GroupName,c.OldTopicNameEnglish,c.NewTopicNameEnglish
                            ,c.EmailSuperVisor,c.[Status],c.Staff_Comment
                            from ChangeTopicRequests c
                            join FinalGroups f on f.FinalGroup_ID = c.FinalGroup_ID
                            where c.FinalGroup_ID = (select FinalGroup_ID from Students where Student_ID = @studentId)
                            and f.Semester_ID = @semesterId and c.Deleted_At is null order by c.Created_At desc";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            parameters[1] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[1].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<ChangeTopicRequest> changeTopicRequests = new List<ChangeTopicRequest>();
            foreach (DataRow dr in result.Rows)
            {
                changeTopicRequests.Add(new ChangeTopicRequest()
                {
                    RequestID = Convert.ToInt32(dr["Request_ID"].ToString()),
                    FinalGroup = new FinalGroup()
                    {
                        GroupName = dr["GroupName"].ToString()
                    },
                    OldTopicNameEnglish = dr["OldTopicNameEnglish"].ToString(),
                    NewTopicNameEnglish = dr["NewTopicNameEnglish"].ToString(),
                    EmailSuperVisor = dr["EmailSuperVisor"].ToString(),
                    Status = Convert.ToInt32(dr["Status"].ToString()),
                    StaffComment = (dr["Staff_Comment"].ToString() == null) ? "" : dr["Staff_Comment"].ToString()
                });
            }
            return changeTopicRequests;
        }
        public static ChangeTopicRequest GetDetailChangeTopicRequestsByRequestId(int requestId)
        {
            string sql = @"select c.Request_ID,f.GroupName,c.OldTopicNameEnglish,c.OldTopicNameVietNamese,c.OldAbbreviation
                            ,c.NewTopicNameEnglish,c.NewTopicNameVietNamese,c.NewAbbreviation
                            ,c.EmailSuperVisor,c.[Status],Reason_Change_Topic,Staff_Comment
                            from ChangeTopicRequests c
                            join FinalGroups f on f.FinalGroup_ID = c.FinalGroup_ID
                            where c.Request_ID = @requestId  and c.Deleted_At is null ";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@requestId", SqlDbType.Int);
            parameters[0].Value = requestId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new ChangeTopicRequest()
            {
                RequestID = Convert.ToInt32(dr["Request_ID"].ToString()),
                FinalGroup = new FinalGroup()
                {
                    GroupName = dr["GroupName"].ToString()
                },
                OldTopicNameEnglish = dr["OldTopicNameEnglish"].ToString(),
                OldTopicNameVietNamese = dr["OldTopicNameVietNamese"].ToString(),
                OldAbbreviation = dr["OldAbbreviation"].ToString(),
                NewTopicNameEnglish = dr["NewTopicNameEnglish"].ToString(),
                NewTopicNameVietNamese = dr["NewTopicNameVietNamese"].ToString(),
                NewAbbreviation = dr["NewAbbreviation"].ToString(),
                EmailSuperVisor = dr["EmailSuperVisor"].ToString(),
                StaffComment = dr["Staff_Comment"].ToString(),
                ReasonChangeTopic = dr["Reason_Change_Topic"].ToString(),
                Status = Convert.ToInt32(dr["Status"].ToString())
            };
        }
        public static List<ChangeTopicRequest> GetChangeTopicRequestsBySearchText(string searchText, int status, int semesterId, int offsetNumber, int fetchNumber)
        {
            string sql = @"select ctr.Request_ID, f.GroupName, ctr.OldTopicNameEnglish
                            ,ctr.NewTopicNameEnglish
                            ,ctr.EmailSuperVisor ,ctr.Staff_Comment
                            from ChangeTopicRequests ctr 
                            join FinalGroups f on f.FinalGroup_ID =  ctr.FinalGroup_ID
                            join Semesters s on s.Semester_ID =  f.Semester_ID
                            where s.Semester_ID = @semesterId and ctr.[Status] = @status 
                            and(@searchText =  '' or REPLACE(UPPER(f.GroupName), ' ', '') like @searchText )
                            and ctr.Deleted_At is null order by ctr.Created_At desc
                            OFFSET @offsetNumber rows fetch next @fetchNumber rows only";
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
            List<ChangeTopicRequest> changeTopicRequests = new List<ChangeTopicRequest>();
            foreach (DataRow dr in result.Rows)
            {
                changeTopicRequests.Add(new ChangeTopicRequest()
                {
                    RequestID = Convert.ToInt32(dr["Request_ID"].ToString()),
                    FinalGroup = new FinalGroup()
                    {
                        GroupName = dr["GroupName"].ToString()
                    },
                    OldTopicNameEnglish = dr["OldTopicNameEnglish"].ToString(),
                    NewTopicNameEnglish = dr["NewTopicNameEnglish"].ToString(),
                    EmailSuperVisor = dr["EmailSuperVisor"].ToString(),
                    StaffComment = (dr["Staff_Comment"].ToString() == null) ? "" : dr["Staff_Comment"].ToString(),
                });
            }
            return changeTopicRequests;
        }

        public static int CountRecordChangeTopicRequestsBySearchText(string searchText, int status, int semesterId)
        {
            string sql = @"select count(*) as countChangeTopicRequest
                            from ChangeTopicRequests ctr 
                            join FinalGroups f on f.FinalGroup_ID =  ctr.FinalGroup_ID
                            join Semesters s on s.Semester_ID =  f.Semester_ID
                            where s.Semester_ID = @semesterId and ctr.[Status] = @status 
                            and(@searchText =  '' or REPLACE(UPPER(f.GroupName), ' ', '') like @searchText )
                            and ctr.Deleted_At is null ";
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
            return Convert.ToInt32(dr["countChangeTopicRequest"]);
        }


        public static int DeleteChangeTopicRequestsByFinalGroup(int finalGropId)
        {
            string sql = @"update ChangeTopicRequests 
                            set Deleted_At = CURRENT_TIMESTAMP
                            where FinalGroup_ID =  @finalGroupId";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@finalGroupId", SqlDbType.Int);
            parameters[0].Value = finalGropId;
            return DbContext.ExecuteSQL(sql, parameters);
        }

    }
}
