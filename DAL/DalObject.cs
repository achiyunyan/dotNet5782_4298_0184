using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DataSource
    {
        internal static List<Drone> Drones = new List<Drone>(10);
        internal static List<Station> Stations = new List<Station>(5);
        internal static List<Customer> Customers = new List<Customer>(100);
        internal static List<Parcel> Parcels = new List<Parcel>(1000);

        internal class Config
        {
            internal static void Initialize()
            {
                Random rand = new Random();
                int a = new int();
                a = rand.Next();
                //adds ranmdom data
            }
        }
    }
}
