using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace PL.PO
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
        public bool IsValid { set; get; }
        public event PropertyChangedEventHandler PropertyChanged;
        #region properties
        public string Model
        {
            set
            {
                internalDrone.Model = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
            get
            {
                return internalDrone.WeightCategory;
            }
        }
        #endregion
        public bool checkValid()
        {
            return
                Id != 0 &&
                Model != "" &&
                Location != null;
        }
        protected void OnPropertyChanged(string propertyName = null)
        {
            IsValid = checkValid();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
