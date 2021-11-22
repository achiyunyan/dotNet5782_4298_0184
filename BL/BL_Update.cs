﻿using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void UpdateDrone(int id, string model)
        {
            IDAL.DO.Drone dalDrone;
            try//I can assume that every drone exists in both lists of drones
            {
                dalDrone = myDal.GetDrone(id);
            }
            catch(IDAL.DO.NotExistsException exec)
            {
                throw new BlException(exec.Message);
            }
            ListDrone dr=Drones.Find(st => st.Id == id);
            dalDrone.Model = model;
            myDal.UpdateDrone(dalDrone);//if we got so far so ther is is no concern of exeptions
            Drones.Remove(dr);
            dr.Model = model;
            Drones.Add(dr);
        }

        public void UpdateStation(int id, string name, int chargingSlots)
        {
            IDAL.DO.Station dalStation;
            try//I can assume that every drone exists in both lists of drones
            {
                dalStation = myDal.GetStation(id);
            }
            catch (IDAL.DO.NotExistsException exec)
            {
                throw new BlException(exec.Message);
            }
            if (name != "")
                dalStation.Name = name;
            if (chargingSlots != 0)
            {
                int freeSlots = chargingSlots - GetStation(id).DroneList.Count;
                if (freeSlots>=0)
                {
                    dalStation.ChargeSlots = freeSlots;
                }
                else
                {
                    throw new BlException("Too few charging slots!");
                }
            }
               
            myDal.UpdateStation(dalStation);//if we got so far so ther is is no concern of exeptions
        }

        public void UpdateCustomer(int id, string name, string phone)
        {
            IDAL.DO.Customer dalCustomer;
            try//I can assume that every drone exists in both lists of drones
            {
                dalCustomer = myDal.GetCustomer(id);
            }
            catch (IDAL.DO.NotExistsException exec)
            {
                throw new BlException(exec.Message);
            }
            if (name != "")
                dalCustomer.Name = name;
            if (phone != "")
                dalCustomer.Phone = phone;
            myDal.UpdateCustomer(dalCustomer);//if we got so far so ther is is no concern of exeptions
        }

        public void SendDroneToCharge(int id)
        {
            ListDrone drone;
            try
            {
                drone = Drones.Find(dr => dr.Id==id);
            }
            catch(IDAL.DO.NotExistsException exec)
            {
                throw new BlException(exec.Message);
            }
            if (drone.State != DroneState.Available)
                throw new BlException($"Drone {id} is not available!");
            
            if(myDal.GetStationsList().Any(st=>st.ChargeSlots>0))
            {
                
                double distanceToClose=default,tempDis;
                List<IDAL.DO.Station> dalStationList = (List<IDAL.DO.Station>)myDal.GetStationsList();

                IDAL.DO.Station closestDalStation = dalStationList.First(st => st.ChargeSlots>0);
                for (int i = 1; i < dalStationList.Count; i++)
                {
                    distanceToClose = DistanceBetweenTwoPoints(drone.Location.Latitude, drone.Location.Longitude, closestDalStation.Latitude, closestDalStation.Longitude);
                    tempDis = DistanceBetweenTwoPoints(drone.Location.Latitude, drone.Location.Longitude, dalStationList[i].Latitude, dalStationList[i].Longitude);
                    if (distanceToClose > tempDis && dalStationList[i].ChargeSlots > 0)
                        closestDalStation = dalStationList[i];
                }

                if (drone.Battery - ElectricityUsePerKmAvailable * distanceToClose < 0)
                    throw new BlException();

                drone.Location = new Location { Latitude = closestDalStation.Latitude, Longitude = closestDalStation.Longitude };
                drone.State = DroneState.Maintenance;
                drone.Battery -= ElectricityUsePerKmAvailable * distanceToClose;
                //update the station, which is a struct
                closestDalStation.ChargeSlots -= 1;
                myDal.AddDroneCharge(new IDAL.DO.DroneCharge
                {
                    DroneId = id,
                    StationId=closestDalStation.Id
                });
            }
            else
            {
                throw new BlException("No Free Charging Slots"); 
            }

        }
        public void DroneRelease(int id,int chargingTime)
        {
            ListDrone BlDrone= Drones.Find(st => st.Id == id);
            if (!Drones.Any(st => st.Id == id))
            {
                throw new BlException($"Drone: {id} not exists!!");
            }
            else 
            {
                if (BlDrone.State == DroneState.Maintenance)
                {
                    IDAL.DO.Station stationOfDrone = new IDAL.DO.Station();
                    BlDrone.Battery += chargingTime * ElectricityChargePerHour;
                    if (BlDrone.Battery > 100)
                        BlDrone.Battery = 100;
                    BlDrone.State = DroneState.Available;
                    foreach(IDAL.DO.DroneCharge dalDroneCharge in myDal.GetDroneCharges())
                    {
                        if (dalDroneCharge.DroneId == id)
                        {
                            stationOfDrone = myDal.GetStation(dalDroneCharge.StationId);
                            stationOfDrone.ChargeSlots += 1;
                            myDal.DeleteDroneCharge(dalDroneCharge);
                            return;
                        }
                    }
                }
                else
                {
                    throw new BlException($"Drone: {id} is not in charge!");
                }
                
            }
        }

        public void LinkParcelToDroneBL(int droneId)
        {
            if(Drones.Any(dr => dr.Id== droneId))
            {
                ListDrone BlDrone = Drones.Find(dr => dr.Id == droneId);
                if (BlDrone.State != DroneState.Available)
                    throw new BlException($"Drone: {droneId} not exists!");

                List<IDAL.DO.Parcel> parcels = ((List<IDAL.DO.Parcel>)myDal.GetParcelsList()).FindAll(par => par.Scheduled == DateTime.MinValue);
                if (parcels.Count == 0)
                    throw new BlException("No availalable parcel!");
                parcels = parcels.FindAll(par => (int)par.Weight <= (int)BlDrone.WeightCategory);
                if (parcels.Count == 0)
                    throw new BlException($"Drone {droneId} cannot carry any parcel!");
                parcels = parcels.FindAll(par => PossibleFly(droneId, par.Id));
                if (parcels.Count == 0)
                    throw new BlException($"Drone {droneId} cannot fulfill the fly(not enough battery)");
                IDAL.DO.Parcel bestParcel = BestParcel(parcels, droneId);
                BlDrone.State = DroneState.Delivery;
                BlDrone.ParcelId = bestParcel.Id;
                bestParcel.Scheduled = DateTime.Now;
                bestParcel.DroneId = droneId;
                myDal.UpdateParcel(bestParcel);
            }
            else
            {
                throw new BlException($"Drone: {droneId} not exists!");
            }
            
        }


        private IDAL.DO.Parcel BestParcel(List<IDAL.DO.Parcel> parlist,int droneId)
        {
            IDAL.DO.Parcel temp;
            Drone BlDrone = GetDrone(droneId);

                int max = 0;
            for (int i = 1; i < parlist.Count; i++)
            {
                if (CompareParcels(parlist[i], parlist[max]) < 0)//parlist[i]<parlist[max]
                    max = i;
                else if (CompareParcels(parlist[i], parlist[max]) == 0)
                {
                    Location parcel1Location = GetCustomer(parlist[i].SenderId).Location;
                    Location parcel2Location = GetCustomer(parlist[max].SenderId).Location;
                    double disFromDroneToParcel1 = DistanceBetweenTwoPoints(BlDrone.Location, parcel1Location);
                    double disFromDroneToParcel2 = DistanceBetweenTwoPoints(BlDrone.Location, parcel2Location);
                    if (disFromDroneToParcel1 < disFromDroneToParcel2)
                        max = i;
                }
            }
            return parlist[max];
            
       
        }
        private int CompareParcels(IDAL.DO.Parcel p1, IDAL.DO.Parcel p2)
        {
            if(p1.Priority.CompareTo(p2.Priority)!=0)
                return -1*p1.Priority.CompareTo(p2.Priority);
            else 
                return -1*p1.Weight.CompareTo(p2.Weight);
        }
        
        private bool PossibleFly(int droneId,int parcelId)
        {
            Drone BlDrone = GetDrone(droneId);
            Parcel BlParcel = GetParcel(parcelId);
            Location parcelLocation = GetCustomer(BlParcel.Sender.Id).Location;
            Location parcelDestination = GetCustomer(BlParcel.Receiver.Id).Location;
            double disFromDroneToParcel =DistanceBetweenTwoPoints(BlDrone.Location,parcelLocation);
            double disFromtSenderToReciver = DistanceBetweenTwoPoints(parcelLocation, parcelDestination);
            double disFromReciverTosStation = DistanceBetweenTwoPoints(parcelDestination, ClosestStationLocation(BlDrone.Location));
            double electricityNeeded = (disFromDroneToParcel + disFromReciverTosStation) * ElectricityUsePerKmAvailable + disFromtSenderToReciver * ElecriciryUsePerWeight(myDal.GetParcel(parcelId).Weight);
            return BlDrone.Battery >= electricityNeeded;
        }
    }
}
