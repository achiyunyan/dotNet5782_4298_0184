using System;
using BlApi;

namespace ConsoleUI_BL
{
    partial class Program
    {
        static BlApi.IBL myBl = BlFactory.GetBl();
        static void Main(string[] args)
        {
            int choise = new int();
            choise = 0;
            while (choise != 5)
            {
                //Console.Clear();
                Console.WriteLine("Insert the number of the action you would like to commit:\n");
                Console.WriteLine("1.Options of adding\n" +
                                    "2.Options of updating\n" +
                                    "3.Options of display\n" +
                                    "4.Options of lists' displaing\n" +
                                    "5.Destination from point to station/customer\n" +
                                   "6.exit\n");
                choise = InputCheck(6);
                switch (choise)
                {
                    case 1:
                        Adding();
                        break;
                    case 2:
                        Updating();
                        break;
                    case 3:
                        Displaying();
                        break;
                    case 4:
                        ListsView();
                        break;
                    case 5:
                        //DistanceFromCordinate();
                        break;
                    case 6:
                        return;
                }
                Console.WriteLine("\nPress ENTER to continue:");
                Console.ReadLine();
            }
        }
        /// <summary>
        /// right now does nothing,will be relevant later...
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        static int InputCheck(int max)
        {
            int choise = new int();
            do
            {
                bool success = Int32.TryParse(Console.ReadLine(), out choise);
                if (!(choise >= 1 && choise <= max) || !success)
                {
                    Console.WriteLine("Your number must be between 1 to {0}", max);
                }
            } while (!(choise >= 1 && choise <= max));
            return choise;
        }
        /// <summary>
        /// manages the adding menu
        /// </summary>
        static void Adding()
        {
            int choice = new int();
            Console.WriteLine("Insert the number of the action you would like to commit:\n");
            Console.WriteLine("1.Add a base station  \n" +
                              "2.Add a drone \n" +
                              "3.Add a new customer\n" +
                              "4.Add a parcel to delivery\n");
            choice = InputCheck(4);
            AddObject(choice);
        }
        /// <summary>
        /// manages the updating menu
        /// </summary>
        static void Updating()
        {
            int choice = new int();
            Console.WriteLine("Insert the number of the action you would like to commit:\n");
            Console.WriteLine("1.Update a drone:\n"+
                              "2.Update a station:\n"+  
                              "3.Update a customer\n"+
                              "4.Send a drone to charge in a base station\n" +
                              "5.Release a drone from charging in a base station\n"+
                              "6.Link a parcel to a drone \n" +
                              "7.Pick up a parcel with a drone\n" +
                              "8.Suply a parcel to the customer \n" );
            choice = InputCheck(8);
            UpdateObject(choice);
        }
        /// <summary>
        /// manages the object displaying menu
        /// </summary>
        static void Displaying()
        {
            int choice = new int();
            Console.WriteLine("Insert the number of the action you would like to commit:\n");
            Console.WriteLine("1.Displaying a base station \n" +
                              "2.Displaying a drone\n" +
                              "3.Displaying a customer\n" +
                              "4.Displaying a parcel\n");
            choice = InputCheck(4);
            ObjectPrint(choice);
        }
        /// <summary>
        /// manages the lists displaying menu
        /// </summary>
        static void ListsView()
        {
            int choice = new int();
            Console.WriteLine("Insert the number of the list you would like to see:\n");
            Console.WriteLine("1.List of base stations \n" +
                              "2.List of drones\n" +
                              "3.List of the customers \n" +
                              "4.List of parcels \n" +
                              "5.List of non linked parcels\n" +
                              "6.List of stations where there are free charging slots");
            choice = InputCheck(6);
            ListPrint(choice);
        }        
    }
}
