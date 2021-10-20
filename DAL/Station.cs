namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int Id { set; get; }
            public int Name { set; get; }
            public double Longitude { set; get; }
            public double lattitude { set; get; }
            public int ChargeSlots { set; get; }
            public override string ToString()
            {
                return "Id: " + Id + " Name: " + Name + " Longitude: " + Longitude + " lattitude: " + lattitude + " ChargeSlots: " + ChargeSlots;
            }
        }
    }
}
