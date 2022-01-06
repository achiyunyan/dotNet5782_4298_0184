using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace PO
{
    class Parcel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        BO.Parcel blParcel;

        public Parcel(BO.Parcel sentParcel)
        {
            blParcel = sentParcel;
        }

        public BO.Parcel GetBlParcel()
        {
            return blParcel;
        }
        #region properties
        public int Id
        {
            set
            { 
                blParcel.Id = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); 
            }
            get 
            { 
                return blParcel.Id; 
            } 
        }
        public CustomerInParcel Sender
        {
            set
            {
                blParcel.Sender = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return blParcel.Sender;
            }
        }
        public CustomerInParcel Receiver
        {
            set
            {
                blParcel.Receiver = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return blParcel.Receiver;
            }
        }
        public WeightCategory WeightCategory
        {
            set
            {
                blParcel.WeightCategory = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return blParcel.WeightCategory;
            }
        }
        public Priority Priority
        {
            set
            {
                blParcel.Priority = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return blParcel.Priority;
            }
        }
        public DroneInParcel Drone
        {
            set
            {
                blParcel.Drone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return blParcel.Drone;
            }
        }
        public DateTime? Requested
        {
            set
            {
                blParcel.Requested = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return blParcel.Requested;
            }
        }
        public DateTime? Scheduled
        {
            set
            {
                blParcel.Scheduled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return blParcel.Scheduled;
            }
        }
        public DateTime? PickedUp
        {
            set
            {
                blParcel.PickedUp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return blParcel.PickedUp;
            }
        }
        public DateTime? Delivered
        {
            set
            {
                blParcel.Delivered = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return blParcel.Delivered;
            }
        }
        #endregion
    }
}
