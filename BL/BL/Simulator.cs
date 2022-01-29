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
        enum Maintenance { SearchingStation, OnWayToSation, Charging }
        private const int DELAY = 500; // half a second
        private const double SPEED = 1;// km/s
        

        internal Simulator(int droneId, Action update, Func<bool> checkStop, BL bl)
        {
            const double possibleDistance = (double)DELAY / 1000 * SPEED; // the max distance reached with the given speed for the every update
            Maintenance maintenanceStage = Maintenance.Charging; // charging stage 
            bool delivery = false; //// false- Associated, true - Collected

            ListDrone drone = bl.GetListDrone(droneId);
            if (drone.State == DroneState.Maintenance)
            {
                lock (bl)
                {
                    bl.SetDroneBatteryAndReturnCharge(drone);//gets the current battery
                }
            }
            if (drone.State == DroneState.Delivery)
            {
                lock (bl)
                {
                    delivery = bl.ListDroneToDrone(drone).Parcel.State;
                }
            }
            double distance = 0;// the distance left to reach
            Location destination = new Location(); // the current destansion of the drone

            while (checkStop())
            {
                Thread.Sleep(DELAY);
                switch (drone.State)
                {
                    case DroneState.Maintenance:
                        MaintenanceMode(bl, drone, possibleDistance, ref distance, ref destination, ref maintenanceStage);
                        break;
                    case DroneState.Available:
                        AvailableMode(droneId, bl, drone, ref distance, ref destination, ref maintenanceStage, ref delivery);
                        break;
                    case DroneState.Delivery:
                        DeliveryMode(droneId, bl, drone, possibleDistance, ref distance, ref destination, ref delivery);
                        break;
                }
                update();
            }

            // If the drone is searching for a place or on his way to charge it is not realy in maintenance mode
            if (drone.State == DroneState.Maintenance && maintenanceStage != Maintenance.Charging) 
                drone.State = DroneState.Available;
        }

        /// <summary>
        /// Responsible on drone actions while is avaliable
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="bl"></param>
        /// <param name="drone"></param>
        /// <param name="distance"></param>
        /// <param name="destination"></param>
        /// <param name="maintenanceStage"></param>
        /// <param name="delivery"></param>
        private void AvailableMode(int droneId, BL bl, ListDrone drone, ref double distance, ref Location destination, ref Maintenance maintenanceStage, ref bool delivery)
        {
            try
            {
                lock (bl)
                {
                    bl.LinkParcelToDroneBL(droneId);//trys to link a parcel to the drone
                }
            }
            catch (BlException exep)
            {
                if (exep.Message == "No availalable parcel!" || exep.Message == "Cannot carry any parcel!")
                {
                    return; //wait - charging won't help
                }
                else if (drone.Battery != 100) ; //Cannot fulfill the fly(not enough battery) -> send the drone to charge
                {
                    drone.State = DroneState.Maintenance;
                    maintenanceStage = Maintenance.SearchingStation;
                }
                return;
            }
            lock (bl)
            {
                destination = bl.ListDroneToDrone(drone).Parcel.PickUp;
                distance = bl.ListDroneToDrone(drone).Parcel.Distance;// distance to the sender
            }
            delivery = false;//Parcel associated
        }

        /// <summary>
        /// Responsible on drone actions while in maintenance
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="drone"></param>
        /// <param name="possibleDistance"></param>
        /// <param name="distance"></param>
        /// <param name="destination"></param>
        /// <param name="maintenanceStage"></param>
        private void MaintenanceMode(BL bl, ListDrone drone, double possibleDistance, ref double distance, ref Location destination, ref Maintenance maintenanceStage)
        {
            switch (maintenanceStage)
            {
                case Maintenance.SearchingStation:
                    try
                    {
                        lock (bl)
                        {
                            // distance to charging station, destination is the station location
                            distance = bl.SendDroneToCharge(drone.Id, true, out destination);
                        }
                    }
                    catch
                    {
                        break;//search again in the next round
                    }
                    maintenanceStage = Maintenance.OnWayToSation;
                    break;

                case Maintenance.OnWayToSation:
                    if (distance == 0)// arrived to charging station
                    {
                        maintenanceStage = Maintenance.Charging;
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
                    lock (bl)
                    {
                        if (drone.Battery == 100)//charged to max
                        {
                            bl.DroneRelease(drone.Id);
                            break;
                        }
                        drone.Battery = Min(drone.Battery + bl.GetElectricityChargePerSec() * DELAY / 1000, 100); //charging...
                    }
                    break;
            }
        }

        /// <summary>
        /// Responsible on drone actions while in delivery
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="bl"></param>
        /// <param name="drone"></param>
        /// <param name="possibleDistance"></param>
        /// <param name="distance"></param>
        /// <param name="destination"></param>
        /// <param name="delivery"></param>
        private void DeliveryMode(int droneId, BL bl, ListDrone drone, double possibleDistance,  ref double distance, ref Location destination, ref bool delivery)
        {
            switch (delivery)
            {
                case false://Associated
                    if (distance == 0)// arrived to sender
                    {
                        lock (bl)
                        {
                            double battery = drone.Battery;
                            bl.PickParcel(droneId); 
                            drone.Battery = battery;// battey updated to the real battery 'because Pick function is not for simulator
                            destination = bl.ListDroneToDrone(drone).Parcel.Destination;// receiver location
                            distance = bl.ListDroneToDrone(drone).Parcel.Distance;// distance to receiver
                        }
                        delivery = true;
                    }
                    else // on the way to sender
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
                    if (distance == 0)// arrived to receiver 
                    {
                        lock (bl)
                        {
                            double battery = drone.Battery;
                            bl.DeliverParcel(droneId);
                            drone.Battery = battery;//battey updated to the real battery 'because deliver function is not for simulator
                            drone.State = DroneState.Available;
                        }
                    }
                    else// on the way to receiver
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
        }

        /// <summary>
        /// changes the drone location to the distance from the detination
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="dest"></param>
        /// <param name="distance">in km</param>
        /// <param name="bl"></param>
        private void MoveToRealLocation(ListDrone drone, Location dest, double distance, BL bl)
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
