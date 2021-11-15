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
        private static DalObject.DalObject myDal = new DalObject.DalObject();
        private static List<ListDrone> Drones = new List<ListDrone>();
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
            Drones.Add(new ListDrone()
            {
                Id = blDrone.Id,
                Model = blDrone.Model,
                Location = new Location { Latitude = station.Latitude, Longitude = station.Longitude },
                Battery = rand.Next(20, 41),
                State = DroneState.Maintenance,
                WeightCategory = blDrone.WeightCategory,
            });
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
    }
}
