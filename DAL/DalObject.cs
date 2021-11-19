using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;


namespace DalObject
{
    public class DalObject : IDAL.IDal   
    {

        public DalObject()//costructor for dalObject
        {
            DataSource.Config.Initialize();
        }
        /// <summary>
        /// adds station to the stations' list
        /// </summary>
        /// <param name="addStation"></param>
        public void AddStation(Station addStation)
        {
            if(DataSource.Stations.Any(st => st.Id == addStation.Id))
            {
                throw new AlreadyExistsException($"id: {addStation.Id} already exists!!");
            }
            DataSource.Stations.Add(addStation);
        }
        /// <summary>
        /// Adds drone to the drones' list 
        /// </summary>
        /// <param name="AddDrone"></param>
        public void AddDrone(Drone addDrone)
        {
            if (DataSource.Drones.Any(st => st.Id == addDrone.Id))
            {
                throw new AlreadyExistsException($"id: {addDrone.Id} already exists!!");
            }
            DataSource.Drones.Add(addDrone);
        }
        /// <summary>
        /// Adds customer to the customers' list
        /// </summary>
        /// <param name="AddCustomer"></param>
        public void AddCustomer(Customer addCustomer)
        {
            if (DataSource.Customers.Any(st => st.Id == addCustomer.Id))
            {
                throw new AlreadyExistsException($"id: {addCustomer.Id} already exists!!");
            }
            DataSource.Customers.Add(addCustomer);
        }
        /// <summary>
        /// Adds parcel to the parcels' list
        /// </summary>
        /// <param name="AddParcel"></param>
        public void AddParcel(Parcel addParcel)
        {
            addParcel.Id = ++DataSource.Config.parcelNum;
            DataSource.Parcels.Add(addParcel);
        }
        public void AddDroneCharge(DroneCharge addDroneCharge)
        {
            DataSource.DroneCharges.Add(addDroneCharge);
        }
        public void UpdateDrone(Drone updateDrone)
        {
            int indexOfDrone = DataSource.Drones.IndexOf(DataSource.Drones.Find(dr => dr.Id == updateDrone.Id));
            if (indexOfDrone == -1)
            {
                throw new NotExistsException($"id: {updateDrone.Id} not exists!!");
            }
            DataSource.Drones[indexOfDrone] = updateDrone;
        }
        public void UpdateCustomer(Customer updateCustomer)
        {
            int indexOfCustomer = DataSource.Customers.IndexOf(DataSource.Customers.Find(cu => cu.Id == updateCustomer.Id));
            if (indexOfCustomer == -1)
            {
                throw new NotExistsException($"id: {updateCustomer.Id} not exists!!");
            }
            DataSource.Customers[indexOfCustomer] = updateCustomer;
        }
        public void UpdateStation(Station updateStation)
        {
            int indexOfStation = DataSource.Stations.IndexOf(DataSource.Stations.Find(st => st.Id == updateStation.Id));
            if (indexOfStation == -1)
            {
                throw new NotExistsException($"id: {updateStation.Id} not exists!!");
            }
            DataSource.Stations[indexOfStation] = updateStation;
        }

        public void UpdateParcel(Parcel updateParcel)
        {
            int indexOfParcel = DataSource.Parcels.IndexOf(DataSource.Parcels.Find(pr => pr.Id == updateParcel.Id));
            if (indexOfParcel == -1)
            {
                throw new NotExistsException($"id: {updateParcel.Id} not exists!!");
            }
            DataSource.Parcels[indexOfParcel]= updateParcel;
        }
        public void DeleteStation(Station deleteStation)
        {
            if (!DataSource.Stations.Remove(deleteStation))
            {
                throw new NotExistsException($"id: {deleteStation.Id} not exists!!");
            }
        }

        public void DeleteDrone(Drone deleteDrone)
        {
            if (!DataSource.Drones.Remove(deleteDrone))
            {
                throw new NotExistsException($"id: {deleteDrone.Id} not exists!!");
            }
        }

        public void DeleteCustomer(Customer deleteCustomer)
        {
            if (!DataSource.Customers.Remove(deleteCustomer))
            {
                throw new NotExistsException($"id: {deleteCustomer.Id} not exists!!");
            }
        }

        public void DeleteParcel(Parcel deleteParcel)
        {
            if (!DataSource.Parcels.Remove(deleteParcel))
            {
                throw new NotExistsException($"id: {deleteParcel.Id} not exists!!");
            }
        }

