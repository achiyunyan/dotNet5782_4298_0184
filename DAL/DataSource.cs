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
        internal static List<Drone> Drones = new List<Drone>();
        internal static List<Station> Stations = new List<Station>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal static List<DroneCharge> DroneCharges = new List<DroneCharge>();
        private static Random rand = new Random();
        internal static class Config
        {
            private static int parcelNum = 1000000;
            private static string[] names = new string[] { "Adam", "Alex", "Aaron", "Ben", "Charly", "Chase", "Che", "Chester", "Chevy", "Chi", "Chibudom", "Chidera", "Chimsom", "Chin", "Chintu", "Chiqal", "Chiron", "Chris", "Chris-Daniel", "Chrismedi", "Christian", "Christie", "Christoph", "Christopher", "Christopher-Lee", "Christy", "Chu", "Chukwuemeka", "Cian", "Ciann", "Ciar", "Ciaran", "Ciarian", "Cieran", "Cillian", "Cillin", "Cinar", "CJ", "C-Jay", "Clark", "Clarke", "Clayton", "Clement", "Clifford", "Clyde", "Cobain", "Coban", "Coben", "Cobi", "Cobie", "Coby", "Codey", "Codi", "Codie", "Cody", "Cody-Lee", "Coel", "Cohan", "Cohen", "Colby", "Cole", "Colin", "Coll", "Colm", "Colt", "Colton", "Colum", "Dan", "David", "Edward", "Fred", "Frank", "George", "Hal", "Hank", "Ike", "John", "Jack", "Joe", "Larry", "Monte", "Matthew", "Mark", "Nathan", "Otto", "Paul", "Peter", "Roger", "Roger", "Steve", "Thomas", "Tim", "Ty", "Victor", "Walter", "Wiktor", "Wilkie", "Will", "William", "William-John", "Willum", "Wilson", "Windsor", "Wojciech", "Woyenbrakemi", "Wyatt", "Wylie", "Wynn", "Xabier", "Xander", "Xavier", "Xiao", "Xida", "Xin", "Xue", "Yadgor", "Yago", "Yahya", "Yakup", "Yang", "Yanick", "Yann", "Yannick", "Yaseen", "Yasin", "Yasir", "Yassin", "Yoji", "Yong", "Yoolgeun", "Yorgos", "Youcef", "Yousif", "Youssef", "Yu", "Yuanyu", "Yuri", "Yusef", "Yusuf", "Yves", "Zaaine", "Zaak", "Zac", "Zach", "Zachariya", "Zachary", "Zachary-Marc", "Zachery", "Zack", "Zackary", "Zaid", "Zain", "Zaine", "Miguel", "Mika", "Mikael", "Mikee", "Mikey", "Mikhail", "Mikolaj", "Miles", "Millar", "Miller", "Milo", "Milos", "Milosz", "Mir", "Mirza", "Mitch", "Mitchel", "Mitchell", "Moad", "Moayd", "Mobeen", "Modoulamin", "Modu", "Mohamad", "Mohamed", "Mohammad", "Mohammad-Bilal", "Mohammed", "Mohanad", "Mohd", "Momin", "Momooreoluwa", "Montague", "Montgomery", "Monty" };
            private static string[] stationsNames = new string[] { "Rehavia", "Katamon", "Givat Mordechai", "Arnona", "Romema" };
            private static double[] latitudes = new double[] { 31.773883970410303, 31.761073049323283, 31.762682895985005, 31.747320910723996, 31.791571360711526 };
            private static double[] longitudes = new double[] { 35.21284491679851, 35.206849163507805, 35.19817109950662, 35.21964424530334, 35.20602291668603 };
            internal static void Initialize()
            {
                RandomCustomers();
                RandomStations();
                RandomDrones();
                RandomParcel();
            }

            /// <summary>
            /// Adds between 2 to 5 stations
            /// </summary>
            private static void RandomStations() 
            {
                int size = rand.Next(2, 6);
                for (int i = 0; i < size; i++)
                {
                    Stations.Add(new Station
                    {
                        Id = rand.Next(10000, 100000),
                        Name = stationsNames[i],
                        ChargeSlots = rand.Next(2, 4),
                        Latitude = latitudes[i],
                        Longitude = longitudes[i]
                    });
                }
            }
              
            /// <summary>
            /// Adds between 5 to 10 drones
            /// </summary>
            private static void RandomDrones()
            {
                int size = rand.Next(5, 11);
                for (int i = 0; i < size; i++)
                {
                    int intMaxWeight = rand.Next(3);
                    DroneStatus status = new DroneStatus();
                    int id = new int();
                    id = rand.Next(10000, 100000);
                    int battery = (rand.Next(101));
                    if (battery < 20) // If the battery is low puts the drone in charging
                    {
                        status = DroneStatus.maintenance;
                        DroneCharges.Add(new DroneCharge
                        {
                            DroneId = id,
                            StationId = Stations[rand.Next(Stations.Count)].Id
                        });
                    }
                    else if (rand.Next(2) == 0)
                        status = DroneStatus.available;
                    else
                        status = DroneStatus.delivery;
                    Drones.Add(new Drone
                    {
                        Id = id,
                        Model = "EX50" + (intMaxWeight + 1).ToString(),
                        Battery = battery,
                        MaxWeight = (WeightCategories)intMaxWeight,
                        Status = status
                    });
                    
                    // Adds a linked parcel to the drone if it is in delivery
                    if (status == DroneStatus.delivery)
                    {
                        DateTime now = new DateTime();
                        now = DateTime.Now;
                        TimeSpan timeSpan1 = new TimeSpan(rand.Next(2, 4), rand.Next(24), rand.Next(60), rand.Next(60));
                        TimeSpan timeSpan2 = new TimeSpan(rand.Next(1), rand.Next(24), rand.Next(60), rand.Next(60));
                        Parcels.Add(new Parcel
                        {
                            Id = parcelNum++,
                            SenderId = Customers[rand.Next(Customers.Count)].Id,
                            TargetId = Customers[rand.Next(Customers.Count)].Id,
                            Weight = (WeightCategories)rand.Next(intMaxWeight),
                            Priority = (Priorities)rand.Next(3),
                            DroneId = id,
                            Requested = now - timeSpan1,
                            Scheduled = (rand.Next(2) == 0) ? DateTime.MinValue : now - timeSpan1 + timeSpan2,
                            Delivered = DateTime.MinValue,
                            PickedUp = DateTime.MinValue
                        });
                    }
                }
            }

            /// <summary>
            /// Adds between 10 to 100 customers
            /// </summary>
            private static void RandomCustomers()
            {
                int size = rand.Next(10, 100);
                for (int i = 0; i < size; i++)
                {
                    Customers.Add(new Customer
                    {
                        Id = rand.Next(1000000000),
                        Name = names[rand.Next(names.Length)],
                        Phone = $"0{rand.Next(1000000000) + 5000000000}",
                        Latitude = rand.NextDouble() * (31.809648051878856 - 31.742227429597634) + 31.742227429597634,
                        Longitude = rand.NextDouble() * (35.22496332365079 - 35.16242159781234) + 35.16242159781234
                    });
                }
            }

            /// <summary>
            /// Adds between 10 to 1000 parcels
            /// </summary>
            private static void RandomParcel() 
            {
                int size = rand.Next(10, 1001);
                for (int i = Parcels.Count; i < size; i++)
                {
                    if (rand.Next(0, 5) != 0) // new unlinked parcel from the last week
                    {
                        TimeSpan timeSpan = new TimeSpan(rand.Next(7), rand.Next(24), rand.Next(60), rand.Next(60));
                        Parcels.Add(new Parcel
                        {
                            Id = parcelNum++,
                            SenderId = Customers[rand.Next(Customers.Count)].Id,
                            TargetId = Customers[rand.Next(Customers.Count)].Id,
                            Weight = (WeightCategories)rand.Next(3),
                            Priority = (Priorities)rand.Next(3),
                            DroneId = 0,
                            Requested = DateTime.Now - timeSpan,
                            Scheduled = DateTime.MinValue,
                            Delivered = DateTime.MinValue,
                            PickedUp = DateTime.MinValue
                        });
                    }
                    else // new deliverd parcel from two weeks ago up to a week ago
                    {
                        DateTime now = DateTime.Now;
                        TimeSpan timeSpan1 = new TimeSpan(rand.Next(7, 14), rand.Next(24), rand.Next(60), rand.Next(60));
                        TimeSpan timeSpan2 = new TimeSpan(rand.Next(1), rand.Next(24), rand.Next(60), rand.Next(60));
                        TimeSpan timeSpan3 = new TimeSpan(rand.Next(1), rand.Next(24), rand.Next(60), rand.Next(60));
                        TimeSpan timeSpan4 = new TimeSpan(rand.Next(1), rand.Next(24), rand.Next(60), rand.Next(60));
                        Parcels.Add(new Parcel
                        {
                            Id = parcelNum++,
                            SenderId = Customers[rand.Next(Customers.Count)].Id,
                            TargetId = Customers[rand.Next(Customers.Count)].Id,
                            Weight = (WeightCategories)rand.Next(3),
                            Priority = (Priorities)rand.Next(3),
                            DroneId = Drones[rand.Next(Drones.Count)].Id,
                            Requested = now - timeSpan1,
                            Scheduled = now - timeSpan1 + timeSpan2,
                            Delivered = now - timeSpan1 + timeSpan2 + timeSpan3,
                            PickedUp = now - timeSpan1 + timeSpan2 + timeSpan3 + timeSpan4
                        });
                    }
                }
            }

        }
    }
}
