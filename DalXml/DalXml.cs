using DO;
using DalApi;
using System;
using System.Collections.Generic;

namespace Dal
{
    internal sealed class DalXml : IDal
    {
        public void AddCustomer(Customer AddCustomer)
        {
            throw new NotImplementedException();
        }

        public void AddDrone(Drone AddDrone)
        {
            throw new NotImplementedException();
        }

        public void AddDroneCharge(DroneCharge addDroneCharge)
        {
            throw new NotImplementedException();
        }

        public void AddParcel(Parcel AddParcel)
        {
            throw new NotImplementedException();
        }

        public void AddStation(Station addStation)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomer(Customer deleteCustomer)
        {
            throw new NotImplementedException();
        }

        public void DeleteDrone(Drone deleteDrone)
        {
            throw new NotImplementedException();
        }

        public void DeleteDroneCharge(DroneCharge deleteDroneCharge)
        {
            throw new NotImplementedException();
        }

        public void DeleteParcel(Parcel deleteParcel)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(Station deleteStation)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomersList(Func<Customer, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Drone GetDrone(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> GetDronesList(Func<Drone, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public double GetElectricityChargePerHour()
        {
            throw new NotImplementedException();
        }

        public double GetElectricityUsePerKmAvailable()
        {
            throw new NotImplementedException();
        }

        public double GetElectricityUsePerKmHeavy()
        {
            throw new NotImplementedException();
        }

        public double GetElectricityUsePerKmLight()
        {
            throw new NotImplementedException();
        }

        public double GetElectricityUsePerKmMedium()
        {
            throw new NotImplementedException();
        }

        public Parcel GetParcel(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetParcelsList(Func<Parcel, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetStationsList(Func<Station, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomer(Customer updateCustomer)
        {
            throw new NotImplementedException();
        }

        public void UpdateDrone(Drone updateDrone)
        {
            throw new NotImplementedException();
        }

        public void UpdateParcel(Parcel updateParcel)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(Station updateStation)
        {
            throw new NotImplementedException();
        }
    }
}
