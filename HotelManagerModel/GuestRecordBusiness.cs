using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Model
{
    [Serializable]
    public class GuestRecordBusiness:GuestRecord
    {
        //房间状态(结账 未结账)
        public string ResideName { get; set; }
    }
}
