using System;
/*using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
using static DalObject.DalObject;

namespace ConsoleUI
{
    partial class Program
    {
        static public void AddObject(int choice)
        {
            int id = new int();
            int weight = new int();
            string name;
            double latitude = new double();
            double longitude = new double();
            switch (choice)
            {
                case 1://add a station
                    Console.WriteLine("Enter station Id: ");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter station name: ");
                    name = Console.ReadLine();
                    //Right now the client chooses the cordinates.not final...
                    Console.WriteLine("Enter station latitude: (between  31.742227429597634 to 31.809648051878856 )");
                    double.TryParse(Console.ReadLine(), out latitude);
                    Console.WriteLine("Enter station longitude: (between  35.16242159781234 to 35.22496332365079 )");
                    double.TryParse(Console.ReadLine(), out longitude);
                    AddStation(new IDAL.DO.Station
                    {
                        Id = id,
                        Name = name,
                        ChargeSlots = 3,
                        Latitude = latitude,
                        Longitude = longitude
                    });
                    break;
                case 2://add a drone
                    Random rand = new Random();
                    Console.WriteLine("Enter drone Id: ");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter drone Max Weight lift ability: \n1)Light \n2)Medium \n3)Heavy");
                    int.TryParse(Console.ReadLine(), out weight);
                    AddDrone(new IDAL.DO.Drone
                    {
                        Id = id,
                        Model = "EX50" + (weight).ToString(),
                        MaxWeight = (IDAL.DO.WeightCategories)(weight - 1),
                        Battery = rand.Next(40, 60),
                        Status = (IDAL.DO.DroneStatus.available)
                    });
                    break;
                case 3:// add a customer
                    string phone;
                    Console.WriteLine("Enter customer Id: ");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter customer name:");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter customer phone:");
                    phone = Console.ReadLine();
                    //Right now the client chooses the cordinates.not final...
                    Console.WriteLine("Enter customer latitude: (between  31.742227429597634 to 31.809648051878856 )");
                    double.TryParse(Console.ReadLine(), out latitude);
                    Console.WriteLine("Enter customer longitude: (between  35.16242159781234 to 35.22496332365079 )");
                    double.TryParse(Console.ReadLine(), out longitude);
                    AddCustomer(new IDAL.DO.Customer
                    {
                        Id = id,
                        Name = name,
                        Phone = phone,
                        Latitude = latitude,
                        Longitude = longitude
                    });
                    break;
                case 4://add a parcel
                    int senderId = new int();
                    int reciverId = new int();
                    Console.WriteLine("Enter Parcel Id: ");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter sender Id: ");
                    int.TryParse(Console.ReadLine(), out senderId);
                    Console.WriteLine("Enter reciver Id: ");
                    int.TryParse(Console.ReadLine(), out reciverId);
                    Console.WriteLine("Enter parcel Weight lift: \n1)Light \n2)Medium \n3)Heavy");
                    int.TryParse(Console.ReadLine(), out weight);
                    AddParcel(new IDAL.DO.Parcel
                    {
                        Id = id,
                        SenderId = senderId,
                        TargetId = reciverId,
                        DroneId = 0,
                        Priority=IDAL.DO.Priorities.normal,
                        Weight = (IDAL.DO.WeightCategories)(weight-1),
                        Requested = DateTime.Now,
                        Scheduled = DateTime.MinValue,
                        Delivered = DateTime.MinValue,
                        PickedUp = DateTime.MinValue
                    });
                    break;
            }
        }

        static public void UpdateObject(int choise)
        {
            int id = new int();
            switch (choise)
            {
                case 1://Link Parcel to Drone
                    Console.WriteLine("Enter Parcel Id: ");
                    int.TryParse(Console.ReadLine(), out id);
                    LinkParcelToDrone(id);
                    break;
                case 2://pick up a parcel by a drone
                    Console.WriteLine("Enter Parcel Id: ");
                    int.TryParse(Console.ReadLine(), out id);
                    PickUpParcel(id);
                    break;
                case 3:

                    break;

            }
        }

        static public void ObjectPrint(int choise)
        {
            int tempId = new int();
            switch (choise)
            {
                case 1://station
                    Console.WriteLine("Enter the id of the station you would like to see:");
                    Int32.TryParse(Console.ReadLine(), out tempId);
                    if (GetStation(tempId).Id == -1)
                        Console.WriteLine("Station was not found");
                    else
                        Console.WriteLine(GetStation(tempId).ToString());
                    break;
                case 2://drone
                    Console.WriteLine("Enter the id of the drone you would like to see:");
                    Int32.TryParse(Console.ReadLine(), out tempId);
                    if (GetDrone(tempId).Id == -1)
                        Console.WriteLine("Drone was not found");
                    else
                        Console.WriteLine(GetDrone(tempId).ToString());
                    break;
                case 3://customer
                    Console.WriteLine("Enter the id of the customer you would like to see:");
                    Int32.TryParse(Console.ReadLine(), out tempId);
                    if (GetCustomer(tempId).Id == -1)
                        Console.WriteLine("Customer was not found");
                    else
                        Console.WriteLine(GetCustomer(tempId).ToString());
                    break;
                case 4://parcel
                    Console.WriteLine("Enter the id of the parcel you would like to see:");
                    Int32.TryParse(Console.ReadLine(), out tempId);
                    if (GetParcel(tempId).Id == -1)
                        Console.WriteLine("Parcel was not found");
                    else
                        Console.WriteLine(GetParcel(tempId).ToString());
                    break;
            }
        }
        static public void ListPrint(int choise)
        {
            switch (choise)
            {
                case 1://station
                    Console.WriteLine("Stations:\n");
                    foreach (IDAL.DO.Station target in GetStationsList())
                    {
                        Console.WriteLine(target.ToString());
                    }
                    break;
                case 2://drone
                    Console.WriteLine("Drones:");
                    foreach (IDAL.DO.Drone target in GetDronesList())
                    {
                        Console.WriteLine(target.ToString());
                    }
                    break;
                case 3://customer
                    Console.WriteLine("Customers:");
                    foreach (IDAL.DO.Customer target in GetCustomersList())
                    {
                        Console.WriteLine(target.ToString());
                    }
                    break;
                case 4://parcel
                    Console.WriteLine("Parcels:");
                    foreach (IDAL.DO.Parcel target in GetParcelsList())
                    {
                        Console.WriteLine(target.ToString());
                    }
                    break;
                case 5://non linked parcels
                    foreach (IDAL.DO.Parcel target in GetParcelsList())
                    {
                        if (target.DroneId == 0)
                            Console.WriteLine(target.ToString() + "\n");
                    }
                    break;
                case 6://stations with free hubs
                    foreach (IDAL.DO.Station target in GetStationsList())
                    {
                        if (target.ChargeSlots > 0)
                            Console.WriteLine(target.ToString() + "\n");
                    }
                    break;
            }
        }

    }
}
