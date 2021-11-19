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
        void DeliverParcel(int parcelId);
        void freeDroneFromCharge(int droneId, int stationId);
        void LinkParcelToDrone(Parcel parcel, int droneId);
        void PickUpParcel(int parcelId);
        void SendDroneToCharge(int droneId, int stationId);
        public double DistanceBetweenTwoPoints(double lat1, double lon1, double lat2, double lon2);
    }
}