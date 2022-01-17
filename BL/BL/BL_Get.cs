using BlApi;
using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace BL
{
    public partial class BL : BlApi.IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int id)
        {
            DO.Station dalStation;
            try
            {
                lock (myDal)
                {
                    dalStation = myDal.GetStation(id);
                }
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id)
        {
            return ListDroneToDrone(GetListDrone(id));
        }

        internal ListDrone GetListDrone(int id)
        {
            ListDrone listDrone = Drones.Find(dr => dr.Id == id);
            if (listDrone == default)
            {
                throw new BlException($"id: {id} not exists!!");
            }
            return listDrone;
        }

        internal Drone ListDroneToDrone(ListDrone listDrone)
        {
            return new Drone
            {
                Id = listDrone.Id,
                State = listDrone.State,
                Battery = listDrone.Battery,
                Location = listDrone.Location,
                Model = listDrone.Model,
                WeightCategory = listDrone.WeightCategory,
                Parcel = GetParcelInTransit(listDrone.ParcelId, listDrone.Location)
            };
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id)
        {
            DO.Customer dalCustomer;
            try
            {
                lock (myDal)
                {
                    dalCustomer = myDal.GetCustomer(id);
                }
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
            IEnumerable<DO.Parcel> inParcels;
            IEnumerable<DO.Parcel> outParcels;
            lock (myDal)
            {
                inParcels = myDal.GetParcelsList().Where(pr => pr.ReciverId == dalCustomer.Id);
                outParcels = myDal.GetParcelsList().Where(pr => pr.SenderId == dalCustomer.Id);
            }
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int id)
        {
            DO.Parcel dalParcel;
            try
            {
                lock (myDal)
                {
                    dalParcel = myDal.GetParcel(id);
                }
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ListStation> GetStationsList()
        {
            lock (myDal)
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
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ListDrone> GetDronesList(Func<ListDrone, bool> predicate = null)
        {
            if (predicate == null)
                return Drones.ToList();
            return Drones.Where(predicate).ToList();
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ListCustomer> GetCustomersList()
        {
            IEnumerable<DO.Customer> dalCustomers;
            IEnumerable<DO.Parcel> dalParcels;
            lock (myDal)
            {
                dalCustomers = myDal.GetCustomersList();
                dalParcels = myDal.GetParcelsList();
            }
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ListParcel> GetParcelsList(Func<DO.Parcel, bool> predicate = null)
        {
            lock (myDal)
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
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ListParcel> GetFilteredParcelsList(DateTime? firstDate, DateTime? secondDate, object Sender, object Receiver, object Priority, object State, object Weight)
        {
            IEnumerable<DO.Parcel> dalParcels;
            if (firstDate == null)
                firstDate = DateTime.MinValue;
            if (secondDate == null)
                secondDate = DateTime.MaxValue;
            IEnumerable<ListParcel> parcels;
            lock (myDal)
            {
                dalParcels = myDal.GetParcelsList(pr => pr.Requested >= firstDate && pr.Requested <= secondDate || DalParcelLastTime(pr) >= firstDate && DalParcelLastTime(pr) <= secondDate);
                parcels = from dalParcel in dalParcels
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
        }

        private IEnumerable<ListParcel> GetParcelsList()
        {
            lock (myDal)
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
        }

        private IEnumerable<ListStation> GetStationsList(Func<DO.Station, bool> predicate)
        {
            lock (myDal)
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ListParcel> GetNonLinkedParcelsList()
        {
            return GetParcelsList(pr => pr.Scheduled == null);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
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
            DO.Customer dalCustomer;
            lock (myDal)
            {
                dalCustomer = myDal.GetCustomer(id);
            }
            return new CustomerInParcel
            {
                Id = dalCustomer.Id,
                Name = dalCustomer.Name
            };
        }

        private ParcelInTransit GetParcelInTransit(int id, Location droneLocation)
        {
            DO.Parcel dalParcel;
            try
            {
                lock (myDal)
                {
                    dalParcel = myDal.GetParcel(id);
                }
            }
            catch (DO.NotExistsException)
            {
                return default;
            }
            DO.Customer reciver;
            DO.Customer sender;
            lock (myDal)
            {
                reciver = myDal.GetCustomer(dalParcel.ReciverId);
                sender = myDal.GetCustomer(dalParcel.SenderId);
            }
            double distance;
            if (dalParcel.PickedUp != null) //Collected
                distance = DistanceBetweenTwoPoints(sender.Latitude, sender.Longitude, reciver.Latitude, reciver.Longitude);
            else //Associated
                distance = DistanceBetweenTwoPoints(sender.Latitude, sender.Longitude, droneLocation.Latitude, droneLocation.Longitude);
            ParcelInTransit parcelInTransit = new ParcelInTransit
            {
                Id = dalParcel.Id,
                WeightCategory = (WeightCategory)(int)dalParcel.Weight,
                Distance = distance,
                PickUp = new Location { Latitude = sender.Latitude, Longitude = sender.Longitude },
                Destination = new Location { Latitude = reciver.Latitude, Longitude = reciver.Longitude },
                Priority = (Priority)(int)dalParcel.Priority,
                State = dalParcel.PickedUp != null,
                Sender = new CustomerInParcel { Id = sender.Id, Name = sender.Name },
                Receiver = new CustomerInParcel { Id = reciver.Id, Name = reciver.Name }
            };
            return parcelInTransit;
        } 
        
        internal double GetElectricityChargePerSec()
        {
            return ElectricityChargePerSec;
        }
    }
}