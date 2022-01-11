using System;

namespace DO
{
    public struct Parcel
    {
        public int Id { set; get; }
        public int SenderId { set; get; }
        public int ReciverId { set; get; }
        public WeightCategories Weight { set; get; }
        public Priorities Priority { set; get; }
        public DateTime? Requested { set; get; }
        public DateTime? Scheduled { set; get; }
        public DateTime? PickedUp { set; get; }
        public DateTime? Delivered { set; get; }
        public int DroneId { set; get; }
        public override string ToString()
        {
            return $" Parcel Id:        {Id}\n" +
                   $" Sender Id:        {SenderId}\n" +
                   $" Reciver Id:       {ReciverId}\n" +
                   $" Weight category:  {Weight}\n" +
                   $" Priority:         {Priority}\n" +
                   $" Drone Id:         {DroneId}\n" +
                   $" Requested time:   {Requested}\n" +
                   ((Scheduled == null) ? "" : $" Scheduled time:   {Scheduled}\n") +
                   ((PickedUp == null) ? "" : $" PickedUp time:    {PickedUp}\n") +
                   ((Delivered == null) ? "" : $" Delivery time:    {Delivered}\n");
        }
    }
}
