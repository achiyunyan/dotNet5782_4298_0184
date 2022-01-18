using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace BL
{
    public partial class BL : BlApi.IBL
    {
        public void StartSimulator(int id, Action update, Func<bool> checkStop)
        {
            new Simulator(id, update, checkStop, this);
        }
        internal void UpdateDroneBattery(int id, double battery)
        {
            Drones.First(dr => dr.Id == id).Battery = battery;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(int id, string model)
        {
            DO.Drone dalDrone;
            try//I can assume that every drone exists in both lists of drones
            {
                lock (myDal)
                {
                    dalDrone = myDal.GetDrone(id);
                }
            }
            catch (DO.NotExistsException exec)
            {
                throw new BlException(exec.Message);
            }
            ListDrone dr = Drones.Find(st => st.Id == id);
            dalDrone.Model = model;
            lock (myDal)
            {
                myDal.UpdateDrone(dalDrone);//if we got so far so ther is is no concern of exeptions
            }
            Drones.Remove(dr);
            dr.Model = model;
            Drones.Add(dr);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int id, string name, int chargingSlots)
        {
            DO.Station dalStation;
            try
            {
                lock (myDal)
                {
                    dalStation = myDal.GetStation(id);
                }
            }
            catch (DO.NotExistsException exec)
            {
                throw new BlException(exec.Message);
            }
            if (name != "" && name != null)
                dalStation.Name = name;
            if (chargingSlots != 0)
            {
                int freeSlots = chargingSlots - GetStation(id).DronesList.Count;
                if (freeSlots >= 0)
                {
                    dalStation.ChargeSlots = freeSlots;
                }
                else
                {
                    throw new BlException("Too few charging slots!");
                }
            }
            lock (myDal)
            {
                myDal.UpdateStation(dalStation);//if we got so far so ther is is no concern of exeptions
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int id, string name, string phone)
        {
            DO.Customer dalCustomer;
            try//I can assume that every drone exists in both lists of drones
            {
                lock (myDal)
                {
                    dalCustomer = myDal.GetCustomer(id);
                }
            }
            catch (DO.NotExistsException exec)
            {
                throw new BlException(exec.Message);
            }
            if (name != "")
                dalCustomer.Name = name;
            if (phone != "")
                dalCustomer.Phone = phone;
            lock (myDal)
            {
                myDal.UpdateCustomer(dalCustomer);//if we got so far so ther is is no concern of exeptions
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendDroneToCharge(int id)
        {
            Location location;
            SendDroneToCharge(id, false, out location);
        }
        internal double SendDroneToCharge(int id, bool simulation, out Location stationLocation)
        {
            ListDrone drone;
            try
            {
                drone = Drones.Find(dr => dr.Id == id);
            }
            catch (DO.NotExistsException exec)
            {
                throw new BlException(exec.Message);
            }
            if (!simulation && drone.State != DroneState.Available)
                throw new BlException($"Drone {id} is not available!");
            lock (myDal)
            {
                if (!myDal.GetStationsList().Any(st => st.ChargeSlots > 0))
                {
                    throw new BlException("No Free Charging Slots");
                }
                double distanceToClose = default, tempDis;
                IEnumerable<DO.Station> dalStationList = myDal.GetStationsList();

                DO.Station closestDalStation = dalStationList.First(st => st.ChargeSlots > 0);
                for (int i = 1; i < dalStationList.Count(); i++)
                {
                    distanceToClose = DistanceBetweenTwoPoints(drone.Location.Latitude, drone.Location.Longitude, closestDalStation.Latitude, closestDalStation.Longitude);
                    tempDis = DistanceBetweenTwoPoints(drone.Location.Latitude, drone.Location.Longitude, dalStationList.ElementAt(i).Latitude, dalStationList.ElementAt(i).Longitude);
                    if (distanceToClose > tempDis && dalStationList.ElementAt(i).ChargeSlots > 0)
                        closestDalStation = dalStationList.ElementAt(i);
                }

                stationLocation = new Location { Latitude = closestDalStation.Latitude, Longitude = closestDalStation.Longitude };

                if (drone.Battery - ElectricityUsePerKmAvailable * distanceToClose < 0)
                    throw new BlException("Not enough battery");
                //update the station, which is a struct
                closestDalStation.ChargeSlots -= 1;
                myDal.UpdateStation(closestDalStation);
                myDal.AddDroneCharge(new DO.DroneCharge
                {
                    DroneId = id,
                    StationId = closestDalStation.Id,
                    StatrtTime = DateTime.Now
                });
                if (simulation)
                    return distanceToClose;
                drone.Location = new Location { Latitude = closestDalStation.Latitude, Longitude = closestDalStation.Longitude };
                drone.State = DroneState.Maintenance;
                drone.Battery -= ElectricityUsePerKmAvailable * distanceToClose;
                return 0;
            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneRelease(int id)
        {
            ListDrone BlDrone = Drones.Find(st => st.Id == id);
            if (!Drones.Any(st => st.Id == id))
            {
                throw new BlException($"Drone: {id} not exists!!");
            }
            else
            {
                if (BlDrone.State == DroneState.Maintenance)
                {
                    DO.Station stationOfDrone = new DO.Station();
                    BlDrone.State = DroneState.Available;
                    lock (myDal)
                    {
                        DO.DroneCharge dalDroneCharge = SetDroneBatteryAndReturnCharge(BlDrone);
                        stationOfDrone = myDal.GetStation(dalDroneCharge.StationId);
                        stationOfDrone.ChargeSlots += 1;
                        myDal.UpdateStation(stationOfDrone);
                        myDal.DeleteDroneCharge(dalDroneCharge);
                        return;
                    }
                }
                else
                {
                    throw new BlException($"Drone: {id} is not in charge!");
                }

            }
        }

        internal DO.DroneCharge SetDroneBatteryAndReturnCharge(ListDrone BlDrone)
        {
            lock (myDal)
            {
                DO.DroneCharge dalDroneCharge = myDal.GetDroneCharges().First(drch => drch.DroneId == BlDrone.Id);
                BlDrone.Battery += (DateTime.Now - dalDroneCharge.StatrtTime).Seconds * ElectricityChargePerSec;
                if (BlDrone.Battery > 100)
                    BlDrone.Battery = 100;
                return dalDroneCharge;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void LinkParcelToDroneBL(int droneId)
        {
            if (Drones.Any(dr => dr.Id == droneId))
            {
                ListDrone BlDrone = Drones.Find(dr => dr.Id == droneId);
                if (BlDrone.State != DroneState.Available)
                    throw new BlException($"Drone: {droneId} not available!");
                IEnumerable<DO.Parcel> allParcels;
                lock (myDal)
                {
                    allParcels = myDal.GetParcelsList();
                }
                List<DO.Parcel> parcels = new List<DO.Parcel>();
                bool noAvailalableParcel = true;
                bool cannotCarryAnyParcel = true;
                bool cannotFulfill = true;
                foreach (var par in allParcels)
                {
                    if (par.Scheduled == null)
                    {
                        noAvailalableParcel = false;
                        if ((int)par.Weight <= (int)BlDrone.WeightCategory)
                        {
                            cannotCarryAnyParcel = false;
                            if (PossibleFly(ListDroneToDrone(BlDrone), par))
                            {
                                cannotFulfill = false;
                                parcels.Add(par);
                            }
                        }
                    }
                }
                if (noAvailalableParcel)
                    throw new BlException("No availalable parcel!");
                if (cannotCarryAnyParcel)
                    throw new BlException("Cannot carry any parcel!");
                if (cannotFulfill)
                    throw new BlException("Cannot fulfill the fly(not enough battery)");
                DO.Parcel bestParcel = BestParcel(parcels, droneId);
                BlDrone.State = DroneState.Delivery;
                BlDrone.ParcelId = bestParcel.Id;
                bestParcel.Scheduled = DateTime.Now;
                bestParcel.DroneId = droneId;
                lock (myDal)
                {
                    myDal.UpdateParcel(bestParcel);
                }
            }
            else
            {
                throw new BlException($"Drone: {droneId} not exists!");
            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PickParcel(int droneId)
        {
            GetDrone(droneId);
            ListDrone BlDrone = Drones.Find(dr => dr.Id == droneId);
            DO.Parcel pickedParcel;
            lock (myDal)
            {
                pickedParcel = myDal.GetParcel(BlDrone.ParcelId);
            }
            if (BlDrone.State != DroneState.Delivery || pickedParcel.PickedUp != null)
                throw new BlException($"Drone {droneId} can't pick the parcel!");

            Customer sender = GetCustomer(pickedParcel.SenderId);
            BlDrone.Battery -= ElectricityUsePerKmAvailable * DistanceBetweenTwoPoints(BlDrone.Location, sender.Location);
            BlDrone.Location.Latitude = sender.Location.Latitude;
            BlDrone.Location.Longitude = sender.Location.Longitude;
            pickedParcel.PickedUp = DateTime.Now;
            lock (myDal)
            {
                myDal.UpdateParcel(pickedParcel);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliverParcel(int droneId)
        {
            GetDrone(droneId);
            ListDrone BlDrone = Drones.Find(dr => dr.Id == droneId);
            DO.Parcel deliveredParcel;
            lock (myDal)
            {
                deliveredParcel = myDal.GetParcel(BlDrone.ParcelId);
            }
            if (BlDrone.State != DroneState.Delivery || deliveredParcel.Delivered != null)
            {
                throw new BlException($"Drone {droneId} can't deliver the parcel!");
            }
            Customer reciver = GetCustomer(deliveredParcel.ReciverId);
            BlDrone.Battery -= ElecriciryUsePerWeight(deliveredParcel.Weight) * DistanceBetweenTwoPoints(BlDrone.Location, reciver.Location);
            BlDrone.Location.Latitude = reciver.Location.Latitude;
            BlDrone.Location.Longitude = reciver.Location.Longitude;
            BlDrone.State = DroneState.Available;
            BlDrone.ParcelId = 0;
            deliveredParcel.Delivered = DateTime.Now;
            lock (myDal)
            {
                myDal.UpdateParcel(deliveredParcel);
            }
        }

        private DO.Parcel BestParcel(IEnumerable<DO.Parcel> parlist, int droneId)
        {
            Drone BlDrone = GetDrone(droneId);
            return (from parcel in parlist
                    orderby parcel.Priority descending
                    orderby parcel.Weight descending
                    orderby DistanceBetweenTwoPoints(BlDrone.Location, GetCustomer(parcel.SenderId).Location)
                    select parcel).ElementAt(0);
        }

        private bool PossibleFly(Drone BlDrone, DO.Parcel dalParcel)
        {
            Parcel BlParcel = DALParcelToBL(dalParcel);
            Location parcelLocation = GetCustomer(BlParcel.Sender.Id).Location;
            Location parcelDestination = GetCustomer(BlParcel.Receiver.Id).Location;
            double disFromDroneToParcel = DistanceBetweenTwoPoints(BlDrone.Location, parcelLocation);
            double disFromtSenderToReciver = DistanceBetweenTwoPoints(parcelLocation, parcelDestination);
            double disFromReciverTosStation = DistanceBetweenTwoPoints(parcelDestination, ClosestStationLocation(BlDrone.Location));
            double electricityNeeded = (disFromDroneToParcel + disFromReciverTosStation) * ElectricityUsePerKmAvailable + disFromtSenderToReciver * ElecriciryUsePerWeight(dalParcel.Weight);
            return BlDrone.Battery >= electricityNeeded;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(Parcel parcel)
        {
            if (parcel.Drone == null)
            {
                lock (myDal)
                {
                    myDal.DeleteParcel(myDal.GetParcel(parcel.Id));
                }
            }
            else
                throw new BlException($"Parcel {parcel.Id} is already Scheduled!");
        }
    }
}
