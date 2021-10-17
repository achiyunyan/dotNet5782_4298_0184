namespace IDAL
{
    namespace DO
    {
        struct Customer
        {
            public int Id { set; get; }
            public string Name { set; get; }
            public string Phone { set; get; }
            public double Longitude { set; get; }
            public double Latitude { set; get; }
            public override string ToString()
            {
                return "Id: " + Id + " Name: " + Name + " Phone: " + Phone + 
                    "\nLongitude: " + Longitude + " Latitude: " + Latitude;
            }
        }
    }
}
