using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace DS
{
    public class DataSource
    {
        
        internal static List<Drone> Drones = new List<Drone>();
        internal static List<Station> Stations = new List<Station>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal static List<DroneCharge> DroneCharges = new List<DroneCharge>();
        private static Random rand = new Random();
        internal class Config
        {
            #region Data
            internal static double ElectricityChargePerSec = 20;
            internal static double ElectricityUsePerKmAvailable = 5;
            internal static double ElectricityUsePerKmLight = 7;
            internal static double ElectricityUsePerKmMedium = 8;
            internal static double ElectricityUsePerKmHeavy = 9;
            #endregion

            internal static int parcelNum = 1000000; // Parcels running number

            private static string[] names = new string[] { "Adam", "Alex", "Aaron", "Ben", "Charly", "Chase", "Che", "Chester", "Chevy", "Chi", "Chibudom", "Chidera", "Chimsom", "Chin", "Chintu", "Chiqal", "Chiron", "Chris", "Chris-Daniel", "Chrismedi", "Christian", "Christie", "Christoph", "Christopher", "Christopher-Lee", "Christy", "Chu", "Chukwuemeka", "Cian", "Ciann", "Ciar", "Ciaran", "Ciarian", "Cieran", "Cillian", "Cillin", "Cinar", "CJ", "C-Jay", "Clark", "Clarke", "Clayton", "Clement", "Clifford", "Clyde", "Cobain", "Coban", "Coben", "Cobi", "Cobie", "Coby", "Codey", "Codi", "Codie", "Cody", "Cody-Lee", "Coel", "Cohan", "Cohen", "Colby", "Cole", "Colin", "Coll", "Colm", "Colt", "Colton", "Colum", "Dan", "David", "Edward", "Fred", "Frank", "George", "Hal", "Hank", "Ike", "John", "Jack", "Joe", "Larry", "Monte", "Matthew", "Mark", "Nathan", "Otto", "Paul", "Peter", "Roger", "Roger", "Steve", "Thomas", "Tim", "Ty", "Victor", "Walter", "Wiktor", "Wilkie", "Will", "William", "William-John", "Willum", "Wilson", "Windsor", "Wojciech", "Woyenbrakemi", "Wyatt", "Wylie", "Wynn", "Xabier", "Xander", "Xavier", "Xiao", "Xida", "Xin", "Xue", "Yadgor", "Yago", "Yahya", "Yakup", "Yang", "Yanick", "Yann", "Yannick", "Yaseen", "Yasin", "Yasir", "Yassin", "Yoji", "Yong", "Yoolgeun", "Yorgos", "Youcef", "Yousif", "Youssef", "Yu", "Yuanyu", "Yuri", "Yusef", "Yusuf", "Yves", "Zaaine", "Zaak", "Zac", "Zach", "Zachariya", "Zachary", "Zachary-Marc", "Zachery", "Zack", "Zackary", "Zaid", "Zain", "Zaine", "Miguel", "Mika", "Mikael", "Mikee", "Mikey", "Mikhail", "Mikolaj", "Miles", "Millar", "Miller", "Milo", "Milos", "Milosz", "Mir", "Mirza", "Mitch", "Mitchel", "Mitchell", "Moad", "Moayd", "Mobeen", "Modoulamin", "Modu", "Mohamad", "Mohamed", "Mohammad", "Mohammad-Bilal", "Mohammed", "Mohanad", "Mohd", "Momin", "Momooreoluwa", "Montague", "Montgomery", "Monty" };
            private static string[] stationsNames = new string[] { "Rehavia", "Katamon", "Givat Mordechai", "Arnona", "Romema" };
            private static double[] stationsLatitudes = new double[] { 31.773883970410303, 31.761073049323283, 31.762682895985005, 31.747320910723996, 31.791571360711526 };
            private static double[] stationsLongitudes = new double[] { 35.21284491679851, 35.206849163507805, 35.19817109950662, 35.21964424530334, 35.20602291668603 };
            
            
            
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
                int size = rand.Next(2, 6); // number of stations
                for (int i = 0; i < size; i++)
                {
                    int id;
                    do
                    {
                        id = rand.Next(10000, 100000); // random Id
                    } while (Stations.Any(st => st.Id == id)); // checks if the Id doesn't allready exists
                    Stations.Add(new Station
                    {
                        Id = id,
                        Name = stationsNames[i],
                        ChargeSlots = rand.Next(2, 4),
                        Latitude = stationsLatitudes[i],
                        Longitude = stationsLongitudes[i]
                    });
                }
            }

            /// <summary>
            /// Adds between 5 to 10 drones
            /// </summary>
            private static void RandomDrones()
            {
                int size = rand.Next(5, 11); // number of drones
                for (int i = 0; i < size; i++)
                {
                    int id;
                    do
                    {
                        id = rand.Next(10000, 100000);
                    } while (Drones.Any(dr => dr.Id == id));
                    int intMaxWeight = rand.Next(3);
                    Drones.Add(new Drone
                    {
                        Id = id,
                        Model = "EX50" + (intMaxWeight + 1).ToString(),
                        MaxWeight = (WeightCategories)intMaxWeight
                    });
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
                    int id;
                    do
                    {
                        id = rand.Next(100000000,1000000000);
                    } while (Customers.Any(st => st.Id == id));
                    Customers.Add(new Customer
                    {
                        Id = id,
                        Name = names[rand.Next(names.Length)],
                        Phone = $"0{rand.Next(100000000) + 500000000}",
                        // random location in Jerusalem
                        Latitude = rand.NextDouble() * (31.809648051878856 - 31.742227429597634) + 31.742227429597634,
                        Longitude = rand.NextDouble() * (35.22496332365079 - 35.16242159781234) + 35.16242159781234
                    });
                }
            }

            /// <summary>
            /// Adds between 10 to 100 parcels
            /// </summary>            
            private static void RandomParcel()
            {
                int maxDronesInDelivery = rand.Next(Drones.Count - 4, Drones.Count - 2);
                // if not restricted all drones will be in delivery because the big number of parcels
                int size = rand.Next(10, 101);
                int parcelState; // 0 - deliverd , 1 - collected ,2 - scheduled, 3 - created 
                bool NotCreated;

                DateTime now;
                TimeSpan timeSpan1;
                TimeSpan timeSpan2;
                TimeSpan timeSpan3;
                TimeSpan timeSpan4;

                for (int i = 0; i < size; i++)
                {
                    NotCreated = true;
                    while(NotCreated)
                    {
                        parcelState = rand.Next(0, 4);
                        if (parcelState == 3) // new unlinked parcel from the last week
                        {
                            timeSpan1 = new TimeSpan(rand.Next(2), rand.Next(24), rand.Next(60), rand.Next(60));
                            Parcels.Add(new Parcel
                            {
                                Id = parcelNum++,
                                SenderId = Customers[rand.Next(Customers.Count)].Id,
                                ReciverId = Customers[rand.Next(Customers.Count)].Id,
                                Weight = (WeightCategories)rand.Next(3),
                                Priority = (Priorities)rand.Next(3),
                                DroneId = 0,
                                Requested = DateTime.Now - timeSpan1,
                                Scheduled = null,
                                PickedUp = null,
                                Delivered = null
                            });
                            NotCreated = false;                            
                        }

                        else if (parcelState == 2 && maxDronesInDelivery > 0) // adds Scheduled parcel
                        {
                            now = DateTime.Now;
                            timeSpan1 = new TimeSpan(rand.Next(2, 7), rand.Next(24), rand.Next(60), rand.Next(60));
                            timeSpan2 = new TimeSpan(rand.Next(1), rand.Next(24), rand.Next(60), rand.Next(60));
                            int droneId = Drones[rand.Next(Drones.Count)].Id;
                            int intWeight = rand.Next(3);
                            if (!Parcels.Any(st => st.Delivered == null && st.DroneId == droneId) && (int)Drones.First(st => st.Id == droneId).MaxWeight >= intWeight)
                            {
                                Parcels.Add(new Parcel
                                {
                                    Id = parcelNum++,
                                    SenderId = Customers[rand.Next(Customers.Count)].Id,
                                    ReciverId = Customers[rand.Next(Customers.Count)].Id,
                                    Weight = (WeightCategories)intWeight,
                                    Priority = (Priorities)rand.Next(3),
                                    DroneId = droneId,
                                    Requested = now - timeSpan1,
                                    Scheduled = now - timeSpan1 + timeSpan2,
                                    PickedUp = null,
                                    Delivered = null
                                });
                                maxDronesInDelivery--;
                                NotCreated = false;
                            }
                        }

                        else if (parcelState == 1 && maxDronesInDelivery > 0) // collected
                        {
                            now = DateTime.Now;
                            timeSpan1 = new TimeSpan(rand.Next(2, 7), rand.Next(24), rand.Next(60), rand.Next(60));
                            timeSpan2 = new TimeSpan(rand.Next(1), rand.Next(24), rand.Next(60), rand.Next(60));
                            timeSpan3 = new TimeSpan(rand.Next(1), rand.Next(24), rand.Next(60), rand.Next(60));
                            int droneId = Drones[rand.Next(Drones.Count)].Id;
                            int intWeight = rand.Next(3);
                            if (!Parcels.Any(st => st.Delivered == null && st.DroneId == droneId) && (int)Drones.First(st => st.Id == droneId).MaxWeight >= intWeight)
                            {
                                Parcels.Add(new Parcel
                                {
                                    Id = parcelNum++,
                                    SenderId = Customers[rand.Next(Customers.Count)].Id,
                                    ReciverId = Customers[rand.Next(Customers.Count)].Id,
                                    Weight = (WeightCategories)intWeight,
                                    Priority = (Priorities)rand.Next(3),
                                    DroneId = droneId,
                                    Requested = now - timeSpan1,
                                    Scheduled = now - timeSpan1 + timeSpan2,
                                    PickedUp = now - timeSpan1 + timeSpan2 + timeSpan3,
                                    Delivered = null
                                });
                                maxDronesInDelivery--;
                                NotCreated = false;
                            }
                        }

                        else if (parcelState == 0) // new deliverd parcel from two weeks ago up to a week ago
                        {
                            now = DateTime.Now;
                            timeSpan1 = new TimeSpan(rand.Next(7, 14), rand.Next(24), rand.Next(60), rand.Next(60));
                            timeSpan2 = new TimeSpan(rand.Next(1), rand.Next(24), rand.Next(60), rand.Next(60));
                            timeSpan3 = new TimeSpan(rand.Next(1), rand.Next(24), rand.Next(60), rand.Next(60));
                            timeSpan4 = new TimeSpan(rand.Next(1), rand.Next(24), rand.Next(60), rand.Next(60));
                            int droneId = Drones[rand.Next(Drones.Count)].Id;
                            int intWeight = rand.Next(3);
                            if ((int)Drones.First(st => st.Id == droneId).MaxWeight >= intWeight)
                            {
                                Parcels.Add(new Parcel
                                {
                                    Id = parcelNum++,
                                    SenderId = Customers[rand.Next(Customers.Count)].Id,
                                    ReciverId = Customers[rand.Next(Customers.Count)].Id,
                                    Weight = (WeightCategories)intWeight,
                                    Priority = (Priorities)rand.Next(3),
                                    DroneId = droneId,
                                    Requested = now - timeSpan1,
                                    Scheduled = now - timeSpan1 + timeSpan2,
                                    PickedUp = now - timeSpan1 + timeSpan2 + timeSpan3,
                                    Delivered = now - timeSpan1 + timeSpan2 + timeSpan3 + timeSpan4
                                });
                                NotCreated = false;
                            }
                        }
                    }
                }
            }

        }
    }
}
