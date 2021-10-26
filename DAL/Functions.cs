using System;
namespace IDAL
{
    namespace DO
    {
        internal class Functions
        {
            internal static string ToSexagesimal(double lat, double lon)
            {
                int m1 = (int)lat;
                double help = (lat - m1) * 60;
                int m2 = (int)help;
                double m3 = (help - m2) * 60;
                
                int n1 = (int)lon;
                help = (lon - n1) * 60;
                int n2 = (int)help;
                double n3 = (help - n2) * 60;

                return $"{((lat < 0) ? "S" : "N")} {m1}°{m2}'{Math.Round(m3,3)}\",{((lon < 0) ? "W" : "E")} {n1}°{n2}'{Math.Round(n3,3)}\"";
            }
        }
    }
}
