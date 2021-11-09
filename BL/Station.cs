using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Station
    {
        private List<DroneInCharge> droneList = new List<DroneInCharge>();

        public int Id { set; get; }
        public string Name { set; get; }
        public Location Location { get; set; }
        public int FreeChargeSlots { set; get; }
        public List<DroneInCharge> DroneList { get { return droneList; } }
        public override string ToString()
        {
            string str;
            str = $" Station Id:       {Id}\n" +
                  $" Name:             {Name}\n" +
                  $" Free Charge slots:{FreeChargeSlots}\n" +
                  $" Location:         {Location}\n";
            if(droneList.Any())
            {
                str += "Drones in charge:\n\n";
                foreach (var dr in droneList)
                    str += dr.ToString() + '\n';
            }
            return str;
        }
    }
}