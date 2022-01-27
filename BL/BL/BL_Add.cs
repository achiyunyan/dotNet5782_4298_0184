using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using BlApi;
using System.Runtime.CompilerServices;

namespace BL
{
    public partial class BL : IBL
    {
        #region Data 
        internal static IDal myDal;
        private static List<ListDrone> Drones = new List<ListDrone>();
        internal double ElectricityUsePerKmAvailable;
        private double ElectricityUsePerKmLight;
        private double ElectricityUsePerKmMedium;
        private double ElectricityUsePerKmHeavy;
        internal double ElectricityChargePerSec;
        #endregion

        private static Random rand = new Random();

        #region singleton
        private static readonly IBL instance = new BL();
        public static IBL Instance { get { return instance; } }
        #endregion

        /// <summary>
        /// constractor for BL
        /// </summary>
        private BL()
        {
            myDal = FactoryDal.GetDal();
            IEnumerable<DO.Drone> dalDrones;
            IEnumerable<DO.Parcel> dalParcels;
            lock (myDal)// loads data from dal
            {
                ElectricityUsePerKmAvailable = myDal.GetElectricityUsePerKmAvailable();
                ElectricityUsePerKmLight = myDal.GetElectricityUsePerKmLight();
                ElectricityUsePerKmMedium = myDal.GetElectricityUsePerKmMedium();
                ElectricityUsePerKmHeavy = myDal.GetElectricityUsePerKmHeavy();
                ElectricityChargePerSec = myDal.GetElectricityChargePerSec();

                dalDrones = myDal.GetDronesList();
                dalParcels = myDal.GetParcelsList();
            }

            double battery = default; 
            DroneState state = default;
            int parcelId;
            Location location = default;
            bool deliveryPossible;
            bool maintenancePossible;

            foreach (var drone in dalDrones) // set every drone to bl
            {
                parcelId = 0;
                deliveryPossible = false;
                if (dalParcels.Any(pr => pr.DroneId == drone.Id && pr.Delivered == null)) // in delivery (there is a linked parcel)
                {
                    DO.Parcel dalParcel = dalParcels.First(pr => pr.DroneId == drone.Id && pr.Delivered == null);
                    DO.Customer sender;
                    DO.Customer reciver;
                    lock (myDal)
                    {
                        sender = myDal.GetCustomer(dalParcel.SenderId);
                        reciver = myDal.GetCustomer(dalParcel.ReciverId);
                    }
                    Location reciverLocation = new Location { Latitude = reciver.Latitude, Longitude = reciver.Longitude };
                    Location senderLocation = new Location { Latitude = sender.Latitude, Longitude = sender.Longitude };
                    
                    //calculates the drone location based on its state
                    if (dalParcel.PickedUp == null) //Scheduled -> closest station location
                    {
                        location = ClosestStationLocation(sender.Latitude, sender.Longitude, false);
                    }
                    else //Collected -> sender location
                    {
                        lock (myDal)
                        {
                            location = new Location
                            {
                                Latitude = myDal.GetCustomer(dalParcel.SenderId).Latitude,
                                Longitude = myDal.GetCustomer(dalParcel.SenderId).Longitude
                            };
                        }
                    }

                    //checks if the delivery flight is possible
                    // drone's location (Available) -> sender location (Carrying weight) -> receiver location (Available) -> closest station
                    double deliveryDis = DistanceBetweenTwoPoints(senderLocation, reciverLocation);
                    double disToClosestStation = DistanceBetweenTwoPoints(reciverLocation, ClosestStationLocation(reciverLocation, false));
                    double disToSender = DistanceBetweenTwoPoints(location, senderLocation);
                    double minimum = ElecriciryUsePerWeight(dalParcel.Weight) * deliveryDis + ElectricityUsePerKmAvailable * (disToClosestStation + disToSender);
                    
                    if (minimum > 100)// can't fulfil the mission -> restart the parcel
                    {
                        dalParcel.Scheduled = null;
                        dalParcel.PickedUp = null;
                        lock(myDal)
                        {
                            myDal.UpdateParcel(dalParcel);
                        }
                    }
                    else
                    {
                        deliveryPossible = true;
                        battery = rand.Next((int)Math.Ceiling(minimum), 101);
                        parcelId = dalParcel.Id;
                        state = DroneState.Delivery;
                    }    
                }
                if(!deliveryPossible) // not in delivery
                {
                    maintenancePossible = false;
                    if (rand.Next(0, 2) == 0)// in charge (if there are stations with free charging slots)
                    {
                        IEnumerable<DO.Station> dalStationsWithSlots;
                        lock (myDal)
                        {
                            dalStationsWithSlots = myDal.GetStationsList(st => st.ChargeSlots > 0);
                        }
                        if (dalStationsWithSlots.Any()) // there is a place to charge
                        {
                            maintenancePossible = true; // not available
                            state = DroneState.Maintenance;
                            battery = rand.Next(0, 21);
                            int index = rand.Next(0, dalStationsWithSlots.Count());
                            DO.Station station = dalStationsWithSlots.ElementAt(index);
                            location = new Location { Latitude = station.Latitude, Longitude = station.Longitude };
                            station.ChargeSlots -= 1;
                            lock (myDal)
                            {
                                myDal.UpdateStation(station);
                                myDal.AddDroneCharge(new DO.DroneCharge { DroneId = drone.Id, StationId = dalStationsWithSlots.ElementAt(index).Id });
                            }
                        }
                    }

                    if (!maintenancePossible) // available
                    {                        
                        IEnumerable<DO.Parcel> dalDeliveredParcels = dalParcels.Where(par => par.Delivered != null); // customers that got parcels
                        DO.Customer customer;
                        // picks a random customer that is close enough to a station
                        do
                        {
                            lock (myDal)
                            {
                                DO.Parcel x = dalDeliveredParcels.ElementAt(rand.Next(0, dalDeliveredParcels.Count()));
                                customer = myDal.GetCustomer(x.ReciverId);
                            }
                        } while (DistanceFromClosestStation(customer.Latitude, customer.Longitude) * ElectricityUsePerKmAvailable > 100);

                        state = DroneState.Available;
                        location = new Location { Latitude = customer.Latitude, Longitude = customer.Longitude };
                        battery = rand.Next((int)(DistanceFromClosestStation(customer.Latitude, customer.Longitude) * ElectricityUsePerKmAvailable), 101);
                    }
                }
                // adds the drone do the bl list
                Drones.Add(new ListDrone
                {
                    Id = drone.Id,
                    Battery = battery,
                    Model = drone.Model,
                    Location = location,
                    ParcelId = parcelId,
                    State = state,
                    WeightCategory = (WeightCategory)(int)drone.MaxWeight
                });
            }
        }

