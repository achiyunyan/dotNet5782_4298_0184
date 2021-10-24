using System;
/*using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
using DalObject;

namespace ConsoleUI
{
    partial class Program
    {
        static public void UpdateObject(int choise)
        {
            switch(choise)
            {
                case 1:
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
                    Console.WriteLine(DalObject.DalObject.GetStation(tempId).ToString());
                    break;
                case 2://drone
                    Console.WriteLine("Enter the id of the drone you would like to see:");
                    Int32.TryParse(Console.ReadLine(), out tempId);
                    Console.WriteLine(DalObject.DalObject.GetDrone(tempId).ToString());
                    break;
                case 3://customer
                    Console.WriteLine("Enter the id of the customer you would like to see:");
                    Int32.TryParse(Console.ReadLine(), out tempId);
                    Console.WriteLine(DalObject.DalObject.GetCustomer(tempId).ToString());
                    break;
                case 4://parcel
                    Console.WriteLine("Enter the id of the parcel you would like to see:");
                    Int32.TryParse(Console.ReadLine(), out tempId);
                    Console.WriteLine(DalObject.DalObject.GetParcel(tempId).ToString());
                    break;
            }
        }
        static public void ListPrint(int choise)
        {
            switch (choise)
            {
                case 1://station
                    Console.WriteLine("Stations:\n");
                    foreach (IDAL.DO.Station target in DalObject.DalObject.GetStationsList())
                    {
                        Console.WriteLine(target.ToString());
                    }
                    break;
                case 2://drone
                    Console.WriteLine("Drones:");
                    foreach (IDAL.DO.Drone target in DalObject.DalObject.GetDronesList())
                    {
                        Console.WriteLine(target.ToString());
                    }
                    break;
                case 3://customer
                    Console.WriteLine("Customers:");
                    foreach (IDAL.DO.Customer target in DalObject.DalObject.GetCustomersList())
                    {
                        Console.WriteLine(target.ToString());
                    }
                    break;
                case 4://parcel
                    Console.WriteLine("Parcels:");
                    foreach (IDAL.DO.Parcel target in DalObject.DalObject.GetParcelsList())
                    {
                        Console.WriteLine(target.ToString());
                    }
                    break;
                case 5://non linked parcels
                    foreach (IDAL.DO.Parcel target in DalObject.DalObject.GetParcelsList())
                    {
                        if (target.DroneId == -1)
                            Console.WriteLine(target.ToString() + "\n");
                    }
                    break;
                case 6://stations with free hubs
                    foreach (IDAL.DO.Station target in DalObject.DalObject.GetStationsList())
                    {
                        if (target.ChargeSlots > 0)
                            Console.WriteLine(target.ToString() + "\n");
                    }
                    break;
            }
        }

    }
}
