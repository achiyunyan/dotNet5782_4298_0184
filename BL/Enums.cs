using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public enum DroneState
    {
        Available, 
        Maintenance, 
        Delivery
    }

    public enum WeightCategory
    {
        Light,
        Medium,
        Heavy
    }

    public enum Priority
    {
        Normal,
        Express,
        Emergency
    }

    public enum ParcelState
    {        
        Created, 
        Associated, 
        Collected, 
        Provided
    }
}
