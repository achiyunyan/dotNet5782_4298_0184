//using IDAL.DO;
using System;
/*using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace ConsoleUI
{
    partial class Program
    {
        static void Main(string[] args)
        {
            int choise = new int();
            choise = 0;
            while (choise != 5)
            {
                Console.Clear();
                Console.WriteLine("Insert the number of the action you would like to commit:\n");
                Console.WriteLine("1.Options of adding \n" +
                                  "2.Options of updating\n" +
                                  "3.Options of display\n" +
                                  "4.options of lists' displaing\n" +
                                  "5.exit\n");
                choise = InputCheck(5);
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
                       break;
                }
            }

        }
        static int InputCheck(int max)
        {
            int choise = new int();
            do
            {
                bool success = Int32.TryParse(Console.ReadLine(), out choise);
                if (!(choise >= 1 && choise <= max)||!success)
                {
                    Console.WriteLine("Your number must be between 1 to {0}",max);
                }
            } while (!(choise >= 1 && choise <= max));
            return choise;
        }
        static void Adding()
        {
            int choice = new int();
            Console.WriteLine("Insert the number of the action you would like to commit:\n");
            Console.WriteLine("1.Add a base station to the stations' list \n" +
                              "2.Add a drone to the drones' list\n" +
                              "3.Add a new customer\n" +
                              "4.Add a parcel to delivery\n");
            choice = InputCheck(4);
            //add the results of choose and funcs so it will actualy do something...
        }
        static void Updating()
        {
            int choice = new int();
            Console.WriteLine("Insert the number of the action you would like to commit:\n");
            Console.WriteLine("1.Affiliating a parcel to a drone \n" +
                              "2.Pick up a parcel with a drone\n" +
                              "3.Suplying a parcel to the customer \n" +
                              "4.Sending a drone to charge in a base station\n" +
                              "5.Releasing a drone from charging in a base station\n");
            choice = InputCheck(5);
            //add the results of choose and funcs so it will actualy do something...
        }
        static void Displaying()
        {
            int choice = new int();
            Console.WriteLine("Insert the number of the action you would like to commit:\n");
            Console.WriteLine("1.Displaying a base station \n" +
                              "2.Displaying a drone\n" +
                              "3.Displaying a customer\n" +
                              "4.Displaying a parcel\n");
            choice = InputCheck(4);
            //add the results of choose and funcs so it will actualy do something...
        }
        static void ListsView()
        {
            int choice = new int();
            Console.WriteLine("Insert the number of the list you would like to see:\n");
            Console.WriteLine("1.List of base stations \n" +
                              "2.List of drones\n" +
                              "3.List of the customers \n" +
                              "4.List of parcels \n" +
                              "5.List of non affiliated parcels\n"+
                              "6.List of stations where there are free charging hubs");
            choice = InputCheck(6);
            //ListsPrint(choice);
        }
    }
}