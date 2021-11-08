using IDAL.DO;
using System.Collections.Generic;

namespace IDAL
{
    public interface IDal
    {
        #region Create  from CRUD
        void AddCustomer(Customer AddCustomer);
        void AddDrone(Drone AddDrone);
        void AddParcel(Parcel AddParcel);
        void AddStation(Station addStation);
        #endregion
        #region Request one from CRUD
        Customer GetCustomer(int id);
         Drone GetDrone(int id);
         Parcel GetParcel(int id);
        Station GetStation(int id);
        #endregion
        #region Request all from CRUD
        IEnumerable<Customer> GetCustomersList();
        IEnumerable<Drone> GetDronesList();
        IEnumerable<Parcel> GetParcelsList();
        IEnumerable<Station> GetStationsList();
        #endregion
        void DeliverParcel(int parcelId);
        void freeDroneFromCharge(int droneId, int stationId);
         void LinkParcelToDrone(Parcel parcel, int droneId);
        void PickUpParcel(int parcelId);
        void SendDroneToCharge(int droneId, int stationId);
    }
}