        internal double ElecriciryUsePerWeight(WeightCategory weight)
        {
            return ElecriciryUsePerWeight((DO.WeightCategories)(int)weight);
        }

        private double ElecriciryUsePerWeight(DO.WeightCategories weight)
        {
            switch (weight)
            {
                case DO.WeightCategories.Light:
                    return ElectricityUsePerKmLight;
                case DO.WeightCategories.Medium:
                    return ElectricityUsePerKmMedium;
                case DO.WeightCategories.Heavy:
                    return ElectricityUsePerKmHeavy;
            }
            return default;
        }

        internal Location ClosestStationLocation(Location loc, bool withFreeChargeSlots = true)
        {
            return ClosestStationLocation(loc.Latitude, loc.Longitude, withFreeChargeSlots);
        }

        private Location ClosestStationLocation(double lat, double lon, bool withFreeChargeSlots = true)
        {
            IEnumerable<DO.Station> dalStations;
            lock (myDal)
            {
                dalStations = myDal.GetStationsList();
            }
            double dis = double.MaxValue;
            Location location = new Location();
            string name = "";
            foreach (var station in dalStations.Where(station => !withFreeChargeSlots || station.ChargeSlots > 0))
            {
                location.Latitude = station.Latitude;
                location.Longitude = station.Longitude;
                name = station.Name;
                dis = DistanceBetweenTwoPoints(lat, lon, station.Latitude, station.Longitude);
            }
            if (dis == double.MaxValue)
                throw new BlException("No possible to reach stations!");
            return location;
        }

