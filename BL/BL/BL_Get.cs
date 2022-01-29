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
        #region Get Item
        /// <summary>
        /// returns a station by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// returns a drone by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id)
        {
            return ListDroneToDrone(GetListDrone(id));
        }

        /// <summary>
        /// returns a customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// returns a parcel by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        #endregion

        #region Get List
        /// <summary>
        /// returns a list of stations (class - ListStation)
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// returns a list of drones by a predicate (class - ListDrone)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ListDrone> GetDronesList(Func<ListDrone, bool> predicate = null)
        {
            if (predicate == null)
                return Drones.ToList();
            return Drones.Where(predicate).ToList();
        }

        /// <summary>
        /// returns a list of customers (Class - ListCustomer)
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// return a list of parcels (Class - ListParcel)
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ListParcel> GetParcelsList()
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
        #endregion

        #region Get Filtered List
        /// <summary>
        /// returns a filtered list of parcels
        /// </summary>
        /// <param name="firstDate">parcels that where active after this date</param>
        /// <param name="secondDate">parcels that where active before this date</param>
        /// <param name="Sender">parcels whose sender is him</param>
        /// <param name="Receiver">parcels whose receiver is him</param>
        /// <param name="Priority">parcels in this priority</param>
        /// <param name="State">parcels in this state</param>
        /// <param name="Weight">parcels that weight in that category</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ListParcel> GetFilteredParcelsList(DateTime? firstDate, DateTime? secondDate, object Sender, object Receiver, object Priority, object State, object Weight)
        {
            IEnumerable<DO.Parcel> dalParcels;
            if (firstDate == null)//if not limited
                firstDate = DateTime.MinValue;
            if (secondDate == null)//if not limited
                secondDate = DateTime.MaxValue;
            IEnumerable<ListParcel> parcels;
            lock (myDal)
            {
                //parcels that where active between firstDate to secondDate
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
            //the filters are given in objects from comobox so they are not active if they are null (nothing chosen) or "" (empty selection)
            if (!(Sender == null || (string)Sender == ""))
                parcels = parcels.Where(par => par.SenderName == (string)Sender);

            if (!(Receiver == null || (string)Receiver == ""))
                parcels = parcels.Where(par => par.ReceiverName == (string)Receiver);

            if (!(Priority == null || (string)Priority == ""))
                parcels = parcels.Where(par => par.Priority == (Priority)Priority);

            if (!(State == null || State == ""))
                parcels = parcels.Where(par => par.State == (ParcelState)State);

            if (!(Weight == null || Weight == ""))
                parcels = parcels.Where(par => par.WeightCategory == (WeightCategory)Weight);

            return parcels;
        }

        /// <summary>
        /// returns a list of unlinked parcels
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ListParcel> GetNonLinkedParcelsList()
        {
            return GetParcelsList(pr => pr.Scheduled == null);
        }

        /// <summary>
        /// returns a list of stations with free charge slots
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ListStation> GetStationsWithFreeSlotsList()
        {
            return GetStationsList(st => st.ChargeSlots > 0);
        }
        #endregion

        #region drone help functions
        /// <summary>
        /// returns listsdrone by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal ListDrone GetListDrone(int id)
        {
            ListDrone listDrone = Drones.Find(dr => dr.Id == id);
            if (listDrone == default)
            {
                throw new BlException($"id: {id} not exists!!");
            }
            return listDrone;
        }

        /// <summary>
        /// convert a listdrone to drone
        /// </summary>
        /// <param name="listDrone"></param>
        /// <returns></returns>
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

        //GetParcelInTransit is in parcel help functions
        #endregion

        #region parcel help functions
        /// <summary>
        /// convert a dal parcel to bl
        /// </summary>
        /// <param name="dalParcel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// return a list of parcel filtered by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private IEnumerable<ListParcel> GetParcelsList(Func<DO.Parcel, bool> predicate = null)
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

        /// <summary>
        /// return the most recent active time of the parcel (dal)
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
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

        /// <summary>
        /// returns the parcel state for dal's parcels
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// returns a CustomerInParcel by id (used in parcel)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// returns a ParcelInTransit by id (used in drone)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="droneLocation"></param>
        /// <returns></returns>
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
            if (dalParcel.PickedUp != null) //Collected -> distance from sender to receiver
                distance = DistanceBetweenTwoPoints(sender.Latitude, sender.Longitude, reciver.Latitude, reciver.Longitude);
            else //Associated -> distance from drone to sender
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
        #endregion

        #region station help functions
        /// <summary>
        /// return a list of stations filtered by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
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
        #endregion

        #region simulator functions       
        /// <summary>
        /// returns ElectricityChargePerSec
        /// </summary>
        /// <returns></returns>
        internal double GetElectricityChargePerSec()
        {
            return ElectricityChargePerSec;
        }
        #endregion
    }
}