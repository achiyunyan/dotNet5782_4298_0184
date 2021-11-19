using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelInTransit
    {
        public int Id { set; get; }
        public bool State { set; get; } // 0 - Associated, 1 - Collected
        public Priority Priority { set; get; }
        public WeightCategory WeightCategory { set; get; }
        public CustomerInParcel Sender { set; get; }
        public CustomerInParcel Receiver { set; get; }
        public Location PickUp { set; get; }
        public Location Destination { set; get; }
        public double Distance { set; get; }
        public override string ToString()
        {
            return $"   Parcel Id:        {Id}\n" +
                   $"   State:            {State}\n" +
                   $"   Priority:         {Priority}\n" +
                   $"   Weight Category:  {WeightCategory}\n" +
                   $"   Sender:\n{Sender}" +
                   $"   Receiver:\n{Receiver}" +
                   $"   PickUp Location:  {PickUp}\n" +
                   $"   Destination Location: {Destination}\n" +
                   $"   Distance:         {Distance}\n";
        }
    }
}
