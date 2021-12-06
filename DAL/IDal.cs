using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        IEnumerable<Customer> GetCustomersList(Func<Customer, bool> predicate = null);
        IEnumerable<Drone> GetDronesList(Func<Drone, bool> predicate = null);
        IEnumerable<Parcel> GetParcelsList(Func<Parcel, bool> predicate = null);
        IEnumerable<Station> GetStationsList(Func<Station, bool> predicate = null);
        IEnumerable<DroneCharge> GetDroneCharges();
    }
}