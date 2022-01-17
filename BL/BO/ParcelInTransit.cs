using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelInTransit
    {
        public int Id { set; get; }
        public bool State { set; get; } // false- Associated, true - Collected
        public Priority Priority { set; get; }
        public WeightCategory WeightCategory { set; get; }
        public CustomerInParcel Sender { set; get; }
        public CustomerInParcel Receiver { set; get; }
        public Location PickUp { set; get; }
        public Location Destination { set; get; }
        public double Distance { set; get; }
        public override string ToString()
        {
            return $"Parcel Id:        {Id}\n" +
                    "State:            " + (State ? "Collected" : "Associated") + "\n" +
                   $"Priority:         {Priority}\n" +
                   $"Weight Category:  {WeightCategory}\n" +
                   $"Distance:         {Math.Round(Distance,2)}km\n" +
                   $"Sender:\n{Sender}" +
                   $"Receiver:\n{Receiver}" +
                   $"PickUp Location:      {PickUp}\n" +
                   $"Destination Location: {Destination}\n";
        }
    }
}
