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
        public Station GetStation(int id)
        {
            IDAL.DO.Station dalStation;
            try
            {
                dalStation = myDal.GetStation(id);
            }
            catch (IDAL.DO.NotExistsException stex)
            {
                string str = "bl ereceive exception: " + stex.Message;
                throw new BlException(str);
            }
            Station station = new Station
            {
                Id = dalStation.Id,
                Name = dalStation.Name,
                FreeChargeSlots = dalStation.ChargeSlots,
                Location = new Location { Latitude = dalStation.Latitude, Longitude = dalStation.Longitude }
            };
            foreach(var drone in Drones)
            {
                if (drone.State == DroneState.Maintenance && drone.Location == station.Location)
                    station.DroneList.Add(new DroneInCharge { Id = drone.Id, Battery = drone.Battery });
            }
            return station;
        }

        public Drone GetDrone(int id)
        {
            ListDrone listDrone;
            try
            {
                listDrone = Drones.Find(dr => dr.Id == id);
            }
            catch (ArgumentNullException stex)
            {
                string str = "bl ereceive exception: " + stex.Message;
                throw new BlException(str);
            }
            Drone drone = new Drone
            {
                Id = listDrone.Id,
                State = listDrone.State,
                Battery = listDrone.Battery,
                Location = listDrone.Location,
                Model = listDrone.Model,
                WeightCategory = listDrone.WeightCategory,
                Parcel = GetParcelInTransit(listDrone.ParcelId)
            };
            return drone;
        }

        private ParcelInTransit GetParcelInTransit(int id)
        {
            IDAL.DO.Parcel dalParcel;
            try
            {
                dalParcel = myDal.GetParcel(id);
            }
            catch (IDAL.DO.NotExistsException)
            {
                return default;
            }
            IDAL.DO.Customer reciver = myDal.GetCustomer(dalParcel.ReciverId);
            IDAL.DO.Customer sender = myDal.GetCustomer(dalParcel.SenderId);
            ParcelInTransit parcelInTransit = new ParcelInTransit
            {
                Id = dalParcel.Id,
                WeightCategory = (WeightCategory)(int)dalParcel.Weight,
                Distance = myDal.DistanceBetweenTwoPoints(sender.Latitude, sender.Longitude, reciver.Latitude, reciver.Longitude),
                PickUp = new Location { Latitude = sender.Latitude, Longitude = sender.Longitude },
                Destination = new Location { Latitude = reciver.Latitude, Longitude = reciver.Longitude },
                Priority = (Priority)(int)dalParcel.Priority,
                State = dalParcel.PickedUp == DateTime.MinValue,
                Sender = new CustomerInParcel { Id = sender.Id, Name = sender.Name },
                Receiver = new CustomerInParcel { Id = reciver.Id, Name = reciver.Name }
            };
            return parcelInTransit;
        }
        
        
    }
}