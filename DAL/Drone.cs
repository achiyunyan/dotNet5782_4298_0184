using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int Id { set; get; }
            public string Model { set; get; }
            public WeightCategories MaxWeight { set; get; }
            public DroneStatus Status { set; get; }
            public double Battery { set; get; }
            public override string ToString()
            {
                //  return "Id: " + Id + " Model: " + Model + " MaxWeight: " + MaxWeight +
                //     "\nStatus: " + Status + " Battery: " + Battery + "%";
                string str = "";
                str += $" Id is:           {Id} \n";
                str += $" Model is:        {Model} \n";
                str += $" MaxWeight:       {MaxWeight} \n";
                str += $" Status:          {Status} \n";
                str += $" Battery level:   {Battery} % \n";
                return str;
            }
        }
    }
}
