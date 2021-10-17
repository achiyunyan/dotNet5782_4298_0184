namespace IDAL
{
    namespace DO
    {
        struct DroneCharge
        {
            public int DroneId { set; get; }
            public int StationId { set; get; }
            public override string ToString()
            {
                return "Drone Id: " + DroneId + " Station ID: " + StationId;
            }
        }
    }
}
