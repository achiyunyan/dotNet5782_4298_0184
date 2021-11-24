//using IDAL.DO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleUI
//{
//    partial class Program
//    {
//        /// <summary>
//        /// The function does an adding operation according to the choice
//        /// </summary>
//        /// <param name="choice"></param>
//        static private void AddObject(int choice)
//        {
//            int id = new int();
//            int weight = new int();
//            string name;
//            double latitude = new double();
//            double longitude = new double();
//            switch (choice)
//            {
//                case 1://add a station
//                    Console.WriteLine("Enter station Id: ");
//                    int.TryParse(Console.ReadLine(), out id);
//                    Console.WriteLine("Enter station name: ");
//                    name = Console.ReadLine();
//                    Console.WriteLine("Enter station latitude: (between  31.742227429597634 to 31.809648051878856 )");
//                    double.TryParse(Console.ReadLine(), out latitude);
//                    Console.WriteLine("Enter station longitude: (between  35.16242159781234 to 35.22496332365079 )");
//                    double.TryParse(Console.ReadLine(), out longitude);
//                    dataBase.AddStation(new IDAL.DO.Station
//                    {
//                        Id = id,
//                        Name = name,
//                        ChargeSlots = 3,
//                        Latitude = latitude,
//                        Longitude = longitude
//                    });
//                    break;
//                case 2://add a drone
//                    Random rand = new Random();
//                    Console.WriteLine("Enter drone Id: ");
//                    int.TryParse(Console.ReadLine(), out id);                    
//                    Console.WriteLine("Enter drone Max Weight lift ability: \n1)Light \n2)Medium \n3)Heavy");
//                    int.TryParse(Console.ReadLine(), out weight);
//                    dataBase.AddDrone(new IDAL.DO.Drone
//                    {
//                        Id = id,
//                        Model = "EX50" + (weight).ToString(),
//                        MaxWeight = (IDAL.DO.WeightCategories)(weight - 1),
//                    });
//                    break;
//                case 3:// add a customer
//                    string phone;
//                    Console.WriteLine("Enter customer Id: ");
//                    int.TryParse(Console.ReadLine(), out id);
//                    Console.WriteLine("Enter customer name:");
//                    name = Console.ReadLine();
//                    Console.WriteLine("Enter customer phone:");
//                    phone = Console.ReadLine();
//                    //Right now the client chooses the cordinates.not final...
//                    Console.WriteLine("Enter customer latitude: (between  31.742227429597634 to 31.809648051878856 )");
//                    double.TryParse(Console.ReadLine(), out latitude);
//                    Console.WriteLine("Enter customer longitude: (between  35.16242159781234 to 35.22496332365079 )");
//                    double.TryParse(Console.ReadLine(), out longitude);
//                    dataBase.AddCustomer(new IDAL.DO.Customer
//                    {
//                        Id = id,
//                        Name = name,
//                        Phone = phone,
//                        Latitude = latitude,
//                        Longitude = longitude
//                    });
//                    break;
//                case 4://add a parcel
//                    int senderId = new int();
//                    int reciverId = new int();                    
//                    Console.WriteLine("Enter sender Id: ");
//                    int.TryParse(Console.ReadLine(), out senderId);
//                    Console.WriteLine("Enter reciver Id: ");
//                    int.TryParse(Console.ReadLine(), out reciverId);
//                    Console.WriteLine("Enter parcel Weight lift: \n1)Light \n2)Medium \n3)Heavy");
//                    int.TryParse(Console.ReadLine(), out weight);
//                    dataBase.AddParcel(new IDAL.DO.Parcel
//                    {
//                        SenderId = senderId,
//                        ReciverId = reciverId,
//                        DroneId = 0,
//                        Priority = IDAL.DO.Priorities.Normal,
//                        Weight = (IDAL.DO.WeightCategories)(weight - 1),
//                        Requested = DateTime.Now,
//                        Scheduled = DateTime.MinValue,
//                        Delivered = DateTime.MinValue,
//                        PickedUp = DateTime.MinValue
//                    });
//                    break;
//            }
//        }
//        /// <summary>
//        /// The function does an updating operation according to the choice
//        /// </summary>
//        /// <param name="choise"></param>
//         static private void UpdateObject(int choise)
//        {
//            int parcelId = new int();
//            int droneId = new int();
//            int stationId = new int();
//            switch (choise)
//            {
//                case 1://Link Parcel to Drone
//                    Console.WriteLine("Enter Parcel Id: ");
//                    int.TryParse(Console.ReadLine(), out parcelId);
//                    Console.WriteLine("Enter Drone Id: ");
//                    int.TryParse(Console.ReadLine(), out droneId);
//                    Parcel parcel = new Parcel
//                    {
//                        Id = parcelId,
//                         Priority=Priorities.Emergency
//                    };
//                    dataBase.LinkParcelToDrone(parcel, droneId);
//                    break;
//                case 2://pick up a parcel by a drone
//                    Console.WriteLine("Enter Parcel Id: ");
//                    int.TryParse(Console.ReadLine(), out parcelId);
//                    dataBase.PickUpParcel(parcelId);
//                    break;
//                case 3://deliver a parcel
//                    Console.WriteLine("Enter Parcel Id: ");
//                    int.TryParse(Console.ReadLine(), out parcelId);
//                    dataBase.DeliverParcel(parcelId);
//                    break;
//                case 4://Send a drone to charge
//                    Console.WriteLine("Enter Drone Id: ");
//                    int.TryParse(Console.ReadLine(), out droneId);
//                    Console.WriteLine("Choose a station to charge the drone here from the options below:\n\n ");
//                    foreach (IDAL.DO.Station target in dataBase.GetStationsList())
//                    {
//                        if (target.ChargeSlots > 0)
//                            Console.WriteLine(target.ToString() + "\n");
//                    }
//                    Console.WriteLine("Enter the id of the wanted station please: ");
//                    int.TryParse(Console.ReadLine(), out stationId);
//                    dataBase.SendDroneToCharge(droneId, stationId);
//                    break;
//                case 5://free drone from charge
//                    Console.WriteLine();
//                    Console.WriteLine("Enter Drone Id: ");
//                    int.TryParse(Console.ReadLine(), out droneId);
//                    Console.WriteLine("Enter Station Id: ");
//                    int.TryParse(Console.ReadLine(), out stationId);
//                    dataBase.freeDroneFromCharge(droneId, stationId);
//                    break;

