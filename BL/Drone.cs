using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Drone
    {
        public int Id { set; get; }
        public string Model { set; get; }
        public WeightCategory WeightCategory { set; get; }
        public int Battery { set; get; }
        public DroneState State { set; get; }
        public ParcelInTransit Parcel { set; get; }
        public Location Location { set; get; }
        public override string ToString()
        {
            return $" Drone Id:         {Id}\n" +
                   $" Model:            {Model}\n" +
                   $" Weight Category:  {WeightCategory}\n" +
                   $" Battery:          {Battery}%\n" +
                   $" State:            {State}\n" +
                   $" Parcel:\n         {Parcel}\n" +
                   $" Location:         {Location}\n";
        }
    }
}
