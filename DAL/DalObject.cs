using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DalObject
    {

        public DalObject()
        {
            DataSource.Config.Initialize();
        }
        static public Station GetStation()
        {
            int tempId = new int();
            Station targ = new Station();
            Console.WriteLine("Enter the id of the station you would like to see:");
            Int32.TryParse(Console.ReadLine(), out tempId);
            foreach (Station target in DataSource.Stations)
            {
                if (target.Id.Equals(tempId))
                    return target;
            }
            return targ;
        }
        static public Drone GetDrone()
        {
            int tempId = new int();
            Drone targ = new Drone();
            Console.WriteLine("Enter the id of the drone you would like to see:");
            Int32.TryParse(Console.ReadLine(), out tempId);
            foreach (Drone target in DataSource.Drones)
            {
                if (target.Id.Equals(tempId))
                    return target;
            }
            return targ;
        }
        static public Customer GetCustomer()
        {
            int tempId = new int();
            Customer targ = new Customer();
            Console.WriteLine("Enter the id of the customer you would like to see:");
            Int32.TryParse(Console.ReadLine(), out tempId);
            foreach (Customer target in DataSource.Customers)
            {
                if (target.Id.Equals(tempId))
                    return target;
            }
            return targ;
        }
        static public Parcel GetParcel()
        {
            int tempId = new int();
            Parcel targ = new Parcel();
            Console.WriteLine("Enter the id of the parcel you would like to see:");
            Int32.TryParse(Console.ReadLine(), out tempId);
            foreach (Parcel target in DataSource.Parcels)
            {
                if (target.Id.Equals(tempId))
                    Console.WriteLine(target.ToString());
            }
            return targ;
        }

        static public void ListPrint(int choise)
        {
            switch (choise)
            {
                case 1://station
                    Console.WriteLine("Stations:");
                    foreach (IDAL.DO.Station target in DataSource.Stations)
                    {
                        Console.WriteLine(target.ToString());
                    }
                    break;
                case 2://drone
                    Console.WriteLine("Drones:");
                    foreach (IDAL.DO.Drone target in DataSource.Drones)
                    {
                        Console.WriteLine(target.ToString());
                    }
                    break;
                case 3://customer
                    Console.WriteLine("Customers:");
                    foreach (IDAL.DO.Customer target in DataSource.Customers)
                    {
                        Console.WriteLine(target.ToString());
                    }
                    break;
                case 4://parcel
                    Console.WriteLine("Parcels:");
                    foreach (IDAL.DO.Parcel target in DataSource.Parcels)
                    {
                        Console.WriteLine(target.ToString());
                    }
                    break;
                case 5://non affiliated parcels
                    foreach (IDAL.DO.Parcel target in DataSource.Parcels)
                    {
                        if (target.DroneId == -1)
                            Console.WriteLine(target.ToString() + "\n");
                    }
                    break;
                case 6://stations with free hubs
                    foreach (IDAL.DO.Station target in DataSource.Stations)
                    {
                        if (target.ChargeSlots > 0)
                            Console.WriteLine(target.ToString() + "\n");
                    }
                    break;
            }
        }
    }
}
