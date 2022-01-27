using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
using BO;
using System.Threading;
using static BL.BL;
using Itinero.LocalGeo;


namespace BL
{
    internal class Simulator
    {
        enum Maintenance {SearchingStation, OnWayToSation, Charging }
        private const int DELAY = 500; // half a second
        private const double SPEED = 1;// km/s
        private const double possibleDistance = (double)DELAY / 1000 * SPEED;
        Maintenance maintenance = Maintenance.Charging;        
        bool delivery = false; //// false- Associated, true - Collected
        internal Simulator(int droneId, Action update, Func<bool> checkStop, BL bl)
        {
            ListDrone drone = bl.GetListDrone(droneId);
            if (drone.State == DroneState.Maintenance)
            {
                lock (bl)
                {
                    bl.SetDroneBatteryAndReturnCharge(drone);
                }
            }
            if (drone.State == DroneState.Delivery)
            {
                lock (bl)
                { 
                    delivery = bl.ListDroneToDrone(drone).Parcel.State;
                } 
            }
            double distance = 0;
            Location destination = new Location();
            while (checkStop())
            {                
                Thread.Sleep(DELAY);
                switch (drone.State)
                {
                    case DroneState.Maintenance:
                        switch (maintenance)
                        {
                            case Maintenance.SearchingStation:
                                try
                                {
                                    lock (bl)
                                    {
                                        distance = bl.SendDroneToCharge(drone.Id, true, out destination);
                                    }
                                }
                                catch
                                {
                                    break;//search again in the next round
                                }
                                maintenance = Maintenance.OnWayToSation;
                                break;
                            case Maintenance.OnWayToSation:
                                if (distance == 0)
                                {
                                    maintenance = Maintenance.Charging;
                                    break;
                                }
                                lock (bl)
                                {
                                    drone.Battery -= Min(distance, possibleDistance) * bl.ElectricityUsePerKmAvailable;
                                }                                
                                distance -= Min(distance, possibleDistance);
                                MoveToRealLocation(drone, destination, distance, bl);
                                break;
                            case Maintenance.Charging:
                                lock(bl)
                                {
                                    if(drone.Battery == 100)
                                    {
                                        bl.DroneRelease(drone.Id);
                                        break;
                                    }
                                    drone.Battery = Min(drone.Battery + bl.GetElectricityChargePerSec() * DELAY / 1000, 100);                                    
                                }
                                break;
                        }
                        break;
                    case DroneState.Available:
                        try
                        {
                            lock (bl)
                            {
                                bl.LinkParcelToDroneBL(droneId);
                            }
                        }
                        catch(BlException exep)
                        {
                            if (exep.Message == "No availalable parcel!" || exep.Message == "Cannot carry any parcel!")
                            {
                                break; //wait
                            }
                            else if (drone.Battery != 100); //Cannot fulfill the fly(not enough battery)
                            {
                                drone.State = DroneState.Maintenance;
                                maintenance = Maintenance.SearchingStation;
                            }
                            break;
                        }
                        lock (bl) 
                        {
                            destination = bl.ListDroneToDrone(drone).Parcel.PickUp;
                            drone.State = DroneState.Delivery;
                            distance = bl.ListDroneToDrone(drone).Parcel.Distance;
                        }
                        delivery = false;
                        break;
                    case DroneState.Delivery:
                        switch (delivery)
                        {
                            case false://Associated
                                if (distance == 0)
                                {
                                    lock (bl)
                                    {
                                        double battery = drone.Battery;
                                        bl.PickParcel(droneId);
                                        drone.Battery = battery;
                                        destination = bl.ListDroneToDrone(drone).Parcel.Destination;
                                        distance = bl.ListDroneToDrone(drone).Parcel.Distance;
                                    }
                                    delivery = true;
                                }
                                else
                                {
                                    lock (bl)
                                    {
                                        double x = Min(distance, possibleDistance) * bl.ElecriciryUsePerWeight(bl.ListDroneToDrone(drone).Parcel.WeightCategory);
                                        drone.Battery -= Min(distance, possibleDistance) * bl.ElecriciryUsePerWeight(bl.ListDroneToDrone(drone).Parcel.WeightCategory);
                                        distance -= Min(distance, possibleDistance);
                                    }
                                    MoveToRealLocation(drone, destination, distance, bl);
                                }
                                break;
                            case true://Collected
                                if (distance == 0)
                                {
                                    lock (bl)
                                    {
                                        double battery = drone.Battery;
                                        bl.DeliverParcel(droneId);
                                        drone.Battery = battery;
                                        drone.State = DroneState.Available;
                                    }
                                }
                                else
                                {
                                    lock (bl)
                                    {
                                        drone.Battery -= Min(distance, possibleDistance) * bl.ElecriciryUsePerWeight(bl.ListDroneToDrone(drone).Parcel.WeightCategory);
                                        distance -= Min(distance, possibleDistance);
                                    }
                                    MoveToRealLocation(drone, destination, distance, bl);
                                }
                                break;
                        }
                        break;
                }
                update();
            }
            if (drone.State == DroneState.Maintenance && maintenance != Maintenance.Charging)
                drone.State = DroneState.Available;
        }

        void MoveToRealLocation(ListDrone drone, Location dest, double distance, BL bl)
        {
            lock (bl)
            {
                Coordinate source = new Coordinate { Latitude = (float)drone.Location.Latitude, Longitude = (float)drone.Location.Longitude };
                Coordinate detination = new Coordinate { Latitude = (float)dest.Latitude, Longitude = (float)dest.Longitude };
                Line line = new Line(source, detination);
                Coordinate locationMovedTo = line.LocationAfterDistance((float)distance * 1000);
                drone.Location = new Location { Latitude = locationMovedTo.Latitude, Longitude = locationMovedTo.Longitude };
            }
        }
    }

}
