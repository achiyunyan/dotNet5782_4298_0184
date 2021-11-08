using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public enum DroneStates
    {
        Available, 
        Maintenance, 
        Delivery
    }

    public enum WeightCategories
    {
        Light,
        Medium,
        Heavy
    }

    public enum Priorities
    {
        Normal,
        Express,
        Emergency
    }

    public enum ParcelStates
    {        
        Created, 
        Associated, 
        Collected, 
        Provided
    }
}
