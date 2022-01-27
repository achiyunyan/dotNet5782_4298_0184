using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlApi
{
    public interface IBL
    {
        #region Add
        void AddDrone(Drone blDrone, int stationId);
        void AddStation(Station blStation);
        void AddCustomer(Customer customer);
        void AddParcel(int senderId, int reciverId, int weight, int priority);
        #endregion
        #region Update
        void UpdateDrone(int id, string model);
        void UpdateStation(int id, string name, int chargingSlots);
        void UpdateCustomer(int id, string name, string phone);
        #endregion
        #region Get Item
        Station GetStation(int id);
        Drone GetDrone(int id);
        Customer GetCustomer(int id);
        Parcel GetParcel(int id);
        #endregion
        #region Get List
        IEnumerable<ListStation> GetStationsList();
        IEnumerable<ListDrone> GetDronesList();
        IEnumerable<ListCustomer> GetCustomersList();
        IEnumerable<ListParcel> GetParcelsList();
        //filtered lists
        IEnumerable<ListParcel> GetNonLinkedParcelsList();
        IEnumerable<ListStation> GetStationsWithFreeSlotsList();
        IEnumerable<ListParcel> GetFilteredParcelsList(DateTime? firstDate, DateTime? secondDate, object Sender, object Receiver, object Priority, object State, object Weight);
        #endregion
        #region Drone Actions
        void SendDroneToCharge(int droneId);
        void DroneRelease(int id);
        void LinkParcelToDroneBL(int droneId);
        void PickParcel(int droneId);
        void DeliverParcel(int droneId);
        #endregion
        void DeleteParcel(Parcel parcel);
        void StartSimulator(int id, Action update, Func<bool> checkStop);
    }
}