using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.Model;
using System.Data.SqlClient;

namespace HotelManager.DAL
{
    public static class RoomStateService
    {
        /// <summary>
        /// 获取房间状态
        /// </summary>
        /// <returns></returns>
        public static List<RoomState> GetRoomState()
        {
            string sql = "select * from RoomState";
            SqlDataReader reader = SqlHelper.DataReader(sql);
            List<RoomState> roomState = new List<RoomState>();
            while (reader.Read())
            {
                RoomState state = new RoomState();
                state.RoomStateId = Convert.ToInt32(reader["roomStateId"]);
                state.RoomStateName = reader["roomStateName"].ToString();
                roomState.Add(state);
            }
            reader.Close();
            return roomState;
        }
    }
}
