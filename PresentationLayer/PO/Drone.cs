using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace PO
{
    class Drone : INotifyPropertyChanged
    {
        private BO.Drone internalDrone;
        public Drone()
        {
            internalDrone = new BO.Drone();
        }
        public Drone(BO.Drone drone)
        {
            internalDrone = drone;
        }
        public BO.Drone getInternal()
        {
            return new BO.Drone()
            {
                Id = internalDrone.Id,
                Model = internalDrone.Model,
                Battery = internalDrone.Battery,
                Location = internalDrone.Location,
                Parcel = internalDrone.Parcel,
                State=internalDrone.State,
                WeightCategory=internalDrone.WeightCategory
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #region properties
        public string Model
        {
            set
            {
                internalDrone.Model = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return internalDrone.Model;
            }
        }
        public int Id
        {
            set
            {
                internalDrone.Id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return internalDrone.Id;
            }
        }
        public BO.DroneState State
        {
            set
            {
                internalDrone.State = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return internalDrone.State;
            }
        }
        public Location Location
        {
            set
            {
                internalDrone.Location = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return internalDrone.Location;
            }
        }
        public double Battery
        {
            set
            {
                internalDrone.Battery = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return internalDrone.Battery;
            }
        }
        public BO.ParcelInTransit Parcel
        {
            set
            {
                internalDrone.Parcel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return internalDrone.Parcel;
            }
        }
        public BO.WeightCategory WeightCategory
        {
            set
            {
                internalDrone.WeightCategory = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return internalDrone.WeightCategory;
            }
        }
        #endregion        
    }
}