        private double DistanceFromClosestStation(double lat, double lon)
        {
            IEnumerable<DO.Station> dalStations;
            lock (myDal)
            {
                dalStations = myDal.GetStationsList();
            }
            double dis = double.MaxValue;
            foreach (var station in dalStations.Where(station => dis >= DistanceBetweenTwoPoints(lat, lon, station.Latitude, station.Longitude) && station.ChargeSlots > 0))
            {
                dis = DistanceBetweenTwoPoints(lat, lon, station.Latitude, station.Longitude);
            }

            return dis;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station blStation)
        {
            try
            {
                lock (myDal)
                {
                    myDal.AddStation(new DO.Station()
                    {
                        Id = blStation.Id,
                        Name = blStation.Name,
                        ChargeSlots = blStation.FreeChargeSlots,
                        Latitude = blStation.Location.Latitude,
                        Longitude = blStation.Location.Longitude
                    });
                }
            }
            catch (DO.AlreadyExistsException stex)
            {
                string str = "bl ereceive exception: " + stex.Message;
                throw new BlException(str);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone blDrone, int stationId)
        {
            DO.Station station = default;
            try
            {
                lock (myDal)
                {
                    station = myDal.GetStation(stationId);
                }
            }
            catch (DO.NotExistsException stex)
            {
                string str = "bl ereceive exception: " + stex.Message;
                throw new BlException(str);
            }
            DO.Drone dalDrone = new DO.Drone()
            {
                Id = blDrone.Id,
                Model = blDrone.Model,
                MaxWeight = (DO.WeightCategories)blDrone.WeightCategory,
            };
            try
            {
                lock (myDal)
                {
                    myDal.AddDrone(dalDrone);
                }
            }
            catch (DO.AlreadyExistsException stex)
            {
                string str = "bl ereceive exception: " + stex.Message;
                throw new BlException(str);
            }
            Drones.Add(new ListDrone()
            {
                Id = blDrone.Id,
                Model = blDrone.Model,
                Location = new Location { Latitude = station.Latitude, Longitude = station.Longitude },
                Battery = rand.Next(20, 41),
                State = DroneState.Maintenance,
                WeightCategory = blDrone.WeightCategory,
            });
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer blCustomer)
        {
            DO.Customer dalCustomer = new DO.Customer()
            {
                Id = blCustomer.Id,
                Name = blCustomer.Name,
                Phone = blCustomer.Phone,
                Latitude = blCustomer.Location.Latitude,
                Longitude = blCustomer.Location.Longitude
            };
            try
            {
                lock (myDal)
                {
                    myDal.AddCustomer(dalCustomer);
                }
            }
            catch (DO.AlreadyExistsException stex)
            {
                string str = "bl ereceive exception: " + stex.Message;
                throw new BlException(str);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(int senderId, int reciverId, int weight, int priority)
        {
            DO.Parcel dalParcel = new DO.Parcel()
            {
                SenderId = senderId,
                ReciverId = reciverId,
                Weight = (DO.WeightCategories)weight,
                Priority = (DO.Priorities)priority,
                Requested = DateTime.Now,
                Scheduled = null,
                PickedUp = null,
                Delivered = null,
                DroneId = 0
            };
            try
            {
                lock (myDal)
                {
                    myDal.AddParcel(dalParcel);
                }
            }
            catch (DO.AlreadyExistsException stex)
            {
                string str = "bl ereceive exception: " + stex.Message;
                throw new BlException(str);
            }
        }

        internal double DistanceBetweenTwoPoints(Location loc1, Location loc2)
        {
            return DistanceBetweenTwoPoints(loc1.Latitude, loc1.Longitude, loc2.Latitude, loc2.Longitude); 
        }

        private double DistanceBetweenTwoPoints(double lat1, double lon1, double lat2, double lon2)
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            return dist * 1.609344;
        }
    }
}
