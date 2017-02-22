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
    public static class DishListService
    {
        /// <summary>
        /// 查询所有餐饮
        /// </summary>
        /// <returns></returns>
        public static List<DishList> GetList()
        {
            string sql = "select * from DishList";
            SqlDataReader reader = SqlHelper.DataReader(sql);
            List<DishList> dishList = new List<DishList>();
            while (reader.Read())
            {
                DishList dl = new DishList();
                dl.DishId = Convert.ToInt32(reader["DishId"]);
                dl.DishName = reader["DishName"].ToString();
                dl.Price = Convert.ToDecimal(reader["Price"]);
                dl.Unit = reader["Unit"].ToString();
                dishList.Add(dl);
            }
            reader.Close();
            return dishList;
        }


        /// <summary>
        /// 更新餐饮支出
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static int InsertPrice(int roomId, decimal price)
        {
            string sql = "update GuestRecord set DishPrice= dishPrice+@dishPrice where RoomId=@roomId and ResideID=1";
            SqlParameter[] para = { new SqlParameter("@dishPrice", price),
                                  new SqlParameter("@RoomId",roomId)};
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }

        /// <summary>
        /// 更新餐饮信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int UpdateInfo(DishList list)
        {
            string sql = "update DishList set DishName=@dishName,Unit=@unit,Price=@price where DishId=@dishId";
            SqlParameter[] para = {new SqlParameter("@dishName",list.DishName),
                                      new SqlParameter("@unit",list.Unit),
                                      new SqlParameter("@price",list.Price),
                                      new SqlParameter("@dishId",list.DishId)
                                  };
            return SqlHelper.ExecuteNonQuery(sql,CommandType.Text,para);
        }

        /// <summary>
        /// 新增餐饮信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int InsertInfo(DishList list)
        {
            string sql = "insert into DishList values(@dishName,@unit,@price)";
            SqlParameter[] para = {new SqlParameter("@dishName",list.DishName),
                                      new SqlParameter("@unit",list.Unit),
                                        new SqlParameter("@price",list.Price)
                                  };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }

        /// <summary>
        /// 删除餐饮信息
        /// </summary>
        /// <param name="dishId"></param>
        /// <returns></returns>
        public static int DelDishInfo(int dishId)
        {
            string sql = "delete DishList where DishId=@dishId";
            SqlParameter[] para = { new SqlParameter("@dishId", dishId) };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }

        /// <summary>
        /// 根据名称查询信息
        /// </summary>
        /// <param name="dishName"></param>
        /// <returns></returns>
        public static List<DishList> FindList(string dishName)
        {
            string sql = "select * from DishList where DishName like '%'+@dishName+'%'";
            SqlParameter[] para = { new SqlParameter("@dishName", dishName) };
            SqlDataReader reader = SqlHelper.DataReader(sql, CommandType.Text, para);
            List<DishList> dishList = new List<DishList>();
            while (reader.Read())
            {
                DishList list = new DishList();
                list.DishId = Convert.ToInt32(reader["DishId"]);
                list.DishName = reader["DishName"].ToString();
                list.Price = Convert.ToDecimal(reader["Price"]);
                list.Unit = reader["Unit"].ToString();
                dishList.Add(list);
            }
            reader.Close();
            return dishList;
        }
    }
}
