using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    public class Customer
    {
        private List<CustomerDelivery> outList = new List<CustomerDelivery>();
        private List<CustomerDelivery> inList = new List<CustomerDelivery>();

        public int Id { set; get; }
        public string Name { set; get; }
        public string Phone { set; get; }
        public Location Location { get; set; }

        public List<CustomerDelivery> OutDeliveries { get { return outList; } }
        public List<CustomerDelivery> InDeliveries { get { return inList; } }
    }
}
