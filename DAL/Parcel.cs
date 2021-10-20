using System;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public int Id { set; get; }
            public int SenderId { set; get; }
            public int TargetId { set; get; }
            public WeightCategories Weight { set; get; }
            public Priorities Priority { set; get; }
            public DateTime Requested { set; get; }
            public int DroneId { set; get; }
            public DateTime Scheduled { set; get; }
            public DateTime PickedUp { set; get; }
            public DateTime Delivered { set; get; }
            public override string ToString()
            {
                return "Id: " + Id + " Sender Id: " + SenderId + " Target Id: " + TargetId + " Weight: " + Weight +
                    "\nPriority: " + Priority + " Requested time: " + Requested + " Drone Id: " + DroneId +
                    "\nScheduled time: " + Scheduled + " PickedUp time: " + PickedUp + "Delivery time: " + Delivered;
            }
        }
    }
}
