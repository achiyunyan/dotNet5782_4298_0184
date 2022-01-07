using BlApi;
using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BL
{
    public partial class BL : BlApi.IBL
    {
        public Station GetStation(int id)
        {
            DO.Station dalStation;
            try
            {
                dalStation = myDal.GetStation(id);
            }
            catch (DO.NotExistsException stex)
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
            foreach (var drone in Drones)
            {
                if (drone.State == DroneState.Maintenance && drone.Location.Latitude == station.Location.Latitude && drone.Location.Longitude == station.Location.Longitude)
                    station.DronesList.Add(new DroneInCharge { Id = drone.Id, Battery = drone.Battery });
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
            return ListDroneToDrone(listDrone);
        }

        private Drone ListDroneToDrone(ListDrone listDrone)
        {
            return new Drone
            {
                Id = listDrone.Id,
                State = listDrone.State,
                Battery = listDrone.Battery,
                Location = listDrone.Location,
                Model = listDrone.Model,
                WeightCategory = listDrone.WeightCategory,
                Parcel = GetParcelInTransit(listDrone.ParcelId)
            };
        }

        public Customer GetCustomer(int id)
        {
            DO.Customer dalCustomer;
            try
            {
                dalCustomer = myDal.GetCustomer(id);
            }
            catch (DO.NotExistsException stex)
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
            IEnumerable<DO.Parcel> inParcels = myDal.GetParcelsList().Where(pr => pr.ReciverId == dalCustomer.Id);
            IEnumerable<DO.Parcel> outParcels = myDal.GetParcelsList().Where(pr => pr.SenderId == dalCustomer.Id);
            foreach (var parcel in inParcels)
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
            DO.Parcel dalParcel;
            try
            {
                dalParcel = myDal.GetParcel(id);
            }
            catch (DO.NotExistsException stex)
            {
                string str = "bl ereceive exception: " + stex.Message;
                throw new BlException(str);
            }
            return DALParcelToBL(dalParcel);
        }

        private Parcel DALParcelToBL(DO.Parcel dalParcel)
        {
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

        public IEnumerable<ListStation> GetStationsList()
        {
            return from dalStation in myDal.GetStationsList()
                   select new ListStation
                   {
                       Id = dalStation.Id,
                       Name = dalStation.Name,
                       FreeChargeSlots = dalStation.ChargeSlots,
                       BusyChargeSlots = Drones.Count(dr => dr.State == DroneState.Maintenance && dr.Location.Latitude == dalStation.Latitude && dr.Location.Longitude == dalStation.Longitude)
                   };
        }

        public IEnumerable<ListDrone> GetDronesList(Func<ListDrone, bool> predicate = null)
        {
            if (predicate == null)
                return Drones.ToList();
            return Drones.Where(predicate).ToList();
        }

        public IEnumerable<ListCustomer> GetCustomersList()
        {
            IEnumerable<DO.Customer> dalCustomers = myDal.GetCustomersList();
            IEnumerable<DO.Parcel> dalParcels = myDal.GetParcelsList();
            return from dalCustomer in dalCustomers
                   select (new ListCustomer
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

        public IEnumerable<ListParcel> GetParcelsList(Func<DO.Parcel, bool> predicate = null)
        {                       
            return from dalParcel in myDal.GetParcelsList()
                   select new ListParcel
                   {
                       Id = dalParcel.Id,
                       Priority = (Priority)(int)dalParcel.Priority,
                       WeightCategory = (WeightCategory)(int)dalParcel.Weight,
                       State = GetParcelStateByDalParcel(dalParcel),
                       SenderName = myDal.GetCustomer(dalParcel.SenderId).Name,
                       ReceiverName = myDal.GetCustomer(dalParcel.ReciverId).Name
                   };
        }

        public IEnumerable<ListParcel> GetFilteredParcelsList(DateTime? firstDate, DateTime? secondDate, object Sender, object Receiver, object Priority, object State, object Weight)
        {
            IEnumerable<DO.Parcel> dalParcels;
            if (firstDate == null)
                firstDate = DateTime.MinValue;
            if (secondDate == null)
                secondDate = DateTime.MaxValue;
            dalParcels = myDal.GetParcelsList(pr => pr.Requested >= firstDate && pr.Requested <= secondDate || DalParcelLastTime(pr) >= firstDate && DalParcelLastTime(pr) <= secondDate);
            IEnumerable<ListParcel> parcels = from dalParcel in dalParcels
                                              select new ListParcel
                                              {
                                                  Id = dalParcel.Id,
                                                  Priority = (Priority)(int)dalParcel.Priority,
                                                  WeightCategory = (WeightCategory)(int)dalParcel.Weight,
                                                  State = GetParcelStateByDalParcel(dalParcel),
                                                  SenderName = myDal.GetCustomer(dalParcel.SenderId).Name,
                                                  ReceiverName = myDal.GetCustomer(dalParcel.ReciverId).Name
                                              };
            if (!(Sender == null || Sender == ""))
                parcels = parcels.Where(par => par.SenderName == Sender);

            if (!(Receiver == null || Receiver == ""))
                parcels = parcels.Where(par => par.ReceiverName == Receiver);

            if (!(Priority == null || Priority == ""))
                parcels = parcels.Where(par => par.Priority == (Priority)Priority);

            if (!(State == null || State == ""))
                parcels = parcels.Where(par => par.State == (ParcelState)State);

            if (!(Weight == null || Weight == ""))
                parcels = parcels.Where(par => par.WeightCategory == (WeightCategory)Weight);
            return parcels;
            dalParcels = myDal.GetParcelsList(pr => pr.Requested >= firstDate && pr.Requested <= secondDate || DalParcelLastTime(pr) >= firstDate && DalParcelLastTime(pr) <= secondDate);            
            return from dalParcel in dalParcels
                   select new ListParcel
                   {
                       Id = dalParcel.Id,
                       Priority = (Priority)(int)dalParcel.Priority,
                       WeightCategory = (WeightCategory)(int)dalParcel.Weight,
                       State = GetParcelStateByDalParcel(dalParcel),
                       SenderName = myDal.GetCustomer(dalParcel.SenderId).Name,
                       ReceiverName = myDal.GetCustomer(dalParcel.ReciverId).Name
                   };
        }

        private IEnumerable<ListParcel> GetParcelsList()
        {
            return from dalParcel in myDal.GetParcelsList()
                   select new ListParcel
                   {
                       Id = dalParcel.Id,
                       Priority = (Priority)(int)dalParcel.Priority,
                       WeightCategory = (WeightCategory)(int)dalParcel.Weight,
                       State = GetParcelStateByDalParcel(dalParcel),
                       SenderName = myDal.GetCustomer(dalParcel.SenderId).Name,
                       ReceiverName = myDal.GetCustomer(dalParcel.ReciverId).Name
                   };
        }

        private IEnumerable<ListStation> GetStationsList(Func<DO.Station, bool> predicate)
        {
            return from dalStation in myDal.GetStationsList(predicate)
                   select new ListStation
                   {
                       Id = dalStation.Id,
                       Name = dalStation.Name,
                       FreeChargeSlots = dalStation.ChargeSlots,
                       BusyChargeSlots = Drones.Count(dr => dr.State == DroneState.Maintenance && dr.Location.Latitude == dalStation.Latitude && dr.Location.Longitude == dalStation.Longitude)
                   };
        }

        private DateTime? DalParcelLastTime(DO.Parcel pr)
        {
            if (pr.Delivered != null)
                return pr.Delivered;
            if (pr.PickedUp != null)
                return pr.PickedUp;
            if (pr.Scheduled != null)
                return pr.Scheduled;
            return pr.Requested;
        }

        public IEnumerable<ListParcel> GetNonLinkedParcelsList()
        {
            return GetParcelsList(pr => pr.Scheduled == null);
        }

        public IEnumerable<ListStation> GetStationsWithFreeSlotsList()
        {
            return GetStationsList(st => st.ChargeSlots > 0);
        }
        private ParcelState GetParcelStateByDalParcel(DO.Parcel parcel)
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
            DO.Customer dalCustomer = myDal.GetCustomer(id);
            return new CustomerInParcel
            {
                Id = dalCustomer.Id,
                Name = dalCustomer.Name
            };
        }

        private ParcelInTransit GetParcelInTransit(int id)
        {
            DO.Parcel dalParcel;
            try
            {
                dalParcel = myDal.GetParcel(id);
            }
            catch (DO.NotExistsException)
            {
                return default;
            }
            DO.Customer reciver = myDal.GetCustomer(dalParcel.ReciverId);
            DO.Customer sender = myDal.GetCustomer(dalParcel.SenderId);
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