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
            if (GetStation(addStation.Id).Id!=-1)
                return;
            DataSource.Stations.Add(addStation);
        }
        static public void AddDrone(Drone AddDrone)
        {
            if (GetDrone(AddDrone.Id).Id != -1)
                return;
            DataSource.Drones.Add(AddDrone);
        }
        static public void AddCustomer(Customer AddCustomer)
        {
            if (GetCustomer(AddCustomer.Id).Id != -1)
                return;
            DataSource.Customers.Add(AddCustomer);
        }
        static public void AddParcel(Parcel AddParcel)
        {
            if (GetParcel(AddParcel.Id).Id != -1)
                return;
            DataSource.Parcels.Add(AddParcel);
        }
        static public bool LinkParcelToDrone(int parcelId)//
        {
            Parcel linkedParcel= GetParcel(parcelId);
            int indexOfLinked = DataSource.Parcels.IndexOf(linkedParcel);
            List<Drone> tempDronesList = GetDronesList();
            int sizeOfLIst = GetDronesList().Count();
            for (int i = 0; i <sizeOfLIst ; i++)
            {
                if (tempDronesList[i].Status == DroneStatus.available && tempDronesList[i].MaxWeight >= linkedParcel.Weight)
                {
                    Drone temp = tempDronesList[i];
                    temp.Status = DroneStatus.delivery;//not necessarily works,requires further tests
                    linkedParcel.DroneId = tempDronesList[i].Id;
                    linkedParcel.Scheduled = DateTime.Now;
                    DataSource.Parcels[indexOfLinked] = linkedParcel;
                    return true;
                }
            }
            return false;
        }
        static public void PickUpParcel(int parcelId)
        {
            Parcel pickedParcel = GetParcel(parcelId);
            int indexOfLinked = DataSource.Parcels.IndexOf(pickedParcel);
            pickedParcel.PickedUp = DateTime.Now;
            DataSource.Parcels[indexOfLinked] = pickedParcel;
        }
        static public void DeliverParcel(int parcelId)//to finish...
        {
            Parcel pickedParcel = GetParcel(parcelId);
            int indexOfPicked = DataSource.Parcels.IndexOf(pickedParcel);
            pickedParcel.Delivered = DateTime.Now;
            DataSource.Parcels[indexOfPicked] = pickedParcel;
        }
        static public void SendDroneToCharge(int DroneId)
        {
            Drone ToCharge = GetDrone(DroneId);
            int indexOfCharged = DataSource.Drones.IndexOf(ToCharge);
            //not final
        }
        static public Station GetStation(int id)
        {
            Station targ = new Station();
            targ.Id = -1;
            foreach (Station target in DataSource.Stations)
            {
                if (target.Id.Equals(id))
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
                if (target.Id.Equals(id))
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
                if (target.Id.Equals(id))
                    return target;
            }
            return targ;
        }
        static public Parcel GetParcel(int id)
        {
            Parcel targ = new Parcel();
            targ.Id = -1;
            foreach (Parcel target in DataSource.Parcels)
            {
                if (target.Id.Equals(id))
                    return target;
            }
            return targ;
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
