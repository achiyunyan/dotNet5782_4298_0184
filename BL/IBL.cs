﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlApi
{
    public interface IBL
    {
        void AddDrone(Drone blDrone, int stationId);
        void AddStation(Station blStation);
        void AddCustomer(Customer customer);
        void AddParcel(int senderId, int reciverId, int weight, int priority);
        void UpdateDrone(int id, string model);
        void UpdateStation(int id, string name, int chargingSlots);
        void UpdateCustomer(int id, string name, string phone);
        void SendDroneToCharge(int droneId);
        void DroneRelease(int id, int chargingTimes);  
        void LinkParcelToDroneBL(int droneId);
        void PickParcel(int droneId);
        void DeliverParcel(int droneId);
        Station GetStation(int id);
        Drone GetDrone(int id);
        Customer GetCustomer(int id);
        Parcel GetParcel(int id);
        IEnumerable<ListStation> GetStationsList(Func<DO.Station, bool> predicate = null);
        IEnumerable<ListDrone> GetDronesList(Func<ListDrone, bool> predicate = null);        
        IEnumerable<ListCustomer> GetCustomersList(Func<DO.Customer, bool> predicate = null);        
        IEnumerable<ListParcel> GetParcelsList(Func<DO.Parcel, bool> predicate = null, DateTime? firstDate = null, DateTime? secondDate = null);
        IEnumerable<ListParcel> GetNonLinkedParcelsList();
        IEnumerable<ListStation> GetStationsWithFreeSlotsList();
        void DeleteParcel(Parcel parcel);
    }
}