//            }
//        }
//        /// <summary>
//        /// The function does a printing operation according to the choice
//        /// </summary>
//        /// <param name="choise"></param>
//        static private void ObjectPrint(int choise)
//        {
//            int tempId = new int();
//            switch (choise)
//            {
//                case 1://station print
//                    Console.WriteLine("Enter the id of the station you would like to see:");
//                    Int32.TryParse(Console.ReadLine(), out tempId);
//                    if (dataBase.GetStation(tempId).Id == 0)
//                        Console.WriteLine("Station was not found");
//                    else
//                        Console.WriteLine(dataBase.GetStation(tempId).ToString());
//                    break;
//                case 2://drone print
//                    Console.WriteLine("Enter the id of the drone you would like to see:");
//                    Int32.TryParse(Console.ReadLine(), out tempId);
//                    if (dataBase.GetDrone(tempId).Id == 0)
//                        Console.WriteLine("Drone was not found");
//                    else
//                        Console.WriteLine(dataBase.GetDrone(tempId).ToString());
//                    break;
//                case 3://customer print
//                    Console.WriteLine("Enter the id of the customer you would like to see:");
//                    Int32.TryParse(Console.ReadLine(), out tempId);
//                    if (dataBase.GetCustomer(tempId).Id == 0)
//                        Console.WriteLine("Customer was not found");
//                    else
//                        Console.WriteLine(dataBase.GetCustomer(tempId).ToString());
//                    break;
//                case 4://parcel print
//                    Console.WriteLine("Enter the id of the parcel you would like to see:");
//                    Int32.TryParse(Console.ReadLine(), out tempId);
//                    if (dataBase.GetParcel(tempId).Id == 0)
//                        Console.WriteLine("Parcel was not found");
//                    else
//                        Console.WriteLine(dataBase.GetParcel(tempId).ToString());
//                    break;
//            }
//        }
//        /// <summary>
//        /// The function does a list print operation according to the choice
//        /// </summary>
//        /// <param name="choise"></param>
//         static private void ListPrint(int choise)
//        {
//            switch (choise)
//            {
//                case 1://stations' list print
//                    Console.WriteLine("Stations:\n");
//                    foreach (IDAL.DO.Station target in dataBase.GetStationsList())
//                    {
//                        Console.WriteLine(target.ToString());
//                    }
//                    break;
//                case 2://drones' list print
//                    Console.WriteLine("Drones:");
//                    foreach (IDAL.DO.Drone target in dataBase.GetDronesList())
//                    {
//                        Console.WriteLine(target.ToString());
//                    }
//                    break;
//                case 3://customers' list print
//                    Console.WriteLine("Customers:");
//                    foreach (IDAL.DO.Customer target in dataBase.GetCustomersList())
//                    {
//                        Console.WriteLine(target.ToString());
//                    }
//                    break;
//                case 4://parcels' list print
//                    Console.WriteLine("Parcels:");
//                    foreach (IDAL.DO.Parcel target in dataBase.GetParcelsList())
//                    {
//                        Console.WriteLine(target.ToString());
//                    }
//                    break;
//                case 5://non linked parcels print
//                    foreach (IDAL.DO.Parcel target in dataBase.GetParcelsList())
//                    {
//                        if (target.DroneId == 0)
//                            Console.WriteLine(target.ToString() + "\n");
//                    }
//                    break;
//                case 6://stations with free hubs print
//                    foreach (IDAL.DO.Station target in dataBase.GetStationsList())
//                    {
//                        if (target.ChargeSlots > 0)
//                            Console.WriteLine(target.ToString() + "\n");
//                    }
//                    break;
//            }
//        }
//        static private double DistanceBetweenTwoPoints(double lat1, double lon1, double lat2, double lon2)
//        {
//            return dataBase.DistanceBetweenTwoPoints(lat1, lon1, lat2, lon2);
//        }

//    }
//}
