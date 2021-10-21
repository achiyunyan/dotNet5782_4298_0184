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

        private static Random rand = new Random();
        internal static class Config
        {
            private static int parcelNum =0;
 
            internal static void Initialize()
            {
                RandomStations();
                RandomDrones();
                RandomCustomers();
                RandomParcel();
            }
            private static void RandomStations()
            {
                int size = rand.Next(2,6);
                for (int i = 0; i < size; i++)
                {
                    Stations.Add(new Station
                    {
                        Id = rand.Next(10000, 100000),
                        Name = rand.Next(10000, 100000),
                        ChargeSlots = rand.Next(1, 4),
                        lattitude = rand.NextDouble() * (31.803392 - 31.746814) + 31.746814,
                        //to finish...
                    }) ;
                  
                    //31.746814, 35.167912
                    //31.803392, 35.227046
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
