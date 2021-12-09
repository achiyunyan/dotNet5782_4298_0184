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
                if (drone.State == DroneState.Maintenance && drone.Location.Latitude == station.Location.Latitude && drone.Location.Longitude == station.Location.Longitude)
                    station.DroneList.Add(new DroneInCharge { Id = drone.Id, Battery = drone.Battery });
            }
            return station;
        }

        public Drone GetDrone(int id)
        {
            ListDrone listDrone;
            listDrone = Drones.Find(dr => dr.Id == id);            
            if (listDrone == default)
            {
                throw new BlException($"id: {id} not exists!!");
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

        public Customer GetCustomer(int id)
        {
            IDAL.DO.Customer dalCustomer;
            try
            {
                dalCustomer = myDal.GetCustomer(id);
            }
            catch (IDAL.DO.NotExistsException stex)
            {
                string str = "bl ereceive exception: " + stex.Message;
                throw new BlException(str);
            }
            Customer customer = new Customer
            {
                Id = dalCustomer.Id,
                Name = dalCustomer.Name,
                Phone = dalCustomer.Phone,
                Location = new Location { Latitude = dalCustomer.Latitude, Longitude = dalCustomer.Longitude }         
            };
            List<IDAL.DO.Parcel> inParcels = ((List<IDAL.DO.Parcel>)myDal.GetParcelsList()).FindAll(pr => pr.ReciverId == dalCustomer.Id);
            List<IDAL.DO.Parcel> outParcels = ((List<IDAL.DO.Parcel>)myDal.GetParcelsList()).FindAll(pr => pr.SenderId == dalCustomer.Id);
            foreach(var parcel in inParcels)
            {
                customer.InDeliveries.Add(new ParcelInCustomer
                {
                    Id = parcel.Id,
                    Priority = (Priority)(int)parcel.Priority,
                    WeightCategory = (WeightCategory)(int)parcel.Weight,
                    State = GetParcelStateByDalParcel(parcel),
                    Customer = GetCustomerInParcel(parcel.SenderId)
                });
            }
            foreach (var parcel in outParcels)
            {
                customer.OutDeliveries.Add(new ParcelInCustomer
                {
                    Id = parcel.Id,
                    Priority = (Priority)(int)parcel.Priority,
                    WeightCategory = (WeightCategory)(int)parcel.Weight,
                    State = GetParcelStateByDalParcel(parcel),
                    Customer = GetCustomerInParcel(parcel.ReciverId)
                });
            }
            return customer;
        }

        public Parcel GetParcel(int id)
        {
            IDAL.DO.Parcel dalParcel = myDal.GetParcel(id);
            Parcel parcel = new Parcel
            {
                Id = dalParcel.Id,
                Sender = GetCustomerInParcel(dalParcel.SenderId),
                Receiver = GetCustomerInParcel(dalParcel.ReciverId),
                Priority = (Priority)(int)dalParcel.Priority,
                WeightCategory = (WeightCategory)(int)dalParcel.Weight,
                Requested = dalParcel.Requested,
                Scheduled = dalParcel.Scheduled,
                PickedUp = dalParcel.PickedUp,
                Delivered = dalParcel.Delivered
            };
            if (parcel.Scheduled != null)
            {
                ListDrone drone = Drones.Find(dr => dr.Id == dalParcel.DroneId);
                parcel.Drone = new DroneInParcel
                {
                    Id = drone.Id,
                    Battery = drone.Battery,
                    Location = drone.Location
                };
            }
            return parcel;
        }

        public IEnumerable<ListStation> GetStationsList(Func<IDAL.DO.Station, bool> predicate = null)
        {
            List<IDAL.DO.Station> dalStations = (List<IDAL.DO.Station>)myDal.GetStationsList(predicate);
            List<ListStation> stations = new List<ListStation>();
            foreach(var dalStation in dalStations)
            {
                stations.Add(new ListStation
                {
                    Id = dalStation.Id,
                    Name = dalStation.Name,
                    FreeChargeSlots = dalStation.ChargeSlots,
                    BusyChargeSlots = Drones.Count(dr => dr.State == DroneState.Maintenance && dr.Location.Latitude == dalStation.Latitude && dr.Location.Longitude == dalStation.Longitude)
                });
            }
            return stations;
        }

        public IEnumerable<ListDrone> GetDronesList(Func<ListDrone,bool> predicate = null)
        {
            if(predicate== null)
                return Drones.ToList();
            return Drones.Where(predicate);
        }

        public IEnumerable<ListCustomer> GetCustomersList(Func<IDAL.DO.Customer, bool> predicate = null)
        {
            List<IDAL.DO.Customer> dalCustomers = (List<IDAL.DO.Customer>)myDal.GetCustomersList(predicate);
            List<IDAL.DO.Parcel> dalParcels = (List<IDAL.DO.Parcel>)myDal.GetParcelsList();
            List<ListCustomer> customers = new List<ListCustomer>();
            foreach(var dalCustomer in dalCustomers)
            {
                customers.Add(new ListCustomer
                {
                    Id = dalCustomer.Id,
                    Name = dalCustomer.Name,
                    Phone = dalCustomer.Phone,
                    OnTheWayParcels = dalParcels.Count(par => par.ReciverId == dalCustomer.Id && par.Delivered == null),
                    ReceivedParcels = dalParcels.Count(par => par.ReciverId == dalCustomer.Id && par.Delivered != null),
                    SentNotSuppliedParcels = dalParcels.Count(par => par.SenderId == dalCustomer.Id && par.Delivered == null),
                    SentSuppliedParcels = dalParcels.Count(par => par.SenderId == dalCustomer.Id && par.Delivered != null)
                });
            }
            return customers;
        }

        public IEnumerable<ListParcel> GetParcelsList(Func<IDAL.DO.Parcel, bool> predicate = null)
        {
            List<IDAL.DO.Parcel> dalParcels = (List<IDAL.DO.Parcel>)myDal.GetParcelsList(predicate);
            List<ListParcel> parcels = new List<ListParcel>();
            foreach(var dalParcel in dalParcels)
            {
                parcels.Add(new ListParcel
                {
                    Id = dalParcel.Id,
                    Priority = (Priority)(int)dalParcel.Priority,
                    WeightCategory = (WeightCategory)(int)dalParcel.Weight,
                    State = GetParcelStateByDalParcel(dalParcel),
                    SenderName = myDal.GetCustomer(dalParcel.SenderId).Name,
                    ReceiverName = myDal.GetCustomer(dalParcel.ReciverId).Name
                });
            }
            return parcels;
        }

        public IEnumerable<ListParcel> GetNonLinkedParcelsList()
        {
            return GetParcelsList(pr => pr.Scheduled ==null);
        }

        public IEnumerable<ListStation> GetStationsWithFreeSlotsList()
        {
            return GetStationsList(st => st.ChargeSlots > 0);
        }
        private ParcelState GetParcelStateByDalParcel(IDAL.DO.Parcel parcel)
        {
            if (parcel.Scheduled == null)
                return ParcelState.Created;
            if (parcel.PickedUp == null)
                return ParcelState.Associated;
            if (parcel.Delivered == null)
                return ParcelState.Collected;
            else
                return ParcelState.Provided;
        }

        private CustomerInParcel GetCustomerInParcel(int id)
        {
            IDAL.DO.Customer dalCustomer = myDal.GetCustomer(id);
            return new CustomerInParcel 
            { 
                Id = dalCustomer.Id, 
                Name = dalCustomer.Name 
            };
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
                Distance = DistanceBetweenTwoPoints(sender.Latitude, sender.Longitude, reciver.Latitude, reciver.Longitude),
                PickUp = new Location { Latitude = sender.Latitude, Longitude = sender.Longitude },
                Destination = new Location { Latitude = reciver.Latitude, Longitude = reciver.Longitude },
                Priority = (Priority)(int)dalParcel.Priority,
                State = dalParcel.PickedUp != null,
                Sender = new CustomerInParcel { Id = sender.Id, Name = sender.Name },
                Receiver = new CustomerInParcel { Id = reciver.Id, Name = reciver.Name }
            };
            return parcelInTransit;
        }
        
        
    }
}