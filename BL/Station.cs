namespace BL
{
    namespace BO
    {
        public class Station
        {
            public int Id { set; get; }
            public string Name { set; get; }
            public Location Location { get; set; }
            public int ChargeSlots { set; get; }
        }
    }
}