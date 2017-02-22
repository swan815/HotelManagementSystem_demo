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
    public static class GuestService
    {
        /// <summary>
        /// 入住登记
        /// </summary>
        /// <param name="guest"></param>
        /// <returns></returns>
        public static bool AddGuest(GuestRecord guest)
        {
            string sql = "insert into GuestRecord(GuestName,Gender,IdentityId,Phone,RoomId,ResideId,ResideDate,Deposit) values(@GuestName,@Gender,@IdentityId,@Phone,@RoomId,@ResideId,@ResideDate,@Deposit);update Room set RoomStateId=@RoomStateId where RoomId=@RoomId";
            SqlParameter[] para = {(new SqlParameter("@GuestName",SqlDbType.NChar,20){Value=guest.GuestName}),
                                      new SqlParameter("Gender",guest.Gender == "男"?0:1),
                                      new SqlParameter("@IdentityId",guest.IdentityId),
                                      new SqlParameter("Phone",guest.Phone),
                                      new SqlParameter("@RoomId",guest.RoomId),
                                      new SqlParameter("@ResideId",guest.ResideId),
                                      new SqlParameter("@ResideDate",guest.ResideDate),
                                      new SqlParameter("@Deposit",guest.Deposit),
                                      new SqlParameter("@RoomStateId",1)
                                      };
            return SqlHelper.GetExecuteNonQuery(sql, CommandType.Text, para);
        }

        /// <summary>
        /// 查询时间段内未付款顾客信息
        /// </summary>
        /// <returns></returns>
        public static List<GuestRecordBusiness> GetAllGuest(DateTime Sdate, DateTime Edate)
        {
            List<GuestRecordBusiness> guest = new List<GuestRecordBusiness>();
            StringBuilder sb = new StringBuilder();
            string sql = "select g.RoomId,GuestName,IdentityId,ResideDate,Deposit,ResideName,g.ResideId,DishPrice,LeaveDate,roomId from GuestRecord as g inner join ResideState as r on g.ResideId=r.ResideId where g.ResideId=@resideId and ResideDate between @sdate and @edate";
            SqlParameter[] para = { new SqlParameter("@resideId", 1),
                                    new SqlParameter("sdate",Sdate),
                                    new SqlParameter("edate",Edate)
                                  };
            SqlDataReader reader = SqlHelper.DataReader(sql, CommandType.Text, para);
            while (reader.Read())
            {
                GuestRecordBusiness g = new GuestRecordBusiness();
                g.GuestName = reader["GuestName"].ToString();
                g.IdentityId = reader["IdentityId"].ToString();
                g.ResideDate = Convert.ToDateTime(reader["ResideDate"]);
                g.Deposit = Convert.ToDecimal(reader["Deposit"]);
                g.RoomId = Convert.ToInt32(reader["RoomId"]);
                g.DishPrice = Convert.ToDecimal(reader["DishPrice"]);
                guest.Add(g);
            }
            reader.Close();
            return guest;
        }

        /// <summary>
        /// 查询所有未付款顾客信息
        /// </summary>
        /// <returns></returns>
        public static List<GuestRecordBusiness> GetAllGuest()
        {
            List<GuestRecordBusiness> guest = new List<GuestRecordBusiness>();
            string sql = "select GuestId,GuestName,IdentityId,ResideDate,Deposit,DishPrice,RoomId from GuestRecord as g inner join ResideState as r on g.ResideId=r.ResideId where g.ResideId=@resideId";
            SqlParameter[] para = { new SqlParameter("@resideId", 1) };
            SqlDataReader reader = SqlHelper.DataReader(sql, CommandType.Text, para);
            while (reader.Read())
            {
                GuestRecordBusiness g = new GuestRecordBusiness();
                g.GuestId = Convert.ToInt32(reader["GuestId"]);
                g.GuestName = reader["GuestName"].ToString();
                g.IdentityId = reader["IdentityId"].ToString();
                g.ResideDate = Convert.ToDateTime(reader["ResideDate"]);
                g.Deposit = Convert.ToDecimal(reader["Deposit"]);
                g.DishPrice = Convert.ToDecimal(reader["DishPrice"]);
                g.RoomId = Convert.ToInt32(reader["RoomId"]);
                guest.Add(g);
            }
            reader.Close();
            return guest;
        }

        /// <summary>
        /// 查询所有已付款客户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<GuestRecord> GetCusInfo(string name)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select RoomId,GuestName,Gender,IdentityId,Phone,ResideDate,LeaveDate from GuestRecord as g,ResideState as r where g.ResideId=r.ResideId and g.ResideId=2");
            if (!string.IsNullOrEmpty(name))
            {
                sb.AppendLine(" and g.GuestName like '%'+@guestName+'%'");
            }
            SqlParameter[] para = { new SqlParameter("@guestName", name) };
            SqlDataReader reader = SqlHelper.DataReader(sb.ToString(), CommandType.Text, para);
            List<GuestRecord> record = new List<GuestRecord>();
            while (reader.Read())
            {
                GuestRecord gr = new GuestRecord();
                gr.RoomId = Convert.ToInt32(reader["RoomId"]);
                gr.GuestName = reader["GuestName"].ToString();
                gr.Gender = Convert.ToInt32(reader["Gender"]) == 0 ? "男" : "女";
                gr.IdentityId = reader["IdentityId"].ToString();
                gr.Phone = reader["Phone"].ToString();
                gr.ResideDate = Convert.ToDateTime(reader["ResideDate"]);
                gr.LeaveDate = Convert.ToDateTime(reader["LeaveDate"]);
                record.Add(gr);
            }
            reader.Close();
            return record;
        }

        /// <summary>
        /// 房间类型价格
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public static decimal GetMoney(int roomId)
        {
            string sql = "select TypePrice from RoomType as r inner join Room as m on r.TypeId=m.RoomTypeId where m.RoomId=@roomId";
            SqlParameter[] para = { new SqlParameter("@roomId", roomId) };
            return (decimal)SqlHelper.ExecuteScalar(sql, CommandType.Text, para);
        }


        /// <summary>
        /// 更新结账状态,房间状态,交易号
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public static bool UpdateState(GuestRecord gr)
        {
            string sql = @"update GuestRecord set ResideId=@resideId,TotalMoney=@totalMoney,LeaveDate=@leaveDate,TradeNo=@tradeNo where RoomId=@roomId;update Room set RoomStateId=@roomStateId where RoomId=@roomId;";
            SqlParameter[] para = { new SqlParameter("@resideId",2),
                                      new SqlParameter("@totalMoney",gr.TotalMoney),
                                      new SqlParameter("leaveDate",gr.LeaveDate),
                                      new SqlParameter("tradeNo",gr.TradeNo),
                                      new SqlParameter("roomId",gr.RoomId),
                                    new SqlParameter("roomStateId",2)
                                  };
            return SqlHelper.GetExecuteNonQuery(sql, CommandType.Text, para);
        }
    }
}
