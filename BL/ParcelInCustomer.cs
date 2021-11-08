using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelInCustomer
    {
        public int Id { set; get; }
        public WeightCategories WeightCategory { set; get; }
        public Priorities Priority { set; get; }
        public ParcelStates State { set; get; }
        public CustomerInParcel Customer { set; get; }
    }
}
