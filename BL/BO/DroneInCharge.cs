using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneInCharge
    {
        public int Id { set; get; }
        public double Battery { set; get; }
        public override string ToString()
        {
            return $"   Drone Id:         {Id}\n" +
                   $"   Battery:          {Math.Round(Battery)}%\n";
        }
    }
}
