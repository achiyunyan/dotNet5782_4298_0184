using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneInCharge
    {
        public int Id { set; get; }
        public double Battery { set; get; }
        public override string ToString()
        {
            return $" Drone Id:         {Id}\n" +
                   $" Battery:          {Battery}%\n";
        }
    }
}
