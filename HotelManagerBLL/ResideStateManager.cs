using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.Model;
using HotelManager.DAL;

namespace HotelManager.BLL
{
    public static class ResideStateManager
    {
        /// <summary>
        /// 获取房间状态列表
        /// </summary>
        /// <returns></returns>
        public static List<ResideState> GetAllState()
        {
            return ResideStateService.GetAllState();
        }
    }
}
