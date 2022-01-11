using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Drone
    {
        public int Id { set; get; }
        public string Model { set; get; }
        public WeightCategory WeightCategory { set; get; }
        public double Battery { set; get; }
        public DroneState State { set; get; }
        public ParcelInTransit Parcel { set; get; }
        public Location Location { set; get; }
        public override string ToString()
        {
            return $" Drone Id:         {Id}\n" +
                   $" Model:            {Model}\n" +
                   $" Weight Category:  {WeightCategory}\n" +
                   $" Battery:          {Math.Round(Battery)}%\n" +
                   $" State:            {State}\n" +
                   $" Location:         {Location}\n" +
                   ((Parcel != default) ? $" Parcel:\n{Parcel}" : "");
        }
    }
}
