using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.Model;
using System.Data;


namespace HotelManager.DAL
{
    public static class TypeServicer
    {
        /// <summary>
        /// 获取所有房间类型信息
        /// </summary>
        /// <returns></returns>
        public static List<RoomType> GetAllRoomTypeInfo(string typeName)
        {
            List<RoomType> roomType = new List<RoomType>();
            string sql = "";
            if (string.IsNullOrEmpty(typeName))
            {
                sql = "select * from RoomType";
            }
            else
            {
                sql = "select * from RoomType where TypeName like '%'+@typeName+'%'";
            }
            SqlParameter[] para = { new SqlParameter("@typeName", typeName) };
            SqlDataReader reader = SqlHelper.DataReader(sql, CommandType.Text, para);
            while (reader.Read())
            {
                RoomType type = new RoomType();
                type.TypeId = Convert.ToInt32(reader["TypeId"]);
                type.TypeName = reader["TypeName"].ToString();
                type.TypePrice = Convert.ToDecimal(reader["TypePrice"]);
                roomType.Add(type);
            }
            reader.Close();
            return roomType;
        }

        /// <summary>
        /// 添加房间类型
        /// </summary>
        /// <param name="type">房间类型对象</param>
        /// <returns>受影响行数</returns>
        public static int AddRoomType(RoomType type)
        {
            string sql = "insert into RoomType values(@typeName,@typePrice)";
            SqlParameter[] para = {(new SqlParameter("@typeName",SqlDbType.NVarChar,50){Value=type.TypeName}),
                                      new SqlParameter("@typePrice",type.TypePrice)
                                  };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }

        /// <summary>
        /// 修改房间类型信息
        /// </summary>
        /// <param name="typeId">房间类型ID</param>
        /// <param name="typeName">房间类型名称</param>
        /// <param name="typePrice">房间类型价格</param>
        /// <returns></returns>
        public static int UpdateRoomType(RoomType roomType)
        {
            string sql = "update RoomType set TypeName=@typeName,TypePrice=@typePrice where TypeId=@typeId";
            SqlParameter[] para = {(new SqlParameter("@typeName",SqlDbType.NVarChar,50){Value=roomType.TypeName}),
                                    new SqlParameter("@typePrice",roomType.TypePrice),
                                    new SqlParameter("@typeId",roomType.TypeId)
                                  };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }

        /// <summary>
        /// 删除房间信息
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static int DeleteRoomType(int typeId)
        {
            string sql = "delete RoomType where TypeId=@typeId";
            SqlParameter[] para = { new SqlParameter("@typeId", typeId) };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }
    }
}
