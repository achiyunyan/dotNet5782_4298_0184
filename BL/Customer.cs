using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Customer
    {
        private List<ParcelInCustomer> outList = new List<ParcelInCustomer>();
        private List<ParcelInCustomer> inList = new List<ParcelInCustomer>();

        public int Id { set; get; }
        public string Name { set; get; }
        public string Phone { set; get; }
        public Location Location { get; set; }

        public List<ParcelInCustomer> OutDeliveries { get { return outList; } }
        public List<ParcelInCustomer> InDeliveries { get { return inList; } }
        public override string ToString()
        {
            string str;
            str = $" Customer Id:      {Id}\n" +
                  $" Name:             {Name}\n" +
                  $" Phone number:     {Phone}\n" +
                  $" Location:         {Location}\n";
            if (outList.Any())
            {
                str += " Outgoing parcels:\n\n";
                foreach (var item in outList)
                {
                    str += item.ToString() + '\n';
                }
            }
            if (inList.Any())
            {
                str += " Incoming parcels:\n\n";
                foreach (var item in inList)
                {
                    str += item.ToString() + '\n';
                }
            }
            return str;
        }
    }
}
