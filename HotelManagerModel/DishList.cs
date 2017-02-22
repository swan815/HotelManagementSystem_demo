using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Model
{
    [Serializable]
    public class DishList
    {
        //id
        public int DishId { get; set; }
        //名称
        public string DishName { get; set; }
        //价格
        public decimal Price { get; set; }
        //单位
        public string Unit { get; set; }
    }
}
