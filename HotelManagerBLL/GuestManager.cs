using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.Model;
using HotelManager.DAL;

namespace HotelManager.BLL
{
    public static class GuestManager
    {
        /// <summary>
        /// 入住登记
        /// </summary>
        /// <param name="guest"></param>
        /// <returns></returns>
        public static bool AddGuest(GuestRecord guest)
        {
            return GuestService.AddGuest(guest);
        }

        /// <summary>
        /// 查询时间段内未付款顾客信息
        /// </summary>
        /// <returns></returns>
        public static List<GuestRecordBusiness> GetAllGuest(DateTime Sdate, DateTime Edate)
        {
            return GuestService.GetAllGuest(Sdate, Edate);
        }

        /// <summary>
        /// 查询所有未付款顾客信息
        /// </summary>
        /// <returns></returns>
        public static List<GuestRecordBusiness> GetAllGuest()
        {
            return GuestService.GetAllGuest();
        }

        /// <summary>
        /// 查询所有已付款客户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<GuestRecord> GetCusInfo(string name)
        {
            return GuestService.GetCusInfo(name);
        }

        /// <summary>
        /// 应付房款
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public static decimal PayMoney(int roomId, DateTime dt)
        {
            decimal money = GuestService.GetMoney(roomId);
            TimeSpan ts = DateTime.Now - dt;
            if (ts.Days == 0)
            {
                return money;
            }
            return money = ts.Days * money;
        }

        /// <summary>
        /// 更新结账和房间状态
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public static bool UpdateState(GuestRecord gr)
        {
            return GuestService.UpdateState(gr);
        }
    }
}
