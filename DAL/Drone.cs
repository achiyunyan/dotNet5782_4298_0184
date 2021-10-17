using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        struct Drone
        {
            public int Id { set; get; }
            public string Model { set; get; }
            public WeightCategories MaxWeight { set; get; }
            public DroneStatus Status { set; get; }
            public double Battery { set; get; }
        }
    }
}
