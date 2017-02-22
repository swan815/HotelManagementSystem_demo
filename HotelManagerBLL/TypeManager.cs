using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.DAL;
using HotelManager.Model;

namespace HotelManager.BLL
{
    public static class TypeManager
    {
        /// <summary>
        /// 获取所有房间类型信息
        /// </summary>
        /// <returns></returns>
        public static List<RoomType> GetAllRoomTypeInfo(string typeName)
        {
            return TypeServicer.GetAllRoomTypeInfo(typeName);
        }

        /// <summary>
        /// 添加房间类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool AddRoomType(RoomType type)
        {
            bool flag = false;
            int result = TypeServicer.AddRoomType(type);
            return flag = result == 1 ? true : false;
        }

        /// <summary>
        /// 修改房间类型信息
        /// </summary>
        /// <param name="typeId">房间类型ID</param>
        /// <param name="typeName">房间类型名称</param>
        /// <param name="typePrice">房间类型价格</param>
        /// <returns></returns>
        public static bool UpdateRoomType(RoomType roomType)
        {
            bool flag = false;
            return flag = TypeServicer.UpdateRoomType(roomType) == 1 ? true : false;
        }

        /// <summary>
        /// 删除房间信息
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static bool DeleteRoomType(int typeId)
        {
            bool flag = false;
            return flag = TypeServicer.DeleteRoomType(typeId) == 1 ? true : false;
        }
    }
}
