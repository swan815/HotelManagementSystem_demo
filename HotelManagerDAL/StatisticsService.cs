using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace HotelManager.DAL
{
    public static class StatisticsService
    {
        /// <summary>
        /// 查询每月房款收入
        /// </summary>
        /// <returns></returns>
        public static double[] GetMoney(int year)
        {
            double[] money = new double[12];
            for (int i = 0; i < 12; i++)
            {
                SqlParameter[] para = { new SqlParameter("@year", year),
                                        new SqlParameter("@month",i+1)
                                      };
                object obj = SqlHelper.ExecuteScalar("usp_money", CommandType.StoredProcedure, para);
                if (obj == DBNull.Value)
                {
                    money[i] = 0;
                }
                else
                {
                    money[i] = Convert.ToDouble(obj);
                }
            }
            return money;
        }

        /// <summary>
        /// 查询每月餐饮收入
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static double[] GetDishPrice(int year)
        {
            double[] price = new double[12];
            for (int i = 0; i < 12; i++)
            {
                SqlParameter[] para = { new SqlParameter("@year", year),
                                        new SqlParameter("@month",i+1)
                                      };
                object obj = SqlHelper.ExecuteScalar("usp_dishPrice", CommandType.StoredProcedure, para);
                if (obj == DBNull.Value)
                {
                    price[i] = 0;
                }
                else
                {
                    price[i] = Convert.ToDouble(obj);
                }
            }
            return price;
        }

        /// <summary>
        /// 查询年度房款总收入
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static double SumTotalMoney(int year)
        {
            string sql = "select sum(TotalMoney) from GuestRecord where YEAR(LeaveDate)=@year";
            SqlParameter[] para = { new SqlParameter("@year", year) };
            object obj = SqlHelper.ExecuteScalar(sql, CommandType.Text, para);
            if (obj == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }

        /// <summary>
        /// 查询年度餐饮总收入
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static double SumDishMoney(int year)
        {
            string sql = "select sum(DishPrice) from GuestRecord where YEAR(LeaveDate)=@year";
            SqlParameter[] para = { new SqlParameter("@year", year) };
            object obj = SqlHelper.ExecuteScalar(sql, CommandType.Text, para);
            if (obj == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }
    }
}
