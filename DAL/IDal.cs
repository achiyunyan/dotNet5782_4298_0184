using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IDAL.DalApi
{
    //TODO lesader et habalagan
    public interface IDal
    {
        void AddCustomer(Customer addCustomer);
        void AddDrone(Drone addDrone);
        void AddDroneCharge(DroneCharge addDroneCharge);
        void AddParcel(Parcel addParcel);
        void AddStation(Station addStation);
        void DeleteCustomer(Customer deleteCustomer);
        void DeleteDrone(Drone deleteDrone);
        void DeleteDroneCharge(DroneCharge deleteDroneCharge);
        void DeleteParcel(Parcel deleteParcel);
        void DeleteStation(Station deleteStation);
        void UpdateCustomer(Customer updateCustomer);
        void UpdateDrone(Drone updateDrone);
        void UpdateParcel(Parcel updateParcel);
        void UpdateStation(Station updateStation);
        Customer GetCustomer(int id);
        IEnumerable<Customer> GetCustomersList(Func<Customer, bool> predicate = null);
        Drone GetDrone(int id);
        IEnumerable<DroneCharge> GetDroneCharges();
        IEnumerable<Drone> GetDronesList(Func<Drone, bool> predicate = null);
        double GetElectricityChargePerHour();
        double GetElectricityUsePerKmAvailable();
        double GetElectricityUsePerKmHeavy();
        double GetElectricityUsePerKmLight();
        double GetElectricityUsePerKmMedium();
        Parcel GetParcel(int id);
        IEnumerable<Parcel> GetParcelsList(Func<Parcel, bool> predicate = null);
        Station GetStation(int id);
        IEnumerable<Station> GetStationsList(Func<Station, bool> predicate = null);
     }
}