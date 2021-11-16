using IBL.BO;
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
            try
            {
                int id;
                int weight;
                string name;
                double latitude;
                double longitude;
                string phone;
                switch (choice)
                {
                    case 1://add a station
                        int freeSlots;
                        InputStation(out id, out name, out latitude, out longitude, out freeSlots);
                        myBl.AddStation(new Station()
                        {
                            Id = id,
                            Name = name,
                            FreeChargeSlots = freeSlots,
                            Location = new Location { Latitude = latitude, Longitude = longitude }
                        });
                        break;
                    case 2://add a drone
                        string model;
                        int stationId;
                        InputDrone(out id, out weight, out model, out stationId);
                        myBl.AddDrone(new Drone()
                        {
                            Id = id,
                            Model = model,
                            WeightCategory = (IBL.BO.WeightCategory)(weight - 1),
                        }, stationId); ;
                        break;
                    case 3://add customer
                        InputCustomer(out id, out name, out phone, out longitude, out latitude);
                        myBl.AddCustomer(new Customer()
                        {
                            Id = id,
                            Name = name,
                            Phone = phone,
                            Location = new Location { Latitude = latitude, Longitude = longitude }
                        });
                        break;
                    case 4://add parcel 
                        int senderId;
                        int reciverId;
                        int priority;
                        InputParcel(out senderId, out reciverId, out weight, out priority);
                        myBl.AddParcel(senderId, reciverId, weight, priority);
                        break;
                }
            }
            catch(BL.BlException exem)
            {
                Console.WriteLine(exem.Message);
            }
        }
              
        private static void InputStation(out int id, out string name, out double latitude, out double longitude, out int slots)
        {
            Console.WriteLine("Enter station Id: ");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("Enter station name: ");
            name = Console.ReadLine();
            Console.WriteLine("Enter station latitude: (between  31.742227429597634 to 31.809648051878856 )");
            double.TryParse(Console.ReadLine(), out latitude);
            Console.WriteLine("Enter station longitude: (between  35.16242159781234 to 35.22496332365079 )");
            double.TryParse(Console.ReadLine(), out longitude);
            Console.WriteLine("Enter number of available slots: ");
            int.TryParse(Console.ReadLine(), out slots);
        }

        private static void InputDrone(out int id, out int weight, out string model, out int stationId)
        {
            Console.WriteLine("Enter drone Id: ");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("Enter drone Model: ");
            model = Console.ReadLine();
            Console.WriteLine("Enter drone Max Weight lift ability: \n1)Light \n2)Medium \n3)Heavy");
            int.TryParse(Console.ReadLine(), out weight);
            Console.WriteLine("Enter drone initial station id: ");
            int.TryParse(Console.ReadLine(), out stationId);
        }

        private static void InputCustomer(out int id, out string name, out string phone, out double latitude, out double longitude)
        {
            Console.WriteLine("Enter customer Id: ");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("Enter customer name: ");
            name = Console.ReadLine();
            Console.WriteLine("Enter customer phone number: ");
            phone = Console.ReadLine();
            Console.WriteLine("Enter customer latitude: (between  31.742227429597634 to 31.809648051878856 )");
            double.TryParse(Console.ReadLine(), out latitude);
            Console.WriteLine("Enter customer longitude: (between  35.16242159781234 to 35.22496332365079 )");
            double.TryParse(Console.ReadLine(), out longitude);
        }

        private static void InputParcel(out int senderId, out int reciverId, out int weight, out int priority)
        {
            Console.WriteLine("Enter sender Id: ");
            int.TryParse(Console.ReadLine(), out senderId);
            Console.WriteLine("Enter reciver Id: ");
            int.TryParse(Console.ReadLine(), out reciverId);
            Console.WriteLine("Enter parcel weight: \n1)Light \n2)Medium \n3)Heavy");
            int.TryParse(Console.ReadLine(), out weight);
            Console.WriteLine("Enter parcel priority: \n1)Normal \n2)Express \n3)Emergency");
            int.TryParse(Console.ReadLine(), out priority);
        }
    }
}
