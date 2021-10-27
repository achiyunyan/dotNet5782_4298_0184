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

        public DalObject()
        {
            DataSource.Config.Initialize();
        }
        static public void AddStation(Station addStation)
        {
            //if (GetStation(addStation.Id).Id != -1)
            //    return;
            DataSource.Stations.Add(addStation);
        }

        static public void AddDrone(Drone AddDrone)
        {
            //if (GetDrone(AddDrone.Id).Id != -1)
            //    return;
            DataSource.Drones.Add(AddDrone);
        }

        static public void AddCustomer(Customer AddCustomer)
        {
            //if (GetCustomer(AddCustomer.Id).Id != -1)
            //    return;
            DataSource.Customers.Add(AddCustomer);
        }
        static public void AddParcel(Parcel AddParcel)
        {
            //if (GetParcel(AddParcel.Id).Id != -1)
            //    return;
            DataSource.Parcels.Add(AddParcel);
        }
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
        static public void PickUpParcel(int parcelId)
        {
            Parcel pickedParcel = GetParcel(parcelId);
            int indexOfLinked = DataSource.Parcels.IndexOf(pickedParcel);
            pickedParcel.PickedUp = DateTime.Now;
            DataSource.Parcels[indexOfLinked] = pickedParcel;
        }
        static public void DeliverParcel(int parcelId)
        {
            Parcel pickedParcel = GetParcel(parcelId);
            int indexOfPicked = DataSource.Parcels.IndexOf(pickedParcel);
            pickedParcel.Delivered = DateTime.Now;
            DataSource.Parcels[indexOfPicked] = pickedParcel;
        }
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
            DataSource.Drones[indexOfFree] = freeDrone;

        }
        static public Station GetStation(int id)
        {
            Station targ = new Station();
            targ.Id = -1;
            foreach (Station target in DataSource.Stations)
            {
                if (target.Id==id)
                    return target;
            }
            return targ;
        }
        static public Drone GetDrone(int id)
        {
            Drone targ = new Drone();
            targ.Id = -1;
            foreach (Drone target in DataSource.Drones)
            {
                if (target.Id == id)
                    return target;
            }
            return targ;
        }
        static public Customer GetCustomer(int id)
        {
            Customer targ = new Customer();
            targ.Id = -1;
            foreach (Customer target in DataSource.Customers)
            {
                if (target.Id==id)
                    return target;
            }
            return targ;
        }
        static public Parcel GetParcel(int id)
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
            //targ.Id = -1;
            //foreach (Parcel target in DataSource.Parcels)
            //{
            //    if (target.Id.Equals(id))
            //        return target;
            //}
            return new Parcel();
        }
        static public List<Station> GetStationsList()
        {
            return DataSource.Stations.ToList();
        }
        static public List<Drone> GetDronesList()
        {
            return DataSource.Drones.ToList();
        }
        static public List<Customer> GetCustomersList()
        {
            return DataSource.Customers.ToList();
        }
        static public List<Parcel> GetParcelsList()
        {
            return DataSource.Parcels.ToList();
        }

    }
}
