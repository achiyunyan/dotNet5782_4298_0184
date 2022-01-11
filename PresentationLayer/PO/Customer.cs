using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace PO
{
    class Customer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        BO.Customer blCustomer;

        public Customer(BO.Customer sentCustomer)
        {
            blCustomer = sentCustomer;
        }

        public BO.Customer GetBlParcel()
        {
            return blCustomer;
        }
        #region properties
        public int Id
        {
            set
            {
                blCustomer.Id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return blCustomer.Id;
            }
        }
        public string Name
        {
            set
            {
                blCustomer.Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return blCustomer.Name;
            }
        }
        public string Phone
        {
            set
            {
                blCustomer.Phone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return blCustomer.Phone;
            }
        }
        public Location Location
        {
            set
            {
                blCustomer.Location = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            get
            {
                return blCustomer.Location;
            }
        }       
        public List<ParcelInCustomer> OutDeliveries
        {
            get
            {
                if(blCustomer.OutDeliveries.Count == 0)
                    return null;
                return blCustomer.OutDeliveries;
            }
        }
        public List<ParcelInCustomer> InDeliveries
        {
            get
            {
                if (blCustomer.InDeliveries.Count == 0)
                    return null;
                return blCustomer.InDeliveries;
            }
        }     
        #endregion
    }
}
