namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {

            public int Id { set; get; }
            public string Name { set; get; }
            public string Phone { set; get; }
            public double Longitude { set; get; }
            public double Latitude { set; get; }
            public override string ToString()
            {
                return $" Customer Id:       {Id}\n" +
                       $" Name:              {Name}\n" +
                       $" Phone number:      {Phone}\n" +
                       $" Location:          ({Latitude},{Longitude})\n";
            }
        }
    }
}
