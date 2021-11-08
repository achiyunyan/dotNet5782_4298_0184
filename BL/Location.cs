using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
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

            return $"({((Latitude < 0) ? "S" : "N")} {m1}°{m2}'{Math.Round(m3, 3)}\"," +
                $"{((Longitude < 0) ? "W" : "E")} {n1}°{n2}'{Math.Round(n3, 3)}\")";
        }
    }
}
