using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DataSource
    {
        internal static List<Drone> Drones = new List<Drone>(10);
        internal static List<Station> Stations = new List<Station>(5);
        internal static List<Customer> Customers = new List<Customer>(100);
        internal static List<Parcel> Parcels = new List<Parcel>(1000);

        internal class Config
        {
            private static int parcelNum;
            internal Config() { parcelNum = 0; }
            internal static void Initialize()
            {
                RandomStations();
                RandomDrones();
                RandomCustomers();
                RandomParcel();
            }
            private static void RandomStations()
            {
                Random rand = new Random();
                int size = new int();
                size = rand.Next(2,6);
                for (int i = 0; i < size; i++)
                {
                    Stations.Add(new Station
                    {
                        Id = rand.Next(10000,100000),
                        Name = rand.Next(10000,100000),
                        ChargeSlots = rand.Next(1,4)
                    });
                }
            }
            private static void RandomDrones()
            {
                Random rand = new Random();
                int size = new int();
                size = rand.Next(5,11);
                for (int i = 0; i < size; i++)
                {
                    int intMaxWeight = rand.Next(3);
                    DroneStatus status = new DroneStatus();
                    int id = new int();
                    id = rand.Next(10000, 100000);
                    status = (DroneStatus)rand.Next(3);
                    Drones.Add(new Drone
                    {
                        Id = id,
                        Model = "EX50" + (intMaxWeight + 1).ToString(),
                        Battery = (rand.Next(101)),
                        MaxWeight = (WeightCategories)intMaxWeight,
                        Status = status
                    });
                    if (status == DroneStatus.delivery)
                        Parcels.Add(new Parcel
                        {
                            Id = parcelNum++,
                            SenderId = Customers[rand.Next(Customers.Count)].Id,
                            TargetId = Customers[rand.Next(Customers.Count)].Id,
                            Weight = (WeightCategories)rand.Next(intMaxWeight),
                            Priority = (Priorities)rand.Next(3),
                            DroneId = id,
                            Requested = DateTime.Now,
                        });
                }
            }
            private static void RandomCustomers()
            {
                Random rand = new Random();
                int size = new int();
                size = rand.Next(10,100);
                string []names = { "Adam", "Alex", "Aaron", "Ben", "Carl", "Dan", "David", "Edward", "Fred", "Frank", "George", "Hal", "Hank", "Ike", "John", "Jack", "Joe", "Larry", "Monte", "Matthew", "Mark", "Nathan", "Otto", "Paul", "Peter", "Roger", "Roger", "Steve", "Thomas", "Tim", "Ty", "Victor", "Walter", "Wiktor", "Wilkie", "Will", "William", "William-John", "Willum", "Wilson", "Windsor", "Wojciech", "Woyenbrakemi", "Wyatt", "Wylie", "Wynn", "Xabier", "Xander", "Xavier", "Xiao", "Xida", "Xin", "Xue", "Yadgor", "Yago", "Yahya", "Yakup", "Yang", "Yanick", "Yann", "Yannick", "Yaseen", "Yasin", "Yasir", "Yassin", "Yoji", "Yong", "Yoolgeun", "Yorgos", "Youcef", "Yousif", "Youssef", "Yu", "Yuanyu", "Yuri", "Yusef", "Yusuf", "Yves", "Zaaine", "Zaak", "Zac", "Zach", "Zachariya", "Zachary", "Zachary-Marc", "Zachery", "Zack", "Zackary", "Zaid", "Zain", "Zaine", "Miguel", "Mika", "Mikael", "Mikee", "Mikey", "Mikhail", "Mikolaj", "Miles", "Millar", "Miller", "Milo", "Milos", "Milosz", "Mir", "Mirza", "Mitch", "Mitchel", "Mitchell", "Moad", "Moayd", "Mobeen", "Modoulamin", "Modu", "Mohamad", "Mohamed", "Mohammad", "Mohammad-Bilal", "Mohammed", "Mohanad", "Mohd", "Momin", "Momooreoluwa", "Montague", "Montgomery", "Monty" };
                for (int i = 0; i < size; i++)
                {
                    Customers.Add(new Customer
                    {
                        Id = rand.Next(1000000000),
                        Name = names[rand.Next(names.Length)],
                        Phone = "0" + (rand.Next(100000000) + 5000000000 ).ToString(),
                    });
                }
            }

            private static void RandomParcel()
            {
                Random rand = new Random();
                int size = new int();
                size = rand.Next(10, 1000);
                for (int i = Parcels.Count; i < size; i++)
                {
                    Parcels.Add(new Parcel
                    {
                        Id = parcelNum++,
                        SenderId = Customers[rand.Next(Customers.Count)].Id,
                        TargetId = Customers[rand.Next(Customers.Count)].Id,
                        Weight = (WeightCategories)rand.Next(3),
                        Priority = (Priorities)rand.Next(3),
                        DroneId = 0,
                        Requested = DateTime.Now,
                    });
                }
            }

        }
    }
}
