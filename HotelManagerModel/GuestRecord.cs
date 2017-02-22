using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Model
{
    [Serializable]
    /// <summary>
    /// 顾客
    /// </summary>
    public class GuestRecord
    {
        public GuestRecord()
        {
            this.ResideId = 1;
        }
        //顾客编号
        public int GuestId { get; set; }
        //顾客姓名
        public string GuestName { get; set; }
        //身份证号
        public string IdentityId { get; set; }
        //联系电话
        public string Phone { get; set; }
        //性别
        public string Gender { get; set; }
        //房间编号
        public int RoomId { get; set; }
        //入住状态ID
        public int ResideId { get; set; }
        //入住日期
        public DateTime ResideDate { get; set; }
        //押金
        public decimal Deposit { get; set; }
        //房款
        public decimal TotalMoney { get; set; }
        //退房日期
        public DateTime LeaveDate { get; set; }
        //交易号
        public string TradeNo { get; set; }
        //餐饮费用
        public decimal DishPrice { get; set; }
    }
}
