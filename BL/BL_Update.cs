using IBL.BO;
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
                dalStation.ChargeSlots = chargingSlots;
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
        {//אפשר לסכם שלא הבנתי...מחר יהיה בסדר:)
            ListDrone drone;
            try
            {
                drone = Drones.Find(dr => dr.Id==id);
            }
            catch(IDAL.DO.NotExistsException exec)
            {
                throw new BlException(exec.Message);
            }
            if(myDal.GetStationsList().Any(st=>st.ChargeSlots>0))
            {
                double distanceToClose=default,tempDis;
                List<IDAL.DO.Station> dalStationList = (List<IDAL.DO.Station>)myDal.GetStationsList();
                IDAL.DO.Station closestDalStation = dalStationList.First(st => st.ChargeSlots>0);
                for (int i = 1; i < dalStationList.Count; i++)
                {
                    distanceToClose = myDal.DistanceBetweenTwoPoints(drone.Location.Latitude, drone.Location.Longitude, closestDalStation.Latitude, closestDalStation.Longitude);
                    tempDis = myDal.DistanceBetweenTwoPoints(drone.Location.Latitude, drone.Location.Longitude, dalStationList[i].Latitude, dalStationList[i].Longitude);
                    if (distanceToClose > tempDis && dalStationList[i].ChargeSlots > 0)
                        closestDalStation = dalStationList[i];
                }
                drone.Location = new Location { Latitude = closestDalStation.Latitude, Longitude = closestDalStation.Longitude };
                drone.State = DroneState.Maintenance;
                drone.Battery -= (int)(ElectricityUsePerKmAvailable * distanceToClose);
                
            }
            else
            {
                throw new BlException("NoFreeChargingSlots"); 
            }

        }
    }
}
