using CapstoneProjectManagementSystem.Models.Dao.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models.Dao
{
    public class Student_FavoriteGroupIdeaDao
    {
        public static List<StudentFavoriteGroupIdea> GetFavoriteIdeaListByStudentId(string studentId)
        {
            string sql = "select * from Student_FavoriteGroupIdea " +
                "where Student_ID = @studentID and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@studentID", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<StudentFavoriteGroupIdea> favoriteGroupIdeaList = new List<StudentFavoriteGroupIdea>();
            foreach (DataRow dr in result.Rows)
            {
                favoriteGroupIdeaList.Add(new StudentFavoriteGroupIdea()
                {
                    StudentID = dr["Student_ID"].ToString(),
                    GroupIdeaID = Convert.ToInt32(dr["GroupIdea_ID"]),
                    GroupIdea = new GroupIdea()
                    {
                        GroupIdeaID = Convert.ToInt32(dr["GroupIdea_ID"])
                    },
                });
            }
            return favoriteGroupIdeaList;
        }
        public static StudentFavoriteGroupIdea GetRecord(string studentId,int groupId)
        {
            string sql = "select * from Student_FavoriteGroupIdea " +
                "where Student_ID = @studentID and GroupIdea_ID = @groupIdeaId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@studentID", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            parameters[1] = new SqlParameter("@groupIdeaId", SqlDbType.Int);
            parameters[1].Value = groupId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new StudentFavoriteGroupIdea()
            {
                StudentID = dr["Student_ID"].ToString(),
                GroupIdeaID = Convert.ToInt32(dr["GroupIdea_ID"]),
                GroupIdea = new GroupIdea()
                {
                    GroupIdeaID = Convert.ToInt32(dr["GroupIdea_ID"])
                },
            };
        }
        public static int AddRecord(string studentId, int groupId)
        {
            string sql = @"insert into Student_FavoriteGroupIdea(Student_ID,GroupIdea_ID)
                            values (@studentId,@groupId)";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@studentId", SqlDbType.VarChar);
            parameters[0].Value = studentId;
            parameters[1] = new SqlParameter("@groupId", SqlDbType.Int);
            parameters[1].Value = groupId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static int DeleteRecord(string studentId, int groupIdeaId)
        {
            string sql = "update Student_FavoriteGroupIdea " +
                "set Deleted_At = @deleteDate " +
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
        public static int DeleteAllRecordOfAGroupIdea(int groupIdeaId)
        {
            string sql = "update Student_FavoriteGroupIdea " +
                "set Deleted_At = @deleteDate " +
                "where GroupIdea_ID = @groupIdeaId and [Deleted_At] is null";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("deleteDate", SqlDbType.DateTime);
            parameters[0].Value = DateTime.Now;
            parameters[1] = new SqlParameter("groupIdeaId", SqlDbType.Int);
            parameters[1].Value = groupIdeaId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
    }
}
