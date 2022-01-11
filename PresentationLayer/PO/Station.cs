using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    class Station : INotifyPropertyChanged
    {
        private BO.Station station;
        public Station()
        {
            station = new BO.Station();
        }
        public Station(BO.Station getStation)
        {
            station = getStation;
        }
        public BO.Station getInternal()
        {
            return station;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #region properties        
        public int Id
        {
            set
            {
                station.Id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return station.Id;
            }
        }
        public string Name
        {
            set
            {
                station.Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return station.Name;
            }
        }
        public BO.Location Location
        {
            set
            {
                station.Location = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return station.Location;
            }
        }
        public int FreeChargeSlots
        {
            set
            {
                station.FreeChargeSlots = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return station.FreeChargeSlots;
            }
        }
        public List<BO.DroneInCharge> DronesList
        {
            get
            {
                if (station.DronesList.Count == 0)
                    return null;
                return station.DronesList;
            }
        }
        #endregion        
    }
}