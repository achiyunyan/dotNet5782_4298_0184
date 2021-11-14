using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ListDrone
    {
        public int Id { set; get; }
        public string Model { set; get; }
        public WeightCategory WeightCategory { set; get; }
        public int Battery { set; get; }
        public DroneState State { set; get; }
        public Location Location { set; get; }
        public int ParcelId { set; get; } = 0;
        public override string ToString()
        {
            return $" Drone Id:         {Id}\n" +
                   $" Model:            {Model}\n" +
                   $" Weight Category:  {WeightCategory}\n" +
                   $" Battery:          {Battery}%\n" +
                   $" State:            {State}\n" +
                   $" Location:         {Location}\n" +
                   ((ParcelId == 0) ? " " : $" Parcel Id:        {ParcelId}\n");
        }
    }
}
