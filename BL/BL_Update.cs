using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void UpdateDrone(Drone drone)
        {
            IDAL.DO.Drone dalDrone = new IDAL.DO.Drone()
            {
                Id = drone.Id,
                MaxWeight = (IDAL.DO.WeightCategories)drone.WeightCategory,
                Model = drone.Model
            };
        }
    }
}
