using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Parcel
    {
        public int Id { set; get; }
        public CustomerInParcel Sender { set; get; }
        public CustomerInParcel Receiver { set; get; }
        public WeightCategory WeightCategory { set; get; }
        public Priority Priority { set; get; }
        public DroneInParcel Drone { set; get; }
        public DateTime Requested { set; get; }        
        public DateTime Scheduled { set; get; }
        public DateTime PickedUp { set; get; }
        public DateTime Delivered { set; get; }
        public override string ToString()
        {
            return $" Parcel Id:        {Id}\n" +
                   $" Sender:\n" + Sender.ToString() +
                   $" Receiver:\n" + Receiver.ToString() +
                   $" Weight Category:  {WeightCategory}\n" +
                   $" Priority:         {Priority}\n" +
                   $" Drone:\n" + Drone.ToString() +
                   $" Requested time:   {Requested}\n" +
                   ((Scheduled == DateTime.MinValue) ? "" : $" Scheduled time:   {Scheduled}\n") +
                   ((PickedUp == DateTime.MinValue)  ? "" : $" PickedUp time:    {PickedUp}\n") +
                   ((Delivered == DateTime.MinValue) ? "" : $" Delivery time:    {Delivered}\n");
        }
    }
}
