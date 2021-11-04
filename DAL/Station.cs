namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int Id { set; get; }
            public string Name { set; get; }
            public double Longitude { set; get; }
            public double Latitude { set; get; }
            public int ChargeSlots { set; get; }
            public override string ToString()
            {
                return $" Station Id:       {Id}\n" +
                       $" Name:             {Name}\n" +
                       $" Location:         ({Functions.ToSexagesimal(Latitude,Longitude)})\n" +
                       $" Charge Slots:     {ChargeSlots}\n";
            }
        }
    }
}
