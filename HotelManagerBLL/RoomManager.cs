using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.Model;
using HotelManager.DAL;

namespace HotelManager.BLL
{
    public static class RoomManager
    {
        /// <summary>
        /// 获取房间信息
        /// </summary>
        /// <returns></returns>
        public static List<RoomBusiness> GetRoomInfo(int roomId)
        {
            return RoomService.GetRoomInfo(roomId);
        }

        /// <summary>
        /// 删除房间信息
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public static bool DelRoomInfo(int roomId)
        {
            return RoomService.DelRoomInfo(roomId) == 1 ? true : false;
        }

        /// <summary>
        /// 添加房间信息
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public static bool AddRoomInfo(RoomBusiness room)
        {
            return RoomService.AddRoomInfo(room) == 1 ? true : false;
        }


        /// <summary>
        /// 更新房间信息
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public static bool UpdateRoomInfo(RoomBusiness room)
        {
            return RoomService.UpdateRoomInfo(room) == 1 ? true : false;
        }

        /// <summary>
        /// 查询未入住房间
        /// </summary>
        /// <returns></returns>
        public static List<Room> FreeRoom()
        {
            return RoomService.GetFreeRoom();
        }


        /// <summary>
        /// 获取所有房间类型信息
        /// </summary>
        /// <returns></returns>
        public static List<RoomType> GetAllRoomType()
        {
            return RoomService.GetAllRoomType();
        }

        /// <summary>
        /// 根据房间类型查询房间状态
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static List<Room> GetRoomStateByType(int typeId)
        {
            return RoomService.GetRoomStateByType(typeId);
        }

        /// <summary>
        /// 查询所有房间状态
        /// </summary>
        /// <returns></returns>
        public static List<Room> GetRoomState()
        {
            return RoomService.GetRoomState();
        }

        /// <summary>
        /// 查询房间数
        /// </summary>
        /// <returns></returns>
        public static int GetRoomCount(int typeId)
        {
            return RoomService.GetRoomCount(typeId);
        }

        /// <summary>
        /// 根据状态ID查询房间数
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        public static int GetRoomStateCount(int stateId,int typeId)
        {
            return RoomService.GetRoomStateCount(stateId,typeId);
        }
    }
}
