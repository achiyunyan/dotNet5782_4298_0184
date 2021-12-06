using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Parcel
    {
        public int Id { set; get; }
        public CustomerInParcel Sender { set; get; }
        public CustomerInParcel Receiver { set; get; }
        public WeightCategory WeightCategory { set; get; }
        public Priority Priority { set; get; }
        public DroneInParcel Drone { set; get; }
        public DateTime? Requested { set; get; }        
        public DateTime? Scheduled { set; get; }
        public DateTime? PickedUp { set; get; }
        public DateTime? Delivered { set; get; }
        public override string ToString()
        {
            return $" Parcel Id:        {Id}\n" +
                   $" Sender:\n {Sender}" +
                   $" Receiver:\n {Receiver}" +
                   $" Weight Category:  {WeightCategory}\n" +
                   $" Priority:         {Priority}\n" +
                   $" Drone:\n {Drone}" +
                   $" Requested time:   {Requested}\n" +
                   ((Scheduled == null) ? "" : $" Scheduled time:   {Scheduled}\n") +
                   ((PickedUp == null)  ? "" : $" PickedUp time:    {PickedUp}\n") +
                   ((Delivered == null) ? "" : $" Delivery time:    {Delivered}\n");
        }
    }
}
