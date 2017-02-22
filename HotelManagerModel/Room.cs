using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Model
{
    [Serializable]
    /// <summary>
    /// 房间信息实体类
    /// </summary>
    public class Room
    {
        //房间编号
        public int RoomId { get; set; }
        //房间类型ID
        public int RoomTypeId { get; set; }
        //房间状态ID
        public int RoomStateId { get; set; }
        //房间描述
        public string Description { get; set; }
        //床位数
        public int BedNum { get; set; }
        //入住顾客数量
        public int GuestNum { get; set; }
    }
}
