using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class ListParcel
    {
        public int Id { set; get; }
        public string SenderName { set; get; }
        public string ReceiverName { set; get; }
        public WeightCategory WeightCategory { set; get; }
        public Priority Priority { set; get; }
        public ParcelState State { set; get; }
    }
}
