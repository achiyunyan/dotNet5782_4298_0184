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
        static private void ObjectPrint(int choice)
        {
            try
            {
                int id;
                switch (choice)
                {
                    case 1: // base station displaying
                        Console.WriteLine("Enter base station Id: \n");
                        int.TryParse(Console.ReadLine(),out id);
                        Console.WriteLine(myBl.GetStation(id));
                        break;
                    case 2: // drone displaying
                        Console.WriteLine("Enter drone Id: \n");
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(myBl.GetDrone(id));
                        break;
                    case 3: // customer displaying
                        Console.WriteLine("Enter customer Id: \n");
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(myBl.GetCustomer(id));
                        break;
                    case 4: // parcel displaying
                        Console.WriteLine("Enter parcel Id: \n");
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(myBl.GetParcel(id));
                        break;
                    
                }
            }
            catch (BL.BlException exem)
            {
                Console.WriteLine(exem.Message);
            }
        }

        static private void ListPrint(int choice)
        {
            switch(choice)
            {
                case 1: // stations
                    Console.WriteLine("Stations:\n\n");
                    foreach(var station in myBl.GetStationsList())
                    {
                        Console.WriteLine(station.ToString() + '\n');
                    }
                    break;
                case 2: // drones
                    Console.WriteLine("Drones:\n\n");
                    foreach (var drone in myBl.GetDronesList())
                    {
                        Console.WriteLine(drone.ToString() + '\n');
                    }
                    break;
                case 3: // customers
                    Console.WriteLine("Customers:\n\n");
                    foreach (var customer in myBl.GetCustomersList())
                    {
                        Console.WriteLine(customer.ToString() + '\n');
                    }
                    break;
                case 4: // parcels
                    Console.WriteLine("Parcels:\n\n");
                    foreach (var parcel in myBl.GetParcelsList())
                    {
                        Console.WriteLine(parcel.ToString() + '\n');
                    }
                    break;
            }
        }
    }
}
