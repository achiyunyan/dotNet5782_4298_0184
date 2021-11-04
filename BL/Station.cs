namespace BL
{
    namespace BO
    {
        public class Station
        {
            public int Id { set; get; }
            public string Name { set; get; }
            public double Longitude { set; get; }
            public double Latitude { set; get; }
            public int ChargeSlots { set; get; }
        }
    }
}