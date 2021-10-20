using System;
/*using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace ConsoleUI
{
    partial class Program
    {

        static public void ObjectPrint(int choise)
        {
            bool success = new bool();
            int tempId = new int();
            switch (choise)
            {
                case 1://station
                    Console.WriteLine("Enter the id of the station you would like to see:");
                    success = Int32.TryParse(Console.ReadLine(), out tempId);
                    foreach (IDAL.DO.Station target in DalObject.DataSource.Stations)
                    {
                        if (target.Id.Equals(tempId))
                            Console.WriteLine(target.ToString());
                    }
                    break;
                case 2://drone
                    Console.WriteLine("Enter the id of the drone you would like to see:");
                    success = Int32.TryParse(Console.ReadLine(), out tempId);
                    foreach (IDAL.DO.Drone target in DalObject.DataSource.Drones)
                    {
                        if (target.Id.Equals(tempId))
                            Console.WriteLine(target.ToString());
                    }
                    break;
                case 3://customer
                    Console.WriteLine("Enter the id of the customer you would like to see:");
                    success = Int32.TryParse(Console.ReadLine(), out tempId);
                    foreach (IDAL.DO.Customer target in DalObject.DataSource.Customer)
                    {
                        if (target.Id.Equals(tempId))
                            Console.WriteLine(target.ToString());
                    }
                    break;
                case 4://parcel
                    Console.WriteLine("Enter the id of the parcel you would like to see:");
                    success = Int32.TryParse(Console.ReadLine(), out choise);
                    foreach (IDAL.DO.Parcel target in DalObject.DataSource.Parcel)
                    {
                        if (target.Id.Equals(choise))
                            Console.WriteLine(target.ToString());
                    }
                    break;
            }
        }
        static public void ListPrint(int choise)
        {
            switch (choise)
            {
                case 1://station
                    Console.WriteLine("Stations:");
                    foreach (IDAL.DO.Station target in DalObject.DataSource.Stations)
                    {
                            Console.WriteLine(target.ToString());
                    }
                    break;
                case 2://drone
                    Console.WriteLine("Drones:");
                    foreach (IDAL.DO.Drone target in DalObject.DataSource.Drones)
                    {
                            Console.WriteLine(target.ToString());
                    }
                    break;
                case 3://customer
                    Console.WriteLine("Customers:");
                    foreach (IDAL.DO.Customer target in DalObject.DataSource.Customer)
                    {
                            Console.WriteLine(target.ToString());
                    }
                    break;
                case 4://parcel
                    Console.WriteLine("Parcels:");
                    foreach (IDAL.DO.Parcel target in DalObject.DataSource.Parcel)
                    {
                            Console.WriteLine(target.ToString());
                    }
                    break;
                case 5://non affiliated parcels
                    foreach (IDAL.DO.Parcel target in DalObject.DataSource.Parcel)
                    {
                        if (target.DroneId == -1)
                            Console.WriteLine(target.ToString() + "\n");
                    }
                    break;
                case 6://stations with free hubs
                    foreach (IDAL.DO.Station target in DalObject.DataSource.Stations)
                    {
                        if (target.ChargeSlots > 0)
                            Console.WriteLine(target.ToString() + "\n");
                    }
                    break;
            }
        }
    }
}
