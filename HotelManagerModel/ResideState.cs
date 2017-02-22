using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Model
{
    [Serializable]
    /// <summary>
    /// 入住状态
    /// </summary>
    public class ResideState
    {
        //状态ID
        public int ResideId { get; set; }
        //状态名称
        public string ResideName { get; set; }
    }
}
