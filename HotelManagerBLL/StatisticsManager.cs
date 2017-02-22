using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.DAL;

namespace HotelManager.BLL
{
    public static class StatisticsManager
    {
        /// <summary>
        /// 查询每月房款收入
        /// </summary>
        /// <returns></returns>
        public static double[] GetMoney(int year)
        {
            return StatisticsService.GetMoney(year);
        }


        /// <summary>
        /// 查询每月餐饮收入
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static double[] GetDishPrice(int year)
        {
            return StatisticsService.GetDishPrice(year);
        }


         /// <summary>
        /// 查询年度房款总收入
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static double SumTotalMoney(int year)
        {
            return StatisticsService.SumTotalMoney(year);
        }

        /// <summary>
        /// 查询年度餐饮总收入
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static double SumDishMoney(int year)
        {
            return StatisticsService.SumDishMoney(year);
        }
    }
}
