using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station
    {
        private List<DroneInCharge> dronesList = new List<DroneInCharge>();

        public int Id { set; get; }
        public string Name { set; get; }
        public Location Location { get; set; }
        public int FreeChargeSlots { set; get; }
        public List<DroneInCharge> DronesList { get { return dronesList; } }
        public override string ToString()
        {
            string str;
            str = $" Station Id:       {Id}\n" +
                  $" Name:             {Name}\n" +
                  $" Free Charge slots:{FreeChargeSlots}\n" +
                  $" Location:         {Location}\n";
            if(dronesList.Any())
            {
                str += "Drones in charge:\n\n";
                foreach (var dr in dronesList)
                    str += dr.ToString() + '\n';
            }
            return str;
        }
    }
}