        /// <summary>
        /// links a chosen parcel to a chosen drone 
        /// </summary>
        /// <param name="parcelId"></param>
        /// <param name="droneId"></param>
        public void LinkParcelToDrone(Parcel parcel, int droneId)
        {
            Parcel linkedParcel = GetParcel(parcel.Id);
            linkedParcel.DroneId = droneId;
            int indexOfParcel = DataSource.Parcels.IndexOf(linkedParcel);
            DataSource.Parcels[indexOfParcel] = linkedParcel;
            // Drone linkedDrone = GetDrone(droneId);
            //  int indexOfDrone = DataSource.Drones.IndexOf(linkedDrone);
            //            linkedParcel.DroneId = linkedDrone.Id;
            //linkedParcel.Scheduled = DateTime.Now;
            //Drones[indexOfDrone] = linkedDrone;

            /* Parcel linkedParcel = GetParcel(parcelId);
             int indexOfLinked = DataSource.Parcels.IndexOf(linkedParcel);
             int sizeOfLIst = Drones.Count();
             for (int i = 0; i < sizeOfLIst; i++)
             {
                 if (Drones[i].Status == DroneStatus.available && Drones[i].MaxWeight >= linkedParcel.Weight)
                 {
                     Drone temp = Drones[i];
                     temp.Status = DroneStatus.delivery;//not necessarily works,requires further tests
                     Drones[i] = temp;
                     linkedParcel.DroneId = Drones[i].Id;
                     linkedParcel.Scheduled = DateTime.Now;
                     Parcels[indexOfLinked] = linkedParcel;
                 }
             }*/
        }
        /// <summary>
        /// updates the pickup time of a liked parcel to the current time,which means that the drone starts flying
        /// </summary>
        /// <param name="parcelId"></param>
        public void PickUpParcel(int parcelId)
        {
            Parcel pickedParcel = GetParcel(parcelId);
            int indexOfLinked = DataSource.Parcels.IndexOf(pickedParcel);
            pickedParcel.PickedUp = DateTime.Now;
            DataSource.Parcels[indexOfLinked] = pickedParcel;
        }
        /// <summary>
        /// delivers a chosen parcel to the customer and free the drone
        /// </summary>
        /// <param name="parcelId"></param>
        public void DeliverParcel(int parcelId)
        {
            Parcel pickedParcel = GetParcel(parcelId);
            Drone deliveryDrone = GetDrone(pickedParcel.DroneId);
            int indexOfPicked = DataSource.Parcels.IndexOf(pickedParcel);
            int indexOfDrone = DataSource.Drones.IndexOf(deliveryDrone);
            pickedParcel.Delivered = DateTime.Now;
            DataSource.Parcels[indexOfPicked] = pickedParcel;
            DataSource.Drones[indexOfDrone] = deliveryDrone;

        }
        /// <summary>
        /// sends chosen drone to charge in a chosen station 
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        public void SendDroneToCharge(int droneId, int stationId)
        {
            Drone ToCharge = GetDrone(droneId);
            Station ChargeStation = GetStation(stationId);
            int indexOfChargedDrone = DataSource.Drones.IndexOf(ToCharge);
            int indexOfChargeStation = DataSource.Stations.IndexOf(ChargeStation);
            ChargeStation.ChargeSlots -= 1;
            DataSource.DroneCharges.Add(new DroneCharge
            {
                DroneId = droneId,
                StationId = stationId
            });
            DataSource.Drones[indexOfChargedDrone] = ToCharge;
            DataSource.Stations[indexOfChargeStation] = ChargeStation;
        }
        /// <summary>
        /// free a chosen drone from a chosen station , and fill its battery
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        public void freeDroneFromCharge(int droneId, int stationId)
        {
            Drone freeDrone = GetDrone(droneId);
            Station ChargeStation = GetStation(stationId);
            int indexOfFree = DataSource.Drones.IndexOf(freeDrone);
            int indexOfChargeStation = DataSource.Stations.IndexOf(ChargeStation);
            ChargeStation.ChargeSlots += 1;
            DataSource.DroneCharges.Remove(new DroneCharge { DroneId = droneId, StationId = stationId });
            DataSource.Drones[indexOfFree] = freeDrone;
            DataSource.Stations[indexOfChargeStation] = ChargeStation;

        }
        /// <summary>
        /// returns a station by chosen id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Station GetStation(int id)
        {
            Station targ;
            for (int i = 0; i < DataSource.Stations.Count; i++)
            {
                targ = DataSource.Stations[i];
                if (targ.Id == id)
                {
                    return targ;
                }
            }
            throw new NotExistsException($"id: {id} not exists!!");
        }
        /// <summary>
        /// returns a drone by chosen id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Drone GetDrone(int id)
        {
            Drone targ;
            for (int i = 0; i < DataSource.Drones.Count; i++)
            {
                targ = DataSource.Drones[i];
                if (targ.Id == id)
                {
                    return targ;
                }
            }
            throw new IDAL.DO.NotExistsException($"id: {id} not exists!!");
        }
        /// <summary>
        /// returns a customer by chosen id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomer(int id)
        {
            Customer targ;
            for (int i = 0; i < DataSource.Customers.Count; i++)
            {
                targ = DataSource.Customers[i];
                if (targ.Id == id)
                {
                    return targ;
                }
            }
            throw new NotExistsException($"id: {id} not exists!!");
        }
        /// <summary>
        /// returns a parcel by chosen id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Parcel GetParcel(int id)
        {
            Parcel targ;
            for (int i = 0; i < DataSource.Parcels.Count; i++)
            {
                targ = DataSource.Parcels[i];
                if (targ.Id == id)
                {
                    return targ;
                }
            }
            throw new NotExistsException($"id: {id} not exists!!");
            return default;
        }
        /// <summary>
        /// returns a copy of he stations' list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> GetStationsList()
        {
            return DataSource.Stations.ToList();
        }
        /// <summary>
        /// returns a copy of he Drones' list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Drone> GetDronesList()
        {
            return DataSource.Drones.ToList();
        }
        /// <summary>
        /// returns a copy of he Customers' list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomersList()
        {
            return DataSource.Customers.ToList();
        }
        /// <summary>
        /// returns a copy of he Parcels' list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> GetParcelsList()
        {
            return DataSource.Parcels.ToList();
        }
         
        public double GetElectricityUsePerKmAvailable()
        {
            return DataSource.Config.ElectricityUsePerKmAvailable;
        }

        public double GetElectricityUsePerKmLight()
        {
            return DataSource.Config.ElectricityUsePerKmLight;
        }

        public double GetElectricityUsePerKmMedium()
        {
            return DataSource.Config.ElectricityUsePerKmMedium;
        }

        public double GetElectricityUsePerKmHeavy()
        {
            return DataSource.Config.ElectricityUsePerKmHeavy;
        }

        public double GetElectricityChargePerHour()
        {
            return DataSource.Config.ElectricityChargePerHour;
        }

        public double DistanceBetweenTwoPoints(double lat1, double lon1, double lat2, double lon2)
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
