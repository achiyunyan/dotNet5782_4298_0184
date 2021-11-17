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
                case 7://pick a prcel
                    PickParcelConsole();
                    break;
                case 8://deliver a parcel
                    DeliverParcelConsole();

            }
        }

        private static void UpdateDroneConsole()
        {
            int id;
            Console.WriteLine("Enter Drone Id:");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("Enter Drone new Model:");
            //try
            //{
            //    myBl.UpdateDrone(new Drone()
            //    {

            //    });
            //}
        }
    }
}
