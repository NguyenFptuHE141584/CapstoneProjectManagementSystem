using CapstoneProjectManagementSystem.Models.Dao.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models.Dao
{
    public class ChangeFinalGroupRequestDao
    {
        public static int CreateChangeFinalGroupRequestDao(string fromStudentId, string toStudentId)
        {
            string sql = @"insert into Change_FinalGroup_Requests (FromStudent_ID,ToStudent_ID)
                            values(@fromStudentId,@toStudentId)";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@fromStudentId", SqlDbType.VarChar);
            parameters[0].Value = fromStudentId;
            parameters[1] = new SqlParameter("@toStudentId", SqlDbType.VarChar);
            parameters[1].Value = toStudentId;
            return DbContext.ExecuteSQL(sql, parameters);
        }

        public static int UpdateStatusAcceptOfToStudentByChangeFinalGroupRequestId(int changeFinalGroupRequestId)
        {
            string sql = @"update Change_FinalGroup_Requests
                            set StatusOfTo = 1 where Change_FinalGroup_Request_ID = @changeFinalGroupRequestId";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@changeFinalGroupRequestId", SqlDbType.Int);
            parameters[0].Value = changeFinalGroupRequestId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int UpdateStatusRejectOfToStudentByChangeFinalGroupRequestId(int changeFinalGroupRequestId)
        {
            string sql = @"update Change_FinalGroup_Requests
                            set StatusOfTo = 2 where Change_FinalGroup_Request_ID = @changeFinalGroupRequestId";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@changeFinalGroupRequestId", SqlDbType.Int);
            parameters[0].Value = changeFinalGroupRequestId;
            return DbContext.ExecuteSQL(sql, parameters);
        }

        // get list request this student send request 
        public static List<ChangeFinalGroupRequest> GetListChangeFinalGroupRequestFromOfStudent(string fromStudentId, int semesterId)
        {
            string sql = @"select cfr.Change_FinalGroup_Request_ID,fromUser.FptEmail as fromEmail,fromGroup.GroupName as fromGroup
                                    ,toUser.FptEmail as toEmail,toGroup.GroupName as toGroup ,cfr.StatusOfTo
                                    from Change_FinalGroup_Requests cfr
                                    join Students fromStudent on fromStudent.Student_ID = cfr.FromStudent_ID
                                    join Users fromUser on fromUser.[User_ID] = fromStudent.Student_ID
                                    join FinalGroups fromGroup on fromGroup.FinalGroup_ID = fromStudent.FinalGroup_ID
                                    join Students toStudent on toStudent.Student_ID = cfr.ToStudent_ID
                                    join Users toUser on toUser.[User_ID] = toStudent.Student_ID
                                    join FinalGroups toGroup on toGroup.FinalGroup_ID = toStudent.FinalGroup_ID
                                    where cfr.FromStudent_ID = @fromStudentId
                                    and cfr.Deleted_At is null
                                    and fromGroup.Semester_ID = @semesterId and toGroup.Semester_ID = @semesterId 
                                    and (fromGroup.Deleted_At is null and toGroup.Deleted_At is null) order by cfr.Created_At desc";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@fromStudentId", SqlDbType.VarChar);
            parameters[0].Value = fromStudentId;
            parameters[1] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[1].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<ChangeFinalGroupRequest> changeFinalGroupRequests = new List<ChangeFinalGroupRequest>();
            foreach (DataRow dr in result.Rows)
            {
                changeFinalGroupRequests.Add(new ChangeFinalGroupRequest()
                {
                    ChangeFinalGroupRequestId = Convert.ToInt32(dr["Change_FinalGroup_Request_ID"].ToString()),
                    FromStudent = new Student()
                    {
                        User = new User()
                        {
                            FptEmail = dr["fromEmail"].ToString(),
                        },
                        FinalGroup = new FinalGroup()
                        {
                            GroupName = dr["fromGroup"].ToString(),
                        }
                    },
                    ToStudent = new Student()
                    {
                        User = new User()
                        {
                            FptEmail = dr["toEmail"].ToString(),
                        },
                        FinalGroup = new FinalGroup()
                        {
                            GroupName = dr["toGroup"].ToString(),
                        }
                    },
                    StatusOfToStudent = Convert.ToInt32(dr["StatusOfTo"].ToString())
                });
            }
            return changeFinalGroupRequests;
        }

        public static List<ChangeFinalGroupRequest> GetListChangeFinalGroupRequestToOfStudent(string toStudentId, int semesterId)
        {
            string sql = @"select cfr.Change_FinalGroup_Request_ID,fromUser.FptEmail as fromEmail,fromGroup.GroupName as fromGroup
                                    ,toUser.FptEmail as toEmail,toGroup.GroupName as toGroup ,cfr.StatusOfTo
                                    from Change_FinalGroup_Requests cfr
                                    join Students fromStudent on fromStudent.Student_ID = cfr.FromStudent_ID
                                    join Users fromUser on fromUser.[User_ID] = fromStudent.Student_ID
                                    join FinalGroups fromGroup on fromGroup.FinalGroup_ID = fromStudent.FinalGroup_ID
                                    join Students toStudent on toStudent.Student_ID = cfr.ToStudent_ID
                                    join Users toUser on toUser.[User_ID] = toStudent.Student_ID
                                    join FinalGroups toGroup on toGroup.FinalGroup_ID = toStudent.FinalGroup_ID
                                    where cfr.ToStudent_ID = @toStudentId
                                    and cfr.Deleted_At is null
                                    and fromGroup.Semester_ID = @semesterId and toGroup.Semester_ID = @semesterId 
                                    and (fromGroup.Deleted_At is null and toGroup.Deleted_At is null) order by cfr.Created_At desc";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@toStudentId", SqlDbType.VarChar);
            parameters[0].Value = toStudentId;
            parameters[1] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[1].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<ChangeFinalGroupRequest> changeFinalGroupRequests = new List<ChangeFinalGroupRequest>();
            foreach (DataRow dr in result.Rows)
            {
                changeFinalGroupRequests.Add(new ChangeFinalGroupRequest()
                {
                    ChangeFinalGroupRequestId = Convert.ToInt32(dr["Change_FinalGroup_Request_ID"].ToString()),
                    FromStudent = new Student()
                    {
                        User = new User()
                        {
                            FptEmail = dr["fromEmail"].ToString(),
                        },
                        FinalGroup = new FinalGroup()
                        {
                            GroupName = dr["fromGroup"].ToString(),
                        }
                    },
                    ToStudent = new Student()
                    {
                        User = new User()
                        {
                            FptEmail = dr["toEmail"].ToString(),
                        },
                        FinalGroup = new FinalGroup()
                        {
                            GroupName = dr["toGroup"].ToString(),
                        }
                    },
                    StatusOfToStudent = Convert.ToInt32(dr["StatusOfTo"].ToString())
                });
            }
            return changeFinalGroupRequests;
        }
        public static List<ChangeFinalGroupRequest> GetListChangeFinalGroupRequest(string studentId, int semesterId)
        {
            string sql = @"select cfr.Change_FinalGroup_Request_ID,fromUser.FptEmail as fromEmail,fromGroup.GroupName as fromGroup
                            ,toUser.FptEmail as toEmail,toGroup.GroupName as toGroup ,StaffComment,StatusOfStaff
                            from Change_FinalGroup_Requests cfr
                            join Students fromStudent on fromStudent.Student_ID = cfr.FromStudent_ID
                            join Users fromUser on fromUser.[User_ID] = fromStudent.Student_ID
                            join FinalGroups fromGroup on fromGroup.FinalGroup_ID = fromStudent.FinalGroup_ID
                            join Students toStudent on toStudent.Student_ID = cfr.ToStudent_ID
                            join Users toUser on toUser.[User_ID] = toStudent.Student_ID
                            join FinalGroups toGroup on toGroup.FinalGroup_ID = toStudent.FinalGroup_ID
                            where (cfr.ToStudent_ID = @studentId  and cfr.StatusOfTo = 1 or cfr.FromStudent_ID = @studentId and cfr.StatusOfTo = 1) 
                            and fromGroup.Semester_ID = @semesterId and toGroup.Semester_ID = @semesterId
                            and cfr.Deleted_At is null
                            and (fromGroup.Deleted_At is null and toGroup.Deleted_At is null) order by cfr.Created_At desc";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            parameters[1] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[1].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<ChangeFinalGroupRequest> changeFinalGroupRequests = new List<ChangeFinalGroupRequest>();
            foreach (DataRow dr in result.Rows)
            {
                changeFinalGroupRequests.Add(new ChangeFinalGroupRequest()
                {
                    ChangeFinalGroupRequestId = Convert.ToInt32(dr["Change_FinalGroup_Request_ID"].ToString()),
                    FromStudent = new Student()
                    {
                        User = new User()
                        {
                            FptEmail = dr["fromEmail"].ToString(),
                        },
                        FinalGroup = new FinalGroup()
                        {
                            GroupName = dr["fromGroup"].ToString(),
                        }
                    },
                    ToStudent = new Student()
                    {
                        User = new User()
                        {
                            FptEmail = dr["toEmail"].ToString(),
                        },
                        FinalGroup = new FinalGroup()
                        {
                            GroupName = dr["toGroup"].ToString(),
                        }
                    },
                    StaffComment = dr["StaffComment"].ToString(),
                    StatusOfStaff = Convert.ToInt32(dr["StatusOfStaff"].ToString())
                });
            }
            return changeFinalGroupRequests;
        }

        public static string GetFromStudentIdByChangeFinalGroupRequestIdAndToStudentId(int changeFinalGroupRequestId, string toStudentId)
        {
            string sql = @"select FromStudent_ID from Change_FinalGroup_Requests 
                            where ToStudent_ID = @toStudentId and Change_FinalGroup_Request_ID = @changeFinalGroupRequestId";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@toStudentId", SqlDbType.VarChar);
            parameters[0].Value = toStudentId;
            parameters[1] = new SqlParameter("@changeFinalGroupRequestId", SqlDbType.Int);
            parameters[1].Value = changeFinalGroupRequestId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return dr["FromStudent_ID"].ToString();
        }

        public static int CountRecordChangeFinalGroupBySearchText(string searchText, int status, int semesterId)
        {
            string sql = @"select count(*) as [NumberOfRecordChangeFinalGroupRequest]
                                from Change_FinalGroup_Requests cfr
                                join Students fromStudent on fromStudent.Student_ID = cfr.FromStudent_ID
                                join Users fromUser on fromUser.[User_ID] = fromStudent.Student_ID
                                join FinalGroups fromGroup on fromGroup.FinalGroup_ID = fromStudent.FinalGroup_ID
                                join Students toStudent on toStudent.Student_ID = cfr.ToStudent_ID
                                join Users toUser on toUser.[User_ID] = toStudent.Student_ID
                                join FinalGroups toGroup on toGroup.FinalGroup_ID = toStudent.FinalGroup_ID
                                where cfr.StatusOfTo = 1
                                and cfr.StatusOfStaff = @status
                                and cfr.Deleted_At is null
                                and fromGroup.Semester_ID = @semesterId and toGroup.Semester_ID = @semesterId
                                and (fromGroup.Deleted_At is null and toGroup.Deleted_At is null) 
                                and (@searchText =  '' or REPLACE(UPPER(toGroup.GroupName), ' ', '') like @searchText
                                or REPLACE(UPPER(fromGroup.GroupName), ' ', '') like @searchText
                                or REPLACE(UPPER(fromUser.FptEmail), ' ', '') like @searchText
                                or REPLACE(UPPER(toUser.FptEmail), ' ', '') like @searchText)";
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@searchText", SqlDbType.NVarChar);
            parameters[0].Value = searchText;
            parameters[1] = new SqlParameter("@status", SqlDbType.Int);
            parameters[1].Value = status;
            parameters[2] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[2].Value = semesterId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            return Convert.ToInt32(dr["NumberOfRecordChangeFinalGroupRequest"]);
        }


        public static List<ChangeFinalGroupRequest> GetListChangeFinalGroupRequestBySearchText
                (string searchText, int status, int semesterId, int offsetNumber, int fetchNumber)
        {
            string sql = @"select cfr.Change_FinalGroup_Request_ID,fromUser.FptEmail as fromEmail,fromGroup.GroupName as fromGroup
                                ,toUser.FptEmail as toEmail,toGroup.GroupName as toGroup ,cfr.StaffComment,cfr.StatusOfStaff
                                from Change_FinalGroup_Requests cfr
                                join Students fromStudent on fromStudent.Student_ID = cfr.FromStudent_ID
                                join Users fromUser on fromUser.[User_ID] = fromStudent.Student_ID
                                join FinalGroups fromGroup on fromGroup.FinalGroup_ID = fromStudent.FinalGroup_ID
                                join Students toStudent on toStudent.Student_ID = cfr.ToStudent_ID
                                join Users toUser on toUser.[User_ID] = toStudent.Student_ID
                                join FinalGroups toGroup on toGroup.FinalGroup_ID = toStudent.FinalGroup_ID
                                where cfr.StatusOfTo = 1
                                and cfr.StatusOfStaff = @status
                                and cfr.Deleted_At is null
                                and fromGroup.Semester_ID = @semesterId and toGroup.Semester_ID = @semesterId
                                and (fromGroup.Deleted_At is null and toGroup.Deleted_At is null) 
                                and (@searchText =  '' or REPLACE(UPPER(toGroup.GroupName), ' ', '') like @searchText
                                or REPLACE(UPPER(fromGroup.GroupName), ' ', '') like @searchText
                                or REPLACE(UPPER(fromUser.FptEmail), ' ', '') like @searchText
                                or REPLACE(UPPER(toUser.FptEmail), ' ', '') like @searchText)
                                order by cfr.Created_At desc
                                OFFSET @offsetNumber rows fetch next @fetchNumber rows only";
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@searchText", SqlDbType.NVarChar);
            parameters[0].Value = searchText;
            parameters[1] = new SqlParameter("@status", SqlDbType.Int);
            parameters[1].Value = status;
            parameters[2] = new SqlParameter("@semesterId", SqlDbType.Int);
            parameters[2].Value = semesterId;
            parameters[3] = new SqlParameter("@offsetNumber", SqlDbType.Int);
            parameters[3].Value = offsetNumber;
            parameters[4] = new SqlParameter("fetchNumber", SqlDbType.Int);
            parameters[4].Value = fetchNumber;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<ChangeFinalGroupRequest> changeFinalGroupRequests = new List<ChangeFinalGroupRequest>();
            foreach (DataRow dr in result.Rows)
            {
                changeFinalGroupRequests.Add(new ChangeFinalGroupRequest()
                {
                    ChangeFinalGroupRequestId = Convert.ToInt32(dr["Change_FinalGroup_Request_ID"].ToString()),
                    FromStudent = new Student()
                    {
                        User = new User()
                        {
                            FptEmail = dr["fromEmail"].ToString(),
                        },
                        FinalGroup = new FinalGroup()
                        {
                            GroupName = dr["fromGroup"].ToString(),
                        }
                    },
                    ToStudent = new Student()
                    {
                        User = new User()
                        {
                            FptEmail = dr["toEmail"].ToString(),
                        },
                        FinalGroup = new FinalGroup()
                        {
                            GroupName = dr["toGroup"].ToString()
                        }
                    },
                    StaffComment = dr["StaffComment"].ToString(),
                    StatusOfStaff = Convert.ToInt32(dr["StatusOfStaff"].ToString()),
                }) ;
            }
            return changeFinalGroupRequests;
        }
        public static ChangeFinalGroupRequest GetInforOfStudentExchangeFinalGroup(int changeFinalGroupRequestId)
        {
            string sql = @"select Change_FinalGroup_Request_ID, FromStudent_ID as FromStudentID,fromStudent.FinalGroup_ID as FromFinalGroup,fromStudent.IsLeader as FromIsLeader
                            ,ToStudent_ID as ToStudentID,toStudent.FinalGroup_ID as ToFinalGroup, toStudent.IsLeader as ToIsLeader
                            from Change_FinalGroup_Requests cfr
                            join Students fromStudent on fromStudent.Student_ID =  cfr.FromStudent_ID
                            join Students toStudent on toStudent.Student_ID = cfr.ToStudent_ID
                            where Change_FinalGroup_Request_ID = @changeFinalGroupRequestId";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@changeFinalGroupRequestId", SqlDbType.Int);
            parameters[0].Value = changeFinalGroupRequestId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new ChangeFinalGroupRequest()
            {
                ChangeFinalGroupRequestId = Convert.ToInt32(dr["Change_FinalGroup_Request_ID"]),
                FromStudent = new Student()
                {
                    StudentID = dr["FromStudentID"].ToString(),
                    IsLeader = Convert.ToBoolean(dr["FromIsLeader"]),
                    FinalGroup = new FinalGroup()
                    {
                        FinalGroupID =  Convert.ToInt32(dr["FromFinalGroup"].ToString()),

                    },
                },
                ToStudent = new Student()
                {
                    StudentID = dr["ToStudentID"].ToString(),
                    IsLeader = Convert.ToBoolean(dr["ToIsLeader"]),
                    FinalGroup = new FinalGroup()
                    {
                        FinalGroupID = Convert.ToInt32(dr["ToFinalGroup"].ToString()),

                    },
                }
            };
        }

        public static int UpdateGroupForStudentByChangeFinalGroupRequest(ChangeFinalGroupRequest changeFinalGroupRequest)
        {
            string sql = @"update Change_FinalGroup_Requests set StatusOfStaff = 1
                            where Change_FinalGroup_Request_ID = @changeFinalGroupRequestId

                           update Students set FinalGroup_ID = @toFinalGroup ,IsLeader = @toIsLeader
                            where Student_ID = @fromStudentId

                           update Students set FinalGroup_ID = @fromFinalGroup ,IsLeader = @fromIsLeader
                            where Student_ID = @toStudentId";
            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@changeFinalGroupRequestId", SqlDbType.Int);
            parameters[0].Value = changeFinalGroupRequest.ChangeFinalGroupRequestId;

            parameters[1] = new SqlParameter("@fromFinalGroup", SqlDbType.Int);
            parameters[1].Value = changeFinalGroupRequest.FromStudent.FinalGroup.FinalGroupID;
            parameters[2] = new SqlParameter("@fromIsLeader", SqlDbType.Bit);
            parameters[2].Value = changeFinalGroupRequest.FromStudent.IsLeader;
            parameters[3] = new SqlParameter("@fromStudentId", SqlDbType.VarChar);
            parameters[3].Value = changeFinalGroupRequest.FromStudent.StudentID;

            parameters[4] = new SqlParameter("@toFinalGroup", SqlDbType.Int);
            parameters[4].Value = changeFinalGroupRequest.ToStudent.FinalGroup.FinalGroupID;
            parameters[5] = new SqlParameter("@toIsLeader", SqlDbType.Bit);
            parameters[5].Value = changeFinalGroupRequest.ToStudent.IsLeader;
            parameters[6] = new SqlParameter("@toStudentId", SqlDbType.VarChar);
            parameters[6].Value = changeFinalGroupRequest.ToStudent.StudentID;

            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int UpdateStatusOfStaffByChangeFinalGroupRequestId(int changeFinalGroupRequestId,string staffComment)
        {
            string sql = @"update Change_FinalGroup_Requests set StatusOfStaff = 2 , StaffComment = @staffComment
                            where Change_FinalGroup_Request_ID = @changeFinalGroupRequestId";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@changeFinalGroupRequestId", SqlDbType.Int);
            parameters[0].Value = changeFinalGroupRequestId; 
            parameters[1] = new SqlParameter("@staffComment", SqlDbType.NVarChar);
            parameters[1].Value = staffComment;
            return DbContext.ExecuteSQL(sql, parameters);
        }
    }
}
