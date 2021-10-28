using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using static DalObject.DataSource;

namespace DalObject
{
    public class DalObject
    {

        public DalObject()//costructor for dalObject
        {
            DataSource.Config.Initialize();
        }
        /// <summary>
        /// adds station to the stations' list
        /// </summary>
        /// <param name="addStation"></param>
        static public void AddStation(Station addStation)
        {
            if (GetStation(addStation.Id).Id != 0)
                return;
            DataSource.Stations.Add(addStation);
        }
        /// <summary>
        /// Adds drone to the drones' list 
        /// </summary>
        /// <param name="AddDrone"></param>
        static public void AddDrone(Drone AddDrone)
        {
            if (GetDrone(AddDrone.Id).Id != 0)
                return;
            DataSource.Drones.Add(AddDrone);
        }
        /// <summary>
        /// Adds customer to the customers' list
        /// </summary>
        /// <param name="AddCustomer"></param>
        static public void AddCustomer(Customer AddCustomer)
        {
            if (GetCustomer(AddCustomer.Id).Id !=0)
                return;
            DataSource.Customers.Add(AddCustomer);
        }
        /// <summary>
        /// Adds parcel to the parcels' list
        /// </summary>
        /// <param name="AddParcel"></param>
        static public void AddParcel(Parcel AddParcel)
        {
            if (GetParcel(AddParcel.Id).Id !=0)
                return;
            DataSource.Parcels.Add(AddParcel);
        }
        /// <summary>
        /// links a chosen parcel to a chosen drone 
        /// </summary>
        /// <param name="parcelId"></param>
        /// <param name="droneId"></param>
        static public void LinkParcelToDrone(int parcelId,int droneId)
        {
            Parcel linkedParcel = GetParcel(parcelId);
            Drone linkedDrone = GetDrone(droneId);
            int indexOfParcel =Parcels.IndexOf(linkedParcel);
            int indexOfDrone = Drones.IndexOf(linkedDrone);
            linkedDrone.Status = DroneStatus.delivery;
            linkedParcel.DroneId = linkedDrone.Id;
            linkedParcel.Scheduled = DateTime.Now;
            Drones[indexOfDrone] = linkedDrone;
            Parcels[indexOfParcel] = linkedParcel;

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
        static public void PickUpParcel(int parcelId)
        {
            Parcel pickedParcel = GetParcel(parcelId);
            int indexOfLinked = Parcels.IndexOf(pickedParcel);
            pickedParcel.PickedUp = DateTime.Now;
            Parcels[indexOfLinked] = pickedParcel;
        }
        /// <summary>
        /// delivers a chosen parcel to the customer and free the drone
        /// </summary>
        /// <param name="parcelId"></param>
        static public void DeliverParcel(int parcelId)
        {
            Parcel pickedParcel = GetParcel(parcelId);
            Drone deliveryDrone = GetDrone(pickedParcel.DroneId);
            int indexOfPicked = Parcels.IndexOf(pickedParcel);
            int indexOfDrone = Drones.IndexOf(deliveryDrone);
            pickedParcel.Delivered = DateTime.Now;
            deliveryDrone.Status = DroneStatus.available;
            Parcels[indexOfPicked] = pickedParcel;
            Drones[indexOfDrone] = deliveryDrone;

        }
        /// <summary>
        /// sends chosen drone to charge in a chosen station 
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        static public void SendDroneToCharge(int droneId,int stationId)
        {
            Drone ToCharge = GetDrone(droneId);
            Station ChargeStation = GetStation(stationId);
            int indexOfChargedDrone = Drones.IndexOf(ToCharge);
            int indexOfChargeStation = Stations.IndexOf(ChargeStation);
            ToCharge.Status = DroneStatus.maintenance;
            ChargeStation.ChargeSlots -= 1;
            DroneCharges.Add(new DroneCharge
            {
                DroneId = droneId,
                StationId=stationId
            });
            Drones[indexOfChargedDrone] = ToCharge;
            Stations[indexOfChargeStation] = ChargeStation;   
        }
        /// <summary>
        /// free a chosen drone from a chosen station , and fill its battery
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        static public void freeDroneFromCharge(int droneId,int stationId)
        {
            Drone freeDrone = GetDrone(droneId);
            Station ChargeStation = GetStation(stationId);
            int indexOfFree = DataSource.Drones.IndexOf(freeDrone);
            int indexOfChargeStation = Stations.IndexOf(ChargeStation);
            freeDrone.Status = DroneStatus.available;
            freeDrone.Battery = 100;
            ChargeStation.ChargeSlots += 1;
            DroneCharges.Remove(new DroneCharge { DroneId = droneId, StationId = stationId });
            Drones[indexOfFree] = freeDrone;
            Stations[indexOfChargeStation] = ChargeStation;

        }
        /// <summary>
        /// returns a station by chosen id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static public Station GetStation(int id)
        {
            Station targ ;
            for (int i = 0; i < Stations.Count; i++)
            {
                targ = Stations[i];
                if (targ.Id == id)
                {
                    return targ;
                }
            }
            return new Station();
        }
        /// <summary>
        /// returns a drone by chosen id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static public Drone GetDrone(int id)
        {
            Drone targ;
            for (int i = 0; i < Drones.Count; i++)
            {
                targ = Drones[i];
                if (targ.Id == id)
                {
                    return targ;
                }
            }
            return new Drone();
        }
        /// <summary>
        /// returns a customer by chosen id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static public Customer GetCustomer(int id)
        {
            Customer targ  ;
            for (int i = 0; i < Customers.Count; i++)
            {
                targ = Customers[i];
                if (targ.Id == id)
                {
                    return targ;
                }
            }
            return new Customer();
        }
        /// <summary>
        /// returns a parcel by chosen id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static public Parcel GetParcel(int id)
        {
            Parcel targ;
            for (int i = 0; i < Parcels.Count; i++)
            {
                targ =Parcels[i];
                if (targ.Id == id)
                {
                    return targ;
                }
            }
            return new Parcel();
        }
        /// <summary>
        /// returns a copy of he stations' list
        /// </summary>
        /// <returns></returns>
        static public List<Station> GetStationsList()
        {
            return DataSource.Stations.ToList();
        }
        /// <summary>
        /// returns a copy of he Drones' list
        /// </summary>
        /// <returns></returns>
        static public List<Drone> GetDronesList()
        {
            return DataSource.Drones.ToList();
        }
        /// <summary>
        /// returns a copy of he Customers' list
        /// </summary>
        /// <returns></returns>
        static public List<Customer> GetCustomersList()
        {
            return DataSource.Customers.ToList();
        }
        /// <summary>
        /// returns a copy of he Parcels' list
        /// </summary>
        /// <returns></returns>
        static public List<Parcel> GetParcelsList()
        {
            return DataSource.Parcels.ToList();
        }

    }
}
