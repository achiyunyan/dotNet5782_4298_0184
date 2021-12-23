using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Drone
    {
        public int Id { set; get; }
        public string Model { set; get; }
        public WeightCategories MaxWeight { set; get; }
        public override string ToString()
        {
            return $" Drone Id:         {Id}\n" +
                   $" Model:            {Model}\n" +
                   $" Maximum Weight:   {MaxWeight}\n";
        }
    }
}
