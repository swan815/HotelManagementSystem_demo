using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Model
{
    [Serializable]
    /// <summary>
    /// 房间类型
    /// </summary>
    public class RoomType
    {
        public RoomType() { }
        public RoomType(string typeName,decimal typePrice)
        {
            this.TypeName = typeName;
            this.TypePrice = typePrice;
        }
        public RoomType(int typeId,string typeName, decimal typePrice)
        {
            this.TypeId = typeId;
            this.TypeName = typeName;
            this.TypePrice = typePrice;
        }

        //类型编号
        public int TypeId { get; set; }
        //类型名称
        public string TypeName { get; set; }
        //价格
        public decimal TypePrice { get; set; }
    }
}
