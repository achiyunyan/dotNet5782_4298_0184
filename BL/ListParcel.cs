using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ListParcel
    {
        public int Id { set; get; }
        public string SenderName { set; get; }
        public string ReceiverName { set; get; }
        public WeightCategory WeightCategory { set; get; }
        public Priority Priority { set; get; }
        public ParcelState State { set; get; }
        public override string ToString()
        {
            return $" Parcel Id:        {Id}\n" +
                   $" Sender Name:      {SenderName}\n" +
                   $" Receiver Name:    {ReceiverName}\n" +
                   $" Weight Category:  {WeightCategory}\n" +
                   $" Priority:         {Priority}\n" +
                   $" State:            {State}\n";
        }
    }
}
