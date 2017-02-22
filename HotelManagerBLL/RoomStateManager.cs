using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.Model;
using HotelManager.DAL;

namespace HotelManager.BLL
{
    public static class RoomStateManager
    {
        /// <summary>
        /// 获取房间状态
        /// </summary>
        /// <returns></returns>
        public static List<RoomState> GetRoomState()
        {
            return RoomStateService.GetRoomState();
        }
    }
}
