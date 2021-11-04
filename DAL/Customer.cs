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
                int m1 = (int)Latitude;
                double help = (Latitude - m1) * 60;
                int m2 = (int)help;
                double m3 = (help - m2) * 60;

                int n1 = (int)Longitude;
                help = (Longitude - n1) * 60;
                int n2 = (int)help;
                double n3 = (help - n2) * 60;

                return $" Customer Id:       {Id}\n" +
                       $" Name:              {Name}\n" +
                       $" Phone number:      {Phone}\n" +
                       $" Location:          ({Functions.ToSexagesimal(Latitude,Longitude)})\n";
            }
        }
    }
}
