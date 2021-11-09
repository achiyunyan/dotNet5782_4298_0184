using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    partial class Program
    {
        static private void AddObject(int choice)
        {
            int id = new int();
            int weight = new int();
            string name;
            double latitude = new double();
            double longitude = new double();
            IBL.BO.Location loc = new IBL.BO.Location();
            switch (choice)
            {
                case 1://add a station
                    Console.WriteLine("Enter station Id: ");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter station name: ");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter station latitude: (between  31.742227429597634 to 31.809648051878856 )");
                    double.TryParse(Console.ReadLine(), out latitude);
                    Console.WriteLine("Enter station longitude: (between  35.16242159781234 to 35.22496332365079 )");
                    double.TryParse(Console.ReadLine(), out longitude);
                    loc.Latitude = latitude;
                    loc.Longitude = longitude;
                    myBl.AddStation(new IBL.BO.Station()
                    {
                        Id = id,
                        Name = name,
                        ChargeSlots = 3,
                        Location = loc
                    });
                    break;
                case 2://add a drone
                    Console.WriteLine("Enter drone Id: ");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter drone Max Weight lift ability: \n1)Light \n2)Medium \n3)Heavy");
                    int.TryParse(Console.ReadLine(), out weight);
                    myBl.AddDrone(new IBL.BO.Drone()
                    {
                        Id = id,
                        Model = "EX50" + (weight).ToString(),
                        WeightCategory = (IBL.BO.WeightCategory)(weight - 1)
                    });
                    break;
                case 3:

                    break;
            }
        }

    }
}
