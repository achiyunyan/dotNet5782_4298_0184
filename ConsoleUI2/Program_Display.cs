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
    }
}
