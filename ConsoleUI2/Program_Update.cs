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
        static public void UpdateObject(int choice)
        {
            switch (choice)
            {
                case 1://drone update
                    UpdateDroneConsole();
                    break;
                case 2://station update
                    UpdateStationConsole();
                    break;
                case 3://customer update
                    UpdateCustomerConsole();
                    break;
                case 4://send drone to charge
                    SendDroneToChargeConsole();
                    break;
                case 5://relaese drone from charging
                    DroneReleaseConsole();
                    break;
                case 6://Link a parcel to a drone
                    LinkParcelToDroneConsole();
                    break;
                    /* case 7://pick a prcel
                        PickParcelConsole();
                        break;
                    case 8://deliver a parcel
                        DeliverParcelConsole();*/

            }
        }

        private static void LinkParcelToDroneConsole()
        {

            int id;
            Console.WriteLine("Enter drone id:");
            int.TryParse(Console.ReadLine(), out id);
            try
            {
                myBl.LinkParcelToDroneBL(id);
            }
            catch (BL.BlException exec)
            {
                Console.WriteLine(exec.Message);
            }
        }

        private static void DroneReleaseConsole()
        {
            int id, chargingTime;
            Console.WriteLine("Enter drone id");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("Enter time in charge(hours) :");
            int.TryParse(Console.ReadLine(), out chargingTime);
            try
            {
                myBl.DroneRelease(id, chargingTime);
            }
            catch (BL.BlException exec)
            {
                Console.WriteLine(exec.Message);
            }
        }

        private static void SendDroneToChargeConsole()
        {
            int id;
            Console.WriteLine("Enter drone id");
            int.TryParse(Console.ReadLine(), out id);
            try
            {
                myBl.SendDroneToCharge(id);
            }
            catch (BL.BlException exec)
            {
                Console.WriteLine(exec.Message);
            }
        }

        private static void UpdateCustomerConsole()
        {
            int id;
            string name, phone;
            Console.WriteLine("Enter Customer Id:");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("Enter name:");
            name = Console.ReadLine();
            Console.WriteLine("Enter phone:");
            phone = Console.ReadLine();
            try
            {
                myBl.UpdateCustomer(id, name, phone);
            }
            catch (BL.BlException exec)
            {
                Console.WriteLine(exec.Message);
            }
        }

        private static void UpdateStationConsole()
        {
            int id, chargingSlots;
            Console.WriteLine("Enter station Id:");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter charging slots:");
            int.TryParse(Console.ReadLine(), out chargingSlots);
            try
            {
                myBl.UpdateStation(id, name, chargingSlots);
            }
            catch (BL.BlException exec)
            {
                Console.WriteLine(exec.Message);
            }
        }

        private static void UpdateDroneConsole()
        {
            int id;
            Console.WriteLine("Enter Drone Id:");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("Enter Drone new Model:");
            try
            {
                myBl.UpdateDrone(id, Console.ReadLine());
            }
            catch (BL.BlException exec)
            {
                Console.WriteLine(exec.Message);
            }
        }

    }
}
