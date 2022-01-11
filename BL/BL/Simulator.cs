using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
using BO;
using System.Threading;
using static BL.BL;


namespace BL
{
    internal class Simulator
    {
        enum Maintenance {SearchingStation, OnWayToSation, Charging }
        private int DELAY = 500; // half a second
        private double SPEED = 1;// km/s
        Maintenance maintenance = Maintenance.Charging;
        internal Simulator(int droneId, Action update, Func<bool> checkStop, BL bl)
        {
            Drone drone;
            double distance = 0;
            double possibleDistance;
            Location stationLocation;
            while (checkStop())
            {
                drone = bl.GetDrone(droneId);
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
                                        stationLocation = bl.ClosestStationLocation(drone.Location);
                                    }
                                }
                                catch
                                {
                                    break;//search again in the next round
                                }
                                maintenance = Maintenance.OnWayToSation;
                                distance = bl.DistanceBetweenTwoPoints(stationLocation, drone.Location);
                                break;
                            case Maintenance.OnWayToSation:
                                if (distance < 0.005)
                                {
                                    maintenance = Maintenance.Charging;
                                    break;
                                }
                                possibleDistance = DELAY / 1000 * SPEED;
                                lock (bl)
                                {
                                    bl.UpdateDroneBattery(drone.Id, Min(distance, possibleDistance) * bl.ElecriciryUsePerWeight(drone.Parcel.WeightCategory));
                                }
                                distance -= Min(distance, possibleDistance);
                                break;
                            case Maintenance.Charging:
                                lock(bl)
                                {

                                }
                                break;
                        }
                        break;
                }
            }
        }
    }

}
