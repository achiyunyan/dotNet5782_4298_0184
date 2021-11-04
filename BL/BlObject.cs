using BL.BO;
using System;

namespace BL
{
    public class BlObject : IBL
    {
        static DalObject.DalObject myDal = new DalObject.DalObject();
        public void AddStation(Station blStation)
        {
            IDAL.DO.Station dalStation = new IDAL.DO.Station()
            {
                Id = blStation.Id,
                Name = blStation.Name,
                ChargeSlots = blStation.ChargeSlots,
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
                throw new StationBlException(str);
            }
        }


    }
}
