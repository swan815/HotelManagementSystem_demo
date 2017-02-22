using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Model
{
    [Serializable]
    public class RoomState
    {
        //状态编号：
        public int RoomStateId { get; set; }
        //状态名称
        public string RoomStateName { get; set; }
    }
}
