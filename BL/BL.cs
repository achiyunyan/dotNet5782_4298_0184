using IBL.BO;
using System;

namespace BL
{
    public class BL : IBL.IBL
    {
        
        static DalObject.DalObject myDal = new DalObject.DalObject();
        public void AddStation(Station blStation)
        {
            IDAL.DO.Station dalStation = new IDAL.DO.Station()
            {
                Id = blStation.Id,
                Name = blStation.Name,
                ChargeSlots = blStation.FreeChargeSlots,
                Latitude = blStation.Location.Latitude,
                Longitude = blStation.Location.Longitude
            };
            try
            {
                myDal.AddStation(dalStation);
            }
            catch (IDAL.DO.StationException stex)
            {
                //TODO
                string str = "bl ereceive exception: " + stex.Message;
                //throw new StationBlException(str);
            }
        }
        public void AddDrone(Drone blDrone)
        {
            IDAL.DO.Drone dalDrone = new IDAL.DO.Drone()
            {
                Id = blDrone.Id,
                Model = blDrone.Model,
                MaxWeight = (IDAL.DO.WeightCategories)blDrone.WeightCategory,
            };
            try
            {
                myDal.AddDrone(dalDrone);
            }
            catch (IDAL.DO.DroneException stex)
            {
                //TODO
                string str = "bl ereceive exception: " + stex.Message;
                //throw new StationBlException(str);
            }
        }
        public void AddCustomer(Customer blCustomer)
        {
            IDAL.DO.Customer dalCustomer = new IDAL.DO.Customer()
            {
                Id = blCustomer.Id,
            };
            try
            {
                myDal.AddCustomer(dalCustomer);
            }
            catch (IDAL.DO.CustomerException stex)
            {
                //TODO
                string str = "bl ereceive exception: " + stex.Message;
                //throw new StationBlException(str);
            }
        }



    }
}
