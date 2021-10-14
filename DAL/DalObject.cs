using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    struct Customer
    { 
        public int Id { set; get; }
        public string Name { set; get; }
        public string Phone { set; get; }
        public double Longitude { set; get; }
        public double Latitude { set; get; } 
    }
    struct parcel
    {
        public int Id { set; get; }
        public int SenderId { set; get; }
        public int TargetId { set; get; }
        public IDAL.DO.WeightCategories Weight { set; get; }
        public IDAL.DO.Priorities Priority { set; get; }
        public DateTime Requested { set; get; }
        public int DroneId { set; get; }
        public DateTime Scheduled { set; get; }
        public DateTime PickedUp { set; get; }
        public DateTime Delivered { set; get; }
    }
    
    struct Station
    {
        public int Id { set; get; }
        public int Name { set; get; }
        public double Longitude { set; get; }
        public double lattitude { set; get; }
        public int ChargeSlots { set; get; }

    }

    struct DroneCharge
    {
        public int DroneId { set; get; }
        public int StationId { set; get; }
    }
}
        
    
    struct Drone
        {
           public int Id { set; get; }
           public string Model { set; get; } 

        }
   }
