using IDAL.DO;
using System.Collections.Generic;

namespace IDAL
{
    public interface IDal
    {
        void AddCustomer(Customer AddCustomer);
        void AddDrone(Drone AddDrone);
        void AddParcel(Parcel AddParcel);
        void AddStation(Station addStation);
        Customer GetCustomer(int id);
        Drone GetDrone(int id);
        Parcel GetParcel(int id);
        Station GetStation(int id);
        IEnumerable<Customer> GetCustomersList();
        IEnumerable<Drone> GetDronesList();
        IEnumerable<Parcel> GetParcelsList();
        IEnumerable<Station> GetStationsList();
        IEnumerable<DroneCharge> GetDroneCharges();
    }
}