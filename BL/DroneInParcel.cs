namespace IBL.BO
{
    public class DroneInParcel
    {
        public int Id { set; get; }
        public double Battery { set; get; }
        public Location Location { set; get; }
        public override string ToString()
        {
            return $" Drone Id:         {Id}\n" +
                   $" Battery:          {Battery}%\n" +
                   $" Location:         {Location}\n";
        }
    }
}