using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.Model;
using System.Data.SqlClient;
using System.Data;

namespace HotelManager.DAL
{
    public static class RoomService
    {
        /// <summary>
        /// 获取房间信息
        /// </summary>
        /// <returns></returns>
        public static List<RoomBusiness> GetRoomInfo(int roomId)
        {
            StringBuilder sb = new StringBuilder("select RoomId,BedNum,b.RoomStateName,r.TypeName,Description from Room as a ,RoomState as b,RoomType as r where a.RoomStateId=b.RoomStateId and a.RoomTypeId=r.TypeId");
            if (roomId != 0)
            {
                sb.AppendLine(" and a.RoomId=@roomId");
            }
            SqlParameter[] para = { new SqlParameter("@roomId", roomId) };
            SqlDataReader reader = SqlHelper.DataReader(sb.ToString(), CommandType.Text, para);
            List<RoomBusiness> room = new List<RoomBusiness>();
            while (reader.Read())
            {
                RoomBusiness r = new RoomBusiness();
                r.RoomId = Convert.ToInt32(reader["RoomId"]);
                r.BedNum = Convert.ToInt32(reader["BedNum"]);
                r.RoomStateName = reader["RoomStateName"].ToString();
                r.TypeName = reader["TypeName"].ToString();
                r.Description = reader["Description"].ToString();
                room.Add(r);
            }
            reader.Close();
            return room;
        }

        /// <summary>
        /// 删除房间信息
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public static int DelRoomInfo(int roomId)
        {
            string sql = "delete Room where RoomId=@roomId";
            SqlParameter[] para = { new SqlParameter("@roomId", roomId) };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }

        /// <summary>
        /// 添加房间信息
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public static int AddRoomInfo(RoomBusiness room)
        {
            string sql = "insert into Room(RoomTypeId,RoomStateId,Description,BedNum) values(@roomTypeId,@roomStateId,@description,@bedNum)";
            SqlParameter[] para = {new SqlParameter("@roomTypeId",room.RoomTypeId),
                                      new SqlParameter("@roomStateId",room.RoomStateId),
                                      (new SqlParameter("@description",SqlDbType.NVarChar,200){Value=room.Description}),
                                      new SqlParameter("@bedNum",room.BedNum)
                                  };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }

        /// <summary>
        /// 更新房间信息
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public static int UpdateRoomInfo(RoomBusiness room)
        {
            string sql = "update Room set Description=@description,BedNum=@bedNum,RoomTypeId=@roomTypeId,RoomStateId=@roomStateId where RoomId=@roomId";
            SqlParameter[] para = {(new SqlParameter("@description",SqlDbType.NVarChar,200){Value=room.Description}),
                                      new SqlParameter("@BedNum",room.BedNum),
                                      new SqlParameter("@roomTypeId",room.RoomTypeId),
                                      new SqlParameter("@roomStateId",room.RoomStateId),
                                      new SqlParameter("@roomId",room.RoomId)
                                  
                                  };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }

        /// <summary>
        /// 查询未入住房间
        /// </summary>
        /// <returns></returns>
        public static List<Room> GetFreeRoom()
        {
            string sql = "select * from Room as r,RoomState as s where r.RoomStateId=s.RoomStateId and s.RoomStateName='空闲'";
            SqlDataReader reader = SqlHelper.DataReader(sql);
            List<Room> room = new List<Room>();
            while (reader.Read())
            {
                Room r = new Room();
                r.RoomId = Convert.ToInt32(reader["RoomId"]);
                r.BedNum = Convert.ToInt32(reader["BedNum"]);
                r.Description = reader["Description"].ToString();
                room.Add(r);
            }
            reader.Close();
            return room;
        }


        /// <summary>
        /// 获取所有房间类型信息
        /// </summary>
        /// <returns></returns>
        public static List<RoomType> GetAllRoomType()
        {
            List<RoomType> roomType = new List<RoomType>();
            string sql = "select * from RoomType";
            SqlDataReader reader = SqlHelper.DataReader(sql);
            while (reader.Read())
            {
                RoomType r = new RoomType();
                r.TypeId = Convert.ToInt32(reader["TypeId"]);
                r.TypeName = reader["TypeName"].ToString();
                roomType.Add(r);
            }
            reader.Close();
            return roomType;
        }

        /// <summary>
        /// 根据房间类型查询房间状态
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static List<Room> GetRoomStateByType(int typeId)
        {
            List<Room> room = new List<Room>();
            string sql = "select * from Room where RoomTypeId=@roomTypeId";
            SqlParameter[] para = { new SqlParameter("@roomTypeId", typeId) };
            SqlDataReader reader = SqlHelper.DataReader(sql, CommandType.Text, para);
            while (reader.Read())
            {
                Room r = new Room();
                r.RoomId = Convert.ToInt32(reader["RoomId"]);
                r.RoomStateId = Convert.ToInt32(reader["RoomStateId"]);
                room.Add(r);
            }
            reader.Close();
            return room;
        }

        /// <summary>
        /// 查询所有房间状态
        /// </summary>
        /// <returns></returns>
        public static List<Room> GetRoomState()
        {
            List<Room> room = new List<Room>();
            string sql = "select * from Room";
            SqlDataReader reader = SqlHelper.DataReader(sql);
            while (reader.Read())
            {
                Room r = new Room();
                r.RoomId = Convert.ToInt32(reader["RoomId"]);
                r.RoomStateId = Convert.ToInt32(reader["RoomStateId"]);
                room.Add(r);
            }
            reader.Close();
            return room;
        }

        /// <summary>
        /// 查询总房间数
        /// </summary>
        /// <returns></returns>
        public static int GetRoomCount(int typeId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select count(1) from Room");
            if (typeId != 0)
            {
                sb.AppendLine(" where RoomTypeId=@roomTypeId");
            }
            SqlParameter[] para = { new SqlParameter("@roomTypeId", typeId) };
            return (int)SqlHelper.ExecuteScalar(sb.ToString(), CommandType.Text, para);
        }

        /// <summary>
        /// 根据状态ID和房间类型查询房间数
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        public static int GetRoomStateCount(int stateId, int typeId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select count(1) from Room where RoomStateId=@roomStateId");
            if (typeId != 0)
            {
                sb.AppendLine(" and RoomTypeId=@roomTypeId");
            }
            SqlParameter[] para = { new SqlParameter("roomStateId", stateId) ,
                                    new SqlParameter("roomTypeId",typeId)};
            return (int)SqlHelper.ExecuteScalar(sb.ToString(), CommandType.Text, para);
        }
    }
}
