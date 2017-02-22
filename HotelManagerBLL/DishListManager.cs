using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.Model;
using HotelManager.DAL;

namespace HotelManager.BLL
{
    public static class DishListManager
    {
        /// <summary>
        /// 查询所有餐饮
        /// </summary>
        /// <returns></returns>
        public static List<DishList> GetList()
        {
            return DishListService.GetList();
        }

        /// <summary>
        /// 更新餐饮支出
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static bool InsertPrice(int roomId, decimal price)
        {
            return DishListService.InsertPrice(roomId, price) == 1 ? true : false;
        }

        /// <summary>
        /// 更新餐饮信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool UpdateInfo(DishList list)
        {
            return DishListService.UpdateInfo(list) == 1 ? true : false;
        }

        /// <summary>
        /// 新增餐饮信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool InsertInfo(DishList list)
        {
            return DishListService.InsertInfo(list) == 1 ? true : false;
        }

         /// <summary>
        /// 删除餐饮信息
        /// </summary>
        /// <param name="dishId"></param>
        /// <returns></returns>
        public static bool DelDishInfo(int dishId)
        {
            return DishListService.DelDishInfo(dishId) == 1 ? true : false;
        }

           /// <summary>
        /// 根据名称查询信息
        /// </summary>
        /// <param name="dishName"></param>
        /// <returns></returns>
        public static List<DishList> FindList(string dishName)
        {
            return DishListService.FindList(dishName);
        }
    }
}
