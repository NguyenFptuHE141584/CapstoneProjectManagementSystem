using CapstoneProjectManagementSystem.Models.Dao.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models.Dao
{
    public class NotificationDao
    {
        public static List<Notification> GetListNotificationNotReadByReceiverID(int numberOfRecord, string userId)
        {
            string sql = @"select top (@numberOfRecord) n.Notification_ID,n.Notification_Content,n.Attached_Link,n.Readed,n.Created_At
                            from Notifications n 
                            join Users [user] on [user].[User_ID] = n.[User_ID]
                            where  n.[User_ID] = @userId and n.Readed = 0 and n.Deleted_At is null order by Created_At desc";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
            parameters[0].Value = userId;
            parameters[1] = new SqlParameter("@numberOfRecord", SqlDbType.Int);
            parameters[1].Value = numberOfRecord;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<Notification> notifications = new List<Notification>();
            foreach (DataRow dr in result.Rows)
            {
                notifications.Add(new Notification()
                {
                    NotificationID = Convert.ToInt32(dr["Notification_ID"].ToString()),
                    Readed = Convert.ToBoolean(dr["Readed"].ToString()),
                    NotificationContent = dr["Notification_Content"].ToString(),
                    AttachedLink = dr["Attached_Link"].ToString(),
                    CreatedAt = Convert.ToDateTime(dr["Created_At"])
                });
            }
            return notifications;
        }
        public static List<Notification> GetListAllNotificationByUserId(int numberOfRecord, string userId)
        {
            string sql = @"select top (@numberOfRecord) n.Notification_ID,n.Notification_Content,n.Attached_Link,n.Readed,n.Created_At
                            from Notifications n 
                            join Users [user] on [user].[User_ID] = n.[User_ID]
                            where  n.[User_ID] = @userId and n.Deleted_At is null order by Created_At desc";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
            parameters[0].Value = userId;
            parameters[1] = new SqlParameter("@numberOfRecord", SqlDbType.Int);
            parameters[1].Value = numberOfRecord;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            List<Notification> notifications = new List<Notification>();
            foreach (DataRow dr in result.Rows)
            {
                notifications.Add(new Notification()
                {
                    NotificationID = Convert.ToInt32(dr["Notification_ID"].ToString()),
                    Readed = Convert.ToBoolean(dr["Readed"].ToString()),
                    NotificationContent = dr["Notification_Content"].ToString(),
                    AttachedLink = dr["Attached_Link"].ToString(),
                    CreatedAt = Convert.ToDateTime(dr["Created_At"])
                });
            }
            return notifications;
        }

        public static int CountNotificationNotRead(string userId)
        {
            string sql = @"select count(*) as CountNotReaded from Notifications
                            where [User_ID] = @userId and Deleted_At is null and Readed = 0";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
            parameters[0].Value = userId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            return Convert.ToInt32(dr["CountNotReaded"].ToString());
        }

        public static int CountAllNotification(string userId)
        {
            string sql = @"select count(*) as CountAllNotification from Notifications
                            where [User_ID] = @userId and Deleted_At is null";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
            parameters[0].Value = userId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return 0;
            DataRow dr = result.Rows[0];
            return Convert.ToInt32(dr["CountAllNotification"].ToString());
        }

        public static int InsertDataNotification(string userId,string notificationContent,string attachedLink)
        {
            string sql = @"insert into Notifications(User_ID,Notification_Content,Attached_Link)
                            values(@userId,@notificationContent,@attachedLink)";
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@userId", SqlDbType.VarChar);
            parameters[0].Value = userId;
            parameters[1] = new SqlParameter("@notificationContent", SqlDbType.NVarChar);
            parameters[1].Value = notificationContent;
            parameters[2] = new SqlParameter("@attachedLink", SqlDbType.VarChar);
            parameters[2].Value = attachedLink;
            return DbContext.ExecuteSQL(sql, parameters);
        }
        public static string GetAttachedLinkByNotificationId(int notificationId)
        {
            string sql = @"select Attached_Link from Notifications where Notification_ID = @notificationId";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@notificationId", SqlDbType.VarChar);
            parameters[0].Value = notificationId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return dr["Attached_Link"].ToString();
        }

        public static int UpdateReadedNotificationByNotificationId(int notificationId)
        {
            string sql = @"update Notifications set Readed = 1 where Notification_ID = @notificationId";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@notificationId", SqlDbType.VarChar);
            parameters[0].Value = notificationId;
            return DbContext.ExecuteSQL(sql, parameters);
        }
    }
}
