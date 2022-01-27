using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DalApi
{
    public interface IDal
    {
        #region Add
        void AddCustomer(Customer addCustomer);
        void AddDrone(Drone addDrone);
        void AddDroneCharge(DroneCharge addDroneCharge);
        void AddParcel(Parcel addParcel);
        void AddStation(Station addStation);
        #endregion
        #region Delete
        void DeleteCustomer(Customer deleteCustomer);
        void DeleteDrone(Drone deleteDrone);
        void DeleteDroneCharge(DroneCharge deleteDroneCharge);
        void DeleteParcel(Parcel deleteParcel);
        void DeleteStation(Station deleteStation);
        #endregion
        #region Update
        void UpdateCustomer(Customer updateCustomer);
        void UpdateDrone(Drone updateDrone);
        void UpdateParcel(Parcel updateParcel);
        void UpdateStation(Station updateStation);
        #endregion
        #region Get Item
        Customer GetCustomer(int id);
        Parcel GetParcel(int id);
        Drone GetDrone(int id);
        Station GetStation(int id);
        #endregion
        #region Get List
        IEnumerable<Customer> GetCustomersList(Func<Customer, bool> predicate = null);
        IEnumerable<DroneCharge> GetDroneChargesList(Func<DroneCharge, bool> predicate = null);
        IEnumerable<Drone> GetDronesList(Func<Drone, bool> predicate = null);
        IEnumerable<Parcel> GetParcelsList(Func<Parcel, bool> predicate = null);
        IEnumerable<Station> GetStationsList(Func<Station, bool> predicate = null);
        #endregion
        #region Get Electricity Data
        double GetElectricityChargePerSec();
        double GetElectricityUsePerKmAvailable();
        double GetElectricityUsePerKmHeavy();
        double GetElectricityUsePerKmLight();
        double GetElectricityUsePerKmMedium();
        #endregion
    }
}