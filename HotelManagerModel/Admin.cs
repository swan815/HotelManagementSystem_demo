using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Model
{
    [Serializable]
    /// <summary>
    /// 登陆用户名密码
    /// </summary>
    public class Admin
    {
        public Admin() { }
        public Admin(string loginId,string passWord)
        {
            this.LoginId = loginId;
            this.PassWord = passWord;
        }
        public string LoginId { get; set; }//用户名
        public string PassWord { get; set; }//密码
    }
}
