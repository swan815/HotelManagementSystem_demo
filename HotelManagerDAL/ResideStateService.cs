using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.Model;
using System.Data.SqlClient;

namespace HotelManager.DAL
{
    public static class ResideStateService
    {
        /// <summary>
        /// 获取房间状态列表
        /// </summary>
        /// <returns></returns>
        public static List<ResideState> GetAllState()
        {
            string sql = "Select * from ResideState";
            SqlDataReader reader = SqlHelper.DataReader(sql);
            List<ResideState> state = new List<ResideState>();
            while (reader.Read())
            {
                ResideState s = new ResideState();
                s.ResideId = Convert.ToInt32(reader["ResideId"]);
                s.ResideName = reader["ResideName"].ToString();
                state.Add(s);
            }
            reader.Close();
            return state;
        }
    }
}
