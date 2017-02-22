using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.Model;
using System.Data;
using System.Data.SqlClient;

namespace HotelManager.DAL
{
    public static class AdminService
    {
        /// <summary>
        /// 检查用户名密码
        /// </summary>
        /// <param name="admin">用户对象</param>
        /// <returns></returns>
        public static int CheckIdPwd(Admin admin)
        {
            string sql = "select count(1) from Admin where LoginId=@loginId and PassWord=@passWord";
            SqlParameter[] para = {(new SqlParameter("loginId",SqlDbType.NVarChar,50){Value=admin.LoginId}),
                                    new SqlParameter("passWord",SqlDbType.NVarChar,50){Value=admin.PassWord}
                                  };
            return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text, para);
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static int UpdatePwd(Admin admin)
        {
            string sql = "update Admin set PassWord=@passWord where LoginId=@loginId";
            SqlParameter[] para = { (new SqlParameter("@passWord",SqlDbType.NVarChar,50){Value=admin.PassWord}),
                                    (new SqlParameter("@loginId",SqlDbType.NVarChar,50){Value=admin.LoginId})
                                  };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }
    }
}
