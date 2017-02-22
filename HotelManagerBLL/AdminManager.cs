using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.Model;
using HotelManager.DAL;
using HotelManager.Common;

namespace HotelManager.BLL
{
    public static class AdminManager
    {
        /// <summary>
        /// 检查用户名密码
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static bool CheckIdPwd(Admin admin)
        {
            admin.PassWord = Encryption.Md5(admin.PassWord);
            return AdminService.CheckIdPwd(admin) == 1 ? true : false;
        }

         /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static bool UpdatePwd(Admin admin)
        {
            //加密
            admin.PassWord = Encryption.Md5(admin.PassWord);
            return AdminService.UpdatePwd(admin) == 1 ? true : false;
        }
    }
}
