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
        static public void ObjectPrint(int choise)
        {
            switch (choise)
            {
                case 1://station
                    Console.WriteLine(DalObject.DalObject.GetStation().ToString());
                    break;
                case 2://drone
                    Console.WriteLine(DalObject.DalObject.GetDrone().ToString());
                    break;
                case 3://customer
                    Console.WriteLine(DalObject.DalObject.GetCustomer().ToString());
                    break;
                case 4://parcel
                    Console.WriteLine(DalObject.DalObject.GetParcel().ToString());
                    break;
            }
        }


    }
}
