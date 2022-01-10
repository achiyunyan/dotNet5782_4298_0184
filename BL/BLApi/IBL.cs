﻿using BO;
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
        void AddDrone(Drone blDrone, int stationId);
        void AddStation(Station blStation);
        void AddCustomer(Customer customer);
        void AddParcel(int senderId, int reciverId, int weight, int priority);
        void UpdateDrone(int id, string model);
        void UpdateStation(int id, string name, int chargingSlots);
        void UpdateCustomer(int id, string name, string phone);
        void SendDroneToCharge(int droneId);
        void DroneRelease(int id);
        void LinkParcelToDroneBL(int droneId);
        void PickParcel(int droneId);
        void DeliverParcel(int droneId);
        Station GetStation(int id);
        Drone GetDrone(int id);
        Customer GetCustomer(int id);
        Parcel GetParcel(int id);
        IEnumerable<ListStation> GetStationsList();
        IEnumerable<ListDrone> GetDronesList(Func<ListDrone, bool> predicate = null);
        IEnumerable<ListCustomer> GetCustomersList();
        IEnumerable<ListParcel> GetParcelsList(Func<DO.Parcel, bool> predicate = null);
        IEnumerable<ListParcel> GetNonLinkedParcelsList();
        IEnumerable<ListStation> GetStationsWithFreeSlotsList();
        void DeleteParcel(Parcel parcel);
        IEnumerable<ListParcel> GetFilteredParcelsList(DateTime? firstDate, DateTime? secondDate, object Sender, object Receiver, object Priority, object State, object Weight);
        void StartDroneSimulator(int id, Action update, Func<bool> checkStop);
    }
}