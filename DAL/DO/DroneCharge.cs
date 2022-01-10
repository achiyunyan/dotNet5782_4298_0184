using System;

namespace DO
{
    public struct DroneCharge
    {
        public int DroneId { set; get; }
        public int StationId { set; get; }
        public DateTime StatrtTime { set; get;}
        public override string ToString()
        {
            return $" Drone Id:         {DroneId}\n" +
                   $" Station Id:       {StationId}\n" +
                   $" Statrt Time:      {StatrtTime}\n";
        }
    }
}
