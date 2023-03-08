using CapstoneProjectManagementSystem.Models.Dao.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models.Dao.StaffDao
{
    public class StaffDao
    {
        public static Staff GetUserIsStaffByRoleId(int roleId)
        {
            string sql = @"select User_ID,FptEmail from Users where Role_ID = 3";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@roleId", SqlDbType.Int);
            parameters[0].Value = roleId;
            DataTable result = DbContext.GetDataBySQL(sql, parameters);
            if (result.Rows.Count == 0) return null;
            DataRow dr = result.Rows[0];
            return new Staff()
            {
                StaffID = (dr["User_ID"].ToString() == null) ? "" : dr["User_ID"].ToString(),
                User = new User()
                {
                    FptEmail = (dr["FptEmail"].ToString() == null) ? "" : dr["FptEmail"].ToString()
                },
            };
        }
    }
}
