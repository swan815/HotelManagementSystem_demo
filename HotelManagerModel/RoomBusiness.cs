using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Model
{
    [Serializable]
    public class RoomBusiness:Room
    {
        //房间状态名称
        public string RoomStateName { get; set; }
        //房间类型名称
        public string TypeName { get; set; }
    }
}
