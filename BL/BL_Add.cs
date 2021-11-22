using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BL
{
    public partial class BL : IBL.IBL
    {
        private static Random rand = new Random();
        private DalObject.DalObject myDal;
        private static List<ListDrone> Drones = new List<ListDrone>();
        private double ElectricityUsePerKmAvailable;
        private double ElectricityUsePerKmLight;
        private double ElectricityUsePerKmMedium;
        private double ElectricityUsePerKmHeavy;
        private double ElectricityChargePerHour;

        public BL()
        {
            myDal = new DalObject.DalObject();

            ElectricityUsePerKmAvailable = myDal.GetElectricityUsePerKmAvailable();
            ElectricityUsePerKmLight = myDal.GetElectricityUsePerKmLight();
            ElectricityUsePerKmMedium = myDal.GetElectricityUsePerKmMedium();
            ElectricityUsePerKmHeavy = myDal.GetElectricityUsePerKmHeavy();
            ElectricityChargePerHour = myDal.GetElectricityChargePerHour();

            List<IDAL.DO.Drone> dalDrones = (List<IDAL.DO.Drone>)myDal.GetDronesList();
            List<IDAL.DO.Parcel> dalParcels = (List<IDAL.DO.Parcel>)myDal.GetParcelsList();
            double battery = default;
            DroneState state = default;
            int parcelId;
            Location location = default;
            bool isAvaliable = default;

            foreach (var drone in dalDrones)
            {
                parcelId = 0;
                if (dalParcels.Any(pr => pr.DroneId == drone.Id && pr.Delivered == DateTime.MinValue)) // in delivery
                {
                    IDAL.DO.Parcel dalParcel = dalParcels.First(pr => pr.DroneId == drone.Id && pr.Delivered == DateTime.MinValue);
                    state = DroneState.Delivery;
                    IDAL.DO.Customer sender = myDal.GetCustomer(dalParcel.SenderId);
                    IDAL.DO.Customer reciver = myDal.GetCustomer(dalParcel.ReciverId);
                    double dis = DistanceBetweenTwoPoints(sender.Latitude, sender.Longitude, reciver.Latitude, reciver.Longitude);
                    parcelId = dalParcel.Id;

                    if (dalParcel.PickedUp == DateTime.MinValue)
                    {
                        location = ClosestStationLocation(sender.Latitude, sender.Longitude);
                    }
                    else
                    {
                        location = new Location
                        {
                            Latitude = myDal.GetCustomer(dalParcel.SenderId).Latitude,
                            Longitude = myDal.GetCustomer(dalParcel.SenderId).Longitude
                        };
                    }
                    battery = rand.Next((int)(ElecriciryUsePerWeight(dalParcel.Weight) * dis), 101);
                }
                else // not in delivery
                {
                    isAvaliable = true;
                    if (rand.Next(0, 2) == 0)// in charge
                    {
                        List<IDAL.DO.Station> dalStationsWithSlots = ((List<IDAL.DO.Station>)myDal.GetStationsList()).FindAll(st => st.ChargeSlots > 0);
                        if (dalStationsWithSlots.Count != 0)
                        {
                            isAvaliable = false;
                            state = DroneState.Maintenance;
                            battery = rand.Next(0, 21);
                            int index = rand.Next(0, dalStationsWithSlots.Count);
                            location = new Location { Latitude = dalStationsWithSlots[index].Latitude, Longitude = dalStationsWithSlots[index].Longitude };
                            myDal.AddDroneCharge(new IDAL.DO.DroneCharge { DroneId = drone.Id, StationId = dalStationsWithSlots[index].Id });
                        }
                    }

                    if(isAvaliable) // available
                    {
                        state = DroneState.Available;
                        List<IDAL.DO.Parcel> dalDeliveredParcels = dalParcels.FindAll(par => par.Delivered != DateTime.MinValue);
                        IDAL.DO.Customer customer = myDal.GetCustomer(dalDeliveredParcels[rand.Next(0, dalDeliveredParcels.Count)].ReciverId);
                        location = new Location { Latitude = customer.Latitude, Longitude = customer.Longitude };
                        battery = rand.Next((int)(DistanceFromClosestStation(customer.Latitude, customer.Longitude) * ElectricityUsePerKmAvailable), 101);
                    }
                }
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

        private double ElecriciryUsePerWeight(IDAL.DO.WeightCategories weight)
        {
            switch (weight)
            {
                case IDAL.DO.WeightCategories.Light:
                    return ElectricityUsePerKmLight;
                case IDAL.DO.WeightCategories.Medium:
                    return ElectricityUsePerKmMedium;
                case IDAL.DO.WeightCategories.Heavy:
                    return ElectricityUsePerKmHeavy;
            }
            return default;
        }

        private Location ClosestStationLocation(Location loc)
        {
            return ClosestStationLocation(loc.Latitude, loc.Longitude);
        }

        private Location ClosestStationLocation(double lat, double lon)
        {
            List<IDAL.DO.Station> dalStations = (List<IDAL.DO.Station>)myDal.GetStationsList();
            double dis = double.MaxValue;
            Location location = new Location();
            foreach (var station in dalStations)
            {
                if (dis >= DistanceBetweenTwoPoints(lat, lon, station.Latitude, station.Longitude) && station.ChargeSlots > 0)
                {
                    location.Latitude = station.Latitude;
                    location.Longitude = station.Longitude;
                    dis = DistanceBetweenTwoPoints(lat, lon, station.Latitude, station.Longitude);
                }
            }
            return location;
        }

        private double DistanceFromClosestStation(double lat, double lon)
        {
            List<IDAL.DO.Station> dalStations = (List<IDAL.DO.Station>)myDal.GetStationsList();
            double dis = double.MaxValue;
            foreach (var station in dalStations)
            {
                if (dis >= DistanceBetweenTwoPoints(lat, lon, station.Latitude, station.Longitude) && station.ChargeSlots > 0)
                {
                    dis = DistanceBetweenTwoPoints(lat, lon, station.Latitude, station.Longitude);
                }
            }
            return dis;
        }

        public void AddStation(Station blStation)
        {
            try
            {
                myDal.AddStation(new IDAL.DO.Station()
                {
                    Id = blStation.Id,
                    Name = blStation.Name,
                    ChargeSlots = blStation.FreeChargeSlots,
                    Latitude = blStation.Location.Latitude,
                    Longitude = blStation.Location.Longitude
                });
            }
            catch (IDAL.DO.AlreadyExistsException stex)
            {
                string str = "bl ereceive exception: " + stex.Message;
                throw new BlException(str);
            }
        }
        public void AddDrone(Drone blDrone, int stationId)
        {
            IDAL.DO.Station station = default;
            try
            {
                station = myDal.GetStation(stationId);
            }
            catch (IDAL.DO.NotExistsException stex)
            {
                string str = "bl ereceive exception: " + stex.Message;
                throw new BlException(str);
            }
            IDAL.DO.Drone dalDrone = new IDAL.DO.Drone()
            {
                Id = blDrone.Id,
                Model = blDrone.Model,
                MaxWeight = (IDAL.DO.WeightCategories)blDrone.WeightCategory,
            };
            try
            {
                myDal.AddDrone(dalDrone);
            }
            catch (IDAL.DO.AlreadyExistsException stex)
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
        public void AddCustomer(Customer blCustomer)
        {
            IDAL.DO.Customer dalCustomer = new IDAL.DO.Customer()
            {
                Id = blCustomer.Id,
                Name = blCustomer.Name,
                Phone = blCustomer.Phone,
                Latitude = blCustomer.Location.Latitude,
                Longitude = blCustomer.Location.Longitude
            };
            try
            {
                myDal.AddCustomer(dalCustomer);
            }
            catch (IDAL.DO.AlreadyExistsException stex)
            {
                string str = "bl ereceive exception: " + stex.Message;
                throw new BlException(str);
            }
        }

        public void AddParcel(int senderId, int reciverId, int weight, int priority)
        {
            IDAL.DO.Parcel dalParcel = new IDAL.DO.Parcel()
            {
                SenderId = senderId,
                ReciverId = reciverId,
                Weight = (IDAL.DO.WeightCategories)weight,
                Priority = (IDAL.DO.Priorities)priority,
                Requested = DateTime.Now,
                Scheduled = DateTime.MinValue,
                PickedUp = DateTime.MinValue,
                Delivered = DateTime.MinValue,
                DroneId = 0
            };
            try
            {
                myDal.AddParcel(dalParcel);
            }
            catch (IDAL.DO.AlreadyExistsException stex)
            {
                string str = "bl ereceive exception: " + stex.Message;
                throw new BlException(str);
            }
        }

        private double DistanceBetweenTwoPoints(Location loc1, Location loc2)
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
